///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-11-18</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2013-8-1</ChangeDate>
///     <ChangeLog>������ PreCompile()�����Ķ��塣�Ա���϶��������ɵ��ⲿϰ�ߡ���ǰ��initComiler() ������ΪBase��ʼ���Լ��Ĳ���ʹ�á�</ChangeLog>
///  </ChangeHistory>
///</FileHistory>

using System;
using System.Collections.Generic;
using System.Text;

using Alivever.Com.DevBasic.BasicLib.LogCtrl;


namespace Alivever.Com.Compiler
{
    /// <summary>
    /// ���б�����ʵ���Ļ��࣬���Ķ�������������CCompilerTpl
    /// </summary>
    public class CCompilerBase
    {
        /// <summary>
        /// Ψһ��ID�ַ�����ʾ
        /// </summary>
        //string IdStr = string.Empty;

        /// <summary>
        /// ������ʵ����ID�����ڽ����߳�ʵ���Ĺ���
        /// </summary>
        public string InsIdStr = string.Empty;

        /// <summary>
        /// ��������ʵ����Ӧ�Ķ���˵������
        /// </summary>
        public CCompilerTpl CompilerTpl = null;

        /// <summary>
        /// ���ڱ�ʾ���α���Ľ���Ƿ���Ч�����۴˹������Ƿ���������
        /// ����������ΪFalse�Ļ������ʾ���α���������������ֹ�ˣ�
        /// ���б����в������м�������Ч��Ӧ�����ȡ��ʹ�á�
        /// </summary>
        public bool ResultAvailable = true;

        /// <summary>
        /// �������������һ����ͱ����е�һԱʱ,
        /// true����֪ͨ�ϲ���������Լ������к�������.
        /// false֪ͨ�ϲ������Ӧ��������ֹ�����������.
        /// </summary>
        public bool GoOnCompile = true;

        /// <summary>
        /// ������������
        /// </summary>
        ///string KeyObj = string.Empty;

        /// <summary>
        /// �������Ľ���������
        /// </summary>
        ///string DescStr = string.Empty;

        /// <summary>
        /// ÿ�α�����������ĸ��ֱ�����Ϣ
        /// </summary>
        public CCompileInfoInsList Infos = new CCompileInfoInsList();//List<CCompileInfoIns> Infos = new List<CCompileInfoIns>();

        /// <summary>
        /// Dictionary[int nStep, string KeyObj],����������ÿ�α��빲��Ҫ���ٸ����裬�Լ�ÿ����������֡� ����������Ͻ����ˢ����ʾ������
        /// </summary>
        public  Dictionary<int, string> CompileSteps = new Dictionary<int, string>();

        /// <summary>
        /// ��ǰ����ִ�еĲ�����š�
        /// </summary>
        public int CrrStepNumber = 0;

        /// <summary>
        /// ���������Ҫ��ÿһ�������н������ء����ڳ�ʼ��
        /// ��ǰ�㷨�����Infos������InitCompileSteps()
        /// </summary>
        virtual protected  bool InitCompiler()
        {
       
            //CrrStepNumber = 0;
            Infos.Clear();
            InitCompileSteps();

            return true;
        }//InitCompileSteps()


        /// <summary>
        /// Ԥ���뺯������ִ�������ı�����ǰ����һ�о����㷨��ǰ��׼��������
        /// ���method�ڼ̳��������غ�ʹ�á�
        /// </summary>
        virtual protected  bool PreCompile()
        {
            return true;
        }//InitCompileSteps()


        /// <summary>
        /// ���б�����,��������Ƕ��ⲿ�ɼ���ִ�к������ṩͨ�õı�������������㷨��
        /// 1.����InitCompiler()�����ʧ�ܣ���Log��������CancelCompile()��ResultAvailable = false,����False
        /// 2������DoCompile(),�����ʧ�ܣ���Log��������CancelCompile()��ResultAvailable = false,����False
        /// 3.����PostCompile()�����ʧ�ܣ���Log��������CancelCompile()��ResultAvailable = false,����False
        /// 4. ������ж���������������True
        /// ע������������˵��Ӧ�ñ�����������࣬��Ӧ�ý������ı����㷨д��DoCompile()�����С�
        /// </summary>
        /// <returns>False��ʾ��������г������ش�������쳣��ֹ��</returns>
        virtual public bool Run()//RunCompiler()
        {
            string errStr = string.Empty;

            ////��ʼ������������
            if (!this.InitCompiler())
            {
                errStr = "��ʼ��������ʧ�ܡ����α�����ֹ��";
                GSdkMLog.At(this.GetType().Namespace).Write(this.GetType().Name + ".RunCompiler", 2, errStr);
                this.Infos.Add(InfoMaker.NormalError(errStr));
                ResultAvailable = false;
                this.CancelCompile();
                return false;
            }

            ////��ʼ������������
            if (!this.PreCompile())
            {
                errStr = "Ԥ����ʧ�ܡ����α�����ֹ��";
                GSdkMLog.At(this.GetType().Namespace).Write(this.GetType().Name + ".PreCompiler", 2, errStr);
                this.Infos.Add(InfoMaker.NormalError(errStr));
                ResultAvailable = false;
                this.CancelCompile();
                return false;
            }

            ////ִ�б���
            this.CrrStepNumber++;
            if (!this.DoCompile())
            {
                errStr = "ִ���������㷨ʱʧ�ܡ����α�����ֹ��";
                GSdkMLog.At(this.GetType().Namespace).Write(this.GetType().Name+".RunCompiler", 2, errStr);
                this.Infos.Add(InfoMaker.NormalError( errStr ));
                //ResultAvailable = false;
                this.CancelCompile();
                return false;
            }

            ////���б������
            this.CrrStepNumber++;
            if (!this.PostCompile())
            {
                errStr = "ִ�б������ʱʧ�ܡ����α�����ֹ��";
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
        /// ���������㷨��ɺ���еĺ�������ͨ�����ڽ���������Ƿ���ȷ�����ر������ڱ�������д򿪵���Դ��
        /// ��ǰ�㷨������ReleaseResources()������true
        /// </summary>
        /// <returns></returns>
        protected virtual bool PostCompile()
        {
            ReleaseResources();
            return true;
        }

        /// <summary>
        /// �����Ľ����㷨���������е����඼Ӧ���������������
        /// </summary>
        protected virtual bool DoCompile()
        {
            return true;
        }//DoCompile()

        /// <summary>
        /// �ڱ�������г����ʱ���ṩ������β�㷨�����ڹر������ڱ��������򿪵��ļ���򿪵ľ����
        /// �൱������ʰ�оֵĹ�����Ĭ�ϵ���ReleaseResources()������ ResultAvailable������Ϊnull�� 
        /// �����Ҫ����GoOnCompile=false�����������Լ�����Ҫ��ʱ��ֵ��
        /// </summary>
        protected virtual void CancelCompile()
        {
            this.ReleaseResources();
            //this.GoOnCompile = false;
            this.ResultAvailable = false;

            string errStr = "����ȡ�����α��롣��Ǳ��α�������Ч��";
            this.Infos.Add(InfoMaker.NormalError(errStr));

        }//CancelCompile()

        /// <summary>
        /// ���ñ���������
        /// 1.���this.CompileSteps
        /// 2.����this.CrrStepNumber = 0
        /// </summary>
        protected virtual void InitCompileSteps()
        {
            this.CompileSteps.Clear();
            this.CrrStepNumber = 0;
        }//InitCompileSteps()

        /// <summary>
        /// ���۵�ǰ�����Ƿ�������,���ͷŵ������ڱ�������д򿪵���δ�ͷŵ�����Դ.
        /// nothing have done in this define.
        /// ע:����������Ľ����,�����Ӧ����������������������ʱ��������.
        /// 
        /// </summary>
        protected virtual void ReleaseResources()
        {

        }
    }//class CCompilerBase
}//namespace
