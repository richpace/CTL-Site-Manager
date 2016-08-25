using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;

namespace Site_Manager
{
    public partial class sdiSiteSchedule : Form
    {
        // FIELDS //
        private classSiteDB dbSite;

        // CONSTRUCTORS //
        public sdiSiteSchedule(classSiteDB inDB)
        {
            InitializeComponent();
            dbSite = inDB;
            displayTree();
            ShowDialog();
        }

        // EVENTS //
        private void btnCutsheet_Click(object sender, EventArgs e)
        {
            exportExcel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeSchedule_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                switch (e.Node.Level)
                {
                    case 0:
                        checkNodeChildren(e.Node);
                        break;
                    default:
                        e.Node.Parent.Parent.Checked = e.Node.Checked;
                        checkNodeChildren(e.Node.Parent.Parent);
                        break;
                }
            }
        }

        // SUPPORT LOGIC //
        private void displayTree()
        {
            foreach (KeyValuePair<string, classCircuit> CX in dbSite.WB)
                if (CX.Value.MigrationDate.Year > 1) postInterface(CX.Value);

            if (treeSchedule.Nodes.Count == 0)
            {
                treeSchedule.CheckBoxes = false;
                lblNothing.Visible = true;
                btnCutsheet.Enabled = false;
            }
            else
            {
                treeSchedule.CheckBoxes = true;
                lblNothing.Visible = false;
                btnCutsheet.Enabled = true;
                treeSchedule.Sort();
            }
        }

        private void postInterface(classCircuit inCX)
        {
            string keyType;
            classUnit U = dbSite.WB.Units[inCX.Unit];

            string keyDate = inCX.MigrationDate.ToShortDateString();

            if (treeSchedule.Nodes.ContainsKey(keyDate) == false)
                treeSchedule.Nodes.Add(keyDate, keyDate);

            TreeNode nodeDate = treeSchedule.Nodes[keyDate];

            switch (inCX.Type)
            {
                case typeCircuit.Ethernet:
                    keyType = "PHY";
                    break;
                case typeCircuit.xSTS:
                    keyType = "MON";
                    break;
                default:
                    keyType = "DCS";
                    break;
            }

            if (nodeDate.Nodes.ContainsKey(keyType) == false)
                nodeDate.Nodes.Add(keyType, keyType);

            TreeNode nodeType = nodeDate.Nodes[keyType];

            string textCX = inCX.Customer + "; " + inCX.ID + "; " + inCX.Device;
            string keyCX = inCX.ID;
            nodeType.Nodes.Add(keyCX, textCX);
            nodeType.Nodes[keyCX].Tag = keyCX +"*"+U.ID;
        }

        private void checkNodeChildren(TreeNode inNode)
        {
            for (int n = 0; n < inNode.Nodes.Count; n++)
            {
                inNode.Nodes[n].Checked = inNode.Checked;
                if (inNode.Nodes[n].Nodes.Count > 0) checkNodeChildren(inNode.Nodes[n]);
            }
        }

        private string formatDate(string inDate)
        {
            string dd = inDate.Split("/".ToCharArray())[1];
            string mmm = inDate.Split("/".ToCharArray())[0];
            string yy = inDate.Split("/".ToCharArray())[2].Remove(0,2);

            switch (mmm)
            {
                case "1":
                    mmm = "JAN";
                    break;
                case "2":
                    mmm = "FEB";
                    break;
                case "3":
                    mmm = "MAR";
                    break;
                case "4":
                    mmm = "APR";
                    break;
                case "5":
                    mmm = "MAY";
                    break;
                case "6":
                    mmm = "JUN";
                    break;
                case "7":
                    mmm = "JUL";
                    break;
                case "8":
                    mmm = "AUG";
                    break;
                case "9":
                    mmm = "SEP";
                    break;
                case "10":
                    mmm = "OCT";
                    break;
                case "11":
                    mmm = "NOV";
                    break;
                case "12":
                    mmm = "DEC";
                    break;
            }
            return dd+mmm+yy; 
        }

        private void exportExcel()
        {
            Microsoft.Office.Interop.Excel.Application XL;
            Workbook wbXL;
            TreeNode nodeDate;
            TreeNode nodeType;

            if (treeSchedule.Nodes.Count > 0)
            {
                XL = new Microsoft.Office.Interop.Excel.Application();
                for (int d = 0; d < treeSchedule.Nodes.Count; d++)
                {
                    nodeDate = treeSchedule.Nodes[d];
                    if (nodeDate.Checked == true)
                    {
                        wbXL = XL.Workbooks.Add();
                        for (int t = 0; t < nodeDate.Nodes.Count; t++)
                        {
                            nodeType = nodeDate.Nodes[t];
                            switch (nodeType.Name)
                            {
                                case "DCS":
                                    excelDCS(wbXL, nodeType);
                                    break;
                                case "MON":
                                    excelMON(wbXL, nodeType);
                                    break;
                                case "PHY":
                                    excelPHY(wbXL, nodeType);
                                    break;
                            }
                        }
                        wbXL.Worksheets["Sheet1"].Delete();
                        wbXL.SaveAs("CUTSHEET-" + formatDate(nodeDate.Text));
                    }
                }
                XL.Visible = true;
            }
        }

        private void excelDCS(Workbook inWB, TreeNode inNode)
        {
            Worksheet wsDCS = inWB.Worksheets.Add();
            int xlRow = 0;

            string intCUSTID;
            string intCID;
            string intLDEV;
            string intLINT;
            string intADEV;
            string intAINT;

            wsDCS.Name = "DCS";
            wsDCS.Cells.NumberFormat = "@";
            wsDCS.Range["A1:C1"].MergeCells = true;
            wsDCS.Range["D1:F1"].MergeCells = true;
            wsDCS.Range["G1:I1"].MergeCells = true;
            wsDCS.Range["A1:C1"].Value = "(CUST)";
            wsDCS.Range["D1:F1"].Value = "(FROM)";
            wsDCS.Range["G1:I1"].Value = "(TO)";
            wsDCS.Rows[1].Font.Bold = true;
            wsDCS.Cells[2, 1].Value = "Customer";
            wsDCS.Cells[2, 2].Value = "Circuit ID";
            wsDCS.Cells[2, 3].Value = "Customer DCS Port";
            wsDCS.Cells[2, 4].Value = "Legacy Device";
            wsDCS.Cells[2, 5].Value = "Legacy Interface";
            wsDCS.Cells[2, 6].Value = "Legacy DCS Port";
            wsDCS.Cells[2, 7].Value = "ASR Device";
            wsDCS.Cells[2, 8].Value = "ASR Interface";
            wsDCS.Cells[2, 9].Value = "ASR DCS Port";
            wsDCS.Cells[2, 10].Value = "Engineering Order ID";
            wsDCS.Cells[2, 11].Value = "CAC";
            wsDCS.Cells[2, 12].Value = "T3 ID";
            wsDCS.Rows[2].Font.Bold = true;

            foreach (TreeNode N in inNode.Nodes)
            {
                string tagNode = (string)N.Tag;
                string tagCID = tagNode.Split("*".ToCharArray())[0];
                string tagUID = tagNode.Substring(tagNode.IndexOf("*")+1);
                classCircuit CX = dbSite.WB[tagCID];
                intCID = CX.ID;
                intCUSTID = CX.Customer;
                intLINT = CX.Interface;

                //classMap M = dbSite.Maps[dbSite.WB.Units[dbSite.WB[intCID].Unit].Circuit];
                classMap M = dbSite.Maps[tagUID];
                intLDEV = M.Legacy;
                intADEV = M.ASR;
                intAINT = intLINT.Replace(M.PrefixLegacy, M.PrefixASR);

                xlRow++;
                wsDCS.Cells[xlRow + 2, 1].Value = intCUSTID;
                wsDCS.Cells[xlRow + 2, 2].Value = intCID;
                wsDCS.Cells[xlRow + 2, 4].Value = intLDEV;
                wsDCS.Cells[xlRow + 2, 5].Value = intLINT;
                wsDCS.Cells[xlRow + 2, 7].Value = intADEV;
                wsDCS.Cells[xlRow + 2, 8].Value = intAINT;
            }
            wsDCS.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsDCS.Cells.EntireColumn.AutoFit();
        }

        private void excelMON(Workbook inWB, TreeNode inNode)
        {
            Worksheet wsMON = inWB.Worksheets.Add();
            int xlRow = 0;

            string intCUSTID;
            string intCID;
            string intLDEV;
            string intLINT;
            string intADEV;
            string intAINT;

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

            foreach (TreeNode N in inNode.Nodes)
            {
                string tagNode = (string)N.Tag;
                string tagCID = tagNode.Split("*".ToCharArray())[0];
                string tagUID = tagNode.Substring(tagNode.IndexOf("*") + 1);
                classCircuit CX = dbSite.WB[tagCID];
                intCID = CX.ID;
                intCUSTID = CX.Customer;
                intLINT = CX.Interface;

                //classMap M = dbSite.Maps[dbSite.WB.Units[dbSite.WB[intCID].Unit].Circuit];
                classMap M = dbSite.Maps[tagUID];
                intLDEV = M.Legacy;
                intADEV = M.ASR;
                intAINT = intLINT.Replace(M.PrefixLegacy, M.PrefixASR);

                xlRow++;
                wsMON.Cells[xlRow + 2, 1].Value = intCUSTID;
                wsMON.Cells[xlRow + 2, 2].Value = intCID;
                wsMON.Cells[xlRow + 2, 5].Value = intLDEV;
                wsMON.Cells[xlRow + 2, 6].Value = intLINT;
                wsMON.Cells[xlRow + 2, 8].Value = intADEV;
                wsMON.Cells[xlRow + 2, 9].Value = intAINT;
            }
            wsMON.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsMON.Cells.EntireColumn.AutoFit();
        }

        private void excelPHY(Workbook inWB, TreeNode inNode)
        {
            Worksheet wsPHY = inWB.Worksheets.Add();
            int xlRow = 0;

            string intCUSTID;
            string intCID;
            string intLDEV;
            string intLINT;
            string intADEV;
            string intAINT;

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

            foreach (TreeNode N in inNode.Nodes)
            {
                string tagNode = (string)N.Tag;
                string tagCID = tagNode.Split("*".ToCharArray())[0];
                string tagUID = tagNode.Substring(tagNode.IndexOf("*") + 1);
                classCircuit CX = dbSite.WB[tagCID];
                intCID = CX.ID;
                intCUSTID = CX.Customer;
                intLINT = CX.Interface;

                //classMap M = dbSite.Maps[dbSite.WB.Units[dbSite.WB[intCID].Unit].Circuit];
                classMap M = dbSite.Maps[tagUID];
                intLDEV = M.Legacy;
                intADEV = M.ASR;
                intAINT = intLINT.Replace(M.PrefixLegacy, M.PrefixASR);

                xlRow++;
                wsPHY.Cells[xlRow + 2, 1].Value = intCUSTID;
                wsPHY.Cells[xlRow + 2, 2].Value = intCID;
                wsPHY.Cells[xlRow + 2, 3].Value = intLDEV;
                wsPHY.Cells[xlRow + 2, 4].Value = intLINT;
                wsPHY.Cells[xlRow + 2, 8].Value = intADEV;
                wsPHY.Cells[xlRow + 2, 9].Value = intAINT;
            }
            wsPHY.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsPHY.Cells.EntireColumn.AutoFit();
        }

    }
}
