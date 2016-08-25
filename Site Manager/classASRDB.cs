using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Site_Manager
{
    public class classASRDB : SortedDictionary<string, classASR>
    {
        // FIELDS //
        private SortedDictionary<string, classUnit> units = new SortedDictionary<string, classUnit>();

        private SortedDictionary<int, string> divGE = new SortedDictionary<int, string>();
        private SortedDictionary<int, string> divSTS = new SortedDictionary<int, string>();
        private SortedDictionary<int, string> ds0 = new SortedDictionary<int, string>();

        private int nextDIVSTS = 0;
        private int nextDIVGE = 0;
        private int nextDS0 = 0;

        // CONSTRUCTORS //

        // PROPERTIES //
        public SortedDictionary<string, classUnit> Units
        {
            get { return units; }
        }

        // METHODS //
        public string Add()
        {
            string fnASR = getFN();

            if (validFN(fnASR) == true)
            {
                classASR asrNew = Add(fnASR);
                if (asrNew != null) return asrNew.Name;
            }

            return null;
        }

        public classASR Add(string inASR)
        {
            classASR asrNew = new classASR(inASR);

            if (this.ContainsKey(asrNew.Name) == false)
            {
                Add(asrNew.Name, asrNew);
                auditASRUnits(asrNew);
                return asrNew;
            }

            return null;
        }

        public void UnitAssignable(string inUID, bool inAssignable)
        {
            classUnit u = units[inUID];

            u.Assignable = inAssignable;
            if (u.Assignable == false)
            {
                u.Assigned = false;
            }
        }

        public classUnit NextDS0()
        {
            if (units[ds0[nextDS0]].Assigned == false)
            {
                return units[ds0[nextDS0]];
            }
            else
            {
                nextDS0++;
                if (nextDS0 >= ds0.Count) nextDS0 = 0;
                return NextDS0();
            }
        }

        public classUnit NextDIV(UnitType inType)
        {
            switch (inType)
            {
                case UnitType.GE:
                    foreach (KeyValuePair<string, classUnit> U in units )
                    {
                        if (U.Key.Contains(divGE[nextDIVGE]) == true && U.Value.Assigned == false)
                        {
                            nextDIVGE++;
                            if (nextDIVGE >= divGE.Count) nextDIVGE = 0;
                            return U.Value;
                        }
                    }
                    return null;

                default:
                    foreach (KeyValuePair<string, classUnit> U in units)
                    {
                        if (U.Key.Contains(divSTS[nextDIVSTS]) == true && U.Value.Assigned == false)
                        {
                            nextDIVSTS++;
                            if (nextDIVSTS >= divSTS.Count) nextDIVSTS = 0;
                            return U.Value;
                        }
                    }
                    return null;
            }
        }

        public classUnit NextUnit(classUnit inUnit)
        {
            return getNextUnit(inUnit);
        }

        // SUPPORT LOGIC //
        private string getFN()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select IOX Device Show Inventory All file...";
            dlg.ShowDialog();
            return dlg.FileName;
        }

        private bool validFN(string inFN)
        {
            if (inFN.Length > 0)
            {
                if (inFN.ToLower().Contains("show_inventory_all") == true) return true;
            }
            return false;
        }

        private void auditASRUnits(classASR inASR)
        {
            string unitPort = null;
            string unitPrefix = null;
            UnitType unitType;
            classUnit newUnit;

            for (int c = 0; c <= inASR.Cards.GetUpperBound(0); c++)
            {
                if (inASR.Cards[c] != null)
                {
                    string cardID = inASR.Name + "*0/" + c.ToString();
                    if (inASR.Cards[c].PID.ToUpper().Contains("SIP") == true)
                    {
                        divSTS.Add(divSTS.Count, inASR.Name + "*" + "0/" + c.ToString());
                    }
                    if (inASR.Cards[c].PID.ToUpper().Contains("MOD") == true)
                    {
                        divGE.Add(divGE.Count, inASR.Name + "*" + "0/" + c.ToString());
                    }
                    for (int pa = 0; pa <= inASR.Cards[c].PA.GetUpperBound(0); pa++)
                    {
                        if (inASR.Cards[c].PA[pa] != null)
                        {
                            for (int p = 0; p <= inASR.Cards[c].PA[pa].Port.GetUpperBound(0); p++)
                            {
                                if (inASR.Cards[c].PA[pa].Port[p] != null)
                                {
                                    unitPort = "0/" + c.ToString() + "/" + pa.ToString() + "/" + p.ToString();

                                    for (int u = 0; u <= inASR.Cards[c].PA[pa].Port[p].Unit.GetUpperBound(0); u++)
                                    {
                                        if (inASR.Cards[c].PA[pa].Port[p].Unit[u] != null)
                                        {
                                            unitType = inASR.Cards[c].PA[pa].Port[p].Unit[u].Type;
                                            switch (unitType)
                                            {
                                                case UnitType.GE:
                                                    unitPrefix = "0/" + c.ToString() + "/" + pa.ToString() + "/" + p.ToString();
                                                    newUnit = new classUnit(unitType, unitPrefix, unitPort);
                                                    newUnit.Device = inASR.Name;
                                                    newUnit.Card = inASR.Cards[c].PID.ToUpper();
                                                    units.Add(inASR.Name + "*" + unitPrefix, newUnit);
                                                    break;

                                                case UnitType.DCS:
                                                    for (int ds1 = 1; ds1 <= 28; ds1++)
                                                    {
                                                        unitPrefix = "0/" + c.ToString() + "/" + pa.ToString() + "/" + p.ToString() + "/" + (u + 1).ToString() + "/" + ds1.ToString();
                                                        newUnit = new classUnit(unitType, unitPrefix, unitPort);
                                                        newUnit.Device = inASR.Name;
                                                        newUnit.Card = inASR.Cards[c].PID.ToUpper();
                                                        units.Add(inASR.Name + "*" + unitPrefix, newUnit);
                                                        ds0.Add(ds0.Count, inASR.Name + "*" + unitPrefix);
                                                    }
                                                    break;

                                                case UnitType.MON:
                                                    unitPrefix = "0/" + c.ToString() + "/" + pa.ToString() + "/" + p.ToString() + "/" + (u + 1).ToString();
                                                    newUnit = new classUnit(unitType, unitPrefix, unitPort);
                                                    newUnit.Device = inASR.Name;
                                                    newUnit.Card = inASR.Cards[c].PID.ToUpper();
                                                    units.Add(inASR.Name + "*" + unitPrefix, newUnit);
                                                    break;

                                                default:
                                                    throw new Exception("Unexpected situation...");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private classUnit getNextUnit(classUnit inUnit)
        {
            foreach (KeyValuePair<string, classUnit> UNIT in units)
            {
                if (UNIT.Value.Type == inUnit.Type && UNIT.Value.Assigned == false && UNIT.Value.Assignable == true)
                {
                    try
                    {
                        for (int w = 0; w < inUnit.Width; w++)
                        {
                            string prefixCarrier = UNIT.Value.Prefix.Remove(UNIT.Value.Prefix.LastIndexOf("/") + 1);
                            int channel = int.Parse(UNIT.Value.Prefix.Substring(UNIT.Value.Prefix.LastIndexOf("/") + 1));
                            string prefixUnit = prefixCarrier + channel.ToString();

                            classUnit tempUnit = units[UNIT.Value.Device + "*" + prefixUnit];
                        }

                        for (int w = 0; w < inUnit.Width; w++)
                        {
                            string prefixCarrier = UNIT.Value.Prefix.Remove(UNIT.Value.Prefix.LastIndexOf("/") + 1);
                            int channel = int.Parse(UNIT.Value.Prefix.Substring(UNIT.Value.Prefix.LastIndexOf("/") + 1));
                            string prefixUnit = prefixCarrier + channel.ToString();

                            classUnit U = units[UNIT.Value.Device + "*" + prefixUnit];
                            U.Assigned = true;
                        }
                        UNIT.Value.Width = inUnit.Width;
                        return UNIT.Value;
                    }

                    catch
                    {
                        break;
                    }
                }
            }
            return null;
        }
    }
}
