///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2011-01-27</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections //BoneCreation.InfoSys.Desingers.DesignerExport.CR.RFAI_TplDoc
{
    /// <summary>
    /// 由于C#的KeyValuePair
    /// </summary>
    public class CPair<TItem1, TItem2>
    {
        public virtual TItem1 Item1 { get; set; }

        public virtual TItem2 Item2 { get; set; }

        public CPair(TItem1 _item1, TItem2 _item2)
        {
            this.Item1 = _item1;
            this.Item2 = _item2;
        }

    }//class CPair
}//namespace
