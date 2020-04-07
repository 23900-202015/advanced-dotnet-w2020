using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    class DatabaseBalancer

    {
        private static readonly DatabaseBalancer _instance = new DatabaseBalancer();

        private DatabaseBalancer()
        {
        }

        public static DatabaseBalancer GetInstance()
        {
            return _instance;
        }

    }
}
