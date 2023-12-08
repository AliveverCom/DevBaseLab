using System;

namespace Alivever.Com.DevBasic.BasicLib.AbListCtrl
{
	/// <summary>
	/// AbItemBase 的摘要说明。
	/// CItemBase.m_nID -> AbItem ID
	/// CItemBase.m_sName -> AbItem Name string
	/// CItemBase.m_sDesc -> AbItem Desc./Tootip
	/// </summary>
	public class CAbItemBase : CItemBase
	{
		public string m_AccAbKey = ""; // acceleray ab. key
		
		public CAbItemBase()
		{
		}

		public CAbItemBase( int _nID , string _sName , string _AccAbKey, string _sDesc )
				: base( _nID, _sName, _sDesc )
		{
			m_AccAbKey = _AccAbKey;
		}

	}//class CAbItemBase
}//namespace
