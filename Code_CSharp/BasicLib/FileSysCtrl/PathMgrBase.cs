///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-13-18</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>

using System;
using System.IO;

namespace Alivever.Com.DevBasic.BasicLib.FileSysCtrl
{
	/// <summary>
	/// PathMgrBase 的摘要说明。
	/// </summary>
	public class CPathMgrBase :ICloneable
	{
		public const char UnixFilePathSeparator = '/';
		public const char WinFilePathSeparator = '\\';
		public const char WinRegPathSeparator = '\\';
		public const char URLSeparator = '/';

		protected EFileSysType  m_PathType = EFileSysType.MsFile ;

		protected string  m_crrPath;

		public virtual string PathName
		{
			get{ return m_crrPath ; } set{ m_crrPath = value;}
		}

		public virtual EFileSysType PathType
		{
			get{ return m_PathType ; } set{ m_PathType = value;}
		}

		public override string ToString()
		{
			return m_crrPath;
		}

		public CPathMgrBase()
		{

		}

		public CPathMgrBase( string _crrPath )
		{
			m_crrPath = _crrPath;
		}

		public CPathMgrBase( string _crrPath, EFileSysType  _PathType )
		{
			m_crrPath = _crrPath;
			m_PathType = _PathType ;
		}

		public CPathMgrBase( EFileSysType  _PathType )
		{
			m_PathType = _PathType ;
		}


		public static char GetSeparator( EFileSysType _pathType)
		{
			switch( _pathType )
			{
				case EFileSysType.MsFile :
					return WinFilePathSeparator;
				case EFileSysType.UnixFile :
					return UnixFilePathSeparator;
				case EFileSysType.WinRes :
					return WinRegPathSeparator;
				case EFileSysType.WebURL :
					return URLSeparator;
			};
					

			return ' ';
		}//GetSeparator(1)

		/// <summary>
		/// return path string = _crrPath - _sRootPath
		/// </summary>
		/// <param name="_crrPath"></param>
		/// <param name="_sRootPath"></param>
		/// <returns></returns>
		public static string GetRelativePath( string _crrPath, string _sRootPath )
		{
			int nRelativePath = _crrPath.IndexOf(_sRootPath) + _sRootPath.Length;
			if ( nRelativePath == _crrPath.Length )
				return "";

			return _crrPath.Substring( nRelativePath +1, 
										 _crrPath.Length - nRelativePath -1 );
		}

		public string GetRelativePath( string _sRootPath )
		{
			/*
			int nRelativePath = m_crrPath.IndexOf(_sRootPath) + _sRootPath.Length;
			if ( nRelativePath == m_crrPath.Length )
				return "";

			return m_crrPath.Substring( nRelativePath + 1, 
										m_crrPath.Length - nRelativePath -1 );
			*/
			return GetRelativePath(_sRootPath , m_crrPath );
		}

		public string GetFullPath( string _sRootPath )
		{
			return _sRootPath + GetSeparator() + m_crrPath; 
		}

		public string GetFullString( string _sRootPath )
		{
			return GetFullPath(_sRootPath);
		}

		public char GetSeparator( )
		{
			return GetSeparator( m_PathType );
		}

		public string GetParentPath()
		{
			int nLastSepPos = m_crrPath.LastIndexOf( GetSeparator( ) );

			return m_crrPath.Substring( 0 , (nLastSepPos-1)>0?(nLastSepPos-1):0 );
		}

		public string GetCrrPathName()
		{
//			int nLastSepPos = m_crrPath.LastIndexOf( GetSeparator( ) );
//
//			if ( nLastSepPos >=0 )
//				return m_crrPath.Substring( nLastSepPos+1 , m_crrPath.Length - nLastSepPos-1);
//			else
//				return m_crrPath;

			return GetCrrPathName( m_crrPath ,  GetSeparator( )); 
			
		}

		public static string GetCrrPathName( string _crrPath, char _Separator )
		{
			if ( _crrPath == null )
				return null;

			int nLastSepPos = _crrPath.LastIndexOf( _Separator );

			if ( nLastSepPos >=0 )
				return _crrPath.Substring( nLastSepPos+1 , _crrPath.Length - nLastSepPos-1);
			else
				return _crrPath;
			
		}


		public virtual string AddSubPath( string _subPath )
		{
			m_crrPath += GetSeparator( ) + _subPath;
			return m_crrPath;
		}

		/// <summary>
		/// Temporarily get a string with _subPath, but do not save in to object.
		/// </summary>
		/// <param name="_subPath"></param>
		/// <returns></returns>
		public virtual string AddSubPathTemp( string _subPath )
		{
			return m_crrPath + GetSeparator( ) + _subPath;
		}

		/// <summary>
		/// Temporarily get a string with _FileName, but do not save in to object.
		/// </summary>
		/// <param name="_subPath"></param>
		/// <returns></returns>
		public string GetFullFileName( string _FileName )
		{
			return m_crrPath + GetSeparator( ) + _FileName;
		}

		public virtual EFileSysItemTpye GetSelfType()
		{
			return EFileSysItemTpye.Path;
		}

		public virtual Object Clone()
		{
			return new CPathMgrBase( this.m_crrPath, this.m_PathType );
		}
		//public override string ToString()
		//{
		//	return m_crrPath;
		//}

		public string Parent
		{
			get{ return "";}
		}
	}//class PathMgrBase
}//class PathMgrBase
