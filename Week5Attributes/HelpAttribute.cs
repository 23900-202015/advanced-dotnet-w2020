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
 * Date: 2019-2-7
 */

using System;

namespace Week5Attributes
{
	/// <summary>
	/// Represents a help attribute.
	/// </summary>
	/// <seealso cref="System.Attribute" />
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class HelpAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HelpAttribute"/> class.
		/// </summary>
		/// <param name="content">The content.</param>
		public HelpAttribute(string content)
		{
			this.Content = content;
		}

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public string Content { get; }
	}
}