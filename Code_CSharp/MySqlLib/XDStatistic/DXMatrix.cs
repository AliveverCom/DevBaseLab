using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.com.MySqlLib.XDStatistic
{
    /// <summary>
    /// 多维度交叉矩阵。
    /// 在查询之前，预先生成整个矩阵的定义（含每个格子的SQL），然后再依此执行每一个格子。
    /// </summary>
    class CDXMatrix
    {
        #region class members

        public List<CXDRow> Rows = new List<CXDRow>();

        /// <summary>
        /// 矩阵的预定义。 包含了每一个维度以及维度内的值.
        /// 这个值必须在初始化的时候穿入。
        /// </summary>
        public List<CDimensionDef> DimensionDefs ;

        /// <summary>
        /// 每一个需要被统计的列的定义。
        /// 这个值必须在初始化的时候穿入。
        /// </summary>
        public List<CValueColumnDef> ValueColumnDefs ;

        /// <summary>
        /// 所有的结果。
        /// 在执行前，这个矩阵的被一个格子都被预先初始化（SQL），执行后，每一个格子都具有的最后的执行结果值。
        /// </summary>
        public List<CXDRow> ResultRows = new List<CXDRow>();

        #endregion //class members

        #region constructors

        public CDXMatrix(List<CDimensionDef> _DimensionDefs, List<CValueColumnDef> _ValueColumnDefs)
        {
            this.DimensionDefs   =  _DimensionDefs;
            this.ValueColumnDefs = _ValueColumnDefs;
        }

        #endregion //constructors

        /// <summary>
        /// 在整个矩阵被执行完毕以后，进行必要的收尾工作。比如检查结果的有效性。清除一些异常的结果，或根据已有的结果补充一算一些衍生结果等。
        /// 这个方法如有必要，需要有子类继承并实现之。
        /// </summary>
        /// <returns> true 代表结果可用，false代表结果不可用 。遇到异常时会抛出异常</returns>
        protected bool PostExecute()
        {
            return true;
        }//PostExecute()

        /// <summary>
        /// 真正执行矩阵的函数。
        /// </summary>
        /// <returns></returns>
        protected bool DoExecute()
        {
            //写到这里了
            return false;
        }//DoExecute()


        /// <summary>
        /// 初始化并执行整个统计矩阵，并将结果存储在this.ResultRows中
        /// </summary>
        /// <returns> true 代表结果可用，false代表结果不可用 。遇到异常时会抛出异常</returns>
        public bool Execute()
        {
            InitMatrix();

            if (!DoExecute())
                return false;

            if (!PostExecute())
                return false;

            return true;

        }//Execute()

        /// <summary>
        /// 根据定义，初始化真个矩阵，填入每一个格子的 SQL 或 维度值。
        /// </summary>
        protected void InitMatrix()
        {
            if (this.DimensionDefs == null || this.ValueColumnDefs == null)
                throw new Exception("维度定义或值列定义的对象为空。");

            //初始化各维度交叉的初值
            int[] crrWeiDuJiaoCha = new int[this.DimensionDefs.Count];
            InitWeiDuJiaoCha(crrWeiDuJiaoCha, 0 );

            //for (int crrIdx = 0; crrIdx < crrWeiDuJiaoCha.Length; ++crrIdx)
            for (; ; )
            {
                //如果最高位维度已经超位，则说明所有交叉组合都已迭代完毕，停止循环。
                if (!WeiDuJiaoCha_AddOne(crrWeiDuJiaoCha))
                {
                    break;
                }

                CXDRow crrRow = new CXDRow();

                ////预填充crrRow的所有维度的格子
                PreBuildCrrRow_Dimensions(crrWeiDuJiaoCha, crrRow );

                ////预填充crrRow的所有值的格子
                PreBuildCrrRow_ValueColumns(crrWeiDuJiaoCha, crrRow );

                ////把初始化好的行加入到结果矩阵中
                this.ResultRows.Add(crrRow);
            }//for

        }//InitMatrix()

        /// <summary>
        /// 预填充crrRow的所有维度的格子
        /// </summary>
        /// <param name="crrWeiDuJiaoCha"></param>
        private void PreBuildCrrRow_Dimensions(int[] crrWeiDuJiaoCha, CXDRow crrRow)
        {
            for (int i = 0; i < this.DimensionDefs.Count; i++)
            {
                CXDColumnItem crrItem = new CXDColumnItem()
                    {
                        Name = this.DimensionDefs[i].Name,
                        SqlStr = this.DimensionDefs[i].ItemsDef[crrWeiDuJiaoCha[i]].SqlStr,
                        ValueStr = this.DimensionDefs[i].ItemsDef[crrWeiDuJiaoCha[i]].Name
                    };


                crrRow.DimensionColumns.Add(crrItem);
            }
        }//InitCrrRow()

        /// <summary>
        /// 预填充crrRow的所有值的格子
        /// </summary>
        /// <param name="crrWeiDuJiaoCha"></param>
        private void PreBuildCrrRow_ValueColumns(int[] crrWeiDuJiaoCha, CXDRow crrRow)
        {
            for (int i = 0; i < this.DimensionDefs.Count; i++)
            {
                CXDColumnItem crrItem = new CXDColumnItem();

                crrItem.Name = this.ValueColumnDefs[i].Name;

                string DimensionColumnsSql = crrRow.GetDimensionColumnsSqlStr().Trim();
                string tpStr = this.ValueColumnDefs[i].SqlStr.ToLower().TrimEnd();
                //如果当前ValueSql 不是以 where 结尾的话--说明where后面已经加入了其他条件，则需要事在 ValueSql 尾部加入 and ，以便在后面加上维度的筛选条件。
                if (tpStr.LastIndexOf("where")  != tpStr.Length - "where".Length 
                    && DimensionColumnsSql.Length != 0)
                    crrItem.SqlStr = this.ValueColumnDefs[i].SqlStr + " and " + DimensionColumnsSql ; 
                else 
                    crrItem.SqlStr = this.ValueColumnDefs[i].SqlStr + " " + DimensionColumnsSql ; 


                crrRow.DimensionColumns.Add(crrItem);

            }//for

        }//InitCrrRow()

        /// <summary>
        /// 给crrWeiDuJiaoCha加1。
        /// 把crrWeiDuJiaoCha看成一个多位的数，只是每一位的机制不一样。
        /// 每次给最后一位加1，如果需要进位，则后几位重新置零。
        /// 只要可以正常加1，就返回true。
        /// 当超过最高位表达范围时，返回false。表示结束。
        /// </summary>
        /// <param name="crrWeiDuJiaoCha"></param>
        /// <param name="crrIdx"></param>
        private bool WeiDuJiaoCha_AddOne(int[] crrWeiDuJiaoCha)
        {
            int crrIdx = WeiDuJiaoCha_AddOne(crrWeiDuJiaoCha, crrWeiDuJiaoCha.Length - 1);

            //如果最高位已经超位，则返回false；
            if (crrIdx < 0)
                return false;
            else if(crrIdx != (crrWeiDuJiaoCha.Length -1) )
            {
                this.InitWeiDuJiaoCha(crrWeiDuJiaoCha, crrIdx);
            }

            return true;
        }//WeiDuJiaoCha_AddOne()

        /// <summary>
        /// 递归向上加1
        /// 只要可以正常加1，就返回true。
        /// 
        /// </summary>
        /// <param name="crrWeiDuJiaoCha"></param>
        /// <returns>
        /// 返回实际本增加的那个维度的index
        /// 当超过最高位表达范围时，返回-1。表示结束。
        /// </returns>
        private int WeiDuJiaoCha_AddOne(int[] crrWeiDuJiaoCha, int crrWeiDu )
        {
            ///如果需要进位
            if (crrWeiDuJiaoCha[crrWeiDu] + 1 >= this.DimensionDefs[crrWeiDu].ItemsDef.Count)
            {
                //如果已经是最高位了，就返回false，表示无法再向上增加了。
                if (crrWeiDu == 0)
                    return -1;
                else
                    return WeiDuJiaoCha_AddOne(crrWeiDuJiaoCha, crrWeiDu - 1);
            }
            else //如果不需要进位
            {
                ++crrWeiDuJiaoCha[crrWeiDu];
                return crrWeiDu;
            }

        }//WeiDuJiaoCha_AddOne

        /// <summary>
        /// 从 _crrWeiDuJiaoCha[_idx] 开始，把_idx及其后面所有的单元全部清零。
        /// </summary>
        /// <param name="_idx"></param>
        protected void InitWeiDuJiaoCha(int[] _crrWeiDuJiaoCha, int _idx )
        {
            for (int i = _idx; i < _crrWeiDuJiaoCha.Length; ++i)
            {
                _crrWeiDuJiaoCha[i] = 0;
            }

        }//InitWeiDuJiaoCha

    }//CDXMatrix
}//CDXMatrix
