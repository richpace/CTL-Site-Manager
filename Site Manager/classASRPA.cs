using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classASRPA
    {
        // FIELDS //
        private string paPID;
        private int paSlot = -1;
        private classASRPort[] paPorts;

        // CONSTRUCTORS //
        public classASRPA(string inPID, int inSlot)
        {
            paPID = inPID;
            paSlot = inSlot;

            processPID();
        }

        // PROPERTIES //
        public classASRPort[] Port
        {
            get
            {
                return paPorts;
            }
        }

        public string PID
        {
            get { return paPID; }
        }

        // SUPPORT LOGIC //
        private void processPID()
        {
            switch (paPID)
            {
                case "SPA-2XCHOC12/DS0":
                    paPorts = new classASRPort[2];
                    for (int i = 0; i < 2; i++)
                    {
                        paPorts[i] = new classASRPort(i, "CHOC12");
                    }
                    break;
                case "SPA-1XCHOC48/DS3":
                    paPorts = new classASRPort[1];
                    for (int i = 0; i < 1; i++)
                    {
                        paPorts[i] = new classASRPort(i, "CHOC48");
                    }
                    break;
                case "A9K-MPA-20X1GE":
                    paPorts = new classASRPort[20];
                    for (int i = 0; i < 20; i++)
                    {
                        paPorts[i] = new classASRPort(i, "GE");
                    }
                    break;
            }
        }
    }
}
