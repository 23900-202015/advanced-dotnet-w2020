using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Week10MemoryManagement
{
	// mark our class as disposable
	// meaning that it will clean up all the memory and objects
	// that are used as a part of the objects lifecycle
	public class MyDisposableObject : IDisposable
	{
		public MyDisposableObject() : this(new object())
		{
			
		}

		public MyDisposableObject(object myObject, FileStream fileStream = null)
		{
			this.MyObject = myObject;
			this.FileStream = fileStream;
		}

		public object MyObject { get; set; }

		public FileStream FileStream { get; set; }

		// we need to implement this because we marked our class as IDisposable
		// this method's purpose is to cleanup all the objects or other instances
		// including freeing up managed and unmanaged resources
		// A managed resource is a class, interface, object, instance, etc,
		// that does not use unsafe or fixed code
		// An unmanaged resource is a resource that has direct access to 
		// 'raw' resources on the computer, such as Pointers, FileHandle's, etc
		public void Dispose()
		{
			this.FileStream?.Dispose();
		}
	}
}
