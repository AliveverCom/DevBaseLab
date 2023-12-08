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
    /// �������ͼ�Ļ�����Ϣ������
    /// </summary>
    public class CCompilerRegisterItem
    {
        /// <summary>
        /// ΨһID��ʾ
        /// </summary>
        public string IdStr = string.Empty;

        /// <summary>
        /// ����������ơ�����Ϊ��ֵ
        /// </summary>
        public string NameStr = string.Empty;

        /// <summary>
        ///  ���������˵��
        /// </summary>
        public string DescStr = string.Empty;

        
        /// <summary>
        /// ����ͼ����������ģ��[string IdStr, CCompilerTpl]
        /// </summary>
        public Dictionary<string, CCompilerTpl> Tpls = new Dictionary<string,CCompilerTpl>();

        /// <summary>
        /// ����ָ���ı��������͵ĵ���Ӧ��ģ��[string IdStr,CCompilerTpl ]
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
        /// ����ָ���ı��������͵ĵ�Ψһ��Ĭ��ģ��
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
