using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// ItemBase 的摘要说明。
	/// </summary>
	public class CItemBase : CItemBasic
	{
		//Description
		protected string m_sDesc;

		public virtual string Desc
		{
			get{ return (m_sDesc==null?"": m_sDesc );}
			set{ m_sDesc = value; }
		}

		public CItemBase()
		{
		}

		public CItemBase( string _sName )
			: base (  _sName )
		{
		}

		public CItemBase( int _nID , string _sName )
			: base ( _nID ,  _sName )
		{
		}


		public CItemBase( int _nID , string _sName , string _sDesc )
			: base ( _nID ,  _sName )
		{
			m_sDesc  = _sDesc ;
		}

		public override string ToLogString( bool _bShowAll )
		{
			return "(" +base.ToLogString(_bShowAll)+ "\",\"" + m_sDesc + "\")";
		}

		public void SetValues( CItemBase _newData )
		{
			base.SetValues( _newData );
			m_sDesc = _newData.m_sName ;
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
		public override string ToLogString( bool _bShowAll ,string _sPrefix , string _sSuffix )//, short _nDeepness );
		{
			return _sPrefix + ToLogString( _bShowAll ) + _sSuffix;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// "m_nID , m_sName , m_sDesc"
		/// </returns>
		public override string LogItemTitals(  bool _bShowAll )
		{
			return base.LogItemTitals( _bShowAll) + ", m_sDesc";
		}

	}
}
