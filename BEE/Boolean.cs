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

            return evaluate(expression.removeWhiteSpace());
        }

        public static char[] IdentifyVariables(string expression) => string.Join("", expression.Split('(', ')', '+', '\'', ' ')).Distinct().ToArray();

        private static bool evaluate(string expression)
        {
            switch (determineInstruction(expression))
            {
                case Instruction.IsolateTerms:
                    return isolateTerms(expression).Any(e => evaluate(e));

                case Instruction.IsolateProducts:
                    return !isolateProducts(expression).Any(e => !evaluate(e));

                case Instruction.Unbox:
                    int negations = expression.countLast();
                    return negations % 2 == 0 ? evaluate(unbox(expression, negations)) : !evaluate(unbox(expression, negations));
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

            if (subExpressions == 1 && expression[0] == '(' && expression.Last(c => c != '\'') == ')')
                return Instruction.Unbox;

            if (expression.Where(c => c != '\'').Count() == 1)
                return Instruction.EvaluateVariable;

            return Instruction.IsolateProducts;
        }

        private static List<string> isolateTerms(string expression)
        {
            List<string> terms = new List<string>();
            int startingIndex = 0;
            int insideParentheses = 0;
            expression += '+';

            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    insideParentheses++;

                else if (expression[i] == ')')
                    insideParentheses--;

                if (insideParentheses == 0 && expression[i] == '+')
                {
                    terms.Add(expression.Substring(startingIndex, i - startingIndex));
                    startingIndex = i + 1;
                }
            }

            //terms.Add(expression.Substring(startingIndex, expression.Length - startingIndex));

            return terms;
        }

        private static List<string> isolateProducts(string expression)
        {
            List<string> products = new List<string>();
            int startingIndex = expression.Length - 1;
            int insideParentheses = 0;

            for (int i = expression.Length - 1; i >= 0; i--)
            {
                if (expression[i] == ')')
                    insideParentheses++;

                else if (expression[i] == '(')
                    insideParentheses--;

                if (insideParentheses > 0 || expression[i] == '\'')
                    continue;

                products.Add(expression.Substring(i, (startingIndex - i) + 1));
                startingIndex = i - 1;
            }

            return products;
        }
        
        private static string unbox(string expression, int negations) => expression.Substring(1, expression.Length - (negations + 2));

        private static bool evaluateVariable(string expression) => expression.countLast('\'') % 2 == 0 ? variables[expression[0]] : !variables[expression[0]];
    }
}