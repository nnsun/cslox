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

        static readonly Dictionary<string, TokenType> keywords;

        static Scanner()
        {
            keywords = new Dictionary<string, TokenType>();
            keywords["and"] = TokenType.AND;
            keywords["class"] = TokenType.CLASS;
            keywords["else"] = TokenType.ELSE;
            keywords["false"] = TokenType.FALSE;
            keywords["for"] = TokenType.FOR;
            keywords["fun"] = TokenType.FUN;
            keywords["if"] = TokenType.IF;
            keywords["nil"] = TokenType.NIL;
            keywords["or"] = TokenType.OR;
            keywords["print"] = TokenType.PRINT;
            keywords["return"] = TokenType.RETURN;
            keywords["super"] = TokenType.SUPER;
            keywords["this"] = TokenType.THIS;
            keywords["true"] = TokenType.TRUE;
            keywords["var"] = TokenType.VAR;
            keywords["while"] = TokenType.WHILE;
        }

        public Scanner(string source)
        {
            this.source = source;
        }

        public List<Token> ScanTokens()
        {
            while (!IsAtEnd())
            {
                start = current;
                ScanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        void ScanToken()
        {
            char c = Advance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LEFT_PAREN);
                    break;
                case ')':
                    AddToken(TokenType.RIGHT_PAREN);
                    break;
                case '{':
                    AddToken(TokenType.LEFT_BRACE);
                    break;
                case '}':
                    AddToken(TokenType.RIGHT_BRACE);
                    break;
                case ',':
                    AddToken(TokenType.COMMA);
                    break;
                case '.':
                    AddToken(TokenType.DOT);
                    break;
                case '-':
                    AddToken(TokenType.MINUS);
                    break;
                case '+':
                    AddToken(TokenType.PLUS);
                    break;
                case ';':
                    AddToken(TokenType.SEMICOLON);
                    break;
                case '*':
                    AddToken(TokenType.STAR);
                    break;
                case '!':
                    AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '<':
                    AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;
                case '>':
                    AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;
                case '/':
                    if (Match('/'))
                    {
                        while (Peek() != '\n' && !IsAtEnd())
                        {
                            Advance();
                        }
                    }
                    else
                    {
                        AddToken(TokenType.SLASH);
                    }
                    break;
                case ' ':
                case '\r':
                case '\t':
                    break;
                case '\n':
                    line++;
                    break;
                case '"':
                    String();
                    break;
                default:
                    if (Char.IsDigit(c))
                    {
                        Number();
                    }
                    else if (Char.IsLetter(c) || c == '_')
                    {
                        Identifier();
                    }
                    else
                    {
                        Lox.Error(line, "Unexpected character");
                    }
                    break;
            }
        }

        void String()
        {
            while (Peek() != '"' && !IsAtEnd())
            {
                if (Peek() == '\n')
                {
                    line++;
                }
                Advance();
            }

            if (IsAtEnd())
            {
                Lox.Error(line, "Unterminated string.");
                return;
            }

            Advance();

            String value = source.Substring(start + 1, current - start - 2);
            AddToken(TokenType.STRING, value);
        }

        void Number()
        {
            while (Char.IsDigit(Peek()))
            {
                Advance();
            }

            if (Peek() == '.' && Char.IsDigit(PeekNext()))
            {
                Advance();
            }

            while (Char.IsDigit(Peek()))
            {
                Advance();
            }

            AddToken(TokenType.NUMBER, Double.Parse(source.Substring(start, current - start)));
        }

        void Identifier()
        {
            char nextChar = Peek();
            while (Char.IsDigit(nextChar) || Char.IsLetter(nextChar) || nextChar == '_')
            {
                Advance();
                nextChar = Peek();
            }

            string text = source.Substring(start, current - start);
            TokenType type;
            if (keywords.ContainsKey(text))
            {
                type = keywords[text];
            }
            else
            {
                type = TokenType.IDENTIFIER;
            }
            AddToken(type);
        }

        bool Match(char expected)
        {
            if (IsAtEnd() || source[current] != expected)
            {
                return false;
            }

            current++;
            return true;
        }

        char Peek()
        {
            if (IsAtEnd())
            {
                return '\0';
            }
            return source[current];
        }

        char PeekNext()
        {
            if (current + 1 >= source.Length)
            {
                return '\0';
            }
            return source[current + 1];
        }

        bool IsAtEnd()
        {
            return current >= source.Length;
        }

        char Advance()
        {
            current++;
            return source[current - 1];
        }

        void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        void AddToken(TokenType type, Object literal)
        {
            String text = source.Substring(start, current - start);
            tokens.Add(new Token(type, text, literal, line));
        }
    }
}
