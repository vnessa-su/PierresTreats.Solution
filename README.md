# _{Application Name}_

#### _{Brief description of application},{Date of current version}_

#### To see my live website go to {GH_PAGES_LINK_HERE}!

#### By _**{List of contributors}**_

## Description

_{This is a detailed description of your application. Give as much detail as needed to explain what the application does as well as any other information you want users or other developers to have.}_

## User Story

* _{As a user I want to be able to...}_

## Technologies Used

* C#
* ASP.NET&#8203; Core
* Razor
* Entity Framework Core
* MySQL
* MSTest
* VS Code

## Setup/Installation Requirements

### Prerequisites
* [MySQL](https://www.mysql.com/)
* [.NET](https://dotnet.microsoft.com/)
* A text editor like [VS Code](https://code.visualstudio.com/)

### Installation
1. Clone the repository: `git clone {PUT_REPO_HERE}`
2. Navigate to the `\{NAME_OF_DIRECTORY}` directory
3. Open with your preferred text editor to view the code base
* #### Database Setup
1. Log into your MySQL server using `mysql -u root -p`
2. Enter in password when prompted
3. Create database: `mysql> CREATE DATABASE {PROJECT_DATABASE};`
4. Select database: `mysql> USE {PROJECT_DATABASE};`
5. Check that {PROJECT_DATABASE} is selected: `mysql> SELECT DATABASE();`
6. Load tables from provided file: `mysql> source {FILENAME}.sql;`
7. Check that {NAME_OF_TABLES} tables were loaded: `mysql> SHOW TABLES;`
8. Leave the MySQL server running in this terminal
* #### Run the Program
1. Open a new terminal
1. Navigate to the `\{NAME_OF_DIRECTORY}` directory
2. Create appsettings.json file: `touch appsettings.json`
3. Open appsettings.json in a text editor and add in:
```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=<port number>;Database={PROJECT_DATABASE};Uid=root;Pwd=<password>;"
  }
}
```
  * Replace `<port number>` with the port number the server is running on, default is usually 3306
  * Replace `<password>` with your MySQL password
5. Save the file and go back to the terminal
6. Run `dotnet restore`
7. Run `dotnet build`
8. Start the program with `dotnet run`
9. Open http://localhost:5000/ in your preferred browser
* #### Run the Tests
1. Navigate to the `\{NAME_OF_DIRECTORY}` directory
2. Run `dotnet restore`
3. Start the tests with `dotnet test`

## Known Bugs

_No known bugs_

## Contact Information

For any questions or comments, please reach out through GitHub.

## License

[MIT License](license)

Copyright (c) [year] [names]