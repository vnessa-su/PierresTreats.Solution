# Pierre's Sweet and Savory Treats

#### Web app to associate treats to flavors in a database with restrictions to certain pages based on logged-in status.

#### By Vanessa Su

## Description

MVC pattern web app that associates different treats with different flavors, and vice versa. There are also orders that are associated with registered users, containing treats and their quantities in the order. All treats/flavors/orders and their associations to one another are stored in a MySQL database. Using EF Core Identity, a user can register an account as well as log in/out. A user that is not logged in can still view the treat and flavor lists, and their details, but can not add, delete, or edit anything on the site. Only users that are logged in can create/delete/edit/view orders.

## User Story

- See a splash page listing all treats and flavors
- Click on an treat or flavor to see its details
- For treats, see a list of flavors that describe it
- For flavors, see a list of treats with that quality
- Click Log In link to Log into an existing account or register a new one
- Click Log Off to log out of an account
- Only be able to do the following if logged in:
  - Add new treats/flavors without needing to associate them with a treats/flavors
  - Edit information of treats/flavors
  - Add and remove treats/flavors from specific treats/flavors
  - Delete treats/flavors
  - See list of logged-in user's orders
  - Add new order to user
  - Edit order delivery date information
  - Add and remove treats from specific orders
  - Delete orders

## Technologies Used

- C#
- ASP.NET&#8203; Core
- Razor
- Entity Framework Core
- MySQL
- VS Code

## Setup/Installation Requirements

### Prerequisites

- [MySQL](https://www.mysql.com/)
- [.NET](https://dotnet.microsoft.com/)
- A text editor like [VS Code](https://code.visualstudio.com/)

### Installation

1. Clone the repository: `git clone https://github.com/vnessa-su/PierresTreats.Solution.git`
2. Navigate to the `/PierresTreats.Solution` directory
3. Open with your preferred text editor to view the code base

- #### Database Setup

1. Navigate to the `/PierresTreats` directory
2. Create appsettings.json file: `touch appsettings.json`
3. Open appsettings.json in a text editor and add in:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=<port number>;Database=vanessa_su;Uid=root;Pwd=<password>;"
  }
}
```

_Replace `<port number>` with the port number the server is running on, default is usually 3306_

_Replace `<password>` with your MySQL password_

5. Save the file and go back to the terminal
6. Run `dotnet ef database update`

- #### Run the Program

1. Navigate to the `/PierresTreats` directory
2. Run `dotnet restore`
3. Run `dotnet build`
4. Start the program with `dotnet run`
5. Open http://localhost:5001/ in your preferred browser

## Known Bugs

- You can add multiple treats and flavors that are named the same
- Adding treats to an order where it already exists only adds to the previous quantity
- You can only delete all of a treat from the order, can't just reduce the quantity

## Contact Information

For any questions or comments, please reach out through GitHub.

## License

[MIT License](license)

Copyright (c) 2021 Vanessa Su
