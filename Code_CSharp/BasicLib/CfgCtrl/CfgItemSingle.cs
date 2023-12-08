///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 205-1-1</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2011-01-14</ChangeDate>
///     <ChangeLog>
///     发现当前的cfg体系过于复杂，也不符合.net4.0的语法和设计模式。
///     因此暂时注释掉所有过去的cfg体系
///     </ChangeLog>
///  </ChangeHistory>
///</FileHistory>


//using System;

//namespace Alivever.Com.DevBasic.BasicLib.CfgCtrl
//{
//    /// <summary>
//    /// CCfgItemSingle 单值配置项。
//    /// </summary>
//    public class CCfgItemSingle //: CCfgItemDefine
//    {
//        /// <summary>
//        /// 逗号分割的唯一标示字符串，可以和
//        /// </summary>
//        public string KeyIdStr;

//        protected string m_ValueStr = "";

//        public CCfgItemSingle()
//        {
//        }
	
//        public override int IntValue
//        {
//            get{ return 0; }
//            set { m_ValueStr = value.ToString();  }
//        }

//        public override string StrValue
//        {
//            get{ return  m_ValueStr ; }
//            set { m_ValueStr = value; }
//        }

//        public override float FloatValue
//        {
//            get{ return (float)0;}
//            set { m_ValueStr = value.ToString(); }
//        }

//        public override bool BoolValue
//        {
//            get{ return false ;}
//            set { m_ValueStr = value.ToString(); }
//        }

//    }//CCfgItemSingle
//}
