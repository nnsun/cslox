using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace tool
{
    class GenerateAst
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

            DefineAst(outputDir, "Expr", types);
        }

        static void DefineAst(string outputDir, string baseName, List<string> types)
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

                DefineVisitor(sw, baseName, types);

                sw.WriteLine();
                sw.WriteLine(TabsToSpaces(2) + "internal abstract T Accept<T>(IVisitor<T> visitor);");

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

        static void DefineVisitor(StreamWriter sw, string baseName, List<string> types)
        {
            sw.WriteLine(TabsToSpaces(2) + "internal interface IVisitor<T>");
            sw.WriteLine(TabsToSpaces(2) + '{');

            foreach (string type in types)
            {
                string typeName = type.Split(":")[0].Trim();
                sw.WriteLine(TabsToSpaces(3) + "T Visit" + typeName + baseName + '(' + 
                    typeName + " " + baseName.ToLower() + ");");
            }

            sw.WriteLine(TabsToSpaces(2) + '}');
        }

        static void DefineType(StreamWriter sw, string baseName, string className, string fields)
        {
            sw.WriteLine();
            sw.WriteLine(TabsToSpaces(2) + "internal class " + className + " : " + baseName);
            sw.WriteLine(TabsToSpaces(2) + '{');
            sw.WriteLine(TabsToSpaces(3) + "public " + className + "(" + fields + ")");
            sw.WriteLine(TabsToSpaces(3) + '{');

            string[] fieldArr = fields.Split(", ");
            foreach (string field in fieldArr)
            {
                string name = field.Split(' ')[1];
                sw.WriteLine(TabsToSpaces(4) + "this." + name + " = " + name + ';');
            }

            sw.WriteLine(TabsToSpaces(3) + '}');

            sw.WriteLine();
            sw.WriteLine(TabsToSpaces(3) + "internal override T Accept<T>(IVisitor<T> visitor)");
            sw.WriteLine(TabsToSpaces(3) + "{");
            sw.WriteLine(TabsToSpaces(4) + "return visitor.Visit" + className + baseName + "(this);");
            sw.WriteLine(TabsToSpaces(3) + "}");

            sw.WriteLine();
            foreach (string field in fieldArr)
            {
                sw.WriteLine(TabsToSpaces(3) + "internal readonly " + field + ';');
            }

            sw.WriteLine(TabsToSpaces(2) + '}');
        }

        static string TabsToSpaces(int n)
        {
            return new string(' ', 4 * n);
        }
    }
}
