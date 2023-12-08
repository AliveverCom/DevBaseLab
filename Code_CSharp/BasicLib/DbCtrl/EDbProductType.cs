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
	/// <summary>
	/// EDbProductType 的摘要说明。
	/// </summary>
	public enum EDbProductType
	{
		Unknown, 

		//related DB 
		StandardSQL, 
		Oracle,
		DB2,
		MySQL,
		SQLserver,
		Access,
		FoxPro,
		ODBC,

		//non-related DB
		Cache,
		CQL
	}
}
