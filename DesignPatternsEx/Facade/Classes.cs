using System;
using System.Collections.Generic;
using System.Text;

namespace Facade
{
    // The 'Facade' class
    class InsuranceFacade
    {
        private DrivingLicense ssA;
        private Accident ssB;
        private Claim ssC;

        public InsuranceFacade()
        {
            ssA = new DrivingLicense();
            ssB = new Accident();
            ssC = new Claim();
        }

        public void SetRate(Driver driver)
        {
            bool discountRateEligibility = true;
            Console.WriteLine("Checking discount rate eligibility...");
            if (!ssA.HasDrivingLicense(driver))
            {
                discountRateEligibility = false;
            }
            if (!ssB.HasNoAccidents(driver))
            {
                discountRateEligibility = false;
            }
            if (!ssC.HasNoClaim(driver))
            {
                discountRateEligibility = false;
            }
            if (discountRateEligibility)
            {
                Console.WriteLine("{0} is eligible to the discount rate", driver.DriverName);
            }
            else
            {
                Console.WriteLine("{0} is only eligible to the standard rate", driver.DriverName);
            }
        }


    }


    // A 'Subsystem' class
    class DrivingLicense
    {
        public bool HasDrivingLicense(Driver driver)
        {
            Console.WriteLine("Checking License Information for {0}...", driver.LicenceNumber);
            return true;
        }
    }
    // A 'Subsystem' class
    class Accident
    {
        public bool HasNoAccidents(Driver driver)
        {
            Console.WriteLine("Checking Accidents History for {0}...", driver.DriverName);
            return true;
        }
    }
    // A 'Subsystem' class
    class Claim
    {
        public bool HasNoClaim(Driver driver)
        {
            Console.WriteLine("Checking Previous Claims for {0}...", driver.DriverName);
            return true;
        }
    }

    //Helper Class
    public class Driver
    {
        public string LicenceNumber { get; set; }
        public string DriverName { get; set; }

        public Driver(string licenceNumber, string driverName)
        {
            this.LicenceNumber = licenceNumber;
            this.DriverName = driverName;
        }
    }

}
