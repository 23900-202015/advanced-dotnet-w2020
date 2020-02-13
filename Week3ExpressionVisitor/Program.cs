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
 * Date: 2020-1-16
 */
using System;
using System.Linq.Expressions;

namespace Week3ExpressionVisitor
{
	/// <summary>
	/// The main entry point to the program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		private static void Main(string[] args)
		{
			Expression<Func<string, bool>> andAlsoExpression = c => c.Length > 10 && c.StartsWith("A");

			Console.WriteLine($"Original expression: {andAlsoExpression}");

			var andAlsoExpressionVisitor = new AndAlsoExpressionRewriter();
			var updatedExpression = andAlsoExpressionVisitor.Visit(andAlsoExpression);

			Console.WriteLine($"Updated expression: {updatedExpression}");

			// declare and initialize a new expression that takes 3 doubles
			// and returns the result as a double

			// the first 3 arguments are the input parameters
			// and the last argument is the return type
			Expression<Func<double, double, double, double>> mathExpression = (a, b, c) => a + b - c;

			Console.WriteLine($"Original math expression: {mathExpression}");

			// declare an initialize the MathExpressionVisitor class
			var mathExpressionVisitor = new MathExpressionVisitor();

			// visit the math expression which rewrites all the operators to become 
			// multiplication operations
			var updatedMathExpression = mathExpressionVisitor.Visit(mathExpression);

			Console.WriteLine($"Updated math expression: {updatedMathExpression}");

			Console.ReadKey();
		}
	}
}
