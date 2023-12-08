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

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
	/// <summary>
	/// SdkLogMsgMgrBase 的摘要说明。
	/// For m_MsgClassNo, 0-99999 are remained for SDK.Customer should use begin 10000
	/// </summary>
	public class SdkLogMsgMgr : CLogMsgMgrBase
	{
		
		/// <summary>
		/// Class Nomber of message. 0-99 are remained for public
		/// Customer should use begin 100
		/// 100-99999 are remained for SDK
		/// Customer should use begin 10000
		/// </summary>
		public const int MsgClsNo_SdkExpired	= 100;
		public const int MsgClsNo_SdkLastNo		= 101; 
		public const int MsgClsNo_SdkEndNo		= 99999;

//		//////////////////// begin Singleton ///////////////////
//		/// <summary>
//		/// Singleton attrivute;
//		/// </summary>
//		protected SdkLogMsgMgr m_Ins;
//		
//		/// <summary>
//		/// Singleton attrivute. If yuu overrided CreateLogMsg(3), 
//		/// please also override this attribute so that singleton Ins 
//		/// can auto invoke new/currect method.
//		/// </summary>
//		public virtual CLogMsgMgrBase Ins
//		{
//			get
//			{ 
//				if ( m_Ins == null)
//					new SdkLogMsgMgr();
//			 
//				return m_Ins; 
//			}
//		}
//		//////////////////// begin Singleton ///////////////////
		///
		public SdkLogMsgMgr()
		{
		}

		/// <summary>
		/// The function to create new LogMsg for Cyslon SDK.
		/// Note: This method should used singleton instance.
		/// </summary>
		/// <param name="_MsgClassNo"></param>
		/// <param name="_bNeedExplain">Dedicates if Explain is needed to fill into m_sName</param>
		/// <param name="_sRuntimeDesc"></param>
		/// <returns></returns>
		public static new CLogMsgBase CreateLogMsg( int _MsgClassNo, bool _bNeedExplain, string _sRuntimeDesc )
		{
			if ( _MsgClassNo < CLogMsgMgrBase.MsgClsNo_LastPublicNo )
				return CLogMsgMgrBase.CreatePublicLogMsg(_MsgClassNo, _bNeedExplain, _sRuntimeDesc);

			switch ( _MsgClassNo )
			{
			case MsgClsNo_SdkExpired : 
				return new CLogMsgBase( 
					MsgClsNo_SdkExpired,
					CLogMsgBase.MsgType_Error,
					CLogMsgBase.MsgLevel_5,
					( _bNeedExplain ? "Cyslon SDK is expired. Please connect to www.cyslon.com for new licence. Thanks " : null),
					_sRuntimeDesc );
			case MsgClsNo_SdkLastNo : 	
				return new CLogMsgBase( 
					MsgClsNo_SdkLastNo,
					CLogMsgBase.MsgType_Error,
					CLogMsgBase.MsgLevel_5,
					( _bNeedExplain ? "SdkLastNo. The max class nomber that is currently used." : null),
					_sRuntimeDesc );
			case MsgClsNo_SdkEndNo : 		
				return new CLogMsgBase( 
					MsgClsNo_SdkEndNo,
					CLogMsgBase.MsgType_Error,
					CLogMsgBase.MsgLevel_5,
					( _bNeedExplain ? "The Last class nomber which is remained for SDK. Developers should use larger number for your own messages." : null),
					_sRuntimeDesc );
			}//switch ( _MsgClassNo )

			return null;
		}//CreateSdkLogMsg
	
	}//class SdkLogMsgMgr
}//namespace
