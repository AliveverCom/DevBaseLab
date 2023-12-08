/// <History>Alivever.Com.DevBasic.BasicLib.
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-02-05  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
	/// <summary>
	/// GMLog 的摘要说明。
	/// </summary>
	public class GMLog
	{
		protected static MLog s_MLog =new MLog();

		static GMLog()
		{
			//s_MLog = new MLog();
		}

		public static CLogger At(string _sName )
		{
			return s_MLog[_sName];
		}

		public static void AddLogger( string _sKey , CLogger _oLogger )
		{
			s_MLog.AddLogger( _sKey , _oLogger );
		}

	}
}
