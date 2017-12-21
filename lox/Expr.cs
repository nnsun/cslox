using System;

namespace lox
{
    abstract class Expr
    {
        internal interface IVisitor<T>
        {
            T VisitBinaryExpr(Binary expr);
            T VisitGroupingExpr(Grouping expr);
            T VisitLiteralExpr(Literal expr);
            T VisitUnaryExpr(Unary expr);
        }

        internal abstract T Accept<T>(IVisitor<T> visitor);

        internal class Binary : Expr
        {
            Binary(Expr left, Token op, Expr right)
            {
                this.left = left;
                this.op = op;
                this.right = right;
            }

            internal override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitBinaryExpr(this);
            }

            readonly Expr left;
            readonly Token op;
            readonly Expr right;
        }

        internal class Grouping : Expr
        {
            Grouping(Expr expression)
            {
                this.expression = expression;
            }

            internal override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitGroupingExpr(this);
            }

            readonly Expr expression;
        }

        internal class Literal : Expr
        {
            Literal(Object value)
            {
                this.value = value;
            }

            internal override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitLiteralExpr(this);
            }

            readonly Object value;
        }

        internal class Unary : Expr
        {
            Unary(Token op, Expr right)
            {
                this.op = op;
                this.right = right;
            }

            internal override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitUnaryExpr(this);
            }

            readonly Token op;
            readonly Expr right;
        }
    }
}
