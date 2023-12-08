using System;
using System.Collections;
using System.Data.Odbc;

using  Alivever.Com.DevBasic.BasicLib.DbCtrl;
using  Alivever.Com.DevBasic.BasicLib.LogCtrl;

namespace Alivever.Com.DevBasic.BasicLib.CfgLib
{
	/// <summary>
	/// AbChoiceItemsCfgOdbc 的摘要说明。
	/// </summary>
	public class CAbChoiceItemsCfgOdbc : CAbChoiceItemsCfgBase
	{
		protected COdbcMgr  m_OdbcMgr;

		protected string m_TableName;

		//protected string m_LastError;

		public CAbChoiceItemsCfgOdbc( COdbcMgr  _OdbcMgr )
		{
			m_OdbcMgr = _OdbcMgr;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_nListID"></param>
		/// <returns>
		/// Hashtable( CAbChoiceItem.nID , CAbChoiceItem );
		/// return empty Hashtable if not found.
		/// return 'null' If DB Error;
		/// </returns>
		public override Hashtable GetItemsByListID(int _nListID )
		{
			string sqlStr = "select "+ XTag.TAbclItems.Cli_ID + ", "
									 + XTag.TAbclItems.Cli_Name +  ", "
									 + XTag.TAbclItems.Cli_AccKey +  ", "
									 + XTag.TAbclItems.Cli_Desc +  ", "
							+" from " + XTag.tAbclItems 
							+" where "+ XTag.TAbclItems.Cl_ID +"="+_nListID ;

			OdbcDataReader dataReader = m_OdbcMgr.RunSqlMultiResult( sqlStr );
									
			if ( dataReader == null )
			{
				m_LastError = m_OdbcMgr.GetLastError();
				return null;
			}

			Hashtable rstTable = new Hashtable();

			while( dataReader.Read() )
			{
				int nID = int.Parse( dataReader.GetString(0) );
				rstTable.Add( nID , new CAbChoiceItem( nID, 
														dataReader.GetString(1),
														dataReader.GetString(2),
														dataReader.GetString(3) 
													 )
							);
			}//while(dataReader)
			dataReader.Close();

			return rstTable;
		}//GetItemsByListID(s)

		/// <summary> Insert a new CAbChoiceItem into table. </summary>
		/// <param name="_newItem"> 
		/// if _newItem.nID less then 0, then allocate a new ID.
		/// if _newItem.nID large then -1, use specified ID.
		/// _newItem.nID will be changed to a New ID, =1 if fail. 
		/// </param>
		/// <returns> return New ID of that item. return -1 when fail.</returns>
		public override int InsertItem( int _nListID ,ref CAbChoiceItem _newItem )
		{
			if ( _newItem == null )
			{
				m_LastError = "内部输入数据位空。";
				GSdkMLog.At( GT.pkgName).Write("CAbChoiceItemsCfgOdbc.InsertItem(1)",1 , "_newItem = null\n");
				return -1;
			}

			if ( !m_OdbcMgr.LockTable( XTag.tAbclItems , false ) )
			{
				m_LastError = "锁定数据库出错。";
				GSdkMLog.At( GT.pkgName).Write("CAbChoiceItemsCfgOdbc.InsertItem(1)",1 , m_OdbcMgr.GetLastError()+"\n");
				return -1;
			}

			int nNewCli_ID = -1;

			if ( !GetMaxIdOfList( _nListID, ref nNewCli_ID ) )
			{
				m_LastError = "获得最大记录号出错。";
				GSdkMLog.At( GT.pkgName).Write("CAbChoiceItemsCfgOdbc.InsertItem(1)",1 , m_OdbcMgr.GetLastError()+"\n");
				return -1;
			}

			if ( _newItem.nID < 0 )
				nNewCli_ID += 1;
			else
				nNewCli_ID = _newItem.nID;

			string sqlStr = "insert into " + XTag.tAbclItems + "( "
									+ XTag.TAbclItems.Cl_ID + ", "
									+ XTag.TAbclItems.Cli_ID + ", "
									+ XTag.TAbclItems.Cli_Name + ", "
									+ XTag.TAbclItems.Cli_AccKey + ", "
									+ XTag.TAbclItems.Cli_Desc + " ) "
							+ "values ( " + _nListID.ToString() + ", "
									 + nNewCli_ID.ToString() + ", "	
									 + "'"+ _newItem.Name + "', "	
									 + "'"+ _newItem.m_AccKey + "', "	
									 + "'"+ _newItem.Desc + "' ) ";
	
			if ( !m_OdbcMgr.RunSqlNoReturn( sqlStr ) )
			{
				m_LastError = "插入新记录出错";
				GSdkMLog.At( GT.pkgName).Write("CAbChoiceItemsCfgOdbc.InsertItem(1)",1 , m_OdbcMgr.GetLastError()+"\n");
				m_OdbcMgr.UnlockTable( XTag.tAbclItems );
				return -1;
			}

			m_OdbcMgr.UnlockTable( XTag.tAbclItems );
			return nNewCli_ID;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_listID"></param>
		/// <param name="_MaxID">
		/// Return '-1' if no item found .
		/// Return '-2' if run sql error.
		/// </param>
		/// <returns></returns>
		public override bool GetMaxIdOfList( int _nListID , ref int _MaxID )
		{
			Object obj = null;

			string sqlStr = "select max( "+ XTag.TAbclItems.Cli_ID + " ) "
							+" from " + XTag.tAbclItems 
							+" where "+ XTag.TAbclItems.Cl_ID +"="+_nListID ;

			if ( ! m_OdbcMgr.RunSqlSingleResult( sqlStr,ref obj ) )
			{
				m_LastError = m_OdbcMgr.GetLastError();
				m_OdbcMgr.UnlockTable( XTag.tAbclItems );
				_MaxID = -2 ;
				return false;
			}
			
			if ( obj == System.DBNull.Value )
				_MaxID = -1;
			else
				_MaxID = (Int16) obj;
			return true;
		}

		public override EDbState GetDbState()
		{
			return m_OdbcMgr.GetDbState();
		}


	}//class CAbChoiceItemsCfgOdbc
}//namespace
