using System;
using System.Net;
using System.Threading;

namespace HttpListenerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            NonblockingListener(new string[] { "Http://localhost:8080/testAsyncSite/" });
            SimpleListenerExample(new string[]{ "Http://localhost:8080/testSite/"});
        }
        
        // This example requires the System and System.Net namespaces.
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            while (true)
            {
                Console.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();
                Thread.Sleep(4000);
                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            listener.Stop();
            
        }

        public static void NonblockingListener(string[] prefixes)
        {
            HttpListener listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            while (true)
            {
                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);

                // Applications can do some work here while waiting for the 
                // request. If no work can be done until you have processed a request,
                // use a wait handle to prevent this thread from terminating
                // while the asynchronous operation completes.
                Console.WriteLine("Waiting for request to be processed asyncronously.");
                result.AsyncWaitHandle.WaitOne();
                Console.WriteLine("Request processed asyncronously.");
            }
            listener.Close();
        }

        public static void ListenerCallback(IAsyncResult result)
        {
            Thread.Sleep(4000);

            HttpListener listener = (HttpListener)result.AsyncState;
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello async world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
    }
}
