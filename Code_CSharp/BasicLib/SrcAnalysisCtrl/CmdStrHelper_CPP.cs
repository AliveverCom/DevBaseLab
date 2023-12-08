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
	/// CmdStrHelper_CPP 的摘要说明。
	/// </summary>
	public class CCmdStrHelper_CPP:CCmdStrHelper
	{
		public CCmdStrHelper_CPP()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// <summary>
		/// see description for ToMinBlankStr(3)
		/// 2004-10-14 CYShao : currently, this method is written for C++ style.
		/// if other langue is need , please rewrite it for more subfunctions.
		/// </summary>
		/// <param name="_srcStr"></param>
		/// <returns></returns>
		public override string ToMinBlankStr( string _srcStr, bool _bIncludeInStr  )
		{
			bool bIsPreCharBlank = false ,//if prevous char = ' '
				bIsInStr = false ,
				bIsPreChartrans = false; //if prevous char = @'\'
			string rstStr = "" ;

			foreach( char crrChr in _srcStr )
			{
				if ( crrChr == ' ' )
				{
					if ( bIsPreCharBlank && bIsInStr && _bIncludeInStr )
						continue;
					else
					{
						bIsPreCharBlank = true;
					}
				}//if ( crrChr == ' ' )
				else if ( crrChr == '\"' )
				{
					if ( bIsInStr )
					{
						if ( !bIsPreChartrans )
							bIsInStr = false;
					}
					else
						bIsInStr = true;
				}//else if ( crrChr == '\"' )
				else
				{
					if (  crrChr == '\\' && bIsInStr )
					{
						if ( bIsPreChartrans ) // if '\\'
							bIsPreChartrans = false;
						else 
							bIsPreChartrans = true;
					}
					else
						bIsPreChartrans =false;
				}//if (  crrChr == '\\')

				rstStr += crrChr;
			}//foreach( char crrChr in _srcStr )

			return rstStr;
		}//ToMinBlankStr

	}//class CmdStrHelper_CPP
}
