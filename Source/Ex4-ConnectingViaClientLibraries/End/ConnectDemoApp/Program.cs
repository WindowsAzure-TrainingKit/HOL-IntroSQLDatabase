using System;

namespace ConnectDemoApp
{
    class Program
    {
        /// <summary>
        /// Connection Variables. Please replace the values with those discussed in the lab.
        /// </summary>
        public static string userName = "[USERNAME]";
        public static string password = "[PASSWORD]";
        public static string datasource = "[SERVER].database.windows.net";
        public static string databaseName = "HoLTestDB";

        static void Main(string[] args)
        {
            //Invoke the ADO.NET connection demo
            Console.WriteLine("Starting the ADO.NET Connection Demo..");
            AdoConnectionDemo demo1 = new AdoConnectionDemo(userName, password, datasource, databaseName);
            demo1.ConnectToSQLAzureDemo();
            Console.WriteLine("Demo Complete.. Press any key");
            Console.ReadKey();

            //Invoke the ODBC connection demo
            Console.WriteLine("Starting the ODBC Connection Demo..");
            OdbcConnectionDemo demo2 = new OdbcConnectionDemo(userName, password, datasource, databaseName);
            demo2.ConnectToSQLAzureDemo();
            Console.WriteLine("Demo Complete.. Press any key");
            Console.ReadKey();

            //Invoke the OleDB connection demo
            Console.WriteLine("Starting the OLEDB Connection Demo..");
            OleDbConnectionDemo demo3 = new OleDbConnectionDemo(userName, password, datasource, databaseName);
            demo3.ConnectToSQLAzureDemo();
            Console.WriteLine("Demo Complete.. Press any key");
            Console.ReadKey();

            //Invoke the EF connection demo
            Console.WriteLine("Starting the Linq to SQL Connection Demo..");
            EFConnectionDemo demo4 = new EFConnectionDemo();
            demo4.ConnectToSQLAzureDemo();
            Console.WriteLine("Demo Complete.. Press any key");
            Console.ReadKey();
        }
    }
}
