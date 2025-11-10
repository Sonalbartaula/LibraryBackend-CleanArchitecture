# Library Backend - Clean Architecture

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/Sonalbartaula/library-backend-CleanArchitecture.git
   ```
2. Navigate to the project directory:
   ```
   cd library-backend-CleanArchitecture
   ```
3. Install the required dependencies:
   ```
   dotnet restore
   ```
4. Configure the database connection string in the `appsettings.json` file.
5. Apply the database migrations:
   ```
   dotnet ef database update
   ```
6. Build the project:
   ```
   dotnet build
   ```
7. Run the application:
   ```
   dotnet run
   ```

## Usage

The Library Backend API provides the following endpoints:

### Book Management
- `GET /api/Book/Books`: Get a list of all books.
- `POST /api/Book/AddBooks`: Add a new book (requires "Admin" role).
- `PUT /api/Book/UpdateBooks`: Update an existing book (requires "Admin" role).
- `DELETE /api/Book/DeleteBooks/{id}`: Delete a book (requires "Admin" role).
- `GET /api/Book/SearchBooks`: Search for books based on various criteria.

### Student Management
- `GET /api/Student/GetAll`: Get a list of all students.
- `GET /api/Student/{id}`: Get a student by ID.
- `GET /api/Student/Search`: Search for students based on various criteria.
- `POST /api/Student/Add`: Add a new student.
- `PUT /api/Student/Update`: Update an existing student.

### Transaction Management
- `POST /api/Transaction/Checkout`: Checkout a book (requires "Admin" or "Librarian" role).
- `PUT /api/Transaction/Return/{id}`: Return a book (requires "Admin" or "Librarian" role).
- `PUT /api/Transaction/Renew/{id}`: Renew a book loan (requires "Admin" or "Librarian" role).
- `GET /api/Transaction/ActiveLoans`: Get a list of active loans (requires "Admin" or "Librarian" role).
- `GET /api/Transaction/History`: Get the transaction history (requires "Admin" or "Librarian" role).

### Authentication
- `POST /api/Auth/register`: Register a new user.
- `POST /api/Auth/login`: Authenticate a user and get an access token.
- `POST /api/Auth/refresh-token`: Refresh an expired access token.
- `GET /api/Auth/Auth-Endpoint`: Access a protected endpoint (requires authentication).
- `GET /api/Auth/Admin-Endpoint`: Access a protected endpoint (requires "Admin" role).

## API

The Library Backend API is built using ASP.NET Core and follows a clean architecture pattern. The main components are:

- **Controllers**: Responsible for handling HTTP requests and responses.
- **Services**: Contain the business logic for the application.
- **Repositories**: Provide data access and manipulation functionality.
- **Entities**: Represent the data models used in the application.

## Contributing

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and ensure that the tests pass.
4. Submit a pull request with a detailed description of your changes.

## License

This project is licensed under the [MIT License](LICENSE).

## Testing

The project includes unit tests for the various components. To run the tests, use the following command:

```
dotnet test
```

The tests cover the functionality of the controllers, services, and repositories.
