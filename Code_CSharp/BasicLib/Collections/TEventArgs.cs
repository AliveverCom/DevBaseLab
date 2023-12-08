using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections
{
    /// <summary>
    /// 只有一个模板参数的EventArgs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CTEventArgs<T> : EventArgs
    {
        public T ValueT;

        public CTEventArgs(T _ValueT)
        {
            this.ValueT = _ValueT;
        }
    }//class CTEventArgs<T>
}//namespace
