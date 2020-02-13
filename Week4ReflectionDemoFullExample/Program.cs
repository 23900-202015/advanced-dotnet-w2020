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
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Week4ReflectionDemoFullExample
{
    public class Program
    {
        private static readonly Dictionary<Type, string> typeMap = new Dictionary<Type, string>
        {
            { typeof(Guid), "uniqueidentifier NOT NULL" },
            { typeof(Guid?), "uniqueidentifier NULL" },
            { typeof(DateTimeOffset), "datetimeoffset(7) NOT NULL" },
            { typeof(DateTimeOffset?), "datetimeoffset(7) NULL" },
            { typeof(int), "INT NOT NULL" },
            { typeof(int?), "INT NULL" },
            { typeof(double), "decimal(20, 0) NOT NULL" },
            { typeof(double?), "decimal(20, 0) NULL" },
            { typeof(string), "varchar(max) NULL" }
        };

        private static void Main(string[] args)
        {

            // find all the types in the current assembly
            // that are not abstract classes and are not generic types
            // and that implement the IDatabaseTable interface
            var types = typeof(Program).Assembly.ExportedTypes.Where(c => c.IsClass &&
                                                                            !c.IsAbstract &&
                                                                            !c.IsGenericType &&
                                                                            typeof(IDatabaseTable).IsAssignableFrom(c));

            var results = types.Select(GenerateTable);

            foreach (var result in results)
            {
	            Console.WriteLine(result);
            }

            Console.ReadKey();
        }

        public static string GenerateTable(Type type)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"CREATE TABLE {type.Name}");
            builder.AppendLine("(");

            // find all the properties on the given type
            // that are public instance properties
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                // generate the syntax
                // for the table, based on the property info
                builder.AppendLine($"\t{GenerateColumn(propertyInfo)}");
            }

            builder.AppendLine(");");

            return builder.ToString();
        }

        public static string GenerateColumn(PropertyInfo propertyInfo)
        {
	        var type = typeof(Program).Assembly.ExportedTypes.FirstOrDefault(c => c == propertyInfo.PropertyType);

	        if (typeMap.ContainsKey(propertyInfo.PropertyType) || type == null)
	        {
		        return $"{propertyInfo.Name} {typeMap[propertyInfo.PropertyType]},";
	        }

	        var property = propertyInfo.ReflectedType?.GetProperty($"{type.Name}Id");

	        if (property == null)
	        {
		        throw new InvalidOperationException($"Unable to create column, no backing property type for type: {propertyInfo.Name}");
	        }

	        if (propertyInfo.ReflectedType.GetProperty(type.Name) != null)
	        {
		        return null;
	        }

	        return $"{propertyInfo.Name} {typeMap[property.PropertyType]},";
        }
    }

    public interface IDatabaseTable
    {

    }

    public class Owner : IDatabaseTable
    {
	    public Owner()
	    {
		    
	    }

	    public Guid Id { get; set; }

	    public DateTimeOffset CreationTime { get; set; }
    }

    public interface IShape : IDatabaseTable
    {
        Guid Id { get; set; }

        DateTimeOffset CreationTime { get; set; }
    }

    public abstract class Shape : IShape
    {
        protected Shape()
        {

        }

        public Guid Id { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public Owner Owner { get; set; }

        public Guid OwnerId { get; set; }
    }

    public class Circle : Shape
    {
        public Circle()
        {

        }

        public double Radius { get; set; }
    }

    public class Rectangle : Shape
    {
        public Rectangle()
        {

        }

        public double Length { get; set; }

        public double Width { get; set; }
    }

    public class Triangle : Shape
    {
        public Triangle()
        {

        }

        public double Base { get; set; }

        public double Height { get; set; }
    }

    public class Square : Shape
    {
        public double Side { get; set; }
    }
}
