using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Site_Manager
{
    public class classIOX
    {
        // FIELDS //
        private string ioxFN = null;
        private string ioxName = null;
        private DateTime ioxTimeStamp;
        private classIOXCard[] ioxCards = new classIOXCard[4];
        private classUnitDB ioxUnits = new classUnitDB();
        private bool ioxAssigned = false;

        // CONSTRUCTORS //
        public classIOX(string inFN)
        {
            parseCLIOutput(inFN);
        }

        // INDEXERS //

        // PROPERTIES //
        public string Filename
        {
            get
            {
                return ioxFN;
            }
        }

        public bool Assigned
        {
            get
            {
                return ioxAssigned;
            }

            set
            {
                ioxAssigned = value;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return ioxTimeStamp;
            }
        }

        public string Hostname
        {
            get
            {
                return ioxName;
            }
        }

        public classUnitDB Units
        {
            get
            {
                return ioxUnits;
            }
        }

        // METHODS //
        public classUnit NextOpenUnit(classUnit inUnit)
        {
            int unitWidth = inUnit.Width;
            string unitType = inUnit.Type;

            switch (unitType)
            {
                //case "STS-CH":
                //    for (int u = 0; u < Units.Count; u++)
                //    {
                //        if ((Units[u].Type == unitType) && (Units[u].Assigned == false))
                //        {
                //            return Units[u];
                //        }
                //    }
                //    break;
                case "STS-CL":
                    for (int u = 0; u < Units.Count; u++)
                    {
                        if ((Units[u].Type == unitType) && (Units[u].Assigned == false))
                        {
                            if (unitWidth == 1)
                            {
                                return Units[u];
                            }
                            else
                            {
                                int ch = 0;
                                string unitPort = Units[u].Port;

                                for (int s = 0; s<unitWidth; s++)
                                {
                                    if (Units[u + s].Assigned == false && Units[u + s].Port == unitPort)
                                    {
                                        ch++;
                                    }
                                    if (ch == unitWidth)
                                    {
                                        return Units[u];
                                    }
                                }
                            }
                        }
                    }
                    break;
                //case "GE":
                //    for (int u = 0; u < Units.Count; u++)
                //    {
                //        if ((Units[u].Type == unitType) && (Units[u].Assigned == false))
                //        {
                //            return Units[u];
                //        }
                //    }
                //    break;
                default:
                    for (int u = 0; u < Units.Count; u++)
                    {
                        if ((Units[u].Type == unitType) && (Units[u].Assigned == false))
                        {
                            return Units[u];
                        }
                    }
                    break;
            }

            return null;
        }

        public classUnit NextOpenUnit(string inType)
        {
            switch (inType)
            {
                case "STS-CH":
                    for (int u = 0; u < Units.Count; u++)
                    {
                        if ((Units[u].Type == inType) && (Units[u].Assigned == false))
                        {
                            //ioxUnits[u].Assign();
                            return Units[u];
                        }
                    }
                    break;
                case "STS-CL":
                    for (int u = 0; u < Units.Count; u++)
                    {
                        if ((Units[u].Type == inType) && (Units[u].Assigned == false))
                        {
                            //ioxUnits[u].Assign();
                            return Units[u];
                        }
                    }
                    break;
                case "GE":
                    for (int u = 0; u < Units.Count; u++)
                    {
                        if ((Units[u].Type == inType) && (Units[u].Assigned == false))
                        {
                            //ioxUnits[u].Assign();
                            return Units[u];
                        }
                    }
                    break;
            }

            return null;
        }

        public void AssignUnit(classUnit inUnit, int inWidth)
        {
            int unitIndex = Units.Contains(inUnit.Prefix);
            for (int u = 0; u< inWidth; u++)
            {
                Units[unitIndex + u].Assign();
            }
        }

        // SUPPORT LOGIC //

        private string getIOXName(string inFN)
        {
            //string s = null;
            string[] sa = inFN.Split("\\".ToCharArray());
            string s = sa[sa.GetUpperBound(0)];
            s = s.Replace("-show_inventory_all.txt", "");
            
            return s;
        }

        private string formatNAME(string inNAME)
        {
            string s = inNAME.Trim();
            s = s.Replace("module", "").Trim();
            return s;
        }

        private void auditUnits()
        {
            string unitPort = null;
            string unitPrefix = null;
            string unitType = null;

            for (int c = 0; c <= ioxCards.GetUpperBound(0); c++)
            {
                if (ioxCards[c] != null)
                {
                    for (int pa = 0; pa <= ioxCards[c].PA.GetUpperBound(0); pa++)
                    {
                        if (ioxCards[c].PA[pa] != null)
                        {
                            for (int p = 0; p <= ioxCards[c].PA[pa].Port.GetUpperBound(0); p++)
                            {
                                if (ioxCards[c].PA[pa].Port[p] != null)
                                {
                                    unitPort = "0/" + c.ToString() + "/" + pa.ToString() + "/" + p.ToString();
                                    for (int u = 0; u <= ioxCards[c].PA[pa].Port[p].Unit.GetUpperBound(0); u++)
                                    {
                                        if (ioxCards[c].PA[pa].Port[p].Unit[u] != null)
                                        {
                                            unitType = ioxCards[c].PA[pa].Port[p].Unit[u].Type;
                                            switch (unitType)
                                            {
                                                case "GE":
                                                    unitPrefix = "0/" + c.ToString() + "/" + pa.ToString() + "/" + p.ToString();
                                                    break;
                                                default:
                                                    unitPrefix = "0/" + c.ToString() + "/" + pa.ToString() + "/" + p.ToString() + "/" + (u + 1).ToString();
                                                    break;
                                            }
                                            ioxUnits.Add(unitType, unitPrefix, unitPort);
                                            //ioxUnits.Add(unitType, unitPrefix);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ioxUnits.Recount();
        }

        private void parseCLIOutput(string inFN)
        {
            ioxFN = inFN;
            ioxName = getIOXName(inFN);
            ioxTimeStamp = File.GetCreationTime(ioxFN);

            StreamReader ioxStream = File.OpenText(ioxFN);

            string strLine;
            string[] saLine;

            string strLine2;
            string[] saLine2;

            string cardName = null;

            while (ioxStream.EndOfStream == false)
            {
                strLine = ioxStream.ReadLine().Replace("\"","");
                saLine = strLine.Split(",".ToCharArray());

                if (strLine.Length > 0)
                {
                    strLine2 = saLine[0];
                    saLine2 = strLine2.Split(":".ToCharArray());

                    switch (saLine2[0].ToLower())
                    {
                        case "name":
                            cardName = formatNAME(saLine2[1].Trim());
                            break;
                        case "pid":
                            processPID(saLine2[1].Trim(), cardName);
                            break;
                    }
                }
            }
            auditUnits();
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
                        ioxCards[i] = new classIOXCard(inPID, i);
                        break;
                    case "A9K-SIP-700":
                        ioxCards[i] = new classIOXCard(inPID, i);
                        break;
                    case "A9K-MPA-20X1GE":
                        paID = inName.Split("/".ToCharArray())[2];
                        p = Convert.ToInt16(paID);
                        ioxCards[i].AddPA(inPID, p);
                        break;
                    case "SPA-2XCHOC12/DS0":
                        paID = inName.Split("/".ToCharArray())[2];
                        p = Convert.ToInt16(paID);
                        ioxCards[i].AddPA(inPID, p);
                        break;
                    case "SPA-1XCHOC48/DS3":
                        paID = inName.Split("/".ToCharArray())[2];
                        p = Convert.ToInt16(paID);
                        ioxCards[i].AddPA(inPID, p);
                        break;
                }
            }
            catch
            {

            }
        }

    }
}
