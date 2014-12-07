using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Web.Utility
{
    public class RegExp
    {
        public static bool IsEmail(string s)
        {
            string pattern = "^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$";
            return Regex.IsMatch(s, pattern);
        }
        public static bool IsIp(string s)
        {
            string pattern = "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$";
            return Regex.IsMatch(s, pattern);
        }
        public static bool IsNumeric(string s)
        {
            string pattern = "^\\-?[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }
        public static bool IsPhysicalPath(string s)
        {
            string pattern = "^\\s*[a-zA-Z]:.*$";
            return Regex.IsMatch(s, pattern);
        }
        public static bool IsRelativePath(string s)
        {
            return s != null && !(s == "") && !s.StartsWith("/") && !s.StartsWith("?") && !Regex.IsMatch(s, "^\\s*[a-zA-Z]{1,10}:.*$");
        }
        public static bool IsSafety(string s)
        {
            string input = s.Replace("%20", " ");
            input = Regex.Replace(input, "\\s", " ");
            string pattern = "select |insert |delete from |count\\(|drop table|update |truncate |asc\\(|mid\\(|char\\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|\"|\\'| or ";
            return !Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        public static bool IsUnicode(string s)
        {
            string pattern = "^[\\u4E00-\\u9FA5\\uE815-\\uFA29]+$";
            return Regex.IsMatch(s, pattern);
        }
        public static bool IsUrl(string s)
        {
            string pattern = "^(http|https|ftp|rtsp|mms):(\\/\\/|\\\\\\\\)[A-Za-z0-9%\\-_@]+\\.[A-Za-z0-9%\\-_@]+[A-Za-z0-9\\.\\/=\\?%\\-&_~`@:\\+!;]*$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }
        public static bool IsIdentityCard(string s)
        {
            return Regex.IsMatch(s, "^(^\\d{15}$|^\\d{18}$|^\\d{17}(\\d|X|x))$", RegexOptions.IgnoreCase);
        }
        public static bool IsMobileNo(string s, bool isRestrict = false)
        {
            bool result;
            if (!isRestrict)
            {
                result = Regex.IsMatch(s, "^[1]\\d{10}$", RegexOptions.IgnoreCase);
            }
            else
            {
                result = Regex.IsMatch(s, "^[1][3-8]\\d{9}$", RegexOptions.IgnoreCase);
            }
            return result;
        }
    }
}
