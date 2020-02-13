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

namespace Week4ClassMapExample
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
            var domainPerson = new DomainModel.Person
            {
                Id = Guid.NewGuid(),
                CreationTime = DateTimeOffset.Now,
                Name = "Dave"
            };

            domainPerson.Addresses.Add("293 Wellington St N. Hamilton Ontario Canada L8L 8E7");

            var mapper = new Mapper();

            var informationModelPerson = mapper.Map<DomainModel.Person, InformationModel.Person>(domainPerson);

            Console.WriteLine(informationModelPerson.CreationTime);
            Console.WriteLine(informationModelPerson.Id);
            Console.WriteLine(informationModelPerson.Name);
            Console.WriteLine(string.Join(" ", informationModelPerson.Addresses));

            Console.ReadKey();


        }
    }
}
