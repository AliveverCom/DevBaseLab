using System;

namespace ShaoChenYe.DevFramework.LogicData.AppVersionCtrl
{
	/// <summary>
	/// AppVersionItem 的摘要说明。
	/// </summary>
	public class CAppVersionItem
	{
		public long    m_nID;
		public string  m_FileName;
		public string  m_VerStr; //1.2.300 . Empty means doesn't need check version.
		public string  m_SubDirName;
		public long    m_nFileSize;
		//public long    m_nFileDate;


		public CAppVersionItem()
		{
		}

		public CAppVersionItem( long    _nID,
								string  _FileName,
								string  _VerStr, //1.2.300 . Empty means doesn't need check version.
								string  _SubDirName,
								long    _nFileSize )
		{
			m_nID         = _nID ;
			m_FileName    = _FileName;
			m_VerStr      = _VerStr; 
			m_SubDirName  = _SubDirName;
			m_nFileSize   = _nFileSize;
		}

	}
}
