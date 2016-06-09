using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classIOXPort
    {
        // FIELDS //
        private int portSlot = -1;
        private string portType;
        private classIOXUnit[] portUnits;

        // CONSTRUCTORS //
        public classIOXPort(int inPort, string inType)
        {
            portSlot = inPort;
            portType = inType;

            processType();
        }

        // PROPERTIES //
        public classIOXUnit[] Unit
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
                    portUnits = new classIOXUnit[1];
                    for (int i = 0; i < 1; i++)
                    {
                        //portUnits[i] = new classIOXUnit(i, "GE");
                        portUnits[i] = new classIOXUnit(i, UnitType.GE);
                    }
                    break;
                case "CHOC12":
                    portUnits = new classIOXUnit[12];
                    for (int i = 0; i < 12; i++)
                    {
                        //portUnits[i] = new classIOXUnit(i, "STS-CH");
                        portUnits[i] = new classIOXUnit(i, UnitType.CH);
                    }
                    break;
                case "CHOC48":
                    portUnits = new classIOXUnit[48];
                    for (int i = 0; i < 48; i++)
                    {
                        //portUnits[i] = new classIOXUnit(i, "STS-CL");
                        portUnits[i] = new classIOXUnit(i, UnitType.CL);
                    }
                    break;
            }
        }
    }
}
