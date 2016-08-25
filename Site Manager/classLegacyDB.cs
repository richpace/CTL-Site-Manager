using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Site_Manager
{
    public class classLegacyDB : Dictionary<string, classCircuit>
    {
        // FIELDS //
        private string fnWB;

        private Dictionary<string, classUnit> wbUnits = new Dictionary<string, classUnit>();

        private int colDevice = 0;
        private int colInterface = 0;
        private int colDescription = 0;
        private int colCID = 0;
        private int colBW = 0;
        private int colStatus = 0;
        private int colInterfaceStatus = 0;
        private int colCustomer = 0;
        private int colMLMember = 0;

        // CONSTRUCTORS //

        // PROPERTIES //
        public Dictionary<string, classUnit> Units
        {
            get { return wbUnits; }
        }

        // METHODS //
        public void LoadWBData()
        {
            fnWB = getFN();
            if (fnWB != null && fnWB != "")
            {
                Clear();
                parseWBData();
                //deriveUnits();
            }
        }

        public void RemoveASR(string inDevice)
        {
            removeASR(inDevice);
            deriveUnits();
        }

        public classCircuit AddCircuit(string[] inData)
        {
            classCircuit CX = buildCX(inData);

            if (CX != null && this.ContainsKey(CX.ID) == false)
            {
                Add(CX.ID, CX);
                addUnit(CX);
            }
            return CX;
        }

        // SUPPORT LOGIC //
        private string getFN()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            dlgOpen.ShowDialog();

            return dlgOpen.FileName;
        }

        private void parseWBData()
        {
            Microsoft.Office.Interop.Excel.Application XL;
            Workbook wbWB;
            Worksheet wsWB;

            XL = new Microsoft.Office.Interop.Excel.Application();
            wbWB = XL.Workbooks.Open(fnWB);
            wsWB = wbWB.Worksheets["WB Data"];

            int rowXL = 1;
            deriveColumns(wsWB);

            string[] xlLine = new string[9];

            do
            {
                rowXL++;

                xlLine[0] = wsWB.Cells[rowXL, colCID].Value;
                xlLine[1] = wsWB.Cells[rowXL, colDevice].Value;
                xlLine[2] = wsWB.Cells[rowXL, colInterface].Value;
                xlLine[3] = wsWB.Cells[rowXL, colCustomer].Value;
                xlLine[4] = wsWB.Cells[rowXL, colStatus].Value;
                xlLine[5] = wsWB.Cells[rowXL, colInterfaceStatus].Value;
                xlLine[6] = wsWB.Cells[rowXL, colDescription].Value;
                if (wsWB.Cells[rowXL, colBW].Value != null) xlLine[8] = wsWB.Cells[rowXL, colBW].Value.ToString().Trim();
                xlLine[7] = wsWB.Cells[rowXL, colMLMember].Value;

                if (xlLine[1] != null) AddCircuit(xlLine);
            } while (xlLine[1] != null);
        }

        private void deriveColumns(Worksheet inWS)
        {
            colDevice = getColumn("device_z", inWS);
            colInterface = getColumn("card_port_z", inWS);
            colDescription = getColumn("descr1", inWS);
            colCID = getColumn("circ_name", inWS);
            colBW = getColumn("opsdb_bw_kbps", inWS);
            colStatus = getColumn("circ_status", inWS);
            colInterfaceStatus = getColumn("opsdb_adm_oper", inWS);
            colCustomer = getColumn("circ_cust", inWS);
            colMLMember = getColumn("member", inWS);

        }

        private int getColumn(string inHeader, Worksheet inWS)
        {
            int c = 0;

            do
            {
                c++;
                if (inWS.Cells[1, c].Value == inHeader) return c;
            } while (inWS.Cells[1, c].Value != null);

            return 0;
        }

        private classCircuit buildCX(string[] inData)
        {
            string xlCID = inData[0];
            string xlDevice = inData[1];
            string xlInterface = inData[2];
            string xlCustomer = inData[3];
            string xlStatus = inData[4];
            string xlInterfaceStatus = inData[5];
            string xlDescription = inData[6];
            string xlMLMember = inData[7];
            string xlBW = inData[8];

            if (xlStatus.ToUpper() == "INSVC" &&
                xlInterfaceStatus.ToUpper() == "UP/UP" &&
                xlDescription != null &&
                xlDescription.ToUpper().Contains("CUST") == true)
            {
                bool addCX = true;
                classCircuit newCX = new classCircuit(xlCID);

                newCX.Customer = xlCustomer;
                newCX.Device = xlDevice.ToUpper();
                if (xlBW != null) newCX.Speed = xlBW.ToString().Trim();

                // look for improvements here...
                if (xlMLMember != null && xlMLMember != "") 
                {
                    if (xlMLMember.Contains("/") == true)
                    {
                        if (xlInterface.ToUpper().Contains("MULTILINK") == true ||
                            xlInterface.ToUpper().Contains("MFR") == true)
                        {
                            newCX.Interface = xlMLMember;
                            newCX.MultilinkID = constructMLID(xlInterface, newCX.Device);
                            if (newCX.MultilinkID.Contains(".") == true) addCX = false;
                        }
                        else
                        {
                            newCX.Interface = xlInterface;
                        }
                    }
                    else
                    {
                        newCX.Interface = xlInterface;
                        newCX.MultilinkID = xlMLMember;
                    }
                }
                else
                {
                    newCX.Interface = xlInterface;
                }

                switch (newCX.Type)
                {
                    case typeCircuit.Ethernet:

                        break;
                    case typeCircuit.xSTS:
                        if (newCX.Subinterface == true) addCX = false;
                        break;
                    case typeCircuit.xDS1:
                        if (newCX.Subinterface == true) addCX = false;
                        break;
                    case typeCircuit.xDS0:
                        if (newCX.Subinterface == true) addCX = false;
                        break;
                }

                if (addCX == true) return newCX;
            }
            return null;
        }

        private string constructMLID(string inInterface, string inDevice)
        {
            string mlID = inInterface;

            if (mlID.ToUpper().Contains("MULTILINK") == true) mlID = inInterface.ToUpper().Replace("MULTILINK", "MTL");

            return inDevice + ":" + mlID;
        }

        private void deriveUnits()
        {
            wbUnits.Clear();

            foreach (KeyValuePair<string, classCircuit> CX in this)
                addUnit(CX.Value);
        }

        private void addUnit(classCircuit inCX)
        {
            string key = "";

            switch (inCX.Type)
            {
                case typeCircuit.Ethernet:
                    key = inCX.Physical;
                    key = key.ToUpper().Replace("GIGABITETHERNET", "");
                    break;
                case typeCircuit.xSTS:
                    if (inCX.Carrier == null) key = inCX.Interface;
                    else key = inCX.Carrier;
                    key = key.ToUpper().Replace("SERIAL", "");
                    key = key.ToUpper().Replace("POS", "");
                    break;
                case typeCircuit.xDS1:
                    if (inCX.Carrier == null) key = inCX.Interface.Remove(inCX.Interface.LastIndexOf(":"));
                    else key = inCX.Carrier.Remove(inCX.Interface.LastIndexOf(":"));
                    key = key.ToUpper().Replace("SERIAL", "");
                    break;
                case typeCircuit.xDS0:
                    key = inCX.DS1;
                    key = key.ToUpper().Replace("SERIAL", "");
                    break;
                default:
                    break;
            }

            key = inCX.Device + "*" + key;
            inCX.Unit = key;
            if (wbUnits.ContainsKey(key) == false)
            {
                classUnit U = new classUnit(inCX);
                wbUnits.Add(key, U);
            }
        }

        private void removeASR(string inDevice)
        {
            List<string> keys = new List<string>();

            foreach (KeyValuePair<string, classCircuit> CX in this)
                if (CX.Value.Device == inDevice)
                    keys.Add(CX.Key);

            foreach (string key in keys)
                this.Remove(key);
        }
    }
}
