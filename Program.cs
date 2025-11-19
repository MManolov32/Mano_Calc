using System;

namespace Mano_Calc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();

            Console.WriteLine("Simple calculator. Type 'exit' to quit.");
            Console.WriteLine("Use spaces between all numbers and operators.");
            Console.WriteLine("Example: 3 + 4 - 2 * 10");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                input = input.Trim();

                if (input.ToLower() == "exit")
                    break;
                // I added this becauce using a comma causes errors.
                if (input.Contains(","))
                {
                    Console.WriteLine("Use a dot (.) instead of a comma (,).");
                    continue;
                }

                // Example input: "3 + 4 - 2"
                string[] parts = input.Split(' ');

                if (parts.Length < 3 || parts.Length % 2 == 0)
                {
                    Console.WriteLine("Invalid format. Use: number operator number operator number ...");
                    continue;
                }

                double result;
                if (!double.TryParse(parts[0], out result))
                {
                    Console.WriteLine("Invalid number: " + parts[0]);
                    continue;
                }

                // Loop through the rest: operator, number, operator, number...
                for (int i = 1; i < parts.Length; i += 2)
                {
                    string op = parts[i];
                    string nextNumString = parts[i + 1];

                    double nextNum;
                    if (!double.TryParse(nextNumString, out nextNum))
                    {
                        Console.WriteLine("Invalid number: " + nextNumString);
                        break;
                    }

                    if (op == "+")
                        result = calc.Add(result, nextNum);
                    else if (op == "-")
                        result = calc.Subtract(result, nextNum);
                    else if (op == "*")
                        result = calc.Multiply(result, nextNum);
                    else if (op == "/")
                    {
                        if (nextNum == 0)
                        {
                            Console.WriteLine("Cannot divide by zero.");
                            break;
                        }
                        result = calc.Divide(result, nextNum);
                    }
                    else
                    {
                        Console.WriteLine("Unknown operator: " + op);
                        break;
                    }
                }

                Console.WriteLine("= " + result);
            }
        }
    }
}
