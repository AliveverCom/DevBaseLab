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

using Alivever.Com.DevBasic.BasicLib;

namespace Alivever.Com.Compiler
{
    /// <summary>
    /// Compile Info Severity. 编译信息的严重等级。这个严重等级参考了bugzilla的严重等级设定
    /// </summary>
    public enum ECISeverity
    {
        /// <summary>
        /// 造成操作系统崩溃或程序文件受损
        /// </summary>
        eBlocker= 0 ,

        /// <summary>
        /// 软件自身崩溃，或无法继续运行
        /// </summary>
        eCritical = 100,

        /// <summary>
        /// 造成严重错误，严重影响正常使用
        /// </summary>
        eMajor = 200,

        /// <summary>
        /// 一般性问题
        /// </summary>
        eNormal = 300,

        /// <summary>
        /// 轻微的问题
        /// </summary>
        eMinor = 400,

        /// <summary>
        /// 琐碎的问题
        /// </summary>
        eTrivial = 500

    }//enum ECISeverity

    /// <summary>
    /// 针对ECISeverity的字符串管理类
    /// </summary>
    public class CECISeverity : CEnumMgrBase<ECISeverity>
    {
        // private static CECompileInfo m_ins = new CECompileInfo();

        public static readonly CECISeverity Ins = new CECISeverity();

        protected CECISeverity()
            : base()
        {
            this.NameStr = "ECISeverity";

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eBlocker,
                "严重等级为崩溃。通常是发现了可能导致严重崩溃、程序无法继续进行的严重问题。需要立即解决。",
                ECISeverity.eBlocker.ToString(),
                "Blocker", "Blk", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eCritical,
                "严重等级为紧急。通常是发现了严重影响程序正常继续的严重问题。建议立即解决。",
                ECISeverity.eCritical.ToString(),
                "Critical", "Crtl", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eMajor,
                "严重等级为重要。通常是发现了影响程序正常使用的较严重问题。建议优先解决",
                ECISeverity.eMajor.ToString(),
                "Major", "Mjr", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eMinor,
                "严重等级为轻微。不影响程序正常使用，但需要改进的问题。",
                ECISeverity.eMinor.ToString(),
                "eMinor", "Mnr", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eNormal,
                "严重等级为一般。使用中的一般性bug,可按照正常优先级处理。",
                ECISeverity.eNormal.ToString(),
                "Normal", "Nml", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eTrivial,
                "严重等级为琐碎。不影响系统正常运行，通常仅是个别客户的小抱怨。如无经历可以暂时不理会。",
                ECISeverity.eTrivial.ToString(),
                "Trivial", "Tvl", string.Empty)
                );

        }//CECISeverity()
    }//class CECISeverity
}//namespace
