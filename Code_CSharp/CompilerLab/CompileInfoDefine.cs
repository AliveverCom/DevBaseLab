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
    /// ������Ϣ�ġ�������Ϣ��
    /// </summary>
    public class CCompileInfoDefine
    {
        /// <summary>
        /// Ψһ�̱����IdStr, �磺E00001, W003232�ȵ�
        /// </summary>
        public string IdStr = "";

        /// <summary>
        /// �õ�ŷָ���ַ�������ʾÿ��������Ϣ��ε�������ϵ����: ViewExport.WordDexigner.SomeError...
        /// </summary>
        public string TraceStrU = "";

        /// <summary>
        /// ����Ϣ�ı������ơ� �������ΪShortName
        /// </summary>
        public string NameStr = "�Զ�����Ϣ";

        /// <summary>
        /// ��������Ϣ����ı�����Ϣ��ͨ���������������ԭ���Լ�����취����
        /// </summary>
        public string DescStr = "";

        /// <summary>
        /// ������Ϣ������
        /// </summary>
        public int InfoType = (int)ECompileInfo.eError;

        /// <summary>
        /// ������Ϣ�����صȼ�
        /// </summary>
        public int Severity = (int)ECISeverity.eNormal;

        #region predefined InfoDef
        public static readonly CCompileInfoDefine BlockerError = new CCompileInfoDefine()
        {
            IdStr = "PD_0100",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eBlocker,
            NameStr = "BlockerError",
            DescStr = "�����Դ��󡣳������������ǻ�ֱ�ӵ��±����쳣��ֹ�����ұ�������Ч��ͨ�������Դ����д���"
        };

        public static readonly CCompileInfoDefine CriticalError = new CCompileInfoDefine()
        {
            IdStr = "PD_0200",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eCritical,
            NameStr = "CriticalError",
            DescStr = "���ش��󡣳������������ǻ�ֱ�ӵ��±�����ֹ�����ұ�������Ч�������Ǳ���������û�а���ģ����д�����±����޷���չ�������"
        };

        public static readonly CCompileInfoDefine MajorError = new CCompileInfoDefine()
        {
            IdStr = "PD_0300",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eMajor,
            NameStr = "MajorError",
            DescStr = "��Ҫ���󡣴������صȼ��ϸߣ������н���ֹ����������Ч������С��Χ�ο��ͶԱ�ʹ�á�"
        };

        public static readonly CCompileInfoDefine NormalError = new CCompileInfoDefine()
        {
            IdStr = "PD_0400",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalError",
            DescStr = "һ���Դ��󡣱��벻����ֹ������������֤��ȫ���ã������ο���"
        };

        public static readonly CCompileInfoDefine MinorError = new CCompileInfoDefine()
        {
            IdStr = "PD_0500",
            InfoType = (int)ECompileInfo.eError,
            Severity = (int)ECISeverity.eMinor,
            NameStr = "NormalError",
            DescStr = "��΢���󡣱��벻����ֹ�����������á�"
        };

        public static readonly CCompileInfoDefine CriticalWarrning = new CCompileInfoDefine()
        {
            IdStr = "PD_0600",
            InfoType = (int)ECompileInfo.eWarrning,
            Severity = (int)ECISeverity.eCritical,
            NameStr = "NormalWarrning",
            DescStr = "���ؾ��档���벻����ֹ�����ܿ��ܽ��������Ĵ���ᷢ��������Review���������ͽ������"
        };

        public static readonly CCompileInfoDefine NormalWarrning = new CCompileInfoDefine()
        {
            IdStr = "PD_0700",
            InfoType = (int)ECompileInfo.eWarrning,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalWarrning",
            DescStr = "�����Ծ��档�����������С�"
        };

        public static readonly CCompileInfoDefine NormalSuggestion = new CCompileInfoDefine()
        {
            IdStr = "PD_0800",

            InfoType = (int)ECompileInfo.eSuggestion,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalSuggestion",
            DescStr = "һ���Խ��顣"
        };

        public static readonly CCompileInfoDefine NormalMessage = new CCompileInfoDefine()
        {
            IdStr = "PD_0900",
            InfoType = (int)ECompileInfo.eMessage,
            Severity = (int)ECISeverity.eNormal,
            NameStr = "NormalMessage",
            DescStr = "��ͨ��Ϣ��"
        };



        #endregion //predefined InfoDef
    }//CCompileInfoDefine
}//namespace
