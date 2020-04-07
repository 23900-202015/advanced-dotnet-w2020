using System;
namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting 'Facade' Design Pattern Application!");

            Driver sampleDriver = new Driver("ABC 123 XYZ","John Doe");

            InsuranceFacade newFacade = new InsuranceFacade();
            newFacade.SetRate(sampleDriver);

            Console.ReadLine();
        }
    }
}
