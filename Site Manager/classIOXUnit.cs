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

        private UnitType unitType2;

        // CONSTRUCTORS //
        public classIOXUnit(int inSlot, string inType)
        {
            unitType = inType;
            unitSlot = inSlot;
        }

        public classIOXUnit(int inSlot, UnitType inType)
        {
            unitType2 = inType;
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

        public UnitType Type2
        {
            get { return unitType2; }
        }
    }
}
