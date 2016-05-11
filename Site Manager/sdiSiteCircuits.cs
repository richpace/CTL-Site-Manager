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
        private classSiteDatabase dbSite;
        private classIOSDB dbIOS;

        private static int xlDCS;
        private static int xlMON;
        private static int xlPHY;

        // CONSTRUCTORS //
        public sdiSiteCircuits(classSiteDatabase inDB)
        {
            InitializeComponent();
            dbSite = inDB;
            dbIOS = dbSite.Legacy;
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
                    checkDCSSubchannel(e.Node);
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
                for (int d = 0; d < treeDCS.Nodes.Count; d++)
                {
                    scheduleNodes(date, treeDCS.Nodes[d]);
                }
            }

            if (treeMON.Nodes.Count > 0)
            {
                for (int d = 0; d < treeMON.Nodes.Count; d++)
                {
                    scheduleNodes(date, treeMON.Nodes[d]);
                }
            }

            if (treePHY.Nodes.Count > 0)
            {
                for (int d = 0; d < treePHY.Nodes.Count; d++)
                {
                    scheduleNodes(date, treePHY.Nodes[d]);
                }
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            exportExcel();
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

        private void parseDB(classIOSDB inIOSDB)
        {

        }

        private void treeDisplay()
        {
            nodesCH();
            nodesCL();
            nodesGE();
        }

        private void nodesCH()
        {
            classIOS ios;
            TreeNode nodeIOS;
            TreeNode nodeUnit;

            treeDCS.Nodes.Clear();

            for (int d = 0; d < dbIOS.Count; d++)
            {
                ios = dbIOS[d];
                nodeIOS = new TreeNode(ios.Hostname);
                treeDCS.Nodes.Add(nodeIOS);

                for (int u = 0; u < ios.Units.Count; u++)
                {
                    if (ios.Units[u].Type == "STS-CH")
                    {
                        nodeUnit = nodesCircuit(ios.Circuits, ios.Units[u], true);
                        if (nodeUnit.Nodes.Count > 0)
                        {
                            nodeIOS.Nodes.Add(nodeUnit);
                        }
                    }
                }
                if (nodeIOS.Nodes.Count == 0)
                {
                    nodeIOS.Remove();
                }
            }
            treeDCS.Sort();
        }

        private void nodesCL()
        {
            classIOS ios;
            TreeNode nodeIOS;
            TreeNode nodeUnit;

            treeMON.Nodes.Clear();

            for (int d = 0; d < dbIOS.Count; d++)
            {
                ios = dbIOS[d];
                nodeIOS = new TreeNode(ios.Hostname);
                treeMON.Nodes.Add(nodeIOS);
                for (int u = 0; u < ios.Units.Count; u++)
                {
                    if (ios.Units[u].Type == "STS-CL")
                    {
                        nodeUnit = nodesCircuit(ios.Circuits, ios.Units[u], true);
                        if (nodeUnit.Nodes.Count > 0)
                        {
                            nodeIOS.Nodes.Add(nodeUnit);
                        }
                    }
                }
                if (nodeIOS.Nodes.Count == 0)
                {
                    nodeIOS.Remove();
                }
            }
            treeMON.Sort();
        }

        private void nodesGE()
        {
            classIOS ios;
            TreeNode nodeIOS;
            TreeNode nodeUnit;

            treePHY.Nodes.Clear();

            for (int d = 0; d < dbIOS.Count; d++)
            {
                ios = dbIOS[d];
                nodeIOS = new TreeNode(ios.Hostname);
                treePHY.Nodes.Add(nodeIOS);
                for (int u = 0; u < ios.Units.Count; u++)
                {
                    if (ios.Units[u].Type == "GE")
                    {
                        nodeUnit = nodesCircuit(ios.Circuits, ios.Units[u], true);
                        if (nodeUnit.Nodes.Count > 0)
                        {
                            nodeIOS.Nodes.Add(nodeUnit);
                        }
                    }
                }
                if (nodeIOS.Nodes.Count == 0)
                {
                    nodeIOS.Remove();
                }
            }
            treePHY.Sort();
        }

        private void nodesSubinterfaces(TreeNode inNode, classInterfaceDB inSubs)
        {
            TreeNode nodeSub;

            for (int s = 0; s < inSubs.Count; s++)
            {
                string textNode = inSubs[s].ID + ": " + inSubs[s].CircuitID + " - " + inSubs[s].Customer;
                nodeSub = new TreeNode(textNode);
                nodeSub.Tag = inSubs[s];
                inNode.Nodes.Add(nodeSub);
            }
        }

        private bool displayNode(classIOSInterface inInt)
        {
            bool passInt = true;

            if (inInt.MigrationDate.Year != 1) passInt = cbDisplayScheduled.Checked;

            if (inInt.Diversity == true) passInt = cbDIV.Checked && passInt;
            if (inInt.SubChannel != null) passInt = cbDense.Checked && passInt;
            if (inInt.MultilinkGroup != null) passInt = cbMultilink.Checked && passInt;

            if (inInt.Diversity == false && inInt.SubChannel == null && inInt.MultilinkGroup == null) passInt = cbOther.Checked && passInt;

            if (inInt.MigrationDate.Year == 1) passInt = !cbScheduledOnly.Checked && passInt;

            return passInt;
        }

        private TreeNode nodesCircuit(classInterfaceDB inInt, classUnit inUnit, bool NEW)
        {
            TreeNode nodeUnit = new TreeNode(inUnit.Prefix);
            classIOSInterface intCircuit;

            if (inUnit.Assigned == false)
            {
                nodeUnit.ForeColor = Color.Red;
                nodeUnit.Expand();
                nodeUnit.EnsureVisible();
            }

            for (int c = 0; c < inInt.Count; c++)
            {
                intCircuit = inInt[c];
                if (intCircuit.Unit == inUnit.Prefix && displayNode(intCircuit) == true)
                {
                    string nodeText = intCircuit.ID + ": " + intCircuit.CircuitID + " - " + intCircuit.Customer;

                    if (textboxSearch.Text.Length == 0)
                    {
                        TreeNode nodeCircuit = new TreeNode(nodeText);

                        if (intCircuit.MigrationDate.Year != 1)
                            nodeCircuit.ForeColor = Color.Blue;
                        else
                            nodeCircuit.ForeColor = nodeUnit.ForeColor;
                        nodeCircuit.Tag = intCircuit;
                        if (intCircuit.SubInterfaces.Count > 0)
                        {
                            nodesSubinterfaces(nodeCircuit, intCircuit.SubInterfaces);
                        }

                        nodeUnit.Nodes.Add(nodeCircuit);
                    }
                    else
                    {
                        if (nodeText.ToLower().Contains(textboxSearch.Text.ToLower()) == true)
                        {
                            TreeNode nodeCircuit = new TreeNode(nodeText);

                            if (intCircuit.MigrationDate.Year != 1)
                                nodeCircuit.ForeColor = Color.Blue;
                            else
                                nodeCircuit.ForeColor = nodeUnit.ForeColor;
                            nodeCircuit.Tag = intCircuit;
                            if (intCircuit.SubInterfaces.Count > 0)
                            {
                                nodesSubinterfaces(nodeCircuit, intCircuit.SubInterfaces);
                            }

                            nodeUnit.Nodes.Add(nodeCircuit);
                            nodeUnit.Expand();
                        }
                    }
                }
            }

            return nodeUnit;
        }

        private void checkDCSSubchannel(TreeNode inNode)
        {
            classIOSInterface intNode = (classIOSInterface)inNode.Tag;
            TreeNode nodeUnit = inNode.Parent;
            classIOSInterface intCircuit;

            if (intNode.SubChannel != null)
            {
                for (int n = 0; n < nodeUnit.Nodes.Count; n++)
                {
                    intCircuit = (classIOSInterface)nodeUnit.Nodes[n].Tag;
                    if (intCircuit.SubChannel == intNode.SubChannel) nodeUnit.Nodes[n].Checked = inNode.Checked;
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
            classIOSInterface intNode = (classIOSInterface)inNode.Tag;

            if (intNode.MultilinkGroup != null)
            {
                string ml = intNode.MultilinkGroup;
                for (int d = 0; d < treeDCS.Nodes.Count; d++)
                {
                    TreeNode nd = treeDCS.Nodes[d];
                    if (nd.Nodes.Count > 0)
                    {
                        for (int u = 0; u < nd.Nodes.Count; u++)
                        {
                            TreeNode nu = nd.Nodes[u];
                            if (nu.Nodes.Count > 0)
                            {
                                for (int c = 0; c < nu.Nodes.Count; c++)
                                {
                                    TreeNode nc = nu.Nodes[c];
                                    if (nc.Nodes.Count > 0)
                                    {
                                        for (int s = 0; s < nc.Nodes.Count; s++)
                                        {
                                            TreeNode ns = nc.Nodes[s];
                                            classIOSInterface intS = (classIOSInterface)ns.Tag;
                                            if (intS.MultilinkGroup == ml) ns.Checked = inNode.Checked;
                                        }
                                    }
                                    else
                                    {
                                        classIOSInterface intC = (classIOSInterface)nc.Tag;
                                        if (intC.MultilinkGroup == ml) nc.Checked = inNode.Checked;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void scheduleNodes(DateTime inDate, TreeNode inNode)
        {
            if (inNode.Level > 1)
            {
                if (inNode.Checked == true)
                {
                    classIOSInterface i = (classIOSInterface)inNode.Tag;

                    i.MigrationDate = inDate;
                }
            }
            if (inNode.Nodes.Count > 0)
            {
                for (int n = 0; n < inNode.Nodes.Count; n++)
                {
                    scheduleNodes(inDate, inNode.Nodes[n]);
                }
            }
        }

        private classMap getMap(string inLID, string inLINT)
        {
            for (int m = 0; m < dbSite.Maps.Count; m++)
            {
                if (dbSite.Maps[m].Legacy == inLID.ToLower() && dbSite.Maps[m].PrefixLegacy == inLINT)
                {
                    return dbSite.Maps[m];
                }
            }
            return null;
        }

        private void exportExcel()
        {
            Microsoft.Office.Interop.Excel.Application XL = new Microsoft.Office.Interop.Excel.Application();
            Workbook wbXL = XL.Workbooks.Add();
            Worksheet wsPHY = excelPHY(wbXL);
            Worksheet wsMON = excelMON(wbXL);
            Worksheet wsDCS = excelDCS(wbXL);
            xlDCS = 3;
            xlMON = 3;
            xlPHY = 3;

            classIOS devLegacy;
            classIOSInterface intLegacy;

            for (int d = 0; d<dbSite.Legacy.Count; d++)
            {
                devLegacy = dbSite.Legacy[d];
                for (int c = 0; c < devLegacy.Circuits.Count; c++)
                {
                    intLegacy = devLegacy.Circuits[c];
                    if (intLegacy.Unit != null && devLegacy.Units[intLegacy.Unit].Assigned == true)
                    {
                        switch (devLegacy.Units[intLegacy.Unit].Type)
                        {
                            case "STS-CH":
                                excelDCSInt(wsDCS, intLegacy, devLegacy.Hostname);
                                break;
                            case "STS-CL":
                                excelMONInt(wsMON, intLegacy, devLegacy.Hostname);
                                break;
                            case "GE":
                                excelPHYInt(wsPHY, intLegacy, devLegacy.Hostname);
                                break;
                        }
                    }
                }
            }

            wsDCS.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsDCS.Rows[2].AutoFilter();
            wsDCS.Cells.EntireColumn.AutoFit();
            wsDCS.Activate();
            XL.ActiveWindow.SplitRow = 2;
            XL.ActiveWindow.FreezePanes = true;

            wsMON.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsMON.Rows[2].AutoFilter();
            wsMON.Cells.EntireColumn.AutoFit();
            wsMON.Activate();
            XL.ActiveWindow.SplitRow = 2;
            XL.ActiveWindow.FreezePanes = true;

            wsPHY.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsPHY.Rows[2].AutoFilter();
            wsPHY.Cells.EntireColumn.AutoFit();
            wsPHY.Activate();
            XL.ActiveWindow.SplitRow = 2;
            XL.ActiveWindow.FreezePanes = true;

            wbXL.Worksheets["Sheet1"].Delete();
            wbXL.SaveAs(dbSite.Name + "-Site Circuits");
            XL.WindowState = XlWindowState.xlMaximized;
            XL.Visible = true;
        }

        private Worksheet excelDCS(Workbook inWB)
        {
            Worksheet wsDCS = inWB.Worksheets.Add();
            wsDCS.Name = "DCS";

            wsDCS.Range["A1:C1"].MergeCells = true;
            wsDCS.Range["D1:G1"].MergeCells = true;
            wsDCS.Range["H1:J1"].MergeCells = true;
            wsDCS.Range["A1:C1"].Value = "(CUST)";
            wsDCS.Range["D1:G1"].Value = "(FROM)";
            wsDCS.Range["H1:J1"].Value = "(TO)";
            wsDCS.Rows[1].Font.Bold = true;
            wsDCS.Cells[2, 1].Value = "Customer";
            wsDCS.Cells[2, 2].Value = "Circuit ID";
            wsDCS.Cells[2, 3].Value = "Customer DCS Port";
            wsDCS.Cells[2, 4].Value = "Legacy Device";
            wsDCS.Cells[2, 5].Value = "STS";
            wsDCS.Cells[2, 6].Value = "Legacy Interface";
            wsDCS.Cells[2, 7].Value = "Legacy DCS Port";
            wsDCS.Cells[2, 8].Value = "ASR Device";
            wsDCS.Cells[2, 9].Value = "ASR Interface";
            wsDCS.Cells[2, 10].Value = "ASR DCS Port";
            wsDCS.Cells[2, 11].Value = "Engineering Order ID";
            wsDCS.Rows[2].Font.Bold = true;

            return wsDCS;
        }

        private Worksheet excelMON(Workbook inWB)
        {
            Worksheet wsMON = inWB.Worksheets.Add();

            wsMON.Name = "MON";
            wsMON.Range["A1:D1"].MergeCells = true;
            wsMON.Range["E1:G1"].MergeCells = true;
            wsMON.Range["H1:J1"].MergeCells = true;
            wsMON.Range["A1:D1"].Value = "(CUST)";
            wsMON.Range["E1:G1"].Value = "(FROM)";
            wsMON.Range["H1:J1"].Value = "(TO)";
            wsMON.Rows[1].Font.Bold = true;
            wsMON.Cells[2, 1].Value = "Customer";
            wsMON.Cells[2, 2].Value = "Circuit ID";
            wsMON.Cells[2, 3].Value = "MON ID";
            wsMON.Cells[2, 4].Value = "Customer MON Port";
            wsMON.Cells[2, 5].Value = "Legacy Device";
            wsMON.Cells[2, 6].Value = "Legacy Interface";
            wsMON.Cells[2, 7].Value = "Legacy MON Port";
            wsMON.Cells[2, 8].Value = "ASR Device";
            wsMON.Cells[2, 9].Value = "ASR Interface";
            wsMON.Cells[2, 10].Value = "ASR MON Port";
            wsMON.Cells[2, 11].Value = "Engineering Order ID";
            wsMON.Rows[2].Font.Bold = true;

            return wsMON;
        }

        private Worksheet excelPHY(Workbook inWB)
        {
            Worksheet wsPHY = inWB.Worksheets.Add();

            wsPHY.Name = "PHY";
            wsPHY.Range["C1:D1"].MergeCells = true;
            wsPHY.Range["E1:F1"].MergeCells = true;
            wsPHY.Range["H1:I1"].MergeCells = true;
            wsPHY.Range["C1:D1"].Value = "(FROM)";
            wsPHY.Range["E1:F1"].Value = "(CUST)";
            wsPHY.Range["H1:I1"].Value = "(TO)";
            wsPHY.Rows[1].Font.Bold = true;
            wsPHY.Cells[2, 1].Value = "Customer";
            wsPHY.Cells[2, 2].Value = "Circuit ID";
            wsPHY.Cells[2, 3].Value = "Legacy Device";
            wsPHY.Cells[2, 4].Value = "Legacy Interface";
            wsPHY.Cells[2, 5].Value = "FDP";
            wsPHY.Cells[2, 6].Value = "FDP Port";
            wsPHY.Cells[2, 7].Value = "Color Code";
            wsPHY.Cells[2, 8].Value = "ASR Device";
            wsPHY.Cells[2, 9].Value = "ASR Interface";
            wsPHY.Cells[2, 10].Value = "Engineering Order ID";
            wsPHY.Rows[2].Font.Bold = true;

            return wsPHY;
        }

        private void excelDCSInt(Worksheet inWS, classIOSInterface inInt, string inLDev)
        {
            classMap wsMap;

            string intCUSTID = inInt.Customer;
            string intCID = inInt.CircuitID;
            string intLDEV = inLDev;
            string intLINT = inInt.ID;
            wsMap = getMap(intLDEV, inInt.Unit);
            string intADEV = wsMap.ASR;
            string intAINT = inInt.ID.Replace(inInt.Unit, wsMap.PrefixASR);

            string intSTS = inInt.Type + inInt.Unit;

            inWS.Cells[xlDCS, 1].Value = intCUSTID;
            inWS.Cells[xlDCS, 2].Value = intCID;
            inWS.Cells[xlDCS, 4].Value = intLDEV.ToUpper();
            inWS.Cells[xlDCS, 5].Value = intSTS.ToUpper();
            inWS.Cells[xlDCS, 6].Value = intLINT.ToUpper();
            inWS.Cells[xlDCS, 8].Value = intADEV.ToUpper();
            inWS.Cells[xlDCS, 9].Value = intAINT.ToUpper();
            xlDCS++;
        }

        private void excelMONInt(Worksheet inWS, classIOSInterface inInt, string inLDev)
        {
            classMap wsMap;

            string intCUSTID = inInt.Customer;
            string intCID = inInt.CircuitID;
            string intLDEV = inLDev;
            string intLINT = inInt.ID;

            wsMap = getMap(intLDEV, inInt.Unit);

            string intADEV = wsMap.ASR.ToUpper();
            string intAINT = inInt.ID.Replace(inInt.Unit, wsMap.PrefixASR);

            inWS.Cells[xlMON, 1].Value = intCUSTID;
            inWS.Cells[xlMON, 2].Value = intCID;
            inWS.Cells[xlMON, 5].Value = intLDEV.ToUpper();
            inWS.Cells[xlMON, 6].Value = intLINT.ToUpper();
            inWS.Cells[xlMON, 8].Value = intADEV.ToUpper();
            inWS.Cells[xlMON, 9].Value = intAINT.ToUpper();
            xlMON++;
        }

        private void excelPHYInt(Worksheet inWS, classIOSInterface inInt, string inLDev)
        {
            classMap wsMap;

            string intCUSTID = inInt.Customer;
            string intCID = inInt.CircuitID;
            string intLDEV = inLDev;
            string intLINT = inInt.ID;

            wsMap = getMap(intLDEV, inInt.Unit);

            string intADEV = wsMap.ASR.ToUpper();
            string intAINT = inInt.ID.Replace(inInt.Unit, wsMap.PrefixASR);

            inWS.Cells[xlPHY, 1].Value = intCUSTID;
            inWS.Cells[xlPHY, 2].Value = intCID;
            inWS.Cells[xlPHY, 3].Value = intLDEV.ToUpper();
            inWS.Cells[xlPHY, 4].Value = intLINT.ToUpper();
            inWS.Cells[xlPHY, 8].Value = intADEV.ToUpper();
            inWS.Cells[xlPHY, 9].Value = intAINT.ToUpper();
            xlPHY++;
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
    }
}
