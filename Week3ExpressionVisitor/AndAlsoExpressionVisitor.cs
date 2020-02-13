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
using System;
using System.Linq.Expressions;

namespace Week3ExpressionVisitor
{

    /// <summary>
    /// Represents an expression visitor re-writer that rewrites AndAlso to OrElse.
    /// </summary>
    public class AndAlsoExpressionRewriter : ExpressionVisitor
    {
        /// <summary>
        /// Dispatches the expression to one of the more specialized visit methods in this class.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        public override Expression Visit(Expression node)
        {
            // if the node is null we want to exit early
            if (node == null)
            {
                return null;
            }

            // we only want to visit binary when the node type is AndAlso
            switch (node.NodeType)
            {
                case ExpressionType.AndAlso:
                    // cast the expression to a binary expression
                    return this.VisitBinary((BinaryExpression)node);
                default:
                    return base.Visit(node);
            }
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.BinaryExpression"></see>.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.AndAlso)
            {
                return base.VisitBinary(node);
            }

            // visit the left side of the tree
            var left = this.Visit(node.Left);

            // visit the right side of the tree
            var right = this.Visit(node.Right);

            if (left == null || right == null)
            {
                throw new InvalidOperationException("Unable to make a binary expression from a null node");
            }

            return Expression.MakeBinary(ExpressionType.OrElse, left, right, node.IsLiftedToNull, node.Method);
        }
    }
}
