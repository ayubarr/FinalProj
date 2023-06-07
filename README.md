# FinalProj
Welcome to the project based on MS SQL Server and API! This guide provides instructions for
updating the database and running the API project. Follow these steps to get started.

Prerequisites
Before getting started, make sure the following components are installed on your system:

  * MS SQL Server (version compatible with your project)
  * Development environment or text editor to make changes to the project (e.g., Visual Studio, Visual Studio Code, etc.)
  * .NET Core SDK (the version required to run your API project)

Steps to Update the Database:
  1 Make sure you have a database update script. This script contains the changes that need to be 
  applied to the existing database.If you don't have the script, reach out to the developer or database team to obtain it. 
  2 Launch SQL Server Management Studio (or any similar database administration tool).
  3 Connect to your MS SQL Server database instance.
  4 Open the database update script in SQL Server Management Studio.
  5 Execute the database update script, following the instructions provided in the script. Pay attention to any warnings or errors that may arise during the update process. If you encounter any issues, seek assistance from the developer or database team.
  6 Verify that the database update was successful by ensuring that no errors occurred and the required changes were applied.
 
Running the API Project
  1 Open the project in your chosen development environment or text editor.
  2 Navigate to the file that contains your API project (usually a .csproj file).
  3 Ensure that your configuration file (appsettings.json or appsettings.<Environment>.json)
  contains the correct MS SQL Server database connection parameters. Specify the server name,
  database name, credentials, and any other necessary parameters.
  4 Run the command to restore dependencies and build the project. This is typically done using dotnet
  restore followed by dotnet build. Ensure that dependencies are successfully restored and the project builds without errors.
  5 Run the dotnet run command to launch the API project. Ensure that the project starts successfully 
  and is listening on the port you specified.
 
Using the API Project
  Once the API project is running, you can use it to interact with the database.
  Open your preferred API testing tool (e.g., Postman) and send requests to various endpoints of your API.
  Ensure that the API is functioning correctly and performing the requested operations on the database.

Additional Resources
  * If you encounter any issues or have questions, refer to the MS SQL Server documentation
  and .NET Core documentation for further assistance.
  * Consult your project's documentation and reach out to the developers
  for additional information on specific details and configurations of your project.
  
  Best of luck with your project using MS SQL Server and API! If you have any questions,
  don't hesitate to reach out to the development team or the community for support.
