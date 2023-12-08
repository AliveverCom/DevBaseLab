/// <History>
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-08-12  </ChangeDate>
///     <Description> </Description>
/// </History>
/// 
using System;

namespace Alivever.Com.DevBasic.BasicLib.SrcAnalysisCtrl
{
	/// <summary>
	/// CmdStrHelper 的摘要说明。
	/// </summary>
	public class CCmdStrHelper
	{
		static CCmdStrHelper m_Inc = null;

		public static CCmdStrHelper Inc
		{
			get{ return m_Inc; }
			set{ m_Inc = value; }
		}

		/// <summary>
		/// FullName: Get String Connection symbol
		/// </summary>
		/// <param name="_eSrcLanguage"></param>
		public virtual char GetStrConnsymbol()
		{
			return '+';

//			switch ( _eSrcLanguage )
//			{
//			case ESrcLanguage.CSharp :
//				return '+';
//			case ESrcLanguage.Java :
//				return '+';
//			case ESrcLanguage.VB :
//				return '&';
//			default:
//				return (char)1;
//			};//switch ( _eSrcLanguage )
		}

		/// <summary>
		/// For C#: "aa"+"bb" --> "aabb"
		/// </summary>
		/// <param name="_srcStr"></param>
		/// <returns> Return result string </returns>
		public string  CombineCmdString( string _srcStr )
		{
			string rstStr = "";

			string sPreStr = "";
			bool bPreSegmentStr = false; //if previous segment is string statues.
			bool bPrePreSegmentStr = false;

			CCmdStrSgmtItr strItr = CAnalyserFactory.Ins.GetCmdStrSgmtItr( _srcStr );
			for(;  strItr.ToString() != null ; strItr.ToNextSegment() )
			{
				//System.Console.WriteLine( strItr );

				// if: "something" something "somthing" 
				if (    bPrePreSegmentStr
					&&  ! bPreSegmentStr
					&&  strItr.IsStringSegment() )
				{
//					if ( bPreSegmentStr == false )
//						int i =0;
					string tpStr = ToMinBlankStr( sPreStr , false );

					//if: "+" or " +" or " + " or "+ "
					if (   ( tpStr.Length ==1 && tpStr[0] == this.GetStrConnsymbol() )
						|| ( tpStr.Length ==2 && tpStr[0] == this.GetStrConnsymbol() )
						|| ( tpStr.Length ==2 && tpStr[0] == this.GetStrConnsymbol() )
						|| ( tpStr.Length ==3 && tpStr[0] == this.GetStrConnsymbol() ) 
					   )
					{
						tpStr = strItr.ToString();
						sPreStr = tpStr.Substring( 1, tpStr.Length-1) ;

						rstStr.Remove( rstStr.Length-1 , 0 );
						rstStr += sPreStr;
						continue;
					}//if: "+" or " +" or " + " or "+ "

				}// if: "something" something "somthing"

					rstStr += sPreStr;
					sPreStr = strItr.ToString();
				
			}//for  strItr

			rstStr += sPreStr;

			return rstStr;
		}//CombineCmdString

		/// <summary>
		/// test pairs in a string. 
		/// Function will immediately return -1 ewhen _pairEnd can't find a perpor _pairStart.
		/// </summary>
		/// <param name="_testStr"></param>
		/// <param name="_pairStart"></param>
		/// <param name="_pairEnd"></param>
		/// <returns>
		/// Return the pair sum of the string.
		/// value == 0 means OK.
		/// value > 0 means _pairStart is more then _pairEnd when the first error is found.
		/// </returns>
		/// <code>
		/// TestPair("{{}}",'{','}') = 0 ; 
		/// TestPair("{}}",'{','}') = -1 ;
		/// TestPair("\" a \" \" ",'\"','\"') = 1 ;
		/// </code>
		public virtual int TestPair( 
			string _testStr,
			char _pairStart,
			char _pairEnd)
		{
			short nLayerSum = 0;
			int js_max = _testStr.Length;
			bool bDiffPairSymble = ! (_pairStart == _pairEnd);

			for( int js = 0 ; js < js_max ; ++js )
			{
				if ( bDiffPairSymble )
				{
					if ( _testStr[js] == _pairStart )
						++ nLayerSum;
					else if (_testStr[js] == _pairEnd )
						-- nLayerSum;

					if ( nLayerSum < 0 )
						return nLayerSum;
				}
				else //_pairStart == _pairEnd
				{
					if ( _testStr[js] == _pairStart )
						if ( nLayerSum == 0 )
							nLayerSum = 1;
						else
							nLayerSum = 0;
				}//if ( bDiffPairSymble )
			}//for
			
			return nLayerSum;
		}//TestPair(3)

		public char[] GetStringSymble()
		{
			char[] aaa = {'\"', '\''};
			return aaa;
		}

		public virtual string RemoveComments( string _srcStr )
		{
			return _srcStr;
		}//RemoveComments_vb(1)

		/// <summary>
		/// remove redandent Blank and Table keys from _srcStr.
		/// For example: _srcStr = a +    b; ---> a + b
		/// </summary>
		/// <param name="_srcStr">source string that should be changed.</param>
		/// <param name="_bIncludeInStr">
		/// Shall we also remove redandent blank when it is a string? 
		/// For example:if true: _srcStr = "a +    b" ; ---> "a + b"
		///             if flase:_srcStr = "a +    b" ; ---> "a +    b"
		/// </param>
		/// <returns> return result string .</returns>
		public virtual string ToMinBlankStr( string _srcStr, bool _bIncludeInStr  )
		{
			string rstStr = "" ;
			string crrSegment ;
			bool bIsPreCharBlank = false ;//if prevous char = ' '

			CCmdStrSgmtItr strItr = CAnalyserFactory.Ins.GetCmdStrSgmtItr( _srcStr );

			for(;  strItr.ToString() != null ; strItr.ToNextSegment() )
			{
				crrSegment = strItr.ToString();

				if ( crrSegment[0] == '\"' && !_bIncludeInStr )
				{
					rstStr += crrSegment;
					continue;
				}

				bIsPreCharBlank = false ;
				foreach( char crrChr in crrSegment )
				{
					if ( crrChr == ' ' )
					{
						if ( bIsPreCharBlank )
							continue;
						else
						{
							bIsPreCharBlank = true;
						}
					}//if ( crrChr == ' ' )
					else 
						bIsPreCharBlank = false;

					rstStr += crrChr;
				}//foreach( char crrChr in _srcStr )

			}//for strItr;

			return rstStr;
		}//ToMinBlankStr

	}//class CmdStrHelper
}//namespace
