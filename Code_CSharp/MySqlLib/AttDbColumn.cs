using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.ComponentModel;


namespace Alivever.com.MySqlLib
{
    /// <summary>
    /// 用户自定义类属性。 描述Class 成员对应的数据 column的映射信息。
    /// 这些信息将用于 自动存取 class 的对象，以及创建数据库等等。
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AttDbColumn:Attribute
    {
        /// <summary>
        /// 自动填充类型
        /// </summary>
        public enum EAutoFill
        {
            NoAutoFill,
            UseDefaultValueAttribute,
            NullItem,
            EmptyString,
            AutoIncreasment,
            OnCurrentTime,
            OnUpdateTime,
            Zero,
            ZeroFill,
            True,
            False

        }//enum EAutoFill

        public enum EColumnDataType
        {
            SameAsCodeDataType,

            SMALLINT,
            INT,
            BIGINT,

            CHAR,
            VARCHAR,

            FLOAT,
            DOUBLE,

            DECIMAL,
            DATATIME,

            Enum,

            Point,
            LineString,
            Line,
            LinearRing,
            Polygon,
            GeometryCollection,
            MultiPoint,
            MultiLineString,
            MultiPolygon

        }//enum EDataType


        public string TableName = null;

        public string Name = null;

        public EColumnDataType DataType = EColumnDataType.SameAsCodeDataType;

        /// <summary>
        /// 该对象的最大长度
        /// </summary>
        public short MaxLength = -1;

        /// <summary>
        /// 当出现 string[] 的时候， MaxLength指定数组长度，而ArrayItemMaxLength指定每一个数组内元素的长度。
        /// </summary>
        public short ArrayItemMaxLength = -1;

        public bool IsNotNull = true;

        /// <summary>
        /// 是否具备与其同名的索引。 true 在 create table的时候会自动标记这个属性。
        /// </summary>
        public bool HasDefaultIndex = true;

        public bool IsPrimaryKey = false;

        public EAutoFill AutoFill = EAutoFill.NoAutoFill;

        public string DefaultValue = null;

        /// <summary>
        /// 是否是update语句中用于Where 自动关联的主键。
        /// 如果true，则程序自动将该列的值纳入条件：where column1='value' and  column2='value'
        /// primaryKey 将忽略这个值， 默认IsUpdateKeyColumn=true
        /// </summary>
        public bool IsUpdateWhereColumn = false;

        /// <summary>
        /// 是否将当前对象的子属性平行展开到当前表中。
        /// 默认将当前变量名和他的下属属性拼合起来。
        /// 例如： Point P1; --> P1_x; P1_y
        /// </summary>
        public bool IsExpandSubattributesInCurrentTable = false;

        /// <summary>
        /// 是否把数组或List 展开到当前表格
        /// int[2] i --> i0 int, i1 int; 
        /// </summary>
        public bool IsExpentArrayItemsInCurrentTable = false;

        /// <summary>
        /// 是否将当前元素展开到另外一个表格中。
        /// </summary>
        public bool IsExpend2AnotherDbTable = false;

        /// <summary>
        /// 是否强制取消所有下属对象的索引. 以便减轻系统消耗。
        /// True： 当某个需要被展开的下属对象的属性有索引时，强制取消他们。
        ///        即在创建表和索引的时候，都不在生成索引。
        /// </summary>
        public bool IsForbidenIndexesOfSubItems = false;

        public string Comments ;

        public AttDbColumn(string _colName)
        {
            this.Name = _colName;
        }

        public List<string> GetCreateColumnStr_ArrayColumns(Type _memberType, string _newDbColName)
        {

            List<string> rstList = new List<string>();
            for (int i=0; i< this.MaxLength; i++)
            {
                rstList.Add(GetCreateColumnStr_singleColumn(_memberType, _newDbColName + i, EUseLength.ArrayItemMaxLength));
            }

            return rstList;
        }//GetCreateColumnStr_ArrayColumns


        public enum EUseLength
        {
            NoLength,
            MaxLength,
            ArrayItemMaxLength
        }//enum EUseLength

        public string GetCreateColumnStr_singleColumn(Type _memberType, string _newDbColName, bool _isUseMaxLength)
        {
            return GetCreateColumnStr_singleColumn(_memberType, _newDbColName, _isUseMaxLength ? EUseLength.MaxLength : EUseLength.NoLength);
        }//GetCreateColumnStr_singleColumn()

        public string GetCreateColumnStr_singleColumn(Type _memberType, string _newDbColName, EUseLength _eUseLength)
        {
            if (_newDbColName == null || _newDbColName.Length == 0)
                throw new Exception($"_newDbColName '{_memberType.Name}' is empty or null.");


            string dataTypeName;
            if (this.DataType == EColumnDataType.SameAsCodeDataType)
                dataTypeName = CodeType2DbType(_memberType);
            else
                dataTypeName = this.DataType.ToString();

            switch(_eUseLength)
            {
                case EUseLength.MaxLength:
                    {
                        if (this.MaxLength >= 0)
                            dataTypeName += $"({this.MaxLength})";

                        break;
                    }
                case EUseLength.ArrayItemMaxLength:
                    {
                        if (this.ArrayItemMaxLength >= 0)
                            dataTypeName += $"({this.ArrayItemMaxLength})";

                        break;
                    }
                case EUseLength.NoLength:
                    break;

            }//switch(_eUseLength)

            string notNullStr = this.IsNotNull ? "NOT NULL" : string.Empty;

            //string defaltValue;

            string commentsStr = this.Comments == null ? string.Empty : $"COMMENT '{CSqlHelper.EncodeString2SqlString_Insert(this.Comments)}'";

            //`acc_id` BIGINT(20) NOT NULL DEFAULT '0'; `testcol` INT(2) NOT NULL AUTO_INCREMENT,
            string dataTypeStr = $"`{_newDbColName}` {dataTypeName} {notNullStr} {this.AutoFill2DefaultValueStr()} {commentsStr}";// `testcol` INT(2) NOT NULL AUTO_INCREMENT,";

            return dataTypeStr;
        }//GetCreateColumnStr_singleColumn

        public string AutoFill2DefaultValueStr()
        {
            switch(this.AutoFill)
            {
                case EAutoFill.AutoIncreasment: return "AUTO_INCREMENT";
                case EAutoFill.EmptyString: return "DEFAULT ''";
                case EAutoFill.NoAutoFill: return string.Empty;
                case EAutoFill.NullItem: return "DEFAULT NULL";
                case EAutoFill.OnCurrentTime: return "DEFAULT CURRENT_TIMESTAMP";
                case EAutoFill.OnUpdateTime: return "DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP";
                case EAutoFill.UseDefaultValueAttribute: return $"DEFAULT '{this.DefaultValue}'";
                case EAutoFill.Zero: return "DEFAULT '0'";
                case EAutoFill.ZeroFill: return "ZEROFILL";
                case EAutoFill.True: return "DEFAULT 'true'";
                case EAutoFill.False: return "DEFAULT 'false'";
            };

            throw new Exception($"unknow this.AutoFill={this.AutoFill}");

        }//AutoFill2DefaultValueStr(EAutoFill _fillType)

        /// <summary>
        /// 从一个MemberInfo中读取唯一的一个 AttDbColumn
        /// </summary>
        /// <param name="_mi"></param>
        /// <returns></returns>
        public static AttDbColumn FromMemberInfo(MemberInfo _mi)
        {

            object[] attbList = _mi.GetCustomAttributes(true);
            foreach (object crrAtt in attbList)
            {
                AttDbColumn attCol = crrAtt as AttDbColumn;

                if (attCol != null)
                {
                    //colNameDir.Add(crrPpt.Name, crrPpt);
                    return attCol;
                }
            }//foreach (object crrAtt in attbList)

            return null;
        }//FromMemberInfo()

        public static string CodeType2DbType(Type _type)
        {
            if (_type.Equals( typeof(short)))
                return "SMALLINT";
            else if (_type.Equals(typeof(int)))
                return "INT";
            else if (_type.Equals(typeof(long)))
                return "BIGINT";
            else if (_type.Equals(typeof(string)))
                return "VARCHAR";
            else if (_type.Equals(typeof(float)))
                return "FLOAT";
            else if (_type.Equals(typeof(double)))
                return "DOUBLE";
            else if (_type.Equals(typeof(char)))
                return "CHAR";
            else if (_type.Equals(typeof(decimal)))
                return "decimal";
            else if (_type.Equals(typeof(DateTime)))
                return "DATETIME";
            else if (_type.Equals(typeof(bool)))
                return "ENUM('true','false')";
            else if (_type.IsEnum)
            {
                string tpStr = string.Empty;
                string[] items = _type.GetEnumNames();
                for (int i= 0; i < items.Length; ++i)
                {
                    string crrItem = items[i];
                    tpStr += (i != items.Length - 1) ? $"'{crrItem}', " : $"'{crrItem}' ";
                }
                string rst = $"ENUM({tpStr})";
                return rst;
            }
            else
                return _type.Name;
        }

        ///// <summary>
        ///// 判断某个数据类型是否是 地理相关的特殊数据类型
        ///// </summary>
        ///// <param name="_eType"></param>
        ///// <returns></returns>
        //public static bool IsGeometryDataType(EColumnDataType _eType )
        //{
        //    if (_eType == EColumnDataType.BIGINT
        //        || _eType == EColumnDataType.Point 
        //        || _eType == EColumnDataType.LineString
        //        || _eType == EColumnDataType.Line
        //        || _eType == EColumnDataType.LinearRing 
        //        || _eType == EColumnDataType.Polygon
        //        || _eType == EColumnDataType.GeometryCollection
        //        || _eType == EColumnDataType.MultiPoint
        //        || _eType == EColumnDataType.MultiLineString
        //        || _eType == EColumnDataType.MultiPolygon
        //        )
        //        return true;
        //    else
        //        return false;

        //}//IsGeometryDataType()
    }//class AttDbColumn


}//namespace
