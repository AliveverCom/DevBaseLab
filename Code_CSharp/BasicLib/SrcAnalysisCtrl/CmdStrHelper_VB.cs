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
	/// CmdStrHelper_VB 的摘要说明。
	/// </summary>
	public class CCmdStrHelper_VB :CCmdStrHelper
	{
		public CCmdStrHelper_VB()
		{
		}

		/// <summary>
		/// FullName: Get String Connection symbol
		/// </summary>
		/// <param name="_eSrcLanguage"></param>
		public override char GetStrConnsymbol()
		{
			return '&';
		}

	}//CmdStrHelper_VB
}//namespace
