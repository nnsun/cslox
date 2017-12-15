using System;
using System.IO;

namespace lox
{
    class Lox
    {
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: cslox [script]");
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        static void RunFile(string path)
        {
            try
            {
                Run(File.ReadAllText(path));
            }
            catch
            {

            }
        }

        static void RunPrompt()
        {
            while (true)
            {
                Console.Write("> ");
                Run(Console.ReadLine());
            }
        }

        static void Run(string source)
        {
            //Scanner scanner = new Scanner(source);
            //List<Token> tokens = scanner.ScanTokens();

            //foreach (Token token in tokens)
            //{
            //    Console.WriteLine(token);
            //}
        }
    }
}
