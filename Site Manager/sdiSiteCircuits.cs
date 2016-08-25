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
using Microsoft.Office.Interop.Excel;

namespace Site_Manager
{
    public partial class sdiSiteCircuits : Form
    {
        // FIELDS //
        private classSiteDB dbSite;

        // CONSTRUCTORS //
        public sdiSiteCircuits(classSiteDB inDB)
        {
            InitializeComponent();
            dbSite = inDB;
            //dbIOS = dbSite.Legacy;
            loadWindow();
            treeDisplay();
            ShowDialog();
        }

        // EVENTS //
        private void treeDCS_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Level > 1)
                {
                    checkUnassigned(e.Node);
                    checkDCSMultilink(e.Node);
                    checkDCSSubchannel(e.Node, false);
                }
                if (e.Node.Nodes.Count > 0) this.checkChildNodes(e.Node, e.Node.Checked);
                if (e.Node.Parent != null) checkParentNode(e.Node.Parent);
            }
        }

        private void treeMON_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                checkUnassigned(e.Node);
                if (e.Node.Nodes.Count > 0) this.checkChildNodes(e.Node, e.Node.Checked);
                if (e.Node.Parent != null) checkParentNode(e.Node.Parent);
            }
        }

        private void treePHY_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                checkUnassigned(e.Node);
                if (e.Node.Level == 2)
                {
                    checkPHYParentNode(e.Node);
                }
                else
                {
                    if (e.Node.Nodes.Count > 0) checkChildNodes(e.Node, e.Node.Checked);
                }
                if (e.Node.Parent != null) checkParentNode(e.Node.Parent);
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            DateTime date;
            if (cbDisplayScheduled.Checked == false)
            {
                date = calendarCircuits.SelectionRange.Start;
            }
            else
            {
                date = DateTime.MinValue;
            }

            if (treeDCS.Nodes.Count > 0)
            {
                foreach (TreeNode N in treeDCS.Nodes)
                    if (N.Checked == true) scheduleNodes(date, N);
            }

            if (treeMON.Nodes.Count > 0)
            {
                foreach (TreeNode N in treeMON.Nodes)
                    if (N.Checked == true) scheduleNodes(date, N);
            }

            if (treePHY.Nodes.Count > 0)
            {
                foreach (TreeNode N in treePHY.Nodes)
                    if (N.Checked == true) scheduleNodes(date, N);
            }

            treeDisplay();
        }

        private void cbDisplayScheduled_CheckedChanged(object sender, EventArgs e)
        {
            cbScheduledOnly.Enabled = cbDisplayScheduled.Checked;
            if (cbDisplayScheduled.Checked == false)
            {
                btnAssign.Text = "Schedule";
                cbScheduledOnly.Checked = false;
            }
            else
            {
                btnAssign.Text = "Unschedule";
            }
            treeDisplay();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cbDIV_CheckedChanged(object sender, EventArgs e)
        {
            treeDisplay();
        }

        private void cbDense_CheckedChanged(object sender, EventArgs e)
        {
            treeDisplay();
        }

        private void cbMultilink_CheckedChanged(object sender, EventArgs e)
        {
            treeDisplay();
        }

        private void cbOther_CheckedChanged(object sender, EventArgs e)
        {
            treeDisplay();
        }

        private void cbScheduledOnly_CheckedChanged(object sender, EventArgs e)
        {
            treeDisplay();
        }

        private void textboxSearch_TextChanged(object sender, EventArgs e)
        {
            treeDisplay();
        }
        // SUPPORT LOGIC //
        private void loadWindow()
        {
            cbOther.Checked = true;
            cbDIV.Checked = true;
            cbDense.Checked = true;
            cbMultilink.Checked = true;
            cbOther.Checked = true;
            cbScheduledOnly.Enabled = false;
        }

        private void treeDisplay()
        {
            nodesCH();
            nodesMON();
            nodesGE();
        }

        private void nodesCH()
        {
            treeDCS.Nodes.Clear();

            foreach (KeyValuePair<string, classUnit> U in dbSite.WB.Units)
            {
                if (U.Value.Type == UnitType.DCS)
                {
                    foreach (KeyValuePair<string, classCircuit> CX in dbSite.WB)
                    {
                        if (CX.Value.Unit == U.Key)
                        {
                            if (displayNode(CX.Value) == true)
                            {
                                if (treeDCS.Nodes.ContainsKey(CX.Value.Device) == false)
                                {
                                    treeDCS.Nodes.Add(CX.Value.Device, CX.Value.Device);
                                }
                                TreeNode nodeLegacy = treeDCS.Nodes[CX.Value.Device];
                                nodeLegacy.Tag = "DEVICE";

                                string sts = CX.Value.STS.ToUpper().Replace("SERIAL", "");
                                if (nodeLegacy.Nodes.ContainsKey(sts) == false)
                                {
                                    nodeLegacy.Nodes.Add(sts, sts);
                                }
                                TreeNode nodeSTS = nodeLegacy.Nodes[sts];
                                nodeSTS.Tag = "STS";

                                string ds1 = CX.Value.DS1.ToUpper().Replace("SERIAL", "");
                                switch (CX.Value.Type)
                                {
                                    case typeCircuit.xDS1:
                                        nodeSTS.Nodes.Add(ds1, CX.Value.DS1 + ": " + CX.Value.ID + " - " + CX.Value.Customer);
                                        //nodeSTS.Nodes[ds1].Tag = CX.Value.Unit + "*" + CX.Value.ID;
                                        nodeSTS.Nodes[ds1].Tag = CX.Value.Unit;
                                        if (CX.Value.MigrationDate.Year > 1)
                                        {
                                            nodeSTS.Nodes[ds1].ForeColor = Color.Blue;
                                            nodeSTS.Nodes[ds1].Parent.Expand();
                                        }
                                        break;
                                    case typeCircuit.xDS0:
                                        string ds0 = CX.Value.Interface.ToUpper().Replace("SERIAL", "");
                                        if (nodeSTS.Nodes.ContainsKey(ds1) == false)
                                        {
                                            nodeSTS.Nodes.Add(ds1, CX.Value.DS1);
                                            nodeSTS.Nodes[ds1].Tag = "DS1.CH";
                                        }
                                        TreeNode nodeDS1 = nodeSTS.Nodes[ds1];
                                        nodeDS1.Nodes.Add(ds0, CX.Value.Interface + ": " + CX.Value.ID + " - " + CX.Value.Customer);
                                        //nodeDS1.Nodes[ds0].Tag = CX.Value.Unit + "*" + CX.Value.ID;
                                        nodeDS1.Nodes[ds0].Tag = CX.Value.Unit ;
                                        if (CX.Value.MigrationDate.Year > 1)
                                        {
                                            nodeDS1.Nodes[ds0].ForeColor = Color.Blue;
                                            nodeDS1.Nodes[ds0].Parent.Expand();
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            treeDCS.Sort();
        }

        private void nodesMON()
        {
            treeMON.Nodes.Clear();

            foreach (KeyValuePair<string, classUnit> U in dbSite.WB.Units)
            {
                if (U.Value.Type== UnitType.MON)
                {
                    foreach (KeyValuePair<string, classCircuit> CX in dbSite.WB)
                    {
                        if (CX.Value.Unit == U.Key)
                        {
                            if (displayNode(CX.Value) == true)
                            {
                                if (treeMON.Nodes.ContainsKey(CX.Value.Device) == false)
                                {
                                    treeMON.Nodes.Add(CX.Value.Device, CX.Value.Device);
                                }
                                TreeNode nodeLegacy = treeMON.Nodes[CX.Value.Device];
                                nodeLegacy.Tag = "DEVICE";

                                if (CX.Value.Subinterface == false)
                                {
                                    string sts = CX.Value.Interface.ToUpper().Replace("SERIAL", "");
                                    sts = sts.ToUpper().Replace("POS", "");
                                    nodeLegacy.Nodes.Add(sts, CX.Value.Interface + ": " + CX.Value.ID + " - " + CX.Value.Customer);
                                    nodeLegacy.Nodes[sts].Tag = CX.Value.Unit + "*" + CX.Value.ID;
                                    if (CX.Value.MigrationDate.Year > 1)
                                    {
                                        nodeLegacy.Nodes[sts].ForeColor = Color.Blue;
                                        nodeLegacy.Nodes[sts].Parent.Expand();
                                    }
                                }
                                else
                                {
                                    string sts = CX.Value.Carrier.ToUpper().Replace("SERIAL", "");
                                    sts = sts.ToUpper().Replace("POS", "");
                                    if (nodeLegacy.Nodes.ContainsKey(sts) == false)
                                    {
                                        nodeLegacy.Nodes.Add(sts, CX.Value.Carrier);
                                        nodeLegacy.Nodes[sts].Tag = "STS";
                                    }
                                    TreeNode nodeSTS = nodeLegacy.Nodes[sts];

                                    string key = CX.Value.Interface.ToUpper().Replace("SERIAL", "");
                                    sts = sts.ToUpper().Replace("POS", "");
                                    nodeSTS.Nodes.Add(key, CX.Value.Interface + ": " + CX.Value.ID + " - " + CX.Value.Customer);
                                    nodeSTS.Nodes[key].Tag = CX.Value.Unit + "*" + CX.Value.ID;
                                    if (CX.Value.MigrationDate.Year > 1)
                                    {
                                        nodeLegacy.Nodes[sts].ForeColor = Color.Blue;
                                        nodeLegacy.Nodes[sts].Parent.Expand();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            treeMON.Sort();
        }

        private void nodesGE()
        {
            treePHY.Nodes.Clear();

            foreach (KeyValuePair<string, classUnit> U in dbSite.WB.Units)
            {
                if (U.Value.Type == UnitType.GE)
                {
                    foreach (KeyValuePair<string, classCircuit> CX in dbSite.WB)
                    {
                        if (CX.Value.Unit == U.Key)
                        {
                            if (displayNode(CX.Value) == true)
                            {
                                if (treePHY.Nodes.ContainsKey(CX.Value.Device) == false)
                                {
                                    treePHY.Nodes.Add(CX.Value.Device, CX.Value.Device);
                                }
                                TreeNode nodeLegacy = treePHY.Nodes[CX.Value.Device];
                                nodeLegacy.Tag = "DEVICE";

                                if (CX.Value.Subinterface == false)
                                {
                                    string ge = CX.Value.Interface.ToUpper().Replace("GIGABITETHERNET", "");
                                    nodeLegacy.Nodes.Add(ge, CX.Value.Interface + ": " + CX.Value.ID + " - " + CX.Value.Customer);
                                    nodeLegacy.Nodes[ge].Tag = CX.Value.Unit + "*" + CX.Value.ID;
                                    if (CX.Value.MigrationDate.Year > 1)
                                    {
                                        nodeLegacy.Nodes[ge].ForeColor = Color.Blue;
                                        nodeLegacy.Nodes[ge].Parent.Expand();
                                    }
                                }
                                else
                                {
                                    string ge = CX.Value.Carrier.ToUpper().Replace("GIGABITETHERNET", "");
                                    if (nodeLegacy.Nodes.ContainsKey(ge) == false)
                                    {
                                        nodeLegacy.Nodes.Add(ge, CX.Value.Carrier);
                                        nodeLegacy.Nodes[ge].Tag = "GE";
                                    }
                                    TreeNode nodeGE = nodeLegacy.Nodes[ge];

                                    ge = CX.Value.Interface.ToUpper().Replace("GIGABITETHERNET", "");
                                    nodeGE.Nodes.Add(ge, CX.Value.Interface + ": " + CX.Value.ID + " - " + CX.Value.Customer);
                                    nodeGE.Nodes[ge].Tag = CX.Value.Unit + "*" + CX.Value.ID;
                                    if (CX.Value.MigrationDate.Year > 1)
                                    {
                                        nodeGE.Nodes[ge].ForeColor = Color.Blue;
                                        nodeGE.Nodes[ge].Parent.Expand();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            treePHY.Sort();
        }

        private bool displayNode(classCircuit inCX)
        {
            bool nodeVisible = true;


            nodeVisible = (inCX.Customer.Contains(textboxSearch.Text.ToUpper()) == true || inCX.ID.Contains(textboxSearch.Text.ToUpper()) == true);

            if (inCX.MigrationDate.Year != 1) nodeVisible = cbDisplayScheduled.Checked;

            if (inCX.Diversity == true) nodeVisible = cbDIV.Checked && nodeVisible;
            if (inCX.Type == typeCircuit.xDS0) nodeVisible = cbDense.Checked && nodeVisible;
            if (inCX.MultilinkID != null) nodeVisible = cbMultilink.Checked && nodeVisible;

            if (inCX.Diversity == false && inCX.Type != typeCircuit.xDS0 && inCX.MultilinkID == null) nodeVisible = cbOther.Checked && nodeVisible;

            if (inCX.MigrationDate.Year == 1) nodeVisible = !cbScheduledOnly.Checked && nodeVisible;

            return nodeVisible;
        }

        private void checkDCSSubchannel(TreeNode inNode, bool newsub)
        {
            if (inNode.Level == 3)
            {
                foreach (TreeNode N in inNode.Parent.Nodes)
                {
                    N.Checked = inNode.Checked;
                }
            }
        }

        private void checkChildNodes(TreeNode inNode, bool inCheck)
        {
            if (inNode.Checked == true)
            {
                inNode.Expand();
            }
            else
            {
                inNode.Collapse();
            }

            foreach (TreeNode node in inNode.Nodes)
            {
                if (node.ForeColor != Color.Red)
                {
                    node.Checked = inCheck;
                    if (node.Nodes.Count > 0)
                    {
                        checkChildNodes(node, inCheck);
                    }
                }
            }
        }

        private void checkUnassigned(TreeNode inNode)
        {
            if (inNode.ForeColor == Color.Red) inNode.Checked = false;
        }

        private void checkParentNode(TreeNode inNode)
        {
            foreach (TreeNode node in inNode.Nodes)
            {
                if (node.Checked == true)
                {
                    inNode.Checked = true;
                    break;
                }
                inNode.Checked = false;
            }
            if (inNode.Parent != null)
            {
                checkParentNode(inNode.Parent);
            }
        }

        private void checkPHYParentNode(TreeNode inNode)
        {
            inNode.Parent.Checked = inNode.Checked;
            foreach (TreeNode node in inNode.Parent.Nodes)
            {
                node.Checked = inNode.Parent.Checked;
            }
        }

        private void checkDCSMultilink(TreeNode inNode)
        {
            if (inNode.Level == 2 && (string)inNode.Tag != "DS1.CH")
            {
                string mlID = dbSite.WB.Units[(string)inNode.Tag].Multilink;

                if (mlID != null)
                {
                    string mlDevice = mlID.Split(":".ToCharArray())[0];

                    foreach (TreeNode N in treeDCS.Nodes[mlDevice].Nodes)
                    {
                        switch ((string)N.Tag)
                        {
                            case "STS":
                                foreach (TreeNode NN in N.Nodes)
                                {

                                    if ((NN.Tag != null && (string)NN.Tag !="DS1.CH") && dbSite.WB.Units[(string)NN.Tag].Multilink == mlID) NN.Checked = inNode.Checked;
                                    //else
                                    //{

                                    //}
                                }

                                break;
                            default:

                                break;
                        }
                    }
                }


            }

        }

        private void scheduleNodes(DateTime inDate, TreeNode inNode)
        {
            if (inNode.Checked == true)
            {
                switch ((string)inNode.Tag)
                {
                    case "DEVICE":
                        foreach (TreeNode N in inNode.Nodes)
                            if (N.Checked == true) scheduleNodes(inDate, N);
                        break;
                    case "STS":
                        foreach (TreeNode N in inNode.Nodes)
                            if (N.Checked == true) scheduleNodes(inDate, N);
                        break;
                    case "DS1.CH":
                        foreach (TreeNode N in inNode.Nodes)
                            if (N.Checked == true) scheduleNodes(inDate, N);
                        break;
                    case "GE":
                        foreach (TreeNode N in inNode.Nodes)
                            if (N.Checked == true) scheduleNodes(inDate, N);
                        break;
                    default:
                        string cc = (string)inNode.Tag;
                        //cc = cc.Split("*".ToCharArray()).Last().Trim();
                        string ccc = inNode.Text.Split(" ".ToCharArray())[1];

                        classCircuit CX = dbSite.WB[ccc];
                        CX.MigrationDate = inDate;
                        break;
                }
            }
        }
    }
}
