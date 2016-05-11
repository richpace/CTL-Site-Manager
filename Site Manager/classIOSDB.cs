using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Windows.Forms;
using Cisco_Device;

namespace Site_Manager
{
    public class classIOSDB : CollectionBase
    {
        // FIELDS //
        private SortedSet<string> iosDevices = new SortedSet<string>();

        // CONSTRUCTORS //

        // INDEXERS //
        public classIOS this[int indexDevice]
        {
            get
            {
                return (classIOS)List[indexDevice];
            }
        }

        public classIOS this[string nameDevice]
        {
            get
            {
                return (classIOS)List[Contains(nameDevice)];
            }
        }

        // PROPERTIES //

        // METHODS //
        public bool Add()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.ShowDialog();
            string fnIOSCLI = dlgOpen.FileName;

            if (validFN(fnIOSCLI) == true)
            {
                classIOS iosNew = new classIOS(fnIOSCLI);

                int index = this.Contains(iosNew.Hostname);

                if (index == -1)
                {
                    Add(iosNew);
                }
                else
                {
                    if (iosNew.Timestamp > this[index].Timestamp)
                    {
                        this.RemoveAt(index);
                        Add(iosNew);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Add(classIOS inIOS)
        {
            iosDevices.Add(inIOS.Hostname);
            List.Add(inIOS);
        }

        public int Contains(string inString)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Hostname == inString)
                {
                    return i;
                }
            }
            return -1;
        }

        // SUPPORT LOGIC //
        private bool validFN(string inFN)
        {
            if (inFN.Length > 0)
            {
                if (inFN.ToLower().Contains("-running.txt") == true) return true;
            }
            return false;
        }
    }
}
