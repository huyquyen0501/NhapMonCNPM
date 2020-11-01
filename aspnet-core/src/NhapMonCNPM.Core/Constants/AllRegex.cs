using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NhapMonCNPM.Constants
{
    public static class AllRegex
    {
        public static Regex mailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public static Regex hasNumber = new Regex(@"[0-9]+");
        public static Regex hasUpperChar = new Regex(@"[A-Z]+");
        public static Regex hasMiniMaxChars = new Regex(@".{8,31}");
        public static Regex hasLowerChar = new Regex(@"[a-z]+");
        public static Regex hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        public static Regex onlyNumber = new Regex("^[0 - 9] *$");

    }
}
