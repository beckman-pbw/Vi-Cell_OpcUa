using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace ViCellBLU_dotNET
{

    // ******************************************************************
    [Serializable()]
    public partial class SampleConfig
    {
        public SampleConfig()
        {
            Name = "";
            Position = new SamplePosition();
            Tag = "";
            Dilution = 1;
            CellTypeName = "";
            QCName = "";
            SaveEveryNthImage = 1;
            WashType = ViCellBLU.WashType.Normal;
        }

        [DataMember()]
        public String Name { get; set; }
        
        [DataMember()]
        public SamplePosition Position { get; set; }

        const UInt32 MAX_DILUTION = 9999;

        const UInt32 MAX_SAVE_EVERY_NTH = 99;

        UInt32 _dilution = 1;
        [DataMember()]
        public UInt32 Dilution
        {
            get { return _dilution; }
            set 
            {
                try
                {
                    if ((value >= 1) && (value <= MAX_DILUTION))
                    {
                        _dilution = value;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        [DataMember()]
        public String Tag { get; set; }

        [DataMember()]
        public String CellTypeName { get; set; }

        [DataMember()]
        public String QCName { get; set; }

        UInt32 _saveEveryNthImage = 1;

        [DataMember()]
        public UInt32 SaveEveryNthImage 
        {
            get { return _saveEveryNthImage; }
            set
            {
                if ((value >= 0) && (value <= MAX_SAVE_EVERY_NTH))
                {
                    _saveEveryNthImage = value;
                }
            }
        }

        [DataMember()]
        public ViCellBLU.WashType WashType { get; set; } = ViCellBLU.WashType.Normal;

        // ************************************
        public override string ToString()
        {
            string st = Name + " " + Position.ToString();
            return st;
        }

    }

}
