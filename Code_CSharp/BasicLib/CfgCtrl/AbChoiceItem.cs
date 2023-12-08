using System;
using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.CfgLib
{
	/// <summary>
	/// AbChoiceItem ��ժҪ˵����
	/// </summary>
	public class CAbChoiceItem : CItemBase
	{
		//��ʾ
		public string m_AccKey = "" ;
		
		public string Tag;

		public CAbChoiceItem()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_ID"></param>
		/// <param name="_Name">Display name</param>
		/// <param name="_AccKey"></param>
		/// <param name="_Desc"></param>
		public CAbChoiceItem( int _ID, string _Name, string _AccKey, string _Desc )
					: base( _ID, _Name,_Desc )
		{
			m_AccKey = _AccKey;
		}

	}//class AbChoiceItem
}//namespace
