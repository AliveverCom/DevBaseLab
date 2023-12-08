///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2011-1-14</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.CfgCtrl
{
    /// <summary>
    /// 所有可以存储逗点分割的Key的配置容器都需要支持这个接口。
    /// </summary>
    public interface ICfgContainer
    {
        /// <summary>
        /// 根据_cfgKey取得相应的配置对象。然后调用者可以根据所需的数据类型取出相应的值
        /// </summary>
        /// <param name="_ItemKey"></param>
        /// <returns></returns>
        CCfgItem GetCfgItem(string _cfgKey);

        /// <summary>
        /// 设置一个CCfgItem。以cfgKey作为比较的标准。
        /// 如果以前没有对应的Key，则新增一个配置向。
        /// 如果已经存在一个Key，则直接更新其中的内容。
        /// </summary>
        /// <param name="_cfgItem"></param>
        /// <returns></returns>
        CCfgItem SetCfgItem(CCfgItem _cfgItem);

        /// <summary>
        /// 删除一个配置相。
        /// </summary>
        /// <param name="_cfgKey"></param>
        /// <returns></returns>
        void RemoveCfgKey(string _cfgKey);

        /// <summary>
        /// 测试是否存在配置项
        /// </summary>
        /// <param name="_cfgKey"></param>
        /// <returns></returns>
        bool ContainsKey(string _cfgKey);

        /// <summary>
        /// 设置一个CCfgItem。以cfgKey作为比较的标准。
        /// 如果以前没有对应的Key，则新增一个配置向。
        /// 如果已经存在一个Key，则直接更新其中的内容。
        /// 这个方法返回的CCfgItem是在方法内部新生成的对象.
        /// </summary>
        /// <param name="_KeyIdStr"></param>
        /// <param name="_DescStr"></param>
        /// <returns></returns>
        CCfgItem SetCfgItem(string _KeyIdStr, string _DescStr);

    }//interface ICfgContainer
}//namespace
