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
using System.Dynamic;
using System.Linq;

namespace Week4DynamicKeywordExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // declare and initialize an expando object
            ExpandoObject expandoObject = new ExpandoObject();

            // declare and initialize an anonymous type
            var result = new List<string>().Select(c => new { c = c[0] });

            // create a new dynamic instance
            dynamic myObject = new { };

            myObject.value = "this is a value";
            myObject.name = "dave";

            Console.WriteLine(myObject.name);
            Console.WriteLine(myObject.value);

            Console.ReadKey();
        }
    }
}
