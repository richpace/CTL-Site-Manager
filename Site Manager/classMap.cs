using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site_Manager
{
    public class classMap
    {
        // FIELDS //
        private string mapType = null;
        private UnitType mapType2;
        private string mapASRID = null;
        private string mapASRPrefix = null;
        private string mapLegacyID = null;
        private string mapLegacyPrefix = null;

        // CONSTRUCTORS //
        public classMap(string inLegacyID, classUnit inLegacyUnit, string inASRID, classUnit inASRUnit)
        {
            mapType2 = inLegacyUnit.Type2;
            mapASRID = inASRID;
            mapASRPrefix = inASRUnit.Prefix;
            mapLegacyID = inLegacyID;
            mapLegacyPrefix = inLegacyUnit.Prefix;

            //inLegacyUnit.Assign();
            //inASRUnit.Assign();
        }

        // PROPERTIES //
        public UnitType Type2
        {
            get { return mapType2; }
        }
        public string Type
        {
            get
            {
                return mapType;
            }
        }

        public string ASR
        {
            get
            {
                return mapASRID;
            }
        }

        public string PrefixASR
        {
            get
            {
                return mapASRPrefix;
            }
        }

        public string Legacy
        {
            get
            {
                return mapLegacyID;
            }
        }

        public string PrefixLegacy
        {
            get
            {
                return mapLegacyPrefix;
            }
        }

        // METHODS //

        // SUPPORT LOGIC //

        //private void createMap()
        //{
            
        //    legID = inL;
        //    legPrefix = inLU.Prefix;
        //    asrID = inA;
        //    asrPrefix = inAU.Prefix;
        //}
    }
}
