/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2005-02-05 </ChangeDate>
///     <Description> </Description>
/// </History>
/// 
using System;
using System.Collections;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// ItemBasicCollectionVector 的摘要说明。
	/// </summary>
	public class CItemBasicCollectionVector
	{

		/// <summary>
		/// map[ _nID , CItemBasic]
		/// </summary>
		protected ArrayList m_Items = null;

		public ArrayList Items
		{
			get
			{ 
				if ( m_Items == null )
					m_Items = new ArrayList();

				return m_Items;
			}
		}

		/// <summary>
		/// if "this[ string _sName]" is Case Sensitive
		/// </summary>
		bool m_bIdxCaseSensitive = false;


		/////////////Begin IEnumerator //////////////////////
		public IEnumerator GetEnumerator()
		{
			return m_Items.GetEnumerator();
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

		public CItemBasicCollectionVector()
		{
			m_Items = new ArrayList() ;
		}

		public CItemBasicCollectionVector( Hashtable _Indexes )
		{
			Reset( _Indexes );
		}

		public CItemBasicCollectionVector( ArrayList _Indexes )
		{
			Reset( _Indexes );
		}

		public void Reset( Hashtable _Indexes )
		{
			foreach( CItemBasic crrIdx in _Indexes )
			{
				Add( crrIdx );
			}
		}

		public void Reset( ArrayList _Indexes )
		{
			m_Items = _Indexes;
		}

		public void Add( CItemBasic _newItem )
		{
			m_Items.Add(  _newItem );
		}

		public virtual void Add( int _nID , string _sName )
		{
			m_Items.Add( new CItemBasic(_nID, _sName) );
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
		/// <Retrun> Vector[CItemBasic] </Retrun>
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
		public virtual ArrayList this[ string _sName] 
		{
			get
			{
				ArrayList itmV = new ArrayList();
				foreach( CItemBasic crrItr in m_Items)
				{
					if (   crrItr.Name == _sName 
						|| (   m_bIdxCaseSensitive 
							&& crrItr.Name.ToLower() == _sName.ToLower() ) )
						itmV.Add( crrItr );
				}

				return itmV;
			}
			//			set
			//			{
			//				m_Items.Remove( this[_sName].m_sName );
			//				m_Items.Add( ((CItemBasic)value).m_sName , value );
			//			}
		}//this[ string _sName] 

		public virtual CItemBasic this[ int _nID ]
		{
			get{ return (CItemBasic)m_Items[_nID] ; }
			set{  m_Items[_nID] = value; }
		} 

	}//class CItemBasicCollectionVector
}//namespace
