using System;
namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting 'Singleton' Design Pattern Application!");
            DatabaseBalancer x = DatabaseBalancer.GetInstance();
            DatabaseBalancer y = DatabaseBalancer.GetInstance();
            if (x == y)
            {
                Console.WriteLine("It's the same object!");
            }

        }
    }
}
