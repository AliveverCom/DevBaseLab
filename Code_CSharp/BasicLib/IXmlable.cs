using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// IAsyncError ��ժҪ˵����
	/// _eXmlType{ default=0, FileStyle=1, DBStyle=2}
	/// </summary>
	public interface IXmlable
	{
		string InitFromXml(char _eXmlType );
		string ToXml(char _eXmlType );
	}//interface IXmlable
}
