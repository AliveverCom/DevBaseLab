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
    /// 编译信息的类型定义
    /// </summary>
    public enum ECompileInfo
    {
        /// <summary>
        /// 未知. 信息类型没有被指派。
        /// </summary>
        eUnknow = 0 ,

        /// <summary>
        /// 崩溃性错误。出现这种问题是会直接导致编译异常中止，并且编译结果无效。
        /// </summary>
        //eCrash = 10, 

        /// <summary>
        /// 编译中出现的错误，可能导致结果集不完整或不完全正确，但并不需要中止，编译还可以继续进行。。
        /// </summary>
        eError = 20,

        /// <summary>
        /// 警告。虽然编译还可以继续，但是有可能引发各类潜在的问题，或者严重影响到今后的工作。
        /// </summary>
        eWarrning = 30,

        /// <summary>
        /// 建议。对于一些常规经验性的地方，提出一些改进建议，以便可以让今后的工作做得更好或更完美。
        /// </summary>
        eSuggestion = 40,

        /// <summary>
        /// 没有任何特定含义的消息。级别比eSuggestion更低。它可能是任何编译中发生的信息。
        /// </summary>
        eMessage = 50

    }//enum ECompileInfo

    public class CECompileInfo : CEnumMgrBase<ECompileInfo>
    {
       // private static CECompileInfo m_ins = new CECompileInfo();

        public static readonly CECompileInfo Ins = new CECompileInfo();

        protected  CECompileInfo() :base()
        {
            this.NameStr = "ECompileInfo";

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eError,
                "编译中出现的错误，可能导致结果集不完整或不完全正确，但并不需要中止，编译还可以继续进行",
                ECompileInfo.eError.ToString(),
                "Error", "Err", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eMessage,
                "没有任何特定含义的消息。级别比eSuggestion更低。它可能是任何编译中发生的信息。",
                ECompileInfo.eError.ToString(),
                "Message", "Msg", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eSuggestion,
                "建议。对于一些常规经验性的地方，提出一些改进建议，以便可以让今后的工作做得更好或更完美。",
                ECompileInfo.eError.ToString(),
                "Suggestion", "Sugg", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eUnknow,
                " 未知. 信息类型没有被指派。",
                ECompileInfo.eError.ToString(),
                "Unknow", "Unknow", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eWarrning,
                "警告。虽然编译还可以继续，但是有可能引发各类潜在的问题，或者严重影响到今后的工作",
                ECompileInfo.eError.ToString(),
                "Warrning", "Warr", string.Empty)
                );

        }
    }//class CECompileInfo
}//namespace
