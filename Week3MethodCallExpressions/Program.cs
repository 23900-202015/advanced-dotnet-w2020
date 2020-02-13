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
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Week3MethodCallExpressions
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
            var parameterExpression = Expression.Parameter(typeof(string), "s");

            // parameter expression is the object we are calling the method on
            // the second parameter hold metadata about the given method we want to invoke
            // we are looking to invoke the ToLower method on the string class
            var methodCallExpression = Expression.Call(parameterExpression, typeof(string).GetMethod("ToLower", Type.EmptyTypes));

            // convert the method call expression to a lambda expression
            // using the method call expression and the parameter expression
            // the method call expression represents the actual method we are invoking
            // the parameter expression represent the type of object we are actually calling for the given method
            var lambdaExpression = Expression.Lambda(methodCallExpression, parameterExpression);

            // the equivlant 'compile time' lambda expression is as follows
            // 's' - is our parameter expression
            // '=>' - is the lambda operator
            // 's.ToLower()' is the method call expression
            // s => s.ToLower()

            var instanceMethodCallResult = lambdaExpression.Compile().DynamicInvoke("TEST");
            Console.WriteLine($"Result of invoking our lambda expression with the value 'TEST': {instanceMethodCallResult}");


            // now we are going to invoke a static method as defined by a method call expression
            // using a lambda expression

            // s - is test, "s" being the parameter expression, as a static value
            //string.IsNullOrEmpty("test");

            // s - is test, "s" being the parameter expression, as an instance value
            //"test".ToLower();
            var staticMethodCallExpression = Expression.Call(typeof(string).GetMethod("IsNullOrEmpty", new Type[] { typeof(string) }),
                new List<ParameterExpression> { parameterExpression });

            // convert the static method call expression to a lambda expression
            var lambdaExpressionWithStaticMethodCall = Expression.Lambda(staticMethodCallExpression, parameterExpression);

            // invoke the lambda and pass a non-null and non-empty string
            var staticMethodCallResult = lambdaExpressionWithStaticMethodCall.Compile().DynamicInvoke("this is not an empty string");
            Console.WriteLine($"Result of invoking our lambda expression with the value 'this is not an empty string': {staticMethodCallResult}");

            // invoke the lambda and pass the representation of a null object
            var staticMethodCallResult2 = lambdaExpressionWithStaticMethodCall.Compile().DynamicInvoke(Expression.Constant(null).Value);
            Console.WriteLine($"Result of invoking our lambda expression with the value 'null': {staticMethodCallResult2}");

            // invoke the lambda and pass an empty string
            var staticMethodCallResult3 = lambdaExpressionWithStaticMethodCall.Compile().DynamicInvoke(string.Empty);
            Console.WriteLine($"Result of invoking our lambda expression with the value 'string.Empty': {staticMethodCallResult3}");

            Console.ReadKey();
		}
	}
}
