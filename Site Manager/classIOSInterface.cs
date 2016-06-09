using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Internet_Protocol;

using Cisco_Device;

namespace Site_Manager
{
    public class classIOSInterface : classabstractInterface
    {
        // FIELDS //
        private string intSubchannel = null;
        private bool intSubinterface = false;
        private string intCustomer;
        private string intCircuitID;
        private string intCORE;
        private bool intDiversity = false;
        private string intUnit = null;
        private DateTime intMigrationDate;

        private classInterfaceDB intSubinterfaces = new classInterfaceDB();

        // CONSTRUCTORS //
        public classIOSInterface(string[] inID, StreamReader inSR)
        {
            parseInterface(inID, inSR);
        }

        // PROPERTIES //
        public string SubChannel
        {
            get
            {
                return intSubchannel;
            }
        }

        public bool SubInterface
        {
            get
            {
                return intSubinterface;
            }
        }

        public classInterfaceDB SubInterfaces
        {
            get
            {
                return intSubinterfaces;
            }
        }

        public DateTime MigrationDate
        {
            get
            {
                return intMigrationDate;
            }
            set
            {
                intMigrationDate = value;
            }
        }

        public string Customer
        {
            get
            {
                return intCustomer;
            }
        }

        public string CircuitID
        {
            get
            {
                return intCircuitID;
            }
        }

        public string CORE
        {
            get
            {
                return intCORE;
            }
        }

        public bool Diversity
        {
            get
            {
                return intDiversity;
            }
        }

        public string Unit
        {
            get
            {
                return intUnit;
            }
        }

        // METHODS //

        // SUPPORT LOGIC //

        private bool isSubinterface(string[] inSA)
        {
            if (inSA.GetUpperBound(0) == 2)
            {
                if (inSA[2].Contains("point-to") == true) return true;

                if (inSA[1].Split(".".ToCharArray()).GetUpperBound(0) > 1) return true;
                if (inSA[1].Split(".".ToCharArray()).GetUpperBound(0) == 1 && inSA[1].ToUpper().Contains("ETHERNET") == true) return true;
                if (inSA[1].Split(":".ToCharArray()).GetUpperBound(0) == 1 && inSA[1].Split(":".ToCharArray())[1].Contains(".") == true) return true;
            }

            return false;
        }

        private string getUnit()
        {
            string s = null;

            switch (Type)
            {
                case "SERIAL":
                    if (intSubinterface == true)
                    {
                        s = intID.Remove(intID.LastIndexOf("."));
                    }
                    else
                    {
                        s = intID;
                    }
                    if (intID.Contains(":") == true)
                    {
                        s = s.Replace(intID.Substring(intID.LastIndexOf("/")), "");
                        s = s.ToUpper().Replace("SERIAL", "");
                    }
                    else
                    {
                        s = s.ToUpper().Replace("SERIAL", "");
                    }
                    break;
                case "GIGABITETHERNET":
                    if (intID.Contains(".") == true)
                    {
                        s = intID.Replace(intID.Substring(intID.LastIndexOf(".")), "");
                    }
                    else
                    {
                        s = intID;
                    }
                    s = s.ToUpper().Replace("GIGABITETHERNET", "");
                    break;
                case "POS":
                    if (intID.Contains(".") == false)
                    {
                        s = intID;
                        s = s.ToUpper().Replace("POS", "");
                    }
                    break;
            }

            return s;
        }

        private string isSubchannel()
        {
            if (intID.Split(":".ToCharArray())[1] != "0")
            {
                return intID.Split(":".ToCharArray())[0]; ;
            }
            else
            {
                return null;
            }
        }

        private void parseInterface(string[] inID, StreamReader inSR)
        {
            string strLine = inSR.ReadLine().Trim();
            string[] strArray = strLine.Split();
            intID = inID[1];

            intSubinterface = isSubinterface(inID);

            intUnit = getUnit();

            if (Type == "SERIAL" && intID.Contains(":") == true) intSubchannel = isSubchannel();

            while (strArray[0] != "!")
            {
                switch (strArray[0])
                {
                    case "no":
                        break;
                    case "description":
                        intDescription = strLine.Substring("description ".Length);
                        switch (intDescription.Split(";".ToCharArray()).GetUpperBound(0))
                        {
                            case 0:
                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            default:
                                if (intDescription.Split(";".ToCharArray())[4].Trim() == "cust")
                                {
                                    intCustomer = intDescription.Split(";".ToCharArray())[0].Trim();
                                    intCircuitID = intDescription.Split(";".ToCharArray())[1].Trim();
                                    if (intCircuitID.EndsWith("DIV") == true)
                                    {
                                        intDiversity = true;
                                    }
                                    intCORE = intDescription.Split(";".ToCharArray())[2].Trim();
                                }
                                break;
                        }
                        break;
                    case "encapsulation":
                        intEncapsulation = strArray[1];
                        break;
                    case "ip":
                        switch (strArray[1])
                        {
                            case "address":
                                intIPv4 = new classIPAddress(strArray[2], strArray[3]);
                                break;
                            case "igmp":
                                intIGMPVersion = strArray[3];
                                break;
                            case "pim":
                                intPIMMode = strArray[2];
                                break;
                        }
                        break;
                    case "ipv6":
                        switch (strArray[1])
                        {
                            case "address":
                                intIPv6 = strArray[2];
                                break;
                        }
                        break;
                    case "ppp":
                        if (strArray[1] == "multilink")
                        {
                            if (strArray.GetUpperBound(0) > 1)
                            {
                                switch (strArray[2])
                                {
                                    case "group":
                                        intMultilinkGroup = strArray[3];
                                        break;
                                    case "endpoint":
                                        intMultilinkEndpointString = strArray[4];
                                        break;
                                }
                            }
                            else
                            {

                            }
                        }
                        break;
                    case "service-policy":
                        switch (strArray[1])
                        {
                            case "input":
                                intQOSIn = strArray[2];
                                break;
                            case "output":
                                intQOSOut = strArray[2];
                                break;
                        }
                        break;
                    case "vrf":
                        intVRF = strArray[2];
                        break;
                    case "shutdown":
                        intShutdown = true;
                        break;
                }
                strLine = inSR.ReadLine().Trim();
                strArray = strLine.Split();
            }
        }
    }
}
