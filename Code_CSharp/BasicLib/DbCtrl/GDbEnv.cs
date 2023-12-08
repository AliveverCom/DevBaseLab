/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2007-02-26  </ChangeDate>
///     <Description> </Description>
/// </History>
using System;

namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// GDbEnv .Singleton pattern
	/// </summary>
	public class GDbEnv
	{
		private GDbEnv m_GDbEnv;
		public string m_DftDbName;
		public EDbProductType m_DbProductType;

		public GDbEnv Ins()
		{
			if ( m_GDbEnv == null )
				m_GDbEnv = new GDbEnv();

			return m_GDbEnv;
		}//Ins()

	}//class GDbEnv
}
