using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.CloneCtrl
{
    /// <summary>
    /// 深层克隆接口。集成本接口的类支持对内部对象的深层克隆。
    /// 这个接口与Cloner无关，且本接口中的函数可以调用Cloner。因为Cloner并没有调用本接口，因此不会造成冲去或循环调用。
    /// </summary>
    interface IDeepCloneable
    {
        /// <summary>
        /// 深层克隆当前对象
        /// </summary>
        /// <returns>返回克隆后的新对象</returns>
        object DeepClone();

        /// <summary>
        /// 深层拷贝当前对象，并将拷贝的结果放入_newObj中。相当于DeepClone()，但不需要重新new对象了。
        /// </summary>
        /// <param name="_newObj"></param>
        /// <returns></returns>
        object DeepCopy(object _newObj);
    }//interface

}//namespace
