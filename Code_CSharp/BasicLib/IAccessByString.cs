using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// IAccessByString 的摘要说明。
	/// </summary>
	public interface IAccessByString
	{
		bool InitFromString(string _initStr) ;

		string ToInitString();
		
	}//interface IAccessByString
}//namespace
