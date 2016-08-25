using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Site_Manager
{
 public   class classASR
    {
        // FIELDS //
        private string asrFN = null;
        private string asrName = null;
        private DateTime asrTimeStamp;
        private classASRCard[] asrCards = new classASRCard[4];

        // CONSTRUCTORS //
        public classASR(string inFN)
        {
            parseCLI(inFN);
        }

        // PROPERTIES //
        public string Name
        {
            get { return asrName; }
        }

        public string Filename
        {
            get { return asrFN; }
        }

        public bool GE
        {
            get { return countGE(); }
        }

        public bool MON
        {
            get { return countMON(); }
        }

        public bool DCS
        {
            get { return countDCS(); }
        }

        public classASRCard[] Cards
        {
            get { return asrCards; }
        }

        // METHODS //

        // SUPPORT LOGIC //
        private void parseCLI(string inFN)
        {
            asrFN = inFN;
            asrName = getASRName(inFN);
            asrTimeStamp = File.GetCreationTime(asrFN);

            StreamReader ioxStream = File.OpenText(asrFN);

            string strLine;
            string[] saLine;

            string strLine2;
            string[] saLine2;

            string cardName = null;

            while (ioxStream.EndOfStream == false)
            {
                strLine = ioxStream.ReadLine().Replace("\"", "");
                saLine = strLine.Split(",".ToCharArray());

                if (strLine.Length > 0)
                {
                    strLine2 = saLine[0];
                    saLine2 = strLine2.Split(":".ToCharArray());

                    switch (saLine2[0].ToLower())
                    {
                        case "name":
                            cardName = formatCardName(saLine2[1].Trim());
                            break;
                        case "pid":
                            processPID(saLine2[1].Trim(), cardName);
                            break;
                    }
                }
            }
        }

        private void processPID(string inPID, string inName)
        {
            try
            {
                string slotID;
                int i;

                string paID;
                int p = -1;

                slotID = inName.Split("/".ToCharArray())[1];
                i = Convert.ToInt16(slotID);

                switch (inPID)
                {
                    case "A9K-MOD160-SE":
                        asrCards[i] = new classASRCard(inPID, i);
                        break;
                    case "A9K-SIP-700":
                        asrCards[i] = new classASRCard(inPID, i);
                        break;
                    case "A9K-MPA-20X1GE":
                        paID = inName.Split("/".ToCharArray())[2];
                        p = Convert.ToInt16(paID);
                        asrCards[i].AddPA(inPID, p);
                        break;
                    case "SPA-2XCHOC12/DS0":
                        paID = inName.Split("/".ToCharArray())[2];
                        p = Convert.ToInt16(paID);
                        asrCards[i].AddPA(inPID, p);
                        break;
                    case "SPA-1XCHOC48/DS3":
                        paID = inName.Split("/".ToCharArray())[2];
                        p = Convert.ToInt16(paID);
                        asrCards[i].AddPA(inPID, p);
                        break;
                }
            }
            catch
            {

            }
        }

        private string getASRName(string inFN)
        {
            string[] sa = inFN.Split("\\".ToCharArray());
            string s = sa[sa.GetUpperBound(0)];
            s = s.Replace("-show_inventory_all.txt", "");

            return s.ToUpper();
        }

        private string formatCardName(string inNAME)
        {
            string s = inNAME.Trim();
            s = s.Replace("module", "").Trim();
            return s;
        }

        private bool countGE()
        {
            for (int c = 0; c < asrCards.Count(); c++)
                if (asrCards[c] != null)
                    for (int p = 0; p < asrCards[c].PA.Count(); p++)
                        if (asrCards[c].PA[p] != null)
                            if (asrCards[c].PA[p].PID == "A9K-MPA-20X1GE")
                                return true;

            return false;
        }

        private bool countMON()
        {
            for (int c = 0; c < asrCards.Count(); c++)
                if (asrCards[c] != null)
                    for (int p = 0; p < asrCards[c].PA.Count(); p++)
                        if (asrCards[c].PA[p] != null)
                            if (asrCards[c].PA[p].PID == "SPA-1XCHOC48/DS3")
                                return true;

            return false;
        }

        private bool countDCS()
        {
            for (int c = 0; c < asrCards.Count(); c++)
                if (asrCards[c] != null)
                    for (int p = 0; p < asrCards[c].PA.Count(); p++)
                        if (asrCards[c].PA[p] != null)
                            if (asrCards[c].PA[p].PID == "SPA-2XCHOC12/DS0")
                                return true;

            return false;
        }
    }
}
