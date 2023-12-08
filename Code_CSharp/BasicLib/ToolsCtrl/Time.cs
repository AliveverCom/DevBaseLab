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
//using System.Object;

//using ShaoChenYe.DevFramework.BGTools;

namespace Alivever.Com.DevBasic.BasicLib.ToolsCtrl
{
	/// <summary>
	/// CDataTime 的摘要说明。
	/// </summary>
	public class CTime
	{

		public const UInt32 SecOfDay  = 24*60*60; 

		public const UInt32 SecOfHour = 60*60; 


		UInt32 m_nTime = 0;

		public CTime(  )
		{
			
		}

		public CTime( Int64 _nTime )
		{
			m_nTime = CTimeHelper.ToTime32( _nTime );
		}

		public CTime( UInt32 _nTime)
		{
			m_nTime = _nTime;
		}

		public UInt32 GetUInt32()
		{	
			return m_nTime;
		}

		public Int64 GetTime64()
		{	
			return CTimeHelper.ToTime64( m_nTime );
		}

		public string DBDataTimeStr()
		{
			DateTime dt= new DateTime( GetTime64() );

			return dt.ToString("yyyy-MM-dd ") + dt.ToString("TClass");
		}

		public CTime Set( UInt32 _nTime )
		{
			m_nTime = _nTime;
			return this;
		}

		public CTime Set( long _nTime )
		{
			m_nTime = CTimeHelper.ToTime32( _nTime );
			return this;
		}

		public CTime Set( string _stime )
		{
			m_nTime = UInt32.Parse( _stime );
			return this;
		}

		public CTime Set( CTime _otime )
		{
			m_nTime = _otime.m_nTime;
			return this;
		}

		public string DateTimeStr(short _nDateStyle , short _nTimeStyle )
		{
			return CTimeHelper.Long2DateTimeString( GetTime64() , _nDateStyle , _nTimeStyle);
		}

		public string DateStr( short _nDateStyle )
		{
			return CTimeHelper.Long2DateString( GetTime64() ,_nDateStyle );
		}

		public string TimeStr( short _nTimeStyle )
		{
			return CTimeHelper.Long2DateString( GetTime64() ,_nTimeStyle );
		}

		public override string ToString()
		{
			return DBDataTimeStr();
		}

		public static CTime FromDateTime( DateTime _datetime )
		{
			return new CTime( _datetime.ToFileTime() );
		}

		public static CTime FromFileTime( long _datetime )
		{
			return new CTime( _datetime );
		}

		public static CTime FromNow()
		{
			return FromDateTime( DateTime.Now );
		}

		public static UInt32  operator +( CTime _otime , UInt32 _ntime )
		{
			return _otime.m_nTime += _ntime;
			//return _otime;
		}

		public static bool  operator <( CTime _otime1 , CTime _otime2 )
		{
			return _otime1.m_nTime < _otime2.m_nTime;
		}

		public static bool  operator >( CTime _otime1 , CTime _otime2 )
		{
			return _otime1.m_nTime > _otime2.m_nTime;
		}

		public static bool  operator ==( CTime _otime1 , CTime _otime2 )
		{
			return _otime1.m_nTime == _otime2.m_nTime;
		}

		public bool  Equals( CTime _otime2 )
		{
			return this.m_nTime == _otime2.m_nTime;
		}

		public static bool  operator !=( CTime _otime1 , CTime _otime2 )
		{
			return _otime1.m_nTime != _otime2.m_nTime;
		}

	}//class CDataTime
}//namespace
