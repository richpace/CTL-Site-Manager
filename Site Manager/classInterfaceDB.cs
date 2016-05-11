using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace Site_Manager
{
    public class classInterfaceDB : CollectionBase
    {
        // CONSTRUCTORS //

        // FIELDS //

        // INDEXERS //
        public classIOSInterface this[int indexInt]
        {
            get
            {
                return (classIOSInterface)List[indexInt];
            }
        }

        public classIOSInterface this[string indexIntID]
        {
            get
            {
                return (classIOSInterface)List[Contains(indexIntID)];
            }
        }
        // PROPERTIES //

        // METHODS //
        public bool Add(classIOSInterface inInt)
        {
            try
            {
                List.Add(inInt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Contains(string inString)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].ID == inString)
                {
                    return i;
                }
            }
            return -1;
        }
        // SUPPORT LOGIC //

    }
}
