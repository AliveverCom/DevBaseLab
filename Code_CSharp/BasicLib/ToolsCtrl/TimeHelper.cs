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
using System.Collections;
using Alivever.Com.DevBasic.BasicLib.LogCtrl;
using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.ToolsCtrl
{
	/// <summary>
	/// TimeHelper 的摘要说明。
	/// </summary>
	public class CTimeHelper
	{
		public enum ELastPairType
		{
			ExtendPair ,
			UseEndtime ,
			Discard 
		};

		/// <summary>
		/// _nStyle = 0 => 2004-03-02
		/// _nStyle = 1 => 2004年 03月 02日
		/// </summary>
		/// <param name="_nTime"></param>
		/// <param name="_nStyle"></param>
		/// <returns></returns>
		public static string  Long2DateString( long _nTime , short _nStyle )
		{
			DateTime oNow = DateTime.FromFileTime( _nTime );
			switch( _nStyle )
			{
				case 0 :
					return oNow.Year +"-"+oNow.Month+"-"+oNow.Day;
				case 1 :
					return oNow.Year + "年 " + oNow.Month + "月 " + oNow.Day   + "日";
				default:
					GSdkMLog.At( GT.pkgName).Write("CTimeHelper.Long2DateString(1)", 0 , "invalid _nStyle="+_nStyle+"\n");
					break;
			};
			return "";
		}//Long2DateString(2)

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_nTime">_nStyle=0  "hh:mm" , _nStyle=1  "hh:mm:ss" </param>
		/// <param name="_nStyle"></param>
		/// <returns></returns>
		public static string  Long2TimeString( long _nTime , short _nStyle )
		{
			DateTime oNow = DateTime.FromFileTime( _nTime );
			switch( _nStyle )
			{
				case 0 :
					return oNow.Hour +":"+ oNow.Minute ;
				case 1 :
					return oNow.Hour +":"+ oNow.Minute + ":" + oNow.Second ;
				default:
					GSdkMLog.At( GT.pkgName).Write("CTimeHelper.Long2TimeString(1)", 0 , "invalid _nStyle="+_nStyle+"\n");
					break;
			};
			return "";
		}//Long2DateString(2)

		public static string  Long2DateTimeString( long _nTime , short _nDateStyle , short _nTimeStyle )
		{
			return Long2DateString( _nTime , _nDateStyle ) + " " + Long2TimeString( _nTime , _nTimeStyle );
		}//Long2DateTimeString(3)

		/// <summary>
		/// Generate a CTimePair Sequence with the same _interval
		/// </summary>
		/// <param name="_timeRange">The total time range of the Sequence.</param>
		/// <param name="_interval">the _interval of each CTimePair</param>
		/// <param name="_bExtendLastPair">
		///		_bExtendLastPair = true means: if the last pair end time is large then
		///		 _timeRange.m_eTime, 
		///	</param>
		/// <returns>
		///		Return a CTimePair Sequence with the same _interval. 
		///		The last pair is determined by _eLastPairType.
		///	</returns>
		public static ArrayList GetTimeSequence( CTimePair _timeRange , long _interval , ELastPairType _eLastPairType )
		{
			ArrayList rstList = new ArrayList();

			/*
			if ( _interval >= _timeRange.GetInterval() )
			{
				rstList.Add( new CTimePair( _timeRange.m_bTime , _timeRange.m_eTime ) )
				return rstList;
			}
			*/

			long crrTiem ;
			for ( crrTiem = _timeRange.m_bTime.GetUInt32() ; crrTiem < _timeRange.m_eTime.GetUInt32() ; crrTiem += _interval )
			{
				rstList.Add( new CTimePair( crrTiem , crrTiem + _interval ) );
			}

			switch ( _eLastPairType )
			{
				case ELastPairType.ExtendPair :
					rstList.Add( new CTimePair( crrTiem , crrTiem + _interval ) );
					break;
				case ELastPairType.UseEndtime :
					rstList.Add( new CTimePair( crrTiem , _timeRange.m_eTime.GetUInt32() ) ); 
					break;
				default : //ELastPairType.Discard  
					break;
			};
			
			return rstList;

		}//GetTimeSequence(3)

		public static UInt32 ToTime32( long _nTime )
		{
			const long nBastTime = 116444448000000000; //new DateTime( 1970, 1, 1 ).ToFileTime();

			return (UInt32) ( ( _nTime - nBastTime) /10000000 );
		}

		public static long ToTime64( UInt32 _nTime )
		{
			const long nBastTime = 116444448000000000; //new DateTime( 1970, 1, 1 ).ToFileTime();
			return (long) ( ((long)_nTime)*10000000 + nBastTime) ;
		}

		/// <summary>
		/// 得到实际的营业时间长度。
		/// weekend is seemed as normal day.( no weekend )
		/// </summary>
		/// <param name="_nBgnTime">The begining date time of the time range</param>
		/// <param name="_nOpenTime"> the Time when commany begin to work everyday</param>
		/// <param name="_nDays"></param>
		/// <returns></returns>
		public static CTimePair GetBusinessTimePair( CTime _nBgnDateTime , UInt32 _nOpenTime , short _nDays )
		{
			return new CTimePair( 
				_nBgnDateTime + _nOpenTime, 
				_nBgnDateTime + (UInt32)(CTime.SecOfDay*_nDays) + _nOpenTime );
		}

        public static readonly string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        private static DateTime UnixTimeZero = new DateTime(1970, 1, 1);

        /// <summary>   
        /// 将unixtime转换为.NET的DateTime   
        /// </summary>   
        /// <param name="timeStamp">秒数</param>   
        /// <returns>转换后的时间</returns>   
        public static DateTime FromUnixTime(long timeStamp)
        {
            return new DateTime((timeStamp + 8 * 60 * 60) * 10000000 + UnixTimeZero.Ticks);
        }

        /// <summary>   
        /// 将.NET的DateTime转换为unix time   
        /// </summary>   
        /// <param name="dateTime">待转换的时间</param>   
        /// <returns>转换后的unix time</returns>   
        public static long ToDateTime(DateTime dateTime)
        {
            return (dateTime.Ticks - UnixTimeZero.Ticks) / 10000000 - 8 * 60 * 60;
        }

	}//class CTimeHelper
}//namespace
