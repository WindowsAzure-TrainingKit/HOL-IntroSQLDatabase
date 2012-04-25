<a name="HOLTop" />
# Introduction to SQL Azure #

---

<a name="Overview" />
## Overview ##

SQL Azure makes the power of Microsoft SQL Server available in a Cloud Hosted offering. Working with SQL Azure should be a familiar experience for most developers because, for the most part, it supports the same tooling and development practices currently used for on premises SQL Server applications.

However, there are some small differences between working with SQL Azure and working with on-premises SQL Server. Some of these differences are inherent in the way that SQL Azure has been architected and some will only apply during the Community Technical Preview phase.

This hands-on lab will walk through a series of simple use cases for SQL Azure such as provisioning your account, creating, and using a database. You will create a simple Windows Azure application to allow you to manipulate the data in the Contact table of a database running in SQL Azure.

<a name="Objectives" />
### Objectives ###

In this hands-on lab, you will learn how to:

-	Use SQL Azure as a cloud hosted database platform for your applications.
-	Provision a new account in SQL Azure and create new databases within the context of your account.
-	Create new SQL Azure users and grant them appropriate permissions.
-	Use SQL Azure to execute statements to create tables and indexes and to insert and query rows.
-	Build a simple data driven ASP.NET page using the graphical controls in Visual Studio.
-	Connect to SQL Azure Database via Client Libraries. 

<a name="Prerequisites" />
### Prerequisites ###

The following is required to complete this hands-on lab:

-	[Microsoft .NET Framework 4.0][1]
-	[Microsoft Visual Studio 2010][2]
-	[SQL Server Management Studio 2008 R2 Express Edition][3]
-	[Windows Azure Tools for Microsoft Visual Studio 1.6][4]
-	Access to a **SQL Azure** account with a server created
	-	**SQL Azure Firewall** enabled for machine running this lab

[1]: http://go.microsoft.com/fwlink/?linkid=186916
[2]: http://msdn.microsoft.com/vstudio/products/
[3]: http://www.microsoft.com/downloads/details.aspx?familyid=56AD557C-03E6-4369-9C1D-E81B33D8026B
[4]: http://www.microsoft.com/windowsazure/sdk/

>**Note:** This hands-on lab has been designed to use the latest release of the Windows Azure Tools for Visual Studio 2010 (version 1.6) and the new Windows Azure Management Portal experience.
To complete this hands-on lab, you need to have a SQL Azure account. To sign up, visit [http://www.microsoft.com/windowsazure/sqlazure/](http://www.microsoft.com/windowsazure/sqlazure/)

<a name="Setup" />
### Setup ###

In order to execute the exercises in this hands-on lab you need to set up your environment.

1. Open a Windows Explorer window and browse to the lab’s **Source** folder.

1. Double-click the **Setup.cmd** file in this folder to launch the setup process that will configure your environment and install the Visual Studio code snippets for this lab.

1. If the User Account Control dialog is shown, confirm the action to proceed.

> **Note:** Make sure you have checked all the dependencies for this lab before running the setup. 

<a name="CodeSnippets" />
### Using the Code Snippets ###

Throughout the lab document, you will be instructed to insert code blocks. For your convenience, most of that code is provided as Visual Studio Code Snippets, which you can use from within Visual Studio 2010 to avoid having to add it manually.

---

<a name="Exercises" />
## Exercises ##

This hands-on lab includes the following exercises:

1. [Preparing Your SQL Azure Account](#Exercise1)
1. [Basic DDL and DML - Creating Tables and Indexes](#Exercise2)
1. [Build a Windows Azure Application that Accesses SQL Azure](#Exercise3)
1. [Connecting via Client Libraires](#Exercise4)

Estimated time to complete this lab: **60 minutes**.

> **Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Every predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this lab describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in these procedures that you need to take into account.

<a name="Exercise1" />
### Exercise 1: Preparing Your SQL Azure Account ###

In this exercise, you will connect to your SQL Azure account and create a database server, add a new user and then reconnect to SQL Azure so that you can begin working with your new database.

<a name="Ex1Task1" />
#### Task 1 - Retrieving your SQL Azure Server Name ####
In this task, you will log into the SQL Azure portal to obtain the name of the SQL Azure server assigned to your account.

1. Open **Internet Explorer** and navigate to the Windows Azure portal at [https://windows.azure.com](https://windows.azure.com).

1. Log in to your Windows Live account.
	
	 ![LoggingIntoTheAzureServicesPortal](images/loggingintotheazureservicesportal.png?raw=true)

	_Logging into the Azure Services Portal_

1. At the Windows Azure portal home page, click **New Database Server** on the ribbon. 
	
	 ![Creating a new SQL Azure database server](images/creating-a-new-sql-azure-database-server.png?raw=true)

	_Creating a new SQL Azure database server_ 

1. If you have not previously created a server, you will need to do so now; otherwise, you may skip this exercise. 

1. To create a server, select your subscription on the left pane. Click **Create** on the **Server** ribbon.
	 
	![CreatingNewSQLAzureDatabaseServer](images/creatingnewsqlazuredatabaseserver.png?raw=true)

	_Creating a new SQL Azure database server_ 

1. Select a region from the **Region** drop down list, and then click **Next**. The location determines which datacenter the database will reside in.
	 
	![ChoosingRegion](images/choosingregion.png?raw=true)

	_Choosing region_ 

1. Enter an administrator account name and password and click **Next**.
	 
	![EnteringAdministratorLoginAndPassword](images/enteringadministratorloginandpassword.png?raw=true)

	_Entering administrator login and password_ 
	
	>**Note:** An administrator account is a master account used to manage the new server. You should avoid using this account in connection strings where the username and password may be exposed.
	>The password policy requires that the password follow certain rules.
	>
	>![PasswordPolicy](images/passwordpolicy.png?raw=true)

	
1. Click **Finish** to create the new server. You will configure firewall rules later on this exercise.

	![FirewallRulesDialog](images/firewallrulesdialog.png?raw=true)

	_Firewall rules dialog_

1. Finally, the new server information, including **Fully Qualified Server Name**, is shown. 

	![SQLAzureProjectsList](images/sqlazureprojectslist.png?raw=true)

	_SQL Azure projects list_ 

	>**Note:** The fully qualified domain name of the server uses the following format:
     _\<ServerName\>.database.windows.net_ where _\<ServerName\>_ identifies the server, for example, _a9ixtp7pux.database.windows.net_.
 
1. Expand the subscription node located on the left pane and select the server name you have created. The **Server Information** page allows you to perform basic administration of the database server. 
	 
	![SQLAzureServerInformationPage](images/sqlazureserverinformationpage.png?raw=true)

	_SQL Azure server information page_ 

1. The **Firewall Rules** allows you to specify a list of IP addresses that can access your SQL Azure Server. The firewall will deny all connections by default, so **be sure to configure your allow list** so that existing clients can connect to the server. 
	 
	![ConfiguringFirewallSettingsForSQLAzure](images/configuringfirewallsettingsforsqlazure.png?raw=true)

	_Configuring the firewall settings for SQL Azure_ 
	
	>**Note:** Changes to your firewall settings can take some time to become effective. 

	You now have a database server created and ready for the next steps in this lab. This database can be connected to from anywhere in the world.

<a name="Exercise2" />
### Exercise 2: Working with Data Basic DDL and DML ###

In this exercise, you will create a new database and work with its data. This means you will create some tables, index those tables appropriately, and then insert and query data. For this purpose, you will use two different tools. The first tool, the Database Manager for SQL Azure, is a browser based Silverlight database administration tool that you can access from the Windows Azure portal. The other tool is SQL Server Management Studio, a tool normally associated with SQL Server management. You will see that this tool is equally useful for managing your SQL Azure databases.

<a name="Ex2Task1" />
#### Task 1 - Creating a New Database ####

1. In the Windows Azure Management portal UI, select the **Database** option.

1. Under **Subscriptions**, expand your pr
oject in the tree view on the left, select the server name where you wish to create a database and then click **Create** in the **Database** group of the ribbon.
	
	![CreatingNewDatabase](images/creatingnewdatabase.png?raw=true)

	_Creating a new database_  

1. In the **Create Database** dialog, set the **Database name** to _HoLTestDB_, select the _Web_ **Edition** and set the **Maximum size** to _1 GB_.
	 
	![ChoosingDatabaseFeatures](images/choosingdatabasefeatures.png?raw=true)

	_Choosing database features_
	
	>**Note:** In this hands-on lab, you create a database using the SQL Azure portal. Databases can also be created by executing a DDL query against your assigned server using the T-SQL CREATE DATABASE statement, specifying which SQL Azure database edition (Web or Business) to create as well as its maximum size. For example, to create a Business Edition database with a maximum size of 30GB, use the following T-SQL command: 
	>
	>**CREATE DATABASE HolTestDB (MAXSIZE = 30GB)** 
	>
	>Once a database reaches its maximum size, you cannot insert additional data until you delete some data to free storage space or increase its maximum size.

<a name="Ex2Task2" />
#### Task 2 - Managing your Database with the Database Manager for SQL Azure ####

In this task, you use the Database Manager for SQL Azure, a Silverlight client that runs in your browser, to connect to your SQL Azure database, create and populate a table, and then query its contents.

1. Expand the server node under your subscription, click the _HoLTestDB_ database to select it, and then click **Manage** on the ribbon.
	 
	![ManagingDatabase](images/managingdatabase.png?raw=true)

	_Managing a database_ 

1. You will be redirected to the **SQL Azure Management Portal**. Enter your Server Administrator username and password and then click **Log on**.

	![SigningSQLAzureManagementPortal](images/signingsqlazuremanagementportal.png?raw=true)

	_Signing in to the SQL Azure Management Portal_ 

1. Wait until you are connected to your database and the **Administration** page is shown.

	![SQLAzureManagementPortalAdministrationPage](images/sqlazuremanagementportaladministrationpage.png?raw=true)

	_SQL Azure Management Portal Administration page_ 


1. On the left pane, click **Design** option.
	 
	![SQLAzureDesignView](images/sqlazuredesignview.png?raw=true)
  
	_SQL Azure Design view_ 

1. Make sure **Tables** option is selected in the navigation menu and click **New table**.
	 
	![CreatingNewTable](images/creatingnewtable.png?raw=true)

	_Creating a new table_

1. In the table creation UI, set the **Name** of the table to _People_. 

1. Next, define three table columns using the information shown below and click **Save**.

	| **Column** | **Type** | **Is Identity?** | **Is Required?** | **Is Primary Key?** |
	|------------|----------|------------------|------------------|---------------------|
	|ID          |Int       |Yes               |Yes               |Yes                  |
	|Name        |nvarchar(50)       |No               |Yes               |No                  |
	|Age         |Int       |No               |Yes               |No                  |
	
	![CreatingTableSchema](images/creatingtableschema.png?raw=true)

	_Creating the table schema_

1. Once the table is saved, click **Data** in the navigation menu.
	 
	![InsertCaption](images/insertcaption.png?raw=true)

	_Insert Caption_

1. Now, click **Add Row** and enter sample data for the _Name_ and _Age_ columns. 

	| **Name** | **Age** |
	|----------|---------|
	|Alexandra |16       |
	|Ian       |18       |
	|Marina    |45       |
	
	![AddingRowsTable](images/addingrowstable.png?raw=true)

	_Adding rows to the table_

1. Repeat the previous step to add another two rows and then click **Save** to commit the data to the table.
	 
	![SavingData](images/savingdata.png?raw=true)

	_Saving Data_

1. Next, click **New Query** in the ribbon.
1. In the query window, enter the following T-SQL statement to select all the rows in the _People_ table and then click **Run**. Verify that the results grid shows the rows that you entered previously.
	
	````T-SQL
	select * from People
	````
	![QueryingDatabase](images/queryingdatabase.png?raw=true)

	_Querying the database_

<a name="Ex2Task3" />
#### Task 3 - Managing your Database with SQL Server Management Studio ####
In this task, you use SQL Server Management Studio, a tool typically used for managing SQL Server, to connect to your SQL Azure server and administer it.

1. Open SQL Server Management Studio from **Start | All Programs | Microsoft SQL Server 2008 R2 | SQL Server Management Studio**. You will be presented with a logon dialog.
1. In the **Connect to Server** dialog, enter your login information ensuring that you select **SQL Server Authentication**. SQL Azure currently only supports SQL Server Authentication. 
	
	![ConnectingSQLAzureSSMS](images/connectingsqlazuressms.png?raw=true)

	_Connecting to SQL Azure with SQL Server Management Studio_ 
1. Click **Connect**.
1. You should now see in your **Object Explorer** the structure of your database. Notice that your SQL Azure database is no different to an on-premises relational database.

	![ObjectExplorerShowingDatabase](images/objectexplorershowingdatabase.png?raw=true)

	_Object Explorer showing the HoLTestDB database_ 

1. In Object Explorer, select the **HoLTestDB** database in the tree view and then click **New Query** on the toolbar.
	   
	![CreatingNewQueryWindow](images/creatingnewquerywindow.png?raw=true)

	_Creating a new query window_ 

1. You now have a query window with an active connection to your account. You can test your connection by display the result of the **@@version** scalar function. To do this, type the following statement into the query window and press the **Execute** button. You will get back a scalar result that indicates the edition as Microsoft SQL Azure.
	
	````T-SQL
	SELECT @@version
	````
	 
	![RetrievingSQLAzureVersion](images/retrievingsqlazureversion.png?raw=true)

	_Retrieving the SQL Azure version_ 

1. Replace the previous query with the statement shown below and click **Execute.** Notice that the results grid shows the databases currently accessible.
	
	````T-SQL
	SELECT * FROM sys.databases
	````
	![QueryResultsShwoingListDatabases](images/queryresultsshwoinglistdatabases.png?raw=true)

	_Query results showing the list of databases in your subscription_

1. You can check that you are now in the context of your user database by executing the following query. Make sure that you replace the previous query.
	
	````T-SQL
	SELECT db_name()
	````
	![QueryingDatabaseCurrentlyUse](images/queryingdatabasecurrentlyuse.png?raw=true)
 
	_Querying the database currently in use_ 

1. Do not close the query window. You will need it during the next task.

<a name="Ex2Task4" />
#### Task 4 - Creating Logins and Database Users ####
Much like SQL Server, SQL Azure allows you to create additional logins and then assign those logins as users with permissions on a database. In this task, you will create a new login and then create a user that uses the new login in your _HoLTestDB_ database.

1. Open a new query window connected to the _master_ database. To do this, in **Object Explorer**, expand the **System Databases** node inside **Databases** and then select _master_. Then, click **New Query** on the toolbar.
	 
	![QueryingMasterDatabase](images/queryingmasterdatabase.png?raw=true)

	_Querying the master database_ 

	>**Note:** You cannot reuse the previous query window connected to the _HoLTestDB_ database because you cannot change the database context without closing the current connection. The USE \<database\_name\> command does not work with SQL Azure. Therefore, you need to open a new query window or disconnect and reconnect in order to change from the _HoLTestDB_ to the _master_ database. 

1. Create a new login by executing the following statement:
	
	<!-- mark: 1 -->
	````SQL
	CREATE LOGIN HoLUser WITH password='Password1'
	````	

	>**Note:** You should choose your own password for this login account and use it where appropriate throughout the lab. If you do not choose a unique password, you should ensure that you remove this login when you finish the lab. To do this, execute the following statement in the _master_ database:
	>
	>**DROP LOGIN HoLUser** 

1. Go back to the query window connected to the _HoLTestDB_ database. If you closed this window, open it again by selecting the _HoLTestDB_ database in **Object Explorer** and then click **New Query**. 
1. In the query window, execute the following statement to create a new user in the HoLTestDB database for the login _HoLUser_.
	
	<!-- mark: 1-2 -->
	````SQL
	-- Create a new user from the login and execute
	CREATE USER HoLUser FROM LOGIN HoLUser
	````

1. Next, add the user to the **db_owner** role of your _HoLTestDB_ database by executing the following:
	
	<!-- mark: 1-2 -->
	````SQL
	-- Add the new user to the db_owner role and execute
	EXEC sp_addrolemember 'db_owner', 'HoLUser'
	````

	>**Note:** By making your user a member of the **db_owner** role, you have granted a very extensive permission set to the user. In a real world scenario, you should be careful to ensure that you grant users only the smallest privilege set possible.
 
1. Change the user associated with the current connection to the newly created _HoLUser_. To do this, right-click the query window, point to **Connection**, and then select **Change Connection**.
	 
	![ChangingDatabaseConnectionProperties](images/changingdatabaseconnectionproperties.png?raw=true)

	_Changing the database connection properties_

1. In the **Connect to Database Engine** dialog, replace the **Login** name with _HoLUser_ and set the **Password** to the value that you chose earlier when you created the database user.
	 
	![ConnectingDatabaseDifferentUser](images/connectingdatabasedifferentuser.png?raw=true)

	_Connecting to the database as a different user_

1. Click **Options** to show additional connection settings. Switch to the **Connection Properties** tab and ensure that the name of the database for the connection is _HoLTestDB_. If the current value is different, you will need to type this rather than use the drop down list, then press the **Connect** button.
	 
	![ConnectingSpecificDatabase](images/connectingspecificdatabase.png?raw=true)

	_Connecting to a specific database_ 
	
	>**Note:** You are now connected to the database as the _HoLUser_ database user. You will continue with this user for the remaining steps of this exercise.

<a name="Ex2Task5" />
#### Task 5 - Creating Tables, Indices, and Queries ####
1. In the query window, replace the current content with the following SQL query to create a _Contact_ table and execute it.
	
	<!-- mark: 1-9 -->
	````SQL
	CREATE TABLE [Contact](
	    [ContactID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	    [Title] [nvarchar](8) NULL,
	    [FirstName] [nvarchar](50) NOT NULL,
	    [LastName] [nvarchar](50) NOT NULL,
	    [EmailAddress] [nvarchar](50) NULL,
	    [Phone] [nvarchar](30) NULL,
	    [Timestamp] [timestamp] NOT NULL
	)
	````
	
	>**Note:** SQL Azure requires that every table have a clustered index. If you create a table without a clustered index, you will not be able to insert rows into the table until you have created one.
	Because the clustered index determines the order of rows on disk, and thus affects certain queries, you may choose to place the clustered index on a column other than the primary key column.
	
1. You will add an index on the _EmailAddress_ field. To do this, execute the following query:
	
	<!-- mark: 1-2 -->
	````SQL
	CREATE INDEX IX_Contact_EmailAddress
	    ON Contact(EmailAddress)
	````
	
1. Execute the following query to add a row to the new _Contact_ table:
	
	<!-- mark: 1-4 -->
	````SQL
	INSERT INTO [Contact]
	([Title],[FirstName],[LastName],[EmailAddress],[Phone])
	     VALUES
	('Mr','David','Alexander','davida@fabrikam.com','555-1234-5555')
	````
	
1. Now, query the data back out, but start by enabling the SHOWPLAN_ALL option to show the execution plan. To do that, execute the following query:
	
	<!-- mark: 1-5 -->
	```` SQL
	SET SHOWPLAN_ALL ON
	GO
	SELECT * FROM Contact WHERE EmailAddress ='davida@fabrikam.com'
	GO
	SET SHOWPLAN_ALL OFF
	````	
	![QueryExecutionPlanSmallSet](images/queryexecutionplansmallset.png?raw=true)

	_Query execution plan for a small set_ 

1. Next, you will add a large number of rows to the database and then examine the query plan again. To do this, execute the following query to create a stored procedure named _AddData_. This stored procedure will loop incrementing a counter each time through and add a new record with an email address with the pattern [Counter]davida@fabrikam.com:
	
	<!-- mark: 1-13 -->
	````SQL
	CREATE PROCEDURE AddData
	@NumRows int
	AS
	DECLARE @counter int
	SELECT @counter = 1
	WHILE (@counter < @NumRows)
	BEGIN
	    INSERT INTO [Contact]
	        ([Title],[FirstName],[LastName],[EmailAddress],[Phone])
	        VALUES
	        ('Mr','David','Alexander',CAST(@counter as nvarchar)+'davida@fabrikam.com','555-1234-5555')
	        SELECT @counter = @counter + 1
	END
	````
	
1. Now, insert 10,000 rows into the _Contact_ table by executing the new stored procedure. Each row will have a unique email address. To do this, execute the following T-SQL statement:
	
	<!-- mark: 1 -->
	````SQL
	EXEC AddData 10000
	````
	
	>**Note:** It may take some time to generate the 10,000 rows.
	
1. Execute the following query again and examine the generated plan. Compare the result with the one obtained previously, when the table contained few rows.
	
	<!-- mark: 1-5 -->
	````SQL
	SET SHOWPLAN_ALL ON
	GO
	SELECT * FROM Contact WHERE EmailAddress ='davida@fabrikam.com'
	GO
	SET SHOWPLAN_ALL OFF
	````
	
	![QueryExecutionPlanLargeSet](images/queryexecutionplanlargeset.png?raw=true)

	_Query execution plan for a large set_ 
	
	>**Note:** Notice that the second time around the query optimizer is likely to use the index that you defined: This is the Index Seek line in the query plan.
	
1. For the most part, you can use any tool that you might have previously used with SQL Server on-premises. For an example of this, look at the query plan graphically. In SQL Server Management Studio press **Ctrl-L** to display the **Estimated Execution Plan**.
	 
	![ShowingQueryExecutionPlanGraphically](images/showingqueryexecutionplangraphically.png?raw=true)

	_Showing the query execution plan graphically_

<a name="Exercise3" />
### Exercise 3: Build a Windows Azure Application that Accesses SQL Azure ###

In this exercise, you will create a simple Windows Azure application to manipulate the data in the _Customer_ table of the _AdventureWorksLT2008_ database.

The purpose of this exercise is to demonstrate just how simple it is to work with SQL Azure and Windows Azure using the graphical Visual Studio 'drag and drop' approach.

<a name="Ex3Task1" />
#### Task 1 - Loading the Sample Database into SQL Azure ####

1. Connect to the _HoLTestDB_ database on your SQL Azure server using the _HoLUser_ login that you created in the previous exercises. You can use either SQL Server Management Studio or the Database Manager for SQL Azure to perform these steps.
1. If you are using SQL Server Management Studio, in the **File** menu, select **Open** | **File** and then navigate to the **Assets** folder inside the **Source** folder of this lab; if you use the Database Manager for SQL Azure, select the **Database** context on the upper left corner and click the **Open Query** button in the **File** group. Select the **AdventureWorks2008LT_Azure.sql** file and click **Open**.
	
	>**Note:** This script contains a cleaned up export script from the _AdventureWorksLT2008_ sample database available for download in the [Codeplex sample databases project site](http://msftdbprodsamples.codeplex.com/). SQL Azure sample databases will be available for downloading that you could use to replace this script file.

1. Execute the query. This may take a few minutes, as you are creating a subset of the Adventure Works database.

<a name="Ex3Task2" />
#### Task 2 - Creating the Visual Studio Project ####
In this task, you create a new Visual Studio project for a Windows Azure Web Site. 

1. Open Microsoft Visual Studio 2010 in elevated administrator mode. To do this, in **Start** | **All Programs** | **Microsoft Visual Studio 2010**, right-click the **Microsoft Visual Studio 2010** shortcut and choose **Run as Administrator**. 

1. If the **User Account Control** dialog appears, click **Continue**.

1. From the **File** menu, choose **New** and then **Project**. 

1. In the **New Project** dialog, expand **Visual C#** in the **Installed Templates** list and select **Cloud**.

1. In the **Templates** list, select **Windows Azure** **Project**. Set the name of the project to “**AdventureWorks**” and the location inside **Ex3-BuildingSQLAzureApp** in the **Source** folder of the lab. Ensure that **Create directory for solution** is checked and then set the name of the solution to “**Begin**”. Click **OK** to create the project.
	  
	![CreatingNewWebCloudService](images/creatingnewwebcloudservice.png?raw=true)

	_Creating a new Web Cloud Service_ 
	
1. In the **New Windows Azure Project** dialog, inside the **Roles** panel, expand the tab for Visual C#, select **ASP.NET Web Role** from the list of available roles and click the right button (**>**) to add an instance of this role to the solution. Before closing the dialog, select the new role in the right panel, click the pencil icon and rename the role as **AdventureWorksWeb**. Click **OK** to create the cloud service solution.
	 
	![AddingWebRoleSolution](images/addingwebrolesolution.png?raw=true)
	
	_Adding a Web Role to the Solution_ 

1. When the project template has finished creating items, you should be presented with the **Default.aspx** page. If not, open this file.

1. Ensure that you are viewing the Default.aspx page in Design View and click the **Design** button.

1. Drag and drop a **GridView** control from the _Data_ section of the Toolbox onto the design canvas.
	  
	![AddingGridViewControl](images/addinggridviewcontrol.png?raw=true)

	_Adding a GridView control_

1. From the _SmartTag_ on the upper right corner of the newly created GridView, choose the **New data source** option on the **Choose Data Source** combo box.
	 
	![CreatingNewDataSource](images/creatingnewdatasource.png?raw=true)

	_Creating a new data source_

1. In the **Data Source Configuration Wizard**, choose a data source type of **Database** and leave the default ID. Click **OK**.
	 
	![ChoosingDataSource](images/choosingdatasource.png?raw=true)

	_Choosing a Data Source_

1. In the **Configure Data Source** dialog, click **New Connection**.
	 
	![CreatingNewConnection](images/creatingnewconnection.png?raw=true)

	_Creating a new Connection_

1. If prompted by a **Choose data source** dialog, select **Microsoft SQL Server** and click **Continue**.
1. Now, configure a connection to your SQL Azure database. In the **Add Connection** dialog, ensure your provider is **Microsoft SQL Server (SqlClient)** selecting **Microsoft SQL Server** inside **Data Source** list and **.NET Framework Data Provider for SQL Server** in the Data Provider combo. Then set the **Server name** to the name of the server for your SQL Azure subscription. Next, change the authentication type to **Use SQL Server Authentication** and type the credentials for your SQL Azure subscription. Finally, enter _HoLTestDB_ in the database name drop down list.
	  
	![ConfiguringConnectionDatabaseSQLAzure](images/configuringconnectiondatabasesqlazure.png?raw=true) 

	_Configuring a connection to the HolTestDB database in SQL Azure_
 
1. Press **Test Connection**. If the connection information is correct, you should receive a dialog indicating success. Click **OK** to proceed.
	 
	![ConfirmationSuccesfulConnection](images/confirmationsuccesfulconnection.png?raw=true)
 
	_Confirmation of a successful connection_ 

1. Click **OK** to close the **Add Connection** dialog.

1. Click **Next** to proceed with the **Data Source Configuration Wizard**.

1. Ensure that the option labeled **Yes, save this connection as** is checked, set the name of the connection to **AdventureWorksLTConnectionString**, and then click **Next**.
	 
	![SavingConnectionString](images/savingconnectionstring.png?raw=true)

	_Saving the connection string in the application configuration file_ 

1. Select the option labeled **Specify a custom SQL statement or stored procedure** and then click **Next**. 
	 
	![UsingCustomSQLStatement](images/usingcustomsqlstatement.png?raw=true)

	_Using a custom SQL statement to query the database_ 
	
	>**Note:** You cannot use the **Specify columns from a table or view** option because _AdventureWorks_ uses a named Schema (SalesLT) that you need to explicitly reference.

1. Paste the following statement into the **SQL Statement** box and click **Next**.
	
	<!-- mark: 1 -->
	````SQL
	SELECT [FirstName], [LastName], [CompanyName], [EmailAddress] FROM [SalesLT].[Customer]
	````
	 
	![DefiningCustomSQLStatement](images/definingcustomsqlstatement.png?raw=true)

	_Defining a custom SQL statement_ 

1. Press **Test Query** and you should see results returned.
	 
	![TestingQueryAgainstDatabase](images/testingqueryagainstdatabase.png?raw=true)

	_Testing the query against the database_ 

1. Click **Finish**.

1. Press **F5** to run the application in the compute emulator.

1. The application will execute and you will see the list of all customers in the browser:
	
	![RetrievingListCustomersDatabase](images/retrievinglistcustomersdatabase.png?raw=true)

	_Retrieving a list of customers from the database_ 

1. Close the browser window.

<a name="Exercise4" />
### Exercise 4: Connecting via Client Libraries ###

In this exercise, you will learn how to use ADO.NET, ODBC, OLEDB and LINQ to SQL technologies to connect to your SQL Azure database and perform some simple T-SQL operations. In addition, you will see how to connect to the database from other technologies like Java and PHP.

Using Microsoft Technologies, you will see that the way in which you interact with your SQL Azure database from your applications is the same as a traditional SQL database. The main differences between the technologies lie in the type of connection and the connection strings used to connect to SQL Azure. After the connection is established, you can then use the appropriate inheritor of the ‘DbCommand’ to issue your commands to SQL Azure.

<a name="Ex4Task1" />
#### Task 1 - Opening the Begin Solution and Exploring the Common Functionalities ####

You will test the different Microsoft technologies connecting to SQL Azure and performing some tasks against a new table. To avoid spending time implementing logic that creates, inserts, queries and deletes a table, this exercise provides a begin solution that implements these common functionalities. This allows you to focus on learning how to connect to SQL Azure and explore the differences between the proposed technologies. 

In this task, you will open the **ConnectDemoApp** solution and explore the **SQLAzureConnectionDemo** class. During the exercise, you will inherit from this class for each different implementation of a data access technology.

1. Open **Microsoft Visual Studio 2010** from **Start** | **All Programs** | **Microsoft Visual Studio 2010** | **Microsoft Visual Studio 2010**.

1. Open the begin solution provided for this exercise. To do this, from the **File** menu, choose **Open Project**. In the **Open Project** dialog, navigate to **Ex4-ConnectingViaClientLibraries\begin** inside the **Source** folder of this lab and then open the solution **ConnectDemoApp.sln** inside the **ConnectDemoApp** folder. A solution with the following structure should open.
	 
	![ConnectDemoAppSolutionStructure](images/connectdemoappsolutionstructure.png?raw=true)  

	_Connect Demo App solution’s structure_ 
	 
1. As mentioned before, you will create a class per technology inheriting from the **SQLAzureConnectionDemo** abstract class. This class provides common functionality to perform basic operations against SQL Azure using the provider that you  implement in the derived class. The table below explains each of the methods in this class to understand how it works and determine which methods you need to implement in the derived classes: 

	| **Method** | **Type** | **Description** |
	|------------|----------|-----------------|
	|CreateConnection  |Abstract|A derived class implements this method in order to create the connection according to the underlying technology.|
	|CreateCommand     |Abstract|A derived class implements this method to create a command according to the underlying technology.|
	|GetServerName     |    |Returns the server name from the data source. It is a common task required to create the connection to the database.|
	|ConnectToSQLAzureDemo| |Sets the connection property based on the result of the CreateConnection abstract method that will be implemented on the derived class. Executes the demo flow against the SQL Azure Database. It gets a command from the derived class using the CreateCommand method and then executes the Execute* methods to create, fill, query and delete a demo table.|
	|ExecuteCreateDemoTableStatement||Executes a create table statement to create the "DemoTable" table.|
	|ExecuteInsertTestDataStatement||Executes an insert statement against the "DemoTable" table.|
	|ExecuteReadInsertedTestData||Executes a select statement trying to retrieve the data inserted by the previous method and calls the ReadData method to show it in the Console.|
	|ReadData          |    |Reads the data retrieved from the table and displays it in the Console.|
	|ExecuteDropDemoTable|  |Executes a delete statement removing the "DemoTable" table from the SQL Azure database.|

	Notice that you will only have to override the **CreateConnection** and **CreateCommand** methods on the implementation of each technology to create a connection to SQL Azure successfully.

<a name="Ex4Task2" />
#### Task 2 - Connecting to SQL Azure Using ADO.NET ####
In this task, you will create a class that inherits from the **SQLAzureConnectionDemo** class and implements the methods to connect to SQL Azure using ADO.NET.

1. Add a new class to the project named **AdoConnectionDemo**. To do this, right-click the **ConnectDemoApp** project in **Solution Explorer** and select **Add** | **Class**. In the **Add New Item** dialog, make sure that you select the **Class** template and set the name to **AdoConnectionDemo.cs**.

1. Make sure that you have the following namespace directives at the top of the file:

	<!-- mark: 1-2-->
	````C#
	using System.Data.Common;
	using System.Data.SqlClient;
	````
 
1. Update the class definition to inherit from **SQLAzureConnectionDemo**. It should look like the following:
	
	<!-- mark: 1 -->
	````C#
	public class AdoConnectionDemo : SQLAzureConnectionDemo
	{
	}
	````
	
1. The **SQLAzureConnectionDemo** class delegates the connection construction to the derived class. Override the **CreateConnection** method to create a **SqlConnection** in your **AdoConnectionDemo** class:
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ADO CreateConnection - C#_)
	
	<!-- mark: 1-4-->
	````C#
	protected override DbConnection CreateConnection(string userName, string password, string dataSource, string databaseName)
	{
	  return new SqlConnection(this.CreateAdoConnectionString(userName, password, dataSource, databaseName));
	}
	````
	
1. Implement the **CreateAdoConnectionString** method used by the **CreateConnection** method. This method is responsible for building up the connection string for the ADO.NET Connection, which takes advantage of the **SqlConnectionStringBuilder** class in the underlying implementation. 
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ADO CreateAdoConnectionString method - C#_)
	
	<!-- mark: 1-15-->
	````C#
	private string CreateAdoConnectionString(string userName, string password, string dataSource, string databaseName)
	{
	  // create a new instance of the SQLConnectionStringBuilder
	  SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
	  {
	    DataSource = dataSource,
	    InitialCatalog = databaseName,
	    Encrypt = true,
	    TrustServerCertificate = false,
	    UserID = userName,
	    Password = password,
	  };
	
	  return connectionStringBuilder.ToString();
	}
	````
	
1. Override the **CreateCommand** method to create an ADO.NET command. Remember that this abstract method is called in the parent class to get the connection and execute the different SQL statement samples.
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ADO CreateCommand method - C#_)
	
	<!-- mark: 1-4 -->
	````C#
	protected override DbCommand CreateCommand(DbConnection connection)
	{
	  return new SqlCommand() { Connection = connection as SqlConnection };
	}
	````
	That is all the code required to use an ADO.NET connection. Now you will include some code on the **Program.cs** file to test the connection and see how the different operations work.
1. Open the **Program.cs** file double-clicking it in the **Solution Explorer** inside the **ConnectDemoApp** project.

1. Implement the logic to create an instance of the **AdoConnectionDemo** class and execute the demo against SQL Azure.
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ADO demo implementation - C#_)
	
	<!-- mark: 3-8 --->
	````C#
	static void Main(string[] args)
	{
	  // Invoke the ADO.NET connection demo
	  Console.WriteLine("Starting the ADO.NET Connection Demo...");
	  AdoConnectionDemo demo1 = new AdoConnectionDemo();
	  demo1.ConnectToSQLAzureDemo(userName, password, datasource, databaseName);
	  Console.WriteLine("Demo Complete... Press any key");
	  Console.ReadKey();
	}
	````
	
1. Locate the member variables declared immediately above method **Main** and update the placeholders with the connection information for your SQL Azure account.
	 
	![ConfiguringConnectionParametersSQLAzure](images/configuringconnectionparameterssqlazure.png?raw=true)

	_Configuring connection parameters for SQL Azure_ 
	
1. Run the application by pressing **F5**. You should see the following output in a console window.
	
	![ExpectedOutputADONetConnectionDemo](images/expectedoutputadonetconnectiondemo.png?raw=true)

	_Expected output from the ADO.NET connection demo_

<a name="Ex4Task3" />
#### Task 3 - Connecting to SQL Azure Using ODBC ####
In this task, you will create a class that inherits from the **SQLAzureConnectionDemo** class and implement the methods for connecting to SQL Azure using ODBC.

1. Add a new class to the project named **OdbcConnectionDemo**. To do this, right-click the **ConnectDemoApp** project in **Solution Explorer** and select **Add** | **Class**. In the **Add New Item** dialog, make sure that you select the **Class** template and set the name to **OdbcConnectionDemo.cs**.

1. Make sure that you have the following namespace directives at the top of the file:
	
	<!-- mark: 1-2 -->
	````C#
	using System.Data.Common;
	using System.Data.Odbc;
	````

1. Update the class definition to inherit from **SQLAzureConnectionDemo**. It should look like the following:

	<!-- mark: 1 -->
	````C#
	public class OdbcConnectionDemo : SQLAzureConnectionDemo
	{
	}
	````
	
1. Override the **CreateConnection** method to create an **OdbcConnection** in your **OdbcConnectionDemo** class:
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ODBC CreateConnection - C#_)
	
	<!-- mark: 1-4 -->
	````C#
	protected override DbConnection CreateConnection(string userName, string password, string dataSource, string databaseName)
	{
	  return new OdbcConnection(this.CreateOdbcConnectionString(userName, password, dataSource, databaseName));
	}
	````
	
1. Implement the **CreateOdbcConnectionString** method used by the **CreateConnection** method. This method is responsible for building up the ODBC Drivers connection string. The proposed implementation is using **SQL Server Native Client 10.0** as its driver. You can specify any other ODBC driver of your preference here. 
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ODBC CreateOdbcConnectionString method - C#_)
	
	<!-- mark: 1-14 -->
	````C#
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
	````
	
1. Override the **CreateCommand** method to create an **OdbcCommand**. Remember that this abstract method is called in the parent class to get the connection and execute the different SQL statement samples.
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ODBC CreateCommand method - C#_)
	
	<!-- mark: 1-4 -->
	````C#
	protected override DbCommand CreateCommand(DbConnection connection)
	{
	  return new OdbcCommand() { Connection = connection as OdbcConnection };
	}
	````
	
	That is the specific code required to use an ODBC connection. Now you will include some code in the **Program.cs** file to test the connection and see how the different operations work.
	
1. Open the **Program.cs** file double-clicking it in the **Solution** **Explorer** inside the **ConnectDemoApp** project.

1. In method **Main**, implement the logic to create a new instance of the **OdbcConnectionDemo** class and execute the demo against SQL Azure. You can add or replace the code from the previous tasks based on whether you want to test all the technologies at once or only this one.
	
	(Code Snippet - _Intro to SQL Azure - Ex4 ODBC demo implementation - C#_)
	
	<!-- mark: 5-10 -->
	````C#
	static void Main(string[] args)
	{
	  //...
	
	  // Invoke the ODBC connection demo
	  Console.WriteLine("Starting the ODBC Connection Demo...");
	  OdbcConnectionDemo demo2 = new OdbcConnectionDemo();
	  demo2.ConnectToSQLAzureDemo(userName, password, datasource, databaseName);
	  Console.WriteLine("Demo Complete... Press any key");
	  Console.ReadKey();
	}
	````
	
1. If you have not done so before, update the value of the member variables located immediately above method **Main** by replacing the placeholders with the connection information for your SQL Azure account. 

1. Run the application by pressing **F5**. You should see the following output in a console window.
	 
	![ExpectedOutputODBCConnectionDemo](images/expectedoutputodbcconnectiondemo.png?raw=true)

	_Expected output from the ODBC connection demo_

<a name="Ex4Task4" />
#### Task 4 - Connecting to SQL Azure Using OLEDB ####

In this task, you will create a class that inherits from the **SQLAzureConnectionDemo** class and implements the methods for connecting to SQL Azure using OLEDB.

1. Add a new class to the project named **OleDbConnectionDemo**. To do this, right-click the **ConnectDemoApp** project in **Solution** **Explorer** and select **Add** | **Class**. In the **Add** **New** **Item** dialog, make sure to select the **Class** template and set the name to **OleDbConnectionDemo.cs**.

1. Make sure that you have the following namespace directives at the top of the file:

	<!-- mark: 1-2 -->
	````C#
	using System.Data.Common;
	using System.Data.OleDb;
	```` 
	
1. Update the class definition to inherit from **SQLAzureConnectionDemo**. It should look like the following:
	
	<!-- mark: 1 -->
	````C#
	public class OleDbConnectionDemo : SQLAzureConnectionDemo
	{
	}
	````
	
1. Override the **CreateConnection** method to create an **OleDbConnection** in your **OleDbConnectionDemo** class:
	
	(Code Snippet - _Intro to SQL Azure - Ex4 OLEDB CreateConnection - C#_)
	
	<!-- mark: 1-4 -->
	```` C#
	protected override DbConnection CreateConnection(string userName, string password, string dataSource, string databaseName)
	{
	  return new OleDbConnection(this.CreateOleDBConnectionString(userName, password, dataSource, databaseName));
	}
	````
	
1. Implement the **CreateOleDbConnectionString** method used by the **CreateConnection** method. This method is responsible for building up the connection string used to create the connection to SQL Azure using OLEDB. 
	
	(Code Snippet - _Intro to SQL Azure - Ex4 OLEDB CreateOleDbConnectionString method - C#_)
	
	<!-- mark: 1-15 -->
	````C#
	private string CreateOleDBConnectionString(string userName, string password, string dataSource, string databaseName)
	{
	  string serverName = GetServerName(dataSource);
	
	  OleDbConnectionStringBuilder connectionStringBuilder = new OleDbConnectionStringBuilder
	  {
	    Provider = "SQLOLEDB",
	    DataSource = dataSource,
	  };
	  connectionStringBuilder["Initial Catalog"] = databaseName;
	  connectionStringBuilder["UId"] = userName + "@" + serverName;
	  connectionStringBuilder["Pwd"] = password;
	
	  return connectionStringBuilder.ConnectionString;
	}
	````
	
1. Override the **CreateCommand** method to create an **OleDbCommand**. Remember that this abstract method is called in the parent class to get the connection and execute the different SQL statement samples.
	
	(Code Snippet - _Intro to SQL Azure - Ex4 OLEDB CreateCommand method - C#_)
	
	<!-- mark: 1-4 -->
	```` C#
	protected override DbCommand CreateCommand(DbConnection connection)
	{
	  return new OleDbCommand() { Connection = connection as OleDbConnection };
	}
	````
	That is the specific code required to use an OLEDB connection. Now you will include some code in the **Program.cs** file to test the connection and see how the different operations work.
	
1. Open the **Program.cs** file double-clicking it in the **Solution** **Explorer** inside the **ConnectDemoApp** project.

1. In method **Main**, implement the logic to create an instance of the **OleDbConnectionDemo** class and execute the demo against SQL Azure. You can add or replace the code from the previous tasks based on whether you want to test all the technologies at once or only this one.
	
	(Code Snippet - _Intro to SQL Azure - Ex4 OLEDB demo implementation - C#_)
	
	<!-- mark: 5-10 -->
	````C#
	static void Main(string[] args)
	{
	  //...
	
	  // Invoke the OleDB connection demo
	  Console.WriteLine("Starting the OLEDB Connection Demo…");
	  OleDbConnectionDemo demo3 = new OleDbConnectionDemo();
	  demo3.ConnectToSQLAzureDemo(userName, password, datasource, databaseName);
	  Console.WriteLine("Demo Complete... Press any key");
	  Console.ReadKey();
 	}
	````
 
1. If you have not done so before, update the value of the member variables located immediately above method **Main** by replacing the placeholders with the connection information for your SQL Azure account.

1. Run the application by pressing **F5**. You should see the following output in a console window.
	 
	![ExpectedOutputOLEDBConnectionDemo](images/expectedoutputoledbconnectiondemo.png?raw=true)

	_Expected output from the OLEDB connection demo_

<a name="Ex4Task5" />
#### Task 5 - Connecting to SQL Azure Using Entity Framework ####
You have connected in three different ways to the database on SQL Azure. The last technology that you are going to try will be **Entity Framework**. You will notice that the class implementation for this demo will not inherit from the **SQLAzureConnectionDemo** class because when using **Entity Framework**, you do not have to manage Connections and Commands; those are administered by the underlying technology and you do not have to worry about them.

1. Open the **App.config** file and change the connection string to point to your SQL Azure Database, and to connect using the test user created earlier in this lab.
	
	>**Note:** This step is required because **Entity Framework** gets the connection settings from the configuration file.
 
1. Add a new ADO.NET Entity Data Model to the project named **HoLModel**. To do this, right-click on the **ConnectDemoApp** project in the **Solution Explorer**. Select **Add | New Item**. In the **Add New Item** dialog, make sure you select **ADO.NET Entity Data Model** template and then set the name to **HoLModel.edmx**.
	 
	![AddingEFModel](images/addingefmodel.png?raw=true)

	_Adding EF model_

1. In the **Entity Data Model Wizard** select **Generate from database** and click **Next**.
	
	![ChoosingModelContents](images/choosingmodelcontents.png?raw=true)
 
	_Choosing model contents_ 

1. In the **Choose Your Data Connection** step, select **Yes, include sensitive data in the connection string** and leave **AdventureWorksLTConnectionString** as data connection and **HolTestDBEntities** as entity connection settings name.
	
	![ChoosingModelContents2](images/choosingmodelcontents2.png?raw=true)

	_Choosing model contents_ 

1. In the **Choose Your Database Objects** step, select all database objects and click **Finish**.
	 
	![ChoosingDatabaseObjects](images/choosingdatabaseobjects.png?raw=true)

	_Choosing database objects_

1. Once created the Model is shown.
	 
	![EFModelCreated](images/efmodelcreated.png?raw=true)

	_EF model created_

1. Add a new class to the project named **EFConnectionDemo**. To do this, right-click the **ConnectDemoApp** project in **Solution** **Explorer** and select **Add** | **Class**. In the **Add** **New** **Item** dialog, make sure you select the **Class** template and then set the name to **EFConnectionDemo.cs**.
	
1. Make sure that you have the following namespace directives at the top of the class:
	
	<!-- mark: 1-2 -->
	````C#
	using System;
	using System.Linq;
	````
	
1. Add the following method to the **EFConnectionDemo** class. This retrieves from the database all the company names and prints them to the console. To do that, it takes advantage of the EF **HolTestDBEntities** class.
	
	(Code Snippet - _Intro to SQL Azure - Ex4 EF ConnectToSQLAzure method - C#_)
	
	<!-- mark: 1-17-->
	````C#
	/// <summary>
	/// HolTestDbEntities takes care of handling your transactions for you
	/// leaving you free use Linq to extract information stores up in the cloud
	/// </summary>
	public void ConnectToSQLAzureDemo()
	{
	  using (HolTestDBEntities context = new HolTestDBEntities())
	  {	
		  IQueryable<string> companyNames = from customer in context.Customers
		    where customer.CustomerID < 20
		    select customer.CompanyName;
		            
		  foreach (var company in companyNames)
		  {
		    Console.WriteLine(company);
		  }
	   }
	}
	````
      
1. Add the following code to invoke the LINQ to SQL demo in method **Main** of the **Program.cs** file. You can add or replace the code from the previous tasks depending on whether you want to test all the technologies at once or only this one.

	(Code Snippet - _Intro to SQL Azure - Ex4 EF demo implementation - C#_)

	<!-- mark: 5-10 -->
	````C#
	static void Main(string[] args)
	{
	  //...
	
	  // Invoke the Entity Framework connection demo
	  Console.WriteLine("Starting the Entity Framework Connection Demo...");
	  EFConnectionDemo demo4 = new EFConnectionDemo();
	  demo4.ConnectToSQLAzureDemo();
	  Console.WriteLine("Demo Complete... Press any key");
	  Console.ReadKey();
	}
	````
	
1. Press **F5** to run your application. You should see a long list of company names. These are retrieved from your database on the SQL Azure Server using LINQ to SQL. 
	 
	![ExpectedOutputEFConnectionDemo](images/expectedoutputefconnectiondemo.png?raw=true)

	_Expected output from the EF connection demo_

<a name="Ex4Task6" />
#### Task 6 - Connecting to SQL Azure via Non-Microsoft Technologies ####

It is trivial to connect to SQL Azure using non-windows technologies.
	
The following PHP version takes on a pattern that you should be familiar with from the previous task. It uses the SQL Server Native Client ODBC driver to establish a connection.

<!-- mark: 1-18 -->
````PHP
<?php
  $host = "server.database.windows.net";
  $dbname = "database";
  $dbuser = "user@server";
  $dbpwd = "password";
  $driver = "{SQL Server Native Client 10.0}";

  // Build connection string
  $dsn = "Driver=$driver;Server=$host;Database=$dbname;Encrypt=true;TrustServerCertificate=true";
  if (!($conn = @odbc_connect($dsn, $dbuser, $dbpwd))) {
	  die("Connection error: " . odbc_errormsg());
  }
	 
  // Got a connection, do what you will
	 
  // Free the connection
	  @odbc_close($conn);
?>
````

Connecting to SQL Azure using JDBC is also trivial. Refer to the following code.

<!-- mark: 1-20 -->
```` JAVA
// Build a connection string
String connectionUrl= "jdbc:sqlserver://server.database.windows.net;" +
		   "database=mydatabase;encrypt=true;user=user@server;password=*****";

// Next, make sure the SQL Server Driver is loaded.
Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
	
// Then try to get a connection. This will null or it will throw an exception if connection cannot be established.

Connection sqlConn = DriverManager.getConnection(connectionUrl);
if (sqlConn == null)
{
	System.out.println("Unable to obtain connection. Exiting...");
	System.exit(1);
}

// Got a connection, do what you will

// Free the connection 
sqlConn.close();
````

---

<a name="Summary" />
## Summary ##

In this lab, you have looked at the basics of working with SQL Azure. If you have any SQL Server experience, you may have found the lab familiar and that is, indeed, the point. Working with SQL Azure should be very familiar to anyone who has worked with SQL Server.

You learned to create new databases, logins and users for those databases. You saw that for the most part, you could simply create objects in SQL Azure as you would with an on-premises SQL Server. 

In addition, you created a simple Windows Azure application that is able to consume a SQL Azure database. 

Finally, you saw that creating connections to SQL Azure using Microsoft technologies is the same as creating connections to any normal on-premises database.