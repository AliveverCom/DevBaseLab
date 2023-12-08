///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-11-17</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>
using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.Compiler
{
    /// <summary>
    /// 一种试图可以对一种导入模板和一种导出模板
    /// 每中模板都对应着一个模板文件，以及一个缩略图
    /// </summary>
    public class CCompilerTpl
    {
        /// <summary>
        /// 唯一ID标示
        /// </summary>
        public string IdStr = string.Empty;

        /// <summary>
        /// 本对象的名称。可以为空值
        /// </summary>
        public string NameStr = string.Empty;

        /// <summary>
        ///  对象的描述说明
        /// </summary>
        public string DescStr = string.Empty;

        /// <summary>
        /// 该模板的图像资源名称，或文件名
        /// </summary>
        public string ImageSrcStr = string.Empty;

        /// <summary>
        /// 模板文件的资源名称，或文件名
        /// </summary>
        public string TplFileSrcStr = string.Empty;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtendStr = string.Empty;

        /// <summary>
        /// 本模板是否是默认的模板
        /// </summary>
        public bool IsDefultTpl = false;

        /// <summary>
        /// 该编译器的功能类型
        /// </summary>
        public ETplFunType TplFunTypeE = ETplFunType.eUnknow;
    }//class CompilerTpl
}//namespace
