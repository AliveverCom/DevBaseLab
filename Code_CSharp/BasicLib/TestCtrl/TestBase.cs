using System;

///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-11-17</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>
namespace Alivever.Com.DevBasic.BasicLib.TestCtrl
{
	/// <summary>
	/// TestBase 的摘要说明。
	/// </summary>
	public abstract class CTestBase
	{

		abstract protected string GetProjectName();

		abstract public bool DoTest();

		public abstract void PrintTestTital();

		public abstract void PrintEndTest();

		public abstract void BeginSubTest( string _sTital );
	}//class CTestBase
}
