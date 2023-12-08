using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.com.MySqlLib.XDStatistic
{
    class CXDRow
    {

        #region class members
        /// <summary>
        /// 维度列，用于定义和显示当前行所处的各维度交叉的是哪些值
        /// </summary>
        public List<CXDColumnItem> DimensionColumns = new List<CXDColumnItem>();

        /// <summary>
        /// 结果列
        /// </summary>
        public List<CXDColumnItem> RstColumns = new List<CXDColumnItem>();

        #endregion class members

        #region 公用辅助函数

        /// <summary>
        /// 根据this.DimensionColumns 给出拼合后的 where 中的字符串。
        /// 该字符串不以 and 开头。用于辅助外部算法合成where语句中的条件。
        /// 本函数调用的前提是 DimensionColumns 已经初始化完毕。
        /// </summary>
        /// <returns></returns>
        public string GetDimensionColumnsSqlStr()
        {
            if (this.DimensionColumns.Count == 0)
                throw new Exception("CXDRow.DimensionColumns has not been defined when build DimensionColumnsSqlStr.");

            string str = string.Empty;

            foreach (CXDColumnItem crrItem in this.DimensionColumns)
            {
                if (crrItem.SqlStr.Length == 0)
                    continue;

                if (str.Length != 0)
                    str += " and ";

                str += crrItem.SqlStr;
            }

            return str;
        }//GetDimensionColumnsSqlStr()

        #endregion //公用辅助函数

        #region 纯文本输出函数。把结果以纯文本的形式向外输出，通常用户把所有结果存储到某个文件中。

        /// <summary>
        /// 生成\t分隔的纯文本标题。 
        /// 循序是：各个维度值、各个结果值、各个SQL
        /// </summary>
        /// <param name="_showSqlStr">是否显示每个值所对应的SQL</param>
        /// <returns>字符串最后不含回车。</returns>
        public string GetHeadersString(bool _showSqlStr)
        {
            string str = string.Empty;
            ////生成标题
            foreach (CXDColumnItem crrItem in this.DimensionColumns)
            {
                str += "维度：" + crrItem.Name + "\t";
            }

            foreach (CXDColumnItem crrItem in this.RstColumns)
            {
                str += "结果：" + crrItem.Name + "\t";
            }

            foreach (CXDColumnItem crrItem in this.RstColumns)
            {
                str += "SQL：" + crrItem.Name + "\t";
            }

            //str += "\n";

            return str;
        }

        /// <summary>
        /// 把当前行的内容以纯文本形式输出，\t作为每一列的间隔。
        /// 循序是：各个维度值、各个结果值、各个SQL
        /// </summary>
        /// <param name="_showSqlStr">是否显示每个值所对应的SQL</param>
        ///  <returns>字符串最后不含回车。</returns>
        public string GetValueRowString(bool _showSqlStr)
        {
            string str = string.Empty;
            ////生成标题
            foreach (CXDColumnItem crrItem in this.DimensionColumns)
            {
                str += crrItem.ValueStr + "\t";
            }

            foreach (CXDColumnItem crrItem in this.RstColumns)
            {
                str += crrItem.ValueStr + "\t";
            }

            foreach (CXDColumnItem crrItem in this.RstColumns)
            {
                str += crrItem.SqlStr + "\t";
            }

            //str += "\n";
            return str;

        }//GetValueRowString()

        #endregion//纯文本输出函数。把结果以纯文本的形式向外输出，通常用户把所有结果存储到某个文件中。

    }//class CXDRow
}//namespace
