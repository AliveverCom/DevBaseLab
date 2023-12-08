/// <History>
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-02-05  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
	/// <summary>
	/// GLogger : Global logger for program process wide
	/// static logger
	/// </summary>
	public class GLog
	{
		protected static CLogger s_Logger;

		CLogger GetLogger()
		{
			return s_Logger;
		}

		static GLog()
		{
			s_Logger = new CLogger();
		}

		/// <returns> previous logger </returns>
		public static CLogger SetLogger( CLogger _logger )
		{
			CLogger tpLogger = s_Logger;

			s_Logger = _logger;
			return tpLogger;
		}

		public static void Write( string _sLogStr )
		{
			s_Logger.Write( _sLogStr );
		}

		public static void Write( short _nLogLevel ,string _Format , params object[] _Objs )
		{
			//s_Logger.Write( _bWarrning , _Format_sLogStr );
			s_Logger.Write( _nLogLevel , _Format , _Objs );
		}

		public static void Write(string _sLogPostion, short _nLogLevel ,string _Format , params object[] _Objs )
		{
			//s_Logger.Write( _sLogPostion,_bWarrning , _sLogStr );
			s_Logger.Write( _sLogPostion, _nLogLevel ,  _Format , _Objs  );
		}

	}//class GLog
}//namespace 
