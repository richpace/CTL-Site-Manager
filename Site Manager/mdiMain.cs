using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Site_Manager
{
    public partial class mdiMain : Form
    {
        // FIELDS //
        private int newTopologyCount = 1;

        // CONSTRUCTORS //
        public mdiMain()
        {
            InitializeComponent();
        }

        // METHODS //

        // EVENTS: MENU //
        private void menuFileNew_Click(object sender, EventArgs e)
        {
            sdiSite windowSite = new sdiSite(newTopologyCount);
            windowSite.MdiParent = this;
            windowSite.Show();

            newTopologyCount++;
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            sdiSite windowSite = new sdiSite(newTopologyCount);
            windowSite.MdiParent = this;
            if (windowSite.Load() == true)
            {
                windowSite.Show();

                newTopologyCount++;
            }
            else
            {
                windowSite.Dispose();
            }
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // EVENTS: GENERAL //

        // SUPPORT LOGIC //

    }
}
