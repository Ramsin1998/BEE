using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEE
{
    public static class Boolean
    {
        private static Dictionary<char, bool> variables = new Dictionary<char, bool>();

        public static bool Solve(string expression, Dictionary<char, bool> variables)
        {
            Boolean.variables = variables;

            return evaluate(string.Join("", expression.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries)));
        }
        //don't make table.
        public static char[] IdentifyVariables(string expression) => String.Join("", expression.Split('(', ')', '+', '\'', ' ')).Distinct().ToArray();

        private static bool evaluate(string expression)
        {
            switch (determineInstruction(expression))
            {
                case Instruction.IsolateTerms:
                    return isolateTerms(expression).Any(e => evaluate(e));

                case Instruction.IsolateProducts:
                    return !isolateProducts(expression).Any(e => !evaluate(e));

                case Instruction.Unbox:
                    return evaluate(unbox(expression));
            }

            return evaluateVariable(expression);
        }

        private static Instruction determineInstruction(string expression)
        {
            int insideParentheses = 0;
            int subExpressions = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    insideParentheses++;

                else if (expression[i] == ')')
                {
                    insideParentheses--;

                    if (insideParentheses == 0)
                        subExpressions++;
                }

                if (expression[i] == '+' && insideParentheses == 0)
                    return Instruction.IsolateTerms;
            }

            if (expression[0] == '(' && subExpressions == 1 && expression.Last(c => c != '\'') == ')')
                return Instruction.Unbox;

            if (expression.Where(c => c != '\'').Count() == 1)
                return Instruction.EvaluateVariable;

            return Instruction.IsolateProducts;
        }

        private static string[] isolateTerms(string expression)
        {

        }

        private static string[] isolateProducts(string expression)
        {

        }

        private static string unbox(string expression)
        {

        }

        private static bool evaluateVariable(string variable)
        {

        }
    }
}