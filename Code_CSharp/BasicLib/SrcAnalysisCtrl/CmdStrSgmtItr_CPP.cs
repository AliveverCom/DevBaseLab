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
	/// CmdStrItr_CPP 的摘要说明。
	/// </summary>
	public class CCmdStrSgmtItr_CPP : CCmdStrSgmtItr
	{
		public CCmdStrSgmtItr_CPP( string _SrcStr) : base( _SrcStr )
		{
		}

		public CCmdStrSgmtItr_CPP() : base()
		{}

		/// <summary>
		/// the same function with operator ++
		/// </summary>
		public override void ToNextSegment()
		{
			int js , js_max = m_SrcStr.Length;
			bool  bInStr = false ,
				  bPreChartrans = false; //if prevous char = @'\'

			m_CrrSegment = "";

			// at end of string
			if ( m_nCrrIdx == js_max)
				return ;

			if ( m_SrcStr[m_nCrrIdx] == '\"' )
				bInStr = true ;

			char crrChr;

			for( js = m_nCrrIdx; js < js_max ; ++js )
			{
				crrChr = m_SrcStr[m_nCrrIdx];
				if ( !bPreChartrans && crrChr == '\"' )
					break;
				else
				{
					if (  crrChr == '\\' && bInStr )
					{
						if ( bPreChartrans ) // if '\\'
							bPreChartrans = false;
						else 
							bPreChartrans = true;
					}
					else
						bPreChartrans =false;
				}//if (  crrChr == '\\')

				m_CrrSegment += crrChr;
			}

			if ( bInStr )
				++m_nCrrIdx ;
		}//ToNextSegment()

	}//class CmdStrItr_CPP
}//namespace
