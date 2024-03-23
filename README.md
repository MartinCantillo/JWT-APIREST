
# JWT .NET Core Project

This project is a simple demonstration of JWT (JSON Web Tokens) implementation in a .NET Core API. It provides endpoints for managing pizzas and user authentication using JWT. Swagger is integrated for easy API documentation and testing.

## Table of Contents

- [Description](#description)
- [Endpoints](#endpoints)
- [Models](#models)
- [Services](#services)
- [Configuration](#configuration)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Description

The project consists of two main controllers: `PizzaController` and `UserController`. The `PizzaController` manages CRUD operations for pizzas, while the `UserController` handles user authentication and generates JWT tokens upon successful login. Swagger UI is integrated to provide interactive API documentation and testing capabilities.

## Endpoints

- **PizzaController**
  - `GET /Pizza/GetAllPizzas`: Get all pizzas.
  - `GET /Pizza/{id}`: Get pizza by ID.
  - `POST /Pizza`: Create a new pizza.
  - `PUT /Pizza/{id}`: Update pizza by ID.
  - `DELETE /Pizza/{id}`: Delete pizza by ID.

- **UserController**
  - `POST /User`: Authenticate user and generate JWT token.

## Models

### Pizza

Represents a pizza entity with the following attributes:
- `Id`: Unique identifier for the pizza.
- `Name`: Name of the pizza.
- `IsGlutenFree`: Indicates whether the pizza is gluten-free.

### User

Represents a user entity with the following attributes:
- `Username`: Username of the user.
- `Password`: Password of the user.

## Services

### PizzaService

A service class responsible for managing pizza data. It provides methods for CRUD operations on pizzas and is used by the `PizzaController`.

## Configuration

The project relies on a configuration file `appsettings.json` to store sensitive information such as the secret key used for JWT token generation. Ensure that this file is properly configured before running the project.

## Getting Started

To get started with this project, follow these steps:
1. Clone the repository to your local machine.
2. Make sure you have .NET Core SDK installed.
3. Configure the `appsettings.json` file with your desired settings.
4. Run the project using `dotnet run` command.
5. Access the Swagger UI at `http://localhost:5069/swagger/index.html` for API documentation and testing.

## Usage

Once the project is running, you can access the API endpoints using Swagger UI or tools like Postman. For user authentication, send a POST request to the `/user` endpoint with valid credentials to receive a JWT token.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvement, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

---

Feel free to further customize the README to include any additional information or instructions specific to your project.
