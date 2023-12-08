/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-02-05  </ChangeDate>
///     <Description> </Description>
/// </History>

using System;
using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
//	public enum EReportMsgTpye
//	{
//		InnerInfo = 0,  Error = 1, Warrning =2 
//	}
	
	/// <summary>
	/// Class of compiling Report Msg .
	/// </summary>
	/// <remarks>
	/// Explain of attributes in CItemBase 
	/// CItemBase.m_nID = seqNo designed for storage
	/// CItemBase.m_sName = Default expain of this ErrorClass
	/// CItemBase.m_Desc = Description in runtime 
	/// 
	///</remarks>
	public class CLogMsgBase : CItemBase
	{

		/// <summary>
		/// Message type. You may us pre-defined values MsgType_??;
		/// The attributes of this class are readonly after initialized.
		/// This class is usually used in compileration programs.
		/// </summary>
		protected byte  m_MsgType = MsgType_Error ;
		public const byte MsgType_InnerInfo = 0 ;
		public const byte MsgType_Error = 1 ;
		public const byte MsgType_Warrning = 2 ;
		public byte MsgType { get{ return m_MsgType;} }

		
		/// <summary>
		/// The level value of the message. 
		/// Value range from 10 to 50 step 10. 
		/// 10 means the most critical and importment, and 0-9 is remained for SDK.
		/// Of couse, you may also use some value like 22 for expand.
		/// You may also use pre-defined constaint value "MsgLevel_??" to instead.
		/// </summary>
		protected byte m_Level = MsgLevel_1;
		public const byte MsgLevel_SDK = 0;
		public const byte MsgLevel_1 = 10;
		public const byte MsgLevel_2 = 20;
		public const byte MsgLevel_3 = 30;
		public const byte MsgLevel_4 = 40;
		public const byte MsgLevel_5 = 50;
		public byte Level{ get{ return m_Level; } }

		/// <summary>
		/// Class Nomber of message. 
		/// Class Nomber of message. 0-99 are remained for public
		/// Customer should use begin 100
		/// </summary>
		protected int m_MsgClassNo ;
		public const int MsgClsNo_None			= 0;
		public const int MsgClsNo_UnknowErr		= 1;
		public const int MsgClsNo_LastPublicNo	= 2;
		public const int MsgClsNo_MaxPublicNo	= 99;
		public int ClassNo{ get { return m_MsgClassNo; } }

		/// <summary>
		/// Create CReportMsgBase( MsgType_Error, MsgLevel_1, m_MsgClassNo=0, _Desc)
		/// </summary>
		/// <param name="_Desc"></param>
		public CLogMsgBase ( string	_Desc ) :  base( -1, "", _Desc )
		{
		}

		public CLogMsgBase ( 
			int			_MsgClassNo, 
			byte		_MsgType ,
			byte		_Level, 
			string      _Explain, 
			string		_Desc ) : base( _Desc )
		{
			m_MsgType	= _MsgType;
			m_MsgClassNo = _MsgClassNo;
			m_Level		= _Level; 
			m_sName     = _Explain;
		}

//		/// <summary>
//		/// Using CItemBasic.m_sName to be Desc of this class.
//		/// </summary>
//		public string RuntimeDesc 
//		{
//			get{ return m_sDesc; }
//			set{ m_sDesc = value; }
//		}

		public string Explain
		{
			get{ return this.Name ; }
		}

	}//class CCplMsgBase
}//namespace
