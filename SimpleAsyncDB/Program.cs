using SimpleAsyncDB.Data;
using System;
using System.Threading.Tasks;

namespace SimpleAsyncDB
{
    class Program
    {
        private static AppDbContext db = new AppDbContext();

        static async Task Main(string[] args)
        {
            //db.Persons.RemoveRange(db.Persons);

            Console.WriteLine("Starting Synchronous DB Operation!");
            DbSave();
            Console.WriteLine("Continue!");



            Console.WriteLine("Starting Asynchronous DB Operation!");
            var task = DbSaveAsync();
            Console.WriteLine("Continue!");
            await task;

            Console.WriteLine("Press enter to quit:");
            Console.ReadLine();
        }

        private static async Task DbSaveAsync()
        {
            await Task.Yield();

            //Asynchronous Insert
            for (int i = 0; i < 10000; i++)
            {
                var newPerson = new Person()
                {
                    FirstName = "ASync",
                    LastName = "Doe",
                    CreationTime = DateTime.Now
                };

                db.Persons.Add(newPerson);
            }

            Console.WriteLine("before async");
            await db.SaveChangesAsync();
            Console.WriteLine("Async Completed!");


        }
        private static void DbSave()
        {
            //Synchronous Insert
            for (int i = 0; i < 10000; i++)
            {
                var newPerson = new Person()
                {
                    FirstName = "Sync",
                    LastName = "Doe",
                    CreationTime = DateTime.Now
                };

                db.Persons.Add(newPerson);
            }

            db.SaveChanges();
            Console.WriteLine("Sync Completed!");
        }
    }
}
