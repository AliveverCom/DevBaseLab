using System;
using System.Collections.Generic;
using System.Text;

namespace Alivever.Com.DevBasic.BasicLib.CfgCtrl
{
    /// <summary>
    /// 单独的配置项
    /// </summary>
    public class CCfgItem
    {
        /// <summary>
        /// 逗号分割的唯一标示字符串
        /// </summary>
        public string KeyIdStr=string.Empty;

        protected string ValueStr = string.Empty;

        public CCfgItem()
		{
		}

        public CCfgItem(string _KeyIdStr, string _ValueStr)
        {
            this.KeyIdStr = _KeyIdStr;
            this.ValueStr = _ValueStr;
        }
	
		public  int IntValue
		{
			get{ return 0; }
            set { ValueStr = value.ToString();  }
		}

		public  string StrValue
		{
			get{ return  ValueStr ; }
            set { ValueStr = value; }
        }

		public  float FloatValue
		{
			get{ return (float)0;}
            set { ValueStr = value.ToString(); }
        }

		public  bool BoolValue
		{
			get{ return false ;}
            set { ValueStr = value.ToString(); }
        }

    }//class CCfgItem
}//namespace
