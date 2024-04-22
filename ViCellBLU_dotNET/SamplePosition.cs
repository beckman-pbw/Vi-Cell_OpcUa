using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace ViCellBLU_dotNET
{
    /// <summary>
    /// The location of the sample. 
    /// </summary>
    [Serializable()]
    public partial class SamplePosition
    {

        // ******************************************************************
        public enum RowDef : ushort
        {
            Invalid ='?',
            RowA = 'A',
            FirstRow = RowA,
            RowB = 'B',
            RowC = 'C',
            RowD = 'D',
            RowE = 'E',
            RowF = 'F',
            RowG = 'G',
            RowH = 'H',
            LastRow = RowH,
            ACupRow = 'Y'
        };

        // ******************************************************************
        public enum ColumnDef : Byte
        {
            Invalid = 0,
            C1 = 1,
            FirstColumn = C1,
            C2,
            C3,
            C4,
            C5,
            C6,
            C7,
            C8,
            C9,
            C10,
            C11,
            C12,
            LastColumn = C12,
            LastTubePos = 24
        };

        // ******************************************************************
        public SamplePosition()
        {
            Row = RowDef.ACupRow;
            Column = ColumnDef.C1;
        }

        // ******************************************************************
        public SamplePosition(RowDef row, ColumnDef column)
        {
            Row = RowDef.Invalid;
            Column = ColumnDef.Invalid;

            if ((row >= RowDef.FirstRow) && (row <= RowDef.LastRow))
            {
                if ((column >= ColumnDef.FirstColumn) && (column <= ColumnDef.LastColumn))
                {
                    Row = row;
                    Column = column;
                }
            }
        }

        // ******************************************************************
        [DataMember()]
        public RowDef Row { get; set; }

        // ******************************************************************
        [DataMember()]
        public ColumnDef Column { get; set; }


        // ******************************************************************
        public bool Set(RowDef row, ColumnDef column)
        {
            if ((row >= RowDef.FirstRow) && (row <= RowDef.LastRow))
            {
                if ((column >= ColumnDef.FirstColumn) && (column <= ColumnDef.LastColumn))
                {
                    Row = row;
                    Column = column;
                    return true;
                }
                return false;
            }

            return false;
        }

        // ******************************************************************
        public void SetACup()
        {
            Row = RowDef.ACupRow;
            Column = ColumnDef.C1;
        }

        // ******************************************************************
        public override string ToString()
        {
            string st = "[" + (char)Row + ":" + ((int)Column).ToString() + "]";
            return st;
        }

    }
}
