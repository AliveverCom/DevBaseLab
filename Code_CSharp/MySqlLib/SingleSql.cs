///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2013-05-12</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog></ChangeLog>
///  </ChangeHistory>
///</FileHistory>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Alivever.com.MySqlLib
{
    public class CSingleSqlExecuter : ICloneable
    {
        #region class members

        /// <summary>
        /// 数据库连接字符串。
        /// 如果DbConnStr不等于空，则不论CloseDbConnWhenExecuted是否为True，都关闭连接。
        /// </summary>
        protected string DbConnStr = string.Empty;

        /// <summary>
        /// 数据库连接。可以由外界通过构造函数传入，也可以根据DbConnStr在执行前自动生成。
        /// 如果对象是从外界传入的，则执行结束时程序会自动判断CloseDbConnWhenExecuted决定是否关闭连接。
        /// </summary>
        protected MySqlConnection DbConn = null;

        /// <summary>
        /// 执行完SQL语句后是否自动关闭数据库连接。
        /// 如果DbConnStr不等于空，则不论CloseDbConnWhenExecuted是否为True，都关闭连接。
        /// </summary>
        public bool CloseDbConnWhenExecuted = false;

        /// <summary>
        /// 是否把执行过程中跳出的异常也看作是一种可以接受的结果。
        /// True = 把异常当作字符串进行正常返回。
        /// False = 把结果当作异常继续向外抛出。
        /// </summary>
        public bool SeeExcutingExecptionAsNomalResult = false;

        /// <summary>
        /// SQL连接或执行时的最大超时，以秒为单位。小于零表示使用系统默认值。
        /// 这个值只有在需要执行很大操作的时候才需要更改。更改后立即生效，不需要重新连接。
        /// </summary>
        public int SqlTimeOutSec = -1;

        /// <summary>
        /// 当通过多线程执行相同任务的时候，会发生多个线程同时尝试创建同一个表，导致异常。
        /// 这个变量将用与CreateTable 时自动锁定，以避免这个现象发生。以及避免外部重复实现该功能。
        /// </summary>
        protected static readonly object Locker_CreateTable = new object();

        #endregion//class members

        #region Constructor

        public CSingleSqlExecuter(MySqlConnection _DbConn)
        {
            this.DbConn = _DbConn;
        }

        public CSingleSqlExecuter(string _DbConnStr)
        {
            this.DbConnStr = _DbConnStr;
            this.CloseDbConnWhenExecuted = true;
        }


        #endregion //Constructor

        public object Clone()
        {
            CSingleSqlExecuter obj = new CSingleSqlExecuter(this.DbConnStr)
            {
                SeeExcutingExecptionAsNomalResult = this.SeeExcutingExecptionAsNomalResult,
                SqlTimeOutSec = this.SqlTimeOutSec,
                CloseDbConnWhenExecuted = this.CloseDbConnWhenExecuted

            };

            return obj;
        }

        /// <summary>
        /// 关闭数据库连接。以便调用者在 多次执行sql之后可以及时关闭数据库连接，释放资源。
        /// 注：当this.CloseDbConnWhenExecuted == true或 this.DbConnStr.Length != 0时才关闭连接。
        /// 如果 this.CloseDbConnWhenExecuted == false 则实际上不执行关闭连接动作。
        /// </summary>
        protected void CloseDbConnByOptions()
        {
            if (this.DbConn == null)
                return;
            //if (this.DbConn == null)
            //    throw new Exception("DbConn Object is null. Can't close it.");

            if (this.CloseDbConnWhenExecuted == true
                && this.DbConnStr.Length != 0)
            //if (this.DbConnStr.Length != 0)
            {
                this.DbConn.Close();

                this.DbConn = null;
            }
        }//CloseDbConn()

        /// <summary>
        /// 关闭数据库连接。以便调用者在 多次执行sql之后可以及时关闭数据库连接，释放资源。
        /// 注：当this.CloseDbConnWhenExecuted == true或 this.DbConnStr.Length != 0时才关闭连接。
        /// 如果 this.CloseDbConnWhenExecuted == false 则实际上不执行关闭连接动作。
        /// </summary>
        public void CloseDbConnNow()
        {
            if (this.DbConn == null)
                return;
            //if (this.DbConn == null)
            //    throw new Exception("DbConn Object is null. Can't close it.");

            //if (this.CloseDbConnWhenExecuted == true
            //    && this.DbConnStr.Length != 0)
            if (this.DbConnStr.Length != 0)
            {
                this.DbConn.Close();

                this.DbConn = null;
            }
        }//CloseDbConn()

        public static void 把结果写入文件(List<List<string>> rstList, string fileName)
        {
            //string str = string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (List<string> crrRow in rstList)
            {
                foreach (string crrColumn in crrRow)
                {
                    //str += crrColumn + "\t";
                    sb.Append(crrColumn + "\t");
                }
                //str += "\r\n";
                sb.Append("\n");
            }


            File.AppendAllText(fileName, sb.ToString(), Encoding.GetEncoding("GB2312"));

        }//把结果写入文件

        /// <summary>
        /// 执行SQL，并将结果连接的Reader直接对外返回。
        /// 注意：这个函数是为外界高级操作所准备的。调用后需要自己手动关闭Reader和链接。
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        protected MySqlDataReader ExecuteSqlStrAsReader(string crrSQL)
        {
            //执行SQL
            if (this.DbConn == null)
                this.DbConn = GenerateDbConnection();

            MySqlCommand mySqlCommand = new MySqlCommand(crrSQL, this.DbConn);
            MySqlDataReader reader = null;

            if (this.SqlTimeOutSec >= 0)
                mySqlCommand.CommandTimeout = this.SqlTimeOutSec;


            //List<List<string>> rstList = new List<List<string>>();

            try
            {
                reader = mySqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("执行SQL失败:" + ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                
                this.CloseDbConnByOptions();

                throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);
            }

            CloseDbConnByOptions();
            return reader;
        }//执行SQL并填充返回结果() 

        /// <summary>
        /// Sequence execution.
        /// </summary>
        /// <param name="_sqlList"></param>
        public void ExecuteSqlStrAsVoid_Sequence(List<string> _sqlList)
        {
            foreach (string crrSql in _sqlList)
                this.ExecuteSqlStrAsVoid(crrSql);
        }//ExecuteSqlStrAsVoid(List<string> _sqlList)

        /// <summary>
        /// Parallel execution. Max sql count = 100;
        /// </summary>
        /// <param name="_sqlList"></param>
        public void ExecuteSqlStrAsVoid_Parallel(List<string> _sqlList)
        {
            Parallel.ForEach(_sqlList,
                new ParallelOptions() { MaxDegreeOfParallelism = _sqlList.Count },
                crrSql =>
                {
                    CSingleSqlExecuter crrDbConn = this.Clone() as CSingleSqlExecuter;
                    crrDbConn.ExecuteSqlStrAsVoid(crrSql);
                    crrDbConn.CloseDbConnNow();
                });

        }//ExecuteSqlStrAsVoid(List<string> _sqlList)


        /// <summary>
        /// 执行SQL，不返回任何结果。
        /// 注意：通常用于执行写操作insert, update等不需要有返回值的命令。
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        public void ExecuteSqlStrAsVoid(string crrSQL)
        {
            //执行SQL
            if (this.DbConn == null)
                this.DbConn = GenerateDbConnection();

            MySqlCommand mySqlCommand = new MySqlCommand(crrSQL, this.DbConn);

            if (this.SqlTimeOutSec >=0)
                mySqlCommand.CommandTimeout = this.SqlTimeOutSec;
            //MySqlDataReader reader = null;

            //List<List<string>> rstList = new List<List<string>>();
            //MySqlDataReader reader;

            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                this.CloseDbConnByOptions();

                throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);
            }

            //if (reader != null)
            //    reader.Close();

            CloseDbConnByOptions();

        }//执行SQL并填充返回结果() 

        /// <summary>
        /// 把结果变成字符串列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected static List<string> ResultToStringList(MySqlDataReader reader)
        {
            List<string> columns = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.IsDBNull(i))
                    columns.Add(null);//columns.Add("");
                else
                    columns.Add(reader.GetString(i));
               
            }
            return columns;
        }//把结果变成字符串

        /// <summary>
        /// 生成一个打开的数据库连接
        /// </summary>
        /// <returns></returns>
        protected MySqlConnection GenerateDbConnection()
        {

            //String mysqlStr = "Database=PM;Data Source=db-testing-ecom6304.db01.baidu.com;User Id=beidou;Password=beidou;pooling=false;CharSet=utf8;port=8414";
            // String mySqlCon = ConfigurationManager.ConnectionStrings["MySqlCon"].ConnectionString;
            MySqlConnection mysql = new MySqlConnection(this.DbConnStr);
            mysql.Open();

            return mysql;
        }//GenerateDbConnection()

        /// <summary>
        /// 执行SQL并将结果填充到一个矩阵中
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        public List<List<string>> ExecuteSqlStrAsMatrix(string crrSQL)
        {
            MySqlDataReader reader = null;  

            List<List<string>> rstList = new List<List<string>>();

            try
            {
                reader = this.ExecuteSqlStrAsReader(crrSQL);
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        //string rst = reader.GetString(0);
                        rstList.Add(ResultToStringList(reader));
                    }
                }//where
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                if (reader != null)
                    reader.Close();

                this.CloseDbConnByOptions();
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("执行SQL失败:" + ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                this.CloseDbConnByOptions();

                if (this.SeeExcutingExecptionAsNomalResult)
                {
                    List<string> strList = new List<string>();
                    strList.Add(ex.Message);
                    rstList.Add(strList);
                }
                else
                    throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);

            }//catch

            if (reader != null )
                reader.Close();

            this.CloseDbConnByOptions();

            return rstList;
        }//执行SQL并填充返回结果() 

        

        /// <summary>
        /// 将数据查询结果中的一行转变为对象. 仅转换_dbColumnNames中指定的属性，而不是全部对象属性赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        protected T ResultToObj<T>(
            MySqlDataReader reader,
            List<CExpandedAttDbColumn> attTree,
            Dictionary<int, Tuple<CExpandedAttDbColumn, List<CExpandedAttDbColumn>>> i2AttDbCol_Dict) 
            where T : class, new()
        {
            if (attTree == null || attTree.Count == 0)
                throw new Exception("ResultToObj<T>()的参数List<CExpandedAttDbColumn> attTree为空。你可能没有定义T的AttDbColumn ");

            CExpandedAttDbColumn_TreeIterator treeIterator = new CExpandedAttDbColumn_TreeIterator(attTree);

            T objLv1 = new T();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                object ObjLvlast = objLv1;
                List<CExpandedAttDbColumn> parents;
                CExpandedAttDbColumn attDbCol;

                //如果缓存里还没有相关结果，就调用一次并记住
                if (!i2AttDbCol_Dict.ContainsKey(i))
                {
                    attDbCol = treeIterator.GetNext(out parents);

                    if (attDbCol == null)
                        throw new Exception($"ResultToObj<T>() 没有找到属性attTree[{i}]对应的列定义");

                    i2AttDbCol_Dict.Add(i, new Tuple<CExpandedAttDbColumn, List<CExpandedAttDbColumn>>(attDbCol, parents));
                }
                else//否则如果缓存中有结果，那就直接使用
                {
                    attDbCol = i2AttDbCol_Dict[i].Item1;
                    parents = i2AttDbCol_Dict[i].Item2;
                }


                //首先创建目标节点迭代路径上的 父辈们
                if (parents != null && parents.Count != 0)
                {
                    try
                    {
                        foreach (CExpandedAttDbColumn crrLvCol in parents)
                        {
                            object crrObj = crrLvCol.GetValueFromObj(ObjLvlast);

                            if (crrObj == null)
                            {
                                crrObj = Activator.CreateInstance(crrLvCol.MemberType);
                                crrLvCol.SetValueForObj(ObjLvlast, crrObj);
                            }
                            ObjLvlast = crrObj;
                        }//foreach(CExpandedAttDbColumn crrLvCol in _parents)
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"ResultToObj<T> 试图创建深度节点中的对象时出错：{ex.Message}");
                    }

                }//if (_parents !=null && _parents.Count !=0)

                try
                {
                    object dbValue = reader.GetValue(i);
                    attDbCol.SetValueForObj(ObjLvlast, dbValue);
                }
                catch (Exception ex)
                {
                    throw new Exception($"ResultToObj<T> 向内存对象赋值时出错：{ex.Message}");
                }

            }//for (int i = 0; i < reader.FieldCount; i++)

            return objLv1;


        }//ResultToObj()


        /// <summary>
        /// 将数据查询结果中的一行转变为对象. 仅转换_dbColumnNames中指定的属性，而不是全部对象属性赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        protected T ResultToObj<T>(
            MySqlDataReader reader,
            string[] _dbColumnNames,
            Dictionary<string, MemberInfo> attMap
            ) where T : class, new()
        {
            if (attMap == null || attMap.Count == 0)
                throw new Exception("ResultToObj<T>()的参数Dictionary<string, MemberInfo> attMap为空。你可能没有定义T的AttDbColumn ");

            T obj = new T();

            for (int i = 0; i < reader.FieldCount; i++)
            //Parallel.For(0, 
            //    reader.FieldCount, 
            //    new ParallelOptions() {  MaxDegreeOfParallelism = Environment.ProcessorCount},
            //    i=>
            {
                //if (reader.IsDBNull(i))
                //    return null;//columns.Add("");

                /////为每一个变量赋值

                //找到当前
                //foreach (string crrColName in _dbColumnNames)
                //{
                //    if (crrColName != reader.GetName(i))
                //        continue;

                //object valueObj = null;
                object dbValue = reader.GetValue(i);

                try
                {
                    switch (attMap[_dbColumnNames[i]])//switch (attMap[crrColName])
                    {
                        case FieldInfo fieldInfo:
                            fieldInfo.SetValue(
                                obj,
                                //CSqlHelper.ChangeType(reader.GetString(reader.GetName(i)), fieldInfo.FieldType)); 
                                //CSqlHelper.ChangeType(reader.GetString(i), fieldInfo.FieldType)); break;
                                CSqlHelper.ChangeType(dbValue is DBNull ? null : dbValue, fieldInfo.FieldType)); 
                            //reader.GetValue(i));
                            break;
                        case PropertyInfo propertyInfo:
                            propertyInfo.SetValue(
                                obj,
                                //CSqlHelper.ChangeType(reader.GetString(reader.GetName(i)), propertyInfo.PropertyType),
                                CSqlHelper.ChangeType(dbValue is DBNull ? null : dbValue, propertyInfo.PropertyType),
                                //reader.GetValue(i),
                                null);
                            break;
                        default:
                            throw new InvalidOperationException($"attMap[{_dbColumnNames[i]}] not found");

                    }
                    //break;
                }
                catch (Exception ex)
                {
                    string str = $"{ex.Message}, 列名称序号={i}, 列名称={_dbColumnNames[i]}";
                    throw new Exception(str);
                }

                //obj.Add(reader.GetString(i));
                //}//foreach (string crrColName in _dbColumnNames)

            };
            return obj;


        }//ResultToObj()





        /// <summary>
        /// 执行SQL并将结果填充到一个矩阵中
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        public List<T> LoadFromDb<T>(string crrSQL, List<CExpandedAttDbColumn> attTree) where T : class, new()
        {
            MySqlDataReader reader = null;

            List<T> rstList = new List<T>();

            Dictionary<string, MemberInfo> attMap = CSqlHelper.生成对应属性映射表(new T());

            try
            {
                reader = this.ExecuteSqlStrAsReader(crrSQL);
                int nRow = 0;
                Dictionary<int, Tuple<CExpandedAttDbColumn, List<CExpandedAttDbColumn>>> i2AttDbCol_Dict = new Dictionary<int, Tuple<CExpandedAttDbColumn, List<CExpandedAttDbColumn>>>();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        //string rst = reader.GetString(0);
                        //rstList.Add(ResultToObj<T>(reader, attTree, attMap));
                        T newObj = ResultToObj<T>(reader, attTree, i2AttDbCol_Dict);
                        rstList.Add(newObj);
                    }

                    nRow++;

                }//where
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                if (reader != null)
                    reader.Close();

                this.CloseDbConnByOptions();
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("执行SQL失败:" + ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                this.CloseDbConnByOptions();

                if (this.SeeExcutingExecptionAsNomalResult)
                {
                    List<string> strList = new List<string>();
                    strList.Add(ex.Message);
                    //rstList.Add(strList);
                }
                else
                    throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);

            }//catch

            if (reader != null)
                reader.Close();

            this.CloseDbConnByOptions();

            return rstList;
        }//执行SQL并填充返回结果() 

        /// <summary>
        /// 执行SQL并将结果填充到一个矩阵中
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        public List<T> LoadFromDb<T>(string crrSQL, string[] _dbColumnNames) where T : class, new()
        {
            MySqlDataReader reader = null;

            List<T> rstList = new List<T>();

            Dictionary<string, MemberInfo> attMap = CSqlHelper.生成对应属性映射表(new T());

            try
            {
                reader = this.ExecuteSqlStrAsReader(crrSQL);
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        //string rst = reader.GetString(0);
                        rstList.Add(ResultToObj<T>(reader, _dbColumnNames, attMap));
                    }
                }//where
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                if (reader != null)
                    reader.Close();

                this.CloseDbConnByOptions();
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("执行SQL失败:" + ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                this.CloseDbConnByOptions();

                if (this.SeeExcutingExecptionAsNomalResult)
                {
                    List<string> strList = new List<string>();
                    strList.Add(ex.Message);
                    //rstList.Add(strList);
                }
                else
                    throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);

            }//catch

            if (reader != null)
                reader.Close();

            this.CloseDbConnByOptions();

            return rstList;
        }//执行SQL并填充返回结果() 


        /// <summary>
        /// 执行SQL并将结果填充到一个矩阵中
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        public List<string> ExecuteSqlStrAsOneRow(string crrSQL)
        {
            MySqlDataReader reader = null;

            List<string> rstList = null;//= new List<string>();

            try
            {
                reader = this.ExecuteSqlStrAsReader(crrSQL);
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        //string rstStr;
                        //if (reader.IsDBNull(0))
                        //    rstStr = null;
                        //else
                        //    rstStr = reader.GetString(0);

                        ////string rst = reader.GetString(0);
                        //rstList.Add(rstStr);

                        rstList = ResultToStringList(reader);
                        break;
                    }
                }//where
            }
            catch (System.Data.SqlTypes.SqlNullValueException   ex)
            {
                if (reader != null)
                    reader.Close();

                this.CloseDbConnByOptions();
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("执行SQL失败:" + ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                this.CloseDbConnByOptions();

                if (this.SeeExcutingExecptionAsNomalResult)
                {
                    //List<string> strList = new List<string>();
                    rstList.Add(ex.Message);
                    //rstList.Add(strList);
                }
                else
                    throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);

            }//catch

            if (reader != null)
                reader.Close();

            this.CloseDbConnByOptions();

            return rstList;
        }//ExecuteSqlStrAsList() 

        /// <summary>
        /// 执行SQL并将结果填充到一个矩阵中
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        public List<string> ExecuteSqlStrAsList(string crrSQL)
        {
            MySqlDataReader reader = null;

            List<string> rstList = new List<string>();

            try
            {
                reader = this.ExecuteSqlStrAsReader(crrSQL);
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        string rstStr;
                        if (reader.IsDBNull(0))
                            rstStr = null;
                        else
                            rstStr = reader.GetString(0);

                        //string rst = reader.GetString(0);
                        rstList.Add(rstStr);

                    }
                }//where
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                if (reader != null)
                    reader.Close();

                this.CloseDbConnByOptions();
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("执行SQL失败:" + ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                this.CloseDbConnByOptions();

                if (this.SeeExcutingExecptionAsNomalResult)
                {
                    //List<string> strList = new List<string>();
                    rstList.Add(ex.Message);
                    //rstList.Add(strList);
                }
                else
                    throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);

            }//catch

            if (reader != null)
                reader.Close();

            this.CloseDbConnByOptions();

            return rstList;
        }//ExecuteSqlStrAsList() 


        /// <summary>
        /// 执行SQL ，并返回第一个结果。
        /// 本功能通常仅用于预先知道SQL执行后只有一个结果的情况。
        /// </summary>
        /// <param name="crrSQL"></param>
        /// <returns></returns>
        public string ExecuteSqlStrAsString(string crrSQL)
        {
            MySqlDataReader reader = null;

            //List<List<string>> rstList = new List<List<string>>();
            string rstStr = null;

            try
            {
                reader = this.ExecuteSqlStrAsReader(crrSQL);
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        if (reader.IsDBNull(0))
                            rstStr = null;
                        else
                            rstStr = reader.GetString(0);

                        break;
                    }
                }//where
            }
            catch (System.Data.SqlTypes.SqlNullValueException ex)
            {
                if (reader != null)
                    reader.Close();

                this.CloseDbConnByOptions();
                return null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("执行SQL失败:" + ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                this.CloseDbConnByOptions();

                if (this.SeeExcutingExecptionAsNomalResult)
                {
                    rstStr = ex.Message;
                }
                else
                    throw new Exception(ex.Message + "; sqlStr=" + crrSQL, ex);

            }//catch

            reader.Close();
            this.CloseDbConnByOptions();

            return rstStr;
        }//执行SQL并填充返回结果() 

        //public  CSingleSqlExecuter Clone()
        //{
        //    CSingleSqlExecuter newObj = new CSingleSqlExecuter(this.DbConnStr);
        //    newObj.CloseDbConnWhenExecuted = this.CloseDbConnWhenExecuted;

        //    newObj.SeeExcutingExecptionAsNomalResult = this.SeeExcutingExecptionAsNomalResult;

        //    newObj.SqlTimeOutSec = this.SqlTimeOutSec;

        //    return newObj;
        //}//Clone()

        /// <summary>
        /// 判断某个库表是否存在
        /// </summary>
        /// <param name="_dbName"></param>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public bool IsDbTableExist(string _dbName, string _tableName)
        {
            string existSql = string.Format("select 1 from information_schema.`TABLES` where  TABLE_SCHEMA = '{0}' and TABLE_NAME = '{1}'",
                    _dbName, _tableName);
            string existStr = this.ExecuteSqlStrAsString(existSql);

            if (existStr == null || existStr.Length == 0 || existStr != "1")
                return false;
            else
                return true;

        }//IsDbTableExist

        public List<string> ExecuteSqlList( 
            IEnumerable<string> lines, 
            bool _ignorErr,
            out List<string> _resultMessageList)
        {
            //循环每一行
            _resultMessageList = new List<string>();
            for (int i = 0; i < lines.Count(); i++)
            {
                string crrLine = lines.ElementAt(i);
                if (crrLine.IndexOf("--") == 0 || crrLine.Length < 3)
                {
                    _resultMessageList.Add($"Massage\tComment Line {i + 1} is skiped.");
                    continue;
                }

                try
                {
                    this.ExecuteSqlStrAsVoid(crrLine);
                }
                catch (Exception ex)
                {
                    string tpStr = $"Error\tLine {i + 1} error.\n{ex.Message}";
                    _resultMessageList.Add(tpStr);
                    if (!_ignorErr)
                        throw new Exception(tpStr);
                }//catch
            }//for(; i< lines.Length; i++)

            return _resultMessageList;
        }//ExecuteSqlList

        public void UpdateSql_ByClassMemberNames<T>(
            T obj,
            string _dbTableName,
            string[] _classMemberNames) where T : class
        {
            string sql = CSqlHelper.BuildUpdateSql_ByClassMemberNames(obj, _dbTableName, _classMemberNames);
            this.ExecuteSqlStrAsVoid(sql);
        }//UpdateSql_ByClassMemberNames<T>

        //public void Insert2Db<T>(
        //        List<T> objList,
        //        string _dbTableName,
        //        string[] _classMemberNames,
        //        Int16 _itemsInOneSql,
        //        bool _ignorErr,
        //        out List<string> _resultMessageList) where T : class
        //{
        //    Insert2Db<T>(
        //         objList,
        //        _dbTableName,
        //         _classMemberNames,
        //        _itemsInOneSql,
        //        _ignorErr,
        //        EWhenExist.ThrowException,
        //        out _resultMessageList);
        //}

        public void Insert2Db<T>(
            List<T> objList,
            string _dbTableName,
            string[] _classMemberNames,
            Int16 _itemsInOneSql,
            bool _ignorErr,
            out List<string> _resultMessageList) where T : class
    {
            Insert2Db(objList, _dbTableName, _classMemberNames, _itemsInOneSql, _ignorErr,out _resultMessageList,
              Environment.ProcessorCount == 1 ? 1 : (Environment.ProcessorCount > 4 ? Environment.ProcessorCount - 2 : Environment.ProcessorCount / 2)
              );
    }

        /// <summary>
        /// 将一个对象序列直接写入数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        /// <param name="_dbTableName"></param>
        /// <param name="_classMemberNames">可以为空。 null表示全部att属性</param>
        /// <param name="_itemsInOneSql"></param>
        public void Insert2Db<T>( 
            List<T> objList,
            string _dbTableName,
            string[] _classMemberNames,
            Int16 _itemsInOneSql,
            bool _ignorErr,
            out List<string> _resultMessageList,
            int _maxThread) where T :class
        {
            //List<string> sqls = CSqlHelper.BuildInsertStringList_ByClassMemberNames<T>(
            //    objList, _dbTableName, _classMemberNames, _itemsInOneSql);

            ////执行插入
            //this.ExecuteSqlList(sqls, _ignorErr, out _resultMessageList);

            //sqls.Clear();

            CSqlHelper.InsertIntoDb<T>(objList, _dbTableName, _classMemberNames, _itemsInOneSql, out _resultMessageList, 
                this,_maxThread);
            //_resultMessageList = null;
        }//Insert2Db

        public void Insert2Db<T>(
            List<T> objList, string _dbTableName) where T : class
        {
            List<string> msgList;
            Insert2Db(objList, _dbTableName, null, 1000, false, out msgList);
        }// Insert2Db<T>

        public void Insert2Db<T>(
            List<T> objList, string _dbTableName, int _maxThread ) where T : class
        {
            List<string> msgList;
            Insert2Db(objList, _dbTableName, null, 1000, false, out msgList, _maxThread);
        }// Insert2Db<T>

        public void Insert2Db<T>(
            T _obj, string _dbTableName) where T : class
        {
            List<string> msgList;

            List<T> objs = new List<T>();
            objs.Add(_obj);

            Insert2Db(objs, _dbTableName, null, 1000, false, out msgList);
        }// Insert2Db<T>


        /// <summary>
        /// 清空某个表中的全部数据
        /// </summary>
        /// <param name="_tableName"></param>
        public void ClearTable(string _tableName)
        {
            string sql = $"TRUNCATE `{_tableName}`";
            this.ExecuteSqlStrAsVoid(sql);

        }//ClearTable(string _tableName)

        /// <summary>
        /// 从数据库中直接读取完整的对象列表.
        /// 默认使用该对象的客户自定义属性中的 表名和列名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> LoadFromDb<T>() where T : class, new()
        {
            return LoadFromDb<T>(null, (string)null);
        }

            /// <summary>
            /// 从数据库中直接读取完整的对象列表.
            /// 默认使用该对象的客户自定义属性中的 表名和列名
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
         public List<T> LoadFromDb<T>(string _sqlWhere, string _sqlOrderBy) where T:class, new()
        {
            //////拼合 select 字符串
            List<CExpandedAttDbColumn> attTree = new List<CExpandedAttDbColumn>();
            CExpandedAttDbColumn.递归_将给定Type的AttDbColumn展开到结果_树型结构(typeof(T), null, ref attTree,false);

            List<CExpandedAttDbColumn> attList = null;
            CExpandedAttDbColumn.Tree2List(attTree, ref attList);

            List<string> hdList = new List<string>();
            string hdStr = string.Empty;
            for(int i =0; i < attList.Count; i++ )
            {
                CExpandedAttDbColumn crrAtt = attList[i];
                hdList.Add( crrAtt.DbColumnName);

                if (i + 1 >= attList.Count)
                    hdStr += $"`{crrAtt.DbColumnName}`" ;
                else
                    hdStr += $"`{crrAtt.DbColumnName}`, ";
            }

            string sql = $"SELECT {hdStr} from {AttDbTable.GetAttDbTableName(typeof(T))} " 
                + (_sqlWhere==null|| _sqlWhere.Trim().Length == 0 ? null : $" where {_sqlWhere} ")
                + (_sqlOrderBy == null || _sqlOrderBy.Trim().Length == 0 ? null : $" order by {_sqlOrderBy} ");
            //return this.LoadFromDb<T>(sql, hdList.ToArray());
            return this.LoadFromDb<T>(sql, attTree);
        }//LoadFromDb<T>() where T:class

        /// <summary>
        /// 从数据库中直接读取完整的对象列表.
        /// 默认使用该对象的客户自定义属性中的 表名和列名。
        /// 如果from中对表起了别名，则可以设置 _alias，以便在拼合select 的时候每列前面加上别名前缀修饰
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> LoadFromDb<T>(string _sqlAllAfterSelect ,char? _alias) where T : class, new()
        {
            //////拼合 select 字符串
            List<CExpandedAttDbColumn> attTree = new List<CExpandedAttDbColumn>();
            CExpandedAttDbColumn.递归_将给定Type的AttDbColumn展开到结果_树型结构(typeof(T), null, ref attTree, false);

            List<CExpandedAttDbColumn> attList = null;
            CExpandedAttDbColumn.Tree2List(attTree, ref attList);

            List<string> hdList = new List<string>();
            string hdStr = string.Empty;
            for (int i = 0; i < attList.Count; i++)
            {
                CExpandedAttDbColumn crrAtt = attList[i];
                hdList.Add($"`{crrAtt.DbColumnName}`");

                    hdStr += (_alias == null? string.Empty: $"{_alias}.")+crrAtt.DbColumnName;

                if (i + 1 < attList.Count)
                    hdStr += ", ";
            }

            string sql = $"SELECT {hdStr} {_sqlAllAfterSelect}";
            //return this.LoadFromDb<T>(sql, hdList.ToArray());
            return this.LoadFromDb<T>(sql, attTree);
        }//LoadFromDb<T>() where T:class

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_attTableType"></param>
        /// <param name="_tableName_Prefix"></param>
        /// <param name="_tableName_Body"></param>
        /// <param name="_tableName_Postfix"></param>
        /// <param name="_WhenExist"></param>
        /// <param name="_bCreateIndex"></param>
        /// <returns>如果实际创建了表格，就返回true。否则返回false</returns>
        public bool CreateTable(
            Type _attTableType,
            string _tableName_Prefix,
            string _tableName_Body,
            string _tableName_Postfix,
            EWhenExist _WhenExist,
            bool _bCreateIndex)
        {
            lock (Locker_CreateTable)
            {
                string _tableName = _tableName_Prefix + _tableName_Body + _tableName_Postfix;

                //查询表是否已经存在
                //string tableName = $"{this.GetTableName(DbKey_Objects)}";
                string sql = $"show tables like '{_tableName}'";
                //string sql = $"SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_NAME = '{tableName}' AND TABLE_SCHEMA='{GCfg.DbName_OutPool}' ";
                List<string> existIds = this.ExecuteSqlStrAsList(sql);

                if (existIds != null && existIds.Count != 0)
                {
                    string logStr = $"Table {_tableName} 在数据库中已存在= {sql}";
                    switch (_WhenExist)
                    {
                        case EWhenExist.DoNothing:
                            //this.MsgList.Add(logStr);
                            return false;
                        case EWhenExist.ThrowException:
                            throw new Exception(logStr);
                        case EWhenExist.DeleteAndDoAgain:
                            sql = $"DROP table "
                                + $" {_tableName}";
                            this.ExecuteSqlStrAsVoid(sql);
                            break;
                        default:
                            throw new Exception($"发现未支持的EWhenExist.{_WhenExist}" + logStr);
                    }//switch(this.In_WhenExist)

                }//if (existIds.Count > 1)


                //执行创建
                string sql_createTable_Obj = CSqlHelper.AttClass2CreateTable_WithoutNormalIndex(
                    _attTableType,//typeof(CFrame_PerceptionObstacle),
                    _tableName_Prefix,//this.In_TaskId + "_",
                    _tableName_Body,
                    _tableName_Postfix,
                    true
                    );

                this.ExecuteSqlStrAsVoid(sql_createTable_Obj);

                if (_bCreateIndex)
                {
                    string sqlIndex = CSqlHelper.AttClass2CreateIndexes(_attTableType, _tableName, false);
                    this.ExecuteSqlStrAsVoid(sqlIndex);
                }
            }//lock (Locker_CreateTable)
            return true;
        }//创建数据表_Objects()

        public bool IsTableExist(string _tableNameExp)
        {
            string sql = $"show tables like '{_tableNameExp}'";
            //string sql = $"SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_NAME = '{tableName}' AND TABLE_SCHEMA='{GCfg.DbName_OutPool}' ";
            List<string> existIds = this.ExecuteSqlStrAsList(sql);

            if (existIds != null && existIds.Count != 0)
                return true;
            else
                return false;

        }//IsTableExist(string _tableNameExp)

        public void EnableTableKeys(string _dbTableName)
        {
            string sql = $"alter table {_dbTableName} enable keys";
            this.ExecuteSqlStrAsVoid(sql);
        }//EnableTableKeys(string _tableName)

        public void DisableTableKeys(string _dbTableName)
        {
            string sql = $"alter table {_dbTableName} disable keys";
            this.ExecuteSqlStrAsVoid(sql);
        }//DisableTableKeys(string _dbTableName)

        public void CreateIndexies(Type _objType, string _dbAndTableName, bool _includePrimaryKey)
        {
            string sql = CSqlHelper.AttClass2CreateIndexes(_objType, _dbAndTableName, _includePrimaryKey);
            this.ExecuteSqlStrAsVoid(sql);
        }//CreateIndexies(Type _objType, string _dbAndTableName, bool _includePrimaryKey)

        public string GetCreateIndexies(Type _objType, string _dbAndTableName, bool _includePrimaryKey)
        {
            return CSqlHelper.AttClass2CreateIndexes(_objType, _dbAndTableName, _includePrimaryKey);
        }

        /// <summary>
        /// count how many items in a table.
        /// </summary>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public int CountTable(string _tableName)
        {
            string sql = $"select count(*) from `{_tableName}`";
            string rst = this.ExecuteSqlStrAsString(sql);
            return int.Parse(rst);
        }

    }//CSingleSqlExecuter
}
