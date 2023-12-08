using System;

namespace Alivever.Com.DevBasic.BasicLib.CfgLib
{
	/// <summary>
	/// XTag ��ժҪ˵����
	/// </summary>
	internal class XTag
	{
		public const string tAbcList = "TAbChoiceList";
		public const string tAbclItems = "TAbChoiceListItems";

		/*=============================================================
			table name    : TAbChoiceList
			description : ѡ���б���
			abbreviation: Cli
			-----------------------------
			ClID        ѡ���б���
			CliName     ѡ���б���ʾ����
			CliDesc     ѡ���б�����
		=============================================================*/
		public class TAbcList //TAbChoiceList
		{
			public const string Cl_ID   = "ClID" ;			
			public const string Cl_Name = "ClName" ;			
			public const string Cl_Desc = "ClDesc" ;			
		}//class TAbcList

		/*=============================================================
			table name    : TChoiceListItems
			description : ѡ���б���
			abbreviation: Cli
			-----------------------------
			CliID       ѡ����
			ClID        �б���
			CliName     ѡ����ʾ����
			CliAccKey   ���ٿ�ݽ�
			CliDesc     ѡ������

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
