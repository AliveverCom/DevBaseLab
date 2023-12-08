using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace Alivever.com.MySqlLib
{
     class CInsertBuilder
    {
        private List<string> In_valuesRows;

        private int In_FirstIndexzNumbuer;

        private int In_RowsPerInsert;

        private string In_HeaderStr;

        public CInsertBuilder(string In_HeaderStr, List<string> _Src, int _FirstIndexzNumbuer,int _ValuesStrPerInsert)
        {
            this.In_FirstIndexzNumbuer = _FirstIndexzNumbuer;
            this.In_valuesRows = _Src;
            this.In_RowsPerInsert = _ValuesStrPerInsert;
            this.In_HeaderStr = In_HeaderStr;
        }//CInsertBuilder

        public List<string> GetInserList()
        {
            List<string> rst = new List<string>();

            StringBuilder crrRstRowStr = new StringBuilder();

            crrRstRowStr.Append(this.In_HeaderStr);
            int lastI_PlusOne = Math.Min(this.In_valuesRows.Count, this.In_FirstIndexzNumbuer + In_RowsPerInsert);
            int _crr_RowsPerInsert = Math.Min(lastI_PlusOne - this.In_FirstIndexzNumbuer, In_RowsPerInsert);

            //bool debug_已经执行过最后一个了 = false;

            //if (In_FirstIndexzNumbuer== 1504000)
            //{
            //    int x = 1, y = 1;
            //    x += y;
            //    y = x;
            //}

            for (int i = this.In_FirstIndexzNumbuer; i < lastI_PlusOne; ++i)
            {
                string crrRow = this.In_valuesRows[i];
                //if (i == 1504572)
                //{
                //    int x = 1, y = 1;
                //    x += y;
                //    y = x;
                //}

                //拼合Value串
                if ((i != 0
                            && ((i - In_FirstIndexzNumbuer + 1) % _crr_RowsPerInsert == 0
                                || (i + 1) == this.In_valuesRows.Count))
                       || _crr_RowsPerInsert == 1)
                {

                    //if (debug_已经执行过最后一个了)
                    //    throw new Exception();

                    crrRstRowStr.Append(crrRow);//crrRstRowStr += crrRow;//

                    //debug_已经执行过最后一个了 = true;
                }
                else
                    crrRstRowStr.Append(crrRow + ",");//crrRstRowStr += crrRow+",";

                //拼合insert value 串
                if ((i != 0
                        && ((i + 1) % In_RowsPerInsert == 0
                            || (i + 1) == In_valuesRows.Count))
                   || In_RowsPerInsert == 1)
                {
                    string tpStr = crrRstRowStr.ToString();
                    //rst.Add(tpStr.Substring(0, crrRstRowStr.Length - 1));//去掉最后一个逗号
                    rst.Add(tpStr);
                    crrRstRowStr.Clear();
                    crrRstRowStr.Append(this.In_HeaderStr);
                    //crrRstRowStr = _headerStr;
                    GC.Collect();
                }
            }//for i

            return rst;

        }
    }//class CInsertBuilder
}
