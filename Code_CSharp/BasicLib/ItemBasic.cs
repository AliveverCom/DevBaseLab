using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// ItemBasic 的摘要说明。
	/// </summary>
	public class CItemBasic : IItemBasic
	{
		/// <summary>
		/// The unique ID of an item. Usually to be auto-sequence No. .
		/// </summary>
		protected int  m_nID = -1;

		protected string m_sName;

		public virtual int nID
		{
			get{ return m_nID; }
			set{ m_nID = value; }
		}

		public virtual string Name
		{
			get{ return (m_sName==null?"": m_sName );}
			set{ m_sName = value; }
		}

		public CItemBasic()
		{
		}

		public CItemBasic( string _sName )
		{
			m_sName  = _sName ;
		}

		public CItemBasic( int _nID , string _sName )
		{
			m_nID    = _nID ;
			m_sName  = _sName ;
		}

		public virtual void SetValues( CItemBasic _newData )
		{
			m_nID = _newData.m_nID;
			m_sName = _newData.m_sName;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_nDeepness">
		/// How many layers Log should be follow.
		/// _nDeepness-- when passing each layer
		/// Stop at _nDeepness == 0
		/// </param>
		/// <param name="_bShowAll">
		/// detial level 
		/// False = e_Simple , True = e_All
		/// </param>
		/// <returns></returns>
		public virtual string ToLogString( bool _bShowAll ,string _sPrefix , string _sSuffix )//, short _nDeepness );
		{
			return _sPrefix + ToLogString( _bShowAll ) + _sSuffix;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// "m_nID , m_sName , m_sDesc"
		/// </returns>
		public virtual string LogItemTitals(  bool _bShowAll )
		{
			return "m_nID, m_sName";
		}

		public virtual string ToLogString( bool _bShowAll )
		{
			return "(" + m_nID + ",\"" + m_sName + ")";
		}

		public override string ToString()
		{
			return this.m_sName;
		}

		public virtual int IntValue
		{
			get{ return 0; }
		}

		public virtual string StrValue
		{
			get{ return  this.Name ; }
		}

		public virtual float FloatValue
		{
			get{ return (float)0;}
		}

		public virtual bool BoolValue
		{
			get{ return false ;}
		}

	}//class ItemBasic 
}//namespace
