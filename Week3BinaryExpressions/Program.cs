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

namespace Week3BinaryExpressions
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
        public static void Main(string[] args)
		{
            // declare and initialize the left parameter expression, represented as the variable x
			var leftParameterExpression = Expression.Parameter(typeof(int), "x");

			// declare and initialize the left parameter expression, represented as the variable y
            var rightParameterExpression = Expression.Parameter(typeof(int), "y");

            // combine the two parameter expressions into a binary expression 
            var binaryExpression = Expression.Multiply(leftParameterExpression, rightParameterExpression);

            // create a lambda expression using the binary expression, the left parameter expression, and the right parameter expression
            // passing 'leftParameterExpression' and 'rightParameterExpression' allows us to actually input values into our function
            var lambdaExpression = Expression.Lambda<Func<int, int, int>>(binaryExpression, leftParameterExpression, rightParameterExpression);

            // compile and invoke the lambda expression, passing the parameters 7 and 6
            var result = lambdaExpression.Compile().DynamicInvoke(7, 6);

            // print the result
            Console.WriteLine(result);
            Console.WriteLine($"The expression represented as a string: {lambdaExpression}");

            // the below line is equivalent to the above code
            Expression<Func<int, int, int>> multiplyExpression = (x, y) => x * y;

            // compile and invoke the expression
            Console.WriteLine(multiplyExpression.Compile().Invoke(7, 6));
            Console.ReadKey();
		}
	}
}
