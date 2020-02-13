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
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Week5Attributes
{
    /// <summary>
    /// Represents the main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            // TODO: create command attribute
            // TODO: create help attribute
            // TODO: create sample methods to perform operations

            Console.WriteLine("Please enter a command to run");

            var input = Console.ReadLine();

            var inputType = input.GetType();
            var methods = typeof(Program).GetMethods(BindingFlags.Static | BindingFlags.Public);

            // if the input is 'help'
            // we want look for all the methods in the program class
            // and display the help content for each method
            if (input == "help")
            {
                var helpAttributes = methods.Where(c => c.GetCustomAttributes<HelpAttribute>().Any()).SelectMany(c => c.GetCustomAttributes<HelpAttribute>());

                foreach (var helpAttribute in helpAttributes)
                {
                    Console.WriteLine(helpAttribute.HelpText);
                }
            }
            else
            {
                var methodToExecute = methods.Where(c => c.GetCustomAttributes<CommandAttribute>().Any(x => x.Command == input)).FirstOrDefault();

                List<object> parameters = new List<object>();
                // if there are any parameters on the method we want to execute
                // we need to ask the user for those parameters
                if (methodToExecute.GetParameters().Any())
                {
                    foreach (var parameter in methodToExecute.GetParameters())
                    {
                        Console.WriteLine($"Please enter a parameter value for the parameter:{parameter.Name}");
                        var parameterInput = Console.ReadLine();

                        parameters.Add(parameterInput);
                    }
                }

                methodToExecute.Invoke(null, parameters.ToArray());
            }

            Console.ReadKey();
        }

        [Command("p")]
        [Command("print")]
        [Help("Prints the given text to the screen.")]
        public static void Print(string text)
        {
            Console.WriteLine(text);
        }

        [Command("r")]
        [Command("run")]
        [Help("Runs a given program by program name")]
        public static void Run(string program)
        {
            Process.Start(program);
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CommandAttribute : Attribute
    {

        public CommandAttribute(string command)
        {
            this.Command = command;
        }

        public string Command { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class HelpAttribute : Attribute
    {
        public HelpAttribute(string helpText)
        {
            this.HelpText = helpText;
        }

        public string HelpText { get; set; }

    }
}
