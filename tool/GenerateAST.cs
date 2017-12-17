using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace tool
{
    class GenerateAST
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: generate_ast <output_directory>");
                Environment.Exit(1);
            }
            string outputDir = args[0];

            List<string> types = new List<string>
            {
                "Binary     : Expr left, Token op, Expr right",
                "Grouping   : Expr expression",
                "Literal    : Object value",
                "Unary      : Token op, Expr right"
            };

            DefineAST(outputDir, "Expr", types);
        }

        static void DefineAST(string outputDir, string baseName, List<string> types)
        {
            string path = outputDir + "/" + baseName + ".cs";

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("using System;");
                sw.WriteLine();
                sw.WriteLine("namespace lox");
                sw.WriteLine('{');
                sw.WriteLine(TabsToSpaces(1) + "abstract class " + baseName);
                sw.WriteLine(TabsToSpaces(1) + '{');

                foreach (string type in types)
                {
                    string className = type.Split(":")[0].Trim();
                    string fields = type.Split(":")[1].Trim();
                    DefineType(sw, baseName, className, fields);
                }

                sw.WriteLine(TabsToSpaces(1) + '}');
                sw.WriteLine('}');
            }
        }

        static void DefineType(StreamWriter sw, string baseName, string className, string fields)
        {
            sw.WriteLine();
            sw.WriteLine(TabsToSpaces(2) + "class " + className + " : " + baseName);
            sw.WriteLine(TabsToSpaces(2) + '{');
            sw.WriteLine(TabsToSpaces(3) + className + "(" + fields + ")");
            sw.WriteLine(TabsToSpaces(3) + '{');

            string[] fieldArr = fields.Split(", ");
            foreach (string field in fieldArr)
            {
                string name = field.Split(' ')[1];
                sw.WriteLine(TabsToSpaces(4) + "this." + name + " = " + name + ';');
            }

            sw.WriteLine(TabsToSpaces(3) + '}');

            sw.WriteLine();
            foreach (string field in fieldArr)
            {
                sw.WriteLine(TabsToSpaces(3) + "readonly " + field + ';');
            }

            sw.WriteLine(TabsToSpaces(2) + '}');
        }

        static string TabsToSpaces(int n)
        {
            return new string(' ', 4 * n);
        }
    }
}
