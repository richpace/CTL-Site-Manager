using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classASRPort
    {
        // FIELDS //
        private int portSlot = -1;
        private string portType;
        private classASRUnit[] portUnits;

        // CONSTRUCTORS //
        public classASRPort(int inPort, string inType)
        {
            portSlot = inPort;
            portType = inType;

            processType();
        }

        // PROPERTIES //
        public classASRUnit[] Unit
        {
            get
            {
                return portUnits;
            }
        }

        // SUPPORT LOGIC //
        private void processType()
        {
            switch (portType)
            {
                case "GE":
                    portUnits = new classASRUnit[1];
                    for (int i = 0; i < 1; i++)
                    {
                        //portUnits[i] = new classIOXUnit(i, "GE");
                        portUnits[i] = new classASRUnit(i, UnitType.GE);
                    }
                    break;
                case "CHOC12":
                    portUnits = new classASRUnit[12];
                    for (int i = 0; i < 12; i++)
                    {
                        //portUnits[i] = new classIOXUnit(i, "STS-CH");
                        portUnits[i] = new classASRUnit(i, UnitType.DCS);
                    }
                    break;
                case "CHOC48":
                    portUnits = new classASRUnit[48];
                    for (int i = 0; i < 48; i++)
                    {
                        //portUnits[i] = new classIOXUnit(i, "STS-CL");
                        portUnits[i] = new classASRUnit(i, UnitType.MON);
                    }
                    break;
            }
        }
    }
}
