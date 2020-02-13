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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Week3PropertyExpressions
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
            Console.WriteLine("Please enter a name to search:");
            var name = Console.ReadLine();

            Console.WriteLine("Please enter an age to search:");
            string ageInput = Console.ReadLine();

            var person = new Person
			{
                Age = 21,
				Name = "Mary"
			};

			// declare and initialize our parameter expression
            // to represent access to our object in the expression tree
            var personParameterExpression = Expression.Parameter(typeof(Person), "p");

            // declare and initialize a property expression
            // supplying a constant expression - which is the actual object
            // the second parameter is the name of the property we want to access
            var propertyExpression = Expression.Property(personParameterExpression, "Name");

            // declare and initialize the parameter expression of type string
            // to allow us to enter a parameter for our method invocation
            var stringParameterExpression = Expression.Parameter(typeof(string), "s");
            var ageParameterExpression = Expression.Parameter(typeof(uint), "i");

            var lambdaExpression = Expression.Lambda(
	            Expression.MakeBinary(ExpressionType.Equal, propertyExpression, stringParameterExpression),

                // supply the parameter expression of the person to be our first parameter
                // supply the parameter expression of the string to be our second parameter
	            new List<ParameterExpression> { personParameterExpression, stringParameterExpression });

            if (uint.TryParse(ageInput, out var age))
            {
                var lambdaExpressionVisitor = new LambdaExpressionVisitor();

                lambdaExpression = (LambdaExpression)lambdaExpressionVisitor.Modify(lambdaExpression, new List<ParameterExpression>
                {
                    personParameterExpression,
                    stringParameterExpression,
                    ageParameterExpression
                });

                var innerResult = lambdaExpression.Compile().DynamicInvoke(person, name, age);

                Console.WriteLine($"Result: {innerResult}");
            }
            else
            {

	            // invoke the method using a given person and name to compare
	            var result = lambdaExpression.Compile().DynamicInvoke(person, "Mary");

	            // print the result
	            Console.WriteLine($"The result of the expression, where 's' is the value of 'Mary': {lambdaExpression}: {result}");

	            // invoke the method using a given person and name to compare
	            result = lambdaExpression.Compile().DynamicInvoke(person, "Smith");

	            // (p, s) => (p.Name == s)
	            // (p, s, i) => (p.Name == s && p.Age == i)

	            // print the result
	            Console.WriteLine($"The result of the expression, where 's' is the value of 'Smith': {lambdaExpression}: {result}");
            }


            Console.ReadKey();
		}
	}

    public class LambdaExpressionVisitor : ExpressionVisitor
    {
	    private List<ParameterExpression> parameters;

        public Expression Modify(Expression originalExpression, List<ParameterExpression> parameters)
        {
	        if (originalExpression == null)
	        {
		        throw new ArgumentNullException(nameof(originalExpression), "Value cannot be null");
	        }

	        this.parameters = parameters;

	        return Expression.Lambda(this.Visit(originalExpression), this.parameters);
        }

        public override Expression Visit(Expression node)
        {
	        switch (node.NodeType)
            {
                case ExpressionType.Parameter:
                    return this.VisitParameter((ParameterExpression)node);
                case ExpressionType.Lambda:
                    return this.VisitLambdaGeneric((LambdaExpression)node);
                case ExpressionType.Equal:
                    return this.VisitBinary((BinaryExpression)node);
                default:
                    return base.Visit(node);
            }
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            // p.Age == i
            // create our age property expression
            var agePropertyExpression = Expression.Property(this.parameters.First(c => c.Type == typeof(Person)), "Age");

            // create our parameter property expression
            var ageParameterExpression = this.parameters.First(c => c.Type == typeof(uint));

            var updatedExpression = Expression.MakeBinary(ExpressionType.AndAlso, node, 
                Expression.MakeBinary(ExpressionType.Equal, agePropertyExpression, ageParameterExpression));

            return updatedExpression;
        }

        protected virtual Expression VisitLambdaGeneric(LambdaExpression node)
        {
	        return this.Visit(node.Body);
        }
    }

    /// <summary>
    /// Represents a person.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Person(string name)
        {
	        this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        public uint Age { get; set; }
    }
}
