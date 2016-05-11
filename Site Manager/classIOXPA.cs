using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classIOXPA
    {
        // FIELDS //
        private string paPID;
        private int paSlot = -1;
        private classIOXPort[] paPorts;

        // CONSTRUCTORS //
        public classIOXPA(string inPID, int inSlot)
        {
            paPID = inPID;
            paSlot = inSlot;

            processPID();
        }

        // PROPERTIES //
        public classIOXPort[] Port
        {
            get
            {
                return paPorts;
            }
        }

        // SUPPORT LOGIC //
        private void processPID()
        {
            switch (paPID)
            {
                case "SPA-2XCHOC12/DS0":
                    paPorts = new classIOXPort[2];
                    for (int i = 0; i < 2; i++)
                    {
                        paPorts[i] = new classIOXPort(i, "CHOC12");
                    }
                    break;
                case "SPA-1XCHOC48/DS3":
                    paPorts = new classIOXPort[1];
                    for (int i = 0; i < 1; i++)
                    {
                        paPorts[i] = new classIOXPort(i, "CHOC48");
                    }
                    break;
                case "A9K-MPA-20X1GE":
                    paPorts = new classIOXPort[20];
                    for (int i = 0; i < 20; i++)
                    {
                        paPorts[i] = new classIOXPort(i, "GE");
                    }
                    break;
            }
        }
    }
}
