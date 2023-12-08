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
    /// 编译器类型注册器。
    /// </summary>
    public abstract class CCompilerRegister
    {
        /// <summary>
        /// 本注册器下属的各类视图
        /// </summary>
        public Dictionary<string, CCompilerRegisterItem> Items = new Dictionary<string, CCompilerRegisterItem>();

        /// <summary>
        /// 本注册器下属的各类视图模板
        /// </summary>
        public Dictionary<string, CCompilerTpl> ItemTpls = new Dictionary<string, CCompilerTpl>();

        /// <summary>
        /// 初始化本注册其中所有支持的试图类型和模板类型。
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, CCompilerRegisterItem> InitHardCodeItems();

    }//class CompilerRegister
}//namespace
