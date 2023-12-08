///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-13-18</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>using System;

namespace Alivever.Com.DevBasic.BasicLib.FileSysCtrl
{
	/// <summary>
	/// EFileSysType 的摘要说明。
	/// </summary>
	public enum EFileSysType
	{
		UnixFile, MsFile, WinRes, WebURL
	}

	public enum EFileSysItemTpye
	{
		File = 1 ,
		Path = 2 ,

		/// <summary>
		/// this is only a value means EFileSysItemTpye.File | EFileSysItemTpye.Path
		/// it is only used in type algrothm. Not be as a type
		/// </summary>
		FileAndPath = 3 

	}//enum EFileSysItemTpyes

}//namespace
