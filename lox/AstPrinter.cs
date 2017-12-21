using System;
using System.Collections.Generic;
using System.Text;

namespace lox
{
    class AstPrinter : Expr.IVisitor<string>
    {
        public string VisitBinaryExpr(Expr.Binary expr)
        {
            throw new NotImplementedException();
        }

        public string VisitGroupingExpr(Expr.Grouping expr)
        {
            throw new NotImplementedException();
        }

        public string VisitLiteralExpr(Expr.Literal expr)
        {
            throw new NotImplementedException();
        }

        public string VisitUnaryExpr(Expr.Unary expr)
        {
            throw new NotImplementedException();
        }
    }
}
