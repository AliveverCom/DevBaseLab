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
	/// HstID        ��¼���
	/// ChgType      �޸ĵĶ������ͣ�read/write/execute/deleted/create��
	/// OptTime,     ������ʱ��
	/// OptUser,     �������û�
	/// OptID        ��������ı�ʾ( notes, flosheet, Dish order, report) 
	/// RcdID,       �����ļ�¼��
	/// PreValue��   ������ǰ��¼��ֵ
	/// ChgValue     �����Ժ��¼��ֵ/ID

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
