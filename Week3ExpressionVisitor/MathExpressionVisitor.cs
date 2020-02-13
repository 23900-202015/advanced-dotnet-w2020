/*
 * Copyright 2016-2020 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: Nityan Khanna
 * Date: 2020-1-18
 */

using System.Linq.Expressions;

namespace Week3ExpressionVisitor
{
	/// <summary>
	/// Represents an expresion visitor that rewrites math expressions. 
	/// Converts all math operators to be multiplication only.
	/// </summary>
	public class MathExpressionVisitor : ExpressionVisitor
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="MathExpressionVisitor"/> class.
        /// </summary>
        public MathExpressionVisitor()
        {

        }

        public override Expression Visit(Expression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                    // we can safely cast the expression to a binary expression
                    // because all the above types in our case statement
                    // are all expression types that only support a binary expression
                    return this.VisitBinary((BinaryExpression)node);

                // add the default case so that we can safely
                // ignore any expressions that do not have the above expression types
                default:
                // always call base.methodname, in our case
                // we are calling base.Visit(node);
                    return base.Visit(node);
            }
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:

                    // visit the right side of the binary expression
                    var right = this.Visit(node.Right);

                    // visit the left side of the binary expression
                    var left = this.Visit(node.Left);

                    // return a new binary expression
                    // with new expression type of Multiply
                    return Expression.MakeBinary(ExpressionType.Multiply, left, right);

                // add the default case so that we can safely
                // ignore any expressions that do not have the above expression types
                default:
                    // always call base.methodname, in our case
                    // we are calling base.VisitBinary(node);
                    return base.VisitBinary(node);
            }
        }
    }
}
