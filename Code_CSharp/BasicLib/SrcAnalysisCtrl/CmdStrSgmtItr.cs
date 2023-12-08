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
	/// Cmd Str Segment Itr
	/// </summary>
	public class CCmdStrSgmtItr
	{
		protected string m_SrcStr;
		protected string m_CrrSegment ="";

		protected int m_nCrrIdx = 0 ;

		public CCmdStrSgmtItr( string _SrcStr )
		{
			m_SrcStr = _SrcStr;
			ToNextSegment();
		}

		public CCmdStrSgmtItr()
		{
		}

		public bool IsStringSegment()
		{
			return (   m_CrrSegment!=null 
				    && m_CrrSegment.Length != 0 
					&& m_CrrSegment[0] != '\"' );
		}


		/// <summary>
		/// Note : All status will be reset when value is reset .
		/// </summary>
		public string SrcStr
		{
			set { ClearStatus() ; m_SrcStr = value; ToNextSegment();}
			get { return m_SrcStr; }
		}

		protected void ClearStatus()
		{
			m_CrrSegment ="";
			m_nCrrIdx = 0 ;
		}

		public override string ToString()
		{
			return m_CrrSegment ;
		}

//		public void operator ++()
//		{
//			
//		}

		/// <summary>
		/// the same function with operator ++
		/// </summary>
		public virtual void ToNextSegment()
		{
			int js , js_max = m_SrcStr.Length;
			bool bInStr = false;

			m_CrrSegment = "";

			// at end of string
			if ( m_nCrrIdx >= js_max)
			{
				m_CrrSegment = null;
				return ;
			}

			if ( m_SrcStr[m_nCrrIdx] == '\"' )
				bInStr = true ;

			for( js = m_nCrrIdx; js < js_max ; ++js )
			{
				if ( m_SrcStr[js] == '\"' && js != m_nCrrIdx )
				{
					if ( !bInStr )
						break;
					else
					{
						m_CrrSegment += m_SrcStr[js];
						break;
					}
				}
				m_CrrSegment += m_SrcStr[js];

			}

			m_nCrrIdx = js;

			if ( bInStr )
				++m_nCrrIdx ;

		}//ToNextSegment()

	}//class CmdStrItr
}//namespace
