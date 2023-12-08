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
using System.Collections;
using System.Collections.Generic;

namespace Alivever.Com.DevBasic.BasicLib.FileSysCtrl
{
	/// <summary>
	/// CFile Iterator
	/// Creater: Shao Chen Ye 2004-10-05
	/// Description: This class is mainly designed for file parse algorithm, 
	/// which mainly uses File and Directory classes in C#. It can help you 
	/// to simplize the structure of your program.
	/// For other normal file operstion and High efficiency, I suggest you to
	/// us system bottom operations. 
	/// </summary>
	public class CFileIterator //temporarily dont'a support IEnumerator
	{
		protected string m_PreLevelRoot = "";
		protected string m_RootDirStr = "";
		protected string m_FilterStr = "*";

		protected ESortOrder m_eSortOrder = ESortOrder.None;

		protected bool m_bUseRelativePath = true; //value=false mease use FullPath

		///////////////////////////////////////////////////////////
		///CYShao: the following attributes are only used for C#
		///In other langues, you should use V-factory patten and 
		///make subclass.
		///////////////////////////////////////////////////////////

		private string [] m_pSubpathV = null; //All subpathes' name of this path level.
		private string [] m_pFilesV = null;   //All Files' name of this path level.

		private Int16 m_nCrrSubpath = -1;// -1 meanse root dir
		private Int16 m_nCrrFile = -1; // -1 meanse none file is get

		private CFileIterator m_pCrrSubpath = null;

		///////////////////////////////////////////////////////////
		//The following are mumber  Functions /////////////////////
		///////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_bEnterSubDir"></param>
        /// <returns>vector[string fileName]</returns>
		public List<string> GetAllFiles(bool _bEnterSubDir )
		{
			PreloadFileNames(this.m_RootDirStr);

            List<string> files = new List<string>();

			if ( _bEnterSubDir == false && m_pFilesV != null )
			{
				foreach( string crrItem in m_pFilesV )
				{
					files.Add(crrItem);
				}
			}
			
			if ( _bEnterSubDir == true )
			{
				if ( m_pFilesV != null )
				{
					foreach( string crrItem in m_pFilesV )
					{
						files.Add(crrItem);
					}
				}

				//Do some thing for subdir
			}

			return files;
			
		}

		protected string GetCrrPathStr()
		{
			
			if ( m_nCrrSubpath == -1 //if first time to iterate. 
				|| m_pSubpathV == null // not begin iteration
				||(   m_pSubpathV != null //no subpath in root dir
				   && m_nCrrSubpath >= m_pSubpathV.Length ) //out of the end 
			   )
			{
				return m_RootDirStr;
			}

			return m_pSubpathV[m_nCrrSubpath];
		}

		protected string GetCrrFileStr()
		{
			if (   m_pFilesV == null 
				|| m_nCrrFile < 0 
				|| m_nCrrFile >= m_pFilesV.Length )
				return null;

			return m_pFilesV[m_nCrrFile];
		}

		/// <summary>
		/// restar Iterator from the beginning of root dir.
		/// program will clean all status to be as root beginning.
		/// </summary>
		public void Restar()
		{
			m_pSubpathV = null; 
			m_pFilesV = null;   

			m_nCrrSubpath = -1;
			m_nCrrFile = -1; 

			m_pCrrSubpath = null;

		}//void Restar()

		public CFileIterator( string _RootDirStr )
		{
			m_RootDirStr = _RootDirStr;
			m_PreLevelRoot = _RootDirStr;
		}

		public CFileIterator( string _RootDir, string _FilterStr )
			: this( _RootDir )
		{
			m_FilterStr = _FilterStr;
		}

		protected CFileIterator( string _RootDir, string _FilterStr, string _PreRootDirStr )
			: this( _RootDir, _FilterStr)
		{
			m_PreLevelRoot = _PreRootDirStr;	
		}

		/// <summary>
		/// GetNextItem(). 
		/// This is the combined function of GetNextFile() and GetNextPath().
		/// </summary>
		/// <param name="_isFileFirst"> 
		/// True: Return files first ,then pathes.(Top first)
		/// False: Return pathes first ,then files.((Bottom first))
		/// Note: This param is avialable only when _nFilePathFilter=EFileSysItemTpye.File | EFileSysItemTpye.Path
		/// </param>
		/// <param name="_nFilePathFilter">
		/// value=EFileSysItemTpye.File: return files only.
		/// value=EFileSysItemTpye.Path: return pathes only.
		/// value=EFileSysItemTpye.File | EFileSysItemTpye.Path: return both files and pathes
		/// </param>
		/// <returns>
		/// Return all items in the path, whatever it is Path or File
		/// </returns>
		public CPathMgrBase GetNextItem( 
					bool _bEnterSubpath ,
					char _nFilePathFilter,
					bool _isFileFirst )
		{
			string crrFileName = null;
			string crrPathName = null;

			if ( ( _nFilePathFilter & (char)EFileSysItemTpye.File )!=0
				&& _isFileFirst )
			{
				crrFileName = GetNextFile();
				if ( crrFileName != null )
					return new CPathFileMgr(
									ToRelativePath( crrFileName ),
									EFileSysItemTpye.File );
			}
				
			//if has subpath
			PreloadPathNames();
			if ( m_pSubpathV != null && m_pSubpathV.Length != 0)
			{
				if ( !_bEnterSubpath )
				{
					crrPathName = GetNextPath();
					if ( (_nFilePathFilter & (char)EFileSysItemTpye.Path)!=0
						&& crrPathName != null )
						return new CPathMgrBase( ToRelativePath( crrPathName) );
				}
				else //try subpath
				{
					CPathMgrBase pItem = GetNextItemInSubpath(
						_bEnterSubpath ,
						_nFilePathFilter,
						_isFileFirst );

					if ( pItem != null )
						return pItem;
				}
			}//if has subpath
				
			if (   ( _nFilePathFilter & (char)EFileSysItemTpye.File )!=0
				&& !_isFileFirst )
			{
				crrFileName = GetNextFile();
				if ( crrFileName != null )
					return new CPathFileMgr( ToRelativePath( crrFileName),
									EFileSysItemTpye.File );
			}

			return null;

		}//GetNextItem(4)

		protected string ToRelativePath( string _crrPath)
		{
			if ( !m_bUseRelativePath )
				return _crrPath;

			return CPathMgrBase.GetRelativePath( _crrPath, m_PreLevelRoot );
		}

		protected CPathMgrBase GetNextItemInSubpath ( 
									bool _bEnterSubpath ,
									char _nFilePathFilter,
									bool _isFileFirst )
		{
			string crrPathName ;
			if ( m_pCrrSubpath == null)
			{
				m_pCrrSubpath = new CFileIterator( "" , m_FilterStr,m_PreLevelRoot);
			}

			crrPathName = GetCrrPathStr();
			if ( crrPathName != m_pCrrSubpath.RootDirStr 
				&&(   (   m_pCrrSubpath.RootDirStr.Length != 0 
					   && crrPathName != Directory.GetParent(m_pCrrSubpath.RootDirStr).FullName 
				      )
				   || m_pCrrSubpath.RootDirStr.Length == 0
				  )
				)
			{
				m_pCrrSubpath.RootDirStr = crrPathName;

				if ( (_nFilePathFilter & (char)EFileSysItemTpye.Path )!=0 )
					return new CPathMgrBase( ToRelativePath( crrPathName) );
			}


			if ( m_nCrrSubpath < m_pSubpathV.Length )
			{
				CPathMgrBase pItem = m_pCrrSubpath.GetNextItem(
					_bEnterSubpath ,
					_nFilePathFilter,
					_isFileFirst );

				// if there is an available item returned. 
				if ( pItem != null )
					return pItem;
				
				//if subpath is over
				crrPathName = GetNextPath();
				if ( crrPathName == null)
					return null;
				else
					return GetNextItem(
						_bEnterSubpath ,
						_nFilePathFilter,
						_isFileFirst );
			}

			return null;

		}//GetNextItemInSubpath()

		//public CPathMgrBase GetNextFile( ESortOrder _eSortOrder )
		public string GetNextFile( )
		{
			//if first time to iterate.
			PreloadFileNames(this.m_RootDirStr);

			if (   m_pFilesV == null //no subpath in root dir
				|| m_nCrrFile >= m_pFilesV.Length ) //out of the end 
				return null;

			if (   m_pFilesV == null 
				|| m_nCrrFile < 0 
				|| m_nCrrFile >= m_pFilesV.Length )
				return null;

			//return new CPathFileMgr( GetCrrPathStr() , m_pFilesV[m_nCrrFile]);
			return m_pFilesV[m_nCrrFile++];
		}

		protected void PreloadPathNames()
		{
			//if first time to iterate.
			if ( m_nCrrSubpath > -1 || m_pSubpathV != null )
				return ;

			m_pSubpathV = Directory.GetDirectories( this.m_RootDirStr, this.m_FilterStr );
			m_nCrrSubpath ++;

			switch (  m_eSortOrder )
			{
			case ESortOrder.Asc : 
				Array.Sort( m_pSubpathV );
				break;

			case ESortOrder.Desc :
				Array.Sort( m_pSubpathV );
				Array.Reverse(m_pSubpathV);
				break;

			default:
				break;
			};

		}//PreloadPathNames()

		protected void PreloadFileNames(string _crrDirStr)
		{
			//if first time to iterate.
			if ( m_nCrrFile > -1 || m_pFilesV != null )
				return ;

            m_pFilesV = Directory.GetFiles(_crrDirStr, this.m_FilterStr);
			m_nCrrFile ++;

			switch (  m_eSortOrder )
			{
				case ESortOrder.Asc : 
					Array.Sort( m_pFilesV );
					break;

				case ESortOrder.Desc :
					Array.Sort( m_pFilesV );
					Array.Reverse(m_pFilesV);
					break;

				default:
					break;
			};//switch (  m_eSortOrder )

		}//PreloadFileNames()

		public string GetNextPath( )
		{

			PreloadPathNames();

			if (   m_pSubpathV == null //no subpath in root dir
				|| m_nCrrSubpath >= m_pSubpathV.Length ) //out of the end 
				return null;

			return m_pSubpathV[m_nCrrSubpath++];
		}

		/*
		/// <summary>
		/// The same with GetNextItem(2) with default value _eSortOrder=None.
		/// </summary>
		public CPathMgrBase GetNextItem( bool _isFileFirst  )
		{
			return null;
		}

		/// <summary>
		/// The same with GetNextFile(1) with default value _eSortOrder=None.
		/// </summary>
		//public CPathMgrBase GetNextFile()
		public string GetNextFile()
		{
			return GetNextFile( ESortOrder.None );
		}

		/// <summary>
		/// The same with GetNextPath(1) with default value _eSortOrder=None.
		/// </summary>
		public string GetNextPath()
		{
			return GetNextPath( ESortOrder.None );
		}
		*/

		/// <summary>
		/// Alert: This object will be reset when you set new value to RootDirStr
		/// </summary>
		public string RootDirStr
		{
			get{ return m_RootDirStr; }
			set{ Restar(); m_RootDirStr=value;}
		}

		public string FilterStr
		{
			get{ return m_FilterStr; }
			set{ m_FilterStr=value;}
		}

		/// <summary>
		/// Alert: This object will be reset when you set new value to RootDirStr
		/// </summary>
		public ESortOrder SortOrder 
		{
			get { return m_eSortOrder; }
			set { Restar(); m_eSortOrder=value; }
		}

		/// <summary>
		/// Alert: This object will be reset when you set new value to RootDirStr
		/// </summary>
		public bool IsUseRelativePath  
		{
			get{ return m_bUseRelativePath; }
			set{ Restar(); m_bUseRelativePath=value;}
		}

//		public virtual void Dispose()
//		{
//			m_pSubpathV.
//			m_pFilesV
//			m_pCrrSubpath
//		}
	}//class CFileItr
}//namespace
