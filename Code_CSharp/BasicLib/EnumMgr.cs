///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2010-02-12</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog>�л����̣�������ļ���CreatBone.InfoSysBuilder.Generic�ƶ���Alivever.Com.DevBasic.BasicLib.</ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib
{
    /// <summary>
    /// ��̬ö�����͡����ڹ���̬���õ�ö�����͡����������õ��ĵط���
    /// ��Ҫ�Լ��ڼ̳������ṩһ��static m_Ins  �Լ� static Ins{get;}���ж��⺯�����Ǿ�̬�ġ�
    /// </summary>
    public class CEnumMgrBase<TKey>
    {
        /// <summary>
        /// ��ǰ���ö�����͵����֡������Ƽ�Ϊȫ��Ψһ�����ơ������ڵ�ǰ��������Ψһ��
        /// </summary>
        public string NameStr = string.Empty;

        /// <summary>
        /// ��ǰö�ٵ�˵��
        /// </summary>
        public string DescStr = string.Empty;

        private Dictionary<TKey, CEnumItemBase<TKey>> m_Items = new Dictionary<TKey, CEnumItemBase<TKey>>();

        public Dictionary<TKey, CEnumItemBase<TKey>> Items { get { return m_Items; } }

        /// <summary>
        /// �����Ͻ�ʹ��������ÿ������
        /// </summary>
        protected CEnumMgrBase()
        {
            
        }

        /// <summary>
        /// ���캯������������NameStr��DescStr
        /// </summary>
        /// <param name="_NameStr"></param>
        /// <param name="_DescStr"></param>
        public CEnumMgrBase(string _NameStr, string _DescStr)
        {
            this.NameStr = _NameStr;
            this.DescStr = _DescStr;
        }

        ///// <summary>
        ///// ������Ԫ����һ��List����ʽ���з��ء������List����ʱ���ɵģ�����Ψһ�����ö���
        ///// </summary>
        ///// <returns></returns>
        //public List<CEnumItemBase> GetItemsList()
        //{
        //    List<CEnumItemBase> rstList = new List<CEnumItemBase>();
        //    foreach (KeyValuePair<string, CEnumItemBase> crrPair in this.Items)
        //    {
        //        rstList.Add( crrPair.Value );
        //    }

        //    return rstList;
        //}

        /// <summary>
        /// ����ĳ��ÿ��
        /// </summary>
        /// <param name="_ItemName"></param>
        /// <returns></returns>
        public CEnumItemBase<TKey> this[TKey _ItemName]
        {
            get
            { return this.Items[_ItemName]; }
        }

        /// <summary>
        /// ��TKey��ö�����͵�ʱ��,���Ը���ö�ٵ�ֵ����ö�Ӧ��CEnumItemBase����.
        /// ���_nEnum�Ҳ�����Ӧ��ö��ֵ�򷵻�null
        /// </summary>
        /// <param name="_nEnum"></param>
        /// <returns></returns>
        public CEnumItemBase<TKey> GetItemByEnumValue(int _nEnum)
        {
            try
            {
                return this[(TKey)Enum.Parse(typeof(TKey), _nEnum.ToString())];
            }
            catch
            {
                return null;
            }
        }//GetItemByEnumValue

        /// <summary>
        /// �ж��Ƿ����_EnumName����.
        /// </summary>
        /// <param name="_EnumName"></param>
        /// <returns></returns>
        public bool ContainsEnumKey(TKey _EnumName)
        {
            return this.Items.ContainsKey(_EnumName);
        }

        /// <summary>
        /// �����е�EnumItems��Ѱ���Ƿ��е�Value==_EnumValue�������м�����ֻҪ��һ���ͷ���true
        /// </summary>
        /// <param name="_EnumValue"></param>
        /// <returns></returns>
        public bool ContainsEnumValue(string _EnumValue)
        {
            foreach (KeyValuePair<TKey, CEnumItemBase<TKey>> crrPair in this.Items)
            {
                if (crrPair.Value.ValueStr == _EnumValue)
                    return true;
            }

            return false;
        }//ContainsEnumValue()

        /// <summary>
        /// ���ݸ������ַ����������. ֻ��_ParseTargetStr��ĳ��ö����ķ����ַ���ȫ��ͬ��ʱ��Żᱻ��Ϊ��ƥ��ġ�
        /// �������Ҫ����EnumItem��ĳ������˳����еĻ�������Ҫ�������ر�����--����EnumItem�ķ����ֶ������ơ�Bb���͡�ABb����ʱ�򣬱������Ƚ���ABB�����򽫵�����Զ����Bb���ȱ�ƥ�䡣
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns>���û���ҵ����ʵķ������ͣ��򷵻�null</returns>
        public virtual TKey Reflect_Equal(string _ParseTargetStr)
        {
            foreach (KeyValuePair<TKey, CEnumItemBase<TKey>> crrPair in this.Items)
            {
                if (crrPair.Value.RefectNames.Contains(_ParseTargetStr))
                    return crrPair.Key ;
            }

            return default(TKey);
        }//Reflect_Equal

        /// <summary>
        /// ���ݸ������ַ����������. ֻҪ_ParseTargetStr�а����� ĳ����������Ļ�����������Ϊ_ParseTargetStr��ĳ������
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns>���û���ҵ����ʵķ������ͣ��򷵻�null</returns>
        public virtual TKey Reflect_Include(string _ParseTargetStr)
        {
            foreach (KeyValuePair<TKey, CEnumItemBase<TKey>> crrPair in this.Items)
            {
                foreach( string crrStr in crrPair.Value.RefectNames )
                {
                    if (_ParseTargetStr.IndexOf(crrStr) >= 0 )
                    return crrPair.Key;
                }
            }

            return default(TKey);
        }//Reflect_Equal

        /// <summary>
        /// ���ص�ǰö�������е�UnknowE�������û�У���Ĭ�Ϸ���null
        /// CEnum��ǰ�ṩĬ�ϲ���"UnknowE"|| "eUnknow"||"Unknow"����������Ĭ��ֵ�����⼸���Ļ�������Ҫ�Լ����ر�������
        /// </summary>
        public virtual CEnumItemBase<TKey> UnknowItem
        {
            get
            {
                //if (this.ContainsEnumKey("UnknowE"))
                //    return this["UnknowE"];
                //else if (this.ContainsEnumKey("eUnknow"))
                //    return this["eUnknow"];
                //else if (this.ContainsEnumKey("Unknow"))
                //    return this["Unknow"];
                //else
                    return null;
            }
        }//UnknowItem

        /// <summary>
        /// ���ص�ǰö�������е�NoneE�������û�У���Ĭ�Ϸ���null
        /// CEnum��ǰ�ṩĬ�ϲ���"NoneE"|| "eNone"||"None"����������Ĭ��ֵ�����⼸���Ļ�������Ҫ�Լ����ر�������
        /// </summary>
        public virtual CEnumItemBase<TKey> NoneItem
        {
            get
            {
                //if (this.ContainsEnumKey("NoneE"))
                //    return this["NoneE"];
                //else if (this.ContainsEnumKey("eNone"))
                //    return this["eNone"];
                //else if (this.ContainsEnumKey("None"))
                //    return this["None"];
                //else
                    return null;
            }
        }//UnknowItem

        /// <summary>
        /// ����Ԥ�����EnumItems���ɸ������ฺ��ʵ�ָ÷��������ڴ�Ӳ����������ļ��м���EnumItems
        /// </summary>
        /// <returns>�����Ƿ�ɹ�</returns>
        public virtual bool LoadEnumItems()
        {
            return true;
        }//LoadEnumItems()

        public virtual List<TKey> AllKeysList
        {
            get
            {
                List<TKey> rstList = new List<TKey>();
                foreach (TKey crrKey in this.Items.Keys)
                    rstList.Add(crrKey);

                return rstList;
            }
        }//List<TKey> AllKeysList

        public CEnumItemBase<TKey> AddItem(CEnumItemBase<TKey> _newItem)
        {
            this.Items.Add(_newItem.KeyObj, _newItem);
            return _newItem;
        }
    }//CEnumMgr
}//namespace
