/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2004-11-06  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;
using System.Data.SqlClient;
using System.Data;
using Alivever.Com.DevBasic.BasicLib.LogCtrl;

namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// CSqlServerMgr 的摘要说明。
	/// </summary>
	public class CSqlServerMgr :CDbMgrBase
	{
		public bool   m_bAlwaysConn = true;		
		public string m_DbOption = "";

		private string m_DbDriver = "";
		private string m_DbServer = "";
		private string m_DbDatabase = "";
		private string m_DbUid    = "";
		private string m_DbPwd    = "";
		//private string m_DBconnStr = "";

		//		public string DBconnStr
		//		{
		//			set{ this.m_DBconnStr = value ;}
		//			get
		//			{ 
		//				if ( m_DBconnStr.Length == 0 )
		//					m_DBconnStr = GenerateDbConnStr();
		//
		//				return m_DBconnStr;
		//			}
		//		}//string DBconnStr


		//private SqlConnection m_DbConn = null ;

		//"Driver={MySQL Sql 3.51Driver};Server=127.0.0.1;Option=16834;Database=mydb;Uid=root;Pwd=;"
		//private string m_DBconnStr = "";


		public CSqlServerMgr()
		{
		}

		public CSqlServerMgr( string _DBconnStr )
		{
			this.DBconnStr = _DBconnStr;
		}

		public CSqlServerMgr( string _DbDriver, string _DbServer, string _DbDatabase, string _DbUid, string _DbPwd)
		{
			SetValues( _DbDriver,  _DbServer,  _DbDatabase,  _DbUid,  _DbPwd);
		}

		public void SetValues( string _DbDriver, string _DbServer, string _DbDatabase, string _DbUid, string _DbPwd)
		{
			m_DbDriver = _DbDriver;
			m_DbServer = _DbServer;
			m_DbDatabase = _DbDatabase;
			m_DbUid    = _DbUid;
			m_DbPwd    = _DbPwd;

		}


		public override bool ConnectDB( )
		{
			return OpenDB( this.DBconnStr );
		}

		public override bool OpenDB( string _DBconnStr )
		{
			if ( m_DbConn != null )
				m_DbConn.Close();

			m_DbConn = new SqlConnection(_DBconnStr);
			return OpenDB( true );
		}//ConnectDB(1)

		public override bool OpenDB(  bool _bForceOpen )
		{

			if ( m_DbConn == null)
			{
				if ( !_bForceOpen )
				{
					m_LastError = "CSqlMgr hasn't been connected.";
					GSdkMLog.At( GT.pkgName).Write("CSqlMgr.OpenDB(1)",1 , "CSqlMgr hasn't been connected.\n");

					return false;
				}
				else
					m_DbConn = new SqlConnection( this.DBconnStr );
			}

			if ( (m_DbConn.State & ConnectionState.Open) != 0 )
				return true;

			try
			{
				m_DbConn.Open();
			}
			catch( Exception e )
			{
				m_LastError = e.Message ;
				this.m_DbState = EDbState.Closed;
				return false;
			}

			this.m_DbState = EDbState.Open;
			return true;
		}//OpenDB()

		public override void CloseDB()
		{
			if ( m_DbConn != null )
				m_DbConn.Close();

			m_DbConn = null;
		}

		/*
		public bool IsConnected()
		{
			return m_DbConn != null || !(m_DbConn.State & ConnectionState.Open) ;
		}
		*/

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sqlStr"></param>
		/// <returns>return null if error occured.</returns>
		public SqlDataReader RunSqlMultiResult(  string _sqlStr )
		{
			if ( this.m_DbState != EDbState.Open && !this.OpenDB( true ) )
			{
				GSdkMLog.At( GT.pkgName).Write("CSqlMgr.RunSqlMultiResult(1)",1 , "Can't run SQL=\""+ _sqlStr +"\",because:" + m_LastError + "\n");
				m_LastError = "CSqlMgr can be connected or opened.";
				return null;
			}

			SqlDataReader odReader;
			try
			{
				SqlCommand mySqlCommand = new SqlCommand( _sqlStr , (SqlConnection)m_DbConn);
				odReader = mySqlCommand.ExecuteReader();
			}
			catch( Exception ex)
			{
				m_LastError = ex.Message ;
				GSdkMLog.At( GT.pkgName).Write("CSqlMgr.RunSqlMultiResult(1)",1 , "run SQL error. SQL=\""+ _sqlStr +"\",Message=" + m_LastError + "\n");
				return null;
			}

			if ( !m_bAlwaysConn )
				m_DbConn.Close();
			return odReader;
		}//RunSqlMultiResult

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sqlStr"></param>
		/// <param name="_RstObj"> retult object. = null if nothing is found.</param>
		/// <returns>Return false if any error occured. </returns>
		public override bool RunSqlSingleResult(  string _sqlStr , ref Object _RstObj )
		{
			if ( this.m_DbState != EDbState.Open && !this.OpenDB( true ) )
			{
				GSdkMLog.At( GT.pkgName).Write("CSqlMgr.RunSqlSingleResult(1)",1 , "Can't run SQL=\""+ _sqlStr +"\",because:" + m_LastError + "\n");
				m_LastError = "CSqlMgr can be connected or opened.";
				return false;
			}

			//object obj;
			try
			{
				SqlCommand mySqlCommand = new SqlCommand( _sqlStr , (SqlConnection)m_DbConn);
				_RstObj = mySqlCommand.ExecuteScalar();
			}
			catch( Exception ex)
			{
				m_LastError = ex.Message ;
				GSdkMLog.At( GT.pkgName).Write("CSqlMgr.RunSqlSingleResult(1)",1 , "run SQL error. SQL=\""+ _sqlStr +"\",Message=" + m_LastError + "\n");
				return false;
			}

			if ( !m_bAlwaysConn )
				m_DbConn.Close();

			return true;
		}//RunSqlSingleResult


		public override bool RunSqlNoReturn( string _sqlStr )
		{
			if ( this.m_DbState != EDbState.Open && !this.OpenDB( true ) )
			{
				GSdkMLog.At( GT.pkgName).Write("CSqlMgr.RunSqlNoReturn(1)",1 , "Can't run SQL=\""+ _sqlStr +"\",because:" + m_LastError + "\n");
				m_LastError = "CSqlMgr can be connected or opened.";
				return false;
			}

			try
			{
				SqlCommand mySqlCommand = new SqlCommand( _sqlStr , (SqlConnection)m_DbConn);
				mySqlCommand.ExecuteNonQuery();
			}
			catch( Exception ex)
			{
				m_LastError = ex.Message ;
				GSdkMLog.At( GT.pkgName).Write("CSqlMgr.RunSqlNoReturn(1)",1 , "run SQL error. SQL=\""+ _sqlStr +"\",Message=" + m_LastError + "\n");
				return false;
			}

			if ( !m_bAlwaysConn )
				m_DbConn.Close();
			return true;
		}//RunSql(string);


		//"Driver={MySQL Sql 3.51Driver};Server=127.0.0.1;Option=16834;Database=mydb;Uid=root;Pwd=;"
		protected override string GenerateDbConnStr()
		{
			string connStr = "";

			connStr += "Driver={"+ m_DbDriver +"};";
			connStr += "Server="+ m_DbServer+";";
			connStr += "Database="+ m_DbDatabase+";";
			connStr += "Uid="+ m_DbUid+";";
			connStr += "Pwd="+ m_DbPwd+";";
			//connStr += "Driver={"+ +"};";

			if ( m_DbOption.Length != 0 )
				connStr += "Option={"+ m_DbOption+"};";
			
			return connStr;
		}//GenerateDbConnStr()

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_tableName">The DB table will be locked.</param>
		/// <param name="_bStillCanRead">False = all other thread can't access this table.</param>
		/// <returns></returns>
		public override bool LockTable( string _tableName , bool _bStillCanRead )
		{
			return true;
		}

		public override void UnlockTable( string _tableName )
		{

		}

		public override int QueryMaxID( string _tableName , string _IdColName )
		{
			Object maxID = 0 ;

			string sqlStr = "select max(" + _IdColName+") from " + _tableName ;
			if ( ! RunSqlSingleResult( sqlStr , ref maxID ) || maxID is System.DBNull )
			{
				return 0;
			}

			return (int)maxID;
		}

	}//class CSqlServerMgr
}
