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
//using Alivever.Com.DevBasic.BasicLib.FileSysCtrl;

//namespace Alivever.Com.DevBasic.BasicLib.CfgLib
//{
//    /// <summary>
//    /// CCfgEnvMgrBase 的摘要说明。
//    /// </summary>
//    public abstract class CCfgEnvMgrBase
//    {
//        public CPathMgrBase   m_RootCfgPath;
//        public CCfgEnv       m_CrrEnv;

		
//        public CCfgEnvMgrBase()
//        {
//            m_RootCfgPath = new CPathMgrBase();
//        }
		
//        public CCfgEnvMgrBase( string _RootPath )
//        {
//            m_RootCfgPath = new CPathMgrBase( _RootPath );
//        }
		
//        public virtual string GetCrrCfgPath()
//        {
//            if ( m_CrrEnv == null )
//                return m_RootCfgPath.ToString();

//            return m_RootCfgPath.AddSubPathTemp( m_CrrEnv.m_PathName );
//        }

//    }//class CCfgEnvMgrBase
//}//namespace
