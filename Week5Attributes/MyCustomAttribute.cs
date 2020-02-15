/*
 * Copyright 2016-2019 Mohawk College of Applied Arts and Technology
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
 * Date: 2019-1-31
 */

using System;

namespace Week5Attributes
{
	/// <summary>
	/// Represents a custom attribute.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	// declare our custom attribute class, by inheriting from System.Attribute.
	// we use the AttributeUsage class to specify where our attribute can be applied
	// by using the AttributeTargets enum, we specify that
	// our attribute can only be applied to classes
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class MyCustomAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MyCustomAttribute"/> class.
		/// </summary>
		public MyCustomAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MyCustomAttribute"/> class.
		/// </summary>
		/// <param name="myValue">My value.</param>
		public MyCustomAttribute(string myValue)
		{
			this.MyValue = myValue;
		}

		/// <summary>
		/// Gets or sets my value.
		/// </summary>
		/// <value>My value.</value>
		public string MyValue { get; set; }
	}
}