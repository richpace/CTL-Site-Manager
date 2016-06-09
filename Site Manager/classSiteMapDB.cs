using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;

namespace Site_Manager
{
    public class classSiteMapDB: CollectionBase
    {
        // CONSTRUCTORS //

        // FIELDS //

        // INDEXERS //
        public classMap this[int indexMap]
        {
            get
            {
                return (classMap)List[indexMap];
            }
        }

        // PROPERTIES //

        // METHODS //
        public void Add(classMap inMap)
        {
            addMap(inMap);
        }

        public int ASRIndex(string inASR, string inPrefix)
        {
            classMap M;

            for (int m = 0; m < List.Count; m++)
            {
                M = (classMap)List[m];
                if ((M.PrefixASR == inPrefix) && (M.ASR == inASR)) return m;
            }
            return -1;
        }

        // SUPPORT LOGIC //
        private void addMap(classMap inMap)
        {
            List.Add(inMap);
        }

    }
}
