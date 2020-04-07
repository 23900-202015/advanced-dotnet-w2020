﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.Demo
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


    // The 'Creator' abstract class
    public abstract class Creator
    {
        public abstract IProduct FactoryMethod();
    }


    // A 'ConcreteCreator' class
    public class ConcreteCreatorA : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProductA();
        }
    }


    // A 'ConcreteCreator' class
    public class ConcreteCreatorB : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProductB();
        }
    }
}
