using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classIOXUnit
    {
        // FIELDS //
        private string unitType;
        private int unitSlot;

        // CONSTRUCTORS //
        public classIOXUnit(int inSlot, string inType)
        {
            unitType = inType;
            unitSlot = inSlot;
        }

        // PROPERTIES //
        public string Type
        {
            get
            {
                return unitType;
            }
        }
    }
}
