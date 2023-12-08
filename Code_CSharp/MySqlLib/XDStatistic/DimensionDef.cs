using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.com.MySqlLib.XDStatistic
{
    /// <summary>
    /// 某个单一维度的定义
    /// </summary>
    class CDimensionDef
    {
        /// <summary>
        /// 被统计字段的名称
        /// </summary>
        public string Name = string.Empty;

        public List<CDimensionItemDef> ItemsDef = new List<CDimensionItemDef>();

    }//CDimensionDef
}//namespace
