///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-11-17</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>
using System;

namespace Alivever.Com.DevBasic.BasicLib.ToolsCtrl
{
	/// <summary>
	/// CTimePair 的摘要说明。
	/// </summary>
	public class CTimePair
	{
		public CTime  m_bTime = new CTime() ; //begin time or the smaller one.
		public CTime  m_eTime = new CTime(); //end time or the larger one.

		public CTimePair( UInt32 _time1, UInt32 _time2 )
		{
			if ( _time1 < _time2 )
			{
				m_bTime.Set( _time1 );
				m_eTime.Set( _time2 );
			}
			else
			{
				m_bTime.Set( _time2);
				m_eTime.Set( _time1);
			}
		}//CTimePair(2)

		public CTimePair( CTime _time1, CTime _time2 )
		{
			if ( _time1 < _time2 )
			{
				m_bTime.Set( _time1 );
				m_eTime.Set( _time2 );
			}
			else
			{
				m_bTime.Set( _time2);
				m_eTime.Set( _time1);
			}
		}//CTimePair(2)

		public CTimePair( long _time1, long _time2 ) 
			: this( CTimeHelper.ToTime32(_time1) , CTimeHelper.ToTime32(_time2) )
		{

		}

		/// <summary>
		/// To test is _testTime is between this time pair
		/// </summary>
		/// <param name="_testTime"></param>
		/// <returns></returns>
		public bool IsInRange( long _testTime)
		{
			return m_bTime.GetUInt32() <= _testTime && m_eTime.GetUInt32() >= _testTime;
		}

		public long GetInterval()
		{
			return m_eTime.GetUInt32() - m_bTime.GetUInt32();
		}

	}//class CTimePair
}//namespace
