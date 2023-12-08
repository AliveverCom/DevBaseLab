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
    /// ����������ע������
    /// </summary>
    public abstract class CCompilerRegister
    {
        /// <summary>
        /// ��ע���������ĸ�����ͼ
        /// </summary>
        public Dictionary<string, CCompilerRegisterItem> Items = new Dictionary<string, CCompilerRegisterItem>();

        /// <summary>
        /// ��ע���������ĸ�����ͼģ��
        /// </summary>
        public Dictionary<string, CCompilerTpl> ItemTpls = new Dictionary<string, CCompilerTpl>();

        /// <summary>
        /// ��ʼ����ע����������֧�ֵ���ͼ���ͺ�ģ�����͡�
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, CCompilerRegisterItem> InitHardCodeItems();

    }//class CompilerRegister
}//namespace
