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
    public partial class sdiASRSelect : Form
    {
        // FIELDS //
        private string ioxName = null;

        // CONSTRUCTORS //
        public sdiASRSelect(classIOXDB inIOXDB)
        {
            InitializeComponent();

            Left = Cursor.Position.X - 20;
            Top = Cursor.Position.Y - 20;

            formLoad(inIOXDB);
        }

        // PROPERTIES //
        public string ASR
        {
            get
            {
                return ioxName;
            }
        }

        // SUPPORT LOGIC //
        private void formLoad(classIOXDB inDB)
        {
            listboxIOX.Items.Add("<NONE>");
            for (int i = 0; i < inDB.Count; i++)
            {
                if (inDB[i].Assigned == false)
                {
                    listboxIOX.Items.Add(inDB[i].Hostname);
                }
            }
            this.ShowDialog();
        }

        private void buttonASRSave_Click(object sender, EventArgs e)
        {
            if (listboxIOX.SelectedItem == null || listboxIOX.SelectedItem.ToString() == "<NONE>")
            {
                ioxName = null;
            }
            else
            {
                ioxName = listboxIOX.SelectedItem.ToString();
            }

            this.Close();
        }

        private void buttonASRCancel_Click(object sender, EventArgs e)
        {
            this.Tag = "CANX";
            this.Close();
        }
    }
}
