﻿/*
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

namespace Week4ClassMapExample.DomainModel
{
	/// <summary>
	/// Represents a person.
	/// </summary>
	public class Person : IDomainModel
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class.
		/// </summary>
		public Person()
	    {
		    this.Addresses = new List<string>();
	    }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the creation time.
		/// </summary>
		/// <value>The creation time.</value>
		public DateTimeOffset CreationTime { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the addresses.
		/// </summary>
		/// <value>The addresses.</value>
		public List<string> Addresses { get; set; }
    }
}
