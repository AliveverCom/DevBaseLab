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
	/// EDbDataType �����趨��ֵ���ٲ������ı䣬���򽫷�����̬���ݿ���ҡ�
	/// </summary>
	public enum EDbDataType
	{
		Unknow = 0 ,
		Number = 10 ,
		Float  = 20 ,
		Char   = 30 ,
		VarChar = 40 ,
		Date = 50 ,
		Time = 60 ,
		DateTime = 70 ,
		Text = 80 ,
		Binary = 90 ,
		Boolean = 100, 
	}//enum EDbDataType
}//namespace
