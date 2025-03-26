using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEE
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string text = Console.ReadLine();
                Dictionary<char, bool> variables = new Dictionary<char, bool>();
                foreach (char c in Boolean.IdentifyVariables(text))
                    variables.Add(c, true);

                Console.Clear();

                foreach (var item in variables.Keys)
                    Console.Write(item);

                Console.WriteLine($"\t {text} \n");

                for (int i=0; i < Math.Pow(2,variables.Count); i++)
                {
                    string bin = Convert.ToString(i, 2);
                    bin = new string('0', variables.Count - bin.Length) + bin;

                    for (int k = 0; k<bin.Length; k++)
                    {
                        variables[variables.ElementAt(k).Key] = bin[k] == '1';
                    }

                    bool result = Boolean.Solve(text, variables);

                    Console.Write(bin + "\t");

                    Console.ForegroundColor = result ? ConsoleColor.Cyan : ConsoleColor.Red;

                    Console.WriteLine(result);

                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }
    }
}
