using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// IAccessByString ��ժҪ˵����
	/// </summary>
	public interface IAccessByString
	{
		bool InitFromString(string _initStr) ;

		string ToInitString();
		
	}//interface IAccessByString
}//namespace
