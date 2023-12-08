///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2011-2-7</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Collections;


namespace Alivever.Com.DevBasic.BasicLib.ToolsCtrl
{
    /// <summary>
    /// 克隆器。 用于对所需对象进行深层克隆或浅层克隆。本类也可以配合[NonCloner]进行更加复杂的克隆过程控制。
    /// 克隆规则：
    /// 1。默认情况下，除了EventHandler不会被克隆以外，其它所有能克隆的都会被克隆。
    /// 2。如果设置了CCloner.IncludeXXXX=false，则不会处理该类型的内容。
    /// 3。如果被标记了[NonCloner]或[NonCloner(nFlogs)]，则符合this.NonCloneFlags的对象不会被处理。
    /// 5。[NonCloner]的部处理原则。
    ///     a. 如果class被标记了[NonCloner]，则单独克隆该类对象，直接返回null
    ///     b. 如果想克隆NonClonerClass[], 则返回新的NonClonerClass[]。新数组长度与源数组相同，但所有元素都是null.
    ///     c. 如果IEmurable[NonClonerClass],则返回的新的同类型IEmurable[NonClonerClass]。但长度是0。即其中不会有任何NonClonerClass元素。
    ///     d. 如果某个class.field被标记了[NonCloner]，则该field的值将被忽略处理。 结果可能是系统默认值或null.
    /// 
    /// 关于[NonCloner]的使用。
    /// 不可被克隆的标记。当CCloner遇到有[NonCloner]这个标记的特定属性时，且( NonCloner.NonCloneFlags & CCloner.NonCloneFlags)!=0，CCloner就会忽略对该目标的克隆。 
    /// 开发人员可以利用这个特性来控制在不同的环境中，在runtime时使某个对象为可克隆或不可克隆。

    /// </summary>
    public class CCloner
    {
        /// <summary>
        /// 当( NonCloner.NonCloneFlags & CCloner.NonCloneFlags)!=0的时候，CCloner就会忽略对该目标的克隆。
        /// 开发人员可以利用这个特性来控制在不同的环境中，在runtime时使某个对象为可克隆或不可克隆。
        /// </summary>
        /// <value>默认值=int.MinValue。即不论该值等于多少。当前对象都会被忽略拷贝。</value>
        public int NonCloneFlags = int.MinValue;//ENonCloneFlags.All;

        /// <summary>
        /// 在拷贝或克隆时，是否处理 EventHandler。False时直接返回null
        /// 由于EventHandler 经常指向自己对象中的一部分，从而造成循环拷贝，因此请慎用。
        /// </summary>
        public bool IncludeEventHandler = false;

        /// <summary>
        /// 在拷贝或克隆时，是否处理 ValueType或string。False时直接返回null
        /// </summary>
        public bool IncludeValueType = true;

        /// <summary>
        /// 在拷贝或克隆时，是否处理 ArrayType。False时直接返回null
        /// </summary>
        public bool IncludeArrayType = true;

        /// <summary>
        /// 在拷贝或克隆时，是否处理 ClassType。False时直接返回null
        /// </summary>
        public bool IncludeClassType = true;

        /// <summary>
        /// 存放所有已经被克隆过的对象。如果今后再碰到该对象，则直接返回第一次克隆的结果。以免造成克隆逻辑错误，或在被克隆对象自己指向自己时死循环。
        /// 格式：Dictionary[oldObj,newObj]
        /// </summary>
        protected Dictionary<object, object> m_DirectlyUseInstances = new Dictionary<object, object>();//ClonedClassObjList

        /// <summary>
        /// 所有可以直接使用并替换的对象。如果今后再碰到该对象，则直接返回其对应的被替换值。
        /// 通常用于控制在克隆过程中进行直接替换的对象，或者用于避免造成克隆逻辑错误，或在被克隆对象自己指向自己时死循环。
        /// </summary>
        public Dictionary<object, object> DirectlyUseInstances
        {
            get { return this.m_DirectlyUseInstances; }
        }

        /// <summary>
        /// 增加在克隆时需要被替换的对象列表。
        /// 当克隆过程中遇到_oldObj对象时，将不再克隆_oldObj，
        /// 而是直接使用_replaceWithObj替换在本该克隆_oldObj的位置上。
        /// 注：
        /// 1。为保证克隆过程的封闭性和安全性，这里仅提供增加ReplaceObj的方法。永远不提供检索和删除。
        /// 2。_oldObj将作为Key来使用。因此不能重复添加_oldObj，否则将会抛出异常。
        /// 3。这个函数只有在开始执行克隆前调用才有效。
        /// </summary>
        /// <param name="_oldObj">需要被替换的目标对象</param>
        /// <param name="_replaceWithObj">需要被替换的新对象</param>
        public void AddDirectlyUseInstance(object _oldObj, object _replaceWithObj)
        {
            if (   _replaceWithObj.GetType() != _oldObj.GetType()
                 && !_replaceWithObj.GetType().IsSubclassOf(_oldObj.GetType()))
                throw new Exception("新对象与被替换对象类型不一致，或不是其子类。");

            if (!this.m_DirectlyUseInstances.ContainsKey(_oldObj))
                this.m_DirectlyUseInstances.Add(_oldObj, _replaceWithObj);
        }//AddDirectlyUseInstance(2)

        /// <summary>
        /// 预先添加一些实例。当发现是在处理这些Instance的时候，直接返回该Instance，不做任何处理
        /// 只能增加，不能删除或减少。如果重复添加的，则忽略后来添加的部分。保证该实例的唯一性。 
        /// </summary>
        public void AddDirectlyUseInstance(IEnumerable<object> _insList)
        {
            foreach (object crrObj in _insList)
            {
                if (!this.m_DirectlyUseInstances.ContainsKey(crrObj))
                    this.m_DirectlyUseInstances.Add(crrObj, crrObj);
            }
        }//AddDirectlyUseInstance(IEnumerable<object>)

        /// <summary>
        /// 预先添加一些实例。当发现是在处理这些Instance的时候，直接返回该Instance，不做任何处理
        /// 只能增加，不能删除或减少。如果重复添加的，则忽略后来添加的部分。保证该实例的唯一性。 
        /// </summary>
        public void AddDirectlyUseInstance(object _obj)
        {
            if (!this.m_DirectlyUseInstances.ContainsKey(_obj))
                this.m_DirectlyUseInstances.Add(_obj, _obj);
        }//AddDirectlyUseInstance(1)

        public CCloner()
        {

        }

        /// <summary>
        /// 将obj完整克隆，并返回克隆后的新对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public  T DeepCopy<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Object cannot be null");
            return (T)DoDeepCopy(obj);
        }

        protected bool IsEventHandler(Type objType)
        {
            if (objType == typeof(EventHandler)
                  || objType.FullName.StartsWith(Type.GetType("System.EventHandler`1").FullName))
                
                return true;
            else
                return false;
        }

        protected object DoDeepCopy(object obj)
        {
            if (obj == null)
                return null;
            //typeof (EventHandler<TEventArgs>);

            Type objType = obj.GetType(); 
            
            ////如果已经声明不处理EventHandler，则直接返回null
            //if (IsEventHandler(objType) && !IncludeEventHandler) 
            //    return null;

            if (IsNonClone(obj))
            {
                if (objType.IsValueType)
                    throw new Exception("IncludeValueType=false时，不能直接拷贝值类型。（如果是类中的值属性可以自动识别并跳过。）");//return null;

                return null;
            }

            if (m_DirectlyUseInstances.ContainsKey(obj))
                return m_DirectlyUseInstances[obj];


            ////如果是值类型或字符串
            ////注：System.String类型似乎比较特殊，复制它的所有字段，并不能复制它本身 
            ////不过由于System.String的不可变性，即使指向同一对象，也无所谓 
            ////而且.NET里本来就用字符串池来维持
            if (objType.IsValueType || objType == typeof(string))
            {
                    return obj;
            }
            else if (objType.IsArray)
            {
                    return DoDeepClone_Array(obj);
            }
            else if (objType.IsClass)
            {
                    return DoDeepClone_Class(obj);
            }
            else
                throw new ArgumentException("Unknown objType");
        }

        /// <summary>
        /// 复制class类型的对象。深度克隆obj，生成并返回新的克隆结果。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private object DoDeepClone_Class(object obj)
        {
            ////构造一个新的同类型对象。
            object newObj = CreateNewInstance(obj);//Activator.CreateInstance(obj.GetType());

            if (!m_DirectlyUseInstances.ContainsKey(obj))
                m_DirectlyUseInstances.Add(obj, newObj);

            DeepClone_Class(obj,newObj);

            return newObj;
        }

        /// <summary>
        /// 本方法直接时候newObj，而不再跟据obj反射生成newObj
        /// 在已经构造好的newObj对象上， 将obj中的内容深度克隆到newObj上。
        /// 
        /// </summary>
        /// <param name="obj">not null. deep copy from..</param>
        /// <param name="newObj">not null. deep copy to..</param>
        /// <returns></returns>
        public void DeepClone_Class<TClass>(TClass obj, TClass newObj) where TClass : class //(object obj, object newObj)
        {
            if (obj==null || newObj == null)
                throw new Exception("参数中的一个是null。本函数的参数均不能为null.");//return; //return newObj;

            if (IsNonClone(obj))
                    return;

            Type objType = obj.GetType();
            //Type newType = newObj.GetType();

            //if (!type.IsClass)
            //    throw new Exception("参数obj不是class类型。");

            ////如果被克隆对象是的泛型参数中包含了被禁止克隆的类型，则停止继续克隆。
            foreach (Type crrType in objType.GetGenericArguments())
            {
                if (IsNonClone(crrType))
                    return;

            }


            FieldInfo[] fields = objType.GetFields(BindingFlags.Public |
                        BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                ////如果该属性是值类型的，且当前配置为不拷贝值类型，则直接跳过。
                //if (field.FieldType.IsValueType && !this.IncludeValueType)
                //    continue;

                //////如果当前数组是class类型的数组，且this.IncludeClassType=false。则什么也不做。
                //if (field.FieldType.IsClass && !this.IncludeClassType)
                //    continue;

                //////如果当前数组是Array类型的数组，且this.IncludeArrayType=false。则什么也不做。
                //if (field.FieldType.IsArray && !this.IncludeArrayType)
                //    continue;

                //判断在当对象它自己的类定义时是否符合NonClone
                if (IsNonClone(field.FieldType))
                    continue;

                //判断当前对象作为当前类的field时是否是NonClone
                if (IsNonClone(field))
                    continue;


                object fieldValue = field.GetValue(obj);
                if (fieldValue == null)
                    continue;

                field.SetValue(newObj, DoDeepCopy(fieldValue));
            }

            CopyFatherClassAttributes(newObj, obj);

            //return newObj;
        }

        

        /// <summary>
        /// 构造一个新的同类型对象。并返回
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private object CreateNewInstance(object obj)
        {
            object newObj = null;

            try
            {
                //尝试调用默认构造函数 
                newObj = Activator.CreateInstance(obj.GetType());
            }
            catch
            {
                //失败的话，只好枚举构造函数了 
                foreach (ConstructorInfo ci in obj.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    try
                    {
                        ParameterInfo[] pis = ci.GetParameters();
                        object[] objs = new object[pis.Length];
                        for (int i = 0; i < pis.Length; i++)
                        {
                            if (pis[i].ParameterType.IsValueType)
                                objs[i] = Activator.CreateInstance(pis[i].ParameterType);
                            else
                                //参数类型可能是抽象类或接口，难以实例化 
                                //我能想到的就是枚举应用程序域里的程序集，找到实现了该抽象类或接口的类 
                                //但显然过于复杂了 
                                objs[i] = null;
                        }
                        newObj = ci.Invoke(objs);
                        //无论调用哪个构造函数，只要成功就行了 
                        break;
                    }
                    catch
                    {
                    }
                }
            }

            ////如果克隆构造没有成功，则直接抛出异常 // 返回null;
            if (newObj == null)
                throw new Exception("克隆时反射对象构造失败。Obj.GetType.FullName: "+obj.GetType().FullName );//return null;

            return newObj;
        }//CreateNewInstance

        /// <summary>
        /// 克隆继承树上的私有实例字段
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="obj"></param>
 
        private void CopyFatherClassAttributes(object newObj, object obj)
        {
            //for 逐级遍历上层父类
            for (Type father = newObj.GetType().BaseType; father != typeof(object); father = father.BaseType)
            {
                foreach (FieldInfo fi in father.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    //只需要处理私有字段，因为非私有成员已经在子类处理过了 
                    if (fi.IsPrivate)
                    {
                        if (fi.FieldType.IsValueType || fi.FieldType == typeof(string))
                        {
                            fi.SetValue(newObj, fi.GetValue(obj));
                        }
                        else
                        {
                            fi.SetValue(newObj, DoDeepCopy(fi.GetValue(obj)));
                        }
                    }
                }
            }//for 逐级遍历上层父类
        }//CopyFatherClassAttributes()

        /// <summary>
        /// 用于复制Array类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objType"></param>
        /// <returns></returns>//(object obj, Type objType)
        private object DoDeepClone_Array(object obj) //where TClass : System.Array //(object obj, object newObj)
        {
            Type objType = obj.GetType();

            Type elementType = objType.GetElementType();//Type.GetType(elementTypeName);

            var objArray = obj as Array;
            Array newObj = Array.CreateInstance(elementType, objArray.Length);

            DeepClone_Array(objArray, newObj);

            this.AddDirectlyUseInstance(objArray, newObj);

            return newObj;
        }//DoDeepCopy_Array

        /// <summary>
        /// 将obj中的全部内容深层拷贝到newObj中。不会跟据obj的类型反射生成newObj
        /// </summary>
        /// <param name="obj">not null. deep copy from..</param>
        /// <param name="newObj">not null. deef copy to..</param>
        public void DeepClone_Array(Array obj, Array newObj)
        {
            if (obj == null || newObj == null)
                throw new Exception("参数中的一个是null。本函数的参数均不能为null.");//return; //return newObj;

            Type objType = obj.GetType();

            Type elementType = objType.GetElementType();

            if (IsNonClone(obj) || IsNonClone(obj.GetType().GetElementType()))
                return;

            if (objType.GetElementType().IsArray && !this.IncludeArrayType)
                return;


            for (int i = 0; i < obj.Length; i++)
            {
                newObj.SetValue(DoDeepCopy(obj.GetValue(i)), i);
            }
            
            Convert.ChangeType(newObj, obj.GetType());
        }//DeepClone_Array

        /// <summary>
        /// 当前对象是否是需要被忽略的克隆。通常是被声明了[Nonclone] ，或 this.IncludeXXX=false所对应的对象。
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsNonClone(object obj)
        {
            Type objType = obj.GetType();

            return IsNonClone(objType);
        }//IsNonclone()

        public bool IsNonClone(FieldInfo field)
        {
            Attribute atb = System.Attribute.GetCustomAttribute(
                field, typeof(NonCloner), false);

            return IsNonClone(atb);
        }

        public bool IsNonClone(Attribute atb)
        {
            if (atb != null)
            {
                NonCloner nc = atb as NonCloner;

                int nRst = nc.NonCloneFlags & this.NonCloneFlags;
                if (nRst != 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 当前对象是否是需要被忽略的克隆。通常是被声明了[Nonclone] ，或 this.IncludeXXX=false所对应的对象。
        /// 注：由于没有直接获得obj，因此可能这个函数的判断逻辑会少于IsNonclone(object obj)。所以在有obj的地方。
        /// 最好直接调用IsNonclone(object obj),而不是本函数。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsNonClone(Type objType)
        {
            ////如果obj被标记为[NonCloner]且与this.NonCloneFlag所制定的条件相符，则不进行克隆处理
            Attribute atb = System.Attribute.GetCustomAttribute( objType, typeof(NonCloner),false );
            if (IsNonClone(atb))
                    return true;

            ////如果当前数组是值类型的数组，且this.IncludeValueType=false。则不进行克隆处理
            if (objType.IsValueType && !this.IncludeValueType)
                return true;

            ////如果当前数组是class类型的数组，且this.IncludeClassType=false。则不进行克隆处理
            if (objType.IsClass && !this.IncludeClassType)
                return true;

            ////如果当前数组是Array类型的数组，且this.IncludeArrayType=false。则不进行克隆处理
            if (objType.IsArray && !this.IncludeArrayType)
                return true;

            ////如果已经声明不处理EventHandler，则不进行克隆处理
            if (IsEventHandler(objType) && !IncludeEventHandler)
                return true;

            return false;

        }//IsNonclone(Type objType)


    }//class CCloner

    /// <summary>
    /// 不可被克隆的标记。当CCloner遇到有[NonCloner]这个标记的特定属性时，且( NonCloner.NonCloneFlags & CCloner.NonCloneFlags)!=0，CCloner就会忽略对该目标的克隆。 
    /// 开发人员可以利用这个特性来控制在不同的环境中，在runtime时使某个对象为可克隆或不可克隆。
    /// </summary>
    public sealed class NonCloner : System.Attribute
    {
        //[Serializable]
        //[ComVisible(true)]
        //[Flags]
        //public enum ENonCloneFlags
        //{
        //    All = int.MinValue,
        //    None = 0
        //}

        /// <summary>
        /// 当( NonCloner.NonCloneFlags & CCloner.NonCloneFlags)!=0的时候，CCloner就会忽略对该目标的克隆。
        /// 开发人员可以利用这个特性来控制在不同的环境中，在runtime时使某个对象为可克隆或不可克隆。
        /// </summary>
        /// <value>默认值=int.MinValue。即不论该值等于多少。当前对象都会被忽略拷贝。</value>
        public int NonCloneFlags = int.MinValue;//ENonCloneFlags.All;

        public NonCloner()
        {

        }

        public NonCloner(int _notCloneFlags)
        {
            this.NonCloneFlags = _notCloneFlags;

            //System.FlagsAttribute a; AttributeTargets
        }


    }//class NonCloner
}//namespace
