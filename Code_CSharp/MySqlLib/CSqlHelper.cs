using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace Alivever.com.MySqlLib
{
    /// <summary>
    /// 帮助生成各类SQL 的静态帮助类
    /// </summary>
    public class CSqlHelper
    {



        /// <summary>
        /// 为了避免SQL注入攻击和SQL出错，把代码里的string 翻译成 String.
        /// 把\变成 \\  把 * % 变成\* \%
        /// </summary>
        /// <param name="_codeString"></param>
        public static string EncodeString2SqlString_Select(string _codeString)
        {
            string newStr = _codeString;

            if (_codeString.Contains('\\'))
                newStr = _codeString.Replace(@"\", @"\\");

            if (_codeString.Contains('%'))
                newStr = _codeString.Replace(@"%", @"\%");

            if (_codeString.Contains('*'))
                newStr = _codeString.Replace(@"*", @"\*");

            if (_codeString.Contains('\''))
                newStr = _codeString.Replace("\'", "\\\'");

            if (_codeString.Contains('\"'))
                newStr = _codeString.Replace("\"", "\\\"");

            return newStr;
        }//CodeStringToSqlString

        /// <summary>
        /// 为了避免SQL注入攻击和SQL出错，把代码里的string 翻译成 String.
        /// 把\变成 \\  把 * % 变成\* \%
        /// </summary>
        /// <param name="_codeString"></param>
        public static string EncodeString2SqlString_Insert(string _codeString)
        {
            string newStr = _codeString;

            if (_codeString.Contains('\\'))
                newStr = _codeString.Replace(@"\", @"\\");

            //if (_codeString.Contains('%'))
            //    newStr = _codeString.Replace(@"%", @"\%");

            //if (_codeString.Contains('*'))
            //    newStr = _codeString.Replace(@"*", @"\*");

            if (_codeString.Contains('\''))
                newStr = _codeString.Replace("\'", "''");

            if (_codeString.Contains('\"'))
                newStr = _codeString.Replace("\"", "\\\"");

            return newStr;
        }//CodeStringToSqlString

        /// <summary>
        /// 把一个value列表翻译成insert语句所需的value串('a','b'),('c','d')。一次处理多组。
        /// </summary>
        /// <param name="_values"></param>
        /// <param name="_rowsPerRstRow">要把_valuesRows中多少行放在一个结果行中。推荐范围100-1000。</param>
        /// <param name="_bEmptyStrToNull">当某一列中出现空字符串的时候，自动替换成null</param>
        /// <returns></returns>
        public static List<string> ToInsertValueStr(List<List<string>> _valuesRows, int _rowsPerRstRow)//, bool _bEmptyStrToNull)
        {
            List<string> rst = new List<string>();
            string crrRstRowStr = string.Empty;

            _rowsPerRstRow = Math.Min(_valuesRows.Count, _rowsPerRstRow);

            for (int i = 0; i < _valuesRows.Count; ++i)
            {
                List<string> crrRow = _valuesRows[i];

                crrRstRowStr += ToInsertValueStr(crrRow) + ",";

                if (
                     (i != 0
                        && ((i + 1) % _rowsPerRstRow == 0
                            || (i + 1) == _valuesRows.Count))
                   || _rowsPerRstRow == 1)
                {
                    rst.Add(crrRstRowStr.Substring(0, crrRstRowStr.Length - 1));//去掉最后一个逗号
                    crrRstRowStr = string.Empty;
                }
            }//for i

            return rst;
        }//ToInsertValueStr

        /// <summary>
        /// 将一个
        /// </summary>
        /// <param name="_fileName"></param>
        /// <param name="_rowsPerInsert"></param>
        /// <returns></returns>
        public static List<string> CsvFileToInsertSql(
            string _sqlInsertHeader,
            string _fileName,
            char _fieldTerminatorInRow,
            int _nSkipTopRows, 
            int _rowsPerInsert)//, bool _bEmptyStrToNull)
        {
            string[] lines = File.ReadAllLines(_fileName);
        
            List<string> _valuesRows = new List<string>();

            //////add all contents into _valuesRows
            for (int i=0; i < lines.Length; i++)
            {
                if (_nSkipTopRows >= 0 && _nSkipTopRows - 1 == i)
                    continue;

                //List<string> crrRow = new List<string>();
                //_valuesRows.Add(crrRow);

                string[] items = lines[i].Split(_fieldTerminatorInRow);
                string crrValueSql = ToInsertValueStr(items);
                _valuesRows.Add(crrValueSql);

            }//for(int i=0; i < lines.Length; i++)

            ////// make _valuesRows to sql list
            //List<string> valueList = ToInsertValueStr(_valuesRows, _rowsPerRstRow);
            return ToInsertStr_ByValueStringList(_sqlInsertHeader, _valuesRows, _rowsPerInsert);
        }//ToInsertValueStr

        public static void ImportCvsFile(
            CSingleSqlExecuter _dbConn,
            bool _bIgnorError,
            out List<string> executedMsg,
            string _sqlInsertHeader,
            string _fileName,
            char _fieldTerminatorInRow,
            int _nSkipTopRows,
            int _rowsPerInsert)//, bool _bEmptyStrToNull)
        {

            List<string> sqlList = CsvFileToInsertSql(_sqlInsertHeader,
             _fileName,
             _fieldTerminatorInRow,
             _nSkipTopRows,
             _rowsPerInsert);//, bool _bEmptyStrToNull)

            //List<string> msgList;
            _dbConn.ExecuteSqlList(sqlList, _bIgnorError,out executedMsg);

        }//ImportCvsFile()

        /// <summary>
        /// 将 数据库的 列名列表 变成 inser into _tableName (name1, name2) values
        /// /// </summary>
        /// <param name="_columnNamesInDbTable"></param>
        /// <returns></returns>
        public static string ColumnNames2InsertHeader(string _tableName, List<string> _columnNamesInDbTable)
        {
            string colsStr = string.Empty;

            for(int i = 0; i < _columnNamesInDbTable.Count; i++)
            {
                if (i == _columnNamesInDbTable.Count - 1)
                    colsStr += $"`{_columnNamesInDbTable[i]}`";
                else
                    colsStr += $"`{_columnNamesInDbTable[i]}`,";

            }

            return $"insert into {_tableName} ({colsStr}) values";

        }//ColumnNames2InsertHeader

        public static List<string> ToInsertStr_ByValueList(
            string _headerStr,
            List<string> _valuesRows)
        {
            string valueStr = ToInsertValueStr(_valuesRows);

            List<string> vStrList = new List<string>();
            vStrList.Add(valueStr);

            return ToInsertStr_ByValueStringList(_headerStr, vStrList, 100);
        }//ToInsertStr_ByValueList()

        public static List<string> ToInsertStr_ByValueList(
            string _headerStr,
            List<List<string>> _valuesRows,
            int _rowsPerInsert)
        {

            List<string> vStrList = new List<string>();

            foreach (List<string> crrRow in _valuesRows)
            {
                string valueStr = ToInsertValueStr(crrRow);
                vStrList.Add(valueStr);
            }

            return ToInsertStr_ByValueStringList(_headerStr, vStrList, _rowsPerInsert);
        }//ToInsertStr_ByValueList()


        /// <summary>
        /// 把一个value列表翻译成insert语句所需的value串('a','b'),('c','d')。一次处理多组。
        /// </summary>
        /// <param name="_values"></param>
        /// <param name="_rowsPerInsert">要把_valuesRows中多少行放在一个结果行中。推荐范围100-1000。</param>
        /// <param name="_bEmptyStrToNull">当某一列中出现空字符串的时候，自动替换成null</param>
        /// <returns></returns>
        public static List<string> ToInsertStr_ByValueStringList(
            string _headerStr, 
            List<string> _valuesRows, 
            int _rowsPerInsert)//, bool _bEmptyStrToNull)
        {
            //如果超过10万行，就是用多线程
            //if (_valuesRows.Count > 100000)
                return ToInsertStr_ByValueStringList_MultiThread(_headerStr, _valuesRows, _rowsPerInsert);

           // CInsertBuilder ib = new CInsertBuilder(_headerStr, _valuesRows, 0, _rowsPerInsert);
           // List<string> crrSqlList = ib.GetInserList();

           // return crrSqlList;

            //List<string> rst = new List<string>();

            //StringBuilder crrRstRowStr = new StringBuilder();
            //crrRstRowStr.Append(_headerStr);
            ////string crrRstRowStr = _headerStr;

            //_rowsPerInsert = Math.Min(_valuesRows.Count, _rowsPerInsert);

            //if (_rowsPerRstRow == 1)
            //{
            //    rst.Add( _headerStr + _valuesRows[0]);
            //    return rst;
            //}

            //for (int i = 0; i < _valuesRows.Count; ++i)
            //{
            //    string crrRow = _valuesRows[i];

            //    //拼合Value串
            //    if ((i != 0
            //                && ((i + 1) % _rowsPerInsert == 0
            //                    || (i + 1) == _valuesRows.Count))
            //           || _rowsPerInsert == 1)
            //        crrRstRowStr.Append(crrRow);//crrRstRowStr += crrRow;//
            //    else
            //        crrRstRowStr.Append(crrRow + ",");//crrRstRowStr += crrRow+",";

            //    //拼合insert value 串
            //    if ((i != 0
            //            && ((i + 1) % _rowsPerInsert == 0
            //                || (i + 1) == _valuesRows.Count))
            //       || _rowsPerInsert == 1 )
            //    {
            //        string tpStr = crrRstRowStr.ToString();
            //        //rst.Add(tpStr.Substring(0, crrRstRowStr.Length - 1));//去掉最后一个逗号
            //        rst.Add(tpStr);
            //        crrRstRowStr.Clear();
            //        crrRstRowStr.Append(_headerStr);
            //        //crrRstRowStr = _headerStr;
            //        GC.Collect();
            //    }

            //    //crrRow = null;
            //    //_valuesRows[i] = null;
            //}//for i

            //_valuesRows.Clear();
            //GC.Collect();

            //return rst;
        }//ToInsertValueStr

        /// <summary>
        /// 把一个value列表翻译成insert语句所需的value串('a','b'),('c','d')。一次处理多组。
        /// </summary>
        /// <param name="_values"></param>
        /// <param name="_rowsPerInsert">要把_valuesRows中多少行放在一个结果行中。推荐范围100-1000。</param>
        /// <param name="_bEmptyStrToNull">当某一列中出现空字符串的时候，自动替换成null</param>
        /// <returns></returns>
        protected static List<string> ToInsertStr_ByValueStringList_MultiThread(
            string _headerStr,
            List<string> _valuesRows,
            int _rowsPerInsert)//, bool _bEmptyStrToNull)
        {
            //List<Tuple<int 第一个序号 , int 最后一个序号>> 
            List<int> tasks = new List<int>();

            //////根据 _rowsPerInsert 把所有任务分成任务段
            for(int i=0; i < _valuesRows.Count(); i+= _rowsPerInsert)
            {
                tasks.Add(i);
            }//for 根据 _rowsPerInsert 把所有任务分成任务段

            List<string> rst = new List<string>();

            //////多线程执行每个任务
            Parallel.ForEach(tasks,
                 new ParallelOptions
                 {
                     MaxDegreeOfParallelism = Math.Max(Environment.ProcessorCount/2, 2)
                 },
                 firstI =>
                 {
                     CInsertBuilder ib = new CInsertBuilder(_headerStr, _valuesRows, firstI, _rowsPerInsert);
                     List<string> crrSqlList = ib.GetInserList();

                     lock (rst)
                         rst.AddRange(crrSqlList);


                 });//Parallel.ForEach(tasks,

                 _valuesRows.Clear();
                 GC.Collect();

            return rst;
        }//ToInsertValueStr



        /// <summary>
        /// 把一个value列表翻译成insert语句所需的value串('a','b')。只处理一组。
        /// </summary>
        /// <param name="_values"></param>
        /// <param name="_bEmptyStrToNull">当某一列中出现空字符串的时候，自动替换成null</param>
        /// <returns></returns>
        public static string ToInsertValueStr(IEnumerable<string> _values)//, bool _bEmptyStrToNull)
        {
            if (_values==null || _values.Count() ==0)
                return string.Empty;

            string rst = "(";

            int i=1;
            //string crrSql;
            foreach (string crrItem in _values)
            {
                if ( crrItem == null ) //|| crrItem.Length == 0 )
                    rst += string.Format("null", crrItem);
                else
                    rst += string.Format("'{0}'", crrItem.Replace("'","\\'"));

                if( i!=_values.Count() )
                {
                    rst += ',';//string.Format("'{0}',", crrItem);
                }
                //else
                //{
                //    rst += string.Format("'{0}'", crrItem);
                //}

                i++;
            }//foreach

            rst += ")";

            return rst;

        }// ToInsertValueStr(IEnumerable<string> values)

        /// <summary>
        /// 把一张数据表格完整地从source服务器拷贝到target服务器
        /// </summary>
        /// <param name="_srouceExec">source服务器</param>
        /// <param name="_targetExec">target服务器</param>
        /// <param name="_selectCountStr">source上执行的完整SQL。用于确定数据总行数.select count(*) from x where s</param>
        /// <param name="_selectDataStr">source上执行的完整SQL.用于选取数据，不要包含limit</param>
        /// <param name="_insertHerder">target执行的insert语句values（不含） 之前的部分</param>
        /// <param name="_rowsPerpage">当数据量过大的时候，需要翻页导入。推荐1000</param>
        /// <returns>返回一同导出了多少行数据。失败的时候直接抛出异常</returns>
        public static long CopyDbTableBetweenServers(CSingleSqlExecuter _srouceExec,
            CSingleSqlExecuter _targetExec,
            string _selectCountStr, 
            string _selectDataStr, 
            string _insertHerder, 
            int _rowsPerpage)
        {
            ////获得原数据总行数
            string countStr = _srouceExec.ExecuteSqlStrAsString(_selectCountStr);
            int ncount = 0 ;

            if (countStr == null 
                || !int.TryParse(countStr,out ncount))
                throw new Exception("执行_selectCountStr结果有问题。"+ _selectCountStr );

            if (ncount == 0)
                return 0;

            ////循环每一页，导数据
            for( int i =0; i < ncount; i += _rowsPerpage)
            {
                string selectDataSql = string.Format( "{0} limit {1},{2}", _selectDataStr , i, _rowsPerpage);
                List<List<string>> dataRst = _srouceExec.ExecuteSqlStrAsMatrix(selectDataSql);

                string valuesStr = ToInsertValueStr(dataRst, _rowsPerpage).First();

                string insertSql = string.Format("{0} values {1}", _insertHerder, valuesStr);

                _targetExec.ExecuteSqlStrAsVoid(insertSql);


            }//for( int i =0; i < ncount; i += _rowsPerpage)

            return ncount;
        }//CopyDbTableBetweenServers()

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_insertHeaderStr"></param>
        /// <param name="_items"></param>
        /// <param name="_rowsPerpage"></param>
        /// <param name="_dbConn"></param>
        /// <param name="_isStopWhenAnyItemError"></param>
        /// <returns>error sql </returns>
        public static List<string> InsertIntoTable<T>(string _insertHeaderStr, 
            List<T> _items, 
            int _rowsPerpage, 
            CSingleSqlExecuter _dbConn,
            bool _isStopWhenAnyItemError) where T : IDbInsertIntoAble
        {
            List<string> errorSql = new List<string>();

            if (_items == null || _items.Count == 0)
                return errorSql;

            List<string> values = new List<string>();
            foreach (T crrItem in _items)
                values.Add(crrItem.GetInsertValueStr());

            List<string> sqlList = ToInsertStr_ByValueStringList(_insertHeaderStr, values, _rowsPerpage);

            foreach (string crrSql in sqlList)
            {
                try
                {
                    _dbConn.ExecuteSqlStrAsVoid(crrSql);
                }
                catch (Exception ex)
                {
                    string errString = string.Format("{0}; Sql={1}", ex.Message, crrSql);
                    errorSql.Add(errString);

                    if (_isStopWhenAnyItemError)
                        throw new Exception(errString);
                }//catch (Exception ex)
            }//foreach

            return errorSql;
        }//InsertIntoTable

        /// <summary>
        /// 从结果集中找出对应列名称的项 
        /// </summary>
        /// <param name="_rowData"></param>
        /// <param name="_columnName"></param>
        /// <returns></returns>
        public static string GetValue(List<string> _rowData, List<string> _columnNames,string _columnName)
        {
            int index = _columnNames.FindIndex(x => x.StartsWith(_columnName));

            if (index < 0 || index >= _columnNames.Count)
                throw new Exception(string.Format("Not found _columnName='{0}' in _columnNames", _columnName));

            return _rowData[index];
        }

        /// <summary>
        /// 根据给定的数据库表中的列名，来生成对应的Insert 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="_dbTableName"></param>
        /// <param name="_dbColumnNames"></param>
        /// <returns></returns>
        public static string BuildInsertString_ByDbColumnNames<T>(T obj, string _dbTableName, string[] _dbColumnNames ) where T:class
        {
            /////生成对应属性映射表
            //string valueStr = string.Empty;

            Dictionary<string, MemberInfo> colNameDir = 生成对应属性映射表(obj);

            /////拼接 value 后面的  (,,,)
            string valueSql = BuildInsert_ValueString(obj, _dbColumnNames, colNameDir);

            /////拼接Insert 语句
            string rstStr = $"insert into {_dbTableName} value {valueSql}";

            return rstStr;
        }//

        public static Dictionary<string, MemberInfo> 生成对应属性映射表<T>(T obj) where T : class
        {
            MemberInfo[] ppts = obj.GetType().GetMembers();
            Dictionary<string, MemberInfo> colNameDir = new Dictionary<string, MemberInfo>();

            foreach (MemberInfo crrPpt in ppts)
            {
                AttDbColumn attCol = AttDbColumn.FromMemberInfo(crrPpt);
                    if (attCol != null)
                        colNameDir.Add(attCol.Name, crrPpt);


                //object[] attbList = crrPpt.GetCustomAttributes(true);
                //foreach (object crrAtt in attbList)
                //{
                //    AttDbColumn attCol = crrAtt as AttDbColumn;

                //    if (attCol != null)
                //        colNameDir.Add(attCol.Name, crrPpt);
                //}//foreach (object crrAtt in attbList)
            }//foreach (PropertyInfo crrPpt in ppts)

            return colNameDir;
        }

        public static string BuildUpdateSql_ByClassMemberNames<T>(
            T obj,
            string _dbTableName,
            string[] _classMemberNames) where T : class
        {
            Dictionary<string, MemberInfo> colNameDir = 生成对应属性映射表(obj);

            //生成where
            string whereSql = BuildUpdate_WhereString<T>(obj, colNameDir);

            //生成value部分
            string valueSql = BuildUpdateValueString_ByClassMemberNames(obj, _classMemberNames);

            /////拼接Insert 语句
            string rstStr = $"update `{_dbTableName}` set { valueSql} where {whereSql}";

            return rstStr;
        }//BuildInsertString_ByClassMemberNames



        ///// <summary>
        ///// 根据给定的“代码内类的属性名称列表”，来生成对应的Insert 语句
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <param name="_dbTableName"></param>
        ///// <param name="_classMemberNames"></param>
        ///// <returns></returns>
        //public static string BuildInsertString_ByClassMemberNames<T>(
        //    T obj,
        //    string _dbTableName,
        //    string[] _classMemberNames) where T : class
        //{
        //    //////合成 InsertHeader
        //    Dictionary<string, MemberInfo> colNameDir = 生成对应属性映射表(obj);

        //    List<string> tpNameList = new List<string>();
        //    foreach (string crrKey in colNameDir.Keys)
        //    {
        //        if (_classMemberNames.Contains(colNameDir[crrKey].Name))
        //            tpNameList.Add(crrKey);
        //    }

        //    string sqlInsertHeader = ColumnNames2InsertHeader(_dbTableName, tpNameList);

        //    //生成value部分
        //    string valueSql = BuildInsertValueString_ByClassMemberNames(obj, _classMemberNames);

        //    /////拼接Insert 语句
        //    string rstStr = $"{sqlInsertHeader} { valueSql}";

        //    return rstStr;
        //}//BuildInsertString_ByClassMemberNames

        public static List<string> BuildInsertStringList_ByClassMemberNames<T>(
            List<T> objList,
            string _dbTableName,
            Int16 _itemsInOneSql) where T : class
        {
            return BuildInsertStringList_ByClassMemberNames(objList, _dbTableName, null, _itemsInOneSql);
        }//BuildInsertStringList_ByClassMemberNames()


        /// <summary>
        /// 针对一个Obj列表，根据给定的“代码内类的属性名称列表”，来生成对应的Insert 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        /// <param name="_dbTableName"></param>
        /// <param name="_classMemberNames">可以为空。 null表示全部att属性</param>
        /// <returns></returns>
        public static List<string> BuildInsertStringList_ByClassMemberNames<T>(
            List<T> objList, 
            string _dbTableName, 
            string[] _classMemberNames,
            Int16 _itemsInOneSql) where T : class
        {
            List<CExpandedAttDbColumn> attTree;
            string sqlInsertHeader;
            BuildInsertStringList_ByClassMemberNames_合成语句前的处理(objList, _dbTableName, _classMemberNames, out attTree, out sqlInsertHeader);

            //////合成 完整语句
            List<string> sqlList = new List<string>();

            //按照每行Sql数量，预先对对象进行分组，以便并行处理
            List<List<T>> ObjListGroups = new List<List<T>>();
            List<T> crrObjList = null;
            for (int i = 0; i < objList.Count; i++)
            {
                if (i % _itemsInOneSql == 0)
                {
                    crrObjList = new List<T>();
                    ObjListGroups.Add(crrObjList);
                }

                crrObjList.Add(objList[i]);
            }//for (int i = 0; i < objList.Count; i++)

            //并发合成SQL
            foreach (List<T> crrList in ObjListGroups)
            {
                if (crrList.Count == 0)
                    continue;

                StringBuilder strb = new StringBuilder();

                strb.Append(sqlInsertHeader);
                for (int i = 0; i < crrList.Count; i++)
                {
                    T crrObject = crrList[i];
                    string valueSql = BuildInsertValueString_ByClassMemberNames(crrObject, attTree);
                    valueSql += (i == crrList.Count - 1) ? string.Empty : ", ";
                    strb.Append(valueSql);

                }
                lock (sqlList)
                {
                    string crrSql = strb.ToString();
                    sqlList.Add(crrSql);
                }
            }//并发合成SQL foreach(List<T> crrList in ObjListGroups)

            return sqlList;

        }//BuildInsertStringList_ByClassMemberNames<T>（）

        private static void BuildInsertStringList_ByClassMemberNames_合成语句前的处理<T>(List<T> objList, string _dbTableName, string[] _classMemberNames, out List<CExpandedAttDbColumn> attTree, out string sqlInsertHeader) where T : class
        {
            if (objList == null || objList.Count == 0)
                throw new Exception("Error. objList shouldn't be null or Count=0 ");

            //if (_classMemberNames == null || _classMemberNames.Length == 0)
            //    throw new Exception("Error. _classMemberNames shouldn't be null or Count=0 ");


            /////获取当前对象的全部成员定义
            //List<Tuple<string, AttDbColumn, Type>> attCols = new List<Tuple<string, AttDbColumn, Type>>();

            //Type objType = objList[0].GetType();
            //递归_将给定Type的AttDbColumn展开到结果列表(
            //    objType,
            //    null,
            //    ref attCols);//List<Tuple<string dbColumnName, AttDbColumn colInfo>>

            //获得最准确的 对象定义
            Type objType = null;
            foreach (var crrItem in objList)
            {
                if (crrItem != null)
                {
                    if (objType == null)
                    {
                        objType = crrItem.GetType();
                        break; //如果今后一个列表中的对象类型是不同形式的子类的话，就去掉这个break，从而继续扫描所有Item，触发else逻辑
                    }
                    else
                    {
                        //如果列表中的对象类型不统一，则使用给定T基类的类型
                        if (!objType.Equals(crrItem))
                        {
                            objType = null;
                            break;
                        }
                    }

                }
            }//foreach (var crrItem in objList)

            if (objType == null)
                objType = typeof(T);// objList.GetType().GetElementType();


            attTree = new List<CExpandedAttDbColumn>();
            CExpandedAttDbColumn.递归_将给定Type的AttDbColumn展开到结果_树型结构(objType, null, ref attTree, false);

            if (_classMemberNames != null)
                attTree = CExpandedAttDbColumn.FilterByMemberNamesOfFirstLevelClass(attTree, _classMemberNames);

            List<CExpandedAttDbColumn> attList = null;
            CExpandedAttDbColumn.Tree2List(attTree, ref attList);

            if (attList.Count == 0)
                throw new Exception($"Can't find [AttDbColumn] on Type '{objType.Name}'. " +
                    $"Please check your class defination." +
                    $"可能属性没有赋予[AttDbColumn]，或者赋予了[AttDbColumn]的属性不是public");

            //////检查 _names 中是否有 class定义中不存在的东东
            if (_classMemberNames != null)
            {
                foreach (string crrName in _classMemberNames)
                {
                    var rst = from crrAtt in attTree where crrAtt.memberInfo.Name == crrName select crrName;
                    if (rst == null || rst.Count() == 0)
                        throw new Exception($"<{objType.Name}>obj does not contain a member " +
                            $"named as '{crrName}'." +
                            $"检查参数 List<class.member>中是不是写错名字了，或者错写成 dbColumnName了 ");
                }//foreach (string crrName in _names)
            }//if (_names != null)

            //List<string> tpNameList = (from crrTuple in attList select crrTuple.DbColumnName).ToList();
            List<string> tpNameList = new List<string>();
            foreach (var crrCol in attList)
            {
                if (crrCol.MemberType.IsArray && crrCol.AttColumn.MaxLength > 0)
                {
                    ;
                    for (int i = 0; i < crrCol.AttColumn.MaxLength; i++)
                    {
                        tpNameList.Add(crrCol.DbColumnName + i);
                    }
                }
                else
                    tpNameList.Add(crrCol.DbColumnName);
            }//foreach (var crrCol in attList)

            sqlInsertHeader = ColumnNames2InsertHeader(_dbTableName, tpNameList);
        }

        /// <summary>
        /// 针对一个Obj列表，多线程生成Sql列表，并多线程执行Sql插入。
        /// 相比先合成全部Sql然后在执行的做法，这个方法合成后直接执行。不产生增量内存。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        /// <param name="_dbTableName"></param>
        /// <param name="_classMemberNames">可以为空。 null表示全部att属性</param>
        /// <returns></returns>
        public static void InsertIntoDb<T>(
            List<T> objList,
            string _dbTableName,
            string[] _classMemberNames,
            Int16 _itemsInOneSql,
            out List<string> _resultMessageList,
            CSingleSqlExecuter _dbConn,
            int _maxThreads) where T : class
        {
            if (_dbConn == null || _maxThreads < 1)
                throw new Exception($"Error: _dbConn==null or _maxThreads <1");


            List<CExpandedAttDbColumn> attTree;
            string sqlInsertHeader;
            BuildInsertStringList_ByClassMemberNames_合成语句前的处理(objList, _dbTableName, _classMemberNames, out attTree, out sqlInsertHeader);

            //////合成 完整语句
            List<string> sqlList = new List<string>();

            //按照每行Sql数量，预先对对象进行分组，以便并行处理
            List<List<T>> ObjListGroups = new List<List<T>>();
            List<T> crrObjList=null;
            for (int i = 0; i < objList.Count; i++)
            {
                if (i % _itemsInOneSql == 0)
                {
                    crrObjList = new List<T>();
                    ObjListGroups.Add(crrObjList);
                }

                crrObjList.Add(objList[i]);
            }//for (int i = 0; i < objList.Count; i++)


            //List<string> _resultMessageList;
            //临时禁用索引

            //确认当前表是否被锁了，如果被锁表的话，那就只能单线程运行了。
            //string sqlLockedTabke = "SHOW OPEN TABLES ";
            //List<List<string>> lockedTables = _dbConn.ExecuteSqlStrAsMatrix(sqlLockedTabke);
            //string[] mysqlDbNames = new string[] { "mysql", "performance_schema", "information_schema", };
            //var crrTableLockInfo = lockedTables.Where(a => mysqlDbNames.Contains(a[0]) 
            //    && a[1]==_dbTableName 
            //    && ( a[2] !="0" || a[3] !="0");

            _resultMessageList = new List<string>();
            //并发合成SQL
            if (_maxThreads == 1 || ObjListGroups.Count==1) //if 单线程
            {
                foreach (List<T> crrList in ObjListGroups)
                {
                    if (crrList.Count == 0)
                        throw new Exception("crrList.Count == 0");

                    InsertIntoDb_内嵌方法_合成一条Sql并执行(crrList, attTree, sqlInsertHeader, _dbConn,  _resultMessageList);

                }//foreach (List<T> crrList in ObjListGroups)
            }//if 单线程
            else //否则多线程执行
            {
                CSingleSqlPool dbConnPool = new CSingleSqlPool();
                dbConnPool.Add(_dbConn, _maxThreads + 2);

                List<string> msgList = new List<string>();
                //foreach (List<T> crrList in ObjListGroups)
                Parallel.ForEach(ObjListGroups,
                new ParallelOptions() { MaxDegreeOfParallelism = _maxThreads },
                crrList =>
                {
                    if (crrList == null ||crrList.Count == 0)
                        throw new Exception($"crrList.Count == '{(crrList==null?"null": "crrList.Count") }'"); 

                    CSingleSqlExecuter crrDbConn;
                    crrDbConn = dbConnPool.Borrow("CHelper.InsertIntoDb<T>()", true);

                    if (crrDbConn == null)
                        throw new Exception("dbConnPool.Borrow() returns null");

                    InsertIntoDb_内嵌方法_合成一条Sql并执行(crrList, attTree, sqlInsertHeader, crrDbConn, msgList);

                    dbConnPool.Return(crrDbConn);
                    //sqlList.Add(crrSql);
                    //}//foreach


                });//多线程 并发合成SQL foreach(List<T> crrList in ObjListGroups)

                dbConnPool.CloseAndClearAllConnections();

                _resultMessageList.AddRange(msgList);
            }//else //否则多线程执行


            //return sqlList;

            ////////下面是以前单线程代码，不执行
            //StringBuilder sb = new StringBuilder();

            //for (int i = 0; i< objList.Count; i++ )
            //{
            //    T crrObject = objList[i];

            //    string valueSql = BuildInsertValueString_ByClassMemberNames(crrObject,  attTree);
            //    valueSql += (i != 0 && (i+1) % _itemsInOneSql == 0 || i== objList.Count-1) ? string.Empty : ", ";
            //    sb.Append(valueSql);

            //    if (i != 0 && (i + 1) % _itemsInOneSql == 0 || i == objList.Count - 1)
            //    {
            //        /////拼接Insert 语句
            //        //string rstStr = $"insert into {_dbTableName} value {sb.ToString()} ;";
            //        string str = sqlInsertHeader + sb.ToString();
            //        sqlList.Add(str);
            //        sb.Clear();
            //        sb = new StringBuilder();

            //    }
            //}//foreach(T crrObject in obj)


            //return sqlList;
        }//BuildInsertStringList_ByClassMemberNames

        private static void InsertIntoDb_内嵌方法_合成一条Sql并执行<T>(
            List<T> crrList, 
            List<CExpandedAttDbColumn> attTree, 
            string sqlInsertHeader, 
            CSingleSqlExecuter crrDbConn,
             List<string> _resultMessageList) where T : class
        {
            StringBuilder strb = new StringBuilder();

            strb.Append(sqlInsertHeader);
            for (int i = 0; i < crrList.Count; i++)
            {
                T crrObject = crrList[i];

                if (crrObject == null)
                    throw new Exception("InsertIntoDb_内嵌方法_合成一条Sql并执行()中，crrObject == null");

                string valueSql = BuildInsertValueString_ByClassMemberNames(crrObject, attTree);
                valueSql += (i == crrList.Count - 1) ? string.Empty : ", ";
                strb.Append(valueSql);

            }

            string crrSql = strb.ToString();
            if (crrSql == null)
                throw new Exception("执行时发现crrSql=null");

            try
            {
                lock (crrDbConn)
                {
                    crrDbConn.ExecuteSqlStrAsVoid(crrSql);
                }
            }
            catch(Exception ex)
            {
                lock (_resultMessageList)
                    _resultMessageList.Add( $"Error:{ex.Message}");
                throw new Exception(ex.Message);
            }//try
            strb.Clear();
        }//InsertIntoDb_内嵌方法_合成一条Sql并执行()




        /// <summary>
        /// 根据给定的“代码内类的属性名称列表”，来生成对应的Insert 语句中 value之后的值列表 ('1','2','3')
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="_classMemberNames"></param>
        /// <returns></returns>
        public static string BuildInsertValueString_ByClassMemberNames<T>(T obj, List<CExpandedAttDbColumn> attTree) where T : class
        {

            //Type objType = obj.GetType();
            //List<CExpandedAttDbColumn> attTree = new List<CExpandedAttDbColumn>();
            //CExpandedAttDbColumn.递归_将给定Type的AttDbColumn展开到结果_树型结构(objType, null, ref attTree);

            //if (_classMemberNames != null)
            //    attTree = CExpandedAttDbColumn.FilterByMemberNamesOfFirstLevelClass(attTree, _classMemberNames);


            /////拼接 value 后面的  (,,,)
            string valueSql = BuildInsert_ValueString(obj,  attTree, false);
            return valueSql;
        }//BuildInsertValueString_ByClassMemberNames

        

        public static string BuildUpdateValueString_ByClassMemberNames<T>(T obj, string[] _classMemberNames) where T : class
        {
            /////生成对应属性映射表
            string valueStr = string.Empty;

            MemberInfo[] ppts = obj.GetType().GetMembers();
            Dictionary<string, MemberInfo> colNameDir = new Dictionary<string, MemberInfo>();

            foreach (MemberInfo crrPpt in ppts)
            {
                object[] attbList = crrPpt.GetCustomAttributes(true);
                foreach (object crrAtt in attbList)
                {
                    AttDbColumn attCol = crrAtt as AttDbColumn;

                    if (attCol != null)
                    {
                        colNameDir.Add(crrPpt.Name, crrPpt);
                        break;
                    }
                }//foreach (object crrAtt in attbList)
            }//foreach (PropertyInfo crrPpt in ppts)

            /////拼接 value 后面的  (,,,)
            string valueSql = BuildUpdate_ValueString(obj, _classMemberNames, colNameDir);
            return valueSql;
        }//BuildInsertValueString_ByClassMemberNames

        
        /// <summary>
        /// 递归执行。
        /// 根据指定的 _names名称列表，从colNameDir找到对应的obj属性的值，
        /// 然后拼合出Insert 语句中 value之后的值列表 ('1','2','3')
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="_names"></param>
        /// <param name="colNameDir"></param>
        /// <returns></returns>
        private static string BuildInsert_ValueString<T>(
            T obj, 
            //string[] _names, 
            List<CExpandedAttDbColumn> attTree,
            bool _is子在递归中
            ) where T : class
        {

            /////拼接 value 后面的  (,,,)
            string valueSql = string.Empty;
            //for (int i = 0; i < _names.Length; i++)
            //{
            //    string crrColumnName = _names[i];

            //foreach(CExpandedAttDbColumn crrAtt in attTree)

            for (int i = 0; i < attTree.Count; i++)
            {
                CExpandedAttDbColumn crrAtt = attTree[i];
                //if (_names != null)
                //{
                //    if (!_names.Contains(crrAtt.memberInfo.Name))
                //        continue;
                //}

                ///如果当前对象需要展开，就递归执行
                object valueObj = GetMemberByMemberInfo(obj, crrAtt.memberInfo);

                if (crrAtt.ExpandedMemberTypes != null)
                {
                    string tpRst = BuildInsert_ValueString(
                        valueObj,
                        //null,
                        crrAtt.ExpandedMemberTypes,
                        true);

                    valueSql += (i != attTree.Count - 1) ? tpRst + ", " : tpRst;
                    continue;
                }
                //switch (crrAtt.memberInfo)
                //{
                //    case FieldInfo fieldInfo:
                //        valueObj = fieldInfo.GetValue(obj); break;
                //    case PropertyInfo propertyInfo:
                //        valueObj = propertyInfo.GetValue(obj, null); break;
                //    default:
                //        throw new InvalidOperationException();
                //}
                //object valueObj = ((PropertyInfo)colNameDir[crrColumnName]).GetValue(obj,null);
                
                string valStr = null;
                if ((valueObj is float && float.IsNaN( (float)valueObj)) 
                    || (valueObj is double && double.IsNaN((double)valueObj)))
                {
                    valStr = "NULL";
                }
                else if (valueObj is bool)
                    valStr = (bool)valueObj? "'true'": "'false'";
                else if (crrAtt.MemberType.IsArray && crrAtt.AttColumn.IsExpentArrayItemsInCurrentTable && crrAtt.AttColumn.MaxLength >0) //如果是数组
                {
                    valStr = string.Empty;
                    var array = valueObj != null? valueObj as Array : new object[crrAtt.AttColumn.MaxLength]; //Convert.ChangeType(valueObj, crrAtt.MemberType);
                    int x = 0;

                    if (crrAtt.AttColumn.MaxLength != array.Length)
                        throw new Exception($"数据列'{crrAtt.DbColumnName}',内存中变量数组的长度({array.Length})不等于列定义中的长度(crrAtt.AttColumn.MaxLength)");

                    foreach (var crrItem in array)
                    {
                        valStr += (crrItem == null) ? "NULL" : $"'{CSqlHelper.EncodeString2SqlString_Insert(crrItem.ToString())}'"; //crrAtt.DbColumnName;//

                        if (x != array.Length - 1)
                            valStr += ", ";

                        x++;

                    }
                }//else if如果是数组
                else
                    valStr = (valueObj == null) ? "NULL" : $"'{CSqlHelper.EncodeString2SqlString_Insert( valueObj.ToString())}'"; //crrAtt.DbColumnName;//

                valueSql += (i != attTree.Count - 1) ?
                     valStr + ", " : //$"'{CodeStringToSqlString(valStr)}', " :
                     valStr;//$"'{CodeStringToSqlString(valStr)}'";

            }//foreach (string crrColumnName in _dbColumnNames)

            if (_is子在递归中)
                return valueSql;
            else
            {
                valueSql = $"({valueSql})";
                return valueSql;
            }
        }// BuildInsertValueString

        private static object GetMemberByMemberInfo(object _obj, MemberInfo _mbInfo )
        {
            if (_obj == null)
                return null;

            object valueObj = null;
            switch (_mbInfo)
            {
                case FieldInfo fieldInfo:
                    valueObj = fieldInfo.GetValue(_obj); break;
                case PropertyInfo propertyInfo:
                    valueObj = propertyInfo.GetValue(_obj, null); break;
                default:
                    throw new InvalidOperationException();
            }
            return valueObj;
        }

        /// <summary>
        /// 根据指定的 _names名称列表，从colNameDir找到对应的obj属性的值，
        /// 然后拼合出Insert 语句中 value之后的值列表 ('1','2','3')
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="_names"></param>
        /// <param name="colNameDir"></param>
        /// <returns></returns>
        private static string BuildInsert_ValueString<T>(T obj, string[] _names, Dictionary<string, MemberInfo> colNameDir) where T : class
        {
            /////拼接 value 后面的  (,,,)
            string valueSql = string.Empty;
            for (int i = 0; i < _names.Length; i++)
            {
                string crrColumnName = _names[i];

                if (!colNameDir.ContainsKey(crrColumnName))
                    throw new Exception($"<{obj.GetType().Name}>obj is not contains a member " +
                        $"who has CustomAttribute AttDbColumn.colNameDir = '{crrColumnName}'." +
                        $"检查参数 List<class.member>中是不是写错名字了，或者错写成 dbColumnName了 ");

                object valueObj = null;
                switch (colNameDir[crrColumnName])
                {
                    case FieldInfo fieldInfo:
                        valueObj = fieldInfo.GetValue(obj); break;
                    case PropertyInfo propertyInfo:
                        valueObj = propertyInfo.GetValue(obj, null); break;
                    default:
                        throw new InvalidOperationException();
                }
                //object valueObj = ((PropertyInfo)colNameDir[crrColumnName]).GetValue(obj,null);
                string valStr = (valueObj == null) ? "NULL" : valueObj.ToString();

                valueSql += (i != _names.Length - 1) ?
                     $"'{EncodeString2SqlString_Insert(valStr)}', " :
                     $"'{EncodeString2SqlString_Insert(valStr)}'";

            }//foreach (string crrColumnName in _dbColumnNames)
            valueSql = $"({valueSql})";
            return valueSql;
        }// BuildInsertValueString

        private static string BuildUpdate_ValueString<T>(
            T obj,
            string[] _names,
            Dictionary<string, MemberInfo> colNameDir) where T : class
        {
            /////拼接 value 后面的  (,,,)
            string valueSql = string.Empty;
            for (int i = 0; i < _names.Length; i++)
            {
                string crrColumnName = _names[i];

                if (!colNameDir.ContainsKey(crrColumnName))
                    throw new Exception($"<{obj.GetType().Name}>obj is not contains a member who has CustomAttribute AttDbColumn.colNameDir = '{crrColumnName}'");

                object valueObj = null;
                MemberInfo mi = colNameDir[crrColumnName] as MemberInfo;
                switch (mi)
                {
                    case FieldInfo fieldInfo:
                        valueObj = fieldInfo.GetValue(obj); break;
                    case PropertyInfo propertyInfo:
                        valueObj = propertyInfo.GetValue(obj, null); break;
                    default:
                        throw new InvalidOperationException();
                }
                //object valueObj = ((PropertyInfo)colNameDir[crrColumnName]).GetValue(obj,null);
                string valStr = (valueObj == null) ? "NULL" : valueObj.ToString();

                valueSql += (i != _names.Length - 1) ?
                     $"`{AttDbColumn.FromMemberInfo(colNameDir[crrColumnName]).Name}` = '{EncodeString2SqlString_Insert(valStr)}', " :
                     $"`{AttDbColumn.FromMemberInfo(colNameDir[crrColumnName]).Name}` = '{EncodeString2SqlString_Insert(valStr)}'";

            }//foreach (string crrColumnName in _dbColumnNames)
            //valueSql = $"({valueSql})";
            return valueSql;
        }// BuildUpdateValueString

        private static string BuildUpdate_WhereString<T>(
            T obj,
            Dictionary<string, MemberInfo> colNameDir) where T : class
        {
            /////拼接 value 后面的  (,,,)
            string valueSql = string.Empty;
            int n第几个where条件 = 0;
            foreach (MemberInfo crrMi in colNameDir.Values)
            {
                AttDbColumn attCol = AttDbColumn.FromMemberInfo(crrMi);

                if (!(attCol.IsPrimaryKey || attCol.IsUpdateWhereColumn))
                {
                    continue;
                }

                object valueObj = null;
                //MemberInfo mi = attCol as MemberInfo;
                switch (crrMi)
                {
                    case FieldInfo fieldInfo:
                        valueObj = fieldInfo.GetValue(obj); break;
                    case PropertyInfo propertyInfo:
                        valueObj = propertyInfo.GetValue(obj, null); break;
                    default:
                        throw new InvalidOperationException();
                }
                //object valueObj = ((PropertyInfo)colNameDir[crrColumnName]).GetValue(obj,null);
                string valStr = (valueObj == null) ? "NULL" : valueObj.ToString();

                valueSql += (n第几个where条件 ==0) ?
                     $"`{attCol.Name}` = '{EncodeString2SqlString_Select(valStr)}' ":
                     $"` and {attCol.Name}` = '{EncodeString2SqlString_Select(valStr)}' ";

                n第几个where条件++;
            }//foreach (string crrColumnName in _dbColumnNames)
            //valueSql = $"({valueSql})";
            return valueSql;
        }// BuildUpdateValueString

        ///// <summary> 
        ///// 根据指定类型，从数据库中获取数据，存放到List<T>集合中 
        ///// </summary> 
        ///// <returns></returns> 
        //public List<T> MakeTablePackage_ByDbColumnNames<T>(
        //    MySqlDataReader _dtGet, string[] _names, bool ignorIfColumnNotMatch ) where T : class, new()
        //{
        //    try
        //    {
        //        List<T> _lstReturn = new List<T>();
        //        //_dtGet = SqlHelper.ExcueteDataTable(_sql, _cmdParams);


        //        //获得属性集合 
        //        T _tmpObj = new T();
        //        Type _type = _tmpObj.GetType();
        //        PropertyInfo[] _properties = _type.GetProperties();


        //        for (int i = 0; i < _dtGet.Rows.Count; i++)
        //        {
        //            T _item = new T();
        //            foreach (PropertyInfo _property in _properties)
        //            {
        //                object _value = _dtGet.Rows[i][_property.Name].ToString();
        //                _property.SetValue(_item,
        //                    ChangeType(_value, _property.PropertyType)
        //                    , null);
        //            }
        //            _lstReturn.Add(_item);
        //        }
        //        return _lstReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("打包数据出错-MakeTablePackage");
        //    }
        //}//MakeTablePackage

        public static object ChangeType(object value, Type conversionType)          //第一个参数加this,表示该ChangeType方法将为object的扩展方法
        {

            if (conversionType.IsGenericType &&
                    conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value != null)
                {
                    NullableConverter nullableConverter = new NullableConverter(conversionType);
                    conversionType = nullableConverter.UnderlyingType;
                }
                else
                {
                    return null;
                }
            }
            //return Convert.ChangeType(value, conversionType);
            if (conversionType.Equals(typeof(System.DateTime)) && (value.Equals("")))
            {
                return null;
            }

            if (conversionType.IsEnum)
            {
                //throw new Exception($"type={conversionType}, value={value}");
                return Enum.Parse(conversionType, value.ToString());
            }

            return Convert.ChangeType(value, conversionType);
        }//ChangeType

        public static void RunSqlFiles_MulitlineSQL(
            CSingleSqlExecuter _dbConn,
            List<string> _fileUrlList,
            Encoding _ecd,
            bool _ignorErr,
            out List<string> _resultMessageList)

        {
            _resultMessageList = new List<string>();

            foreach (string crrFile in _fileUrlList)
            {
                List<string> tpStrList;
                RunSqlFile_MulitlineSQL(
                    _dbConn,
                    crrFile,
                     _ecd,
                    _ignorErr,
                    out tpStrList);

                _resultMessageList.AddRange(tpStrList);
            }
        }//RunSqlFile_MulitlineSQL

        /// <summary>
        /// 执行一个Sql 文件。 文件中可以有多行。 每个SQL也可以拆分成多行。
        /// 程序一条一条sql地执行。
        /// 如果某行以--开头，则被认为是注释行，整行忽略。
        /// 暂不支持命令行中间或结尾出现--，或出现/* */
        /// 空白行将被自动跳过。
        /// 
        /// 注：由于需要进行各各种额外的语法检查，因此这个函数的运行效率会远低于RunSqlFile_SingleLineSQL
        /// </summary>
        /// <param name=""></param>
        public static void RunSqlFile_MulitlineSQL(
            CSingleSqlExecuter _dbConn,
            string _fileUrl,
            Encoding _ecd,
            bool _ignorErr,
            out List<string> _resultMessageList)

        {
            string[] lines = File.ReadAllLines(_fileUrl, _ecd);
            _resultMessageList = new List<string>();

            int i = 0;
            string crrSql = string.Empty;
            List<Tuple<int, string>> sqlList = new List<Tuple<int, string>>(); //List<Tuple<int 结束行号, string SQL>>

            //////循环每一行,从新拼合所有的多行SQL
            for (; i < lines.Length; i++)
            {
                string crrLine = lines[i].Trim();

                //跳过注释行
                if (crrLine.IndexOf("--") == 0 )
                {
                    //crrSql = string.Empty;

                    _resultMessageList.Add($"Massage\tComment line {i + 1} is skiped.\t{_fileUrl}");
                    continue;
                }

                //跳过空白行
                if (crrLine.Length == 0)
                {
                    _resultMessageList.Add($"Massage\tEmpty line {i + 1} is skiped.\t{_fileUrl}");
                    continue;
                }

                //如果crrLine内容有效，则正式合并
                crrSql += $" {crrLine} ";

                if (crrLine.Last() == ';')
                {
                    sqlList.Add(new Tuple<int, string>(i+1, crrSql));
                    crrSql = string.Empty;
                }

            }//循环每一行,从新拼合所有的多行SQL

            //////执行拼合以后的每一行SQL
            foreach(Tuple<int, string> crrLine in sqlList)
            {
                try
                {
                    _dbConn.ExecuteSqlStrAsVoid(crrLine.Item2);
                }
                catch (Exception ex)
                {
                
                    string tpStr = $"Error\n{_fileUrl}\nSql end at line {crrLine.Item1}\n{crrLine.Item2}\n{ex.Message}\n{crrLine.Item2}";
                    _resultMessageList.Add(tpStr);
                    if (!_ignorErr)
                        throw new Exception(tpStr);
                }//catch


            }//执行拼合以后的每一行SQL

        }//RunSqlFile_MulitlineSQL

        public static void RunSqlFiles_SingleLineSQL(
            CSingleSqlExecuter _dbConn,
            List<string> _fileUrlList,
            Encoding _ecd,
            bool _ignorErr,
            out List<string> _resultMessageList)
        {
            _resultMessageList = new List<string>();

            foreach (string crrFile in _fileUrlList)
            {
                List<string> tpStrList;
                RunSqlFile_SingleLineSQL(
                    _dbConn,
                    crrFile,
                     _ecd,
                    _ignorErr,
                    out tpStrList);

                _resultMessageList.AddRange(tpStrList);
            }
        }//RunSqlFile_SingleLineSQL


        /// <summary>
        /// 执行一个Sql 文件。 文件中可以有多行。 每个SQL一行。
        /// 程序一条一条sql地执行。
        /// 如果某行以--开头（前面不能有空格），则被认为是注释行，整行忽略。
        /// 暂不支持命令行中间或结尾出现--，或出现/* */
        /// 空白行将被自动跳过。
        /// </summary>
        /// <param name=""></param>
        /// <returns>过程中发生的任何信息和警告</returns>
        public static void RunSqlFile_SingleLineSQL(
            CSingleSqlExecuter _dbConn, 
            string _fileUrl, 
            Encoding _ecd, 
            bool _ignorErr,
            out List<string> _resultMessageList)
        {
            string[] lines = File.ReadAllLines(_fileUrl, _ecd);

            _resultMessageList = _dbConn.ExecuteSqlList(lines, _ignorErr,out _resultMessageList);

        }//RunSqlFile_SingleLineSQL

        public static string AttClass2CreateTable_WithoutNormalIndex(
            Type _objType,
            string _tableNamePrefix,
            string _tableNamePostfix,
            bool _needPrimaryKey)
        {
            return AttClass2CreateTable_WithoutNormalIndex(_objType,
                 _tableNamePrefix,
                 AttDbTable.GetAttDbTableName(_objType),
                 _tableNamePostfix,
                 _needPrimaryKey);

        }//

        /// <summary>
        /// 将一个 具备DB 自定义特征属性的 class定义转变为 数据库create table 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_obj">必须支持 AttDbTable 属性</param>
        /// <param name="_tableNamePostfix"> 表名的后缀。可以为null。最终的表名=AttrDbTable.name + _needPrimaryKey。 用于同一个表结构，创建多个不同名称的表 </param>
        /// <returns></returns>
        public static string AttClass2CreateTable_WithoutNormalIndex(
            Type _objType,
            string _tableNamePrefix,
            string _tableNameBody,
            string _tableNamePostfix,
            bool _needPrimaryKey)
        {
            //////获取表 特征
            AttDbTable tableDef = AttDbTable.GetAttDbTable(_objType);
            if (tableDef == null)
                throw new Exception($"Can't find [AttDbTable] on Type '{_objType.Name}'. Please check your class defination.");
            
            /////获取当前对象的全部成员定义
            List<Tuple<string, AttDbColumn,Type>> cols = new List<Tuple<string, AttDbColumn, Type>>();

            递归_将给定Type的AttDbColumn展开到结果列表(
                _objType,
                null,
                ref cols,
                false);//List<Tuple<string dbColumnName, AttDbColumn colInfo>>

            if (cols.Count == 0)
                throw new Exception($"Can't find [AttDbColumn] on Type '{_objType.Name}'. " +
                    $"Please check your class defination. Mybe you forgot to add 'public' to members, " +
                    $"or forgot to define any [AttDbColumn]");


            //循环每一个col定义，并合成sql语句
            string colSql = string.Empty;
            string primaryKeyStr = string.Empty;

            for (int i = 0; i < cols.Count; i++)
            {
                Tuple<string, AttDbColumn,Type> crrCol = cols[i];
                string colNewName = crrCol.Item1;
                AttDbColumn colAtt = crrCol.Item2;
                Type memberType = crrCol.Item3;

                //bool isCurrentColArrayOrList = false;

                if (colAtt.IsExpend2AnotherDbTable)
                {
                    throw new NotImplementedException(
                        $"custom attribute '{colNewName}'.IsExpend2AnotherDbTable=true ,but not supported");

                }
                else//if (!colAtt.IsExpend2AnotherDbTable)
                {
                    if (memberType.IsArray)
                    {
                        if (!colAtt.IsExpentArrayItemsInCurrentTable)
                            throw new NotImplementedException(
                                $"custom attribute '{colNewName}' is array. It.IsExpandSubattributesInCurrentTable=false && it.IsExpend2AnotherDbTable = false. These two must have one = true");

                        //isCurrentColArrayOrList = true;

                        //if is nullable value like double? aaa;
                        Type itemType = GetArrayElementType(memberType);
                        if (itemType.IsGenericType && itemType.IsValueType)
                            itemType = Nullable.GetUnderlyingType(itemType);

                        colSql += ArrayOrList2SqlStr(cols, i, colNewName, colAtt, itemType);
                    }
                    else if (memberType.IsGenericType)
                    {
                        //if is nullable value like double? aaa;
                        if (memberType.IsValueType)
                        {
                            string createColStr = colAtt.GetCreateColumnStr_singleColumn(Nullable.GetUnderlyingType(memberType), colNewName, true);
                            colSql += (i != cols.Count - 1) ? $"{createColStr},\t" : $"{createColStr}";
                        }
                        else //else is not nullable value(like double? aaa;)
                        {
                            if (!colAtt.IsExpentArrayItemsInCurrentTable)
                                throw new NotImplementedException(
                                    $"custom attribute '{colNewName}' is GenericType. It.IsExpandSubattributesInCurrentTable=false && it.IsExpend2AnotherDbTable = false. These two must have one = true");

                            //if is List<T>
                            if (memberType.GetGenericTypeDefinition()
                                   == typeof(List<>))
                            {
                                //isCurrentColArrayOrList = true;
                                Type itemType = memberType.GetGenericArguments()[0]; // use this...
                                colSql += ArrayOrList2SqlStr(cols, i, colNewName, colAtt, itemType);
                            }//if is List<T>
                            else////if is not List<T>
                            {
                                throw new NotImplementedException(
                                    $"custom attribute '{colNewName}' Is Generic Type,but not supported");
                            }
                        }//else //else is not nullable value(like double? aaa;)
                    }
                    else // default is value type
                    {
                        string createColStr = colAtt.GetCreateColumnStr_singleColumn(memberType, colNewName, true);
                        colSql += (i != cols.Count - 1) ? $"{createColStr},\t" : $"{createColStr}";
                    }
                }//if (colAtt.IsExpend2AnotherDbTable)

                if (colAtt.IsPrimaryKey)
                    primaryKeyStr = $",PRIMARY KEY (`{colNewName}`)";
            }//for (int i = 0; i < cols.Count; i++)

            string tableSql = $"CREATE TABLE `{_tableNamePrefix}{_tableNameBody}{_tableNamePostfix}` ({colSql}\t{primaryKeyStr}) \tCOLLATE = '{tableDef.TextEncoding}' \t ENGINE = {tableDef.EngineType}";

            return tableSql;

        }//AttClass2CreateTable

        private static string ArrayOrList2SqlStr(List<Tuple<string, AttDbColumn, Type>> cols,  int i, string colNewName, AttDbColumn colAtt, Type memberType)
        {
            if (colAtt.MaxLength < 1)
                throw new Exception($"custom attribute '{colNewName}' is an array,but MaxLength'{colAtt.MaxLength}' is not set.please check your code.");


            List<string> createColList = colAtt.GetCreateColumnStr_ArrayColumns(
                memberType,
                //colAtt.MaxLength,
                //colAtt.ArrayItemMaxLength,
                colNewName);

            if (createColList.Count == 0)
                throw new Exception($"custom attribute '{colNewName}' is an array,but GetCreateColumnStr_ArrayColumns() return empty.");

            string colSql = string.Empty;
            for (int j = 0; j < createColList.Count; j++)
            {
                string createColStr = createColList[j];//colAtt.GetCreateColumnStr_singleColumn(memberType, colNewName+i, false);
                colSql += (i == (cols.Count - 1) && j == (colAtt.MaxLength - 1))
                    ? $"{createColStr}"
                    : $"{createColStr},\t";

            }

            return colSql;
        }

        public static Type GetArrayElementType( Type t)
            {
                if (!t.IsArray) return null;

                string tName = t.FullName.Replace("[]", string.Empty);

                Type elType = t.Assembly.GetType(tName);

                return elType;
            }

        /// <summary>
        /// 将一个 具备DB 自定义特征属性的 class定义转变为 数据库create index 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_obj"></param>
        /// <returns></returns>
        public static string AttClass2CreateIndexes(Type _objType, string _dbAndTableName,bool _includePrimaryKey)
        {
            /////获取当前对象的全部成员定义
            List<Tuple<string, AttDbColumn,Type>> cols = new List<Tuple<string, AttDbColumn,Type>>();
                
            递归_将给定Type的AttDbColumn展开到结果列表(
                _objType,
                null,
                ref cols,
                false);//List<Tuple<string dbColumnName, AttDbColumn colInfo>>

            //确定所有需要添加index的 col名称
            List<string> idxDbCols = new List<string>();
            string primaryKeyColName = null;

            for(int i=0; i< cols.Count; i++)
            {
                Tuple<string, AttDbColumn,Type> crrCol = cols[i];
                string colName = crrCol.Item1;
                AttDbColumn colAtt = crrCol.Item2;
                Type colCodeType = crrCol.Item3;

                //单独处理主键
                if (colAtt.IsPrimaryKey)
                {
                    if (!_includePrimaryKey)
                        continue;
                    else
                    {
                        primaryKeyColName = colName;
                        continue;
                    }
                }

                if (!colAtt.HasDefaultIndex)
                    continue;

                idxDbCols.Add(colName);
            }//for(int i=0; i< cols.Count; i++) 

            //拼合index 字符串
            //循环每一个col定义，并合成sql语句
            string rstSql = $"ALTER TABLE {_dbAndTableName}\t";

            //ALTER TABLE `node_event_instant_single` ADD PRIMARY KEY(`node_id`);
            if (primaryKeyColName != null)
            {
                rstSql += $" ADD PRIMARY KEY (`{primaryKeyColName}`) ";

                if (idxDbCols.Count == 0)
                    rstSql += " ;";
                else
                    rstSql += " ,";

            }

            for (int i = 0; i < idxDbCols.Count; i++)
            {
                string colName = idxDbCols[i];
                string tpStr = $" ADD INDEX `idx_{colName}` (`{colName}`)";
                rstSql += (i != idxDbCols.Count - 1) ? $"{tpStr}, " : $"{tpStr};";
            }//for (int i = 0; i < idxDbCols.Count; i++)

            return rstSql;
        }//AttClass2CreateTable




        private static void 递归_将给定Type的AttDbColumn展开到结果列表(
            Type _objType, 
            string _expendMemberName,
            ref List<Tuple<string, AttDbColumn, Type>> _rstList,
            bool isForbidenIndexesOfSubItems)
        {
            MemberInfo[] mbs = _objType.GetMembers();

            ////整理cols定义列表
            //循环当前对象的全部成员定义
            foreach (MemberInfo crrMember in mbs)
            {
                object[] memberAttList = crrMember.GetCustomAttributes(true);

                //循环当前类成员的每一个 自定义特征，找出并解释AttDbColumn的特征
                foreach (object crrMemberAtt in memberAttList)
                {
                    AttDbColumn crrAtt = crrMemberAtt as AttDbColumn;

                    if (crrAtt == null)
                        continue;

                    //如果上层设置了禁止为下级对象创建索引，那么就在内存中强制crrAtt.HasDefaultIndex = false
                    if (isForbidenIndexesOfSubItems)
                        crrAtt.HasDefaultIndex = false;

                    Type crrType = GetUnderlyingType(crrMember);
                    //如果当前属性需要平行展开，则展开；否则直接添加
                    if (!crrAtt.IsExpandSubattributesInCurrentTable)
                        _rstList.Add(
                            new Tuple<string, AttDbColumn, Type>(
                            (_expendMemberName==null) ? crrAtt.Name : _expendMemberName + crrAtt.Name, 
                            crrAtt,
                            crrType));
                    else
                    {
                        递归_将给定Type的AttDbColumn展开到结果列表(
                            crrType,//GetUnderlyingType(crrMember),
                            (_expendMemberName == null) ? crrAtt.Name : _expendMemberName + crrAtt.Name, //crrAtt.Name,
                            ref _rstList,
                            isForbidenIndexesOfSubItems || crrAtt.IsForbidenIndexesOfSubItems);

                    }//else

                    break;
                }//foreach 循环当前类成员的每一个 自定义特征，找出并解释AttDbColumn的特征

            }//foreach 循环当前对象的全部成员定义

        }//递归_将给定Type的AttDbColumn展开到结果列表

        //public static Type GetUnderlyingType(this MemberInfo member)
        //{
        //    switch (member.MemberType)
        //    {
        //        case MemberTypes.Event:
        //            return ((EventInfo)member).EventHandlerType;
        //        case MemberTypes.Field:
        //            return ((FieldInfo)member).FieldType;
        //        case MemberTypes.Method:
        //            return ((MethodInfo)member).ReturnType;
        //        case MemberTypes.Property:
        //            return ((PropertyInfo)member).PropertyType;
        //        default:
        //            throw new ArgumentException
        //            (
        //             "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
        //            );
        //    }
        //}

        public static Type GetUnderlyingType( MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                     "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }//GetUnderlyingType( MemberInfo member)

    }//class CSqlHelper
}//namespace
