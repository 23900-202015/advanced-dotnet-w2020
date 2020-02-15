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
	/// Represents a command attribute.
	/// </summary>
	// indicate that we only want our CommandAttribute to be applied
	// to methods, and we want multiple instances of our CommandAttribute to be applied
	// to allow for supporting multiple commands on a given method
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public sealed class CommandAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CommandAttribute"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		public CommandAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
	}
}