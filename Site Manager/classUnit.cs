using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public enum UnitType { GE, CH, CL};

    public class classUnit
    {
        // FIELDS //
        private string unitType;
        private string unitPrefix;
        private string unitPort;
        private int unitWidth = 1;
        private bool unitActive = false;
        private bool unitAssigned = false;

        private UnitType unitType2;
        private bool unitAssignable = true;

        // CONSTRUCTORS //
        public classUnit(string inType, string inPrefix, int inWidth)
        {
            unitType = inType;
            unitPrefix = inPrefix;
            unitWidth = inWidth;
        }

        public classUnit(UnitType inType, string inPrefix, int inWidth)
        {
            unitType2 = inType;
            unitPrefix = inPrefix;
            unitWidth = inWidth;
        }

        public classUnit(string inType, string inPrefix, string inPort)
        {
            unitType = inType;
            unitPrefix = inPrefix;
            unitPort = inPort;
        }

        public classUnit(UnitType inType, string inPrefix, string inPort)
        {
            unitType2 = inType;
            unitPrefix = inPrefix;
            unitPort = inPort;
        }

        // PROPERTIES //
        public string Port
        {
            get
            {
                return unitPort;
            }
        }

        public bool Active
        {
            get
            {
                return unitActive;
            }
        }

        public bool Assigned
        {
            get
            {
                return unitAssigned;
                //try
                //{
                //    return unitAssigned;
                //}
                //catch
                //{
                //    return false;
                //}
            }
            set { unitAssigned = value; }
        }

        public string Prefix
        {
            get
            {
                return unitPrefix;
            }
        }

        public string Type
        {
            get
            {
                return unitType;
            }
        }

        public int Width
        {
            get
            {
                return unitWidth;
            }
            set
            {
                unitWidth = value;
            }
        }

        public bool Assignable
        {
            get { return unitAssignable; }
            set { unitAssignable = value; }
        }

        public UnitType Type2
        {
            get { return unitType2; }
            //set { unitType2 = value; }
        }
        // METHODS //
        public void Assign()
        {
            unitAssigned = true;
        }

        public void ActivateUnit()
        {
            unitActive = true;
        }

        // SUPPORT LOGIC //

    }
}
