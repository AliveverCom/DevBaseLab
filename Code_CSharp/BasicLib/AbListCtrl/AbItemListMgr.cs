using System;
using System.Collections;

namespace Alivever.Com.DevBasic.BasicLib.AbListCtrl
{
	/// <summary>
	/// AbItemListMgr 的摘要说明。
	/// </summary>
	public class CAbItemListMgr 
	{
		/// <summary>
		/// vector(CAbItemBase), sorted by CAbItemBase.m_AccAbKey.
		/// from min to max.
		/// </summary>
		ArrayList m_AbItems = null; 

		public CAbItemListMgr()
		{
			new ArrayList();
		}

		public CAbItemListMgr( ArrayList _AbItems )
		{
			new ArrayList();
		}

		public void SetItems( ArrayList _AbItems )
		{
			m_AbItems = _AbItems;
		}

		public void Clear()
		{
			m_AbItems.Clear();
		}

		public void AddItem( CAbItemBase _newItem )
		{
			int js , js_max = m_AbItems.Count;
			bool bInserted = false;

			for ( js = 0 ; js < js_max ; ++js )
			{
				if ( _newItem.m_AccAbKey.CompareTo(((CAbItemBase)m_AbItems[js]).m_AccAbKey) <= 0 )		
				{
					m_AbItems.Insert( js , _newItem );
					bInserted = true ;
					break;
				}
			}

			if ( ! bInserted)
				m_AbItems.Add( _newItem );

		}//AddItem

		public ArrayList GetItemsByAccAbKey( string _AccAbKey )
		{
			ArrayList aList = new ArrayList();
			int js , js_max = m_AbItems.Count;
			bool bFoundKey = false;
			int nIdx ;

			for ( js = 0 ; js < js_max ; ++js )
			{
				nIdx = ((CAbItemBase)m_AbItems[js]).m_AccAbKey.IndexOf( _AccAbKey );
				if ( nIdx == 0 )
				{
					bFoundKey = true;
					aList.Add( m_AbItems[js] );
				}
				else if ( bFoundKey )
					break;
			}

			return aList;
		}//GetItemsByAccAbKey(1)

		public ArrayList GetItemsByStartName( string _SubName )
		{
			ArrayList aList = new ArrayList();
			int js , js_max = m_AbItems.Count;
			bool bFoundKey = false;
			int nIdx ;

			for ( js = 0 ; js < js_max ; ++js )
			{
				nIdx = ((CAbItemBase)m_AbItems[js]).m_AccAbKey.IndexOf( _SubName );
				if ( nIdx == 0 )
				{
					bFoundKey = true;
					aList.Add( m_AbItems[js] );
				}
				else if ( bFoundKey )
					break;
			}

			return aList;
		}//GetItemsByAccAbKey(1)

	}//class CAbItemListMgr
}//namespace
