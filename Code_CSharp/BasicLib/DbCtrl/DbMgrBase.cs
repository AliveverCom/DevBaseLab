/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2007-02-26  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;
using System.Data;

namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// CDbMgrBase 的摘要说明。
	/// </summary>
	public abstract class CDbMgrBase : IDbMgr
	{

		protected EDbState m_DbState = EDbState.None;
		//m_LastError is not thread save
		protected string m_LastError;// { get{  return m_LastError; } }

		protected string m_DBconnStr = "";

		protected IDbConnection m_DbConn = null ;

		public string DBconnStr
		{
			set{ this.m_DBconnStr = value ;}
			get
			{ 
				if ( m_DBconnStr.Length == 0 )
					m_DBconnStr = GenerateDbConnStr();

				return m_DBconnStr;
			}
		}//string DBconnStr

		public CDbMgrBase()
		{
		}

		public virtual EDbState GetDbState()
		{
			return m_DbState;
		}

		public virtual string GetLastError()
		{
			return m_LastError;
		}

		public CDbMgrBase Create( EDbProductType _DbType )
		{
			switch( _DbType )
			{
				case EDbProductType.ODBC : 
					return new COdbcMgr();
				default :
					return null;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_tableName">The DB table will be locked.</param>
		/// <param name="_bStillCanRead">False = all other thread can't access this table.</param>
		/// <returns></returns>
		public abstract bool LockTable( string _tableName , bool _bStillCanRead );

		public abstract void UnlockTable( string _tableName );

		public abstract int QueryMaxID( string _tableName , string _IdColName );


		public abstract bool ConnectDB( );

		public abstract bool OpenDB( string _DBconnStr );

		public abstract bool OpenDB(  bool _bForceOpen );

		public abstract void CloseDB();

		public abstract bool RunSqlNoReturn( string _sqlStr );

		public  abstract bool RunSqlSingleResult(  string _sqlStr , ref Object _RstObj );

		protected abstract string GenerateDbConnStr();


	}//class CDbMgrBase
}//namespace
