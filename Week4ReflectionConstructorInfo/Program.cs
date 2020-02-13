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
 * Date: 2020-2-1
 */
using System;
using System.Linq;
using System.Reflection;

namespace Week4ReflectionConstructorInfo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // using the typeof operator we can retrieve the 'Type object' for the given type
            // in our case, we are retrieving the type object for our Person class
            var type = typeof(Person);

            // the typeof operator can only be used on static types
            // meaning the type must be known at compile time
            // the following lines will cause an error
            var myInt = 5;
            //var typeofInt = typeof(myInt);

            // if we don't the type ahead of time, we cannot use the typeof operator
            // we have to use the GetType method
            var intType = myInt.GetType();

            var constructors = typeof(Person).GetConstructors();

            foreach (var constructor in constructors)
            {
                Console.WriteLine(constructor.Name);
            }

            // find all the public instance constructors on the person class
            constructors = typeof(Person).GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            // for each constructor that does not have a parameter
            // invoke the constructor
            foreach (var constructor in constructors.Where(c => !c.GetParameters().Any()))
            {
                var instance = constructor.Invoke(null);
            }

            // find all the static constructors on the person class
            constructors = typeof(Person).GetConstructors(BindingFlags.Static);

            Console.WriteLine("Program complete");
            Console.ReadKey();
        }
    }
    public class Person
    {
        // define a static constructor
        static Person()
        {

        }

        // define the default constructor
        public static Person()
        {
            Console.WriteLine("Default person constructor invoked");
        }

        // define a parameterized constructor
        public Person(string name)
        {
            this.Name = name;
            Console.WriteLine("Parameterized person constructor invoked");
        }

        public string Name { get; set; }

    }
}
