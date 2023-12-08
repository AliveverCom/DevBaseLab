/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2004-11-06  </ChangeDate>
///     <Description> </Description>
/// </History>

/*********** references and namespaces  ***************/
using System;
using Alivever.Com.DevBasic.BasicLib.LogCtrl;


namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// Class: 
	/// </summary>
	/// <History>
	///	<Devoloper> Shao Chen Ye </Devoloper>
	///	<Date> 2004-11-06  </ChangeDate>
	///     <Description>Create Class </Description>
	/// </History>
	public class CDbObjSqlHelper
	{
		// =========== methods ================

		/// <summary>
		/// Only insert DBAutoKeys and NotNullCols, other will be empty 
		/// </summary>
		public static string ToMinInsertStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			return "INSERT INTO " + _iDbObjSql.GetTableName() 
								+ " ( " + GetMinColsNameStr( _iDbObjSql , _bUseDbAutoKey) 
								+ " ) VALUES ( " 
								+ GetMinColsValueStr(_iDbObjSql,_bUseDbAutoKey) + " )";
		}

		/// <summary>
		/// Only insert DBAutoKeys and NotNullCols, other will be empty 
		/// </summary>
		public static string ToMinReplaceStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			return "REPLACE " + _iDbObjSql.GetTableName() 
								+ " ( " + GetMinColsNameStr( _iDbObjSql , _bUseDbAutoKey) 
								+ " ) VALUES ( " 
								+ GetMinColsValueStr(_iDbObjSql,_bUseDbAutoKey) + " )";
		}

		/// <summary>
		///  
		/// </summary>
		public static string ToInsertStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			return "INSERT INTO " + _iDbObjSql.GetTableName() 
								+ " ( " + GetFullColsNameStr( _iDbObjSql , _bUseDbAutoKey) 
								+ " ) VALUES ( " 
								+ GetFullColsValueStr(_iDbObjSql,_bUseDbAutoKey) + " )";
			
		}//className()

		public static string GetFullColsValueStr( IDbObjSql _iDbObjSql,
			bool _bUseDbAutoKey )
		{
			return "";
		}

		/// <summary>
		///  
		/// </summary>
		public static string ToReplaceStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			return "REPLACE " + _iDbObjSql.GetTableName() 
								+ " ( " + GetFullColsNameStr( _iDbObjSql , _bUseDbAutoKey) 
								+ " ) VALUES ( " 
								+ GetFullColsValueStr(_iDbObjSql,_bUseDbAutoKey) + " )";
			
		}//className()

		protected static string GetFullColsNameStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			string rstCmd = GetMinColsNameStr( _iDbObjSql, _bUseDbAutoKey );
			bool bHasNormalCols = _iDbObjSql.GetNormalColNameStr().Length !=0;

			if (  rstCmd.Length == 0 && !bHasNormalCols ) 
			{
				GSdkMLog.At( GT.pkgName ).Write("CDbObjSqlHelper.GetInsertStr()",0, 
					"Not any column found. _iDbObjSql:\"{0}\"",_iDbObjSql.ToString() );
				return null;
			}

			if ( bHasNormalCols )
				rstCmd += ((_bUseDbAutoKey||_iDbObjSql.GetDbAutoKeyNameStr().Length!=0)?", ":" ") + _iDbObjSql.GetNormalColNameStr();
			
			return rstCmd;

		}

		/*
		protected string GetMinColsValueStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			string rstCmd = GetMinColNameStr( _iDbObjSql, _bUseDbAutoKey );
			bool bHasNormalCols = _iDbObjSql.GetNormalColNameStr().Length !=0;


			if ( bHasNormalCols )
				rstCmd += ((bHasDbAutoKey||bHasDbAutoKey)?", ":" ") + _iDbObjSql.GetNormalColValueStr();
			
			return rstCmd;

		}
*/

		protected static string GetMinColsNameStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			bool bHasDbAutoKey = _bUseDbAutoKey && _iDbObjSql.GetDbAutoKeyNameStr().Length !=0;
			bool bHasNotNullCols = _iDbObjSql.GetNotNullColsNameStr().Length !=0;

			if ( !( bHasDbAutoKey || bHasNotNullCols ) )
			{
				GSdkMLog.At( GT.pkgName ).Write("CDbObjSqlHelper.GetMinCloNameStr()",0, 
					"Not any Keys found. _iDbObjSql:\"{0}\"",_iDbObjSql.ToString() );
				return null;
			}

			string rstCmd = "";

			//add DbAutoKey
			if ( bHasDbAutoKey )
				rstCmd += _iDbObjSql.GetDbAutoKeyNameStr() ;

			//add NotNullColsNameStr
			if ( bHasNotNullCols )
				rstCmd += (bHasDbAutoKey?", ":"") + _iDbObjSql.GetNotNullColsNameStr();

			return rstCmd;
		}//GetMinCloNameStr(2)
						 
		protected static string GetMinColsValueStr( IDbObjSql _iDbObjSql,
						   bool _bUseDbAutoKey )
		{
			string rstCmd = "";

			//add DbAutoKeyValue
			if ( _bUseDbAutoKey && _iDbObjSql.GetDbAutoKeyNameStr().Length !=0 )
				rstCmd += _iDbObjSql.GetDbAutoKeyValueStr() ;

			//add NotNullColsNameStr
			if ( _iDbObjSql.GetNotNullColsNameStr().Length !=0 )
				rstCmd += (rstCmd.Length!=0?", ":" ") + _iDbObjSql.GetNotNullColsValueStr();
			
			return rstCmd;
		}//GetMinColsValueStr(2)

	} //class

}//namespace

