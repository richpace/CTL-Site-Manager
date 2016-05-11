using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classUnit
    {
        // FIELDS //
        private string unitType;
        private string unitPrefix;
        private string unitPort;
        private int unitWidth = 1;
        private bool unitActive = false;
        private bool unitAssigned = false;
        // CONSTRUCTORS //
        public classUnit(string inType, string inPrefix, int inWidth)
        {
            unitType = inType;
            unitPrefix = inPrefix;
            unitWidth = inWidth;
        }

        public classUnit(string inType, string inPrefix, string inPort)
        {
            unitType = inType;
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
