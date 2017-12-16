using System;
using System.Collections.Generic;
using System.Text;

namespace lox
{
    class Scanner
    {
        readonly string source;
        readonly List<Token> tokens = new List<Token>();

        int start = 0;
        int current = 0;
        int line = 1;

        public Scanner(string source)
        {
            this.source = source;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                start = current;
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        bool IsAtEnd()
        {
            return current >= source.Length;
        }
    }
}
