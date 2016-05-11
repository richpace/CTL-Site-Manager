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
            windowArrange();
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
                windowArrange();
                return true;
            }
            return false;
        }

        // EVENTS: MENU //
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            Site.Save();
            windowArrange();
        }

        private void menuFileSaveAs_Click(object sender, EventArgs e)
        {
            Site.SaveAs();
            windowArrange();
        }

        private void menuFileClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuSiteDevicesAddASR_Click(object sender, EventArgs e)
        {
            Site.AddIOXDevice();
            windowArrange();
        }

        private void menuSiteDevicesAddLegacy_Click(object sender, EventArgs e)
        {
            Site.AddIOSDevice();
            windowArrange();
        }

        private void menuSiteMaps_Click(object sender, EventArgs e)
        {
            sdiSiteMaps windowMaps = new sdiSiteMaps(Site);
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
        private void btnMapSites_Click(object sender, EventArgs e)
        {
            dbSite.Mapped = true;
            windowArrange();

            //dbSite.MapUnits();
            dbSite.MapUnits(treeIOS.Nodes, treeIOX.Nodes);
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
                windowArrange();
            }
        }

        private void sdiSite_Resize(object sender, EventArgs e)
        {
            windowArrange();
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

        // SUPPORT LOGIC //
        private void windowArrange()
        {
            dbSite.Assigned = devicesAssigned();

            btnMapSites.Enabled = devicesAssignable() && dbSite.Assigned && !dbSite.Mapped;

            menuSiteDevicesAddASR.Enabled = !dbSite.Mapped;
            menuSiteDevicesAddLegacy.Enabled = !dbSite.Mapped;
            menuSiteCircuits.Enabled = dbSite.Mapped;
            menuSiteMaps.Enabled = dbSite.Mapped;
            menuSiteSchedule.Enabled = dbSite.Mapped;

            displaySiteName();
            displayTree();
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

            if (dbSite.Assigned == false)
            {
                this.Text += " DEVICES UNASSIGNED";
            }
            else
            {
                if (dbSite.Mapped == false)
                {
                    this.Text += " CIRCUITS UNMAPPED";
                }
            }
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
            nodeDevice.Nodes.Add(nodeIOXUnits(inDevice));
            return nodeDevice;
        }

        private TreeNode nodeIOXUnits(classIOX inDevice)
        {
            TreeNode nodeUnits = new TreeNode("Assignable Units:");
            nodeUnits.Nodes.Add("GE: " + inDevice.Units.GE.ToString());
            nodeUnits.Nodes.Add("STS-CH: " + inDevice.Units.CH.ToString());
            nodeUnits.Nodes.Add("STS-CL: " + inDevice.Units.CL.ToString());

            return nodeUnits;
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
            nodeDevice.Nodes.Add(nodeIOSUnits(inDevice));
            nodeDevice.Nodes.Add(nodeIOSPreferredASR(inDevice.PreferredASR));
            nodeDevice.Nodes.Add(nodeIOSInterfaceCount(inDevice));

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

        private TreeNode nodeIOSUnits(classIOS inDevice)
        {
            TreeNode nodeUnits = new TreeNode("Assignable Units:");
            nodeUnits.Nodes.Add("GE: " + inDevice.Units.GE.ToString());
            nodeUnits.Nodes.Add("STS-CH: " + inDevice.Units.CH.ToString());
            nodeUnits.Nodes.Add("STS-CL: " + inDevice.Units.CL.ToString());

            return nodeUnits;
        }

        private TreeNode nodeIOSInterfaceCount(classIOS inDevice)
        {
            TreeNode nodeInterfaceCount = new TreeNode("Interfaces: " + inDevice.Circuits.Count);
            return nodeInterfaceCount;
        }

    }
}
