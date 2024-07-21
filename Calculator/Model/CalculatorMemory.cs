using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using Calculator.Services;

namespace Calculator.Model
{
    internal class CalculatorMemory
    {
        private string current = "0", last, previousOp, previousNum;
        private bool isNeedToCleanInput = false, cleanFlag = false;

        public bool CleanFlag { set { cleanFlag = value; } get { return cleanFlag; } }
        public bool IsNeedToCleanInput { set { isNeedToCleanInput = value; } get { return isNeedToCleanInput; } }
        public string Current { set { current = value; } get { return current; } }

        public string Last { set { last = value; } get { return last; } }
        public CalculatorMemory() { }
        public void ChangeMemory(string op, bool is_any_input)
        {
            IsNeedToCleanInput = true;
            if (previousNum == null)
            {
                previousOp = op;
                previousNum = Current;
                Last += previousNum + " " + previousOp + " ";
            }
            else if (is_any_input)
            {
                Last += Current + " " + op + " ";
                previousNum = CountExpr(Last.Substring(0, Last.Length - 2));
                previousOp = op;
                Current = previousNum;
            }
            else if (!is_any_input)
            {
                Last = Last.Substring(0, Last.Length - 2) + op + " ";
                previousOp = op;
            }

            if (op == "=")
            {
                cleanFlag = true;
            }
        }

        private string CountExpr(string expr)
        {
            try
            {
                expr = expr.Replace(",", ".");
                DataTable table = new DataTable();
                table.Columns.Add("expression", typeof(string), expr);
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                double result = double.Parse((string)row["expression"]);
                return result.ToString();
            }
            catch (DivideByZeroException)
            {
                return "Деление на ноль невозможно!";
            }
        }

        public void Clean()
        {
            previousNum = null;
            previousOp = null;
            Last = "";
            Current = "0";
        }

        public void CleanEntry()
        {
            Current = "0";
        }

    }
}
