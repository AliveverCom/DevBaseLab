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
    /// ������Ϣ�����Ͷ���
    /// </summary>
    public enum ECompileInfo
    {
        /// <summary>
        /// δ֪. ��Ϣ����û�б�ָ�ɡ�
        /// </summary>
        eUnknow = 0 ,

        /// <summary>
        /// �����Դ��󡣳������������ǻ�ֱ�ӵ��±����쳣��ֹ�����ұ�������Ч��
        /// </summary>
        //eCrash = 10, 

        /// <summary>
        /// �����г��ֵĴ��󣬿��ܵ��½��������������ȫ��ȷ����������Ҫ��ֹ�����뻹���Լ������С���
        /// </summary>
        eError = 20,

        /// <summary>
        /// ���档��Ȼ���뻹���Լ����������п�����������Ǳ�ڵ����⣬��������Ӱ�쵽���Ĺ�����
        /// </summary>
        eWarrning = 30,

        /// <summary>
        /// ���顣����һЩ���澭���Եĵط������һЩ�Ľ����飬�Ա�����ý��Ĺ������ø��û��������
        /// </summary>
        eSuggestion = 40,

        /// <summary>
        /// û���κ��ض��������Ϣ�������eSuggestion���͡����������κα����з�������Ϣ��
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
                "�����г��ֵĴ��󣬿��ܵ��½��������������ȫ��ȷ����������Ҫ��ֹ�����뻹���Լ�������",
                ECompileInfo.eError.ToString(),
                "Error", "Err", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eMessage,
                "û���κ��ض��������Ϣ�������eSuggestion���͡����������κα����з�������Ϣ��",
                ECompileInfo.eError.ToString(),
                "Message", "Msg", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eSuggestion,
                "���顣����һЩ���澭���Եĵط������һЩ�Ľ����飬�Ա�����ý��Ĺ������ø��û��������",
                ECompileInfo.eError.ToString(),
                "Suggestion", "Sugg", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eUnknow,
                " δ֪. ��Ϣ����û�б�ָ�ɡ�",
                ECompileInfo.eError.ToString(),
                "Unknow", "Unknow", string.Empty)
                );

            this.AddItem(new CEnumItemBase<ECompileInfo>
                (
                ECompileInfo.eWarrning,
                "���档��Ȼ���뻹���Լ����������п�����������Ǳ�ڵ����⣬��������Ӱ�쵽���Ĺ���",
                ECompileInfo.eError.ToString(),
                "Warrning", "Warr", string.Empty)
                );

        }
    }//class CECompileInfo
}//namespace
