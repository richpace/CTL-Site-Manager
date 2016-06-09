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
        private classSiteDatabase dbSite = new classSiteDatabase();

        // CONSTRUCTORS //
        public sdiSite()
        {
            InitializeComponent();
        }

        public sdiSite(int inID)
        {
            InitializeComponent();

            dbSite.Name = "Site " + inID;
            arrangeWindow();
        }

        // PROPERTIES //
        public new classSiteDatabase Site
        {
            get
            {
                return dbSite;
            }
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

        private void menuSiteDevicesAddASR_Click(object sender, EventArgs e)
        {
            Site.AddIOXDevice();
            arrangeWindow();
        }

        private void menuSiteDevicesAddLegacy_Click(object sender, EventArgs e)
        {
            Site.AddIOSDevice();
            arrangeWindow();
        }

        private void menuSiteMaps_Click(object sender, EventArgs e)
        {
            sdiSiteMaps2 windowMaps = new sdiSiteMaps2(Site);
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
            //dbSite.Mapped = true;
            dbSite.MapUnits(treeIOS.Nodes, treeIOX.Nodes);
            processWindow();
        }

        private void contextLegacyNodeAssignASR_Click(object sender, EventArgs e)
        {
            sdiASRSelect iosSelectASR = new sdiASRSelect(dbSite.ASR);
            classIOS iosDevice = dbSite.Legacy[this.contextLegacyNode.Tag.ToString()];

            if ((string)iosSelectASR.Tag != "CANX")
            {
                if (iosDevice.PreferredASR != null)
                {
                    dbSite.ASR[iosDevice.PreferredASR].Assigned = false;
                }

                iosDevice.AssignASR(iosSelectASR.ASR);

                if (iosSelectASR.ASR != null)
                {
                    dbSite.ASR[iosSelectASR.ASR].Assigned = true;
                }
                processWindow();
            }
        }

        private void sdiSite_Resize(object sender, EventArgs e)
        {
            arrangeWindow();
        }

        private void treeIOS_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string nodeName = getDeviceName(e.Node.FullPath);

            if (e.Button == MouseButtons.Right && dbSite.Mapped == false)
            {
                int X = Cursor.Position.X;
                int Y = Cursor.Position.Y;

                contextLegacyNode.Tag = nodeName;
                contextLegacyNode.Show(X, Y);
            }
        }

        private void treeIOX_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string nodeName = getDeviceName(e.Node.FullPath);

            switch (e.Node.Level)
            {
                case 2:
                    if ((e.Node.Nodes.Count > 0)&&(e.Node.Tag != "UNIT"))
                    {
                        foreach (TreeNode n in e.Node.Nodes)
                        {
                            n.Checked = e.Node.Checked;

                            dbSite.ASR[nodeName].Units[n.Text].Assignable = n.Checked;
                            if (n.Checked == false) updateMaps(nodeName, n.Text);
                        }
                    }
                    else
                    {
                        dbSite.ASR[nodeName].Units[e.Node.Text].Assignable = e.Node.Checked;
                        if (e.Node.Checked == false) updateMaps(nodeName, e.Node.Text);
                    }
                    break;
                case 3:
                    e.Node.Parent.Checked = false;
                    foreach (TreeNode n in e.Node.Parent.Nodes)
                    {
                        e.Node.Parent.Checked = e.Node.Parent.Checked || n.Checked;
                        dbSite.ASR[nodeName].Units[n.Text].Assignable = n.Checked;
                        if (n.Checked == false) updateMaps(nodeName,n.Text);
                    }


                    break;
                default:
                    e.Node.Checked = false;
                    break;
            }
            processWindow();
        }

        private void updateMaps(string inASR, string inPrefix)
        {
            classSiteMapDB maps = dbSite.Maps;

            int mapIndex = maps.ASRIndex(inASR, inPrefix);

            if (mapIndex > -1)
            {
                classMap map = maps[mapIndex];

                classUnit ASRUnit = dbSite.ASR[map.ASR].Units[map.PrefixASR];
                classUnit LegacyUnit = dbSite.Legacy[map.Legacy].Units[map.PrefixLegacy];

                ASRUnit.Assigned = false;
                LegacyUnit.Assigned = false;

                maps.RemoveAt(mapIndex);
            }


        }

        // SUPPORT LOGIC //
        private void arrangeWindow()
        {
            displaySiteName();
            displayTree();

            processWindow();
        }

        private void processWindow()
        {
            //dbSite.Assigned = devicesAssigned();

            //btnAutoAssign.Enabled = devicesAssignable() && dbSite.Assigned && !dbSite.Mapped;

            //menuSiteDevicesAddASR.Enabled = !dbSite.Mapped;
            //menuSiteDevicesAddLegacy.Enabled = !dbSite.Mapped;
            //menuSiteCircuits.Enabled = dbSite.Mapped;
            //menuSiteMaps.Enabled = true;
            //menuSiteMaps.Enabled = dbSite.Mapped;
            //menuSiteSchedule.Enabled = dbSite.Mapped;

            processTreeLegacy();
            processTreeASR();

            loadGrids();
        }

        private bool devicesAssigned()
        {
            for (int d = 0; d < dbSite.Legacy.Count; d++)
            {
                if (dbSite.Legacy[d].PreferredASR == null) return false;
            }

            return true;
        }

        private bool devicesAssignable()
        {
            if (dbSite.Legacy.Count > 0 && dbSite.Legacy.Count == dbSite.ASR.Count)
            {
                return true;
            }
            return false;
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

            //if (dbSite.Assigned == false)
            //{
            //    this.Text += " DEVICES UNASSIGNED";
            //}
            //else
            //{
            //    if (dbSite.Mapped == false)
            //    {
            //        this.Text += " CIRCUITS UNMAPPED";
            //    }
            //}
        }

        private void displayTree()
        {
            displayTreeLegacy();
            displayTreeASR();
        }

        private void displayTreeLegacy()
        {
            treeIOS.Nodes.Clear();
            for (int i = 0; i < dbSite.Legacy.Count; i++)
            {
                treeIOS.Nodes.Add(nodeIOS(dbSite.Legacy[i]));
            }
            treeIOS.Sort();
        }

        private void displayTreeASR()
        {
            treeIOX.Nodes.Clear();
            for (int i = 0; i < dbSite.ASR.Count; i++)
            {
                treeIOX.Nodes.Add(nodeIOX(dbSite.ASR[i]));
            }
            treeIOX.Sort();
        }

        private void processTreeLegacy()
        {
            foreach (TreeNode N in treeIOS.Nodes)
            {
                processLegacyNodes(N);
            }
        }

        private void processLegacyNodes(TreeNode inNode)
        {
            switch (inNode.Level)
            {
                case 0:
                    if (dbSite.Legacy[inNode.Text].PreferredASR == null) inNode.ForeColor = Color.Red;
                    else inNode.ForeColor = Color.Black;
                    break;
                case 1:
                    if (inNode.Text.Contains("Preferred ASR:"))
                    {
                        if (dbSite.Legacy[inNode.Parent.Text].PreferredASR != null)
                        {
                            inNode.ForeColor = Color.Black;
                            inNode.Text = "Preferred ASR: " + dbSite.Legacy[inNode.Parent.Text].PreferredASR;
                        }
                        else
                        {
                            inNode.ForeColor = Color.Red;
                            inNode.Text = "Preferred ASR: NONE";
                        }
                    }
                    break;
                case 2:
                    if (dbSite.Legacy[inNode.Parent.Parent.Text].Units[inNode.Text].Assigned == true)
                    {
                        inNode.ForeColor = Color.Black;
                    }
                    else
                    {
                        inNode.ForeColor = Color.Red;
                    }
                    break;
            }
            foreach (TreeNode N in inNode.Nodes)
            {
                processLegacyNodes(N);
            }
        }

        private void processTreeASR()
        {
            foreach (TreeNode N in treeIOX.Nodes)
            {
                processASRNodes(N);
            }
        }

        private void processASRNodes(TreeNode inNode)
        {
            switch (inNode.Level)
            {
                case 0:
                    if (dbSite.ASR[inNode.Text].Assigned == false) inNode.ForeColor = Color.LightGreen;
                    else inNode.ForeColor = Color.Black;
                    break;

            }
            foreach (TreeNode N in inNode.Nodes)
            {
                processASRNodes(N);
            }
        }

        private string getDeviceName(string inPath)
        {
            return inPath.Split("\\".ToCharArray())[0];
        }

        private TreeNode nodeIOX(classIOX inDevice)
        {
            TreeNode nodeDevice = new TreeNode(inDevice.Hostname);
            if (inDevice.Assigned == false)
            {
                nodeDevice.ForeColor = Color.LightGreen;
            }
            else
            {
                nodeDevice.ForeColor = Color.Black;
            }

            TreeNode nodeGE = nodeIOXUnitsGE(inDevice);
            TreeNode nodeCH = nodeIOXUnitsCH(inDevice);
            TreeNode nodeCL = nodeIOXUnitsCL(inDevice);

            if (nodeGE.Nodes.Count > 0) nodeDevice.Nodes.Add(nodeGE);
            if (nodeCH.Nodes.Count > 0) nodeDevice.Nodes.Add(nodeCH);
            if (nodeCL.Nodes.Count > 0) nodeDevice.Nodes.Add(nodeCL);

            //nodeDevice.Tag = nodeDevice.IsExpanded;

            return nodeDevice;
        }

        private TreeNode nodeIOXUnitsGE(classIOX inDevice)
        {
            TreeNode nodeUnitsGE = new TreeNode("GE:");

            foreach (classUnit u in inDevice.Units)
            {
                if (u.Type2 == UnitType.GE)
                {
                    TreeNode ge = new TreeNode(u.Prefix);
                    ge.Checked = u.Assignable;
                    ge.Tag = "UNIT";
                    nodeUnitsGE.Nodes.Add(ge);
                }
            }
            nodeUnitsGE.Tag = "GE";

            return nodeUnitsGE;
        }

        private TreeNode nodeIOXUnitsCH(classIOX inDevice)
        {
            TreeNode nodeUnitsCH = new TreeNode("OC-12:");
            TreeNode nodeUnitsOC12;

            foreach (classUnit u in inDevice.Units)
            {
                if (u.Type2 == UnitType.CH)
                {
                    TreeNode ch = new TreeNode(u.Prefix);
                    ch.Checked = u.Assignable;

                    if (nodeUnitsCH.Nodes.ContainsKey(u.Port) == false)
                    {
                        nodeUnitsOC12 = new TreeNode(u.Port);
                        nodeUnitsOC12.Name = u.Port;
                        nodeUnitsOC12.Tag = "OC12";
                        nodeUnitsCH.Nodes.Add(nodeUnitsOC12);
                    }
                    else
                    {
                        nodeUnitsOC12 = nodeUnitsCH.Nodes[u.Port];
                    }
                    ch.Tag = "UNIT";
                    nodeUnitsOC12.Nodes.Add(ch);
                    nodeUnitsOC12.Checked = nodeUnitsOC12.Checked || ch.Checked;
                }
            }
            nodeUnitsCH.Tag = "CH";

            return nodeUnitsCH;
        }

        private TreeNode nodeIOXUnitsCL(classIOX inDevice)
        {
            TreeNode nodeUnitsCL = new TreeNode("OC-48:");
            TreeNode nodeUnitsOC48;

            foreach (classUnit u in inDevice.Units)
            {
                if (u.Type2 == UnitType.CL)
                {
                    TreeNode cl = new TreeNode(u.Prefix);
                    cl.Checked = u.Assignable;

                    if (nodeUnitsCL.Nodes.ContainsKey(u.Port) == false)
                    {
                        nodeUnitsOC48 = new TreeNode(u.Port);
                        nodeUnitsOC48.Name = u.Port;
                        nodeUnitsOC48.Tag = "OC48";
                        nodeUnitsCL.Nodes.Add(nodeUnitsOC48);
                    }
                    else
                    {
                        nodeUnitsOC48 = nodeUnitsCL.Nodes[u.Port];
                    }
                    cl.Tag = "UNIT";
                    nodeUnitsOC48.Nodes.Add(cl);
                    nodeUnitsOC48.Checked = nodeUnitsOC48.Checked || cl.Checked;
                }
            }
            nodeUnitsCL.Tag = "CL";

            return nodeUnitsCL;
        }

        private TreeNode nodeIOS(classIOS inDevice)
        {
            TreeNode nodeDevice = new TreeNode(inDevice.Hostname);

            if (inDevice.PreferredASR == null)
            {
                nodeDevice.ForeColor = Color.Red;
            }
            else
            {
                nodeDevice.ForeColor = Color.Black;
            }

            TreeNode nodeGE = nodeIOSUnitsGE(inDevice);
            TreeNode nodeCH = nodeIOSUnitsCH(inDevice);
            TreeNode nodeCL = nodeIOSUnitsCL(inDevice);

            nodeDevice.Nodes.Add(nodeIOSPreferredASR(inDevice.PreferredASR));
            if (nodeGE.Nodes.Count > 0) nodeDevice.Nodes.Add(nodeGE);
            if (nodeCH.Nodes.Count > 0) nodeDevice.Nodes.Add(nodeCH);
            if (nodeCL.Nodes.Count > 0) nodeDevice.Nodes.Add(nodeCL);

            //nodeDevice.Tag = nodeDevice.IsExpanded;

            return nodeDevice;
        }

        private TreeNode nodeIOSPreferredASR(string inASR)
        {
            TreeNode nodeASR = new TreeNode();

            if (inASR == null)
            {
                nodeASR.Text = "Preferred ASR: NONE";
                nodeASR.ForeColor = Color.Red;
            }
            else
            {
                nodeASR.Text = "Preferred ASR: " + inASR;
                nodeASR.ForeColor = Color.Black; ;
            }

            return nodeASR;
        }

        private Color colorUnit(classUnit inUnit)
        {
            if (inUnit.Assigned == false) return Color.Red;

            return Color.Black;
        }

        private TreeNode nodeIOSUnitsGE(classIOS inDevice)
        {
            TreeNode nodeUnitsGE = new TreeNode("GE:");

            foreach (classUnit U in inDevice.Units)
            {
                if (U.Type2 == UnitType.GE)
                {
                    TreeNode ge = new TreeNode(U.Prefix);
                    ge.Checked = U.Assignable;
                    ge.ForeColor = colorUnit(U);
                    nodeUnitsGE.Nodes.Add(ge);
                }
            }

            //nodeUnitsGE.Tag = nodeUnitsGE.IsExpanded;

            return nodeUnitsGE;
        }

        private TreeNode nodeIOSUnitsCL(classIOS inDevice)
        {
            TreeNode nodeUnitsCL = new TreeNode("MON:");

            foreach (classUnit U in inDevice.Units)
            {
                if (U.Type2 == UnitType.CL)
                {
                    TreeNode cl = new TreeNode(U.Prefix);
                    cl.Checked = U.Assignable;
                    cl.ForeColor = colorUnit(U);
                    nodeUnitsCL.Nodes.Add(cl);
                }
            }

            //nodeUnitsCL.Tag = nodeUnitsCL.IsExpanded;

            return nodeUnitsCL;
        }

        private TreeNode nodeIOSUnitsCH(classIOS inDevice)
        {
            TreeNode nodeUnitsCH = new TreeNode("DCS:");

            foreach (classUnit U in inDevice.Units)
            {
                if (U.Type2 == UnitType.CH)
                {
                    TreeNode ch = new TreeNode(U.Prefix);
                    ch.Checked = U.Assignable;
                    ch.ForeColor = colorUnit(U);
                    nodeUnitsCH.Nodes.Add(ch);
                }
            }

            //nodeUnitsCH.Tag = nodeUnitsCH.IsExpanded;

            return nodeUnitsCH;
        }

        private void loadGrids()
        {
            gridUnMaps.Rows.Clear();
            gridMaps.Rows.Clear();
            loadGridMapped();
            loadGridUnmapped();
        }

        private void loadGridUnmapped()
        {
            int row;

            foreach (classIOS L in dbSite.Legacy)
            {
                foreach (classUnit U in L.Units)
                {
                    if (U.Assigned == false)
                    {
                        row = gridUnMaps.Rows.Add();
                        gridUnMaps.Rows[row].Cells[0].Value = getCircuitType(U.Type2);
                        gridUnMaps.Rows[row].Cells[1].Value = L.Hostname.ToUpper();
                        gridUnMaps.Rows[row].Cells[2].Value = U.Prefix;
                    }
                }
            }
            tabUnassigned.Text = "Unassigned (" + gridUnMaps.Rows.Count.ToString() + ")";
        }

        private void loadGridMapped()
        {
            int row;

            foreach (classMap M in dbSite.Maps)
            {
                row = gridMaps.Rows.Add();
                gridMaps.Rows[row].Cells[0].Value = getCircuitType(M.Type2);
                gridMaps.Rows[row].Cells[1].Value = M.Legacy.ToUpper();
                gridMaps.Rows[row].Cells[2].Value = M.PrefixLegacy;
                gridMaps.Rows[row].Cells[3].Value = M.ASR.ToUpper();
                gridMaps.Rows[row].Cells[4].Value = M.PrefixASR;
            }
            tabAssigned.Text = "Assigned (" + gridMaps.Rows.Count.ToString() + ")";
        }

        private string getCircuitType(string inUnitType)
        {
            switch (inUnitType)
            {
                case "STS-CH":
                    return "DCS";
                case "STS-CL":
                    return "MON";
                default:
                    return "PHY";
            }
        }

        private string getCircuitType(UnitType inUnitType)
        {
            switch (inUnitType)
            {
                case UnitType.CH:
                    return "DCS";
                case UnitType.CL:
                    return "MON";
                default:
                    return "PHY";
            }
        }

    }
}
