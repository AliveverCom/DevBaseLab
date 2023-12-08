using System;
using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.CfgCtrl
{
	/// <summary>
	/// CCfgItemDefine 一切可配置相的基类
	/// </summary>
	public class CCfgItemDefine : CItemBase
	{
		//int nID;

		public string IdStr; 

		//string DispName = "";

		//string Desc = "";

		public string DispImage = "";

		public EValueType ValueType;

		/// <summary>
		/// 小数位的位数, 或字符串长度
		/// </summary>
		public int ValueLength = 0;

		/// <summary>
		/// 如果最大值=最小值，则不做判断限制
		/// </summary>
		public int MaxValue = 0; 

		/// <summary>
		/// 如果最大值=最小值，则不做判断限制
		/// </summary>
		public int MinValue = 0; 

		public bool IsReadonly = false;

		public string DefultValue="";

		/// <summary>
		/// 在编辑的时候，此项目是否从选择框中选出
		/// </summary>
		public bool IsUseChoiceList = false;

		public string ChoiceStrID="";

		public bool IsUseChoiceTag = false;

		/// <summary>
		/// 如果是多值存储，则记录其存储值的数据库表名称
		/// </summary>
		public string RecordTableName = "";

		public CCfgItemDefine()
		{
		}

		public CCfgItemDefine(int _nID, string IdStr, string Desc )
		{
			this.m_nID = _nID;
			this.IdStr = IdStr;
			this.m_sName = IdStr;
			this.Desc = Desc;
		}
	}//class CCfgItemDefine
}
