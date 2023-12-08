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
using System.Collections.Generic;


namespace Alivever.Com.DevBasic.BasicLib.ToolsCtrl
{
	/// <summary>
	/// StringHelper 的摘要说明。
	/// </summary>
	public class CStringHelper
	{
		protected static CStringHelper m_Ins = null;
		public static char[] TrimCharV = {' ', '\t'};

		public char[] m_Seperaters = {',', '\t', ' '};
		public bool   m_bParseIncludeSeperaters = false;

		public CStringHelper Ins()
		{
			if ( m_Ins == null )
				m_Ins = new CStringHelper();

			return m_Ins;
		}

		public  ArrayList ParseNameList( string _sNamesStr )
		{
			ArrayList rstList = new ArrayList();
			ParseNameList( _sNamesStr , ref rstList );
			return rstList;
		}

		public  void ParseNameList( string _sNamesStr , ref ArrayList _rstV)
		{
			_sNamesStr.Trim();

			if ( _sNamesStr.Length == 0 )
			{
				if ( _rstV != null )
				{
					_rstV.Clear();
					return ;
				}
				return ;
			}
					
			if ( _rstV == null )
				_rstV = new ArrayList();

			int sptIdx = 0;
			//int nCrrIdx =0;
			for( ; ; )
			{
				_sNamesStr.Trim();
				if ( _sNamesStr.Length == 0 )
					return ;

				sptIdx = _sNamesStr.IndexOfAny( m_Seperaters );

				switch ( sptIdx )
				{
					case 0: 
						if ( !m_bParseIncludeSeperaters )
						{ 
							_sNamesStr = _sNamesStr.Substring( 1).Trim();
							continue;
						}
						break;

					case -1:
						_rstV.Add( _sNamesStr );
						_sNamesStr = "";
						break;

					default:
						_rstV.Add( _sNamesStr.Substring(0, sptIdx ).Trim() );
						_sNamesStr = _sNamesStr.Substring( sptIdx  ).Trim() ;
						break;
				};
			}
			//ArrayList 

		}//GetNameString()


		/// <summary>
		/// trim a string and replace the trimed space with _replaceTimStr
		/// </summary>
		/// <returns> return a processed string </returns>
		public static string ReplaceTrim( string _SrcStr, string _replaceTimStr )
		{
			_SrcStr = ReplaceTrimStart( _SrcStr , _replaceTimStr );
			return ReplaceTrimEnd( _SrcStr , _replaceTimStr );
		}

		/// <summary>
		/// trim a string and replace the trimed space with _replaceTimStr
		/// </summary>
		/// <returns> return a processed string </returns>
		public static string ReplaceTrimStart( string _SrcStr, string _replaceTimStr )
		{
			if (   _SrcStr.Length != 0
				&& _SrcStr.IndexOfAny ( TrimCharV ) == 0 )
				return _replaceTimStr + _SrcStr.TrimStart();
			else
				return _SrcStr;
		}

		/// <summary>
		/// trim a string and replace the trimed space with _replaceTimStr
		/// </summary>
		/// <returns> return a processed string </returns>
		public static string ReplaceTrimEnd( string _SrcStr, string _replaceTimStr )
		{
			if (   _SrcStr.Length != 0
				&& _SrcStr.LastIndexOfAny ( TrimCharV ) == _SrcStr.Length-1 )
				return  _SrcStr.TrimStart() + _replaceTimStr ;
			else
				return _SrcStr;
		}


		/// <summary>
		/// Format _SrcStr's length to _MasSize. If _SrcStr' length is not enough, fill the rest place with _replaceStr.
		/// The filled string will be placed begin with _FillFrom.
		/// If _SrcStr's length is larger then _MasSize, do nothing.
		/// </summary>
		/// <returns> return a processed string </returns>
		public static string FixedSizeFormat( string _SrcStr, int _MasSize, string _replaceStr, EPosition _FillFrom )
		{
			//If _SrcStr's length is larger then _MasSize, do nothing.
			if ( _SrcStr.Length >= _MasSize )
				return _SrcStr;

			for ( int i = ( _MasSize - _SrcStr.Length )/ _replaceStr.Length ; i > 0 ; i--)
			{
				_SrcStr = AddString( _SrcStr,_replaceStr, _FillFrom );
			}

			int nLastFillLength = ( _MasSize - _SrcStr.Length ) % _replaceStr.Length ;

			//if still need to file a part of _replaceStr
			if ( nLastFillLength != 0 )
				_SrcStr = AddString( _SrcStr,_replaceStr.Substring( 0 , nLastFillLength) , _FillFrom );

			return _SrcStr ;
		}

		/// <summary>
		/// Add _NewStr to _SrcStr by position of _FillFrom{Left,Right}
		/// If _FillFrom is illegal, then return _SrcStr directly.
		/// </summary>
		/// <returns> return a processed string </returns>
		public static string AddString( string _SrcStr, string _NewStr, EPosition _FillFrom )
		{
			if ( _FillFrom == EPosition.Left ||  _FillFrom == EPosition.Top )
				return _NewStr + _SrcStr ;
			
			if ( _FillFrom == EPosition.Right ||  _FillFrom == EPosition.Bottom )
				return  _SrcStr + _NewStr ;

			return _SrcStr;
		}

        /// <summary>
        /// 把一个列表转化成一个字符串。每个item之间以 _split 分割
        /// </summary>
        /// <param name="_strList"></param>
        /// <returns></returns>
        public static string ListToStr(IEnumerable<string> _strList, string _split)
        {
            string rst = string.Empty;
            int i = 0;
            foreach (string crrItem in _strList)
            {
                rst += (i == 0 ? string.Empty : _split) + crrItem;
            }

            return rst;
        }//ListToStr()



	}//class CStringHelper
}
