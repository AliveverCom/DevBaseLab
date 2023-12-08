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
	/// IDbCmdNode 的摘要说明。
	/// </summary>
	public interface IDbCmdNode : IAccessByString
	{
		// abstract Attribute DataBaseName
		void DataBaseName( string _sDBname ); //{}
		string DataBaseName() ;//{ return "";}

		// abstract Attribute UseDbNameInCmd
		void UseDbNameInCmd (bool _bUse ) ;//{}
		bool UseDbNameInCmd () ;//{ return false; }

		//TableName
		void TableName( string _sNewTableName );//{}
		string TableName() ;//{ return ""; }

		string ToDbCmdString();//{ return ""; }

		new bool InitFromString(string _initStr); //{ return false; }

		new string ToInitString() ;//{ return ToSqlString();}

		string ToSqlString() ; //{ return ""; }

	}//interface IDbCmdNode
}//namespace
