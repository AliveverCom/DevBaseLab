/// <History>
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-08-12  </ChangeDate>
///     <Description> </Description>
/// </History>
/// 
using System;
using System.IO;
using Alivever.Com.DevBasic.BasicLib.FileSysCtrl;
using Alivever.Com.DevBasic.BasicLib.ToolsCtrl;

namespace Alivever.Com.DevBasic.BasicLib.SrcAnalysisCtrl
{
	/// <summary>
	/// FullName: Command Line Iterator Base
	/// </summary>
	public class CCmdLineItr //
	{
		protected CCmdLineItr m_Inc = null;

		public CCmdLineItr Inc
		{
			get{ return m_Inc ; }
			set{ m_Inc = value; }
		}

		StreamReader m_StrmReader = null ;

		/// <summary>
		/// if need run trim before return a line string
		/// </summary>
		bool m_bAutoTrim = true;
		bool m_bWantEmptyLine = false;

		public ESrcLanguage m_eSrcLanguage = ESrcLanguage.Text;

		public CCmdLineItr( string _StrBuff )
		{
		}

		public CCmdLineItr( CPathFileMgr _targetFile )
		{
			m_StrmReader = new StreamReader( _targetFile.ToString() );
		}

		public CCmdLineItr( StreamReader _StrmReader )
		{
			m_StrmReader = _StrmReader;
		}

		public virtual string GetCmdLine()
		{
			string crrLine , testLine, rstLine ="" ;
			bool bFinished =false , bInContinue=false;
	
			while ( !bFinished )
			{
				crrLine = m_StrmReader.ReadLine();

				// if get the end of stream
				if ( crrLine == null )
					return null;

				testLine = crrLine;

				testLine = testLine.TrimEnd(  );
				if ( !m_bWantEmptyLine && testLine.Length == 0 )
					if ( !bInContinue ) 
						continue;
					else
						break;

				//if not a continue line
				if ( ContinueKey != (char)1 
					&& testLine.LastIndexOf( ContinueKey) != testLine.Length-1)
				{
					bFinished = true ;
				}
				else
				{
					bInContinue = true;
					testLine = testLine.Substring( 0, testLine.Length-1 );
				}

				if ( m_bAutoTrim )
					if ( !bFinished )
					{	
						testLine = CStringHelper.ReplaceTrim( testLine," " ); 
						rstLine += testLine;
					}
					else
					{
						crrLine = CStringHelper.ReplaceTrim( crrLine," " ); 
						rstLine += crrLine;
					}
			}//while ( !bFinished )
			/*
			if ( m_bAutoTrim )
				crrLine.Trim();

			rstLine += crrLine;
			*/
			return rstLine;
		}

		public static char GetContinueKey( ESrcLanguage _eSrcLanguage )
		{
			switch ( _eSrcLanguage )
			{
				case ESrcLanguage.C :
					return '/';
				case ESrcLanguage.CPlusPlus :
					return '/';
				case ESrcLanguage.Perl :
					return '/';
				case ESrcLanguage.VB :
					return '_';
				default:
					return (char)1;
			};//switch ( _eSrcLanguage )
		}//GetContinueKey(1)

		public char ContinueKey
		{
			get{ return CCmdLineItr.GetContinueKey( m_eSrcLanguage ); }
		}
	}//class CCmdLineIterator
}//namespace
