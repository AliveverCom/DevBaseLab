/// <History>
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-02-05  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
	/// <summary>
	/// GSdkLog the global log of cyshao C# SDK
	/// </summary>
	public class GSdkMLog : GMLog
	{
		public static string s_DftSDKPathName = "" ;
		public static string s_DftSDKFileName = "CyslonSdkLog" ;

		//public static bool s_bNeedPackage

		static GSdkMLog() 
		{
			/*
			CLogger lgr = new CLogger ( s_FileName , s_PathName );
			lgr.m_bNeedPackageName = true;
			lgr.m_bNeedDateTime    = true;
			lgr.m_PackageName = "AppUserCtrl";

			s_MLog.AddLogger( "AppUserCtrl" , lgr );
			*/
			CLogger lgr = new CLogger ( s_DftSDKFileName , s_DftSDKPathName );
			lgr.m_bNeedPackageName = true;
			lgr.m_bNeedDateTime    = true;
			lgr.m_PackageName = s_DftSDKFileName;

			s_MLog.AddLogger( s_DftSDKFileName , lgr );

		}

		public static new CLogger At(string _sName )
		{
			CLogger tpLoger = s_MLog[_sName];

			if ( tpLoger == null )
			{
				tpLoger = new CLogger ( s_DftSDKFileName , s_DftSDKPathName );
				tpLoger.m_bNeedPackageName = true;
				tpLoger.m_bNeedDateTime    = true;
				tpLoger.m_PackageName = _sName;

				s_MLog.AddLogger( _sName , tpLoger );
				tpLoger.Write("GSdkMLog.At(1)" , 2 , "Can't Find Logger '"+ _sName +"'. Auto create this logger.\n");
			}

			return tpLoger;
		}

		public static void SetDftSDKFileNamePath( string _sFileName, string _sPath )
		{
			CLogger lgr = s_MLog[s_DftSDKFileName];
			lgr.m_FileName = _sFileName;
			lgr.m_FilePath = _sPath;

			s_DftSDKFileName = _sFileName ;
			s_DftSDKPathName = _sPath ;

		}
	}
}
