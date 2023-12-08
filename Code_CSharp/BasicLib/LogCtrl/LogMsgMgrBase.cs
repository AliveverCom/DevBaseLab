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
using System.Collections;

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
	/// <summary>
	/// CReportMsgClassMgrBase 
	/// </summary>
	public class CLogMsgMgrBase : CItemBasicCollectionVector
	{
		/// <summary>
		/// Class Nomber of message. 0-99 are remained for public
		/// Customer should use begin 100
		/// </summary>
		public const int MsgClsNo_None			= 0;
		public const int MsgClsNo_UnknowErr		= 1;
		public const int MsgClsNo_LastPublicNo	= 2;
		public const int MsgClsNo_MaxPublicNo	= 99;

//		//////////////////// begin Singleton ///////////////////
//		/// <summary>
//		/// Singleton attrivute;
//		/// </summary>
//		//protected CLogMsgMgrBase m_Ins;
//
//		/// <summary>
//		/// Singleton attrivute. If yuu overrided CreateLogMsg(3), 
//		/// please also override this attribute so that singleton Ins 
//		/// can auto invoke new/currect method.
//		/// </summary>
//		public virtual CLogMsgMgrBase Ins
//		{
//			 get;
//			 { 
//				if ( m_Ins == null)
//					new CLogMsgMgrBase();
//	 
//				return m_Ins; 
//			 }
//		}
//		//////////////////// begin Singleton ///////////////////

		public CLogMsgMgrBase()
		{

		}

		/// <summary>
		/// The function to create new LogMsg for Cyslon SDK.
		/// </summary>
		/// <param name="_MsgClassNo"></param>
		/// <param name="_bNeedExplain">Dedicates if Explain is needed to fill into m_sName</param>
		/// <param name="_sRuntimeDesc"></param>
		/// <returns></returns>
		public static CLogMsgBase CreatePublicLogMsg( int _MsgClassNo, bool _bNeedExplain, string _sRuntimeDesc )
		{
			switch ( _MsgClassNo )
			{
			case MsgClsNo_None : 
				return new CLogMsgBase( 
					MsgClsNo_None,
					CLogMsgBase.MsgType_Error,
					CLogMsgBase.MsgLevel_5,
					( _bNeedExplain ? "normal log message." : null),
					_sRuntimeDesc );
			case MsgClsNo_UnknowErr	 : 	
				return new CLogMsgBase( 
					MsgClsNo_UnknowErr,
					CLogMsgBase.MsgType_Error,
					CLogMsgBase.MsgLevel_5,
					( _bNeedExplain ? "Unknow Error." : null),
					_sRuntimeDesc );
			case MsgClsNo_LastPublicNo : 
				return new CLogMsgBase( 
					MsgClsNo_LastPublicNo,
					CLogMsgBase.MsgType_Error,
					CLogMsgBase.MsgLevel_5,
					( _bNeedExplain ? "Mark of Last Public No " : null),
					_sRuntimeDesc );
			case MsgClsNo_MaxPublicNo : 	
				return new CLogMsgBase( 
					MsgClsNo_MaxPublicNo,
					CLogMsgBase.MsgType_Error,
					CLogMsgBase.MsgLevel_5,
					( _bNeedExplain ? "Mark of Max Public No" : null),
					_sRuntimeDesc );
			}//switch ( _MsgClassNo )

			return null;
		}//CreateSdkLogMsg
	
		public ArrayList GetMsgByClassNo( int _nClsNo )
		{
			ArrayList rstV = new ArrayList ();

			foreach( CLogMsgBase crrItem in m_Items )
			{
				if ( crrItem.ClassNo == _nClsNo  )
				rstV.Add( crrItem );
			}

			return rstV;
		}//GetMsgByClassNo(1)

		/// <summary>
		/// Look for LogMsg by _MsgType and Level value less than _MaxLevel.
		/// </summary>
		/// <param name="_MsgType"></param>
		/// <param name="_MinLevel"></param>
		/// <returns></returns>
		public ArrayList GetMsgByMsgType( byte _MsgType, byte _MaxLevel )
		{
			ArrayList rstV = new ArrayList ();

			foreach( CLogMsgBase crrItem in m_Items )
			{
				if ( crrItem.MsgType == _MsgType && crrItem.Level <= _MaxLevel  )
					rstV.Add( crrItem );
			}

			return rstV;
			
		}//GetMsgByMsgType(2)

		/// <summary>
		/// The function to create new LogMsg for both Cyslon SDK and customized classes.
		/// Subclass should override this method to create their own obj for AddMsg(3) and other methods.
		/// </summary>
		/// <param name="_MsgClassNo"></param>
		/// <param name="_bNeedExplain">Dedicates if Explain is needed to fill into m_sName</param>
		/// <param name="_sRuntimeDesc"></param>
		/// <returns></returns>
		public virtual CLogMsgBase CreateLogMsg( int _MsgClassNo, bool _bNeedExplain, string _sRuntimeDesc )
		{
			return null;
		}

		public virtual void AddMsg( int _MsgClassNo, bool _bNeedExplain, string _sRuntimeDesc )
		{
			if ( _MsgClassNo < MsgClsNo_LastPublicNo )
				m_Items.Add( CreatePublicLogMsg( _MsgClassNo, _bNeedExplain, _sRuntimeDesc ) );
			else
				m_Items.Add( CreateLogMsg( _MsgClassNo, _bNeedExplain, _sRuntimeDesc ) );

		}

	}//class CReportMsgClassMgrBase
}//namespace
