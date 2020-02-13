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
 * Date: 2020-2-2
 */
using System;

namespace Week5Serialization
{
    /// <summary>
    /// Represents an airplane.
    /// </summary>
    public class Airplane : Vehicle
	{
		public Airplane()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Airplane"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		public Airplane(Guid id) : base(id)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Airplane"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="dateOfManufacture">The date of manufacture.</param>
		public Airplane(Guid id, DateTimeOffset? dateOfManufacture) : base(id, dateOfManufacture)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Airplane"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="dateOfManufacture">The date of manufacture.</param>
		/// <param name="name">The name.</param>
		public Airplane(Guid id, DateTimeOffset? dateOfManufacture, string name) : base(id, dateOfManufacture, name)
		{
			
		}

		/// <summary>
		/// Gets or sets the flight speed.
		/// </summary>
		/// <value>The flight speed.</value>
		public double FlightSpeed { get; set; }

        /// <summary>
        /// Gets or sets the fuel capacity.
        /// </summary>
        /// <value>The fuel capacity.</value>
        public double FuelCapacity { get; set; }
	}
}
