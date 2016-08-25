using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

//using System.Collections;

namespace Site_Manager
{
    public class classSiteDB //: CollectionBase
    {
        // FIELDS //
        private string dbFName = null;
        private string dbName = null;
        private bool dbDirty = false;

        private classLegacyDB siteCircuits = new classLegacyDB();
        private classASRDB siteASR = new classASRDB();
        private Dictionary<string, classMap> siteMaps = new Dictionary<string, classMap>();

        // CONSTRUCTORS //

        // PROPERTIES //
        public string Name
        {
            get
            {
                return dbName;
            }
            set
            {
                dbName = value;
            }
        }

        public classLegacyDB WB
        {
            get { return siteCircuits; }
        }

        public classASRDB ASR
        {
            get { return siteASR; }
        }

        public Dictionary<string, classMap> Maps
        {
            get { return siteMaps; }
        }

        public bool Dirty
        {
            get
            {
                return dbDirty;
            }
        }

        // METHODS //
        public void AddASR()
        {
            markDBDirty();
            string ASR = siteASR.Add();
            updateUnits(ASR);
            WB.RemoveASR(ASR);
        }

        public void AddWBData()
        {
            addWBData();
            Maps.Clear();
        }

        public void MapUnits(TreeNodeCollection inWB, TreeNodeCollection inIOX)
        {
            mapUnits();
        }

        public void Save()
        {
            if ((dbFName == null))
            {
                dbFName = getFNameSave();
            }

            if (dbFName != null)
            {
                string[] sa = dbFName.Split("\\".ToCharArray());
                string s = sa[sa.GetUpperBound(0)];

                Name = s.Remove(s.IndexOf(".sdb"));

                writeDB();
            }
        }

        public void SaveAs()
        {
            dbFName = getFNameSave();
            if (dbFName != null)
            {
                Save();
            }
        }

        public bool Open()
        {
            dbFName = getFNameOpen();
            if (dbFName != null)
            {

                readDB();
                string[] sa = dbFName.Split("\\".ToCharArray());
                string s = sa[sa.GetUpperBound(0)];

                Name = s.Remove(s.IndexOf(".sdb"));

                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateASRUnit(string inUID, bool inState)
        {
            classUnit uASR = ASR.Units[inUID];
            uASR.Assignable = inState;

            if (inState == false)
            {
                string keyMap = getMapKey(uASR);

                if (keyMap != null)
                {
                    Maps.Remove(keyMap);

                    WB.Units[keyMap].Assigned = false;

                    foreach (KeyValuePair<string, classCircuit> C in WB)
                    {
                        if (C.Value.Unit == keyMap)
                        {
                            C.Value.Assigned = false;
                        }
                    }
                }
            }
        }

        // SUPPORT LOGIC //
        private string getMapKey(classUnit inUnitASR)
        {
            foreach (KeyValuePair<string, classMap> M in Maps)
            {
                if (M.Value.ASR == inUnitASR.Device && M.Value.PrefixASR == inUnitASR.Prefix)
                {
                    return M.Key;
                }
            }
            return null;
        }

        private void addWBData()
        {
            siteCircuits.LoadWBData();

            foreach (KeyValuePair<string, classASR> A in ASR)
                siteCircuits.RemoveASR(A.Key);
        }

        private void addIOS()
        {
            //if (siteIOSDevices.Add() == true) markDBDirty();
        }

        private void updateUnits(string inASRName)
        {
            foreach (KeyValuePair<string, classUnit> UWB in WB.Units)
            {
                if (UWB.Value.Device == inASRName)
                {
                    string D = UWB.Key.Split("*".ToCharArray()).First();
                    string P = UWB.Key.Split("*".ToCharArray()).Last();
                    int PC = Convert.ToInt32(P.Remove(0, P.LastIndexOf("/") + 1));

                    string U = P.Remove(P.LastIndexOf("/"), P.Length - P.LastIndexOf("/"));

                    try
                    {
                        for (int C = 0; C < UWB.Value.Width; C++)
                        {
                            classUnit UASR = ASR.Units[inASRName + "*" + U + "/" + (PC + C).ToString()];
                        }
                        for (int C = 0; C < UWB.Value.Width; C++)
                        {
                            classUnit UASR = ASR.Units[inASRName + "*" + U + "/" + (PC + C).ToString()];
                            UASR.Assigned = true;
                        }
                    }
                    catch { }
                }
            }
        }

        private void markDBDirty()
        {
            dbDirty = true;
        }

        private void markDBClean()
        {
            dbDirty = false;
        }

        private void writeDB()
        {
            StreamWriter dbStream = new StreamWriter(dbFName);

            writeLegacy(dbStream);
            writeASR(dbStream);
            writeMaps(dbStream);
            //writeSchedule(dbStream);

            dbStream.Close();
            dbDirty = false;
        }

        private void writeLegacy(StreamWriter inSW)
        {
            writeCircuits(inSW);
        }

        private void writeCircuits(StreamWriter inSW)
        {
            foreach (KeyValuePair<string, classCircuit> C in WB)
            {
                string header = "CX";
                string delim = "::";
                string outLine = null;

                outLine = header + delim;
                outLine += C.Key + delim;
                outLine += C.Value.Device + delim;
                outLine += C.Value.Interface + delim;
                outLine += C.Value.Customer + delim;
                outLine += "INSVC" + delim;
                outLine += "UP/UP" + delim;
                outLine += "CUST" + delim;
                outLine += C.Value.MultilinkID + delim;
                outLine += C.Value.Speed + delim;

                outLine += C.Value.Unit + delim;
                outLine += C.Value.MigrationDate.ToShortDateString() + delim;

                inSW.WriteLine(outLine);
            }
        }

        private void writeASR(StreamWriter inSW)
        {
            writeDevices(inSW);
        }

        private void writeDevices(StreamWriter inSW)
        {
            string header = "ASR";
            string delim = "::";
            string outLine = null;

            foreach (KeyValuePair<string, classASR> A in ASR)
            {
                outLine = header + delim;
                outLine += A.Value.Filename + delim;
                inSW.WriteLine(outLine);
            }
        }

        private void writeMaps(StreamWriter inSW)
        {
            string header = "MAP";
            string delim = "::";
            string outLine = null;

            foreach (KeyValuePair<string, classMap> M in siteMaps)
            {
                outLine = header + delim;
                outLine += M.Value.Legacy + delim;
                outLine += M.Value.PrefixLegacy + delim;
                outLine += M.Value.ASR + delim;
                outLine += M.Value.PrefixASR + delim;
                inSW.WriteLine(outLine);
            }
        }

        private void readDB()
        {
            StreamReader dbStream = new StreamReader(dbFName);

            string[] delim = new string[] { "::" };
            string[] lineIn;

            while (dbStream.EndOfStream == false)
            {
                lineIn = dbStream.ReadLine().Split(delim, StringSplitOptions.None);

                switch (lineIn[0])
                {
                    case "CX":
                        readCircuit(lineIn);
                        break;
                    case "ASR":
                        readDevice(lineIn);
                        break;
                    case "MAP":
                        readMap(lineIn);
                        break;
                }
            }
        }

        private void readCircuit(string[] inLine)
        {
            //string cxUnit = inLine[10];
            //string cxDate = inLine[11];

            string[] cxLine = new string[9];

            Array.Copy(inLine, 1, cxLine, 0, 9);

            //inLine.CopyTo(cxLine, 1);

            classCircuit CX = WB.AddCircuit(cxLine);

            if (inLine[10] != null)
            {
                CX.Unit = inLine[10];
                //CX.Assigned = true;
            }
            else
            {

            }
            CX.MigrationDate = DateTime.Parse(inLine[11]);
        }

        private void readDevice(string[] inLine)
        {
            string devFN = inLine[1];

            classASR asrNew = new classASR(devFN);

            ASR.Add(devFN);
        }

        private void readMap(string[] inLine)
        {
            string mapL = inLine[1];
            string mapLP = inLine[2];
            string mapA = inLine[3];
            string mapAP = inLine[4];

            classUnit mapLU = WB.Units[mapL + "*" + mapLP];
            classUnit mapAU = ASR.Units[mapA + "*" + mapAP];

            mapLU.Assigned = true;
            mapAU.Assigned = true;

            mapUnit(mapLU, mapAU);
        }

        private string getFNameSave()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.AddExtension = true;
            dlg.DefaultExt = "sdb";
            dlg.OverwritePrompt = true;
            dlg.Filter = "Site Database Files (*.sdb)|*.sdb|All files(*.*)|*.*";
            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                return null;
            }
            else
            {
                return dlg.FileName;
            }
        }

        private string getFNameOpen(string inDeviceName)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Unable to find " + inDeviceName;
            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                return null;
            }
            else
            {
                return dlg.FileName;
            }
        }

        private string getFNameOpen()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "sdb";
            dlg.Filter = "Site Database Files (*.sdb)|*.sdb|All files(*.*)|*.*";
            dlg.ShowDialog();

            if (dlg.FileName=="")
            {
                return null;
            }
            else
            {
                return dlg.FileName;
            }
        }

        // SUPPORT LOGIC: CIRCUIT ASSIGNMENTS //
        private void mapUnits()
        {
            mapWide();
            mapDS0();
            mapDiversity();

            mapRemaining();
        }

        private void mapRemaining()
        {
            foreach (KeyValuePair<string, classUnit> uWB in WB.Units)
            {
                if (uWB.Value.Assigned == false)
                {
                    classUnit uASR = ASR.NextUnit(uWB.Value);
                    if (uASR != null) mapUnit(uWB.Value, uASR);
                }
            }
        }

        private void mapDS0()
        {
            foreach (KeyValuePair<string, classUnit> uWB in WB.Units)
            {
                if (uWB.Value.Type == UnitType.DCS && uWB.Value.Channelized == true && uWB.Value.Assigned == false)
                {
                    classUnit uASR = ASR.NextDS0();
                    if (uASR != null) mapUnit(uWB.Value, uASR);

                }
            }
        }

        private void mapDiversity()
        {
            SortedSet<string> divCustomerList = new SortedSet<string>();

            foreach (KeyValuePair<string,classCircuit> CX in WB)
                if (CX.Value.Diversity == true && divCustomerList.Contains(CX.Value.Customer) == false)
                        divCustomerList.Add(CX.Value.Customer);

            foreach(string idCustomer in divCustomerList)
                foreach (KeyValuePair<string, classCircuit> CX in WB)
                    if (CX.Value.Customer == idCustomer && CX.Value.Assigned == false)
                    {
                        classUnit uLegacy = WB.Units[CX.Value.Unit];
                        if (uLegacy.Assigned == false)
                        {
                            classUnit uASR = ASR.NextDIV(uLegacy.Type);
                            if (uASR != null) mapUnit(uLegacy, uASR);
                        }
                    }
        }

        private void mapWide()
        {
            foreach (KeyValuePair <string, classUnit> uWB in WB.Units)
            {
                if (uWB.Value.Width == 12 && uWB.Value.Assigned == false)
                {
                    classUnit uASR = ASR.NextUnit(uWB.Value);
                    if (uASR != null) mapUnit(uWB.Value, uASR);
                }
            }

            foreach (KeyValuePair<string, classUnit> uWB in WB.Units)
            {
                if (uWB.Value.Width == 3 && uWB.Value.Assigned == false)
                {
                    classUnit uASR = ASR.NextUnit(uWB.Value);
                    if (uASR != null) mapUnit(uWB.Value, uASR);
                }
            }
        }

        private void mapUnit(classUnit inUnitWB, classUnit inUnitASR)
        {
            inUnitWB.Assigned = true;
            inUnitASR.Assigned = true;

            foreach (KeyValuePair<string,classCircuit> CX in WB)
                if (CX.Value.Unit == inUnitWB.ID) CX.Value.Assigned = true;

            Maps.Add(inUnitWB.ID, new classMap(inUnitWB.Device, inUnitWB, inUnitASR.Device, inUnitASR));
        }
    }
}
