# TO-DO Web API

This is a .NET 6 web API project for managing to-dos, including users, to-do items, and subtasks. The project is designed to provide CRUD functionalities for to-dos and subtasks, with authentication and authorization features using JWT tokens.

## Table of Contents
- [Technical Infrastructure](#technical-infrastructure)
- [Data Structures](#data-structures)
- [Acceptance Criteria](#acceptance-criteria)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Technical Infrastructure

The project is built on .NET 6 and follows a layered architecture, adhering to the principles of Onion or Clean architecture. Each layer has its own set of responsibilities:

- **Web API Layer**: This layer handles incoming HTTP requests and responses. It contains controllers for managing user registration, authentication, and CRUD operations for to-dos and subtasks. Middleware is implemented for culture (optional), global exception handling, and request-response logging.

- **Core Layer**: This layer contains the main business logic of the application. It includes services responsible for processing user requests, applying business rules, and interacting with the data access layer.

- **Data Access Layer**: This layer manages data storage and access. It includes data structures for users, to-dos, and subtasks, as well as repositories for interacting with the database.

## Data Structures

The following data structures are used in the project:

- **User**: Represents a registered user with a username and password hash.
- **To-Do**: Represents a to-do item with a title, status, target completion date, and a list of subtasks. Each to-do is associated with an owner (user).
- **Subtask**: Represents a subtask of a to-do item, with a title and a reference to the parent to-do item.
- **BaseEntity / IEntity**: Represents common properties shared by all entities, including an ID, creation date, modification date, and status.

## Acceptance Criteria

The project fulfills the following acceptance criteria:

- User registration: Users can register by sending a POST request with a username and password.
- User authentication: Users can log in to obtain a JWT token by sending a POST request with their username and password.
- CRUD operations for to-do items: Users can create, retrieve, update, and delete their to-do items.
- Filtering by to-do status: Users can filter their to-do items by status, excluding deleted to-dos.
- CRUD operations for subtasks: Users can create, retrieve, update, and delete subtasks associated with their to-do items.

## Getting Started

To run the project locally, follow these steps:

1. Clone this repository.
2. Open the solution in Visual Studio or your preferred IDE.
3. Build the solution.
4. Run the application.

## Usage

Once the application is running, users can interact with the API endpoints using tools like Postman or Swagger documentation.

## Contributing

Contributions are welcome! If you'd like to contribute to this project, please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. Feel free to use and modify it according to your needs.
