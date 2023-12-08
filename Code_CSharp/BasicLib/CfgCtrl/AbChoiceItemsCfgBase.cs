using System;
using System.Collections;
using Alivever.Com.DevBasic.BasicLib.DbCtrl;

namespace Alivever.Com.DevBasic.BasicLib.CfgLib
{
	/// <summary>
	/// AbChoiceItemsCfg 的摘要说明。
	/// </summary>
	public abstract class CAbChoiceItemsCfgBase : IDbMgr
	{
		protected string m_LastError;

		public CAbChoiceItemsCfgBase()
		{
		}

		//public virtual EDbState GetDbState();

		public virtual string GetLastError()
		{
			return m_LastError;
		}

		public abstract EDbState GetDbState();

		public abstract Hashtable GetItemsByListID(int _nListID );

		public abstract int InsertItem( int _nListID ,ref CAbChoiceItem _newItem );

		public abstract bool GetMaxIdOfList( int _nListID , ref int _MaxID );

	}//
}
