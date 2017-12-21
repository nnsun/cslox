using System;

namespace lox
{
    abstract class Expr
    {
        protected interface IVisitor<T>
        {
            T VisitBinaryExpr(Binary expr);
            T VisitGroupingExpr(Grouping expr);
            T VisitLiteralExpr(Literal expr);
            T VisitUnaryExpr(Unary expr);
        }

        protected abstract T Accept<T>(IVisitor<T> visitor);

        protected class Binary : Expr
        {
            Binary(Expr left, Token op, Expr right)
            {
                this.left = left;
                this.op = op;
                this.right = right;
            }

            protected override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitBinaryExpr(this);
            }

            readonly Expr left;
            readonly Token op;
            readonly Expr right;
        }

        protected class Grouping : Expr
        {
            Grouping(Expr expression)
            {
                this.expression = expression;
            }

            protected override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitGroupingExpr(this);
            }

            readonly Expr expression;
        }

        protected class Literal : Expr
        {
            Literal(Object value)
            {
                this.value = value;
            }

            protected override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitLiteralExpr(this);
            }

            readonly Object value;
        }

        protected class Unary : Expr
        {
            Unary(Token op, Expr right)
            {
                this.op = op;
                this.right = right;
            }

            protected override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitUnaryExpr(this);
            }

            readonly Token op;
            readonly Expr right;
        }
    }
}
