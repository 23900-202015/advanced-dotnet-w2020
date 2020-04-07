using System;
using Factory;
using static Factory.Classes;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting 'Factory' Design Pattern Application!");

            // An array of creators
            Creator factoryCreator = new Creator();
            IVehicle carProduct = factoryCreator.GetVehicle("Car");

            Console.WriteLine("Created a new {0}, The mode of trnasport is {1}",
              carProduct.GetType().Name, carProduct.ModeOfTransport);

            IVehicle boatProduct = factoryCreator.GetVehicle("Boat");
            Console.WriteLine("Created a new {0}, The mode of trnasport is {1}",
  boatProduct.GetType().Name, boatProduct.ModeOfTransport);

            Console.ReadLine();
        }
    }
}
