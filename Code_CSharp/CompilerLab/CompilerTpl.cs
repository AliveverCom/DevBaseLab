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
    /// һ����ͼ���Զ�һ�ֵ���ģ���һ�ֵ���ģ��
    /// ÿ��ģ�嶼��Ӧ��һ��ģ���ļ����Լ�һ������ͼ
    /// </summary>
    public class CCompilerTpl
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
        /// ��ģ���ͼ����Դ���ƣ����ļ���
        /// </summary>
        public string ImageSrcStr = string.Empty;

        /// <summary>
        /// ģ���ļ�����Դ���ƣ����ļ���
        /// </summary>
        public string TplFileSrcStr = string.Empty;

        /// <summary>
        /// �ļ���չ��
        /// </summary>
        public string FileExtendStr = string.Empty;

        /// <summary>
        /// ��ģ���Ƿ���Ĭ�ϵ�ģ��
        /// </summary>
        public bool IsDefultTpl = false;

        /// <summary>
        /// �ñ������Ĺ�������
        /// </summary>
        public ETplFunType TplFunTypeE = ETplFunType.eUnknow;
    }//class CompilerTpl
}//namespace
