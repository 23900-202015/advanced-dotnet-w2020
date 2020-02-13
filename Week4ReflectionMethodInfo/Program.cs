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
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Week4ReflectionMethodInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a new instance of our person
            var person = new Person();

            // retrieve the type object for our person object
            var type = person.GetType();

            // find all the methods on the person class
            // that are public instance methods
            // we do this, by using the binding flags enum
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            // print out some metadata about the methods on the person class
            foreach (MethodInfo method in methods)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine($"Method name: {method.Name}");

                // the type that declares the method
                Console.WriteLine($"Declaring type: {method.DeclaringType}");

                // the reflected type represents the type
                // who 'reflected' the type currently being reflected through
                Console.WriteLine($"Reflected type: {method.ReflectedType}");

                Console.WriteLine($"Contains generic parameters: {method.ContainsGenericParameters}");
                Console.Write(Environment.NewLine);
            }

            foreach (var method in methods)
            {
                // if the method has only 1 parameter and the parameter is a string, invoke the method
                if (method.GetParameters().Count(c => c.ParameterType == typeof(string)) == 1)
                {
                    // invoke th method
                    // person - represents the object in which we are invoke the method
                    // the object array - represents the parameters passed to the method
                    method.Invoke(person, new object[] { "dave" });
                }
            }

            Console.WriteLine("The names of our person");

            foreach (var name in person.Names)
            {
                Console.WriteLine(name.Value);
            }

            Console.ReadKey();
        }
    }

    public class Person : MyInterface
    {
        public Person()
        {
            this.Names = new List<Name>();
        }

        public List<Name> Names { get; set; }

        public void AddName(string name)
        {
            this.AddName(new Name(NameType.FirstName, name));
        }

        public void AddName(Name name)
        {
            this.Names.Add(name);
        }

        public void DoOperation()
        {
            Console.WriteLine("The do operation method was called");
        }
    }

    public class Name
    {
        public Name()
        {

        }

        public Name(NameType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public NameType Type { get; set; }

        public string Value { get; set; }
    }

    public enum NameType
    {
        FirstName = 0x00,
        MiddleName = 0x01,
        LastName = 0x02
    }

    public interface MyInterface
    {
        void DoOperation();
    }
}
