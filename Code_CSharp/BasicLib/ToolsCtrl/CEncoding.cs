using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

///<FileHistory>
///  <Creater> Charles shao</creater>
///  <CreaterDate> 2009-11-17</CreaterDate>
///  <ChangeHistory>
///     <Engineer>someone</Engineer>
///     <ChangeDate>2009-00-00</ChangeDate>
///     <ChangeLog>something</ChangeLog>
///  </ChangeHistory>
///</FileHistory>
namespace Alivever.Com.DevBasic.BasicLib.ToolsCtrl
{
    public class CEncoding
    {
        #region encoding methods
        //UNICODE字符转为中文   
        //对这个方法做一点改进 使他支持中英混排   
        public static string UnicodeToText(string unicodeString)
        {
            if (string.IsNullOrEmpty(unicodeString))
                return string.Empty;

            string outStr = unicodeString;

            Regex re = new Regex("\\\\u[0123456789abcdef]{4}", RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(unicodeString);
            foreach (Match ma in mc)
            {
                outStr = outStr.Replace(ma.Value, UnicodeStringToChar(ma.Value).ToString());
            }
            return outStr;
        }

        private static char UnicodeStringToChar(string str)
        {
            char outStr = Char.MinValue;
            outStr = (char)int.Parse(str.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
            return outStr;
        }

        public static string TextToUnicodeString(string _str)
        {
            //byte[] res = Encoding.UTF8.GetBytes(_str);

            string rst = string.Empty;
            for (int i = 0; i < _str.Length; ++i)
            {
                int crrChar = (int)_str[i];

                ////首先跳过不需要编码的字符
                if ((crrChar >= 0x0020 && crrChar <= 0x0FFF)
                    || (crrChar >= 0x4e00 && crrChar <= 0x9FFF))
                    rst += (char)crrChar;
                else
                    rst += "\\u" + (crrChar).ToString("x2");
            }

            //return new String(temp);
            return rst;
        }

        public static string TextToSqlStr(string _str)
        {
            return TextToUnicodeString(_str).Replace("\\", "\\\\").Replace("'", "\\'");
        }

        #endregion    encoding methods


    }//class CEncoding
}//namespace
