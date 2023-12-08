using System;
using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.DevBasic.BasicLib.CfgCtrl
{
	/// <summary>
	/// CCfgItemDefine һ�п�������Ļ���
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
		/// С��λ��λ��, ���ַ�������
		/// </summary>
		public int ValueLength = 0;

		/// <summary>
		/// ������ֵ=��Сֵ�������ж�����
		/// </summary>
		public int MaxValue = 0; 

		/// <summary>
		/// ������ֵ=��Сֵ�������ж�����
		/// </summary>
		public int MinValue = 0; 

		public bool IsReadonly = false;

		public string DefultValue="";

		/// <summary>
		/// �ڱ༭��ʱ�򣬴���Ŀ�Ƿ��ѡ�����ѡ��
		/// </summary>
		public bool IsUseChoiceList = false;

		public string ChoiceStrID="";

		public bool IsUseChoiceTag = false;

		/// <summary>
		/// ����Ƕ�ֵ�洢�����¼��洢ֵ�����ݿ������
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
