using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MultilayerFrameworkSample.Common
{
    /// <summary>
    /// 正对string类型的扩展方法
    /// </summary>
    public static class StringExtend
    {
        /// <summary>
        /// 单词变成单数形式
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns></returns>
        public static string ToSingular(this string word)
        {
            if (string.IsNullOrEmpty(word)) return word;

            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}y");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}");

            return word;
        }

        /// <summary>
        /// 单词变成复数形式
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns></returns>
        public static string ToPlural(this string word)
        {
            if (string.IsNullOrEmpty(word)) return word;

            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}ies");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}s");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}es");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}s");

            return word;
        }
    }
}
