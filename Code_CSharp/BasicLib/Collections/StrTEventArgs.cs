using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections
{
    /// <summary>
    /// 一个传递 [string,T] 两个参数的事件参数。多用于QuickList及其派生类的事件响应函数
    /// </summary>
    public class CStrTEventArgs<T> : CTEventArgs<T>
    {
        public string KeyStr = string.Empty;

        //public T ValueT;

        public CStrTEventArgs(string _KeyStr, T _ValueT) : base( _ValueT )
        {
            this.KeyStr = _KeyStr;
            //this.ValueT = _ValueT;
        }
    }//class CStrTEventArgs
}//namespace
