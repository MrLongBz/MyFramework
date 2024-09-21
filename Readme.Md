# MyF Project

MyF is a modular .NET 8 application that demonstrates a clean architecture approach for building scalable and maintainable web applications.

## Project Structure

The solution is divided into several projects:

- **MyF.Api**: The main entry point of the application, containing controllers and API endpoints.
- **MyF.Entities**: Contains all the entity models and DTOs used across the application.
- **MyF.Infrastructure**: Handles cross-cutting concerns like data access, logging, and mapping.
- **MyF.Services**: Contains the business logic and service implementations.

## Key Features

- Clean Architecture: Separation of concerns with clear boundaries between layers.
- Entity Framework Core with SQL Server: For data persistence and management.
- SqlSugar ORM: Provides a flexible and efficient way to interact with the database.
- Lightweight Mapping: Custom AutoMapper for simple object-to-object mapping.
- User Management: Registration, authentication, and user data management.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (or SQL Server Express)
- Visual Studio 2022 or JetBrains Rider (recommended)

### Setup

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/MyF.git
   ```

2. Navigate to the project directory:
   ```
   cd MyF
   ```

3. Update the connection string in `appsettings.json` in the MyF.Api project to point to your SQL Server instance.

4. Run the following commands to set up the database:
   ```
   dotnet ef database update
   ```

5. Run the application:
   ```
   dotnet run --project MyF.Api
   ```

## API Endpoints

- `GET /User`: Retrieve all users
- `POST /User/register`: Register a new user
- `POST /User/login`: Authenticate a user

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.