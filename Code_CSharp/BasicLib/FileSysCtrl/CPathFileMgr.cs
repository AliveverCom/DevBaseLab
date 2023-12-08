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
	/// CPathFileMgr 的摘要说明。 
	/// </summary>
	public class CPathFileMgr:CPathMgrBase
	{
        /// <summary>
        /// full file name without path
        /// </summary>
		protected string m_FileName ="";

        /// <summary>
        /// full file name without path
        /// </summary>
        public string FileName
		{
			get{ return m_FileName; } 
			set{ m_FileName = value; }
		}

        public string FileNameWithoutExtension
        {
            get
            {
                if (m_FileName == null)
                    return "";

                int nPos = m_FileName.LastIndexOf('.');

                // if not find  Extension
                if (nPos < 0)
                    return m_FileName;

                // if find Extension, return string of Extension
                return m_FileName.Substring(0, nPos );

            }//get
        }//FileNameWithoutExtension

		public string Extension
		{
			get
			{ 
				if ( m_FileName == null )
					return "";

				int nPos =  m_FileName.LastIndexOf('.');

				// if not find  Extension
				if ( nPos < 0 )
					return "";

				// if find Extension, return string of Extension
				return m_FileName.Substring(nPos+1);

			}
			
			set
			{
				if ( value.Length == 0 ) 
					return;

				int nPos =  m_FileName.LastIndexOf('.');

				// if not find  Extention and has a file name, add a new Extension
				if ( nPos < 0 && this.m_FileName.Length !=0 )
				{
					this.m_FileName = this.m_FileName + "." + value;
				}

				// if find extention, change Extension
				m_FileName = m_FileName.Substring(0,nPos) + "." + value;
			}
		} 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sPathName"> a path string without filename </param>
		public CPathFileMgr(string _sPathName )
			: base(_sPathName)
		{
			
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sPathName"> a path string without filename </param>
        /// <param name="_sFileName"> full file name without path</param>
		public CPathFileMgr(string _sPathName, string _sFileName)
			: base(_sPathName)
		{
			m_FileName = _sFileName;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sPathName"> a path string without filename </param>
        /// <param name="_sFileName"> full file name without path</param>
        /// <param name="_eSysType"></param>
		public CPathFileMgr(string _sPathName, string _sFileName, EFileSysType _eSysType)
			: base(_sPathName , _eSysType)
		{
			this.m_FileName = _sFileName;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sNameStr"> a string which type is specified by _eStrType</param>
        /// <param name="_eStrType"></param>
        /// <param name="_eSysType"></param>
		public CPathFileMgr(string _sNameStr, EFileSysItemTpye _eStrType, EFileSysType _eSysType)
		{
			this.PathType = _eSysType;
			m_FileName = ParseAddress(_sNameStr, _eStrType, _eSysType ,ref m_crrPath);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_sNameStr"></param>
		/// <param name="_eStrType"></param>
		/// <param name="_eSysType"></param>
		/// <returns>file name</returns>
		public static string ParseAddress( 
			string _sNameStr,
			EFileSysItemTpye _eStrType, 
			EFileSysType _eSysType,
			ref string o_path )
		{
			string fileName = "";
			if ( _eStrType == EFileSysItemTpye.File ) // if only a file name
			{
				fileName = _sNameStr;
				o_path = null;
			}
			else if (  _eStrType == EFileSysItemTpye.Path )//if path only
			{
				o_path = _sNameStr ;
			}
			else // if path and file 
			{
				int nSpt = _sNameStr.LastIndexOf( CPathMgrBase.GetSeparator(_eSysType) );

				if ( nSpt < 0 ) //if no seprator is found---only a file name
				{
					o_path = "" ; fileName = _sNameStr;
				}
				else //if both path and fileName are in _sNameStr
				{
					o_path =  _sNameStr.Substring( 0 ,  nSpt ) ;
					fileName = _sNameStr.Substring( nSpt + 1, _sNameStr.Length - nSpt-1);
				}
			}
			return fileName;

		}//ParseAddress()

		/// <summary>
		/// = CPathFileMgr( _sNameStr, _eStrType, EFileSysType.MsFile) 
		/// </summary>
		/// <param name="_sNameStr"></param>
		/// <param name="_eStrType"></param>
		public CPathFileMgr(string _sNameStr, EFileSysItemTpye _eStrType)
			: this( _sNameStr, _eStrType, EFileSysType.MsFile)
		{
		}//CPathFileMgr(3)
				

		public override string ToString()
		{
			/*
			if ( GetSelfType() == EFileSysItemTpye.Path )
				return base.ToString () ;
			else
				return base.ToString () + GetSeparator() + m_FileName;
			*/
			if ( base.m_crrPath == null || base.m_crrPath.Length == 0)
				return m_FileName;
			else if ( m_FileName==null || m_FileName.Length == 0 )
				return base.ToString();
			else
				return base.ToString () + GetSeparator() + m_FileName;
		}

		public override EFileSysItemTpye GetSelfType()
		{
			if ( m_FileName == null )
				return EFileSysItemTpye.Path ;
			else
				return EFileSysItemTpye.File ;
		}

		public override Object Clone()
		{
			return new CPathFileMgr( this.m_crrPath, this.FileName ,this.PathType );

		}

		public override string AddSubPath( string _subPath )
		{
			base.AddSubPath(_subPath);
			return this.ToString();
		}

		/// <summary>
		/// Temporarily get a string with _subPath, but do not save in to object.
		/// </summary>
		/// <param name="_subPath"></param>
		/// <returns></returns>
		public override string AddSubPathTemp( string _subPath )
		{
			return m_crrPath + GetSeparator( ) + _subPath  + GetSeparator( ) + this.FileName;
		}

		/// <summary>
		/// Return all context in a file as a string. I suggest you'd better ensure _fileName do can be read, 
		/// or it will return null only. This method will use System.Text.Encoding.Default to read.
		/// </summary>
		/// <param name="_fileName"></param>
		/// <returns>Return null means error. you may use other method to get the reason, or read log.</returns>
		public static string ReadAllStringFromFile(string _fileName )
		{
			return ReadAllStringFromFile( _fileName , System.Text.Encoding.Default );
		}
		
		/// <summary>
		/// Return all context in a file as a string. I suggest you'd better ensure _fileName do can be read, or it will return null only.
		/// </summary>
		/// <param name="_fileName"></param>
		/// <returns>Return null means error. you may use other method to get the reason, or read log.</returns>
		public static string ReadAllStringFromFile(string _fileName , System.Text.Encoding _encoding)
		{
			try 
			{
				// Create an instance of StreamReader to read from a file.
				// The using statement also closes the StreamReader.
				using (StreamReader sr = new StreamReader( _fileName, _encoding )) 
				{
					return sr.ReadToEnd();
				}
			}
			catch (Exception ex ) 
			{
                // Let the user know what went wrong.
                throw ex;
				//return null;
			}

		}

	}//class CPathFileMgr
}//namespace
