using System;
using System.Data;
using System.Collections;

namespace ShaoChenYe.DevFramework.LogicData.AppVersionCtrl
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class CAppVersionCtrlBase
	{
		protected string     m_ProgramPath;
		protected ArrayList  m_AppItems;  //vector<CAppVersionItem>
		protected string     m_CrrProgramVer;

		/// <summary>
		/// Sub-class should use the function to initialize m_AppItems in hard coding.
		/// </summary>
		public CAppVersionCtrlBase(  )
		{
		}

		/// <summary>
		/// Check all files reigisted in m_AppItems, 
		/// All these files must be in m_ProgramPath
		/// </summary>
		/// <param name="_vLowVerItems">
		/// Items that need to be update. 
		/// The file version is lower then required.
		/// </param>
		/// <param name="_vLostItems">
		/// Those files are needed ,but are not in m_ProgramPath
		/// </param>
		/// <param name="_WarnItems">
		/// Whoes version and file name are correct.
		/// but other file info are different, such as file size.
		/// </param>
		/// <returns>
		/// return F when something return ed in ref parameters
		/// return T means all items are corrected.
		/// </returns>
		public bool GetUpdateItems( ref ArrayList _vLowVerItems, 
			                         ref ArrayList _vLostItems  ,
									 ref ArrayList _WarnItems)
		{
			_vLowVerItems = null ;
			_vLostItems = null ;
			_WarnItems = null;

			if ( m_AppItems == null )
				return true;

			/* Do real check and compare 
			   Return F when something found different 
			*/

			return true;
		}

		/// <summary>
		/// Check all files reigisted in m_AppItems, 
		/// All these files must be in m_ProgramPath
		/// </summary>
		/// <param name="_vNewVerItems">
		/// a full file list of a program.
		/// </param>
		/// <param name="_vLowVerItems">
		/// Items that need to be update. 
		/// The file version is lower then required.
		/// </param>
		/// <param name="_vLostItems">
		/// Those files are needed ,but are not in m_ProgramPath
		/// </param>
		/// <param name="_WarnItems">
		/// Whoes version and file name are correct.
		/// but other file info are different, such as file size.
		/// </param>
		/// <returns>
		/// return F when something return ed in ref parameters
		/// return T means all items are corrected.
		/// </returns>
		public bool GetUpdateItems(     ArrayList _vNewVerItems,
									 ref ArrayList _vLowVerItems, 
			                         ref ArrayList _vLostItems  ,
									 ref ArrayList _WarnItems)
		{
			_vLowVerItems = null ;
			_vLostItems = null ;
			_WarnItems = null;

			if ( _vNewVerItems == null )
				return true;

			if ( m_AppItems == null )
			{
				_vLowVerItems = _vNewVerItems;
				return true;
			}

			/* Do real check and compare 
			   Return F when something found different 
			*/

			return true;
		}
	}//class CAppVersionCtrlBase
}//namespace ShaoChenYe.DevFramework.AppVersionCtrl
