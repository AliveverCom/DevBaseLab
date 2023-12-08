using System;
using System.Collections;

using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.CfgLib
{
	/// <summary>
	/// AbChoiceList 的摘要说明。
	/// </summary>
	public class CAbChoiceList : CItemBase
	{
		public string StrID;

		/// <summary>
		/// 字符串长度
		/// </summary>
		public int ValueLength = 0;

		public bool IsReadonly = false;

		public bool IsUseChoiceTag = false;

		private Hashtable m_AbItems = null ;

		/// <summary>
		/// 下属的选项是否可以被删除
		/// </summary>
		public 	bool CanItemBeDeleted = false;

		/// <summary>
		/// 默认被选中的项
		/// </summary>
		public CAbChoiceItem DefaultItem = null;


		public CAbChoiceList()
		{

		}

		public CAbChoiceList(int _nID , string _sName , string _sDesc)
					: base( _nID , _sName , _sDesc )
		{

		}

		public CAbChoiceList(string _StrID , string _sName , string _sDesc)
			: base( 0 , _sName , _sDesc )
		{
			this.StrID = _StrID;
		}

		public void SetItems( Hashtable _AbItems )
		{
			if ( m_AbItems != null )
				m_AbItems.Clear();

			m_AbItems = _AbItems;
		}

		public Hashtable GetItems()
		{
			return m_AbItems;
		}
		
	}//class CAbChoiceList
}//namespace 
