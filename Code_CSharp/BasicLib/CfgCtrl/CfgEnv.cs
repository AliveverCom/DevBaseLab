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
//using Alivever.Com.DevBasic.BasicLib;

//namespace Alivever.Com.DevBasic.BasicLib.CfgLib
//{
//    /// <summary>
//    /// CCfgEnv 的摘要说明。
//    /// </summary>
//    public class CCfgEnv : CItemBase
//    {
//        /// <summary>
//        /// m_PathName will be used as subpath of FilePath or RegPath
//        /// </summary>
//        public string  m_PathName; 

//        public CCfgEnv()
//        {			
//        }

//        public CCfgEnv( int _nID , string _sName )
//            : this(  _nID ,  _sName ,  _sName ,  ""  )
//        {			
//        }

//        public CCfgEnv( int _nID , string _sName , string _sDesc)
//            : this(  _nID ,  _sName ,  _sName ,  _sDesc  )
//        {			
//        }

//        public CCfgEnv( int _nID , string _sName , string _sPathName , string _sDesc  )
//            : base(  _nID ,  _sName ,   _sDesc  )
//        {			
//            m_PathName = _sPathName;
//        }

//    }//class CCfgEnv
//}//namespace
