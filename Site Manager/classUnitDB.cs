using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.IO;

namespace Site_Manager
{
    public class classUnitDB : CollectionBase
    {
        // FIELDS //
        private int unitsGE = 0;
        private int unitsCL = 0;
        private int unitsCH = 0;

        // CONSTRUCTORS //

        // INDEXERS //
        public classUnit this[int indexUnit]
        {
            get
            {
                return (classUnit)List[indexUnit];
            }
        }

        public classUnit this[string nameUnit]
        {
            get
            {
                try
                {
                    return (classUnit)List[Contains(nameUnit)];
                }
                catch
                {
                    return null;
                }
            }
        }

        // PROPERTIES //
        public int GE
        {
            get
            {
                return unitsGE;
            }
        }

        public int CH
        {
            get
            {
                return unitsCH;
            }
        }

        public int CL
        {
            get
            {
                return unitsCL;
            }
        }

        // METHODS //
        //public void Add(string inType)
        //{
        //    addCount(inType);
        //}

        public void Add(string inType, string inPrefix)
        {
            addUnit(inType, inPrefix, 1);
        }

        public void Add(UnitType inType, string inPrefix)
        {
            addUnit(inType, inPrefix, 1);
        }

        public void Add(string inType, string inPrefix, string inPort)
        {
            addUnit(inType, inPrefix, inPort);
        }

        public void Add(UnitType inType, string inPrefix, string inPort)
        {
            addUnit(inType, inPrefix, inPort);
        }

        public void Add(string inType, string inPrefix, int inWidth)
        {
            addUnit(inType, inPrefix, inWidth);
        }

        public void Add(UnitType inType, string inPrefix, int inWidth)
        {
            addUnit(inType, inPrefix, inWidth);
        }

        //public void xAddUnitGE(string inPrefix)
        //{
        //    addUnitGE(inPrefix);
        //}

        //public void AddUnitPOS(string inPrefix, int inWidth)
        //{
        //    addUnitPOS(inPrefix, inWidth);
        //}

        public void ParseController(string[] inID, StreamReader inSR)
        {
            parseController(inID, inSR);
        }

        public void Purge()
        {
            purgeInactive();
        }

        public int Contains(string inPrefix)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Prefix == inPrefix)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Recount()
        {
            countUnits();
        }

        // SUPPORT LOGIC //
        private bool inList(string inPrefix)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Prefix == inPrefix)
                {
                    return true;
                }
            }
            return false;
        }

        private void addCount(string inType)
        {
            switch (inType)
            {
                case "GE":
                    unitsGE++;
                    break;
                case "STS-CH":
                    unitsCH++;
                    break;
                case "STS-CL":
                    unitsCL++;
                    break;
            }
        }

        private void parseController(string[] inID, StreamReader inSR)
        {
            string contType = inID[1];
            string contID = inID[2];

            switch (contType)
            {
                case "T3":
                    parseControllerT3(contID, inSR);
                    break;
                case "SONET":
                    parseControllerSONET(contID, inSR);
                    break;
            }
        }

        private void parseControllerT3(string inID, StreamReader inSR)
        {
            string stsPrefix = inID;
            string stsType = "STS-CH";
            UnitType stsType2 = UnitType.CH;

            string srLine;
            string[] srArray;

            do
            {
                srLine = inSR.ReadLine().Trim();
                srArray = srLine.Split();

                switch (srArray[0])
                {
                    case "no":
                        switch (srArray[1])
                        {
                            case "channelized":
                                stsType2 = UnitType.CL;
                                break;
                        }
                        break;
                }
            } while (srArray[0] != "!");
            Add(stsType2, stsPrefix);
            //addUnit(stsType, stsPrefix);
        }

        private void parseControllerSONET(string inID, StreamReader inSR)
        {
            string contPrefix = inID;
            string stsPrefix = null;
            string stsType = "STS-CL";
            UnitType stsType2 = UnitType.CL;

            string srLine;
            string[] srArray;

            do
            {
                srLine = inSR.ReadLine();
                srArray = srLine.Trim().Split();

                switch (srArray[0])
                {
                    case "au-4":
                        stsPrefix = contPrefix + "." + srArray[1] + "/" + srArray[3];
                        do
                        {
                            srLine = inSR.ReadLine();
                            srArray = srLine.Trim().Split();
                            switch (srArray[0])
                            {
                                case "tug-2":
                                    //Add(stsType, stsPrefix + "/" + srArray[1]);
                                    Add(stsType2, stsPrefix + "/" + srArray[1]);
                                    break;
                                case "mode":
                                    switch (srArray[1])
                                    {
                                        case "c-12":
                                            stsType = "STS-CH";
                                            stsType2 = UnitType.CH;
                                            break;
                                        case "t3":
                                            stsType = "STS-CL";
                                            stsType2 = UnitType.CL;
                                            break;
                                    }
                                    break;
                            }
                        } while (srLine.Trim() != "!");
                        break;
                    case "sts-1":
                        stsType = "STS-CL";
                        stsType2 = UnitType.CL;
                        stsPrefix = contPrefix + "." + srArray[1];
                        do
                        {
                            srLine = inSR.ReadLine();
                            srArray = srLine.Trim().Split();
                            switch (srArray[0])
                            {
                                case "mode":
                                    switch (srArray[1])
                                    {
                                        case "ct3":
                                            stsType = "STS-CH";
                                            stsType2 = UnitType.CH;
                                            break;
                                        case "t3":

                                            break;
                                    }
                                    break;
                            }

                            if (srLine == "!")
                            {
                                break;
                            }
                        } while (srLine != " !");
                        //Add(stsType, stsPrefix);
                        Add(stsType2, stsPrefix);
                        break;
                }
            } while (srLine != "!");
        }

        private void addUnit(UnitType inType, string inPrefix, int inWidth)
        {
            classUnit unitNew = new classUnit(inType, inPrefix, inWidth);

            if (this.Contains(unitNew.Prefix) == -1)
            {
                List.Add(unitNew);
                unitNew.ActivateUnit();
            }
        }

        private void addUnit(string inType, string inPrefix, int inWidth)
        {
            classUnit unitNew = new classUnit(inType, inPrefix, inWidth);

            if (this.Contains(unitNew.Prefix) == -1)
            {
                List.Add(unitNew);
                unitNew.ActivateUnit();
            }
        }

        private void addUnit(UnitType inType, string inPrefix, string inPort)
        {
            classUnit unitNew = new classUnit(inType, inPrefix, inPort);

            if (this.Contains(unitNew.Prefix) == -1)
            {
                List.Add(unitNew);
                unitNew.ActivateUnit();
            }
        }

        private void addUnit(string inType, string inPrefix, string inPort)
        {
            classUnit unitNew = new classUnit(inType, inPrefix, inPort);

            if (this.Contains(unitNew.Prefix) == -1)
            {
                List.Add(unitNew);
                unitNew.ActivateUnit();
            }
        }

        //private void addUnit(string inType, string inPrefix)
        //{
        //    classDeviceUnit unitNew = new classDeviceUnit(inType, inPrefix, 1);

        //    unitNew.ActivateUnit();
        //    if (this.Contains(unitNew.Prefix) == false) List.Add(unitNew);
        //    //addCount(inType);
        //}

        //private void xaddUnitGE(string inPrefix)
        //{
        //    string stsType = "GE";
        //    classDeviceUnit unitNew = new classDeviceUnit(stsType, inPrefix);

        //    unitNew.ActivateUnit();
        //    if (this.Contains(unitNew.Prefix) == false) List.Add(unitNew);
        //    //addCount(stsType);
        //}

        //private void xaddUnitPOS(string inPrefix, int inWidth)
        //{
        //    string stsType = "STS-CL";
        //    classDeviceUnit unitNew = new classDeviceUnit(stsType, inPrefix, inWidth);

        //    unitNew.ActivateUnit();
        //    List.Add(unitNew);
        //    //addCount(stsType);
        //}

        private void purgeInactive()
        {
            for (int i=this.Count-1; i >= 0; i--)
            {
                if (this[i].Active == false)
                {
                    List.RemoveAt(i);
                }
            }
            countUnits();
        }

        private void countUnits()
        {
            unitsCH = 0;
            unitsCL = 0;
            unitsGE = 0;

            for (int i = 0; i < this.Count; i++)
            {
                switch (this[i].Type)
                {
                    case "STS-CH":
                        unitsCH++;
                        break;
                    case "STS-CL":
                        unitsCL++;
                        break;
                    case "GE":
                        unitsGE++;
                        break;
                }
            }
        }
    }
}
