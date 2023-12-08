using System;

namespace Alivever.Com.DevBasic.BasicLib.CfgLib
{
	/// <summary>
	/// XTag 的摘要说明。
	/// </summary>
	internal class XTag
	{
		public const string tAbcList = "TAbChoiceList";
		public const string tAbclItems = "TAbChoiceListItems";

		/*=============================================================
			table name    : TAbChoiceList
			description : 选择列表项
			abbreviation: Cli
			-----------------------------
			ClID        选项列表编号
			CliName     选项列表显示名称
			CliDesc     选项列表描述
		=============================================================*/
		public class TAbcList //TAbChoiceList
		{
			public const string Cl_ID   = "ClID" ;			
			public const string Cl_Name = "ClName" ;			
			public const string Cl_Desc = "ClDesc" ;			
		}//class TAbcList

		/*=============================================================
			table name    : TChoiceListItems
			description : 选择列表项
			abbreviation: Cli
			-----------------------------
			CliID       选项编号
			ClID        列表编号
			CliName     选项显示名称
			CliAccKey   加速快捷健
			CliDesc     选项描述

		=============================================================*/
		public class TAbclItems //TAbChoiceListItems
		{
			public const string Cl_ID      = "ClID" ;			
			public const string Cli_ID     = "CliID" ;			
			public const string Cli_Name   = "CliName" ;			
			public const string Cli_AccKey = "CliAccKey" ;			
			public const string Cli_Desc   = "CliDesc" ;			
        
		}//class TAbclItems

	}//class XTag
}//namespace
