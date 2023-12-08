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
    /// CCompileInfo��ʵ���ࡣ �Ƕ���ĳ�ֱ�������Ϣ����������
    /// </summary>
    public class CCompileInfoIns
    {
        /// <summary>
        /// Ψһ��ʾ�ı�����ϢID
        /// </summary>
        public string IdStr = "";

        /// <summary>
        /// ������Ϣ���ɵ�˳������������š�������ͨ����CCompileInfoIns.Add()�Զ�����
        /// </summary>
        public int Number = 0;

        /// <summary>
        /// ����������Ϣ����ĵ���������˵������ͨ��������ʱ�Զ���ı�����ʾ����Ԥ�������Ϣһ�㽨��ֱ������InfoDefine.DescStr
        /// </summary>
        public string DescSelfStr = "";

        /// <summary>
        /// �������ɱ���Ϣ����������������������ⶨ���еı������޸������ͬ��ɡ�����ֱ��������Ϣ�����е�������
        /// </summary>
        public string DescWithDefine
        {
            get { return DescSelfStr + InfoDefine.DescStr; }
        }

        /// <summary>
        /// ����������Ϣ�Ķ���������
        /// </summary>
        public CCompileInfoDefine InfoDefine;

        /// <summary>
        /// �����뱾������Ϣ������ı���������Ա��û����Կ��ٶ�λ����������λ��
        /// </summary>
        public Dictionary<string, Object> InvovedObjs = new Dictionary<string, object>();
 
        /// <summary>
        /// �������ʼ��һ���Զ��ı�����Ϣ����
        /// </summary>
        /// <param name="_IsError">True=eError, False=eWarrning,��֧��eSuggestion</param>
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
