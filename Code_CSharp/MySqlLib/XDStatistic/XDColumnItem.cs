using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.com.MySqlLib.XDStatistic
{
    /// <summary>
    /// 多维交查询矩阵上的一个结果单元。
    /// 这个单元既可以代表 某个维度单元，由可以代表某个结果单元 。 这个单元包含着自己独立的名字、SQL、Value 
    /// </summary>
    class CXDColumnItem
    {
        /// <summary>
        /// 被统计字段的名称
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 需要执行的SQL语句。 初始化的时候是 SQL语句的头部。 运行时再拼接后面的where 条件。
        /// 当运行时这个值等于string.Empty时，代表当前def不是一个可执行的定义。通常可能是一个间隔符。
        /// </summary>
        public string SqlStr = string.Empty;

        /// <summary>
        /// 以string形式展现的查询结果
        /// </summary>
        public string ValueStr = "未执行";

    }//class CValueColumn
}//namespace
