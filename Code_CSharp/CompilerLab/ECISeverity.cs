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
    /// Compile Info Severity. ������Ϣ�����صȼ���������صȼ��ο���bugzilla�����صȼ��趨
    /// </summary>
    public enum ECISeverity
    {
        /// <summary>
        /// ��ɲ���ϵͳ����������ļ�����
        /// </summary>
        eBlocker= 0 ,

        /// <summary>
        /// ���������������޷���������
        /// </summary>
        eCritical = 100,

        /// <summary>
        /// ������ش�������Ӱ������ʹ��
        /// </summary>
        eMajor = 200,

        /// <summary>
        /// һ��������
        /// </summary>
        eNormal = 300,

        /// <summary>
        /// ��΢������
        /// </summary>
        eMinor = 400,

        /// <summary>
        /// ���������
        /// </summary>
        eTrivial = 500

    }//enum ECISeverity

    /// <summary>
    /// ���ECISeverity���ַ���������
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
                "���صȼ�Ϊ������ͨ���Ƿ����˿��ܵ������ر����������޷��������е��������⡣��Ҫ���������",
                ECISeverity.eBlocker.ToString(),
                "Blocker", "Blk", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eCritical,
                "���صȼ�Ϊ������ͨ���Ƿ���������Ӱ����������������������⡣�������������",
                ECISeverity.eCritical.ToString(),
                "Critical", "Crtl", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eMajor,
                "���صȼ�Ϊ��Ҫ��ͨ���Ƿ�����Ӱ���������ʹ�õĽ��������⡣�������Ƚ��",
                ECISeverity.eMajor.ToString(),
                "Major", "Mjr", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eMinor,
                "���صȼ�Ϊ��΢����Ӱ���������ʹ�ã�����Ҫ�Ľ������⡣",
                ECISeverity.eMinor.ToString(),
                "eMinor", "Mnr", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eNormal,
                "���صȼ�Ϊһ�㡣ʹ���е�һ����bug,�ɰ����������ȼ�����",
                ECISeverity.eNormal.ToString(),
                "Normal", "Nml", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECISeverity>
                (
                ECISeverity.eTrivial,
                "���صȼ�Ϊ���顣��Ӱ��ϵͳ�������У�ͨ�����Ǹ���ͻ���С��Թ�����޾���������ʱ����ᡣ",
                ECISeverity.eTrivial.ToString(),
                "Trivial", "Tvl", string.Empty)
                );

        }//CECISeverity()
    }//class CECISeverity
}//namespace
