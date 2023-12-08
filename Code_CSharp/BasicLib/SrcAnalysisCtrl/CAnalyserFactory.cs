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
	/// CAnalyserFactory 的摘要说明。
	/// </summary>
	public class CAnalyserFactory
	{
		protected static CAnalyserFactory m_Ins = null;
		public ESrcLanguage m_eLanguage = ESrcLanguage.Text;
		bool m_bSetInstanceWhenGetting = true;

		public static  CAnalyserFactory Ins
		{
			get
			{ 
				if ( m_Ins == null )
					m_Ins = new CAnalyserFactory();
				return m_Ins;
			}
		}

		public CAnalyserFactory( ESrcLanguage _eLanguage )
		{
			m_eLanguage = _eLanguage;
		}

		public CAnalyserFactory( )
		{
		}

		public CCmdStrHelper GetCmdStrHelper()
		{
			CCmdStrHelper pRst ;

			switch ( m_eLanguage )
			{
			case ESrcLanguage.VB :
				pRst = new CCmdStrHelper_VB();
				break;
			case ESrcLanguage.CPlusPlus :
				pRst = new CCmdStrHelper_CPP();
				break;
			default:
				pRst = new CCmdStrHelper();
				break;

			}//switch
			if (  m_bSetInstanceWhenGetting
				&& CCmdStrHelper.Inc != null)
			{
				CCmdStrHelper.Inc = pRst;
			}

			return pRst;
		}//GetCmdStrHelper()

		public CCmdStrSgmtItr GetCmdStrSgmtItr()
		{
			switch ( m_eLanguage )
			{
				case ESrcLanguage.VB :
					return  new CCmdStrSgmtItr_VB();
				case ESrcLanguage.CPlusPlus :
					return  new CCmdStrSgmtItr_CPP();
				default:
					return  new CCmdStrSgmtItr();
			}//switch

		}//GetCmdStrHelper()

		public CCmdStrSgmtItr GetCmdStrSgmtItr(string _srcStr )
		{
			switch ( m_eLanguage )
			{
				case ESrcLanguage.VB :
					return  new CCmdStrSgmtItr_VB(_srcStr);
				case ESrcLanguage.CPlusPlus :
					return  new CCmdStrSgmtItr_CPP(_srcStr);
				default:
					return  new CCmdStrSgmtItr(_srcStr);
			}//switch

		}//GetCmdStrHelper()


	}//class CAnalyserFactory
}
