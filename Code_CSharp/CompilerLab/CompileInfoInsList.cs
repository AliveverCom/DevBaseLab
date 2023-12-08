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
    /// ���CCompileInfoIns�б��ࡣ�ṩһЩ���õķ����������ķ���
    /// </summary>
    public class CCompileInfoInsList : List<CCompileInfoIns>
    {
        #region Add Predefined infos

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddBlockerError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.BlockerError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//Add

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddCriticalError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.CriticalError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddCriticalError

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddMajorError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.MajorError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddMajorError

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddNormalError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalError

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddMinorError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.MinorError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddMinorError

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddCriticalWarrning(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.CriticalWarrning(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddCriticalWarrning

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddNormalWarrning(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalWarrning(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalWarrning

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public CCompileInfoIns AddNormalSuggestion(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalSuggestion(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalSuggestion

        /// <summary>
        /// �Զ�����һ��Ԥ�������͵�Info,�������������뵽InfoList��.
        /// </summary>
        /// <param name="_InfoDescStr">��Ҫ������Info������</param>
        /// <returns>�����½����������,�Ա㿪����Ա������ӻ����������.</returns>
        public  CCompileInfoIns AddNormalMessage(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalMessage(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalMessage

        /// <summary>
        /// ��������info�������Ժ�_infoList�����ж������Ž�������Ϊ�ʺϵ�ǰ���е����--���׷�ӡ�
        /// </summary>
        /// <param name="_infoList"></param>
        public void AddRange(CCompileInfoInsList _infoList)
        {
            ////˳�����ÿ��info����ţ������ٲ���ԭ_infoList�е���Ҫ��
            int i=1;
            foreach (CCompileInfoIns crrItem in _infoList)
            {
                crrItem.Number = this.Count + i;
                i++;
            }

            base.AddRange(_infoList);
        }//AddRange(CCompileInfoInsList _infoList)
 
	    #endregion    
    
        #region Select from Infos

        /// <summary>
        /// ͨ�õ���Ϣ��ѯ�㷨�� �����б�����Ϣ�в��Ҳ��������� _InfoType���͵���Ϣ
        /// </summary>
        /// <param name="_InfoType"></param>
        /// <returns></returns>
        public IEnumerable<CCompileInfoIns> GetInfos(ECompileInfo _InfoType)
        {
            CCompileInfoInsList errV = new CCompileInfoInsList();
            foreach (CCompileInfoIns crrInfo in this)
            {
                if (crrInfo.InfoDefine.InfoType == (int)_InfoType)
                    errV.Add(crrInfo);
            }//foreach
            return errV;
        }

        /// <summary>
        /// �����б�����Ϣ�в��Ҳ��������� Error���͵���Ϣ
        /// </summary>
        public IEnumerable<CCompileInfoIns> ErrorInfos
        {
            get
            {
                return this.GetInfos(ECompileInfo.eError);
            }//get
        }//ErrorInfos

        /// <summary>
        ///  �����б�����Ϣ�в��Ҳ��������� Warrning���͵���Ϣ
        /// </summary>
        public IEnumerable<CCompileInfoIns> WarrningInfos
        {
            get
            {
                return this.GetInfos(ECompileInfo.eWarrning);
            }//get
        }//WarrningInfos

        /// <summary>
        /// �����б�����Ϣ�в��Ҳ��������� Suggestion���͵���Ϣ
        /// </summary>
        public IEnumerable<CCompileInfoIns> SuggestionInfos
        {
            get
            {
                return this.GetInfos(ECompileInfo.eSuggestion);
            }//get
        }//SuggestionInfos

        /// <summary>
        /// �����б�����Ϣ�в��Ҳ��������� Message���͵���Ϣ
        /// </summary>
        public IEnumerable<CCompileInfoIns> MessageInfos
        {
            get
            {
                return this.GetInfos(ECompileInfo.eMessage);
            }//get
        }//MessageInfos

        #endregion //Select from Infos

        #region To String

        /// <summary>
        /// �����е���Ϣ,��ÿ��һ�е���ʽת��Ϊһ���ۺϵ��ı�.һ���������Log
        /// </summary>
        public string AllInfoStr
        {
            get
            {
                string rstStr = string.Empty;
                int i = 1;
                foreach (CCompileInfoIns crrInfo in this)
                {
                    ECompileInfo infoType = (ECompileInfo)Enum.Parse(typeof(ECompileInfo), crrInfo.InfoDefine.InfoType.ToString());
                    rstStr += "[" + i.ToString() + "]"
                        + "[" + CECompileInfo.Ins[infoType].DisplayName + "]"
                        + crrInfo.DescSelfStr + "\n\t"; //ע:��ĳЩ������\n�ᱻVS���������Գ������.��˱���\t,�Ա�����滻.
                    i++;

                }
                return rstStr;
            }
        }//string AllInfoStr


        /// <summary>
        /// ��AllInfoStr�����ͬ
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.AllInfoStr;
        }

        #endregion //To String

        #region �����жϽ��
        /// <summary>
        /// �жϵ�ǰ������Ϣ���Ƿ����BlockerError��
        /// ��Ϊ����ĳЩ������˵������������BlockerError���ڵĻ�������Ҫ������ֹ�������������ˡ�
        /// </summary>
        public bool HasBlockerError
        {
            get
            {
                return this.HasInfo(ECISeverity.eBlocker, ECompileInfo.eError);
            }
        }

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ������ָ�����͵�Ԫ�ء�
        /// ����㷨���ֵõ�������󼯽���Ȼ���ٽ��ж����жϵ�����Ч�ʸߺܶࡣ
        /// </summary>
        public bool HasError
        {
            get
            {
                return this.HasInfo(ECompileInfo.eError);
            }
        }

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ������ָ�����͵�Ԫ�ء�
        /// ����㷨���ֵõ�������󼯽���Ȼ���ٽ��ж����жϵ�����Ч�ʸߺܶࡣ
        /// </summary>
        public bool HasMessage
        {
            get
            {
                return this.HasInfo(ECompileInfo.eMessage);
            }
        }

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ������ָ�����͵�Ԫ�ء�
        /// ����㷨���ֵõ�������󼯽���Ȼ���ٽ��ж����жϵ�����Ч�ʸߺܶࡣ
        /// </summary>
        public bool HasSuggestion
        {
            get
            {
                return this.HasInfo(ECompileInfo.eSuggestion);
            }
        }

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ������ָ�����͵�Ԫ�ء�
        /// ����㷨���ֵõ�������󼯽���Ȼ���ٽ��ж����жϵ�����Ч�ʸߺܶࡣ
        /// </summary>
        public bool HasUnknow
        {
            get
            {
                return this.HasInfo(ECompileInfo.eUnknow);
            }
        }

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ������ָ�����͵�Ԫ�ء�
        /// ����㷨���ֵõ�������󼯽���Ȼ���ٽ��ж����жϵ�����Ч�ʸߺܶࡣ
        /// </summary>
        public bool HasWarrning
        {
            get
            {
                return this.HasInfo(ECompileInfo.eWarrning);
            }
        }


        /// <summary>
        /// �����жϣ�ֻҪ��ǰ�����з�һ��������������Ϣ�ͷ���True
        /// </summary>
        /// <param name="_InfoType">��Ϣ����</param>
        /// <returns></returns>
        public bool HasInfo(ECompileInfo _InfoType)
        {
            foreach (CCompileInfoIns crrInfo in this)
            {
                if (crrInfo.InfoDefine.InfoType == (int)_InfoType)
                    return true;
            }//foreach
            return false;

        }//HasInfo(ECompileInfo _InfoType)

        /// <summary>
        /// �����жϣ�ֻҪ��ǰ�����з�һ��������������Ϣ�ͷ���True
        /// </summary>
        /// <param name="_Severity">���س̶�</param>
        /// <param name="_InfoType">��Ϣ����</param>
        /// <returns></returns>
        public bool HasInfo(ECISeverity _Severity, ECompileInfo _InfoType)
        {
            foreach (CCompileInfoIns crrInfo in this)
            {
                if (crrInfo.InfoDefine.InfoType == (int)_InfoType
                    && crrInfo.InfoDefine.Severity == (int)_Severity)
                    return true;
            }//foreach
            return false;
        }//HasInfo(ECISeverity _Severity, ECompileInfo _InfoType)

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ������_InfoType�����������ص���Ϣ��
        /// ��ͨ��������һЩ�������Ľ��С�
        /// ���磺���������warrning��������ϼ���������ǣ�����ͣ��������
        /// </summary>
        /// <param name="_InfoType"></param>
        /// <returns></returns>
        public bool HasInfoAndSupperInfo(ECompileInfo _InfoType)
        {
            foreach (CCompileInfoIns crrInfo in this)
            {
                if (crrInfo.InfoDefine.InfoType <= (int)_InfoType)
                    return true;
            }//foreach
            return false;

        }//HasInfo(ECompileInfo _InfoType)

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ������_InfoType�����������ص���Ϣ��
        /// ��ͨ��������һЩ�������Ľ��С�
        /// ���磺���������warrning��������ϼ���������ǣ�����ͣ��������
        /// </summary>
        /// <param name="_InfoType"></param>
        /// <returns></returns>
        public bool HasInfoAndSupperInfo(ECISeverity _Severity, ECompileInfo _InfoType)
        {
            foreach (CCompileInfoIns crrInfo in this)
            {
                if ((crrInfo.InfoDefine.InfoType == (int)_InfoType && crrInfo.InfoDefine.Severity <= (int)_Severity)
                    || crrInfo.InfoDefine.InfoType < (int)_InfoType)
                    return true;
            }//foreach
            return false;

        }//HasInfoAndSupperInfo(ECompileInfo _InfoType)


        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ�����б�_InfoType�������ص���Ϣ��
        /// ��ͨ��������һЩ�������Ľ��С�
        /// ���磺��������б�warrning�������ϼ���������ǣ�����ͣ��������
        /// </summary>
        /// <param name="_InfoType"></param>
        /// <returns></returns>
        public bool HasSupperInfo(ECompileInfo _InfoType)
        {
            foreach (CCompileInfoIns crrInfo in this)
            {
                if (crrInfo.InfoDefine.InfoType < (int)_InfoType)
                    return true;
            }//foreach
            return false;

        }//HasInfo(ECompileInfo _InfoType)

        /// <summary>
        /// �����жϵ�ǰ��Ϣ�������Ƿ�����б�_InfoType�������ص���Ϣ��
        /// ��ͨ��������һЩ�������Ľ��С�
        /// ���磺��������б�warrning�������ϼ���������ǣ�����ͣ��������
        /// </summary>
        /// <param name="_InfoType"></param>
        /// <returns></returns>
        public bool HasSupperInfo(ECISeverity _Severity, ECompileInfo _InfoType)
        {
            foreach (CCompileInfoIns crrInfo in this)
            {
                if ((crrInfo.InfoDefine.InfoType == (int)_InfoType && crrInfo.InfoDefine.Severity < (int)_Severity)
                    || crrInfo.InfoDefine.InfoType < (int)_InfoType)
                    return true;
            }//foreach
            return false;

        }//HasInfoAndSupperInfo(ECompileInfo _InfoType)

        /// <summary>
        /// �Ƿ���Warrning���б�Warrning���߼���ı�����Ϣ
        /// </summary>
        public bool HasWarrningAndSupper
        {
            get { return this.HasInfoAndSupperInfo(ECompileInfo.eWarrning); }
        }

        /// <summary>
        /// �Ƿ���NormalWarrning���б�NormalWarrning���߼���ı�����Ϣ
        /// </summary>
        public bool HasNormalWarrningAndSupper
        {
            get { return this.HasInfoAndSupperInfo(ECISeverity.eNormal, ECompileInfo.eWarrning); }
        }

        /// <summary>
        /// �Ƿ��б�NormalWarrning���߼���ı�����Ϣ������NormalWarrning��
        /// </summary>
        public bool HasSupperThenNormalWarrning
        {
            get { return this.HasSupperInfo(ECISeverity.eNormal, ECompileInfo.eWarrning); }
        }

        /// <summary>
        /// �Ƿ���NormalError���б�NormalError���߼���ı�����Ϣ
        /// </summary>
        public bool HasNormalErrorAndSupper
        {
            get { return this.HasInfoAndSupperInfo(ECISeverity.eNormal, ECompileInfo.eError); }
        }

        /// <summary>
        /// �Ƿ��б�NormalError���߼���ı�����Ϣ������NormalError��
        /// </summary>
        public bool HasSupperThenNormalError
        {
            get { return this.HasSupperInfo(ECISeverity.eNormal, ECompileInfo.eError); }
        }


        #endregion//�����жϽ��


    }//class CCompileInfoInsList
}//namespace
