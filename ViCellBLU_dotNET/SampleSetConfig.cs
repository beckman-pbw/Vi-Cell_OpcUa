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
    public partial class SampleSetConfig
    {
        public SampleSetConfig()
        {
            Name = "";
            PlateSortOrder = ViCellBlu.PlatePrecessionEnum.ColumnMajor;
            Samples = new List<SampleConfig>();
        }

        [DataMember()]
        public String Name { get; set; }

        [DataMember()]
        public ViCellBlu.PlatePrecessionEnum PlateSortOrder { get; set; }


        [DataMember()]
        public List<SampleConfig> Samples { get; set; }

    };
}
