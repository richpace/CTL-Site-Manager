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
    public partial class sdiSiteMaps2 : Form
    {
        // FIELDS //
        private classSiteDatabase dbSite;

        // CONSTRUCTORS //
        public sdiSiteMaps2(classSiteDatabase inDB)
        {
            InitializeComponent();
            dbSite = inDB;
            loadGrids();
            ShowDialog();
        }

        // EVENTS //
        private void btnSiteMap_Click(object sender, EventArgs e)
        {
            exportExcel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // SUPPORT LOGIC //
        private void loadGrids()
        {
            gridUnMaps.Rows.Clear();
            gridMaps.Rows.Clear();
            loadGridMapped(true);
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
        }

        private void loadGridMapped(bool TEST)
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
        }

        private void loadGridMapped()
        {
            classSiteMapDB dbMap = dbSite.Maps;    

            if (dbMap.Count > 0)
            {
                gridMaps.Rows.Add(dbMap.Count - 1);

                for (int m = 0; m < dbMap.Count; m++)
                {
                    gridMaps.Rows[m].Cells[0].Value = getCircuitType(dbMap[m].Type2);
                    gridMaps.Rows[m].Cells[1].Value = dbMap[m].Legacy.ToUpper();
                    gridMaps.Rows[m].Cells[2].Value = dbMap[m].PrefixLegacy;
                    gridMaps.Rows[m].Cells[3].Value = dbMap[m].ASR.ToUpper();
                    gridMaps.Rows[m].Cells[4].Value = dbMap[m].PrefixASR;
                }
            }
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
                case  UnitType.CH:
                    return "DCS";
                case UnitType.CL:
                    return "MON";
                default:
                    return "PHY";
            }
        }

        private void exportExcel()
        {
            Microsoft.Office.Interop.Excel.Application XL = new Microsoft.Office.Interop.Excel.Application();
            Workbook wbMap = XL.Workbooks.Add();
            Worksheet wsMap = wbMap.Sheets[1];
            int line = 2;

            wsMap.Name = "Site Map";
            wsMap.Cells.NumberFormat = "@";
            wsMap.Cells[1, 1].Value = "Legacy Device";
            wsMap.Cells[1, 2].Value = "Legacy Prefix";
            wsMap.Cells[1, 3].Value = "ASR Device";
            wsMap.Cells[1, 4].Value = "ASR Prefix";
            wsMap.Cells[1, 5].Value = "Type";
            wsMap.Rows[1].Font.Bold = true;

            for (int d = 0; d < dbSite.Legacy.Count; d++)
            {
                for (int m = 0; m < dbSite.Maps.Count; m++)
                {
                    if (dbSite.Maps[m].Legacy == dbSite.Legacy[d].Hostname)
                    {
                        wsMap.Cells[line, 1].Value = dbSite.Maps[m].Legacy.ToUpper();
                        wsMap.Cells[line, 2].Value = dbSite.Maps[m].PrefixLegacy;
                        wsMap.Cells[line, 3].Value = dbSite.Maps[m].ASR.ToUpper();
                        wsMap.Cells[line, 4].Value = dbSite.Maps[m].PrefixASR;
                        wsMap.Cells[line, 5].Value = getCircuitType(dbSite.Maps[m].Type);
                        line++;
                    }
                }
            }
            wsMap.Cells.EntireColumn.AutoFit();
            wbMap.SaveAs(dbSite + "-Site Map");
            XL.WindowState = XlWindowState.xlMaximized;
            XL.Visible = true;
        }

        private void gridMaps_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private int countUnmapped()
        {
            int count = 0;

            foreach(classIOS L in dbSite.Legacy)
            {
                foreach(classUnit U in L.Units)
                {
                    if (U.Assigned == false) count++;
                }
            }

            return count;
        }


    }
}
