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
using Alivever.Com.DevBasic.BasicLib.LogCtrl;


namespace Alivever.Com.DevBasic.BasicLib
{
    /// <summary>
    /// CEnumMgrBase中管理的枚举对象。 对于在其他类中的引用，建议使用string， 并且记住NameStr， 对于需要使用 int 的值进行存储的地方，建议使用Value
    /// </summary>
    public class CEnumItemBase<TKey> 
    {
        /// <summary>
        /// 唯一的程序内部枚举名称
        /// </summary>
        public TKey KeyObj ;

        /// <summary>
        /// 当前枚举值得描述
        /// </summary>
        public string DescStr = string.Empty;

        /// <summary>
        /// 当前枚举的值，它可以是任何对象。这个属性不强制唯一，也可以为空，全凭定义者自己
        /// </summary>
        public string ValueStr = string.Empty;

        /// <summary>
        /// 用于程序显示的全长名称。
        /// </summary>
        public string DisplayName = string.Empty;

        /// <summary>
        /// 用于程序显示的短名称。默认初始化的时候应该与DisplayName相同。
        /// </summary>
        public string DisplayShortName = string.Empty;

        /// <summary>
        /// 加速键，用于选择框中的快速定位
        /// </summary>
        public string AccStr = string.Empty;

        /// <summary>
        /// 用于解释文本时候使用。如果被解析的文本等于或拥有该字符串的话，即认为可以转义成当前的枚举类型。
        /// 录入时需要注意反射字符串的插入顺序--比如EnumItem的反射字段有类似“Bb”和“ABb”的时候，必须首先插入ABB。否则将导致永远都是Bb最先被匹配。
        /// 建议配合RefectNamesContains_XXX(string _ParseTargetStr )函数一同使用
        /// </summary>
        public List<string> RefectNames = new List<string>();

        /// <summary>
        /// 外部禁止使用空构造函数，以免发生错误
        /// </summary>
        protected CEnumItemBase()
        {

        }

        /// <summary>
        /// Name & Desc是必须填写的两个属性。不可以为空。以免引起今后开发中的表意混乱
        /// </summary>
        /// <param name="_NameStr"></param>
        /// <param name="_DescStr"></param>
        private CEnumItemBase(TKey _KeyObj, string _DescStr)
        {
            KeyObj = _KeyObj;
            DescStr = _DescStr;
        }

        public CEnumItemBase(TKey _KeyObj, string _DescStr, string _ValueStr)
            : this(_KeyObj, _DescStr)
        {
            ValueStr = _ValueStr;
        }//CEnumItemBase(4)

        public CEnumItemBase(TKey _KeyObj, 
                            string _DescStr,  
                            string _ValueStr, 
                            string _DisplayName, 
                            string _DisplayShortName, 
                            string _AccStr )
            : this(_KeyObj, _DescStr, _ValueStr)
        {
            DisplayName = _DisplayName;
            DisplayShortName = _DisplayShortName;
            AccStr = _AccStr;
        }//CEnumItemBase(4)

        /// <summary>
        /// 默认_ReflactNamesStr中的分隔符使用','
        /// </summary>
        /// <param name="_NameStr"></param>
        /// <param name="_DescStr"></param>
        /// <param name="_ValueStr"></param>
        /// <param name="_DisplayName"></param>
        /// <param name="_DisplayShortName"></param>
        /// <param name="_AccStr"></param>
        /// <param name="_ReflactNamesStr"></param>
        public CEnumItemBase(TKey _KeyObj,
                    string _DescStr,
                    string _ValueStr,
                    string _DisplayName,
                    string _DisplayShortName,
                    string _AccStr,
                    string _ReflactNamesStr )
            : this(_KeyObj, _DescStr, _ValueStr, _DisplayName, _DisplayShortName, _AccStr)
        {
            this.AddRefectNames(',',_ReflactNamesStr);
        }//CEnumItemBase(4)

        /// <summary>
        /// 文本反射解析函数。 如果RefectNames中有完全等于_ParseTargetStr,则返回True
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns></returns>
        public bool RefectNamesContains_Equal(string _ParseTargetStr )
        {
            return RefectNames.Contains(_ParseTargetStr);
        }//RefectNamesContains_Equal

        /// <summary>
        /// 文本反射解析函数。 如果_ParseTargetStr包含有RefectNames中的一个的话,则返回True
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns></returns>
        public bool RefectNamesContains_Include(string _ParseTargetStr)
        {
            foreach (string crrStr in this.RefectNames)
            {
                if (_ParseTargetStr.IndexOf(crrStr) >= 0)
                    return true;
            }

            return false;
        }//RefectNamesContains_Include

        /// <summary>
        /// 向RefectNames中增加Item，所有Items从splitStr中解析出来，然后自动添加到RefectNames中。
        /// 使用这个方法便于快速增加RefectNames。本方法只负责向RefectNames中增加，而不负责清空。
        /// 因此，如果你需要在增加以前清空RefectNames的话，请在调用本函数前自己手动写代码执行。
        /// </summary>
        /// <param name="_seperator">splitStr中区分各个字段的分隔符，这里只能有1种统一的分隔符</param>
        /// <param name="splitStr">由_seperator分割表示的字符串</param>
        /// <returns>返回成功加入的数量，如果小于0，则表示出错</returns>
        public int AddRefectNames( char _seperator, string splitStr)
        {
            string[] NamesArray = splitStr.Split(_seperator);

            if (NamesArray == null)
            {
                //GSdkMLog.At(this.GetType().Assembly.ToString()).
                //    Write(this.GetType().Name + "AddRefectNames()", 1, "splitStr[" + splitStr + "] parse error.\n");

                return 0;
            }

            this.RefectNames.AddRange(NamesArray);

            return NamesArray.Length;

        }//AddRefectNames

    }//class CEnumItemBase
}//namespace
