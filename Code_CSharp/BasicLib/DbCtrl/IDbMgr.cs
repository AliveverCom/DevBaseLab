/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2007-02-26  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;

namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{

	public enum EDbState
	{ 
		None ,     //m_DbConn==null, or ConnectDB() hasn't been invoked.
		Open ,
		Closed     // Closed or connected only.
	}//enum EDbState

	/// <summary>
	/// IDbMgr 的摘要说明。
	/// </summary>
	public interface IDbMgr : IAsyncErrInfo
	{

		EDbState GetDbState();

		//string GetLastError();
	
	}//interface IDbMgr
}//namespace
