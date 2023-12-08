///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-06-02</CreaterDate>
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
    /// 全局的唯一ID管理器。所有ID均为int，从1开始。 Key表示不同的对象/表。
    /// </summary>
    [Serializable]
    public class CIdKeyMgr
    {
        /// <summary>
        /// 所有输入键的最后一次生成的ID值
        /// </summary>
        protected Dictionary<string, int> Key_nID=new Dictionary<string,int>();

        public static CIdKeyMgr Ins = null;

        /// <summary>
        /// 为指定的Key生成一个新的nID，每次返回后nID自动累加
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public int GetNewID(string _key)
        {
            //如果_key不存在，则直接创建一个Key
            if (!this.Key_nID.ContainsKey(_key))
            {
                GSdkMLog.At(GT.pkgName).Write("CIdKeyMgr.GetNewID", 2, "a new _Key is found:" + _key + "Auto added into CIdKeyMgr.\n");
                Key_nID.Add(_key, 0);
            }

            Key_nID[_key] += 1;

            return Key_nID[_key];
        }//int NewID(string _key)

        /// <summary>
        /// 返回该Key上最后一次生成的ID。不累加
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public int GetLastID(string _key)
        {
            //如果_key不存在，则直接创建一个Key
            if (!this.Key_nID.ContainsKey(_key))
            {
                GSdkMLog.At(GT.pkgName).Write("CIdKeyMgr.Key_nID", 2, "a new _Key is found:" + _key + "Auto added into CIdKeyMgr.\n");
                Key_nID.Add(_key, 0);
            }

            return Key_nID[_key];

        }

        /// <summary>
        /// 根据特定的ID前缀返回相应的前缀合成字符串
        /// </summary>
        /// <param name="_PrefixStr">前缀字符串</param>
        /// <returns></returns>
        public string GetNewPrefixId(string _PrefixStr)
        {
            return _PrefixStr + GetNewID(_PrefixStr).ToString();
        }

        /// <summary>
        /// 根据特定的ID前缀返回最后一次生成的前缀合成字符串
        /// </summary>
        /// <param name="_PrefixStr">前缀字符串</param>
        /// <returns></returns>
        public string GetLastPrefixId(string _PrefixStr)
        {
            return _PrefixStr + GetLastID(_PrefixStr).ToString();
        }

    }//class CIdKeyMgr
}
