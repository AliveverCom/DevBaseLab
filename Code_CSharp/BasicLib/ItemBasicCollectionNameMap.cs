/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2005-02-03 </ChangeDate>
///     <Description> </Description>
/// </History>
/// 
using System;
using System.Collections;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// ItemBasicCollection 的摘要说明。
	/// </summary>
	public class CItemBasicCollectionNameMap:ICollection
	{
		/// <summary>
		/// if "this[ string _sName]" is Case Sensitive
		/// </summary>
		bool m_bIdxCaseSensitive = false;

		/// <summary>
		/// map[ _nID , CItemBasic]
		/// </summary>
		protected Hashtable  m_Items;

		/////////////Begin IEnumerator //////////////////////
		public IEnumerator GetEnumerator()
		{
			return m_Items.Values.GetEnumerator();
		}
		/////////////End IEnumerator //////////////////////

		/////////////Begin ICollection //////////////////////
		public int Count { get{ return m_Items.Count ; } }

		public bool IsSynchronized { get{ return false; } }

		public object SyncRoot {get{ return this ; }}

		public void CopyTo( Array array, int index )
		{
			m_Items.CopyTo( array , index);
		}
		/////////////End ICollection //////////////////////
		
		/////////////Begin Initializtion //////////////////////

		public CItemBasicCollectionNameMap()
		{
			m_Items = new Hashtable() ;
		}

		public CItemBasicCollectionNameMap( Hashtable _Indexes )
		{
			Reset( _Indexes );
		}

		public CItemBasicCollectionNameMap( ArrayList _Indexes )
		{
			Reset( _Indexes );
		}

		public void Reset( Hashtable _Indexes )
		{
			m_Items = _Indexes;
		}

		public void Reset( ArrayList _Indexes )
		{
			foreach( CItemBasic crrIdx in _Indexes )
			{
				Add( crrIdx );
			}
		}

		public void Add( CItemBasic _newIdx )
		{
			m_Items.Add( _newIdx.Name, _newIdx );
		}

		public virtual void Add( int _nID , string _sName )
		{
			m_Items.Add( _sName, new CItemBasic(_nID, _sName) );
		}

		/////////////End Initializtion //////////////////////

		/////////////Begin deconstructuration //////////////////////
		public void Clear()
		{
			m_Items.Clear();
		}

		/////////////End deconstructuration //////////////////////

		/// <summary>
		/// This IndexMgr only provide readonly method.
		/// </summary>
		/// <example>
		/// If you want to replace object, please use the following codes.
		///		m_Items.Remove( this[_sName].m_sName );
		///		m_Items.Add( ((CItemBasic)value).m_sName , value );
		///		
		/// If you and to reset object's value, you may use the following codes.
		///		CItemBasic oItem = this[ _sName];
		///		oItem = oNewObject;
		///	Note: Maybe you should override Operator'=' for deep copy.
		/// </example>
		public virtual CItemBasic this[ string _sName] 
		{
			get
			{
				if ( m_Items.ContainsKey(_sName) )
					return (CItemBasic)m_Items[_sName];

				if ( !m_bIdxCaseSensitive )
					return null;

				foreach( CItemBasic crrItem in m_Items )
				{
					if ( crrItem.Name.ToLower() == _sName )
						return crrItem;
				}

				return null;
			}
//			set
//			{
//				m_Items.Remove( this[_sName].m_sName );
//				m_Items.Add( ((CItemBasic)value).m_sName , value );
//			}
		}//this[ string _sName] 


	}//class CItemBasicCollection
}//namespace
