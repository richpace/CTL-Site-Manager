using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.Excel;

namespace Site_Manager
{
    public partial class sdiSiteSchedule : Form
    {
        // FIELDS //
        private classSiteDatabase dbSite;

        // CONSTRUCTORS //
        public sdiSiteSchedule(classSiteDatabase inDB)
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

        // SUPPORT LOGIC //
        private void displayTree()
        {
            classIOSDB dbLeg = dbSite.Legacy;
            classIOS devLeg;
            classIOSInterface intLeg;

            for (int d = 0; d < dbLeg.Count; d++)
            {
                devLeg = dbLeg[d];
                for (int c = 0; c<devLeg.Circuits.Count; c++)
                {
                    intLeg = devLeg.Circuits[c];
                    if (intLeg.MigrationDate.Year != 1)
                    {
                        postInterface(devLeg, intLeg);
                    }
                }
            }

            if (treeSchedule.Nodes.Count == 0)
            {
                treeSchedule.CheckBoxes = false;
                lblNothing.Visible = true;
            }
            else
            {
                lblNothing.Visible = false;
                treeSchedule.CheckBoxes = true;
                treeSchedule.Sort();
            }
        }

        private classMap getMap(string inLID, string inLINT)
        {
            for (int m =0; m<dbSite.Maps.Count; m++)
            {
                if (dbSite.Maps[m].Legacy == inLID.ToLower() && dbSite.Maps[m].PrefixLegacy == inLINT)
                {
                    return dbSite.Maps[m];
                }
            }
            return null;
        }

        private void postInterface(classIOS inDev, classIOSInterface inInt)
        {
            TreeNode nodeInt = new TreeNode(inInt.Customer + "; " + inInt.CircuitID + "; " + inDev.Hostname.ToUpper());
            nodeInt.Tag = inInt;

            TreeNode nodeDate = getDateNode(inInt.MigrationDate.ToShortDateString(), inDev.Units[inInt.Unit].Type2);
            nodeDate.Nodes.Add(nodeInt);
        }

        private TreeNode getDateNode(string inDate, UnitType inType)
        {
            string typeCX = getCircuitType(inType);
            TreeNode nodeDate;
            TreeNode nodeType;
            if (treeSchedule.Nodes.Count !=0)
            {
                for (int n = 0; n < treeSchedule.Nodes.Count; n++)
                {
                    if ((string)treeSchedule.Nodes[n].Tag == inDate)
                    {
                        nodeDate = treeSchedule.Nodes[n];
                        for (int t = 0; t < nodeDate.Nodes.Count; t++)
                        {
                            if ((string)nodeDate.Nodes[t].Tag == typeCX)
                            {
                                nodeType = nodeDate.Nodes[t];
                                return nodeType;
                            }
                        }
                        nodeType = new TreeNode(typeCX);
                        nodeType.Tag = typeCX;
                        nodeDate.Nodes.Add(nodeType);
                        return nodeType;
                    }
                }
            }
            nodeDate = new TreeNode(inDate);
            nodeDate.Tag = inDate;
            nodeType = new TreeNode(typeCX);
            nodeType.Tag = typeCX;
            nodeDate.Nodes.Add(nodeType);
            
            treeSchedule.Nodes.Add(nodeDate);

            return nodeType;
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

        private void checkNodeChildren(TreeNode inNode)
        {
            for (int n = 0; n < inNode.Nodes.Count; n++)
            {
                inNode.Nodes[n].Checked = inNode.Checked;
                if (inNode.Nodes[n].Nodes.Count > 0) checkNodeChildren(inNode.Nodes[n]);
            }
        }

        private void treeSchedule_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                //if (e.Node.Level == 0) checkNodeChildren(e.Node);
                //if (e.Node.Level == 1)
                //{
                //    e.Node.Parent.Checked = e.Node.Checked;
                //    checkNodeChildren(e.Node.Parent);
                //}
                switch (e.Node.Level)
                {
                    case 0:
                        checkNodeChildren(e.Node);
                        //for (int t = 0; t < e.Node.Nodes.Count; t++) checkNodeChildren(e.Node.Nodes[t]);
                        break;
                    case 1:
                        e.Node.Parent.Checked = e.Node.Checked;
                        checkNodeChildren(e.Node.Parent);
                        //for (int t = 0; t < e.Node.Nodes.Count; t++) checkNodeChildren(e.Node.Nodes[t]);
                        break;
                    case 2:
                        e.Node.Parent.Parent.Checked = e.Node.Checked;
                        checkNodeChildren(e.Node.Parent.Parent);
                        break;
                }
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
                            switch ((string)nodeType.Tag)
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
            classIOSInterface wsInt;
            classMap wsMap;
            string textNode;
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

            for (int n = 0; n < inNode.Nodes.Count; n++)
            {
                textNode = inNode.Nodes[n].Text;
                wsInt = (classIOSInterface)inNode.Nodes[n].Tag;

                intCUSTID = textNode.Split(";".ToCharArray())[0].Trim();
                intCID = textNode.Split(";".ToCharArray())[1].Trim();
                intLDEV = textNode.Split(";".ToCharArray())[2].Trim();
                intLINT = wsInt.ID;

                wsMap = getMap(intLDEV, wsInt.Unit);


                // USE THESE WITH NO PROCDS //
                intADEV = wsMap.ASR.ToUpper();
                intAINT = wsInt.ID.Replace(wsInt.Unit, wsMap.PrefixASR);


                // USE THESE WITH PROCDS //
                //if (wsInt.Diversity == true || wsInt.SubChannel != null)
                //{
                //    intADEV = wsMap.ASR.ToUpper();
                //    intAINT = wsInt.ID.Replace(wsInt.Unit, wsMap.PrefixASR);
                //}
                //else
                //{
                //    intADEV = null;
                //    intAINT = null;
                //}

                wsDCS.Cells[n + 3, 1].Value = intCUSTID;
                wsDCS.Cells[n + 3, 2].Value = intCID;
                wsDCS.Cells[n + 3, 4].Value = intLDEV;
                wsDCS.Cells[n + 3, 5].Value = intLINT;
                wsDCS.Cells[n + 3, 7].Value = intADEV;
                wsDCS.Cells[n + 3, 8].Value = intAINT;
            }
            wsDCS.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsDCS.Cells.EntireColumn.AutoFit();
        }

        private void excelMON(Workbook inWB, TreeNode inNode)
        {
            Worksheet wsMON = inWB.Worksheets.Add();
            classIOSInterface wsInt;
            classMap wsMap;
            string textNode;
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

            for (int n = 0; n < inNode.Nodes.Count; n++)
            {
                textNode = inNode.Nodes[n].Text;
                wsInt = (classIOSInterface)inNode.Nodes[n].Tag;

                intCUSTID = textNode.Split(";".ToCharArray())[0].Trim();
                intCID = textNode.Split(";".ToCharArray())[1].Trim();
                intLDEV = textNode.Split(";".ToCharArray())[2].Trim();

                wsMap = getMap(intLDEV, wsInt.Unit);

                intLINT = wsInt.ID;
                intADEV = wsMap.ASR.ToUpper();
                intAINT = wsInt.ID.Replace(wsInt.Unit, wsMap.PrefixASR);

                wsMON.Cells[n + 3, 1].Value = intCUSTID;
                wsMON.Cells[n + 3, 2].Value = intCID;
                wsMON.Cells[n + 3, 5].Value = intLDEV;
                wsMON.Cells[n + 3, 6].Value = intLINT;
                wsMON.Cells[n + 3, 8].Value = intADEV;
                wsMON.Cells[n + 3, 9].Value = intAINT;
            }
            wsMON.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsMON.Cells.EntireColumn.AutoFit();
        }

        private void excelPHY(Workbook inWB, TreeNode inNode)
        {
            Worksheet wsPHY = inWB.Worksheets.Add();
            classIOSInterface wsInt;
            classMap wsMap;
            string textNode;
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

            for (int n = 0; n < inNode.Nodes.Count; n++)
            {
                textNode = inNode.Nodes[n].Text;
                wsInt = (classIOSInterface)inNode.Nodes[n].Tag;

                intCUSTID = textNode.Split(";".ToCharArray())[0].Trim();
                intCID = textNode.Split(";".ToCharArray())[1].Trim();
                intLDEV = textNode.Split(";".ToCharArray())[2].Trim();

                wsMap = getMap(intLDEV, wsInt.Unit);

                intLINT = wsInt.ID;
                intADEV = wsMap.ASR.ToUpper();
                intAINT = wsInt.ID.Replace(wsInt.Unit, wsMap.PrefixASR);

                wsPHY.Cells[n + 3, 1].Value = intCUSTID;
                wsPHY.Cells[n + 3, 2].Value = intCID;
                wsPHY.Cells[n + 3, 3].Value = intLDEV;
                wsPHY.Cells[n + 3, 4].Value = intLINT;
                wsPHY.Cells[n + 3, 8].Value = intADEV;
                wsPHY.Cells[n + 3, 9].Value = intAINT;
            }
            wsPHY.Cells.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
            wsPHY.Cells.EntireColumn.AutoFit();
        }

    }
}
