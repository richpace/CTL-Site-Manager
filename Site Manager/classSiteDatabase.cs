using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Forms;

//using System.Collections;

namespace Site_Manager
{
    public class classSiteDatabase //: CollectionBase
    {
        // FIELDS //
        private string dbFName = null;
        private string dbName = null;
        private bool dbDirty = false;

        private bool stateAssigned = false;
        private bool stateMapped = false;

        private TreeNodeCollection siteIOSList;
        private TreeNodeCollection siteIOXList;

        private classIOXDB siteIOXDevices = new classIOXDB();
        private classIOSDB siteIOSDevices = new classIOSDB();
        private classSiteMapDB siteMaps = new classSiteMapDB();

        // CONSTRUCTORS //

        // PROPERTIES //
        public bool Assigned
        {
            get
            {
                return stateAssigned;
            }
            set
            {
                stateAssigned = value;
            }
        }

        public bool Mapped
        {
            get
            {
                return stateMapped;
            }
            set
            {
                stateMapped = value;
            }
        }

        public string Name
        {
            get
            {
                return dbName;
            }
            set
            {
                dbName = value;
            }
        }

        public classIOSDB Legacy
        {
            get
            {
                return siteIOSDevices;
            }
        }

        public classIOXDB ASR
        {
            get
            {
                return siteIOXDevices;
            }
        }

        public classSiteMapDB Maps
        {
            get
            {
                return siteMaps;
            }
        }

        public bool Dirty
        {
            get
            {
                return dbDirty;
            }
        }

        // METHODS //
        public void AddIOXDevice()
        {
            addIOX();
        }

        public void AddIOSDevice()
        {
            addIOS();
        }

        public void MapUnits(TreeNodeCollection inIOS, TreeNodeCollection inIOX)
        {
            siteIOSList = inIOS;
            siteIOXList = inIOX;

            mapSiteUnits();
        }

        public void Save()
        {
            if ((dbFName == null))
            {
                dbFName = getFNameSave();
            }

            if (dbFName != null)
            {
                string[] sa = dbFName.Split("\\".ToCharArray());
                string s = sa[sa.GetUpperBound(0)];

                Name = s.Remove(s.IndexOf(".sdb"));

                writeDB();
            }
        }

        public void SaveAs()
        {
            dbFName = getFNameSave();
            if (dbFName != null)
            {
                Save();
            }
        }

        public bool Open()
        {
            dbFName = getFNameOpen();
            if (dbFName != null)
            {

                readDB();
                string[] sa = dbFName.Split("\\".ToCharArray());
                string s = sa[sa.GetUpperBound(0)];

                Name = s.Remove(s.IndexOf(".sdb"));

                return true;
            }
            else
            {
                return false;
            }
        }

        // SUPPORT LOGIC //
        private void addIOX()
        {
            if (siteIOXDevices.Add() == true) markDBDirty();
        }

        private void addIOS()
        {
            if (siteIOSDevices.Add() == true) markDBDirty();
        }

        private void markDBDirty()
        {
            dbDirty = true;
        }

        private void markDBClean()
        {
            dbDirty = false;
        }

        private void writeDB()
        {
            StreamWriter dbStream = new StreamWriter(dbFName);

            writeDevices(dbStream);
            writeStates(dbStream);
            writeMaps(dbStream);
            writeSchedule(dbStream);
            dbStream.Close();
            dbDirty = false;
        }

        private void readDB()
        {
            StreamReader dbStream = new StreamReader(dbFName);

            string[] delim = new string[] { "::" };
            string[] lineIn;

            while (dbStream.EndOfStream == false)
            {
                lineIn = dbStream.ReadLine().Split(delim, StringSplitOptions.None);

                switch (lineIn[0])
                {
                    case "DEV":
                        readDevice(lineIn);
                        break;
                    case "MAP":
                        readMap(lineIn);
                        break;
                    case "STA":
                        readStates(lineIn);
                        break;
                    case "INT":
                        readSchedule(lineIn);
                        break;
                }
            }
        }

        private void writeDevices(StreamWriter inSW)
        {
            string header = "DEV";
            string delim = "::";
            string outLine = null;

            for (int d = 0; d < siteIOSDevices.Count; d++)
            {
                outLine = header + delim;
                outLine += "IOS" + delim;
                outLine += siteIOSDevices[d].Filename + delim;
                outLine += siteIOSDevices[d].PreferredASR + delim;
                inSW.WriteLine(outLine);
            }

            for (int d = 0; d < siteIOXDevices.Count; d++)
            {
                outLine = header + delim;
                outLine += "IOX" + delim;
                outLine += siteIOXDevices[d].Filename + delim;
                outLine += siteIOXDevices[d].Assigned.ToString() + delim;
                inSW.WriteLine(outLine);
            }
        }

        private void readDevice(string[] inLine)
        {
            string devType = inLine[1];
            string devFN = inLine[2];
            classIOX devIOX;
            classIOS devIOS;

            switch (devType)
            {
                case "IOX":
                    try
                    {
                        devIOX = new classIOX(devFN);
                    }
                    catch
                    {
                        string devName = devFN.Split("\\".ToCharArray()).Last();
                        devFN = getFNameOpen(devName);
                        devIOX = new classIOX(devFN);
                    }

                    if (inLine[3].ToLower() == "true")
                    {
                        devIOX.Assigned = true;
                    }
                    siteIOXDevices.Add(devIOX);
                    break;
                case "IOS":
                    try
                    {
                        devIOS = new classIOS(devFN);
                    }
                    catch
                    {
                        string devName = devFN.Split("\\".ToCharArray()).Last();
                        devFN = getFNameOpen(devName);
                        devIOS = new classIOS(devFN);
                    }

                    if (inLine[3] != "")
                    {
                        devIOS.AssignASR(inLine[3]);
                    }
                    siteIOSDevices.Add(devIOS);
                    break;
            }

            //switch (devType)
            //{
            //    case "IOX":
            //        classIOX devIOX = new classIOX(devFN);
            //        if (inLine[3].ToLower() == "true")
            //        {
            //            devIOX.Assigned = true;
            //        }
            //        siteIOXDevices.Add(devIOX);
            //        break;
            //    case "IOS":
            //        classIOS devIOS = new classIOS(devFN);
            //        if (inLine[3] != "")
            //        {
            //            devIOS.AssignASR(inLine[3]);
            //        }
            //        siteIOSDevices.Add(devIOS);
            //        break;
            //}
        }

        private void readDevice(string inLine)
        {
            string[] delim = new string[] { "::" };
            string[] line = inLine.Split(delim, StringSplitOptions.None);
            string devType = line[0];
            string devFN = line[1];
            switch (devType)
            {
                case "IOX":
                    classIOX devIOX = new classIOX(devFN);
                    if (line[2].ToLower() == "true")
                    {
                        devIOX.Assigned = true;
                    }
                    siteIOXDevices.Add(devIOX);
                    break;
                case "IOS":
                    classIOS devIOS = new classIOS(devFN);
                    if (line[2] != "")
                    {
                        devIOS.AssignASR(line[2]);
                    }
                    siteIOSDevices.Add(devIOS);
                    break;
            }
        }

        private void writeMaps(StreamWriter inSW)
        {
            string header = "MAP";
            string delim = "::";
            string outLine = null;

            classMap map = null;

            for (int m = 0; m < siteMaps.Count; m++)
            {
                map = siteMaps[m];
                outLine = header + delim;
                outLine += map.Legacy + delim;
                outLine += map.PrefixLegacy + delim;
                outLine += map.ASR + delim;
                outLine += map.PrefixASR + delim;
                inSW.WriteLine(outLine);
            }
        }

        private void readMap(string[] inLine)
        {
            string mapL = inLine[1];
            string mapLP = inLine[2];
            string mapA = inLine[3];
            string mapAP = inLine[4];

            classUnit mapLU = siteIOSDevices[mapL].Units[mapLP];
            classUnit mapAU = siteIOXDevices[mapA].Units[mapAP];

            mapLU.Assign();
            mapAU.Assign();

            siteMaps.Add(new classMap(mapL, mapLU, mapA, mapAU));
        }

        private void readMap(string inLine)
        {
            string[] delim = new string[] { "::" };
            string[] line = inLine.Split(delim, StringSplitOptions.None);
            string mapL = line[0];
            string mapLP = line[1];
            string mapA = line[2];
            string mapAP = line[3];

            classUnit mapLU = siteIOSDevices[mapL].Units[mapLP];
            classUnit mapAU = siteIOXDevices[mapA].Units[mapAP];

            mapLU.Assign();
            mapAU.Assign();

            siteMaps.Add(new classMap(mapL, mapLU, mapA, mapAU));
        }

        private void writeStates(StreamWriter inSW)
        {
            string header = "STA";
            string delim = "::";
            string outLine = null;

            //outLine = header + delim;
            //outLine += "A" + delim;
            //outLine += stateAssigned.ToString() + delim;
            //inSW.WriteLine(outLine);

            outLine = header + delim;
            outLine += "M" + delim;
            outLine += stateMapped.ToString() + delim;
            inSW.WriteLine(outLine);
        }

        private void readStates(string[] inLine)
        {
            switch (inLine[1].ToUpper())
            {
                case "A":
                    stateAssigned = bool.Parse(inLine[2]);
                    //if (inLine[2].ToLower() == "true")
                    //{
                    //    stateAssigned = true;
                    //}
                    //else
                    //{
                    //    stateAssigned = false;
                    //}
                    break;
                case "M":
                    stateMapped = bool.Parse(inLine[2]);
                    //if (inLine[2].ToLower() == "true")
                    //{
                    //    stateMapped = true;
                    //}
                    //else
                    //{
                    //    stateMapped = false;
                    //}
                    break;
            }
        }

        private void writeSchedule(StreamWriter inSW)
        {
            string header = "INT";
            string delim = "::";
            string outLine = null;

            for (int d = 0; d < Legacy.Count; d++)
            {
                classIOS devLegacy = Legacy[d];
                for (int c = 0; c < devLegacy.Circuits.Count; c++)
                {
                    classIOSInterface intLegacy = devLegacy.Circuits[c];
                    if (intLegacy.MigrationDate.Year != 1)
                    {
                        outLine = header + delim;
                        outLine += devLegacy.Hostname + delim;
                        outLine += intLegacy.ID + delim;
                        outLine += intLegacy.MigrationDate + delim;
                        inSW.WriteLine(outLine);
                    }
                }
            }
        }

        private void readSchedule(string[] inLine)
        {
            classIOSInterface intLegacy;

            classIOS devLegacy = Legacy[inLine[1]];
            intLegacy = devLegacy.Circuits[inLine[2]];
            if (intLegacy != null) intLegacy.MigrationDate = DateTime.Parse(inLine[3]);
        }

        private string getFNameSave()
        {
            SaveFileDialog dlg = new SaveFileDialog();

            dlg.AddExtension = true;
            dlg.DefaultExt = "sdb";
            dlg.OverwritePrompt = true;
            dlg.Filter = "Site Database Files (*.sdb)|*.sdb|All files(*.*)|*.*";
            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                return null;
            }
            else
            {
                return dlg.FileName;
            }
        }

        private string getFNameOpen(string inDeviceName)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Unable to find " + inDeviceName;
            dlg.ShowDialog();

            if (dlg.FileName == "")
            {
                return null;
            }
            else
            {
                return dlg.FileName;
            }
        }

        private string getFNameOpen()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "sdb";
            dlg.Filter = "Site Database Files (*.sdb)|*.sdb|All files(*.*)|*.*";
            dlg.ShowDialog();

            if (dlg.FileName=="")
            {
                return null;
            }
            else
            {
                return dlg.FileName;
            }
        }

        // SUPPORT LOGIC: CIRCUIT ASSIGNMENTS //
        private void mapSiteUnits()
        {
            mapWideUnits();
            mapPreferredUnits();
            mapStragglerUnits();
        }

        private void mapWideUnits()
        {
            mapWidePreferredUnits();
            mapWideStragglerUnits();
        }

        private void mapWidePreferredUnits()
        {
            classIOS legacyDevice;
            classUnit unitID;

            for (int l = 0; l < siteIOSList.Count; l++)
            {
                legacyDevice = Legacy[siteIOSList[l].Text];
                for (int u = 0; u < legacyDevice.Units.Count; u++)
                {
                    unitID = legacyDevice.Units[u];
                    if (unitID.Active == true && unitID.Width == 12)
                    {
                        if (unitID.Assigned == false && unitID.Assignable == true)
                        {
                            mapUnit(legacyDevice.Hostname, unitID, legacyDevice.PreferredASR);
                        }
                    }
                }
                for (int u = 0; u < legacyDevice.Units.Count; u++)
                {
                    unitID = legacyDevice.Units[u];
                    if (unitID.Active == true && unitID.Width == 3)
                    {
                        if (unitID.Assigned == false && unitID.Assignable == true)
                        {
                            mapUnit(legacyDevice.Hostname, unitID, legacyDevice.PreferredASR);
                        }
                    }
                }
            }
        }

        private void mapWideStragglerUnits()
        {
            classIOS legacyDevice;
            classUnit unitID;

            for (int l = 0; l < siteIOSList.Count; l++)
            {
                legacyDevice = Legacy[siteIOSList[l].Text];
                for (int u = 0; u < legacyDevice.Units.Count; u++)
                {
                    unitID = legacyDevice.Units[u];
                    if (unitID.Active == true && unitID.Width == 12)
                    {
                        if (unitID.Assigned == false && unitID.Assignable == true)
                        {
                            mapUnit(legacyDevice.Hostname, unitID);
                        }
                    }
                }
                for (int u = 0; u < legacyDevice.Units.Count; u++)
                {
                    unitID = legacyDevice.Units[u];
                    if (unitID.Active == true && unitID.Width == 3)
                    {
                        if (unitID.Assigned == false && unitID.Assignable == true)
                        {
                            mapUnit(legacyDevice.Hostname, unitID);
                        }
                    }
                }
            }
        }

        private void mapPreferredUnits()
        {
            classIOS legacyDevice;
            classUnit unitID;

            for (int l = 0; l < siteIOSList.Count; l++)
            {
                legacyDevice = Legacy[siteIOSList[l].Text];
                for (int u = 0; u < legacyDevice.Units.Count; u++)
                {
                    unitID = legacyDevice.Units[u];
                    if (unitID.Active == true)
                    {
                        if (unitID.Assigned == false && unitID.Assignable == true)
                        {
                            mapUnit(legacyDevice.Hostname, unitID, legacyDevice.PreferredASR);
                        }
                    }
                }
            }
        }

        private void mapStragglerUnits()
        {
            classIOS legacyDevice;
            classUnit unitID;

            for (int l = 0; l < Legacy.Count; l++)
            {
                legacyDevice = Legacy[l];
                for (int u = 0; u < legacyDevice.Units.Count; u++)
                {
                    unitID = legacyDevice.Units[u];
                    if (unitID.Active == true)
                    {
                        if (unitID.Assigned == false && unitID.Assignable == true)
                        {
                            mapUnit(legacyDevice.Hostname, unitID);
                        }
                    }
                }
            }
        }

        private void mapUnit(string inLegacyName, classUnit inUnit, string inASRName)
        {
            if (inASRName != null)
            {
                classIOX ioxDevice = siteIOXDevices[inASRName];
                classUnit ioxUnit = getPreferredUnit(ioxDevice, inUnit);
                classMap newMap = null;
                int width = inUnit.Width;

                if (ioxUnit != null)
                {
                    newMap = new classMap(inLegacyName, inUnit, inASRName, ioxUnit);
                    Legacy[inLegacyName].AssignUnit(inUnit);
                    ASR[inASRName].AssignUnit(ioxUnit, width);
                    siteMaps.Add(newMap);
                }
            }
        }

        private void mapUnit(string inLegacyName, classUnit inUnit)
        {
            classIOX ioxDevice;
            classUnit ioxUnit;
            classMap newMap = null;
            int width = inUnit.Width;

            for (int a = 0; a < siteIOXList.Count; a++)
            {
                ioxDevice = siteIOXDevices[siteIOXList[a].Text];
                ioxUnit = ioxDevice.NextOpenUnit(inUnit);
                if (ioxUnit != null)
                {
                    newMap = new classMap(inLegacyName, inUnit, ioxDevice.Hostname, ioxUnit);
                    Legacy[inLegacyName].AssignUnit(inUnit);
                    ASR[ioxDevice.Hostname].AssignUnit(ioxUnit, width);
                    siteMaps.Add(newMap);
                    return;
                }
            }
        }

        private classUnit getPreferredUnit(classIOX inASR, classUnit inLegacyUnit)
        {
            return inASR.NextOpenUnit(inLegacyUnit);
        }

    }
}
