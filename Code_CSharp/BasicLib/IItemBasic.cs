using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// IItemBasic 的摘要说明。
	/// </summary>
	public interface IItemBasic :  ILogable
	{
		int nID { get;set; }
		string Name { get;set; }
		//string Desc { get;set; }
	}//interface IItemBasic
}//namespace
