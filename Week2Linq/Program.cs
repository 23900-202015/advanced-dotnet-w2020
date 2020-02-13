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

namespace Week2Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var symbols = new List<string>
            {
                ";",
                ":",
                ",",
                ".",
                "!",
                "?",
            }.ToArray();

            // this query, is simply a pointer to a object
            // where we can execute methods on the object
            // to yield various results, such as Count() and First()
            var query = from s in symbols
                        select s;

            int count = query.Count();
            var firstEntry = query.First();

            // this is our linq query with a where clause
            // allowing us to filter the results based on a given condition
            // or series of conditions
            var secondQuery = from s in symbols
                              where s == ";"
                              select s;

            Console.WriteLine(count);
            Console.WriteLine(firstEntry);

            // calling the method 'First' will give
            // us the first element in the enumerable
            // if there are no elements in the enumerable
            // an exception will be thrown, indicating
            // that the sequence contains no elements
            Console.WriteLine(secondQuery.First());

            // calling the method 'FirstOrDefault' will give us
            // the first element if there are any elements
            // or typically null if there are no elements
            Console.WriteLine(secondQuery.FirstOrDefault());

            var person = new Person("Dave", "Smith", new DateTimeOffset(new DateTime(2012, 01, 01)));

            // print out the full name of our person using the implicit
            // invocation to our extension method (person.GetFullName())
            Console.WriteLine($"The full name of our person is: {person.GetFullName()}");

            // print our the full name of our person using the explicit
            // invocation to our extension method (PersonExtensions.GetFullName(person))
            Console.WriteLine($"The full name of our person is: {PersonExtensions.GetFullName(person)}");

            var patient = new Patient("Mary", "Smith", new DateTimeOffset(new DateTime(1990, 01, 01)), "female");

            Console.WriteLine($"The full name of our patient is : {patient.GetFullName()}");

            Console.WriteLine("program complete");
            Console.ReadKey();
        }
    }

    public class Person
    {
        public Person()
        {

        }

        public Person(string firstName, string lastName, DateTimeOffset dateOfBirth)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }
    }

    public class Patient : Person
    {
        public Patient()
        {

        }
        public Patient(string firstName, string lastName, DateTimeOffset dateOfBirth, string gender)
            : base(firstName, lastName, dateOfBirth)
        {
            this.Gender = gender;
        }

        public string Gender { get; set; }
    }

    // classes which contain extension methods
    // must be static
    public static class PersonExtensions
    {
        // methods in a extension method class must also be static
        // the first parameter of all extension methods
        // must start with the 'this' keyword followed by the
        // class/interface/struct for which the extension method is targeting
        public static string GetFullName(this Person person)
        {
            return $"{person.FirstName } {person.LastName}";
        }

        public static string GetFormattedFullName<T>(this T instance) where T : Person
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance), "Value cannot be null");
            }

            return $"{instance.LastName}, {instance.FirstName}";
        }

        public static string GetFormattedFullNameAlternate<T>(this T instance)
        {
            var person = instance as Person;

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance), "Value cannot be null");
            }

            return $"{person.LastName}, {person.FirstName}";
        }

        // Nullable<DateTimeOffset> is equivalent to DateTimeOffset?
        public static DateTime GetLocalDateOfBirth(this DateTimeOffset? dateOfBirth)
        {
            // we need to check if the date of birth is null
            // secondly we need to check if the date of birth has an underlying value
            if (dateOfBirth == null || !dateOfBirth.HasValue)
            {
                throw new ArgumentNullException(nameof(dateOfBirth), "Value cannot be null");
            }

            return dateOfBirth.Value.ToLocalTime().DateTime;
        }
    }
}
