/// <File> FileName.cs </File>
/// <FileDesc>
/// 
/// </FileDesc>
/// <History>
///	<Devoloper> Shao Chen Ye </Devoloper>
///	<Date> 2007-02-26  </ChangeDate>
///     <Description> </Description>
/// </History>

/*********** references and namespaces  ***************/

using System;
using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.DbCtrl
{
	/// <summary>
	/// CDbCtrlHelper :针对CDbCtrl提供各种零碎地函数
	/// </summary>
	public class CDbCtrlHelper
	{
		public CDbCtrlHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static EDbDataType ToDbDataType( EValueType _objType )
		{

			switch( _objType )
			{
				case EValueType.None: 
					return EDbDataType.Unknow ;
				case EValueType.Binary:  
					return EDbDataType.Binary ;
				case EValueType.Number:  
					return EDbDataType.Number ;
				case EValueType.String:  
					return EDbDataType.VarChar ;
				case EValueType.Text:  
					return EDbDataType.Text ;
				case EValueType.Float:  
					return EDbDataType.Float ;
				case EValueType.Boolean:  
					return EDbDataType.Boolean ;
				case EValueType.Select:  
					return EDbDataType.Number  ;
				case EValueType.Date:  
					return EDbDataType.Date  ;
				case EValueType.Time:  
					return EDbDataType.Time  ;
				case EValueType.DateTime: 
					return EDbDataType.DateTime ;
				case EValueType.Char:
					return EDbDataType.Char ;
				default :
					return EDbDataType.Unknow ;
			}
		}//EDbDataType ToDbDataType()

		public static float GetDefaultDataSize( EDbDataType _dataType )
		{
			switch( _dataType )
			{
				case EDbDataType.Unknow:
					return 0;

				case EDbDataType.Number:
					return 8;

				case EDbDataType.Float:
					return (float)8.2;

				case EDbDataType.Char:
					return 16;

				case EDbDataType.VarChar:
					return 255;

				case EDbDataType.Date:
					return 10; //2005-01-01

				case EDbDataType.Time:
					return 8;//18:08:58

				case EDbDataType.DateTime:
					return 19;//2005-01-01 18:08:58

				case EDbDataType.Text:
					return 0;

				case EDbDataType.Binary:
					return 0;

				case EDbDataType.Boolean:
					return 5;//True / False

				default :
					return 0;
			}//switch( _dataType )
		}//float GetDefaultDataSize()

		public static string GetDefaultValue( EDbDataType _dataType )
		{
			switch( _dataType )
			{
				case EDbDataType.Unknow:
					return "";

				case EDbDataType.Number:
					return "0";

				case EDbDataType.Float:
					return "0";

				case EDbDataType.Char:
					return "";

				case EDbDataType.VarChar:
					return "";

				case EDbDataType.Date:
					return ""; //2005-01-01

				case EDbDataType.Time:
					return "";//18:08:58

				case EDbDataType.DateTime:
					return "";//2005-01-01 18:08:58

				case EDbDataType.Text:
					return "";

				case EDbDataType.Binary:
					return "";

				case EDbDataType.Boolean:
					return "False";//True / False

				default :
					return "ErrorDefaultValue";
			}//switch( _dataType )

		}
	}//class CDbCtrlHelper
}
