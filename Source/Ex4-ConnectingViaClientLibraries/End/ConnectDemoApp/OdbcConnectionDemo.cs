using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Odbc;

namespace ConnectDemoApp
{
    public class OdbcConnectionDemo : SQLAzureConnectionDemo
    {
        public OdbcConnectionDemo(string userName, string password, string dataSource, string databaseName) :
            base(userName, password, dataSource, databaseName)
        {
        }

        protected override DbConnection CreateConnection(string userName, string password, string dataSource, string databaseName)
        {
            return new OdbcConnection(CreateOdbcConnectionString(userName, password, dataSource, databaseName));
        }

        private string CreateOdbcConnectionString(string userName, string password, string dataSource, string databaseName)
        {
            string serverName = GetServerName(dataSource);

            OdbcConnectionStringBuilder connectionStringBuilder = new OdbcConnectionStringBuilder
            {
                Driver = "SQL Server Native Client 10.0",
            };
            connectionStringBuilder["Server"] = "tcp:" + dataSource;
            connectionStringBuilder["Database"] = databaseName;
            connectionStringBuilder["Uid"] = userName + "@" + serverName;
            connectionStringBuilder["Pwd"] = password;
            return connectionStringBuilder.ConnectionString;
        }

        protected override DbCommand CreateCommand(DbConnection connection)
        {
            return new OdbcCommand() { Connection = connection as OdbcConnection };
        }

    }
}
