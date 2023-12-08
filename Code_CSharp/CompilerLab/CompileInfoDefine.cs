///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-11-17</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>

using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.Compiler
{
    /// <summary>
    /// 编译信息的“定义信息”
    /// </summary>
    public class CCompileInfoDefine
    {
        /// <summary>
        /// 唯一短编码的IdStr, 如：E00001, W003232等等
        /// </summary>
        public string IdStr = "";

        /// <summary>
        /// 用点号分割的字符串，表示每个编译信息层次的所属关系。如: ViewExport.WordDexigner.SomeError...
        /// </summary>
        public string TraceStrU = "";

        /// <summary>
        /// 该信息的标题名称。 可以理解为ShortName
        /// </summary>
        public string NameStr = "自定义信息";

        /// <summary>
        /// 本编译信息定义的表述信息。通常描述问题产生的原因，以及解决办法或建议
        /// </summary>
        public string DescStr = "";

        /// <summary>
        /// 编译信息的类型
        /// </summary>
        public int InfoType = (int)ECompileInfo.eError;

        /// <summary>
        /// 编译信息的严重等级
        /// </summary>
        public int Severity = (int)ECISeverity.eNormal;

        #region predefined InfoDef
        public static readonly CCompileInfoDefine BlockerError = new CCompileInfoDefine()
        {
            IdStr = "PD_0100",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eBlocker,
            NameStr = "BlockerError",
            DescStr = "崩溃性错误。出现这种问题是会直接导致编译异常中止，并且编译结果无效。通常是明显代码有错误。"
        };

        public static readonly CCompileInfoDefine CriticalError = new CCompileInfoDefine()
        {
            IdStr = "PD_0200",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eCritical,
            NameStr = "CriticalError",
            DescStr = "严重错误。出现这种问题是会直接导致编译终止，并且编译结果无效。可能是被解析对象没有按照模板书写，导致编译无法开展或继续。"
        };

        public static readonly CCompileInfoDefine MajorError = new CCompileInfoDefine()
        {
            IdStr = "PD_0300",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eMajor,
            NameStr = "MajorError",
            DescStr = "重要错误。错误严重等级较高，编译有将终止，编译结果无效，仅供小范围参考和对比使用。"
        };

        public static readonly CCompileInfoDefine NormalError = new CCompileInfoDefine()
        {
            IdStr = "PD_0400",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalError",
            DescStr = "一般性错误。编译不会终止，编译结果不保证完全可用，仅供参考。"
        };

        public static readonly CCompileInfoDefine MinorError = new CCompileInfoDefine()
        {
            IdStr = "PD_0500",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eMinor,
            NameStr = "NormalError",
            DescStr = "轻微错误。编译不会终止，编译结果可用。"
        };

        public static readonly CCompileInfoDefine CriticalWarrning = new CCompileInfoDefine()
        {
            IdStr = "PD_0600",
            InfoType = (int)ECompileInfo.eWarrning,
            Severity = (int)ECISeverity.eCritical,
            NameStr = "NormalWarrning",
            DescStr = "严重警告。编译不会终止，但很可能将有隐含的错误会发生。建议Review被处理对象和结果集。"
        };

        public static readonly CCompileInfoDefine NormalWarrning = new CCompileInfoDefine()
        {
            IdStr = "PD_0700",
            InfoType = (int)ECompileInfo.eWarrning,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalWarrning",
            DescStr = "正常性警告。编译正常进行。"
        };

        public static readonly CCompileInfoDefine NormalSuggestion = new CCompileInfoDefine()
        {
            IdStr = "PD_0800",

            InfoType = (int)ECompileInfo.eSuggestion,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalSuggestion",
            DescStr = "一般性建议。"
        };

        public static readonly CCompileInfoDefine NormalMessage = new CCompileInfoDefine()
        {
            IdStr = "PD_0900",
            InfoType = (int)ECompileInfo.eMessage,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalMessage",
            DescStr = "普通信息。"
        };



        #endregion //predefined InfoDef
    }//CCompileInfoDefine
}//namespace
