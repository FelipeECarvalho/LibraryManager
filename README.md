# LibraryManager

This is a modern, robust, and scalable application built with **.NET 8**, using the most advanced software architecture practices to ensure high-quality, maintainable, and performant code.

## 🏛️ Architecture and Design

The project's foundation was meticulously planned to ensure a clean structure and a design that reflects the best market practices.

### **Clean Architecture**
The application strictly follows the principles of **Clean Architecture**, ensuring a clear separation of responsibilities among the layers (Core, Application, Infrastructure, Persistence, Presentation). Communication between layers is done through interfaces, promoting low coupling and facilitating testability and maintenance.

### **Domain-Driven Design (DDD)**
The heart of the application is its domain, modeled with the best DDD practices:
- **Value Objects**: To represent immutable concepts with intrinsic validations.
- **Organizational Patterns**: The domain code is highly organized, faithfully reflecting the business rules.
- **Domain Errors**: Centralization and standardization of domain error messages to ensure consistency and clarity.

---
## ✨ Main Features

### **Well-Documented RESTful API**
The API was built following the REST pattern and is fully documented with **Swagger (OpenAPI)**.
- **Pagination**: To optimize performance in queries that return large volumes of data.
- **Standardized Returns**: Use of `ProblemDetails` and a well-defined `Result Pattern` to standardize success and error responses, making the API predictable and easy to consume.

### **Authentication and Authorization with JWT**
A robust security system using **JSON Web Tokens (JWT)**.
- **Refresh Tokens**: Implementation of refresh tokens to keep sessions active securely.
- **Secure Passwords**: Generation and verification of passwords using modern hashing algorithms to ensure the security of credentials.

### **CQRS with Mediator**
The **CQRS (Command Query Responsibility Segregation)** pattern was implemented with the **MediatR** library, separating write operations (Commands) from read operations (Queries).
- **Advanced Behaviors**: The MediatR pipeline has been enriched with behaviors for:
    - **Logging**: Complete traceability of execution.
    - **Validation**: Integration with **FluentValidator** for automatic validations.
    - **Transactional Resilience**: Use of Polly policies to ensure consistency.

### **Resilient Background Jobs**
One of the application's strongest points is its background task system.
- **Queues and Schedules**: Support for queued jobs and scheduled jobs.
- **High Resilience**: The jobs were built to be extremely resilient, with **automatic rescheduling in case of failures**, ensuring that critical tasks are executed.

### **Background Email Sending**
- **FluentEmail**: Used for a fluent and clean construction of emails via SMTP.
- **Email Queues**: The sending process is decoupled from the main request. Emails are queued in the database and processed by a background job, ensuring the user experience is not impacted.

---
## 🔧 Code Quality and Patterns

### **Dependency Injection (IoC)**
The application makes exemplary use of the native .NET Dependency Injection container. **All services, repositories, and other abstractions are registered and resolved via IoC**, resulting in decoupled and highly testable code. The configuration of the infrastructure layer services is an excellent example of the mature application of this pattern.

### **Result Pattern for Cleaner Code**
A major differentiator of this project is the adoption of the **Result Pattern**. This pattern is used for the return of service methods, **avoiding the throwing of exceptions for flow control**. This results in clearer, more predictable code with an explicit distinction between success and failure, drastically improving readability.

### **Resilience with Polly**
Resilience is a pillar of this application. The **Polly** library is widely used to create `Retry` policies, with an emphasis on:
- **Resilient Database Transactions**: Ensuring that critical operations are completed even under adverse conditions.
- **Communication with External Services**.

### **Centralized Exception Handling**
- **Global Exception Handlers**: Middlewares for centralized exception handling, converting unexpected and validation errors into standardized `ProblemDetails` responses.

### **Structured Logging with Serilog**
The entire application is instrumented with structured logging using **Serilog**, generating detailed logs for requests, use case execution, errors, and background jobs.

### **Data Access with EF Core**
- **Repository Pattern & Unit of Work**: Abstraction of the data layer to encapsulate query logic and ensure the atomicity of transactions with the Unit of Work.
- **Best Practices**: The EF Core entities were configured following best practices for performance and maintainability.

### **Configuration with Options Pattern**
The application's settings are strongly typed using the **Options Pattern**, making access to them safe and organized.

---
## 🚀 Technologies Used

- **Framework**: .NET 8
- **Database**: Microsoft SQL Server
- **Containerization**: Docker and Docker Compose
- **Main Libraries**:
  - MediatR
  - FluentValidation
  - Serilog
  - Quartz
  - Polly
  - Entity Framework Core 8
  - FluentEmail
  - JWT Bearer Authentication
  - HybridCache

---
## 🏁 How to Run the Project

1. **Prerequisites**:
   - .NET 8 SDK
   - Docker Desktop

2. **Configuration**:
   - Clone the repository.
   - Navigate to the project folder.
   - Configure your environment variables or the `appsettings.Development.json` file with the necessary credentials.

3. **Running with Docker Compose** (Recommended):
   - In the project root, run the command below to start the API and the database:
     ```bash
     docker-compose up -d
     ```

4. **Running Locally**:
   - Run the Entity Framework migrations:
     ```bash
     dotnet ef database update
     ```
   - Start the application:
     ```bash
     dotnet run --project LibraryManager.API
     ```

The API will be available at `http://localhost:5001` and the Swagger documentation at `http://localhost:5001/swagger`.
