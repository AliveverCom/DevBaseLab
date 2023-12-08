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
    /// 针对CCompileInfoIns列表类。提供一些公用的分析编译结果的方法
    /// </summary>
    public class CCompileInfoInsList : List<CCompileInfoIns>
    {
        #region Add Predefined infos

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddBlockerError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.BlockerError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//Add

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddCriticalError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.CriticalError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddCriticalError

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddMajorError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.MajorError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddMajorError

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddNormalError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalError

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddMinorError(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.MinorError(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddMinorError

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddCriticalWarrning(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.CriticalWarrning(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddCriticalWarrning

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddNormalWarrning(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalWarrning(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalWarrning

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public CCompileInfoIns AddNormalSuggestion(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalSuggestion(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalSuggestion

        /// <summary>
        /// 自动创建一个预定义类型的Info,并将这个对象加入到InfoList中.
        /// </summary>
        /// <param name="_InfoDescStr">需要新增的Info的描述</param>
        /// <returns>返回新建的这个对象,以便开发人员随后增加或更改其属性.</returns>
        public  CCompileInfoIns AddNormalMessage(string _InfoDescStr)
        {
            CCompileInfoIns rstInfo = InfoMaker.NormalMessage(_InfoDescStr);
            rstInfo.Number = this.Count + 1;
            this.Add(rstInfo);
            return rstInfo;
        }//AddNormalMessage

        /// <summary>
        /// 批量增加info。增加以后_infoList中所有对象的序号将被重置为适合当前序列的序号--向后追加。
        /// </summary>
        /// <param name="_infoList"></param>
        public void AddRange(CCompileInfoInsList _infoList)
        {
            ////顺序递沿每个info的序号，而不再采用原_infoList中的需要。
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
        /// 通用的信息查询算法。 在所有编译信息中查找并返回所有 _InfoType类型的信息
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
        /// 在所有编译信息中查找并返回所有 Error类型的信息
        /// </summary>
        public IEnumerable<CCompileInfoIns> ErrorInfos
        {
            get
            {
                return this.GetInfos(ECompileInfo.eError);
            }//get
        }//ErrorInfos

        /// <summary>
        ///  在所有编译信息中查找并返回所有 Warrning类型的信息
        /// </summary>
        public IEnumerable<CCompileInfoIns> WarrningInfos
        {
            get
            {
                return this.GetInfos(ECompileInfo.eWarrning);
            }//get
        }//WarrningInfos

        /// <summary>
        /// 在所有编译信息中查找并返回所有 Suggestion类型的信息
        /// </summary>
        public IEnumerable<CCompileInfoIns> SuggestionInfos
        {
            get
            {
                return this.GetInfos(ECompileInfo.eSuggestion);
            }//get
        }//SuggestionInfos

        /// <summary>
        /// 在所有编译信息中查找并返回所有 Message类型的信息
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
        /// 将所有的信息,以每个一行的形式转化为一个综合的文本.一般用于输出Log
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
                        + crrInfo.DescSelfStr + "\n\t"; //注:在某些场合中\n会被VS或其他调试程序忽略.因此保留\t,以便进行替换.
                    i++;

                }
                return rstStr;
            }
        }//string AllInfoStr


        /// <summary>
        /// 与AllInfoStr结果相同
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.AllInfoStr;
        }

        #endregion //To String

        #region 定性判断结果
        /// <summary>
        /// 判断当前编译信息中是否存在BlockerError。
        /// 因为对于某些操作来说，如果结果中有BlockerError存在的话，就需要立即中止编译或后续处理了。
        /// </summary>
        public bool HasBlockerError
        {
            get
            {
                return this.HasInfo(ECISeverity.eBlocker, ECompileInfo.eError);
            }
        }

        /// <summary>
        /// 定性判断当前信息集合中是否包含有指定类型的元素。
        /// 这个算法比现得到结果对象集进行然后再进行定性判断的运行效率高很多。
        /// </summary>
        public bool HasError
        {
            get
            {
                return this.HasInfo(ECompileInfo.eError);
            }
        }

        /// <summary>
        /// 定性判断当前信息集合中是否包含有指定类型的元素。
        /// 这个算法比现得到结果对象集进行然后再进行定性判断的运行效率高很多。
        /// </summary>
        public bool HasMessage
        {
            get
            {
                return this.HasInfo(ECompileInfo.eMessage);
            }
        }

        /// <summary>
        /// 定性判断当前信息集合中是否包含有指定类型的元素。
        /// 这个算法比现得到结果对象集进行然后再进行定性判断的运行效率高很多。
        /// </summary>
        public bool HasSuggestion
        {
            get
            {
                return this.HasInfo(ECompileInfo.eSuggestion);
            }
        }

        /// <summary>
        /// 定性判断当前信息集合中是否包含有指定类型的元素。
        /// 这个算法比现得到结果对象集进行然后再进行定性判断的运行效率高很多。
        /// </summary>
        public bool HasUnknow
        {
            get
            {
                return this.HasInfo(ECompileInfo.eUnknow);
            }
        }

        /// <summary>
        /// 定性判断当前信息集合中是否包含有指定类型的元素。
        /// 这个算法比现得到结果对象集进行然后再进行定性判断的运行效率高很多。
        /// </summary>
        public bool HasWarrning
        {
            get
            {
                return this.HasInfo(ECompileInfo.eWarrning);
            }
        }


        /// <summary>
        /// 定性判断，只要当前集合中发一个符合条件的信息就返回True
        /// </summary>
        /// <param name="_InfoType">信息类型</param>
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
        /// 定性判断，只要当前集合中发一个符合条件的信息就返回True
        /// </summary>
        /// <param name="_Severity">严重程度</param>
        /// <param name="_InfoType">信息类型</param>
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
        /// 定性判断当前信息集合中是否包含有_InfoType级别或更加严重的信息。
        /// 这通常有助于一些编译后处理的进行。
        /// 例如：如果存在有warrning或更高以上级别的问题是，就暂停后续处理。
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
        /// 定性判断当前信息集合中是否包含有_InfoType级别或更加严重的信息。
        /// 这通常有助于一些编译后处理的进行。
        /// 例如：如果存在有warrning或更高以上级别的问题是，就暂停后续处理。
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
        /// 定性判断当前信息集合中是否包含有比_InfoType更加严重的信息。
        /// 这通常有助于一些编译后处理的进行。
        /// 例如：如果存在有比warrning更高以上级别的问题是，就暂停后续处理。
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
        /// 定性判断当前信息集合中是否包含有比_InfoType更加严重的信息。
        /// 这通常有助于一些编译后处理的进行。
        /// 例如：如果存在有比warrning更高以上级别的问题是，就暂停后续处理。
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
        /// 是否有Warrning或有比Warrning更高级别的编译信息
        /// </summary>
        public bool HasWarrningAndSupper
        {
            get { return this.HasInfoAndSupperInfo(ECompileInfo.eWarrning); }
        }

        /// <summary>
        /// 是否有NormalWarrning或有比NormalWarrning更高级别的编译信息
        /// </summary>
        public bool HasNormalWarrningAndSupper
        {
            get { return this.HasInfoAndSupperInfo(ECISeverity.eNormal, ECompileInfo.eWarrning); }
        }

        /// <summary>
        /// 是否有比NormalWarrning更高级别的编译信息（不含NormalWarrning）
        /// </summary>
        public bool HasSupperThenNormalWarrning
        {
            get { return this.HasSupperInfo(ECISeverity.eNormal, ECompileInfo.eWarrning); }
        }

        /// <summary>
        /// 是否有NormalError或有比NormalError更高级别的编译信息
        /// </summary>
        public bool HasNormalErrorAndSupper
        {
            get { return this.HasInfoAndSupperInfo(ECISeverity.eNormal, ECompileInfo.eError); }
        }

        /// <summary>
        /// 是否有比NormalError更高级别的编译信息（不含NormalError）
        /// </summary>
        public bool HasSupperThenNormalError
        {
            get { return this.HasSupperInfo(ECISeverity.eNormal, ECompileInfo.eError); }
        }


        #endregion//定性判断结果


    }//class CCompileInfoInsList
}//namespace
