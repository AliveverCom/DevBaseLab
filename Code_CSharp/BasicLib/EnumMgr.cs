///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2010-02-12</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog>切换工程：将这个文件从CreatBone.InfoSysBuilder.Generic移动到Alivever.Com.DevBasic.BasicLib.</ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib
{
    /// <summary>
    /// 动态枚举类型。用于管理动态配置的枚举类型。对于真正用到的地方，
    /// 需要自己在继承类中提供一个static m_Ins  以及 static Ins{get;}所有对外函数都是静态的。
    /// </summary>
    public class CEnumMgrBase<TKey>
    {
        /// <summary>
        /// 当前这个枚举类型的名字。极力推荐为全局唯一的名称。至少在当前程序集中是唯一的
        /// </summary>
        public string NameStr = string.Empty;

        /// <summary>
        /// 当前枚举的说明
        /// </summary>
        public string DescStr = string.Empty;

        private Dictionary<TKey, CEnumItemBase<TKey>> m_Items = new Dictionary<TKey, CEnumItemBase<TKey>>();

        public Dictionary<TKey, CEnumItemBase<TKey>> Items { get { return m_Items; } }

        /// <summary>
        /// 对外严禁使用无名的每局类型
        /// </summary>
        protected CEnumMgrBase()
        {
            
        }

        /// <summary>
        /// 构造函数，必须声明NameStr和DescStr
        /// </summary>
        /// <param name="_NameStr"></param>
        /// <param name="_DescStr"></param>
        public CEnumMgrBase(string _NameStr, string _DescStr)
        {
            this.NameStr = _NameStr;
            this.DescStr = _DescStr;
        }

        ///// <summary>
        ///// 将所有元素以一个List的形式进行返回。这里的List是临时生成的，不是唯一的永久对象
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
        /// 跟据某个每局
        /// </summary>
        /// <param name="_ItemName"></param>
        /// <returns></returns>
        public CEnumItemBase<TKey> this[TKey _ItemName]
        {
            get
            { return this.Items[_ItemName]; }
        }

        /// <summary>
        /// 当TKey是枚举类型的时候,可以根据枚举的值来获得对应的CEnumItemBase对象.
        /// 如果_nEnum找不到对应的枚举值则返回null
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
        /// 判定是否包含_EnumName对象.
        /// </summary>
        /// <param name="_EnumName"></param>
        /// <returns></returns>
        public bool ContainsEnumKey(TKey _EnumName)
        {
            return this.Items.ContainsKey(_EnumName);
        }

        /// <summary>
        /// 在所有的EnumItems中寻找是否有的Value==_EnumValue。不论有几个，只要有一个就返回true
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
        /// 根据给定的字符串反射解析. 只有_ParseTargetStr与某个枚举项的反射字符完全相同的时候才会被认为是匹配的。
        /// 如果解析要求按照EnumItem的某种排列顺序进行的话，则需要子类重载本方法--比如EnumItem的反射字段有类似“Bb”和“ABb”的时候，必须首先解析ABB，否则将导致永远都是Bb最先被匹配。
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns>如果没有找到合适的反射类型，则返回null</returns>
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
        /// 根据给定的字符串反射解析. 只要_ParseTargetStr中包含有 某个反射词条的话，就立刻认为_ParseTargetStr是某种类型
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns>如果没有找到合适的反射类型，则返回null</returns>
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
        /// 返回当前枚举类型中的UnknowE对象。如果没有，则默认返回null
        /// CEnum当前提供默认查找"UnknowE"|| "eUnknow"||"Unknow"。如果子类的默认值不是这几个的话，则需要自己重载本方法。
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
        /// 返回当前枚举类型中的NoneE对象。如果没有，则默认返回null
        /// CEnum当前提供默认查找"NoneE"|| "eNone"||"None"。如果子类的默认值不是这几个的话，则需要自己重载本方法。
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
        /// 加载预定义的EnumItems。由各个子类负责实现该方法，用于从硬编码或配置文件中加载EnumItems
        /// </summary>
        /// <returns>返回是否成功</returns>
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
