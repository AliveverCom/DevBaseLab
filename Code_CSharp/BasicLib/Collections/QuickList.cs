///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2010-01-19</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-01-19</ChangeDate>
///     <ChangeLog>在代码中实现了CQuictList<T>， 并且将ClistTree<T>中的一些与tree控制无关的属性和方法转移到了CQuictList<T>中。最后，让ClistTree<T>继承与CQuictList<T></ChangeLog>
///  </ChangeHistory>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog>切换工程：将这个文件从CreatBone.InfoSysBuilder.Generic移动到Alivever.Com.DevBasic.BasicLib.</ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections
{
    /// <summary>
    /// 快速列表。 列表中提供一个常规性的List[T]用于存储有顺序的列表。同时还提供一个Dictionary[string, T ]，用来进行List内部元素的快速检索
    /// 注：由于.Net2.0-3.5都不支持[T] 的==操作。因此这里临时要求T必须继承于 IComparable<T>接口。待今后.Net支持以后，便可以去掉这个约束了。
    /// </summary>
    [Serializable]
    public class CQuickList<T> : IEnumerable<T>//, IEnumerable<KeyValuePair<string,T>>
        //where T:  IComparable<T>
    {

        /// <summary>
        /// 一个有顺序的动态数组
        /// </summary>
        protected List<T> ItemsLevel0 = new List<T>();

        /// <summary>
        /// 模板类型(string _Key, T _Value)。针对IdName的快速检索，其中_IdName必须是唯一的。
        /// </summary>
        protected Dictionary<string, T> IndexLevel0 = new Dictionary<string, T>();

        /// <summary>
        /// 返回当前层级上的所有Keys
        /// </summary>
        public Dictionary<string, T>.KeyCollection Keys { get { return this.IndexLevel0.Keys; } }

        /// <summary>
        /// 该树种的成员是否都是唯一的。
        /// 若IsNodeExclusive=True则在addItem时若发现重复的项目就会抛出异常,同时，remove的时候只删掉第一个发现的对象
        /// 若IsNodeExclusive=False则在addItem时会忽略异常，而remove的时候会删除所有节点上的改对象
        /// </summary>
        public bool IsNodeExclusive = true;

        #region IEnumerable<T> Members

        public virtual IEnumerator<T> GetEnumerator()
        {
            foreach( KeyValuePair<string,T> crrPair in this.IndexLevel0 )
            {
                yield return crrPair.Value;
            }
                        
        }

        //public virtual IEnumerator<KeyValuePair<string,T>> GetEnumerator()
        //{
        //    foreach (KeyValuePair<string, T> crrPair in this.IndexLevel0)
        //    {
        //        yield return crrPair;
        //    }

        //}

        #endregion



        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return IndexLevel0.GetEnumerator();
        }

        #endregion

        public virtual int Count { get { return this.ItemsLevel0.Count; } }

        #region AddItem
        ///// <summary>
        ///// 代理。当试图向本类中增加新元素时之前出发。
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        ///// <returns>True表示可以继续执行AddItem, False表示取消本次插入操作，AddItem将什么也不做</returns>
        //public delegate bool AddItem_EventHandler(string _Key, T _Value);

        /// <summary>
        /// AddItem_PreEventHandler 的简化注册事件
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> AddItem_PreEvent;

        /// <summary>
        /// AddItem_PostEvent 的简化注册事件
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> AddItem_PostEvent;

        /// <summary>
        /// 按顺序增加一条新记录.为了确保插入成功，请首先使用Contains()来检测_IdName是否已经存在
        /// 若IsNodeExclusive=True则在addItem时若发现重复的项目就会抛出异常,同时，remove的时候只删掉第一个发现的对象
        /// 若IsNodeExclusive=False则在addItem时会忽略异常，而remove的时候会删除所有节点上的改对象
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public virtual T AddItem(string _Key, T _Value)
        {
            //如果当前的Key已经存在
            if (this.IndexLevel0.ContainsKey(_Key) && IsNodeExclusive)
            {
                ////试图插入一个已经存在的对象
                //if (this.IndexLevel0[_Key].CompareTo( _Value ) == 0 )
                //    return true; //试图插入一个已经存在的对象，直接返回true
                //else
                //return false;//试图插入一个Key相同，但是_Value不同的对象。报错
                throw new Exception("插入对象时出错，_Key[" + _Key + "]已经存在。");
            }

            //如果事件被相应后返回失败
            if (AddItem_PreEvent != null)
            {
                AddItem_PreEvent(this, new CStrTEventArgs<T>(_Key, _Value));
                //return default(T);
            }

            this.ItemsLevel0.Add(_Value);

            //当IsNodeExclusive=false的时候_Key中的内容可以几经存在了。因此需要判断一下，以避免不必要的异常。
            if (!this.IndexLevel0.ContainsKey(_Key))
                this.IndexLevel0.Add(_Key, _Value);

            if (AddItem_PostEvent != null)
                AddItem_PostEvent(this, new CStrTEventArgs<T>(_Key, _Value));

            return _Value;

        }
        
        ///// <summary>
        ///// 为XML序列化而增加。如有问题，可以删除此函数。
        ///// </summary>
        ///// <param name="_pair"></param>
        //protected void Add(KeyValuePair<string,T> _pair)
        //{
        //    this.AddItem( _pair.Key, _pair.Value);
        //}
        #endregion

        /// <summary>
        /// 所有Items中，是否包含这个Key
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public bool ContainsKey(string _Key)
        {
            return this.IndexLevel0.ContainsKey(_Key);
        }

        /// <summary>
        /// 所有Items中，是否包含这个_Value
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public bool ContainsValue(T _Value)
        {
            return this.IndexLevel0.ContainsValue(_Value);

        }

        #region RemoveItem
        ///// <summary>
        ///// 代理。当试图删除本类中某元素时之前出发。
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        ///// <return>true可以继续删除操作，false阻止该删除操作</return>
        //public delegate bool RemoveItem_EventHandler(string _Key);

        /// <summary>
        /// AddItem_PreEventHandler 的简化注册事件
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItem_PreEvent;

        /// <summary>
        /// RemoveItem_PreEvent 的简化注册事件
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItem_PostEvent;

        /// <summary>
        /// 将一个元素从本类中删除掉。同时删除对象和他的索引
        /// </summary>
        /// <param name="_Key"></param>
        public virtual void RemoveItem(string _Key)
        {
            if (!this.ContainsKey(_Key))
                return;

            if (RemoveItem_PreEvent != null)
                RemoveItem_PreEvent(this, new CStrTEventArgs<T>(_Key, this[_Key]));

            this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
            this.IndexLevel0.Remove(_Key);

            if (RemoveItem_PostEvent != null)
                RemoveItem_PostEvent(this, new CStrTEventArgs<T>(_Key, this[_Key]));
        }//RemoveItem（）
        
        #endregion

        #region RemoveItemLink
        ///// <summary>
        ///// 代理。当试图删除本类中某元素时之前出发。
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        ///// <return>true可以继续删除操作，false阻止该删除操作</return>
        //public  delegate bool RemoveItemLink_EventHandler(string _Key);

        /// <summary>
        /// AddItem_PreEventHandler 的简化注册事件
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItemLink_PreEvent;

        /// <summary>
        /// AddItem_PreEventHandler 的简化注册事件
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItemLink_PostEvent;

        /// <summary>
        /// 以“链接逻辑”将一个元素从本类中删除掉。即直到没有人再引用该对象时，该对象才真正地从外部对象池中被删掉---与Unix File Link的逻辑是一样的。
        /// 删除前触发事件RemoveItemLink_PreEvent
        /// </summary>
        /// <param name="_Key"></param>
        public virtual void RemoveItemLink(string _Key)
        {
            if (!this.ContainsKey(_Key))
                return;

            if (RemoveItemLink_PreEvent != null)
                RemoveItemLink_PreEvent(this, new CStrTEventArgs<T>(_Key, this[_Key]));
                //return;

            this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
            this.IndexLevel0.Remove(_Key);

            if (RemoveItemLink_PostEvent != null)
                RemoveItemLink_PostEvent(this, new CStrTEventArgs<T>(_Key, this[_Key]));
        }//RemoveItem（）

        #endregion

        /// <summary>
        /// 利用_Key，直接检索一个对象
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public virtual T this[string _Key]
        {
            get { return this.IndexLevel0[_Key]; }
        }

        /// <summary>
        /// 利用List[T]中元素的坐标进行检索。
        /// 注：这个方法不支持 Item内部潜逃的其他Item的检索。因此在CListTree的控制当中，本方法只能操作Level0中的Items
        /// </summary>
        /// <param name="_Index"></param>
        /// <returns></returns>
        public T this[int _Index]
        {
            get { return this.ItemsLevel0[_Index]; }
        }

        /// <summary>
        /// New一个全新的List[T] 对象，并将所有的Value的引用复制到这个LIst中，然后返回该List
        /// </summary>
        /// <returns></returns>
        public virtual List<T> AllItem2List()
        {
            List<T> rstList = new List<T>();

            rstList.AddRange( this.ItemsLevel0 );

            return rstList;

        }//AllItem2List()

        /// <summary>
        /// 将一个新列表_newItems中的所有元素（的引用）加入到当前列表中。
        /// 每个元素的加入规则同this.AddItem()
        /// </summary>
        /// <param name="_newItems"></param>
        public virtual void AddRange(CQuickList<T> _newItems)
        {
            foreach (KeyValuePair<string,T> crrPair in _newItems.IndexLevel0)
            {
                this.AddItem(crrPair.Key, crrPair.Value);
            }//foreach
        }//AddRange()
    }//class CQuickList
}//namespace
