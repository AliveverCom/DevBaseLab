///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 205-1-1</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2011-01-14</ChangeDate>
///     <ChangeLog>
///     ���ֵ�ǰ��cfg��ϵ���ڸ��ӣ�Ҳ������.net4.0���﷨�����ģʽ��
///     �����ʱע�͵����й�ȥ��cfg��ϵ
///     </ChangeLog>
///  </ChangeHistory>
///</FileHistory>

//using System;
//using Alivever.Com.DevBasic.BasicLib.FileSysCtrl;

//namespace Alivever.Com.DevBasic.BasicLib.CfgLib
//{
//    /// <summary>
//    /// CCfgEnvMgrBase ��ժҪ˵����
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
