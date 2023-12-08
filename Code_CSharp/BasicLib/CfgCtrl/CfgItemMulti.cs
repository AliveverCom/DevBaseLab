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
//    /// CCfgItemMulti 拥有都个值的配置项。在存储上也需要单独一张表
//    /// </summary>
//    public class CCfgItemMulti : CItemBasic 
//    {

//        //public int nID;

//        //public int DefineID;
//        public CCfgItemDefine DefineIns = null;

//        public string ValueStr
//        {
//            get{ return this.Name; }
//            set{ this.Name = value;}
//        }

//        public CCfgItemMulti( )
//        {
//        }

//        public CCfgItemMulti( int _nID , string _ValueStr )
//            : this(_nID, _ValueStr, null )
//        {

//        }

//        public CCfgItemMulti( int _nID , string _ValueStr, CCfgItemDefine _DefineIns )
//        {
//            this.nID = _nID;
//            this.ValueStr = _ValueStr;
//            this.DefineIns = _DefineIns;
//        }

//    }//class CCfgItemMulti
//}
