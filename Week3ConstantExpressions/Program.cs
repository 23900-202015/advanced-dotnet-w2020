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

namespace Week3ConstantExpressions
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
            // declare and initialize a constant expression
            // with the value "my value"
            var constantExpression = Expression.Constant("my value");

            // constant expressions can also hold integer values
            var constantIntegerExpression = Expression.Constant(5);

            // as well as null
            var constantNullExpression = Expression.Constant(null);

            // each of the constants expressions below
            // represent a single leaf in a tree structure
            var seven = Expression.Constant(7);
            var six = Expression.Constant(6);
            var three = Expression.Constant(3);

            Console.WriteLine(constantExpression);
            Console.WriteLine(constantIntegerExpression);
            Console.WriteLine(constantNullExpression);
            Console.WriteLine(seven);
            Console.WriteLine(six);
            Console.WriteLine(three);

			Console.ReadKey();
		}
	}
}
