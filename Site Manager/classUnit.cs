using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public enum UnitType { GE, MON, DCS };

    public class classUnit
    {
        // FIELDS //
        private UnitType unitType;
        private string unitDevice;
        private string unitPrefix;

        private bool unitAssigned = false;
        private bool unitAssignable = true;

        private int unitWidth = 1;
        private bool unitChannelized = false;
        private string unitML = null;

        private string unitCard;
        private string unitPort;

        // CONSTRUCTORS //
        public classUnit(classCircuit inCX)
        {
            unitDevice = inCX.Device;
            unitPort = inCX.Physical;
            unitML = inCX.MultilinkID;

            switch (inCX.Type)
            {
                case typeCircuit.Ethernet:
                    unitType = UnitType.GE;
                    unitPrefix = inCX.Interface.ToUpper().Replace("GIGABITETHERNET", "");
                    if (inCX.Subinterface == true) unitPrefix = unitPrefix.Remove(unitPrefix.LastIndexOf("."));
                    break;
                case typeCircuit.xSTS:
                    unitType = UnitType.MON;
                    unitPrefix = inCX.Interface.ToUpper().Replace("SERIAL", "");
                    unitPrefix = unitPrefix.ToUpper().Replace("POS", "");
                    if (inCX.Subinterface == true) unitPrefix = unitPrefix.Remove(unitPrefix.LastIndexOf("."));
                    unitWidth = inCX.Width;
                    break;
                case typeCircuit.xDS1:
                    unitType = UnitType.DCS;
                    unitPrefix = inCX.Interface.ToUpper().Replace("SERIAL", "");
                    unitPrefix = unitPrefix.Remove(unitPrefix.LastIndexOf(":"));
                    break;
                case typeCircuit.xDS0:
                    unitType = UnitType.DCS;
                    unitChannelized = true;
                    unitPrefix = inCX.Interface.ToUpper().Replace("SERIAL", "");
                    unitPrefix = unitPrefix.Remove(unitPrefix.LastIndexOf(":"));
                    break;
                default:
                    throw new Exception("Unsupported circuit type");
            }
        }

        public classUnit(UnitType inType, string inPrefix, string inPort)
        {
            unitType = inType;
            unitPrefix = inPrefix;
            unitPort = inPort;
        }

        // PROPERTIES //
        public UnitType Type
        {
            get { return unitType; }
        }

        public string Device
        {
            get { return unitDevice; }
            set { unitDevice = value; }
        }

        public string Prefix
        {
            get { return unitPrefix; }
        }

        public string ID
        {
            get { return Device + "*" + Prefix; }
        }

        public string Card
        {
            get { return unitCard; }
            set { unitCard = value; }
        }

        public string Port
        {
            get { return unitPort; }
        }

        public string Multilink
        {
            get { return unitML; }
        }

        public bool Assigned
        {
            get { return unitAssigned; }
            set { unitAssigned = value; }
        }

        public int Width
        {
            get { return unitWidth; }
            set { unitWidth = value; }
        }

        public bool Assignable
        {
            get { return unitAssignable; }
            set { updateAssignable(value); }
        }

        public bool Channelized
        {
            get { return unitChannelized; }
        }

        // METHODS //

        // SUPPORT LOGIC //
        private void updateAssignable(bool inState)
        {
            unitAssignable = inState;
            if (inState == false) unitAssigned = inState;
        }
    }
}
