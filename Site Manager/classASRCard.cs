using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classASRCard
    {
        // FIELDS //
        private string cardPID = null;
        private int cardSlot = -1;
        private classASRPA[] cardPAs;

        // CONSTRUCTORS //
        public classASRCard(string inPID, int inSlot)
        {
            cardPID = inPID;
            cardSlot = inSlot;

            processPID();
        }

        // PROPERTIES //
        public classASRPA[] PA
        {
            get
            {
                return cardPAs;
            }
        }

        public string PID
        {
            get { return cardPID; }
        }

        // METHODS //
        public void AddPA(string inPID, int inSlot)
        {
            cardPAs[inSlot] = new classASRPA(inPID, inSlot);
        }

        // SUPPORT LOGIC //
        private void processPID()
        {
            switch (cardPID)
            {
                case "A9K-SIP-700":
                    cardPAs = new classASRPA[2];
                    break;
                case "A9K-MOD160-SE":
                    cardPAs = new classASRPA[2];
                    break;
            }
        }
    }
}
