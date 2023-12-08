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
    /// CCompileInfo的实体类。 是对于某种编译结果信息的真正对象。
    /// </summary>
    public class CCompileInfoIns
    {
        /// <summary>
        /// 唯一表示的编译信息ID
        /// </summary>
        public string IdStr = "";

        /// <summary>
        /// 按照信息生成的顺序所给出的序号。这个序号通常由CCompileInfoIns.Add()自动分配
        /// </summary>
        public int Number = 0;

        /// <summary>
        /// 本条编译信息自身的的文字描述说明。他通常用于临时自定义的编译提示。而预定义的信息一般建议直接引用InfoDefine.DescStr
        /// </summary>
        public string DescSelfStr = "";

        /// <summary>
        /// 他首先由本信息的描述，后面加上来自问题定义中的表述和修改意见共同组成。或者直接引用信息定义中的描述。
        /// </summary>
        public string DescWithDefine
        {
            get { return DescSelfStr + InfoDefine.DescStr; }
        }

        /// <summary>
        /// 本条编译信息的定义描述。
        /// </summary>
        public CCompileInfoDefine InfoDefine;

        /// <summary>
        /// 所有与本编译信息相关联的被编译对象。以便用户可以快速定位到问题所在位置
        /// </summary>
        public Dictionary<string, Object> InvovedObjs = new Dictionary<string, object>();
 
        /// <summary>
        /// 本函书初始化一个自定的编译信息对象。
        /// </summary>
        /// <param name="_IsError">True=eError, False=eWarrning,不支持eSuggestion</param>
        /// <param name="_DescSelfStr"></param>
        /// <returns></returns>
        public  static CCompileInfoIns GenerateSelfDefinInfoIns(
            bool _IsError, string _IdStr , string _DescSelfStr)
        {
            CCompileInfoDefine tpInfoDefine = new CCompileInfoDefine();

            if (_IsError)
                tpInfoDefine.InfoType = (int)ECompileInfo.eError;
            else
                tpInfoDefine.InfoType = (int)ECompileInfo.eWarrning;

            CCompileInfoIns infoIns = new CCompileInfoIns();

            infoIns.InfoDefine = tpInfoDefine;

            infoIns.IdStr = _IdStr;

            infoIns.DescSelfStr = _DescSelfStr;

            return infoIns; 

        }//GenerateSelfDefinInfoIns

        public override string ToString()
        {
            return "[" + Enum.Parse(typeof(ECompileInfo) , this.InfoDefine.InfoType.ToString()).ToString() 
                + "]\t[" + Enum.Parse(typeof(ECISeverity), this.InfoDefine.Severity.ToString()).ToString() + "]\t" + this.DescWithDefine;
        }

    }//  class CCompileInfoIns
}//namespace
