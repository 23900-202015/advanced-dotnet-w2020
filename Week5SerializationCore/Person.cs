﻿/*
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
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Week5SerializationCore
{
	/// <summary>
	/// Represents a person.
	/// </summary>
	[XmlRoot]
	[XmlType]
	[JsonObject]
	[Serializable]
	public class Person
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Person" /> class.
		/// </summary>
		public Person()
		{

		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[XmlElement]
		[JsonProperty]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[XmlElement]
		[JsonProperty]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the date of birth.
		/// </summary>
		/// <value>The date of birth.</value>
		[XmlIgnore]
		[JsonIgnore]
		public DateTimeOffset DateOfBirth { get; set; }

		/// <summary>
		/// Gets or sets the date of birth XML.
		/// </summary>
		/// <value>The date of birth XML.</value>
		[XmlElement]
		[JsonProperty]
		public string DateOfBirthXml
		{
			get
			{
				return this.DateOfBirth.ToString("o");
			}
			set
			{
				this.DateOfBirth = DateTimeOffset.Parse(value);
			}
		}
	}
}
