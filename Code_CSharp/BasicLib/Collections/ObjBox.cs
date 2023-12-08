///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2011-02-5</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2011-02-5</ChangeDate>
///     <ChangeLog>
///     创建了CObjBox基本类定义，但是还没有实现完毕
///     这个类最初是为了InfoSysBuilder工程，方便对各类对象进行集中储存和调阅管理所创立的。
///     
///     </ChangeLog>
///  </ChangeHistory>
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections
{
    /// <summary>
    /// 一个对象容器。它负责将一个“priject”中的所有对象集中地进行管理和保存。
    /// 而对于Project中其他各个元素之间的关联，这些元素只记录相应的ID即可。
    /// 需要的时候，用ID到这个ObjBox来取就可以了。
    /// 注：
    /// 1。为了简便起见，在本类实现的最初阶段。this.Itmes进实现T类。而对于格式CPair[T 存储对象，int 对象计数]的高级支持则留在今后实施。
    /// 2。理论上讲，一般不建议两个Key同时指向同一个Obj。可以使用GetKeys(T _obj)来验证数据的有效性。
    /// </summary>
    [Serializable]
    public class CObjBox <T> where T : class //: IEnumerable<T> 
    {
        /// <summary>
        /// 本管理器中所有的对象
        /// 格式[string IdStr ,T Obj ]
        /// </summary>
        protected Dictionary<string, T> Items = new Dictionary<string,T>();//Dictionary<string, CPair<T, int>> Items;

        /// <summary>
        /// 利用_Key，直接检索一个对象
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public virtual T this [string _Key] //: where K:T
        {
            get { return this.Items[_Key]; }

            
        }

        /// <summary>
        /// 返回内部所有的Obj。
        /// 注：如果两个Key同时指向同一个Obj的话，则返回两次该Obj，且顺序随即。
        /// </summary>
        public virtual IEnumerable<T> AllItems
        {
            get
            {
                foreach (KeyValuePair<string, T> crrPair in this.Items)
                {
                    yield return crrPair.Value;
                }
            }
        }//AllItems

        /// <summary>
        /// 得到所有类型是_objType的对象
        /// </summary>
        /// <param name="_ProjKey"></param>
        /// <param name="_objType"></param>
        /// <returns></returns>
        public virtual IEnumerable<KType> GetItems<KType>()where KType:class
        {
            List<KType> rst = new List<KType>();

            foreach( T crrItem in this.Items.Values)
            {
                if ((crrItem.GetType() == typeof(KType)))
                    rst.Add(crrItem as KType);
            }
                
            return rst;

        }

        /// <summary>
        /// 得到所有类型是_objType的对象
        /// </summary>
        /// <param name="_ProjKey"></param>
        /// <param name="_objType"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetItems(Type objType) 
        {
            IEnumerable<T> rst = from T crrItem
                                     in this.Items.Values
                                     where (crrItem.GetType() == objType)
                                     select crrItem;
            return rst.ToArray();

        }


        /// <summary>
        /// 增加一个元素。
        /// 算法：
        /// 1。当CObjBox以前不存在_Key的时候，直接插入_Obj
        /// 2。当CObjBox以前存在_Key的时候，如果CObjBox[_Key]的地址与_Obj相同，则什么也不做，直接返回。因为这个对象已经被插入过了--这里不会报告重复插入一个相同对象的异常。。
        /// 3。当CObjBox以前存在_Key的时候，如果CObjBox[_Key]的地址与_Obj不同，则抛出异常。并记录Log
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_Obj"></param>
        public virtual void Add(string _Key, T _Obj)
        {
            if (this.Items.ContainsKey(_Key))
            {
                if (this.Items[_Key] != _Obj)
                {
                    throw new Exception("试图插入连个内存地址不同的Value。Key="+_Key);
                }
                else
                    return;
            }

            this.Items.Add(_Key,_Obj);
           
        }

        /// <summary>
        /// 清楚内部所有对象.
        /// </summary>
        public virtual void Clear()
        {
            this.Items.Clear();
        }

        //#region IEnumerable<T> Members

        //IEnumerator<T> IEnumerable<T>.GetEnumerator()
        //{
        //    //throw new NotImplementedException();
        //    yield return this.Items.GetEnumerator().Current.Value;
        //}

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    //throw new NotImplementedException();
        //    return this.Items.GetEnumerator();
        //}

        /// <summary>
        /// 判断CObjBox中是否包含指定的_Key
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public bool ContainsKey(string _Key)
        {
            return this.Items.ContainsKey(_Key);

        }//ContainsKey()

        /// <summary>
        /// 返回所有的Keys
        /// </summary>
        public IEnumerable<string> Keys
        {
            get
            {
                return this.Items.Keys.ToList();
            }
        }

        /// <summary>
        /// 根据给定的_obj，返回其所对应的Keys。
        /// 如果两个Key同时指向同一个Obj的话，则返回两个不同的Key。
        /// 这个方法通常是配合进行数据有效性检验而使用的--检查是否存在逻辑错误。
        /// 理论上讲，一般不建议两个Key同时指向同一个Obj
        /// </summary>
        /// <param name="_obj"></param>
        /// <returns></returns>
        public IEnumerable<string> GetKeys(T _obj)
        {
            IEnumerable<string> rst = from string crrKey
                                      in this.Keys
                                      where this.Items[crrKey] == _obj
                                      select crrKey;
            return rst;
        }//GetKeys(T _obj)

        /// <summary>
        /// 返回当前ObjBox中所有元素的数量
        /// </summary>
        public int Count { get { return this.Items.Count; } }

        /// <summary>
        /// 跟据_Key删除相应的数据项。如果_Key并不存在，则什么也不做。不会抛出异常。
        /// </summary>
        /// <param name="_Key"></param>
        public void Remove(string _Key)
        {
            if (!this.ContainsKey(_Key))
                return;

            this.Items.Remove(_Key);

        }//Remove(string _Key) 

        //#endregion//IEnumerable<T> Members
    }//class CObjBox
}//namespace
