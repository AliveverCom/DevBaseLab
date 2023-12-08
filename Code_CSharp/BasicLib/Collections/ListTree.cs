///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-12-30</CreaterDate>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-01-19</ChangeDate>
///     <ChangeLog>�ڴ�����ʵ����CQuictList<T>�� ���ҽ�ClistTree<T>�е�һЩ��tree�����޹ص����Ժͷ���ת�Ƶ���CQuictList<T>�С������ClistTree<T>�̳���CQuictList<T></ChangeLog>
///  </ChangeHistory>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-13</ChangeDate>
///     <ChangeLog>�л����̣�������ļ���CreatBone.InfoSysBuilder.Generic�ƶ���Alivever.Com.DevBasic.BasicLib.</ChangeLog>
///  </ChangeHistory>
///  <ChangeHistory>
///     <Engineer>Charles Shao </Engineer><ChangeDate>2010-03-24</ChangeDate>
///     <ChangeLog>����VS2008���ܹ��ڳ�����ֱ�ӱ��ϳ��̳���ICListTreeable�Ķ�����������ⲿʹ��ICListTreeable����
///                ��ʱ�򶼱��벻�ò�ǿ��װ����CListTree.ICListTreeable����ܹ����ýӿڵ����ݣ����䲻���㡣
///                ��ˣ���ICListTreeableת�Ƶ�һ�������Ľӿڶ����У���CListTree���Ƴ���
///     </ChangeLog>
///  </ChangeHistory>
///</FileHistory>


using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.Collections
{
    /// <summary>
    /// ���ڼ���Ҫʹ�� List����ʾһ����˳������ͬʱ����Ҫ����HashTable���п��ټ����������ṩһ���ۺϵ���
    /// ��Ȼ����Ҳ����ʹ�� List���洢һ���������б�Ȼ������HashTable���п��ٵ�ID������
    /// ʹ��CQuictList.ItemsV��ʾ��һ���������еĶ�����Բ�Ψһ
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
        //    /// ���ص�ǰ�����IdStr
        //    /// </summary>
        //    string LtItemIdStr { get; }

        //    /// <summary>
        //    /// ���û�õ��Լ��ĸ��ڵ�
        //    /// </summary>
        //    //T MotherRef{ get; set; }
        //}

        /// <summary>
        /// ���������������������. 
        /// ע�⣬�������Ӧ�ñ�����ΪC++�е�Friend �����������C#���������ָ�������ʱ��Ȩ�޴�protected��Ϊpublic
        /// </summary>
        protected Dictionary<string, T> IndexAllItems = new Dictionary<string, T>();

        /// <summary>
        /// �������������ֽڵ��Keys
        /// </summary>
        public Dictionary<string, T>.KeyCollection KeysOfTree { get { return this.IndexAllItems.Keys;  } }

        public override event EventHandler<CStrTEventArgs<T>> RemoveItemLink_PreEvent;
        public override event EventHandler<CStrTEventArgs<T>> RemoveItem_PreEvent;
        //public override event EventHandler<CStrTEventArgs<T>> AddItemLink_PostEvent;
        public override event EventHandler<CStrTEventArgs<T>> AddItem_PostEvent;
        public override event EventHandler<CStrTEventArgs<T>> RemoveItem_PostEvent;

        /// <summary>
        /// ����_Key��ֱ�Ӽ���������е�����һ������--��������Level0�ģ������������ġ�
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public override T this[string _Key]
        {
            get { return this.IndexAllItems[_Key]; }
        }

        /// <summary>
        /// ���������������еĲ��ظ��Ľڵ�
        /// </summary>
        public virtual int CountTreeDistinctly { get { return this.IndexAllItems.Count; } }

        /// <summary>
        /// �õ�������ϵ�����Key���������������������key
        /// </summary>
        public Dictionary<string, T>.KeyCollection KeysInTree { get { return this.IndexAllItems.Keys; } }

        /// <summary>
        /// ���Ե�ǰ�ڵ�Ϊ�������������Ƿ�������Key
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        public bool ContainsKeyInTree(string _Key)
        {
            return this.IndexAllItems.ContainsKey(_Key);
            
        }

        /// <summary>
        /// ���ƶ�Ԫ�ؼ��뵽��ǰLevel0�㡣
        /// </summary>
        /// <param name="_IdName"></param>
        /// <param name="_Tag"></param>
        /// <returns>�Ƿ����ɹ���</returns>
        public override T AddItem(string _Key, T _Value)
        {
            //�������ʧ����ֱ�ӷ���
            if (base.AddItem(_Key, _Value) == null)
                return default(T);

            //����Ԫ�ؼ��뵽IndexAllItems��
            if (!IndexAllItems.ContainsKey(_Key))
                IndexAllItems.Add(_Key, _Value);

            //����Ԫ���е�������Ԫ���������뵽��ǰIndexAllItems�С�
            foreach (string crrKey in _Value.SubListTree.Keys)
            {
                this.IndexAllItems.Add(crrKey, _Value.SubListTree[crrKey]);

                //�ٴ�֪ͨ�Լ�����һ����ڵ��������Ԫ��
                if (this.AddItem_PostEvent != null )
                    this.AddItem_PostEvent(this, new CStrTEventArgs<T>(crrKey, _Value.SubListTree[crrKey]));
            }

            _Value.SubListTree.RemoveItemLink_PostEvent += this.OnPostRemoveSubItem;
            _Value.SubListTree.RemoveItem_PostEvent += this.OnPostRemoveSubItem;
            _Value.SubListTree.AddItem_PostEvent += this.OnPostAddSubItem;

            //�������ϼ����ݸ���Ϣ
            if (this.AddItem_PostEvent != null)
                this.AddItem_PostEvent(this, new CStrTEventArgs<T>(_Key, _Value));

            return _Value;
        }//AddInTreeLevel0( string _IdName , T _Tag )

        /// <summary>
        /// Newһ��ȫ�µ�List[T] ���󣬲������е�Value�����ø��Ƶ����LIst�У�Ȼ�󷵻ظ�List
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
        /// Newһ��ȫ�µ�Dictionary[T] ���󣬲������е�Value�����ü���Key���Ƶ����Dictionary�У�Ȼ�󷵻ظ�Dictionary
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
        ///// �����е�
        ///// </summary>
        ///// <param name="_NodeKey"></param>
        ///// <param name="_Key"></param>
        ///// <param name="_Value"></param>
        //public T AddIntoNode(string _NodeKey, string _Key, T _Value)
        //{

        //    //�����ǰ��Key�Ѿ�����
        //    if (!this.IndexLevel0.ContainsKey(_NodeKey) )
        //    {
        //        throw new Exception("�����ǰ��Key[" + _Key + "]�Ѿ�����");
        //    }

        //    this.ItemsLevel0.Add(_Value);
        //    this.IndexLevel0.Add(_Key, _Value);

        //    return IndexLevel0[_NodeKey].GetSubListTree().AddIntoTreeLevel0(_Key, _Value);

        //}//AddInTreeLevel0( string _IdName , T _Tag )

        /// <summary>
        /// ���ظ��������ж������Ŀ����������Ƕ���еĶ�������
        /// </summary>
        public override int Count { get { return this.IndexLevel0.Count; } }

        /// <summary>
        /// �����ص�һ������ϵĶ�������
        /// </summary>
        public int CountFirstLevelItems { get { return this.ItemsLevel0.Count; } }

        /// <summary>
        /// ��һ��_Key����ӱ���ɾ��������ɾ���������ֵĸö���
        /// </summary>
        /// <param name="_Key"></param>
        public override void RemoveItem(string _Key)
        {
            this.RemoveItem( _Key , false, false );
            //base.RemoveItem(_Key);
        }

        /// <summary>
        /// ����ѡ��һ��Ԫ�س��׵ش�һ������ɾ������
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_bIncludeSubtree"></param>
        public void RemoveItem(string _Key, bool _bIncludeSubtree)
        {
            this.RemoveItem(_Key, false, _bIncludeSubtree);
            //base.RemoveItem(_Key);
        }

        /// <summary>
        /// �ԡ������߼�����һ��Ԫ�شӱ�����ɾ��������ֱ��û���������øö���ʱ���ö���������ش��ⲿ������б�ɾ��---��Unix File Link���߼���һ���ġ�
        /// ɾ��ǰ�����¼�RemoveItemLink_PreEvent
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_bDelAsLink">�Ƿ�ʹ��Linkɾ���߼�. trueʱ����RemoveItemLink_PreEvent��Falseʱ����RemoveItem_PreEvent</param>
        /// <param name="_bIncludeSubtree">�Ƿ�������������еĶ���</param>
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

            //����ȷ�����������Ƿ��Ѿ������˸�Ԫ�ء����������ɾ����
            //���⻹��Ҫɾ����ɾ����������������Items��������
            this.RemoveLinksOfSubItem(deletedItem);

            if (this.RemoveItem_PostEvent != null)
                RemoveItem_PostEvent(this, new CStrTEventArgs<T>(_Key, deletedItem));
        }//RemoveItem����

        /// <summary>
        /// ���Լ���������RemoveItemLink_PreEvent��ʱ����
        /// </summary>
        /// <param name="_Key"></param>
        /// <returns></returns>
        protected void OnPostRemoveSubItem(object _sender, CStrTEventArgs<T> e )
        {

            this.RemoveLinksOfSubItem(e.ValueT);
            //this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
            //���ÿһ���ڵ��Key����Ψһ����ֱ��ɾ��������
            //if (this.IsNodeExclusive)
            //{
            //this.IndexAllItems.Remove(e.KeyStr);

            //ɾ��e������������Ŀ��Link����
            //���IsNodeExclusive==true������Ҫȷ�����������Ƿ񻹰��������_Key�Ķ���

        }//OnPostRemoveSubItem()

        /// <summary>
        /// �ڱ���AllItems������ɾ��ָ�����������������е�����Ԫ��
        /// �˷����������RemoveItem�������ϼ��ڵ����Ӧ�¼��ĺ�����������
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

            //����������������ֶ�����ӵ��e.KeyStr����e.KeyStr�ӱ����IndexAllItems��ɾ��
            if (!bFopundEkey)
            {
                this.IndexAllItems.Remove(_delItem.LtItemIdStr);
            }

        }//RemoveLinksOfSubItem()

        ///// <summary>
        ///// ���Լ���������RemoveItemLink_PreEvent��ʱ����
        ///// </summary>
        ///// <param name="_Key"></param>
        ///// <returns></returns>
        //protected bool OnRemoveSubItemLink(string _Key)
        //{
        //    this.ItemsLevel0.Remove(this.IndexLevel0[_Key]);
        //    this.IndexM.Remove(_Key);
        //}

        /// <summary>
        /// ����������������Ԫ���Ժ󣬸��ڵ��AllItemsIndexҲ��Ҫ������Ӧ�Ķ�������
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
