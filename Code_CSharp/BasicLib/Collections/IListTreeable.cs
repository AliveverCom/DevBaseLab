///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2010-03-24</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-24</ChangeDate>
///     <ChangeLog>由于VS2008不能够在程序中直接辨认出继承了ICListTreeable的对象，造成了在外部使用ICListTreeable对象
///                的时候都必须不得不强制装换成CListTree.ICListTreeable后才能够调用接口的内容，极其不方便。
///                因此，将ICListTreeable转移到一个独立的接口定义中，从CListTree中移除。
///     </ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections
{
    /// <summary>
    /// 与CListTreeable配套使用。 CListTreeable中的每一个元素都需要实现这个接口。
    /// 
    /// </summary>
    public interface IListTreeable<TItem>  
        where TItem : IListTreeable<TItem>
    {
        CListTree<TItem> SubListTree { get; }

        /// <summary>
        /// 返回当前对象的IdStr
        /// </summary>
        string LtItemIdStr { get; }

        /// <summary>
        /// 设置或得到自己的父节点
        /// </summary>
        //T MotherRef{ get; set; }
    }//interface ICListTreeable
}//namespace
