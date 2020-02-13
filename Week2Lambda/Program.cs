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
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Week2Lambda
{
    class Program
    {
        // declare out delegate with the name Square
        delegate int Square(int i);

        static void Main(string[] args)
        {
            // assign the body of the delegate as a lambda expression
            // 'x' in this example, represents the input value of the function
            Square squareDelegate = x => (int)Math.Pow(x, 2);

            // invoke our delegate
            var result = squareDelegate(10);

            // the delegate can also be invoked
            // using the Invoke method provided by the framework
            var fiveResult = squareDelegate.Invoke(5);

            // print the result
            Console.WriteLine($"The result is: {result}");
            Console.WriteLine($"The result is: {fiveResult}");

            Func<int, bool> isEvenFunc = x => x % 2 == 0;
            Expression<Func<int, bool>> isEvenExpression = x => x % 2 == 0;

            var funcResult = isEvenFunc(5);
            var compiledExpression = isEvenExpression.Compile();
            var expressionResult = compiledExpression.Invoke(6);

            // compile the xpath expression once, and point to it later for re-use
            //XPathExpression xPath = XPathExpression.Compile("f:/Root/node1/@value");

            // x - is the 'temp' variable that
            // represents the input variable to the function
            // '=>' lambda operator
            // the 'left' side of the lambda operator is at least one or more
            // input variables to the function
            // the 'right' side of the lambda operator is the actual body
            // of the function to execute
            Expression<Func<double, bool>> isOddExpression = x => x % 2 != 0;

            // this expression has 2 parameters
            // the first in an integer
            // the second is an integer
            // the return type is bool
            // the return type of a Func<..> is always the type of the last listed parameter
            Expression<Func<int, int, bool>> multiInputExpression = (x, y) => x > 5 && y > 5;

            // this is a no input expression
            // the return type the first type of the list of parameters
            Expression<Func<bool>> noInputExpression = () => true;

            // using the var keyword allows us to infer the type
            // without the explicit declaration
            var myString = "test";

            // the following line will result in an error because
            // it's impossible for the compiler to determine the type of
            // our variable 'unknownData'
            //var unknownData = null;

            // an async lambda cannot be an Expression
            // because the value of the func, must be re-evaluated each time
            // the function is run
            Func<Task<bool>> asyncLambda = async () =>
            {
                await Task.Delay(2000);
                //Console.WriteLine("after 2 seconds of delay");
                return true;
            };


            var persons = new List<Person>
            {
                new Person("Dave"),
                new Person("Mary"),
                new Person("Jim"),
            };

            // objective is to filter the list to only the persons who's name is Dave
            // 'old' way
            var results = new List<Person>();
            foreach (var person in persons)
            {
                if (person.Name == "Dave")
                {
                    results.Add(person);
                }
            }

            // 'new' way
            // things to note
            // 1. (c => c.Name == "Dave") is a in-line lambda expression
            // 2. the below line, executes a LINQ expression, but the LINQ expression
            // is represented as a lambda expression
            // the 'Where' method allows us to filter the contents of
            // any given collection
            results = persons.Where(c => c.Name == "Dave").ToList();

            // retrieve all the names from the persons in the list
            // and project the results into a new list
            var selectResults = persons.Select(c => c.Name);

            // retrieve all the names from the persons list
            // and create a new anonymous object, with one property 'value'
            // and assign the value of name to the property called value
            // and project the results back
            var anonymousSelectResults = persons.Select(c => new
            {
                value = c.Name
            });

            // the 'Any' method without any parameters
            // simply return true or false
            // if there are any items in the collection
            var anyResults = persons.Any();

            // when specifying conditions on the any method (a lambda expression)
            // the any method, will return true if any element in the list
            // satisfies the given condition
            var anyWithConditionResults = persons.Any(c => c.Name == "Mary");


            // first or default without a predicate
            // will return the first item in the collection
            // or a default if specified
            var firstOrDefaultResults = persons.FirstOrDefault();

            // first or default with a predicate
            // will filter the list using the given predicate
            // and return the first result in the collection that satisfies
            // the condition
            var firstOrDefaultWithPredicate = persons.FirstOrDefault(c => c.Name == "Jim");

            // the below line, will yield the same result as the above line
            var firstOrDefaultWithWherePredicate = persons.Where(c => c.Name == "Jim").FirstOrDefault();

            // the 'all' method will return true or false
            // if all of the elements in the collection
            // satisfy the predicate
            var allResults = persons.All(c => c.Name == "Dave");

            // skip a specific number of results
            var skipResults = persons.Skip(1);

            // skip the results when the condition is true
            // however, upon the first time the condition is false, the method will exit
            // SkipWhile is typically used on ordered collections
            var skipWithPredicate = persons.SkipWhile(c => c.Name == "Dave");

            // take the results when the condition is true
            // however, upon the first time the condition is false, the method will exit
            // TakeWhile is typically used on ordered collections
            var takeWithPredicate = persons.TakeWhile(c => c.Name == "Jim");

            // find all the persons in the list with the name mary and only retrieve the names
            var combinedResult = persons.Where(c => c.Name == "Mary").Select(c => c.Name);

            // the below line, is the same as above
            // the below line filters the collection using LINQ
            // whereas the above line filters the collection using Lambda expressions 
            var equivalentLinqExpression = from p in persons
                                           where p.Name == "Mary"
                                           select p.Name;


            foreach (var name in persons.Where(c => c.Name == "Dave").Select(c => c.Name))
            {

            }

            Console.ReadKey();
        }
    }

    class Person
    {
        public Person()
        {

        }

        public Person(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
    }
}
