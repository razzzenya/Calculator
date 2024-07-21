using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace Calculator.Services
{
    static class InputService
    {
        public static string AddSymToNum(string current, string input, bool isNeedToClear)
        {
            if (isNeedToClear) 
            {
                current = "";
            }

            if (current.Length == 16) 
            {
                return current; 
            }

            if (current == "0")
            {
                if (input == ",")
                {
                    return "0,";
                }
                else if (input != "0")
                {
                    return input;
                }
                return current;
            }

            else
            {
                if (input == ",")
                {
                    if (!IsCommaInString(current))
                    {
                        return current + input;
                    }
                    return current;
                }
                return current + input;
            }
        }

        public static string RemoveDigit(string current) 
        {
            if (current.Length > 1)
            {
                return current.Remove(current.Length - 1);
            }
            return "0";
        }

        private static bool IsCommaInString(string current)
        {
            foreach (char c in current) {
                if (c == ',')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
