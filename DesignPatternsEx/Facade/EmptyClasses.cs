
using System;
using System.Collections.Generic;
using System.Text;



namespace Facade.Demo
{
    // The 'Facade' class
    class FacadeClass

    {
        private SubSystemA ssA;
        private SubSystemB ssB;
        private SubSystemC ssC;

        public FacadeClass()
        {
            ssA = new SubSystemA();
            ssB = new SubSystemB();
            ssC = new SubSystemC();
        }

        public void Operation1()
        {
            Console.WriteLine("\nMethodA() ---- ");
            ssA.MethodA();
            ssB.MethodB();
            ssC.MethodC();
        }

        public void Operation2()
        {
            Console.WriteLine("\nMethodB() ---- ");
            ssC.MethodC();
            ssB.MethodB();
            ssA.MethodA();

        }
    }


    // A 'Subsystem' class
    class SubSystemA

    {
        public void MethodA()
        {
            Console.WriteLine("SubSystemA Method");
        }
    }
    // A 'Subsystem' class
    class SubSystemB

    {
        public void MethodB()
        {
            Console.WriteLine("SubSystemB Method");
        }
    }
    // A 'Subsystem' class
    class SubSystemC

    {
        public void MethodC()
        {
            Console.WriteLine("SubSystemC Method");
        }
    }


}
