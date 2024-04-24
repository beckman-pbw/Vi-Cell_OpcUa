using System;
using System.Windows.Forms;
using ViCellBlu;

namespace ViCellBLU_dotNET_Test
{
    class Misc
    {
	    public static void PopulateSampleResultsBox(SampleResult result, ref ListBox box)
	    {
		    box.Items.Add("Sample Info:");
		    box.Items.Add(string.Format("\tSampleID:   {0}", result.SampleId));
		    box.Items.Add(string.Format("\tStatus:    {0}", result.Status.ToString()));
		    box.Items.Add(string.Format("\tRow:           {0}", result.Position.Row));
		    box.Items.Add(string.Format("\tColumn:     {0}", result.Position.Column));
		    box.Items.Add(string.Format("\tAnalysis By:    {0}", result.AnalysisBy));
		    box.Items.Add(string.Format("\tAnalysis Date:    {0} {1}",
			    result.AnalysisDateTime.ToLocalTime().ToShortDateString(),
			    result.AnalysisDateTime.ToLocalTime().ToShortTimeString()));
		    box.Items.Add(string.Format("\tReanalysis By:    {0}", result.ReanalysisBy));
		    if (result.ReanalysisDateTime.ToBinary() != 0)
		    {
			    box.Items.Add(string.Format("\tReanalysis Date:    {0} {1}", result.ReanalysisDateTime.ToLocalTime().ToShortDateString(), result.ReanalysisDateTime.ToLocalTime().ToShortTimeString()));
			}
		    else
		    {
			    box.Items.Add("\tReanalysis Date:");
		    }
		    box.Items.Add(string.Format("\tTag:    {0}", result.Tag));
            box.Items.Add(string.Format("\tImages For Analysis:    {0}", result.ImagesForAnalysis));
            box.Items.Add(string.Format("\tDilution:    {0}", result.Dilution));
            box.Items.Add(string.Format("\tWashType:    {0}", result.WashType.ToString()));

            box.Items.Add("Results:");
            box.Items.Add(string.Format("\tCellCount:    {0}", result.CellCount));
            box.Items.Add(string.Format("\tViable Cells:    {0}", result.ViableCells));
            box.Items.Add(string.Format("\tTotal Cells/mL:    {0}", result.TotalCellsPerMilliliter));
            box.Items.Add(string.Format("\tViable Cells/mL:    {0}", result.ViableCellsPerMilliliter));
            box.Items.Add(string.Format("\tViability %:    {0}", result.ViabilityPercent));
            box.Items.Add(string.Format("\tAverage Diameter:    {0}", result.AverageDiameter));
            box.Items.Add(string.Format("\tAverage Viable Diameter:    {0}", result.AverageViableDiameter));
            box.Items.Add(string.Format("\tAverage Circularity:    {0}", result.AverageCircularity));
            box.Items.Add(string.Format("\tAverage Viable Circularity:    {0}", result.AverageViableCircularity));
            box.Items.Add(string.Format("\tAverage Cells/Image:    {0}", result.AverageCellsPerImage));
            box.Items.Add(string.Format("\tAverage Background Intensity:    {0}", result.AverageBackgroundIntensity));
            box.Items.Add(string.Format("\tBubble Count:    {0}", result.BubbleCount));
            box.Items.Add(string.Format("\tCluster Count:    {0}", result.ClusterCount));
            box.Items.Add(string.Format("\tQC Status:    {0}", result.QcStatus));

            box.Items.Add("Cell Type Info:");
            box.Items.Add(string.Format("\tQuality Control Name:    {0}", result.QualityControlName));
            box.Items.Add(string.Format("\tCell Type:    {0}", result.CellType));
            box.Items.Add(string.Format("\tMin Diameter:    {0}", result.MinimumDiameter));
            box.Items.Add(string.Format("\tMax Diameter:    {0}", result.MaximumDiameter));
            box.Items.Add(string.Format("\tImages:    {0}", result.Images));
            box.Items.Add(string.Format("\tCell Sharpness:    {0}", result.CellSharpness));
            box.Items.Add(string.Format("\tMin Circularity:    {0}", result.MinimumCircularity));
            box.Items.Add(string.Format("\tDecluster Degree:    {0}", result.DeclusterDegree.ToString()));
            box.Items.Add(string.Format("\tAspiration Cycles:    {0}", result.AspirationCycles));
            box.Items.Add(string.Format("\tViable Spot Brightness:    {0}", result.ViableSpotBrightness));
            box.Items.Add(string.Format("\tViable Spot Area:    {0}", result.ViableSpotArea));
            box.Items.Add(string.Format("\tMixing Cycles:    {0}", result.MixingCycles));
            box.Items.Add(string.Format("\tConcentration Adjustment Factor:    {0}", result.ConcentrationAdjustmentFactor));
        }

        public static string ConvertBytesToSize(double bytes)
        {
            const long byteConversion = 1024;

            // Validating for TB
            if (bytes >= Math.Pow(byteConversion, 4))
            {
                return ConvertToSize(bytes, byteConversion, 4, " TB");
            }

            // Validating for GB
            if (bytes >= Math.Pow(byteConversion, 3))
            {
                return ConvertToSize(bytes, byteConversion, 3, " GB");
            }

            // Validating for MB
            if (bytes >= Math.Pow(byteConversion, 2))
            {
                return ConvertToSize(bytes, byteConversion, 2, " MB");
            }

            // Validating for KB
            if (bytes >= bytes / byteConversion)
            {
                return ConvertToSize(bytes, byteConversion, 1, " KB");
            }

            // Convert to Bytes
            return ConvertToSize(bytes, 1, 1, " Bytes");
        }
        private static string ConvertToSize(double bytes, double byteConversion, int power, string format)
        {
            return $"{Math.Truncate(bytes / Math.Pow(byteConversion, power) * 10) / 10:N1}".ToString() + format;
        }
    }
}
