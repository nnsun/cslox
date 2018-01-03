using System;
using System.Collections.Generic;
using System.Text;

namespace lox
{
    class RuntimeError : Exception
    {
        readonly Token token;

        public RuntimeError(Token token, string message) : base(message)
        {
            this.token = token;
        }
    }
}
