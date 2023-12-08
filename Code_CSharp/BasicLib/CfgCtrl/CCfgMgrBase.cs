using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.CfgCtrl
{
    /// <summary>
    /// 一个配置管理器，其中管理着当前域下属的所有配置。
    /// </summary>
    public class CCfgMgrBase : ICfgContainer
    {
        /// <summary>
        /// 所有下属的配置
        /// </summary>
        protected readonly Dictionary<string, CCfgItem> CfgItems 
            = new Dictionary<string, CCfgItem>();

        #region ICfgContainer Members

        /// <summary>
        /// 根据_cfgKey返回相应的值。找不到对应 _cfgKey的时候返回null
        /// </summary>
        /// <param name="_cfgKey"></param>
        /// <returns></returns>
        CCfgItem ICfgContainer.GetCfgItem(string _cfgKey)
        {
            if (CfgItems.ContainsKey(_cfgKey))
                return CfgItems[_cfgKey];
            else
                return null;
        }

        /// <summary>
        /// 设置一个CCfgItem。以cfgKey作为比较的标准。
        /// 如果以前没有对应的Key，则新增一个配置向。
        /// 如果已经存在一个Key，则直接更新其中的内容。
        /// </summary>
        /// <param name="_cfgItem"></param>
        /// <returns></returns>
        CCfgItem ICfgContainer.SetCfgItem(CCfgItem _cfgItem)
        {
            //CCfgItem rstItem = null;
            if (CfgItems.ContainsKey(_cfgItem.KeyIdStr))
                return CfgItems[_cfgItem.KeyIdStr] = _cfgItem;
            else
                this.CfgItems.Add(_cfgItem.KeyIdStr, _cfgItem);

            return this.CfgItems[_cfgItem.KeyIdStr];

        }

        /// <summary>
        /// 设置一个CCfgItem。以cfgKey作为比较的标准。
        /// 如果以前没有对应的Key，则新增一个配置向。
        /// 如果已经存在一个Key，则直接更新其中的内容。
        /// 这个方法返回的CCfgItem是在方法内部新生成的对象.
        /// </summary>
        /// <param name="_KeyIdStr"></param>
        /// <param name="_DescStr"></param>
        /// <returns></returns>
         CCfgItem ICfgContainer.SetCfgItem(string _KeyIdStr, string _DescStr)
        {
            if (CfgItems.ContainsKey(_KeyIdStr ))
                CfgItems[_KeyIdStr].StrValue = _DescStr;
            else
                this.CfgItems.Add(_KeyIdStr, new CCfgItem( _KeyIdStr, _DescStr ));

            return this.CfgItems[_KeyIdStr];
        }


        /// <summary>
        /// 删除一个配置项。
        /// </summary>
        /// <param name="_cfgKey"></param>
        /// <returns></returns>
        void ICfgContainer.RemoveCfgKey(string _cfgKey)
        {
            //CCfgItem rstItem = null;
            if (CfgItems.ContainsKey(_cfgKey))
                 CfgItems.Remove(_cfgKey);

        }


        /// <summary>
        /// 测试是否存在配置项
        /// </summary>
        /// <param name="_cfgKey"></param>
        /// <returns></returns>
        bool ICfgContainer.ContainsKey(string _cfgKey)
        {
            return this.CfgItems.ContainsKey(_cfgKey);
        }


        #endregion
    }//class CCfgMgr
}//namespace
