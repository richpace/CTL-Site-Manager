using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    class classIOXCard
    {
        // FIELDS //
        private string cardPID = null;
        private int cardSlot = -1;
        private classIOXPA[] cardPAs;

        // CONSTRUCTORS //
        public classIOXCard(string inPID, int inSlot)
        {
            cardPID = inPID;
            cardSlot = inSlot;

            processPID();
        }

        // PROPERTIES //
        public classIOXPA[] PA
        {
            get
            {
                return cardPAs;
            }
        }

        // METHODS //
        public void AddPA(string inPID, int inSlot)
        {
            cardPAs[inSlot] = new classIOXPA(inPID, inSlot);
        }

        // SUPPORT LOGIC //
        private void processPID()
        {
            switch (cardPID)
            {
                case "A9K-SIP-700":
                    cardPAs = new classIOXPA[2];
                    break;
                case "A9K-MOD160-SE":
                    cardPAs = new classIOXPA[2];
                    break;
            }
        }
    }
}
