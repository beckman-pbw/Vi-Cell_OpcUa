using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using ViCellBlu;

namespace ViCellBLU_dotNET
{
    [Serializable()]
    public class SampleResults
    {

        public SampleResults()
        {
        }

        [DataMember()]
        public SampleConfig Configuration { get; set; } = new SampleConfig();

        [DataMember()]
        public CellType CellType { get; set; } = new CellType();

        [DataMember()]
        public Guid SummaryResultUuid { get; set; } = new Guid();

        [DataMember()]
        public DateTime Date { get; set; } = DateTime.Now;

        [DataMember()]
        public String Comment { get; set; } = "";

        [DataMember()]
        public UInt32 CellCount { get; set; } = 0;

        [DataMember()]
        public UInt32 ViableCellCount { get; set; } = 0;

        [DataMember()]
        public Double ViablePercent { get; set; } = 0;

        [DataMember()]
        public Double AvgDiam { get; set; } = 0;

        [DataMember()]
        public Double AvgViableDiam { get; set; } = 0;

        [DataMember()]
        public Double AvgCircularity { get; set; } = 0;

        [DataMember()]
        public Double AvgViableCircularity { get; set; } = 0;

        [DataMember()]
        public Double AvgCellsPerImage { get; set; } = 0;

        [DataMember()]
        public Double AvgBgIntensity { get; set; } = 0;

        [DataMember()]
        public UInt32 BubbleCount { get; set; } = 0;

        [DataMember()]
        public UInt32 ClusterCount { get; set; } = 0;

        // ************************************
        public override string ToString()
        {
            string st = "{" + Configuration.Name + "}";
            return st;
        }
    }
}
