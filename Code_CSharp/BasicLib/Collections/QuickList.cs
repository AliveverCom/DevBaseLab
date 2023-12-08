///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2010-01-19</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-01-19</ChangeDate>
///     <ChangeLog>�ڴ�����ʵ����CQuictList<T>�� ���ҽ�ClistTree<T>�е�һЩ��tree�����޹ص����Ժͷ���ת�Ƶ���CQuictList<T>�С������ClistTree<T>�̳���CQuictList<T></ChangeLog>
///  </ChangeHistory>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog>�л����̣�������ļ���CreatBone.InfoSysBuilder.Generic�ƶ���Alivever.Com.DevBasic.BasicLib.</ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections
{
    /// <summary>
    /// �����б� �б����ṩһ�������Ե�List[T]���ڴ洢��˳����б�ͬʱ���ṩһ��Dictionary[string, T ]����������List�ڲ�Ԫ�صĿ��ټ���
    /// ע������.Net2.0-3.5����֧��[T] ��==���������������ʱҪ��T����̳��� IComparable<T>�ӿڡ������.Net֧���Ժ󣬱����ȥ�����Լ���ˡ�
    /// </summary>
    [Serializable]
    public class CQuickList<T> : IEnumerable<T>//, IEnumerable<KeyValuePair<string,T>>
        //where T:  IComparable<T>
    {

        /// <summary>
        /// һ����˳��Ķ�̬����
        /// </summary>
        protected List<T> ItemsLevel0 = new List<T>();

        /// <summary>
        /// ģ������(string _Key, T _Value)�����IdName�Ŀ��ټ���������_IdName������Ψһ�ġ�
        /// </summary>
        protected Dictionary<string, T> IndexLevel0 = new Dictionary<string, T>();

        /// <summary>
        /// ���ص�ǰ�㼶�ϵ�����Keys
        /// </summary>
        public Dictionary<string, T>.KeyCollection Keys { get { return this.IndexLevel0.Keys; } }

        /// <summary>
        /// �����ֵĳ�Ա�Ƿ���Ψһ�ġ�
        /// ��IsNodeExclusive=True����addItemʱ�������ظ�����Ŀ�ͻ��׳��쳣,ͬʱ��remove��ʱ��ֻɾ����һ�����ֵĶ���
        /// ��IsNodeExclusive=False����addItemʱ������쳣����remove��ʱ���ɾ�����нڵ��ϵĸĶ���
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
        ///// ��������ͼ������������Ԫ��ʱ֮ǰ������
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        ///// <returns>True��ʾ���Լ���ִ��AddItem, False��ʾȡ�����β��������AddItem��ʲôҲ����</returns>
        //public delegate bool AddItem_EventHandler(string _Key, T _Value);

        /// <summary>
        /// AddItem_PreEventHandler �ļ�ע���¼�
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> AddItem_PreEvent;

        /// <summary>
        /// AddItem_PostEvent �ļ�ע���¼�
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> AddItem_PostEvent;

        /// <summary>
        /// ��˳������һ���¼�¼.Ϊ��ȷ������ɹ���������ʹ��Contains()�����_IdName�Ƿ��Ѿ�����
        /// ��IsNodeExclusive=True����addItemʱ�������ظ�����Ŀ�ͻ��׳��쳣,ͬʱ��remove��ʱ��ֻɾ����һ�����ֵĶ���
        /// ��IsNodeExclusive=False����addItemʱ������쳣����remove��ʱ���ɾ�����нڵ��ϵĸĶ���
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public virtual T AddItem(string _Key, T _Value)
        {
            //�����ǰ��Key�Ѿ�����
            if (this.IndexLevel0.ContainsKey(_Key) && IsNodeExclusive)
            {
                ////��ͼ����һ���Ѿ����ڵĶ���
                //if (this.IndexLevel0[_Key].CompareTo( _Value ) == 0 )
                //    return true; //��ͼ����һ���Ѿ����ڵĶ���ֱ�ӷ���true
                //else
                //return false;//��ͼ����һ��Key��ͬ������_Value��ͬ�Ķ��󡣱���
                throw new Exception("�������ʱ����_Key[" + _Key + "]�Ѿ����ڡ�");
            }

            //����¼�����Ӧ�󷵻�ʧ��
            if (AddItem_PreEvent != null)
            {
                AddItem_PreEvent(this, new CStrTEventArgs<T>(_Key, _Value));
                //return default(T);
            }

            this.ItemsLevel0.Add(_Value);

            //��IsNodeExclusive=false��ʱ��_Key�е����ݿ��Լ��������ˡ������Ҫ�ж�һ�£��Ա��ⲻ��Ҫ���쳣��
            if (!this.IndexLevel0.ContainsKey(_Key))
                this.IndexLevel0.Add(_Key, _Value);

            if (AddItem_PostEvent != null)
                AddItem_PostEvent(this, new CStrTEventArgs<T>(_Key, _Value));

            return _Value;

        }
        
        ///// <summary>
        ///// ΪXML���л������ӡ��������⣬����ɾ���˺�����
        ///// </summary>
        ///// <param name="_pair"></param>
        //protected void Add(KeyValuePair<string,T> _pair)
        //{
        //    this.AddItem( _pair.Key, _pair.Value);
        //}
        #endregion

        /// <summary>
        /// ����Items�У��Ƿ�������Key
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public bool ContainsKey(string _Key)
        {
            return this.IndexLevel0.ContainsKey(_Key);
        }

        /// <summary>
        /// ����Items�У��Ƿ�������_Value
        /// </summary>
        /// <param name="_Value"></param>
        /// <returns></returns>
        public bool ContainsValue(T _Value)
        {
            return this.IndexLevel0.ContainsValue(_Value);

        }

        #region RemoveItem
        ///// <summary>
        ///// ��������ͼɾ��������ĳԪ��ʱ֮ǰ������
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        ///// <return>true���Լ���ɾ��������false��ֹ��ɾ������</return>
        //public delegate bool RemoveItem_EventHandler(string _Key);

        /// <summary>
        /// AddItem_PreEventHandler �ļ�ע���¼�
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItem_PreEvent;

        /// <summary>
        /// RemoveItem_PreEvent �ļ�ע���¼�
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItem_PostEvent;

        /// <summary>
        /// ��һ��Ԫ�شӱ�����ɾ������ͬʱɾ���������������
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
        }//RemoveItem����
        
        #endregion

        #region RemoveItemLink
        ///// <summary>
        ///// ��������ͼɾ��������ĳԪ��ʱ֮ǰ������
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        ///// <return>true���Լ���ɾ��������false��ֹ��ɾ������</return>
        //public  delegate bool RemoveItemLink_EventHandler(string _Key);

        /// <summary>
        /// AddItem_PreEventHandler �ļ�ע���¼�
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItemLink_PreEvent;

        /// <summary>
        /// AddItem_PreEventHandler �ļ�ע���¼�
        /// </summary>
        public virtual event EventHandler<CStrTEventArgs<T>> RemoveItemLink_PostEvent;

        /// <summary>
        /// �ԡ������߼�����һ��Ԫ�شӱ�����ɾ��������ֱ��û���������øö���ʱ���ö���������ش��ⲿ������б�ɾ��---��Unix File Link���߼���һ���ġ�
        /// ɾ��ǰ�����¼�RemoveItemLink_PreEvent
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
        }//RemoveItem����

        #endregion

        /// <summary>
        /// ����_Key��ֱ�Ӽ���һ������
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public virtual T this[string _Key]
        {
            get { return this.IndexLevel0[_Key]; }
        }

        /// <summary>
        /// ����List[T]��Ԫ�ص�������м�����
        /// ע�����������֧�� Item�ڲ�Ǳ�ӵ�����Item�ļ����������CListTree�Ŀ��Ƶ��У�������ֻ�ܲ���Level0�е�Items
        /// </summary>
        /// <param name="_Index"></param>
        /// <returns></returns>
        public T this[int _Index]
        {
            get { return this.ItemsLevel0[_Index]; }
        }

        /// <summary>
        /// Newһ��ȫ�µ�List[T] ���󣬲������е�Value�����ø��Ƶ����LIst�У�Ȼ�󷵻ظ�List
        /// </summary>
        /// <returns></returns>
        public virtual List<T> AllItem2List()
        {
            List<T> rstList = new List<T>();

            rstList.AddRange( this.ItemsLevel0 );

            return rstList;

        }//AllItem2List()

        /// <summary>
        /// ��һ�����б�_newItems�е�����Ԫ�أ������ã����뵽��ǰ�б��С�
        /// ÿ��Ԫ�صļ������ͬthis.AddItem()
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
