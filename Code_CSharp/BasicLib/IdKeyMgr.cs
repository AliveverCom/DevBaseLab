///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-06-02</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog>�л����̣�������ļ���CreatBone.InfoSysBuilder.Generic�ƶ���Alivever.Com.DevBasic.BasicLib.</ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;
using Alivever.Com.DevBasic.BasicLib.LogCtrl;

namespace Alivever.Com.DevBasic.BasicLib
{
    /// <summary>
    /// ȫ�ֵ�ΨһID������������ID��Ϊint����1��ʼ�� Key��ʾ��ͬ�Ķ���/��
    /// </summary>
    [Serializable]
    public class CIdKeyMgr
    {
        /// <summary>
        /// ��������������һ�����ɵ�IDֵ
        /// </summary>
        protected Dictionary<string, int> Key_nID=new Dictionary<string,int>();

        public static CIdKeyMgr Ins = null;

        /// <summary>
        /// Ϊָ����Key����һ���µ�nID��ÿ�η��غ�nID�Զ��ۼ�
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public int GetNewID(string _key)
        {
            //���_key�����ڣ���ֱ�Ӵ���һ��Key
            if (!this.Key_nID.ContainsKey(_key))
            {
                GSdkMLog.At(GT.pkgName).Write("CIdKeyMgr.GetNewID", 2, "a new _Key is found:" + _key + "Auto added into CIdKeyMgr.\n");
                Key_nID.Add(_key, 0);
            }

            Key_nID[_key] += 1;

            return Key_nID[_key];
        }//int NewID(string _key)

        /// <summary>
        /// ���ظ�Key�����һ�����ɵ�ID�����ۼ�
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public int GetLastID(string _key)
        {
            //���_key�����ڣ���ֱ�Ӵ���һ��Key
            if (!this.Key_nID.ContainsKey(_key))
            {
                GSdkMLog.At(GT.pkgName).Write("CIdKeyMgr.Key_nID", 2, "a new _Key is found:" + _key + "Auto added into CIdKeyMgr.\n");
                Key_nID.Add(_key, 0);
            }

            return Key_nID[_key];

        }

        /// <summary>
        /// �����ض���IDǰ׺������Ӧ��ǰ׺�ϳ��ַ���
        /// </summary>
        /// <param name="_PrefixStr">ǰ׺�ַ���</param>
        /// <returns></returns>
        public string GetNewPrefixId(string _PrefixStr)
        {
            return _PrefixStr + GetNewID(_PrefixStr).ToString();
        }

        /// <summary>
        /// �����ض���IDǰ׺�������һ�����ɵ�ǰ׺�ϳ��ַ���
        /// </summary>
        /// <param name="_PrefixStr">ǰ׺�ַ���</param>
        /// <returns></returns>
        public string GetLastPrefixId(string _PrefixStr)
        {
            return _PrefixStr + GetLastID(_PrefixStr).ToString();
        }

    }//class CIdKeyMgr
}
