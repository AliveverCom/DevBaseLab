/// <History>
///		<Devoloper> Shao Chen Ye </Devoloper>
///		<ChangeDate> 2005-02-05  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;
using System.Collections;
//using Alivever.Com.DevBasic.BasicLib.;

namespace Alivever.Com.DevBasic.BasicLib.LogCtrl
{
	/// <summary>
	/// LogMgr 的摘要说明。
	/// MLog[1].Write("My log string.");
	/// </summary>
	public class MLog
	{
		Hashtable m_mLoger;

		public CLogger this [string _sKey]
		{
			get 
			{
				if ( m_mLoger.ContainsKey(_sKey) )
					return (CLogger)m_mLoger[ _sKey ]; 
				else
					return null;
			}			
		}

		public MLog()
		{
			m_mLoger = new Hashtable();
		}

		public void AddLogger( string _sKey  , CLogger _oLogger )
		{
			m_mLoger.Add( _sKey , _oLogger );
		}
	}
}//namespace
