using BjBygg.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BjBygg.Application.Application.Common
{
    public class StringTagger
    {
        private static readonly Regex _indexReg = new Regex(@" \(\d+\)");

        static public string TagIfNotUnique(IEnumerable<string> strings, string input, bool normalized = true)
        {
            int existingStrings;
            if (normalized)
                existingStrings = strings.Where(x => _indexReg.Replace(x, "").ToUpper() == input.ToUpper()).Count();
            else
                existingStrings = strings.Where(x => _indexReg.Replace(x, "") == input).Count();

            if (existingStrings != 0) return input + " (" + ++existingStrings + ")";

            else return input;
        }
    }
}
