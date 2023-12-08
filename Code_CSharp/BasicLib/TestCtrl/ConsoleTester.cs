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

namespace Alivever.Com.DevBasic.BasicLib.TestCtrl
{
	/// <summary>
	/// TestBase 的摘要说明。
	/// </summary>
	public abstract class CConsoleTester :CTestBase
	{

		protected abstract override string GetProjectName();

		public abstract override  bool DoTest();

		public override void PrintTestTital()
		{
			//System.Console.WriteLine("/**********************************************************");
			System.Console.WriteLine("/*************The Test of "+ GetProjectName() +"******************\n\n");
		}

		public override void PrintEndTest()
		{
			//System.Console.WriteLine("/**********************************************************");
			System.Console.WriteLine("******************** "+ GetProjectName() +" --> End ***************/\n\n");
		}

		public override void BeginSubTest( string _sTital )
		{
			System.Console.WriteLine("\n ============== "+ _sTital +" =============");
		}

	}//class CConsoleTester
}
