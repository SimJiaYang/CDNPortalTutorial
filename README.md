Markdown
# CDNPortalTutorial

This repository contains the code for a RESTful API built using ASP.NET Core Web API that manages a list of users for a fictional company, CDN (Complete Developer Network). The API provides functionalities for CRUD operations (Create, Read, Update, Delete) on user data.

## Technologies Used

* ASP.NET Core Web API
* Entity Framework Core (demonstrated with in-memory database for assessment purposes)
* C#

## Code Explanation

The code is located in the CDNPortalTutorial project. Key functionalities are within the UserController:

* **User Model:** User class defines the attributes for each user: Name, UserName, Email, PhoneNumber, Skillsets, and Hobby.
* **API Endpoints:**
    * `GET /api/v1/users`: Retrieves a list of all users.
    * `GET /api/v1/users/{id}`: Retrieves a specific user by their ID (GUID).
    * `POST /api/v1/users`: Creates a new user.
    * `PUT /api/v1/users/{id}`: Updates an existing user.
    * `DELETE /api/v1/users/{id}`: Deletes a user.
* **Database Interaction:** (For assessment, an in-memory database is used with ApplicationDbContext). In a real-world scenario, you would configure a connection to a preferred RDBMS like SQL Server, MySQL, etc.
* **Error Handling:** Each API endpoint includes a try-catch block to handle potential exceptions and return appropriate error responses.

## Running the Project

1. Clone this repository.
2. Install dependencies using dotnet restore.
3. Run the application using dotnet run.

**Note:** This is a basic example for assessment purposes. In a production environment, additional configurations like authentication, authorization, and logging would be implemented.

## Requirement Analysis

This project addresses the required functionalities:

* **RESTful API for CRUD operations:** The UserController provides endpoints for GET, POST, PUT, and DELETE requests to manage users.
* **User model attributes:** The User class defines all required attributes.
* **Database connection:** The code demonstrates connection to a database (in-memory for assessment).
