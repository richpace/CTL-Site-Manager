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

        //public bool Add(classDeviceUnit inUnitLegacy, classDeviceUnit inUnitASR)
        //{
        //    try
        //    {
        //        addMap(inUnitLegacy, inUnitASR);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        // SUPPORT LOGIC //
        private void addMap(classMap inMap)
        {
            List.Add(inMap);
        }

        //private void addMap(classDeviceUnit inUnitLegacy, classDeviceUnit inUnitASR)
        //{
        //    classMap newMap = new classMap();
        //    List.Add(newMap);
        //}
    }
}
