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
    /// 设计器视图的基本信息定义类
    /// </summary>
    public class CCompilerRegisterItem
    {
        /// <summary>
        /// 唯一ID标示
        /// </summary>
        public string IdStr = string.Empty;

        /// <summary>
        /// 本对象的名称。可以为空值
        /// </summary>
        public string NameStr = string.Empty;

        /// <summary>
        ///  对象的描述说明
        /// </summary>
        public string DescStr = string.Empty;

        
        /// <summary>
        /// 该视图下属的所有模板[string IdStr, CCompilerTpl]
        /// </summary>
        public Dictionary<string, CCompilerTpl> Tpls = new Dictionary<string,CCompilerTpl>();

        /// <summary>
        /// 根据指定的编译器类型的到相应的模板[string IdStr,CCompilerTpl ]
        /// </summary>
        /// <param name="_eTplFunType"></param>
        /// <returns></returns>
        public Dictionary<string, CCompilerTpl> GetTpls(ETplFunType _eTplFunType)
        {
            Dictionary<string, CCompilerTpl> RstMap = new Dictionary<string, CCompilerTpl>();

            foreach (KeyValuePair<string, CCompilerTpl> crrPair in this.Tpls)
            {
                if (crrPair.Value.TplFunTypeE == _eTplFunType)
                    RstMap.Add(crrPair.Key, crrPair.Value);
            }

            return RstMap;
        }//GetTpls()

        /// <summary>
        /// 根据指定的编译器类型的到唯一的默认模板
        /// </summary>
        /// <param name="_eTplFunType"></param>
        /// <returns></returns>
        public CCompilerTpl GetDefaultTpl(ETplFunType _eTplFunType)
        {
            foreach (KeyValuePair<string, CCompilerTpl> crrPair in this.Tpls)
            {
                if (crrPair.Value.TplFunTypeE == _eTplFunType
                    && crrPair.Value.IsDefultTpl)
                    return crrPair.Value;
            }

            return null;
        }//GetDefaultTpl()



    }//class CCompilerRegisterItem
}//namespace
