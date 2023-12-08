using System;

namespace Alivever.Com.DevBasic.BasicLib
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public interface ILogable
	{
		string ToLogString( bool _bShowAll );

		//string ToLogString(string _sPrefix , string _sSuffix );

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_nDeepness">
		/// How many layers Log should be follow.
		/// _nDeepness-- when passing each layer
		/// Stop at _nDeepness == 0
		/// </param>
		/// <param name="_bShowAll">
		/// detial level 
		/// False = e_Simple , True = e_All
		/// </param>
		/// <returns></returns>
		string ToLogString( bool _bShowAll ,string _sPrefix , string _sSuffix );//, short _nDeepness );

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// "m_nID , m_sName , m_sDesc"
		/// </returns>
		string LogItemTitals(  bool _bShowAll );

	}//interface ILogable
}//namespace
