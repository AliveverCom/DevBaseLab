///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-11-18</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2013-8-1</ChangeDate>
///     <ChangeLog>增加了 PreCompile()方法的定义。以便符合多年来养成的外部习惯。以前的initComiler() 今后仅作为Base初始化自己的参数使用。</ChangeLog>
///  </ChangeHistory>
///</FileHistory>

using System;
using System.Collections.Generic;
using System.Text;

using Alivever.Com.DevBasic.BasicLib.LogCtrl;


namespace Alivever.Com.Compiler
{
    /// <summary>
    /// 所有编译器实例的基类，它的定义描述来自于CCompilerTpl
    /// </summary>
    public class CCompilerBase
    {
        /// <summary>
        /// 唯一的ID字符串标示
        /// </summary>
        //string IdStr = string.Empty;

        /// <summary>
        /// 编译器实例的ID，用于今后多线程实例的管理
        /// </summary>
        public string InsIdStr = string.Empty;

        /// <summary>
        /// 本编译器实例对应的定义说明对象
        /// </summary>
        public CCompilerTpl CompilerTpl = null;

        /// <summary>
        /// 用于表示本次编译的结果是否有效。不论此过程中是否发生过错误。
        /// 如果这个变量为False的话，则表示本次编译曾经被意外终止了，
        /// 所有编译中产生的中间结果均无效，应避免获取和使用。
        /// </summary>
        public bool ResultAvailable = true;

        /// <summary>
        /// 如果本编译器是一组大型编译中的一员时,
        /// true用于通知上层编译器可以继续进行后续编译.
        /// false通知上层编译器应该立即中止本次整体编译.
        /// </summary>
        public bool GoOnCompile = true;

        /// <summary>
        /// 编译器的名称
        /// </summary>
        ///string KeyObj = string.Empty;

        /// <summary>
        /// 编译器的介绍性文字
        /// </summary>
        ///string DescStr = string.Empty;

        /// <summary>
        /// 每次编译后所产生的各种编译信息
        /// </summary>
        public CCompileInfoInsList Infos = new CCompileInfoInsList();//List<CCompileInfoIns> Infos = new List<CCompileInfoIns>();

        /// <summary>
        /// Dictionary[int nStep, string KeyObj],本编译器，每次编译共需要多少个步骤，以及每个步骤地名字。 这样可以配合界面的刷新显示工作。
        /// </summary>
        public  Dictionary<int, string> CompileSteps = new Dictionary<int, string>();

        /// <summary>
        /// 当前正在执行的步骤序号。
        /// </summary>
        public int CrrStepNumber = 0;

        /// <summary>
        /// 这个方法需要在每一个子类中进行重载。用于初始化
        /// 当前算法，清空Infos，调用InitCompileSteps()
        /// </summary>
        virtual protected  bool InitCompiler()
        {
       
            //CrrStepNumber = 0;
            Infos.Clear();
            InitCompileSteps();

            return true;
        }//InitCompileSteps()


        /// <summary>
        /// 预编译函数。在执行真正的编译以前，做一切具体算法的前置准备工作。
        /// 这个method在继承类中重载后使用。
        /// </summary>
        virtual protected  bool PreCompile()
        {
            return true;
        }//InitCompileSteps()


        /// <summary>
        /// 运行编译器,这个方法是对外部可见的执行函数，提供通用的编译器总体控制算法。
        /// 1.调用InitCompiler()，如果失败，记Log，并调用CancelCompile()，ResultAvailable = false,返回False
        /// 2。调用DoCompile(),，如果失败，记Log，并调用CancelCompile()，ResultAvailable = false,返回False
        /// 3.调用PostCompile()，如果失败，记Log，并调用CancelCompile()，ResultAvailable = false,返回False
        /// 4. 如果所有都正常结束，返回True
        /// 注：对于子类来说，应该避免重载这个类，而应该将真正的编译算法写在DoCompile()方法中。
        /// </summary>
        /// <returns>False表示编译过程中出现严重错误而被异常中止。</returns>
        virtual public bool Run()//RunCompiler()
        {
            string errStr = string.Empty;

            ////初始化编译器参数
            if (!this.InitCompiler())
            {
                errStr = "初始化编译器失败。本次编译终止。";
                GSdkMLog.At(this.GetType().Namespace).Write(this.GetType().Name + ".RunCompiler", 2, errStr);
                this.Infos.Add(InfoMaker.NormalError(errStr));
                ResultAvailable = false;
                this.CancelCompile();
                return false;
            }

            ////初始化编译器参数
            if (!this.PreCompile())
            {
                errStr = "预编译失败。本次编译终止。";
                GSdkMLog.At(this.GetType().Namespace).Write(this.GetType().Name + ".PreCompiler", 2, errStr);
                this.Infos.Add(InfoMaker.NormalError(errStr));
                ResultAvailable = false;
                this.CancelCompile();
                return false;
            }

            ////执行编译
            this.CrrStepNumber++;
            if (!this.DoCompile())
            {
                errStr = "执行主解析算法时失败。本次编译终止。";
                GSdkMLog.At(this.GetType().Namespace).Write(this.GetType().Name+".RunCompiler", 2, errStr);
                this.Infos.Add(InfoMaker.NormalError( errStr ));
                //ResultAvailable = false;
                this.CancelCompile();
                return false;
            }

            ////进行编译后处理
            this.CrrStepNumber++;
            if (!this.PostCompile())
            {
                errStr = "执行编译后处理时失败。本次编译终止。";
                GSdkMLog.At(this.GetType().Namespace).Write(this.GetType().Name + ".RunCompiler", 2, errStr);
                this.Infos.Add(InfoMaker.NormalError( errStr ));
                //ResultAvailable = false;
                this.CancelCompile();
                return false;
            }

            //this.ResultAvailable = true;
            
            return true;
        }

        /// <summary>
        /// 在主编译算法完成后进行的后续处理。通常用于交验编译结果是否正确，并关闭所有在编译过程中打开的资源。
        /// 当前算法：调用ReleaseResources()，返回true
        /// </summary>
        /// <returns></returns>
        protected virtual bool PostCompile()
        {
            ReleaseResources();
            return true;
        }

        /// <summary>
        /// 真正的解析算法函数。所有的子类都应该重载这个函数。
        /// </summary>
        protected virtual bool DoCompile()
        {
            return true;
        }//DoCompile()

        /// <summary>
        /// 在编译过程中出错的时候提供紧急收尾算法。用于关闭所有在编译中所打开的文件或打开的句柄。
        /// 相当于作收拾残局的工作。默认调用ReleaseResources()，并将 ResultAvailable被设置为null。 
        /// 如果需要设置GoOnCompile=false，则由子类自己在需要的时候赋值。
        /// </summary>
        protected virtual void CancelCompile()
        {
            this.ReleaseResources();
            //this.GoOnCompile = false;
            this.ResultAvailable = false;

            string errStr = "正在取消本次编译。标记本次编译结果无效。";
            this.Infos.Add(InfoMaker.NormalError(errStr));

        }//CancelCompile()

        /// <summary>
        /// 设置编译器步骤
        /// 1.清空this.CompileSteps
        /// 2.重置this.CrrStepNumber = 0
        /// </summary>
        protected virtual void InitCompileSteps()
        {
            this.CompileSteps.Clear();
            this.CrrStepNumber = 0;
        }//InitCompileSteps()

        /// <summary>
        /// 不论当前编译是否进行完毕,都释放掉所有在编译过程中打开但还未释放掉的资源.
        /// nothing have done in this define.
        /// 注:不包括编译的结果集,结果集应该在整个编译器对象被销毁时进行销毁.
        /// 
        /// </summary>
        protected virtual void ReleaseResources()
        {

        }
    }//class CCompilerBase
}//namespace
