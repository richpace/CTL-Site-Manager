using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public enum typeCircuit { Other, Ethernet, xSTS, xDS1, xDS0 }
    //public enum typeDevice { Legacy, ASR }
    public enum typePhysical { T3, SONET, GE }

    public class classCircuit
    {
        // FIELDS //
        private typeCircuit cxType;

        private string cxID;
        private string cxDevice = null;
        private string cxInterface = null;
        private string cxCustomer;

        //private typeDevice cxDeviceType;

        private int cxWidth = 1;
        private string cxMLID = null;
        private bool cxDiversity = false;
        private bool cxSubinterface = false;
        private string cxCInterface = null;
        private string cxPInterface = null;
        private typePhysical cxPInterfaceType;
        private bool cxChannelized = false;
        private string cxDS1 = null;
        private string cxSTS = null;
        private string cxSpeed = null;

        private string cxUnit = null;

        private bool cxAssigned = false;
        private DateTime cxMigrationDate;

        // CONSTRUCTORS //
        public classCircuit(string inCID)
        {
            cxID = inCID.ToUpper();

            if (cxID.Contains("DIV") == true) cxDiversity = true;

            switch (cxID.Substring(0, 3))
            {
                case "ETH":
                    cxType = typeCircuit.Ethernet;
                    break;
                case "OC1":
                    cxType = typeCircuit.xSTS;
                    cxPInterfaceType = typePhysical.SONET;
                    cxWidth = 12;
                    break;
                case "OC3":
                    cxType = typeCircuit.xSTS;
                    cxPInterfaceType = typePhysical.SONET;
                    cxWidth = 3;
                    break;
                case "DS3":
                    cxType = typeCircuit.xSTS;
                    break;
                case "DS1":
                    cxType = typeCircuit.xDS1;
                    break;
                case "DS0":
                    cxType = typeCircuit.xDS0;
                    break;
                default:
                    if (cxID.Substring(0, 2) == "IT") cxType = typeCircuit.xDS0;
                    break;
            }
        }

        // PROPERTIES //
        public string ID
        {
            get { return cxID; }
        }

        public string Device
        {
            get { return cxDevice; }
            set { cxDevice = value; }
        }

        public string Interface
        {
            get { return cxInterface; }
            set { processInterface(value); }
        }

        public string Customer
        {
            get { return cxCustomer; }
            set { cxCustomer = value; }
        }

        public typeCircuit Type
        {
            get { return cxType; }
        }

        public typePhysical PhysicalType
        {
            get { return cxPInterfaceType; }
        }

        public string Speed
        {
            get { return cxSpeed; }
            set { processSpeed(value); }
        }

        public string Physical
        {
            get { return cxPInterface; }
        }

        public string Carrier
        {
            get { return cxCInterface; }
        }

        public string DS1
        {
            get { return cxDS1; }
            set { cxDS1 = value; }
        }

        public string STS
        {
            get { return cxSTS; }
            set { cxSTS = value; }
        }

        public bool Channelized
        {
            get { return cxChannelized; }
            set { cxChannelized = value; }
        }

        public string MultilinkID
        {
            get { return cxMLID; }
            set { cxMLID = value; }
        }

        public bool Diversity
        {
            get { return cxDiversity; }
        }

        public int Width
        {
            get { return cxWidth; }
            set { cxWidth = value; }
        }

        public bool Subinterface
        {
            get { return cxSubinterface; }
        }

        public string Unit
        {
            get { return cxUnit; }
            set { cxUnit = value; }
        }

        public bool Assigned
        {
            get { return cxAssigned; }
            set { cxAssigned = value; }
        }

        public DateTime MigrationDate
        {
            get { return cxMigrationDate; }
            set { cxMigrationDate = value; }
        }

        // SUPPORT LOGIC //
        private void processUnit(string inUnit)
        {
            cxUnit = inUnit;
            if (inUnit == null)
            {
                cxAssigned = false;
                cxMigrationDate = DateTime.MinValue;
            }
        }

        private void processSpeed(string inSpeed)
        {
            // Do I really need any of this?  Need to check with Nick on DS0 config building...
            int Speed = Convert.ToInt32(inSpeed);
            int mod56 = Speed % 56;
            int mod64 = Speed % 64;

            cxSpeed = inSpeed;

            switch (cxType)
            {
                case typeCircuit.xDS0:
                    if (mod56 != mod64)
                    {
                        if (mod56 == 0)
                            cxWidth = Speed / 56;
                        else
                            cxWidth = Speed / 64;
                    }
                    else
                    {
                        throw new NotImplementedException("Ambiguous speed");
                    }
                    break;
            }
        }

        private void processInterface(string inInterface)
        {
            // Gah!  Can I clean this up?
            cxInterface = inInterface;

            switch (cxType)
            {
                case typeCircuit.Ethernet:
                    cxPInterfaceType = typePhysical.GE;
                    if (cxInterface.Contains(".") == true)
                    {
                        cxSubinterface = true;
                        cxCInterface = cxInterface.Remove(cxInterface.LastIndexOf("."));
                    }
                    else
                    {
                        cxCInterface = cxInterface;
                    }
                    cxPInterface = cxCInterface;
                    break;

                case typeCircuit.xSTS:
                    if (cxPInterfaceType != typePhysical.SONET) cxPInterfaceType = typePhysical.T3;

                    if (cxInterface.Split("/".ToCharArray()).GetLength(0) > 4)
                    {
                        //cxDeviceType = typeDevice.ASR;
                        cxPInterfaceType = typePhysical.SONET;
                        if (cxInterface.Contains(".") == true)
                        {
                            cxSubinterface = true;
                            cxCInterface = cxInterface.Remove(cxInterface.LastIndexOf("."));
                        }
                        else
                        {
                            cxCInterface = cxInterface;
                        }
                        cxPInterface = cxCInterface;

                    }
                    else
                    {
                        if (cxSpeed == null)
                        {
                            cxSubinterface = true;
                            cxCInterface = cxInterface.Remove(cxInterface.LastIndexOf("."));
                        }
                        else
                        {
                            cxCInterface = cxInterface;
                        }
                        if (cxCInterface.Contains(".") == true)
                        {
                            cxPInterfaceType = typePhysical.SONET;
                            cxPInterface = cxCInterface.Remove(cxCInterface.LastIndexOf("."));
                        }
                        else
                        {
                            cxPInterface = cxInterface;
                        }
                    }


                    break;
                case typeCircuit.xDS1:
                    cxPInterfaceType = typePhysical.T3;

                    if (isTDMSubinterface(cxInterface) == true)
                    {
                        cxSubinterface = true;
                        cxCInterface = cxInterface.Remove(cxInterface.LastIndexOf("."));
                    }
                    //else
                    //{
                    //    cxCInterface = cxInterface;
                    //}
                    cxDS1 = cxInterface.Remove(cxInterface.LastIndexOf(":"));
                    cxSTS = cxInterface.Remove(cxInterface.LastIndexOf(":"));
                    cxSTS = cxSTS.Remove(cxSTS.LastIndexOf("/"));

                    if (cxSTS.Contains(".") == true)
                    {
                        cxPInterfaceType = typePhysical.SONET;
                        cxPInterface = cxSTS.Remove(cxSTS.LastIndexOf("."));
                    }
                    else
                    {
                        cxPInterface = cxSTS;
                    }

                    break;

                case typeCircuit.xDS0:
                    cxPInterfaceType = typePhysical.T3;

                    if (isTDMSubinterface(cxInterface) == true)
                    {
                        cxSubinterface = true;
                        cxCInterface = cxInterface.Remove(cxInterface.LastIndexOf("."));
                    }

                    cxDS1 = cxInterface.Remove(cxInterface.LastIndexOf(":"));
                    cxSTS = cxDS1.Remove(cxDS1.LastIndexOf("/"));

                    if (cxSTS.Contains(".") == true)
                    {
                        cxPInterfaceType = typePhysical.SONET;
                        cxPInterface = cxSTS.Remove(cxSTS.LastIndexOf("."));
                    }
                    else
                    {
                        cxPInterface = cxSTS;
                    }

                    break;
            }
        }

        private string deriveSTS(string inDS1)
        {
            return inDS1.Remove(inDS1.LastIndexOf("/"));
        }

        private string deriveDS1(string inDS0)
        {
            return inDS0.Split(":".ToCharArray())[0];
        }

        private bool isTDMSubinterface(string inInterface)
        {
            int colon = inInterface.IndexOf(":");
            if (colon > 0)
            {
                string channels = inInterface.Substring(colon + 1, inInterface.Length - colon - 1);
                return channels.Contains(".");
            }

            return false;
        }
    }
}
