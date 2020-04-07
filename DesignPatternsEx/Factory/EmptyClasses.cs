using System;
using System.Collections.Generic;
using System.Text;

namespace Factory.Demo
{
    class EmptyClasses
    {
        // The 'Product' abstract class
        public abstract class IProduct
        {

        }
        // A 'ConcreteProduct' class
        public class ConcreteProductA : IProduct
        {
        }
        // A 'ConcreteProduct' class
        public class ConcreteProductB : IProduct
        {
        }
        // The 'Creator' class
        public class Creator
        {
            public IProduct GetProduct(string type)
            {
                switch (type)
                {
                    case "ProductA":
                        return new ConcreteProductA();
                    case "ProductB":
                        return new ConcreteProductB();
                    default:
                        throw new NotSupportedException();
                }

            }

        }
    }
}
