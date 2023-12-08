using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace Alivever.Com.DevBasic.BasicLib.XmlCtrl
{
    /// <summary>
    /// 由于VS默认提供的XML序列化有非常多的限制，因此这里自定义一个可以进行XML序列化的接口
    /// </summary>
    interface IXmlSerializable
    {
        /// <summary>
        /// 得到当前类被XML序列化后的字符串
        /// </summary>
        /// <returns></returns>
        string XmlSerialize_Str();

        /// <summary>
        /// 根据给定的XML字符串进行后续解析
        /// </summary>
        /// <param name="_xmlStr"></param>
        void XmlSerialize_Parse(XmlElement _xmlObj);
        
    }
}
