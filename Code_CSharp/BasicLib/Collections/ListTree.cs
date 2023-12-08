///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-12-30</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-01-19</ChangeDate>
///     <ChangeLog>在代码中实现了CQuictList<T>， 并且将ClistTree<T>中的一些与tree控制无关的属性和方法转移到了CQuictList<T>中。最后，让ClistTree<T>继承与CQuictList<T></ChangeLog>
///  </ChangeHistory>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog>切换工程：将这个文件从CreatBone.InfoSysBuilder.Generic移动到Alivever.Com.DevBasic.BasicLib.</ChangeLog>
///  </ChangeHistory>
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
    /// 对于既需要使用 List来表示一棵有顺序树，同时又需要利用HashTable进行快速检索的需求，提供一个综合的类
    /// 当然，你也可以使用 List来存储一个真正的列表，然后利用HashTable进行快速的ID检索。
    /// 使用CQuictList.ItemsV表示的一颗树。其中的对象可以不唯一
    /// </summary>

    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class CListTree<T> : CQuickList<T>
        where T : IListTreeable<T>//, IComparable<T>
    {
        //public interface ICListTreeable
        //{
        //    CListTree<T> SubListTree{get;}

        //    /// <summary>
        //    /// 返回当前对象的IdStr
        //    /// </summary>
        //    string LtItemIdStr { get; }

        //    /// <summary>
        //    /// 设置或得到自己的父节点
        //    /// </summary>
        //    //T MotherRef{ get; set; }
        //}

        /// <summary>
        /// 包含所有子树级别的索引. 
        /// 注意，这个变量应该被申请为C++中的Friend 概念，但是由于C#不存在这种概念，因此暂时将权限从protected变为public
        /// </summary>
        protected Dictionary<string, T> IndexAllItems = new Dictionary<string, T>();

        /// <summary>
        /// 返回所有下属字节点的Keys
        /// </summary>
        public Dictionary<string, T>.KeyCollection KeysOfTree { get { return this.IndexAllItems.Keys;  } }

        public override event EventHandler<CStrTEventArgs<T>> RemoveItemLink_PreEvent;
        public override event EventHandler<CStrTEventArgs<T>> RemoveItem_PreEvent;
        //public override event EventHandler<CStrTEventArgs<T>> AddItemLink_PostEvent;
        public override event EventHandler<CStrTEventArgs<T>> AddItem_PostEvent;
        public override event EventHandler<CStrTEventArgs<T>> RemoveItem_PostEvent;

        /// <summary>
        /// 利用_Key，直接检索这棵树中的任意一个对象--不仅仅是Level0的，而是整棵树的。
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public override T this[string _Key]
        {
            get { return this.IndexAllItems[_Key]; }
        }

        /// <summary>
        /// 返回整棵树上所有的不重复的节点
        /// </summary>
        public virtual int CountTreeDistinctly { get { return this.IndexAllItems.Count; } }

        /// <summary>
        /// 得到这棵树上的所有Key，包括本层和子树的所有key
        /// </summary>
        public Dictionary<string, T>.KeyCollection KeysInTree { get { return this.IndexAllItems.Keys; } }

        /// <summary>
        /// 在以当前节点为根的整棵树中是否包含这个Key
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public bool ContainsKeyInTree(string _Key)
        {
            return this.IndexAllItems.ContainsKey(_Key);
            
        }

        /// <summary>
        /// 将制定元素加入到当前Level0层。
        /// </summary>
        /// <param name="_IdName"></param>
        /// <param name="_Tag"></param>
        /// <returns>是否插入成功。</returns>
        public override T AddItem(string _Key, T _Value)
        {
            //如果插入失败则直接返回
            if (base.AddItem(_Key, _Value) == null)
                return default(T);

            //将新元素加入到IndexAllItems中
            if (!IndexAllItems.ContainsKey(_Key))
                IndexAllItems.Add(_Key, _Value);

            //将新元素中的所有自元素索引加入到当前IndexAllItems中。
            foreach (string crrKey in _Value.SubListTree.Keys)
            {
                this.IndexAllItems.Add(crrKey, _Value.SubListTree[crrKey]);

                //再次通知自己的上一层根节点增加相关元素
                if (this.AddItem_PostEvent != null )
                    this.AddItem_PostEvent(this, new CStrTEventArgs<T>(crrKey, _Value.SubListTree[crrKey]));
            }

            _Value.SubListTree.RemoveItemLink_PostEvent += this.OnPostRemoveSubItem;
            _Value.SubListTree.RemoveItem_PostEvent += this.OnPostRemoveSubItem;
            _Value.SubListTree.AddItem_PostEvent += this.OnPostAddSubItem;

            //继续向上级传递该信息
            if (this.AddItem_PostEvent != null)
                this.AddItem_PostEvent(this, new CStrTEventArgs<T>(_Key, _Value));

            return _Value;
        }//AddInTreeLevel0( string _IdName , T _Tag )

        /// <summary>
        /// New一个全新的List[T] 对象，并将所有的Value的引用复制到这个LIst中，然后返回该List
        /// </summary>
        /// <returns></returns>
        public override List<T> AllItem2List()
        {
            List<T> rstList = new List<T>();

            foreach (string crrKey in IndexAllItems.Keys)
            {
                rstList.Add(this[crrKey]);
            }
            return rstList;

        }//AllItem2List()

        /// <summary>
        /// New一个全新的Dictionary[T] 对象，并将所有的Value的引用及其Key复制到这个Dictionary中，然后返回该Dictionary
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string,T> AllItem2Tree()
        {
            Dictionary<string, T> rstTree = new Dictionary<string, T>();

            foreach (string crrKey in IndexAllItems.Keys)
            {
                rstTree.Add(crrKey,this[crrKey]);
            }
            return rstTree;

        }//AllItem2Tree()


        ///// <summary>
        ///// 在已有的
        ///// </summary>
        ///// <param name="_NodeKey"></param>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        //public T AddIntoNode(string _NodeKey, string _Key, T _Value)
        //{

        //    //如果当前的Key已经存在
        //    if (!this.IndexLevel0.ContainsKey(_NodeKey) )
        //    {
        //        throw new Exception("如果当前的Key[" + _Key + "]已经存在");
        //    }

        //    this.ItemsLevel0.Add(_Value);
        //    this.IndexLevel0.Add(_Key, _Value);

        //    return IndexLevel0[_NodeKey].GetSubListTree().AddIntoTreeLevel0(_Key, _Value);

        //}//AddInTreeLevel0( string _IdName , T _Tag )

        /// <summary>
        /// 返回该树种所有对象的数目。包括各级嵌套中的对象在内
        /// </summary>
        public override int Count { get { return this.IndexLevel0.Count; } }

        /// <summary>
        /// 仅返回第一级层次上的对象数量
        /// </summary>
        public int CountFirstLevelItems { get { return this.ItemsLevel0.Count; } }

        /// <summary>
        /// 将一个_Key对象从本层删除，但不删除其子树种的该对象。
        /// </summary>
        /// <param name="_Key"></param>
        public override void RemoveItem(string _Key)
        {
            this.RemoveItem( _Key , false, false );
            //base.RemoveItem(_Key);
        }

        /// <summary>
        /// 可以选择将一个元素彻底地从一棵树中删除掉。
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_bIncludeSubtree"></param>
        public void RemoveItem(string _Key, bool _bIncludeSubtree)
        {
            this.RemoveItem(_Key, false, _bIncludeSubtree);
            //base.RemoveItem(_Key);
        }

        /// <summary>
        /// 以“链接逻辑”将一个元素从本类中删除掉。即直到没有人再引用该对象时，该对象才真正地从外部对象池中被删掉---与Unix File Link的逻辑是一样的。
        /// 删除前触发事件RemoveItemLink_PreEvent
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_bDelAsLink">是否使用Link删除逻辑. true时触发RemoveItemLink_PreEvent；False时触发RemoveItem_PreEvent</param>
        /// <param name="_bIncludeSubtree">是否包含下属子树中的对象</param>
        public void RemoveItem(string _Key, bool _bDelAsLink, bool _bIncludeSubtree)
        {
            if (!this.ContainsKey(_Key))
                return;

            T deletedItem = this[_Key];

            if (_bDelAsLink && RemoveItemLink_PreEvent!= null)
                RemoveItemLink_PreEvent(this, new CStrTEventArgs<T>(_Key, deletedItem));
                //return;
            else if (!_bDelAsLink && RemoveItem_PreEvent!= null)
                RemoveItem_PreEvent(this, new CStrTEventArgs<T>(_Key, deletedItem ));
                //return;

            //this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
            if (_bIncludeSubtree)
            {
                foreach (T crrItem in this.ItemsLevel0)
                {
                    crrItem.SubListTree.RemoveItem(_Key, _bDelAsLink, _bIncludeSubtree);
                }
            }

            this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
            this.IndexLevel0.Remove(_Key);

            //首先确认下属子树是否已经包含了该元素。如果是则不能删除。
            //另外还需要删除被删除对象下属子树的Items的索引。
            this.RemoveLinksOfSubItem(deletedItem);

            if (this.RemoveItem_PostEvent != null)
                RemoveItem_PostEvent(this, new CStrTEventArgs<T>(_Key, deletedItem));
        }//RemoveItem（）

        /// <summary>
        /// 当自己下属调用RemoveItemLink_PreEvent的时候发上
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        protected void OnPostRemoveSubItem(object _sender, CStrTEventArgs<T> e )
        {

            this.RemoveLinksOfSubItem(e.ValueT);
            //this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
            //如果每一个节点的Key都是唯一的则直接删除并返回
            //if (this.IsNodeExclusive)
            //{
            //this.IndexAllItems.Remove(e.KeyStr);

            //删除e中所有子树项目的Link索引
            //如果IsNodeExclusive==true，则还需要确认下属子树是否还包含有这个_Key的对象

        }//OnPostRemoveSubItem()

        /// <summary>
        /// 在本层AllItems索引中删除指定对象及其所属子树中的所有元素
        /// 此方法用于配合RemoveItem，及其上级节点的响应事件的后续索引处理
        /// </summary>
        /// <param name="_delItem"></param>
        protected void RemoveLinksOfSubItem(T _delItem)
        {
            bool bFoundInSubItem = false, bFopundEkey = false;
            foreach (string crrDelKey in _delItem.SubListTree.Keys)
            {
                if (this.IndexLevel0.ContainsKey(_delItem.LtItemIdStr))
                    bFopundEkey = true;

                if (this.IndexLevel0.ContainsKey(crrDelKey))
                    continue;

                bFoundInSubItem = false;
                foreach (T crrSubItem in this.IndexLevel0.Values)
                {
                    if (crrSubItem.SubListTree.ContainsKeyInTree(_delItem.LtItemIdStr))
                        bFopundEkey = true;

                    if (crrSubItem.SubListTree.ContainsKeyInTree(crrDelKey))
                    {
                        bFoundInSubItem = true;
                        break;
                    }
                }

                if (!bFoundInSubItem)
                    this.IndexAllItems.Remove(crrDelKey);
            }

            //如果下属所有子树种都不在拥有e.KeyStr，则将e.KeyStr从本层的IndexAllItems中删除
            if (!bFopundEkey)
            {
                this.IndexAllItems.Remove(_delItem.LtItemIdStr);
            }

        }//RemoveLinksOfSubItem()

        ///// <summary>
        ///// 当自己下属调用RemoveItemLink_PreEvent的时候发上
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <returns></returns>
        //protected bool OnRemoveSubItemLink(string _Key)
        //{
        //    this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
        //    this.IndexM.Remove(_Key);
        //}

        /// <summary>
        /// 当下属子树增加了元素以后，根节点的AllItemsIndex也需要增加相应的对象索引
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="e"></param>
        protected void OnPostAddSubItem(object _sender, CStrTEventArgs<T> e)
        {
            this.IndexAllItems.Add(
                e.KeyStr,
                e.ValueT );

            if (this.AddItem_PostEvent != null)
                this.AddItem_PostEvent(this, new CStrTEventArgs<T>(e.KeyStr, e.ValueT));
        }//OnPostAddSubItem()

    }//CListHash<T>
}
