using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Alivever.com.MySqlLib
{
    /// <summary>
    /// 用户自定义类属性，数据库的表信息
    /// </summary>
    public class AttDbTable : Attribute
    {
        public enum EEngine
        {
            InnoDB,
            MyISAM,
            MEMORY,
            BRG_MYISAM,
            CSV,
            BLACKHOLE,
            ARCHIVE

        }//public enum EEngine

        public string Name = string.Empty;

        /// <summary>
        /// 数据库的 编码名称
        /// </summary>
        public string TextEncoding = "utf8_bin";

        public EEngine EngineType = EEngine.InnoDB;

        public AttDbTable(string _name)
        {
            this.Name = _name;
        }//AttDbTable(string name)

        /// <summary>
        /// 从一个对象定义中提取他的AttDbTable特征并返回
        /// </summary>
        /// <param name="_objType"></param>
        /// <returns></returns>
        public static AttDbTable GetAttDbTable(Type _objType)
        {
            foreach(object crrMb in _objType.GetCustomAttributes(true))
            {
                //AttDbTable tpRef = crrMb as AttDbTable;
                if (crrMb is AttDbTable)
                    return crrMb as AttDbTable;

            }

            return null;

        }//GetAttDbTable(Type _objType)

        public static string GetAttDbTableName(Type _objType)
        {
            AttDbTable rst= GetAttDbTable(_objType);
            if (rst != null)
                return rst.Name;
            else
                throw new Exception($"Type '{_objType.Name}' didn't define a AttDbTable. Please check its class defination.");
        }

        public static string GetAttDbTableName<T>() where T :class
        {
            Type tp = typeof(T);
            AttDbTable rst = GetAttDbTable(tp);
            if (rst != null)
                return rst.Name;
            else
                throw new Exception($"Type '{tp.Name}' didn't define a AttDbTable. Please check its class defination.");
        }

    }//class AttDbTable
}//namespace
