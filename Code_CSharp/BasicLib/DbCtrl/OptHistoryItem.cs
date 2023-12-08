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
using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// HstID        纪录编号
	/// ChgType      修改的动作类型（read/write/execute/deleted/create）
	/// OptTime,     操作的时间
	/// OptUser,     操作的用户
	/// OptID        操作种类的表示( notes, flosheet, Dish order, report) 
	/// RcdID,       操作的记录号
	/// PreValue，   操作以前记录的值
	/// ChgValue     操作以后记录的值/ID

	/// </summary>
	public class COptHistoryItem 
	{
		public int     m_HstID       ;
		public short   m_ChgType     ;
		public long    m_OptTime     ;
		public int     m_OptUserID   ;
		public int     m_OptID       ;
		public int     m_RcdID       ;
		public string  m_PreValue    ;
		public string  m_ChgValue    ;
	}//class CUserBasic
}//namespace
