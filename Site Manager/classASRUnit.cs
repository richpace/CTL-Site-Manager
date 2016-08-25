using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classASRUnit
    {
        // FIELDS //
        private UnitType unitType;
        private int unitSlot;


        // CONSTRUCTORS //
        public classASRUnit(int inSlot, UnitType inType)
        {
            unitType = inType;
            unitSlot = inSlot;
        }

        // PROPERTIES //
        public UnitType Type
        {
            get { return unitType; }
        }
    }
}
