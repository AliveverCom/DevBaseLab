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
using System.Linq;
using System.Text;

namespace Alivever.Com.Compiler
{
    /// <summary>
    /// 专门扶助生成一些预定义的CompileInfoIns
    /// </summary>
    class InfoMaker
    {
        #region predefined InfoDef
        public static  CCompileInfoIns BlockerError(string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine =CCompileInfoDefine.BlockerError
            };
        }//

        public static  CCompileInfoIns CriticalError (string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine =CCompileInfoDefine.CriticalError
            };
        }//

        public static  CCompileInfoIns MajorError(string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine = CCompileInfoDefine.MajorError
            };
        }//

        public static  CCompileInfoIns NormalError (string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine = CCompileInfoDefine.NormalError
            };
        }//

        public static  CCompileInfoIns MinorError (string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine = CCompileInfoDefine.MinorError
            };
        }//

        public static  CCompileInfoIns CriticalWarrning (string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine = CCompileInfoDefine.CriticalWarrning
            };
        }//

        public static  CCompileInfoIns NormalWarrning (string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine = CCompileInfoDefine.NormalWarrning
            };
        }//

        public static  CCompileInfoIns NormalSuggestion (string _InfoDescStr )
        {
            return new CCompileInfoIns()
            {
                 DescSelfStr = _InfoDescStr,
                 InfoDefine = CCompileInfoDefine.NormalSuggestion
            };
        }//

        public static CCompileInfoIns NormalMessage(string _InfoDescStr)
        {
            return new CCompileInfoIns()
            {
                DescSelfStr = _InfoDescStr,
                InfoDefine = CCompileInfoDefine.NormalMessage
            };
            
        }//



        #endregion //predefined InfoDef

    }//class InfoMaker
}//namespace
