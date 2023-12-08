using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Alivever.com.MySqlLib
{
    /// <summary>
    /// 这个类仅用于展开 AttDbColumn 过程中的数据传输
    /// </summary>
    public class CExpandedAttDbColumn
    {
        /// <summary>
        /// 当前Att 对应的DB 列 的名称。
        /// </summary>
        public string DbColumnName;

        /// <summary>
        /// 当前Att的定义
        /// </summary>
        public AttDbColumn AttColumn;

        public MemberInfo memberInfo;

        /// <summary>
        /// 当前对象类型
        /// </summary>
        public Type MemberType;

        /// <summary>
        /// 当前对象展开后的子项的Att列表。 
        /// 只有设置了IsExpandSubattributesInCurrentTable=true时有效，否则为null
        /// </summary>
        public List<CExpandedAttDbColumn> ExpandedMemberTypes;

        public CExpandedAttDbColumn(string _DbColumnName, AttDbColumn _AttColumn, MemberInfo _memberInfo, Type _MemberType)
        {
            this.DbColumnName = _DbColumnName;
            this.AttColumn = _AttColumn;
            this.MemberType = _MemberType;
            this.memberInfo = _memberInfo;
        }//CExpandedAttDbColumn()

        /// <summary>
        /// 根据给定的 MemberNames 过滤列表第一层中的Att，并返回一个过滤后生成的新列表树
        /// </summary>
        /// <param name="_oldList"></param>
        /// <param name="_memberNames"></param>
        /// <returns>返回一个过滤后生成的新列表树</returns>
        public static List<CExpandedAttDbColumn> FilterByMemberNamesOfFirstLevelClass(
            List<CExpandedAttDbColumn> _oldTree, IEnumerable<string> _memberNames)
        {
            List<CExpandedAttDbColumn> rstList = new List<CExpandedAttDbColumn>();

            foreach (CExpandedAttDbColumn crrItem in _oldTree)
            {
                if (_memberNames.Contains(crrItem.memberInfo.Name))
                    rstList.Add(crrItem);
            }

            return rstList;
        }//FilterByMemberNamesOfFirstLevelClass()

        /// <summary>
        /// 递归。将树状结构展开成一排列表。去掉树状结构中的枝干
        /// </summary>
        /// <param name="_oldTree"></param>
        /// <param name="_rstList"></param>
        public static void Tree2List(List<CExpandedAttDbColumn> _oldTree, ref List<CExpandedAttDbColumn> _rstList)
        {
            if (_rstList == null)
                _rstList = new List<CExpandedAttDbColumn>();

            foreach (CExpandedAttDbColumn crrItem in _oldTree)
            {
                if (crrItem.ExpandedMemberTypes == null)
                    _rstList.Add(crrItem);
                else
                    Tree2List(crrItem.ExpandedMemberTypes, ref _rstList);
            }
        }//Tree2List()

        public static void 递归_将给定Type的AttDbColumn展开到结果_树型结构(
            Type _objType,
            string _prefixOfDbColName,
            ref List<CExpandedAttDbColumn> _rstTree,
            bool isForbidenIndexesOfSubItems)
        {
            if (_rstTree == null)
                _rstTree = new List<CExpandedAttDbColumn>();

            IEnumerable<MemberInfo> mbs = _objType.GetMembers().OrderBy( a => a.Name );

            ////整理cols定义列表
            //循环当前对象的全部成员定义
            foreach (MemberInfo crrMember in mbs)
            {
                object[] memberAttList = crrMember.GetCustomAttributes(true);

                //循环当前类成员的每一个 自定义特征，找出并解释AttDbColumn的特征
                foreach (object crrMemberAtt in memberAttList)
                {
                    AttDbColumn crrAtt = crrMemberAtt as AttDbColumn;

                    if (crrAtt == null)
                        continue;

                    //如果上一层设置了强制取消下层的Index，则在内存中强制HasDefaultIndex = false
                    if (isForbidenIndexesOfSubItems)
                        crrAtt.HasDefaultIndex = false;

                    Type crrType = CSqlHelper.GetUnderlyingType(crrMember);

                    //如果当前属性需要平行展开，则展开；否则直接添加
                    CExpandedAttDbColumn newCol = new CExpandedAttDbColumn(//new Tuple<string, AttDbColumn, Type>(
                            (_prefixOfDbColName == null) ? crrAtt.Name : _prefixOfDbColName + crrAtt.Name,
                            crrAtt,
                            crrMember,
                            crrType);

                    _rstTree.Add(newCol);

                    if (crrAtt.IsExpandSubattributesInCurrentTable)
                    {
                        newCol.ExpandedMemberTypes = new List<CExpandedAttDbColumn>();

                        递归_将给定Type的AttDbColumn展开到结果_树型结构(
                            crrType,//GetUnderlyingType(crrMember),
                            (_prefixOfDbColName == null) ? crrAtt.Name : _prefixOfDbColName + crrAtt.Name,//crrAtt.Name,
                            ref newCol.ExpandedMemberTypes,
                            isForbidenIndexesOfSubItems || crrAtt.IsForbidenIndexesOfSubItems);
                    }//else

                    break;
                }//foreach 循环当前类成员的每一个 自定义特征，找出并解释AttDbColumn的特征

            }//foreach 循环当前对象的全部成员定义

        }//递归_将给定Type的AttDbColumn展开到结果列表

        /// <summary>
        /// 从某个对象中取出 当前AttDbColumn指向的那个属性的值
        /// </summary>
        /// <returns></returns>
        public object GetValueFromObj(object _srcObj)
        {
            try
            {
                switch (this.memberInfo) //switch (attMap[crrColName])
                {
                    case FieldInfo fieldInfo:
                       return  fieldInfo.GetValue(_srcObj);
                        //reader.GetValue(i));
                    case PropertyInfo propertyInfo:
                       return propertyInfo.GetValue( _srcObj);
                    default:
                        throw new InvalidOperationException($"GetValueFromObj(). attributeName='{this.memberInfo.Name}', AttributeType='{this.MemberType.ToString()}' not found");

                }
                //break;
            }
            catch (Exception ex)
            {
                string str = $"Error: {ex.Message}, Getting value from attributeName = '{this.memberInfo.Name}', AttributeType = '{this.MemberType.ToString()}'";
                throw new Exception(str);
            }


        }//GetValueFromObj()

        /// <summary>
        /// 将_srcObj的当前属性定义的位置赋值为_attIns
        /// </summary>
        /// <param name="_srcObj"></param>
        /// <param name="_attIns"></param>
        public void SetValueForObj(object _srcObj, object _attIns)
        {
            try
            {
                switch (this.memberInfo)//switch (attMap[crrColName])
                {
                    case FieldInfo fieldInfo:
                        fieldInfo.SetValue(
                            _srcObj,
                            CSqlHelper.ChangeType(_attIns is DBNull ? null : _attIns, fieldInfo.FieldType)); 
                        //reader.GetValue(i));
                        break;
                    case PropertyInfo propertyInfo:
                        if (propertyInfo.CanWrite)
                        {
                            propertyInfo.SetValue(
                                _srcObj,
                                //CSqlHelper.ChangeType(reader.GetString(reader.GetName(i)), propertyInfo.PropertyType),
                                CSqlHelper.ChangeType(_attIns is DBNull ? null : _attIns, propertyInfo.PropertyType),
                                //reader.GetValue(i),
                                null);
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"SetValueForObj() .attributeName='{this.memberInfo.Name}', AttributeType='{this.MemberType.ToString()}' not found");

                }
                //break;
            }
            catch (Exception ex)
            {
                string str = $"Error:SetValueForObj()  {ex.Message}, Setting value for SrcObj='{_srcObj}' attributeName = '{this.memberInfo.Name}', AttributeType = '{this.MemberType.ToString()}'";
                throw new Exception(str);
            }


        }//SetValueFrObj(object _srcObj, object _attIns)

    }//class CExpandedAttDbColumn
}//namespace Alivever.com.MySqlLib
