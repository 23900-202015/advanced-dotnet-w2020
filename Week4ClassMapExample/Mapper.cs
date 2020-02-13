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

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Week4ClassMapExample
{
    /// <summary>
    /// Represents a mapper that maps a source instance to a target instance.
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mapper"/> class.
        /// </summary>
        public Mapper()
        {

        }

        public TResult Map<TSource, TResult>(TSource source)
            // ensure that the type of TSource is a derived type of IMappable
            // and the new constraint forces the TSource to have a default constructor
            where TSource : IMappable, new()

            // ensure that the type of TResult is a derived type of IMappable
            // and the new constraint forces the TResult to have a default constructor
            where TResult : IMappable, new()
        {
            // retrieve the type object for the source object
            var sourceType = source.GetType();

            //if (!typeof(IMappable).IsAssignableFrom(sourceType))
            //{
            //    throw new ArgumentException($"The type: {sourceType} does not derive from or implement: {typeof(IMappable).AssemblyQualifiedName}");
            //}

            //if (!sourceType.GetConstructors().Any(c => c.GetParameters().Count() == 0))
            //{

            //}

            var result = new TResult();

            //var result = (TResult)Activator.CreateInstance(typeof(TResult));

            // loop through each of the properties in the source object
            // that are public instance properties
            // and attempt to map them to the TResult instance
            foreach (var sourceProperty in 
                sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var targetProperty in 
                    result.GetType().GetProperties(BindingFlags.Instance 
                    | BindingFlags.Public))
                {
                    // determine the following:
                    // 1 - do both types implement the IEnumerable interface
                    // 2 - do both types have generic type arguments
                    // 3 - are the types of the generic type arguments equivalent
                    // 4 - are the source and target properties of the same type
                    if (typeof(IEnumerable<>).IsAssignableFrom(sourceProperty.PropertyType) &&
                        typeof(IEnumerable<>).IsAssignableFrom(targetProperty.PropertyType) &&
                        sourceProperty.PropertyType == targetProperty.PropertyType &&
                        sourceProperty.PropertyType.GenericTypeArguments.Any() &&
                        targetProperty.PropertyType.GenericTypeArguments.Any() &&
                        sourceProperty.PropertyType.GenericTypeArguments[0] ==
                        targetProperty.PropertyType.GenericTypeArguments[0]
                        )
                    {
                        targetProperty.SetValue(result, sourceProperty.GetValue(source));
                    }

                    // if the names match and the property types match
                    // we can set the value of the target property
                    // to the value of the source property
                    else if (targetProperty.PropertyType == sourceProperty.PropertyType &&
                        targetProperty.Name == sourceProperty.Name)
                    {
                        // the 'result' is the object on which to set the target property
                        // set the value to the value of the source property
                        targetProperty.SetValue(result, sourceProperty.GetValue(source));
                    }
                }
            }

            return result;
        }
    }
}
