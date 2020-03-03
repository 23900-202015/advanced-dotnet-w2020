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
 * Date: 2019-2-23
 */

namespace Week7TaskBasedAsyncPattern
{
	/// <summary>
	/// Represents a web result.
	/// </summary>
	public class WebResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WebResult"/> class.
		/// </summary>
		public WebResult()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebResult"/> class.
		/// </summary>
		/// <param name="address">The address.</param>
		public WebResult(string address)
		{
			this.Address = address;
		}

		/// <summary>
		/// Gets the address.
		/// </summary>
		/// <value>The address.</value>
		public string Address { get; }

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>The content.</value>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the length of the content.
		/// </summary>
		/// <value>The length of the content.</value>
		public int? ContentLength => this.Content?.Length;

		/// <summary>
		/// Returns the address and content length as a string.
		/// </summary>
		/// <returns>Returns the address and content length as a string.</returns>
		public override string ToString()
		{
			return $"Address: {this.Address}, Content Length: {this.ContentLength}";
		}
	}
}
