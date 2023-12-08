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
using System.Collections;

namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// COptHistoryMgr 的摘要说明。
	/// </summary>
	public class COptHistoryMgr
	{
		COdbcMgr  m_OdbcMgr;

		public enum EColType
		{
			HstID       ,
			ChgType     ,
			OptTime     ,
			OptUserID   ,
			OptID       ,
			RcdID       ,
			PreValue    ,
			ChgValue    
		};

		public enum EOrderBy
		{
			HstID       ,
			ChgType     ,
			OptTime     ,
			OptUserID   ,
			OptID       ,
			RcdID       ,
			PreValue    ,
			ChgValue    
		};

		public COptHistoryMgr(COdbcMgr  _OdbcMgr)
		{
			m_OdbcMgr = _OdbcMgr;
		}

		bool InsertItem( COptHistoryItem _newItem )
		{
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_cdtV">vector(CItemBasic) </param>
		/// <param name="_orderby"></param>
		/// <returns></returns>
		int QueryCount( ArrayList _cdtV )
		{
			return 0;
		}

		/// <summary>
		/// QueryItems
		/// </summary>
		/// <param name="_cdtV"> ArrayList(CItemBasic) </param>
		/// <param name="_orderby"></param>
		/// <param name="_beginPos"> </param>
		/// <param name="_rstNum"> How many items do you want.</param>
		/// <returns>Return ArrayList(CItemBasic). Return null when DB error.</returns>
		ArrayList QueryItems( ArrayList _cdtV , EOrderBy _orderby , int _beginPos, int _rstNum )
		{
			return null;
		}

	}//class COptHistoryMgr
}//namespace
