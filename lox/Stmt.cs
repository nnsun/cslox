using System;

namespace lox
{
    abstract class Stmt
    {
        internal interface IVisitor<T>
        {
            T VisitExpressionStmt(Expression stmt);
            T VisitPrintStmt(Print stmt);
        }

        internal abstract T Accept<T>(IVisitor<T> visitor);

        internal class Expression : Stmt
        {
            public Expression(Expr expression)
            {
                this.expression = expression;
            }

            internal override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitExpressionStmt(this);
            }

            internal readonly Expr expression;
        }

        internal class Print : Stmt
        {
            public Print(Expr expression)
            {
                this.expression = expression;
            }

            internal override T Accept<T>(IVisitor<T> visitor)
            {
                return visitor.VisitPrintStmt(this);
            }

            internal readonly Expr expression;
        }
    }
}
