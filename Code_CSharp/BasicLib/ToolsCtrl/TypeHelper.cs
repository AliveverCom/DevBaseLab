///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2011-4-2</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Alivever.Com.DevBasic.BasicLib.ToolsCtrl
{
    /// <summary>
    /// 关于Tpye的一些辅助方法。
    /// </summary>
    public class CTypeHelper
    {
        /// <summary>
        /// 从一个_objType查找名称为_fieldName的Field.
        /// 不论这个Field是在子类中，还是父类中，本函数均会不断从子类向父类的方向查找，直到找到后就返回。
        /// 如果确实没有_fieldName，则返回null。 
        /// 注：本函数也支持查找objec他的field
        /// </summary>
        /// <returns></returns>
        public static FieldInfo GetField(Type _objType, string _fieldName, bool _bIncludeNonPublic )
        {
            BindingFlags flage = BindingFlags.Instance | BindingFlags.Public;

            if (_bIncludeNonPublic)
                flage = flage | BindingFlags.NonPublic;

            FieldInfo rstInfo = _objType.GetField(_fieldName, flage);
            if (rstInfo != null)
                return rstInfo;

            //for 逐级遍历上层父类
            for (Type father = _objType.BaseType; ; father = father.BaseType)
            {
                rstInfo = father.GetField(_fieldName);
                if (rstInfo != null)
                    return rstInfo;

                if (father == typeof(object))
                    break;
            }//for

            return null;
        }//GetField

        /// <summary>
        /// 从一个_objType查找名称为_fieldName的Field.
        /// 不论这个Field是在子类中，还是父类中，本函数均会不断从子类向父类的方向查找，直到找到后就返回。
        /// 如果确实没有_fieldName，则返回null。 
        /// 注：本函数也支持查找objec他的field
        /// </summary>
        /// <returns></returns>
        public static PropertyInfo GetProperty(Type _objType, string _PropertyName, bool _bIncludeNonPublic)
        {
            BindingFlags flage = BindingFlags.Instance | BindingFlags.Public;

            if (_bIncludeNonPublic)
                flage = flage | BindingFlags.NonPublic;

            PropertyInfo rstInfo = _objType.GetProperty(_PropertyName, flage);
            if (rstInfo != null)
                return rstInfo;

            //for 逐级遍历上层父类
            for (Type father = _objType.BaseType; ; father = father.BaseType)
            {
                rstInfo = father.GetProperty(_PropertyName);
                if (rstInfo != null)
                    return rstInfo;

                if (father == typeof(object))
                    break;
            }//for

            return null;
        }//Property

        /// <summary>
        /// 从一个_objType查找名称为__MemberName的Field或Propety.
        /// 不论这个Member是在子类中，还是父类中，本函数均会不断从子类向父类的方向查找，直到找到后就返回。
        /// 如果确实没有__MemberName，则返回null。 
        /// 注：本函数也支持查找objec他的Member
        /// </summary>
        /// <returns></returns>
        public static MemberInfo GetMemberOfFieldAndPropety(Type _objType, string _MemberName, bool _bIncludeNonPublic)
        {
            BindingFlags flage = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty;

            if (_bIncludeNonPublic)
                flage = flage | BindingFlags.NonPublic;

            MemberInfo[] rstInfos = _objType.GetMember(_MemberName, flage);
            if (rstInfos != null && rstInfos.Length != 0)
                return rstInfos[0];

            //for 逐级遍历上层父类
            for (Type father = _objType.BaseType; ; father = father.BaseType)
            {
                rstInfos = father.GetMember(_MemberName);
                if (rstInfos != null && rstInfos.Length != 0)
                    return rstInfos[0];

                if (father == typeof(object))
                    break;
            }//for

            return null;
        }//GetMemberOfFieldAndPropety

    }//class TypeHelper
}//namespace
