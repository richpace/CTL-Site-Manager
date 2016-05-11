using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Cisco_Device;

namespace Site_Manager
{
    public class classIOS : classabstractDevice
    {
        // FIELDS //
        private DateTime iosTimeStamp;
        private string iosPreferredASR = null;
        private classUnitDB iosUnits = new classUnitDB();
        private classInterfaceDB iosInterfaces = new classInterfaceDB();

        // CONSTRUCTORS //
        public classIOS(string inFN)
        {
            devFN = inFN;
            iosTimeStamp = File.GetCreationTime(devFN);
            ProcessConfig();
        }

        // PROPERTIES //
        public string PreferredASR
        {
            get
            {
                return iosPreferredASR;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return iosTimeStamp;
            }
        }

        public classUnitDB Units
        {
            get
            {
                return iosUnits;
            }
        }

        public classInterfaceDB Circuits
        {
            get
            {
                return iosInterfaces;
            }
        }

        // METHODS //
        public void AssignUnit(classUnit inUnit)
        {
            Units[inUnit.Prefix].Assign();
        }

        public void AssignASR(string inASRName)
        {
            iosPreferredASR = inASRName;
        }

        public void ProcessConfig()
        {
            StreamReader iosStream = File.OpenText(devFN);

            parseIOSConfig(iosStream);
            iosUnits.Purge();
        }

        // SUPPORT LOGIC //
        private string correctUnit(classIOSInterface inSub)
        {
            if (inSub.Prefix.Split(".".ToCharArray()).GetUpperBound(0) > 1) return inSub.Unit.Remove(inSub.Unit.LastIndexOf("."));
            if (inSub.Prefix.Split(".".ToCharArray()).GetUpperBound(0) == 1) return inSub.Unit.Remove(inSub.Unit.LastIndexOf("."));

            return inSub.Unit;
        }

        public override void parseIOXConfig(StreamReader inSW)
        {
            throw new NotImplementedException();
        }

        public override void parseIOSConfig(StreamReader inSW)
        {

            classIOSInterface intNew;
            
            string[] strLine;

            strLine = inSW.ReadLine().Split();
            while (strLine[0] != "end")
            {
                switch (strLine[0])
                {
                    case "hostname":
                        devName = strLine[1];
                        break;

                    case "interface":
                        intNew = new classIOSInterface(strLine, inSW);
                        if (isValidCustomerInterface(intNew) == true)
                        {
                            if (intNew.SubInterface == true)
                            {
                                //correctUnit(intNew);
                                if (intNew.Unit != null &&  intNew.Unit == Circuits[Circuits.Count-1].Unit)
                                {
                                    Circuits[Circuits.Count-1].SubInterfaces.Add(intNew);
                                }
                            }
                            else
                            {
                                iosInterfaces.Add(intNew);
                                if (intNew.Unit != null)
                                {
                                    switch (intNew.Type)
                                    {
                                        case "SERIAL":
                                            validateController(intNew.Unit);
                                            break;
                                        case "POS":
                                            if (intNew.CircuitID.Contains("OC3") == true)
                                            {
                                                Units.Add("STS-CL", intNew.Unit, 3);
                                            }
                                            if (intNew.CircuitID.Contains("OC12") == true)
                                            {
                                                Units.Add("STS-CL", intNew.Unit, 12);
                                            }
                                            break;
                                        case "GIGABITETHERNET":

                                            {
                                                Units.Add("GE", intNew.Unit);
                                                //Units.AddUnitGE(intNew.ControllerID);
                                                Units[intNew.Unit].ActivateUnit();
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        break;

                    case "controller":
                        Units.ParseController(strLine, inSW);
                        break;
                }
                strLine = inSW.ReadLine().Split();
            }

            inSW.Dispose();
        }

        private bool isSubinterface(classIOSInterface inInt)
        {
            if (inInt.Prefix.Split(".".ToCharArray()).GetUpperBound(0) > 1) return true;
            if (inInt.Prefix.Split(".".ToCharArray()).GetUpperBound(0) == 1 && inInt.Type.ToUpper().Contains("ETHERNET") == true) return true;
            if (inInt.Prefix.Split(":".ToCharArray()).GetUpperBound(0) == 1 && inInt.Prefix.Split(":".ToCharArray())[1].Contains(".") == true) return true;

            if (Units.Contains(inInt.Prefix.Remove(inInt.Prefix.LastIndexOf("."))) != -1) return true;

            return false;
        }

        private bool isValidCustomerInterface(classIOSInterface inInt)
        {
            if (inInt.Shutdown == true) return false;

            if (inInt.VRF == "GLOBAL")
            {
                if (inInt.Encapsulation == null)
                {
                    return false;
                }
                else
                {
                    if (inInt.Encapsulation == "ppp")
                    {
                        if (inInt.MultilinkGroup == null) return false;
                    }
                }
            }

            if (inInt.Description == null)
            {
                return false;
            }
            else
            {
                switch (inInt.Description.Split(";".ToCharArray()).GetUpperBound(0))
                {
                    case 0:
                        return false;
                    case 1:
                        return false;
                    case 2:
                        return false;
                    case 3:
                        return false;
                    default:
                        if (inInt.Description.Split(";".ToArray())[4].Trim() != "cust") return false;
                        break;
                }
            }

            switch (inInt.Type)
            {
                case "MULTILINK":
                    return false;
                case "MFR":
                    return false;
            }

            return true;
        }

        private void findUnits()
        {
            //string intType;
            //string intPrefix;
            //string intCID;
            
            //for (int i= 0; i < devInterfaces.Count; i++)
            //{
            //    intType = devInterfaces[i].Type;
            //    intPrefix = devInterfaces[i].Prefix;
            //    intCID = devInterfaces[i].CircuitID;
                
            //    switch (intType)
            //    {
            //        case "GIGABITETHERNET":

            //            break;
            //        case "SERIAL":

            //            break;
            //        case "POS":

            //            break;
            //    }
            //}
        }

        private void validateController(string inController)
        {
            for (int i = 0; i < iosUnits.Count; i++)
            {
                if (iosUnits[i].Prefix == inController)
                {
                    iosUnits[i].ActivateUnit();
                }
            }


        }

        public override void writeIOSConfig()
        {
            throw new NotImplementedException();
        }

        public override void writeIOXConfig()
        {
            throw new NotImplementedException();
        }
    }
}
