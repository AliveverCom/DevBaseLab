using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.com.MySqlLib.XDStatistic
{
    /// <summary>
    /// 定义每一个Value列
    /// </summary>
    class CValueColumnDef
    {
        /// <summary>
        /// 当前这一列的名称
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 当前这一列的SQL。 通常是 select xxx from xxx where xxx ，其后的部分由各维度组合自动拼合。
        /// 如果这个值为“空”，则表示这一列是间隔符，不需要执行。
        /// </summary>
        public string SqlStr = string.Empty;
    }//CValueColumnDef
}
