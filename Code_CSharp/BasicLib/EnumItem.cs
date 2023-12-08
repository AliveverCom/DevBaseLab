///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2010-02-12</CreaterDate>
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
    /// CEnumMgrBase�й����ö�ٶ��� �������������е����ã�����ʹ��string�� ���Ҽ�סNameStr�� ������Ҫʹ�� int ��ֵ���д洢�ĵط�������ʹ��Value
    /// </summary>
    public class CEnumItemBase<TKey> 
    {
        /// <summary>
        /// Ψһ�ĳ����ڲ�ö������
        /// </summary>
        public TKey KeyObj ;

        /// <summary>
        /// ��ǰö��ֵ������
        /// </summary>
        public string DescStr = string.Empty;

        /// <summary>
        /// ��ǰö�ٵ�ֵ�����������κζ���������Բ�ǿ��Ψһ��Ҳ����Ϊ�գ�ȫƾ�������Լ�
        /// </summary>
        public string ValueStr = string.Empty;

        /// <summary>
        /// ���ڳ�����ʾ��ȫ�����ơ�
        /// </summary>
        public string DisplayName = string.Empty;

        /// <summary>
        /// ���ڳ�����ʾ�Ķ����ơ�Ĭ�ϳ�ʼ����ʱ��Ӧ����DisplayName��ͬ��
        /// </summary>
        public string DisplayShortName = string.Empty;

        /// <summary>
        /// ���ټ�������ѡ����еĿ��ٶ�λ
        /// </summary>
        public string AccStr = string.Empty;

        /// <summary>
        /// ���ڽ����ı�ʱ��ʹ�á�������������ı����ڻ�ӵ�и��ַ����Ļ�������Ϊ����ת��ɵ�ǰ��ö�����͡�
        /// ¼��ʱ��Ҫע�ⷴ���ַ����Ĳ���˳��--����EnumItem�ķ����ֶ������ơ�Bb���͡�ABb����ʱ�򣬱������Ȳ���ABB�����򽫵�����Զ����Bb���ȱ�ƥ�䡣
        /// �������RefectNamesContains_XXX(string _ParseTargetStr )����һͬʹ��
        /// </summary>
        public List<string> RefectNames = new List<string>();

        /// <summary>
        /// �ⲿ��ֹʹ�ÿչ��캯�������ⷢ������
        /// </summary>
        protected CEnumItemBase()
        {

        }

        /// <summary>
        /// Name & Desc�Ǳ�����д���������ԡ�������Ϊ�ա����������󿪷��еı������
        /// </summary>
        /// <param name="_NameStr"></param>
        /// <param name="_DescStr"></param>
        private CEnumItemBase(TKey _KeyObj, string _DescStr)
        {
            KeyObj = _KeyObj;
            DescStr = _DescStr;
        }

        public CEnumItemBase(TKey _KeyObj, string _DescStr, string _ValueStr)
            : this(_KeyObj, _DescStr)
        {
            ValueStr = _ValueStr;
        }//CEnumItemBase(4)

        public CEnumItemBase(TKey _KeyObj, 
                            string _DescStr,  
                            string _ValueStr, 
                            string _DisplayName, 
                            string _DisplayShortName, 
                            string _AccStr )
            : this(_KeyObj, _DescStr, _ValueStr)
        {
            DisplayName = _DisplayName;
            DisplayShortName = _DisplayShortName;
            AccStr = _AccStr;
        }//CEnumItemBase(4)

        /// <summary>
        /// Ĭ��_ReflactNamesStr�еķָ���ʹ��','
        /// </summary>
        /// <param name="_NameStr"></param>
        /// <param name="_DescStr"></param>
        /// <param name="_ValueStr"></param>
        /// <param name="_DisplayName"></param>
        /// <param name="_DisplayShortName"></param>
        /// <param name="_AccStr"></param>
        /// <param name="_ReflactNamesStr"></param>
        public CEnumItemBase(TKey _KeyObj,
                    string _DescStr,
                    string _ValueStr,
                    string _DisplayName,
                    string _DisplayShortName,
                    string _AccStr,
                    string _ReflactNamesStr )
            : this(_KeyObj, _DescStr, _ValueStr, _DisplayName, _DisplayShortName, _AccStr)
        {
            this.AddRefectNames(',',_ReflactNamesStr);
        }//CEnumItemBase(4)

        /// <summary>
        /// �ı�������������� ���RefectNames������ȫ����_ParseTargetStr,�򷵻�True
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns></returns>
        public bool RefectNamesContains_Equal(string _ParseTargetStr )
        {
            return RefectNames.Contains(_ParseTargetStr);
        }//RefectNamesContains_Equal

        /// <summary>
        /// �ı�������������� ���_ParseTargetStr������RefectNames�е�һ���Ļ�,�򷵻�True
        /// </summary>
        /// <param name="_ParseTargetStr"></param>
        /// <returns></returns>
        public bool RefectNamesContains_Include(string _ParseTargetStr)
        {
            foreach (string crrStr in this.RefectNames)
            {
                if (_ParseTargetStr.IndexOf(crrStr) >= 0)
                    return true;
            }

            return false;
        }//RefectNamesContains_Include

        /// <summary>
        /// ��RefectNames������Item������Items��splitStr�н���������Ȼ���Զ���ӵ�RefectNames�С�
        /// ʹ������������ڿ�������RefectNames��������ֻ������RefectNames�����ӣ�����������ա�
        /// ��ˣ��������Ҫ��������ǰ���RefectNames�Ļ������ڵ��ñ�����ǰ�Լ��ֶ�д����ִ�С�
        /// </summary>
        /// <param name="_seperator">splitStr�����ָ����ֶεķָ���������ֻ����1��ͳһ�ķָ���</param>
        /// <param name="splitStr">��_seperator�ָ��ʾ���ַ���</param>
        /// <returns>���سɹ���������������С��0�����ʾ����</returns>
        public int AddRefectNames( char _seperator, string splitStr)
        {
            string[] NamesArray = splitStr.Split(_seperator);

            if (NamesArray == null)
            {
                //GSdkMLog.At(this.GetType().Assembly.ToString()).
                //    Write(this.GetType().Name + "AddRefectNames()", 1, "splitStr[" + splitStr + "] parse error.\n");

                return 0;
            }

            this.RefectNames.AddRange(NamesArray);

            return NamesArray.Length;

        }//AddRefectNames

    }//class CEnumItemBase
}//namespace
