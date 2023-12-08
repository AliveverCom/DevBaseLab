/// <History>
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-02-05  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;
using System.IO;
using System.Diagnostics;

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
	/// <summary>
	/// LogItem 的摘要说明。
	/// </summary>
	public class CLogger
	{
		//Default means no fileNameSubfix.
		private enum ELogType 
		{ 
			e_Default = 99 ,
			e_Warning = 2 ,   //Write Log only.
			e_Error   = 1,    //Debug.Assert , Write Log.
			e_Critical= 0     //Track.Assert , Write Log.
		};

		private const string sc_sWarning  = "Warning";
		private const string sc_sError    = "Error";
		private const string sc_sCritical = "Critical";

		public static string s_FileSuffix = "log";
		public static char s_DirSeporater = '\\';

		//private FileStream m_FStream = null;

		public string m_FilePath   = "" ;
		public string m_FileName   = "ProgramLogger";
		public string m_DftPrefix  = "" ;
		public string m_PackageName= "" ;

		/// <summary>
		/// file will be clear and overwrit when m_nCrrItems == m_nMaxItems
		/// m_nMaxItems less then 0 meanse always appand , don't clear.
		/// </summary>
		public long   m_nMaxItems  = -1;
		public long   m_nCrrItems  = 0 ;

		////////Log file name type
		/// <summary>
		/// True = "FileName_Warning ,FileName_Error"
		/// False = "FileName"
		/// </summary>
		//public bool   m_bMultifile  = false ; 
		public int    m_nMaxFileSize = 1000000; //1MB

		/////////log context format
		//public bool   m_bKeepFileOpen    = false ;
		//public bool   m_bMonopolizeFile  = false ;
		//public bool   m_bDftAsWarning    = true  ;
		public bool   m_bShowAll         = false ; // default Log detail level, false = e_sample
		public bool   m_bNeedDateTime    = false ;
		public bool   m_bNeedProgramID   = false ;
		public bool   m_bNeedPackageName = false ;
		public bool   m_bNeedLogLevel    = true  ;

        //private string ELogType2Str( ELogType _eLogType )
        //{
        //    switch( _eLogType )
        //    {
        //        case ELogType.e_Critical :
        //            return sc_sCritical;
        //        case ELogType.e_Error   :
        //            return sc_sError;
        //        case ELogType.e_Warning   :
        //            return sc_sWarning;
        //        //case ELogType.e_Default   :
        //        default:
        //            return "";
        //    };//switch( _eLogType )
        //}

		public CLogger( )
		{

		}

		public CLogger( string _sFileName , string _FilePath )
		{	
			m_FileName = _sFileName;
			m_FilePath = _FilePath ;

		}

		/*
		private FileStream GetFStream()
		{
			
			if ( m_bKeepFileOpen)
			{
				if ( m_FStream == null )
					OpenFileStream();

				return ;
			}
			

			OpenFileStream();

		}

		private FileStream GetFileStream( bool _bWarrning )
		{
			try
			{
				ELogType _eLogType = Bool2LogType(_bWarrning) ;

				return new FileStream( GetLogFileName( _eLogType ) ,
											FileMode.Append | FileMode.OpenOrCreate,
											FileAccess.Write ,
											FileShare.Read );

			}
			catch( Exception e )
			{
				return null;
			}

		}
		*/

		//public void Write( string _Format , params object[] _Objs )
		public void Write( string _Format  )
		{
			_Format = _Format.Replace("}", "\\}");
			_Format = _Format.Replace("{", "\\{");
			Write( "",ELogType.e_Default , _Format );
		}

		
		public void Write( string _sLogPostion, short _nLogLevel , string _Format , params object[] _Objs )
		{
			Write( _sLogPostion , LogLevelID2LogType(_nLogLevel) , _Format , _Objs);
		}

		public void Write( short _nLogLevel , string _Format , params object[] _Objs )
		{
			Write( "",LogLevelID2LogType(_nLogLevel) , _Format , _Objs);
		}

		private ELogType LogLevelID2LogType( short _nLogLevel )
		{
			//if ( ! m_bMultifile  )
			//	return ELogType.e_Default ;

			switch ( _nLogLevel )
			{
				case (short)ELogType.e_Critical :
					return ELogType.e_Critical;

				case (short)ELogType.e_Error :
					return  ELogType.e_Error;

				case (short)ELogType.e_Warning :
					return  ELogType.e_Warning;
				default:
					return ELogType.e_Default ;
			};
		}

		private void CheckAndCleanFile( string _sFileName )
		{
			FileInfo fi = new FileInfo( _sFileName );

			if ( ! fi.Exists )
				return ;

			bool bNeedClean = false;
			string sCleanlog = "";

			DateTime.Now.ToString("u");
			string timeStr = "["+ DateTime.Now.ToString("u") +"]" ;

			if ( m_nMaxFileSize <= fi.Length )
			{
				sCleanlog += timeStr + " LogFile is cleaned because \"size\" is bigger them configured.\n";
				m_nCrrItems = 0;
				bNeedClean = true;
			}

			if ( m_nMaxItems > 0 && m_nCrrItems >= m_nMaxItems)
			{	
				sCleanlog += timeStr + " LogFile is cleaned because \"Items\" is bigger then configured.\n";
				m_nCrrItems =0;
				bNeedClean = true;
			}

			sCleanlog += "====================================================================================\n";

			if ( !bNeedClean )
				return;
			/////begin to clean file
			FileStream fs =  new FileStream( _sFileName ,
											FileMode.Truncate ,
											FileAccess.Write ,
											FileShare.Read );
			if ( fs == null )
				return ;

			StreamWriter sw = new StreamWriter( fs );
			sw.Write( sCleanlog );
			sw.Close();
			fs.Close();
		}

		//public void Write(string _sLogPostion, bool _bWarrning , string _sLogStr )
		//{
		//	Write( _sLogPostion,Bool2LogType(_bWarrning) , _Objs);
		//}

		private void Write( string _sLogPostion, ELogType _eLogType , string _Format , params object[] _Objs )
		{
			//string tpStr = ""; 
			
			string fName = GetLogFileName( _eLogType );
			CheckAndCleanFile(fName);

			FileStream fs =  new FileStream( fName ,
											FileMode.Append ,
											FileAccess.Write ,
											FileShare.Read );
			if ( fs == null )
				return ;

			if ( _sLogPostion == null )
				_sLogPostion = "";

			if (_sLogPostion != "" )
				_sLogPostion = "[" + _sLogPostion + "]";

			string sContext = "";
			if ( _Objs == null || _Objs.Length == 0 )
				sContext = _Format;
			else
				sContext = string.Format( _Format , _Objs );
			//sContext.Replace("\n" , "\r\n");
			//////////////prepair string
			string logStr = GetLineHeaderStr(_eLogType) + _sLogPostion;
			if ( logStr.Length == 0 )
				logStr = sContext;
			else
				logStr +=  " :" + sContext;

			switch( _eLogType )
			{
				case ELogType.e_Critical :
					Trace.Assert( false , logStr );
					break;
				//case ELogType.e_Error   :
				//	Debug.Assert( false, logStr );
				//	break;
				default:	
					Debug.Write( logStr );
					break;
			};//switch( _eLogType )

			///////write file
			//byte aaa[] = new byte[5]( (byte)'D',(byte)'E',(byte)'m',(byte)'0',(byte)'\n' );
			//byte[] aaa = { (byte)'D',(byte)'E',(byte)'m',(byte)'0',(byte)'\n' };
			//fs.Write( aaa , 0 , 5 );
			//fs.Write( tpStr , 0 , tpStr.Length );
			StreamWriter sw = new StreamWriter( fs );
			sw.Write( logStr );
			sw.Close();
			fs.Close();

			++m_nCrrItems;
		}

		private string GetLineHeaderStr( ELogType _eLogType )
		{
			string tpStr = "";
			if ( m_bNeedDateTime )
			{
				DateTime.Now.ToString("u");
				tpStr += "["+ DateTime.Now.ToString("u") +"]" ;
			}

			if ( m_bNeedLogLevel )
			{
                string ltStr = _eLogType.ToString();//ELogType2Str(_eLogType);
				if ( ltStr.Length != 0 )
					tpStr += "["+ ltStr +"]";
			}

			if ( m_bNeedProgramID )
				tpStr += "[ProgramID]" ;

			if ( m_bNeedPackageName )
				tpStr += "["+m_PackageName+"]";

			return tpStr;
		}

		private string GetLogFileName( ELogType _eLogType )
		{
			string sFName = m_FileName;

			/*
			if ( _eLogType == ELogType.e_Warning )
				sFName += sc_sWarning;
			else if ( _eLogType == ELogType.e_Error )
				sFName += sc_sError;
			*/

			sFName += "." + s_FileSuffix;

			if ( m_FilePath.Length == 0 )
				return sFName;
			else 
				return m_FilePath + s_DirSeporater + sFName;
		}

		/// <summary>
		/// Only for outside invoker.
		/// </summary>
		/// <returns> "/path/MyFile*.log" </returns>
		public string GetLogFileName( )
		{
			string sFileName = m_FileName + "*." + s_FileSuffix;

			if ( m_FilePath.Length == 0 )
				return sFileName;
			else 
				return m_FilePath + s_DirSeporater + sFileName;
		}
	}
}
