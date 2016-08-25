using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cisco_Device;

namespace Site_Manager
{
    public partial class sdiSite : Form
    {
        // FIELDS //
        private classSiteDB dbSite = new classSiteDB();

        // CONSTRUCTORS //
        public sdiSite(int inID)
        {
            InitializeComponent();

            dbSite.Name = "Site " + inID;
            arrangeWindow();
        }

        // PROPERTIES //
        public new classSiteDB Site
        {
            get { return dbSite; }
        }

        // METHODS //
        new public bool Load()
        {
            if (dbSite.Open() != false)
            {
                arrangeWindow();
                return true;
            }
            return false;
        }

        // EVENTS: MENU //
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            Site.Save();
            arrangeWindow();
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            Site.SaveAs();
            arrangeWindow();
        }

        private void menuFileClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuSiteDataASR_Click(object sender, EventArgs e)
        {
            Site.AddASR();
            //string newASR = Site.AddASR();

            //Site.ASR.UpdateUnits(newASR, Site.WB.Units, true);
            //Site.WB.DeleteASR(newASR);

            arrangeWindow();
        }

        private void menuSiteDataWB_Click(object sender, EventArgs e)
        {
            Site.AddWBData();

            arrangeWindow();
        }

        private void menuSiteCircuits_Click(object sender, EventArgs e)
        {
            sdiSiteCircuits windowCircuitsManage = new sdiSiteCircuits(Site);
        }

        private void menuSiteSchedule_Click(object sender, EventArgs e)
        {
            sdiSiteSchedule windowSchedule = new sdiSiteSchedule(Site);
        }

        // EVENTS: GENERAL //
        private void btnAutoAssign_Click(object sender, EventArgs e)
        {
            dbSite.MapUnits(treeWB.Nodes, treeASR.Nodes);

            loadGrids();
        }

        private void sdiSite_Resize(object sender, EventArgs e)
        {
            arrangeWindow();
        }

        private void treeASR_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string uid;

            if (e.Action != TreeViewAction.Unknown)
            {
                switch (e.Node.Level)
                {
                    case 2:
                        if ((string)e.Node.Tag == "OC12")
                        {
                            foreach (TreeNode N in e.Node.Nodes)
                            {
                                N.Checked = e.Node.Checked;
                                uid = parseNodePath(N.FullPath);
                                Site.UpdateASRUnit(uid, N.Checked);
                            }
                        }
                        else
                        {
                            if (e.Node.Nodes.Count > 0)
                            {
                                foreach (TreeNode N in e.Node.Nodes)
                                {
                                    N.Checked = e.Node.Checked;
                                    uid = parseNodePath(N.FullPath);
                                    Site.UpdateASRUnit(uid, N.Checked);
                                }
                            }
                        }
                        break;
                    case 3:
                        e.Node.Parent.Checked = false;
                        foreach (TreeNode N in e.Node.Parent.Nodes)
                        {
                            e.Node.Parent.Checked = e.Node.Parent.Checked || N.Checked;
                        }
                        uid = parseNodePath(e.Node.FullPath);
                        Site.UpdateASRUnit(uid, e.Node.Checked);
                        break;
                    default:
                        e.Node.Checked = false;
                        break;
                }
                loadGrids();
            }
        }

        // SUPPORT LOGIC //
        private void arrangeWindow()
        {
            displaySiteName();
            displayTree();

            loadGrids();
        }

        private void displaySiteName()
        {
            if (dbSite.Dirty == true)
            {
                this.Text = dbSite.Name + "*";
            }
            else
            {
                this.Text = dbSite.Name;
            }
        }

        private void displayTree()
        {
            displayTreeWB();
            displayTreeASR();
        }

        private void displayTreeWB()
        {
            treeWB.Nodes.Clear();

            foreach (KeyValuePair<string, classCircuit> CX in dbSite.WB)
                processWBEntry(CX.Value);

            treeWB.Sort();
        }

        private void processWBEntry(classCircuit inCX)
        {
            // OMG, what a f***ing mess...I need to clean this s**t up

            TreeNode nodeDevice;
            TreeNode nodeUnit;
            TreeNode nodeUnitType;
            TreeNode nodeSTS;

            // DEVICE
            if (treeWB.Nodes.ContainsKey(inCX.Device) == false)
            {
                treeWB.Nodes.Add(inCX.Device, inCX.Device);
                nodeDevice = treeWB.Nodes[inCX.Device];
                nodeDevice.Nodes.Add("PHY", "GE: 0");
                nodeDevice.Nodes.Add("MON", "MON: 0");
                nodeDevice.Nodes.Add("DCS", "DCS: 0");
            }
            else
            {
                nodeDevice = treeWB.Nodes[inCX.Device];
            }

            // UNIT

            switch (inCX.Type)
            {
                case typeCircuit.Ethernet:
                    nodeUnit = nodeDevice.Nodes["PHY"];
                    if (inCX.Subinterface == true)
                    {
                        if (nodeUnit.Nodes.ContainsKey(inCX.Physical) == false)
                        {
                            nodeUnit.Nodes.Add(inCX.Physical, inCX.Physical);
                            TreeNode nodePhysical = nodeUnit.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                        else
                        {
                            TreeNode nodePhysical = nodeUnit.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }
                    else
                    {
                        if (nodeUnit.Nodes.ContainsKey(inCX.Interface) == false)
                        {
                            nodeUnit.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }
                    nodeUnit.Text = "GE: " + nodeUnit.Nodes.Count.ToString().Trim();
                    break;

                case typeCircuit.xSTS:
                    nodeUnit = nodeDevice.Nodes["MON"];
                    if (inCX.Subinterface == true)
                    {
                        if (nodeUnit.Nodes.ContainsKey(inCX.Physical) == false)
                        {
                            nodeUnit.Nodes.Add(inCX.Physical, inCX.Physical);
                            TreeNode nodePhysical = nodeUnit.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                        else
                        {
                            TreeNode nodePhysical = nodeUnit.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }
                    else
                    {
                        if (nodeUnit.Nodes.ContainsKey(inCX.Interface) == false)
                        {
                            nodeUnit.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }
                    nodeUnit.Text = "MON: " + nodeUnit.Nodes.Count.ToString().Trim();
                    break;

                case typeCircuit.xDS1:
                    nodeUnit = nodeDevice.Nodes["DCS"];

                    switch (inCX.PhysicalType)
                    {
                        case typePhysical.T3:
                            if (nodeUnit.Nodes.ContainsKey("T3") == false)
                            {
                                nodeUnit.Nodes.Add("T3", "T3");
                            }
                            nodeUnitType = nodeUnit.Nodes["T3"];
                            break;
                        case typePhysical.SONET:
                            if (nodeUnit.Nodes.ContainsKey("SONET") == false)
                            {
                                nodeUnit.Nodes.Add("SONET", "SONET");
                            }
                            nodeUnitType = nodeUnit.Nodes["SONET"];
                            break;
                        default:
                            nodeUnitType = null;
                            break;
                    }

                    if (nodeUnitType.Nodes.ContainsKey(inCX.STS) == false)
                    {
                        nodeUnitType.Nodes.Add(inCX.STS, inCX.STS);
                    }
                    nodeSTS = nodeUnitType.Nodes[inCX.STS];

                    if (inCX.Subinterface == true)
                    {
                        if (nodeSTS.Nodes.ContainsKey(inCX.Physical) == false)
                        {
                            nodeSTS.Nodes.Add(inCX.Physical, inCX.Physical);
                            TreeNode nodePhysical = nodeSTS.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                        else
                        {
                            TreeNode nodePhysical = nodeSTS.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }
                    else
                    {
                        if (nodeSTS.Nodes.ContainsKey(inCX.Interface) == false)
                        {
                            nodeSTS.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }

                    nodeUnit.Text = "DCS: " + nodeUnit.Nodes.Count.ToString().Trim();
                    break;


                case typeCircuit.xDS0:
                    nodeUnit = nodeDevice.Nodes["DCS"];
                    TreeNode nodeDS1;

                    switch (inCX.PhysicalType)
                    {
                        case typePhysical.T3:
                            if (nodeUnit.Nodes.ContainsKey("T3") == false)
                            {
                                nodeUnit.Nodes.Add("T3", "T3");
                            }
                            nodeUnitType = nodeUnit.Nodes["T3"];
                            break;
                        case typePhysical.SONET:
                            if (nodeUnit.Nodes.ContainsKey("SONET") == false)
                            {
                                nodeUnit.Nodes.Add("SONET", "SONET");
                            }
                            nodeUnitType = nodeUnit.Nodes["SONET"];
                            break;
                        default:
                            nodeUnitType = null;
                            break;
                    }


                    if (nodeUnitType.Nodes.ContainsKey(inCX.STS) == false)
                    {
                        nodeUnitType.Nodes.Add(inCX.STS, inCX.STS);
                    }
                    nodeSTS = nodeUnitType.Nodes[inCX.STS];

                    if (nodeSTS.Nodes.ContainsKey(inCX.DS1) == false)
                    {
                        nodeSTS.Nodes.Add(inCX.DS1, inCX.DS1);
                    }
                    nodeDS1 = nodeSTS.Nodes[inCX.DS1];

                    if (inCX.Subinterface == true)
                    {
                        if (nodeDS1.Nodes.ContainsKey(inCX.Physical) == false)
                        {
                            nodeDS1.Nodes.Add(inCX.Physical, inCX.Physical);
                            TreeNode nodePhysical = nodeDS1.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                        else
                        {
                            TreeNode nodePhysical = nodeDS1.Nodes[inCX.Physical];
                            nodePhysical.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }
                    else
                    {
                        if (nodeDS1.Nodes.ContainsKey(inCX.Interface) == false)
                        {
                            nodeDS1.Nodes.Add(inCX.Interface, inCX.Interface + ": " + inCX.Customer);
                        }
                    }

                    nodeUnit.Text = "DCS: " + nodeUnit.Nodes.Count.ToString().Trim();
                    break;
            }


        } //processWBEntry

        private void displayTreeASR()
        {
            treeASR.Nodes.Clear();

            foreach (KeyValuePair<string, classASR> ASR in dbSite.ASR)
            {
                treeASR.Nodes.Add(ASR.Key, ASR.Key);
                TreeNode nodeASR = treeASR.Nodes[ASR.Key];

                if (ASR.Value.GE == true)
                {
                    TreeNode nodeGE = nodeIOXUnitsGE(dbSite.ASR[ASR.Key]);
                    nodeASR.Nodes.Add(nodeGE);
                }

                if (ASR.Value.MON == true)
                {
                    TreeNode nodeCL = nodeIOXUnitsCL(dbSite.ASR[ASR.Key]);
                    nodeASR.Nodes.Add(nodeCL);
                }

                if (ASR.Value.DCS == true)
                {
                    TreeNode nodeCH = nodeIOXUnitsCH(dbSite.ASR[ASR.Key]);
                    nodeASR.Nodes.Add(nodeCH);
                }
            }

            treeASR.Sort();
        }

        private TreeNode nodeIOXUnitsGE(classASR inDevice)
        {
            TreeNode nodeUnitsGE = new TreeNode("GE:");

            foreach (KeyValuePair<string, classUnit> U in Site.ASR.Units)
                if (U.Value.Type == UnitType.GE && U.Value.Assigned == false)
                {
                    TreeNode ge = new TreeNode(U.Value.Prefix);
                    ge.Checked = U.Value.Assignable;
                    ge.Tag = "UNIT";
                    nodeUnitsGE.Nodes.Add(ge);
                }
            nodeUnitsGE.Tag = "GE";

            return nodeUnitsGE;
        }

        private TreeNode nodeIOXUnitsCH(classASR inDevice)
        {
            TreeNode nodeUnitsCH = new TreeNode("OC-12:");
            TreeNode nodeUnitsOC12;

            foreach (KeyValuePair<string, classUnit> U in Site.ASR.Units)
            {
                if (U.Value.Device == inDevice.Name && U.Value.Type == UnitType.DCS && U.Value.Assigned == false)
                {
                    TreeNode ch = new TreeNode(U.Value.Prefix);
                    ch.Checked = U.Value.Assignable;

                    if (nodeUnitsCH.Nodes.ContainsKey(U.Value.Port) == false)
                    {
                        nodeUnitsOC12 = new TreeNode(U.Value.Port);
                        nodeUnitsOC12.Name = U.Value.Port;
                        nodeUnitsOC12.Tag = "OC12";
                        nodeUnitsCH.Nodes.Add(nodeUnitsOC12);
                    }
                    else
                    {
                        nodeUnitsOC12 = nodeUnitsCH.Nodes[U.Value.Port];
                    }
                    ch.Tag = "UNIT";
                    nodeUnitsOC12.Nodes.Add(ch);
                    nodeUnitsOC12.Checked = nodeUnitsOC12.Checked || ch.Checked;
                }
            }
            nodeUnitsCH.Tag = "CH";

            return nodeUnitsCH;
        }

        private TreeNode nodeIOXUnitsCL(classASR inDevice)
        {
            TreeNode nodeUnitsCL = new TreeNode("OC-48:");
            TreeNode nodeUnitsOC48;

            foreach (KeyValuePair<string, classUnit> U in Site.ASR.Units)
            {
                if (U.Value.Device == inDevice.Name && U.Value.Type == UnitType.MON && U.Value.Assigned == false)
                {
                    TreeNode cl = new TreeNode(U.Value.Prefix);
                    cl.Checked = U.Value.Assignable;

                    if (nodeUnitsCL.Nodes.ContainsKey(U.Value.Port) == false)
                    {
                        nodeUnitsOC48 = new TreeNode(U.Value.Port);
                        nodeUnitsOC48.Name = U.Value.Port;
                        nodeUnitsOC48.Tag = "OC48";
                        nodeUnitsCL.Nodes.Add(nodeUnitsOC48);
                    }
                    else
                    {
                        nodeUnitsOC48 = nodeUnitsCL.Nodes[U.Value.Port];
                    }
                    cl.Tag = "UNIT";
                    nodeUnitsOC48.Nodes.Add(cl);
                    nodeUnitsOC48.Checked = nodeUnitsOC48.Checked || cl.Checked;
                }
            }
            nodeUnitsCL.Tag = "CL";

            return nodeUnitsCL;
        }

        private void loadGrids()
        {
            loadGridUnassigned();
            loadGridAssigned();
        }

        private void loadGridAssigned()
        {
            int row;

            gridAssigned.Rows.Clear();
            foreach (KeyValuePair<string, classUnit> U in dbSite.WB.Units)
            {
                if (U.Value.Assigned == true)
                {
                    if (dbSite.Maps.ContainsKey(U.Key) == true)
                    {
                        classMap M = dbSite.Maps[U.Key];

                        row = gridAssigned.Rows.Add();
                        gridAssigned.Rows[row].Cells[0].Value = M.Type;
                        gridAssigned.Rows[row].Cells[1].Value = M.Legacy;
                        gridAssigned.Rows[row].Cells[2].Value = M.PrefixLegacy.ToUpper();
                        gridAssigned.Rows[row].Cells[3].Value = M.ASR;
                        gridAssigned.Rows[row].Cells[4].Value = M.PrefixASR;
                    }
                }
            }
            tabAssignedCX.Text = "Assigned: " + gridAssigned.Rows.Count.ToString().Trim();
        }

        private void loadGridUnassigned()
        {
            int row;

            gridUnassigned.Rows.Clear();
            foreach (KeyValuePair<string, classUnit> kvpU in dbSite.WB.Units)
            {
                if (kvpU.Value.Assigned == false)
                {
                    row = gridUnassigned.Rows.Add();
                    gridUnassigned.Rows[row].Cells[0].Value = kvpU.Value.Type;
                    gridUnassigned.Rows[row].Cells[1].Value = kvpU.Key.Split("*".ToCharArray())[0];
                    gridUnassigned.Rows[row].Cells[2].Value = kvpU.Key.Split("*".ToCharArray())[1];
                }
            }
            tabUnassignedCX.Text = "Unassigned: " + gridUnassigned.Rows.Count.ToString().Trim();
        }

        private string parseNodePath(string inPath)
        {
            string[] delim = new string[] { "\\" };
            string[] pathArray = inPath.Split(delim, StringSplitOptions.None);

            return pathArray.First() + "*" + pathArray.Last();
        }
    }
}
