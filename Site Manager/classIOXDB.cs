using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Windows.Forms;

namespace Site_Manager
{
    public class classIOXDB : CollectionBase
    {
        // FIELDS //

        // CONSTRUCTORS //

        // INDEXERS //
        public classIOX this[int indexDevice]
        { 
            get
            {
                return (classIOX)List[indexDevice];
            }
        }

        public classIOX this[string nameDevice]
        {
            get
            {
                return (classIOX)List[Contains(nameDevice)];
            }
        }

        // PROPERTIES //
        // METHODS //
        public void Add(classIOX inIOX)
        {
            List.Add(inIOX);
        }

        public bool Add()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.ShowDialog();
            string fnIOXCLI = dlgOpen.FileName;

            if (validFN(fnIOXCLI) == true)
            {
                classIOX ioxNew = new classIOX(fnIOXCLI);

                int index = this.Contains(ioxNew.Hostname);

                if (index == -1)
                {
                    Add(ioxNew);
                }
                else
                {
                    if (ioxNew.Timestamp > this[index].Timestamp)
                    {
                        this.RemoveAt(index);
                        Add(ioxNew);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
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
                if (inFN.ToLower().Contains("show_inventory_all") == true) return true;
            }
            return false;
        }

        private string recommendSiteName(string inFN)
        {
            string s = null;
            string[] sa = inFN.Split("\\".ToCharArray());

            s = sa[sa.GetUpperBound(0)];
            s = s.Replace("-show_inventory_all.txt", "");
            sa = s.Split("-".ToArray());
            s = sa[0].ToUpper();

            return s;
        }

        //private bool addIOX()
        //{
        //    try
        //    {
        //        OpenFileDialog dlgOpen = new OpenFileDialog();
        //        dlgOpen.ShowDialog();
        //        string fnIOXCLI = dlgOpen.FileName;

        //        if (validFN(fnIOXCLI) == true)
        //        {
        //            classIOX ioxNew = new classIOX(fnIOXCLI);
        //            //classIOX ioxNew = new classIOX(fnIOXCLI.ToString());

        //            int index = this.Contains(ioxNew.Hostname);

        //            if (index == -1)
        //            {
        //                List.Add(ioxNew);
        //            }
        //            else
        //            {
        //                if (ioxNew.Timestamp > this[index].Timestamp)
        //                {
        //                    this.RemoveAt(index);
        //                    List.Add(ioxNew);
        //                }
        //            }
        //            dbSite = recommendSiteName(fnIOXCLI);
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}
