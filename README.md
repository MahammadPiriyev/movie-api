# Movie API

A robust RESTful API for managing movie collections with advanced features including JWT authentication, TMDB integration, and category-based organization. Built with ASP.NET Core 8.0 and PostgreSQL.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Compatible-blue)](https://www.docker.com/)

## Overview

This project provides a comprehensive REST API for movie management, enabling users to browse, manage, and organize movies with full CRUD operations. The API integrates with The Movie Database (TMDB) API to fetch real-world movie data and supports user authentication via JWT tokens for secure operations.

## Features

- **🔐 JWT Authentication** - Secure user registration, login, and logout using JSON Web Tokens
- **🎬 Movie Management** - Complete CRUD operations for movie records
- **🏆 Category Organization** - Organize movies by categories with one-to-many relationships
- **🌐 TMDB Integration** - Fetch popular movies and detailed information from TMDB API
- **🔍 Advanced Search** - Filter movies by title, author, description, and category
- **📊 Entity Framework Core** - Efficient database operations with ORM support
- **📚 Swagger/OpenAPI** - Auto-generated API documentation with interactive testing
- **🐳 Docker Support** - Containerized deployment with Docker and Docker Compose
- **☸️ Kubernetes Ready** - Deployment configurations for Kubernetes orchestration
- **⚡ Lazy Loading** - Optimized database queries with EF Core proxies

## Technology Stack

### Backend

- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0.11
- **Database**: PostgreSQL 16
- **Authentication**: JWT (JSON Web Tokens) with Identity
- **API Documentation**: Swagger/Swashbuckle

### Infrastructure

- **Containerization**: Docker
- **Orchestration**: Kubernetes
- **Database Driver**: Npgsql Entity Framework Core

## Project Architecture

```
MovieAPI.Domain/
├── Entities/              # Domain models (Movie, Category, TmdbMovie)
├── Identity/              # Authentication models (Login)
└── Services/              # Business logic interfaces and implementations
    ├── Abstract/          # Service interfaces
    └── Concrete/          # Service implementations

MovieAPI.DataAccess/
├── Context/               # ApplicationDbContext
├── Configurations/        # EF Core entity configurations
└── Migrations/            # Database migrations

MovieAPI.Infrastructure/
└── Repository/            # Data access layer with Unit of Work pattern
    ├── BaseRepository
    ├── MovieRepository
    ├── CategoryRepository
    └── UnitOfWork

MovieAPI/
├── Controllers/           # API endpoints
├── Program.cs             # Application configuration
└── Properties/            # Launch settings
```

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- PostgreSQL 16 or Docker
- Visual Studio 2022, Visual Studio Code, or Rider (recommended)
- Git

## Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/mahammadpiriyev/movie-api.git
cd movie-api
```

### 2. Configure Environment Variables

Create a `.env` file in the project root with the following variables:

```env
# Database Configuration
DB_USER=movieapi_user
DB_PASSWORD=secure_password_here
DB_NAME=movieapi_db
DB_CONNECTION_STRING=Server=localhost;Port=5432;Database=movieapi_db;User Id=movieapi_user;Password=secure_password_here;

# JWT Configuration
JWT__KEY=your_long_secure_jwt_key_min_32_chars
JWT__ISSUER=MovieAPI
JWT__AUDIENCE=MovieAPIClient

# TMDB API (optional for TMDB features)
TMDB_API_KEY=your_tmdb_api_key
```

### 3. Restore Dependencies

```bash
dotnet restore
```

## Running the Application

### Option A: Local Development

1. **Update Connection String**

   Update `appsettings.json` in the MovieAPI project with your PostgreSQL connection details.

2. **Run Database Migrations**

   ```bash
   cd MovieAPI.DataAccess
   dotnet ef database update --project ../MovieAPI
   ```

3. **Start the Application**

   ```bash
   cd MovieAPI
   dotnet run
   ```

   The application will start on:
   - HTTP: `http://localhost:5226`
   - HTTPS: `https://localhost:7191`
   - Swagger UI: `https://localhost:7191/swagger/index.html`

### Option B: Docker Compose

1. **Build and Run with Docker Compose**

   ```bash
   docker-compose up -d
   ```

   The API will be available at `http://localhost:8081`

2. **Verify Services**

   ```bash
   docker-compose ps
   ```

3. **Stop Services**

   ```bash
   docker-compose down
   ```

### Option C: Kubernetes Deployment

Deploy using the provided Kubernetes manifests:

```bash
kubectl apply -f k8s/postgres/
kubectl apply -f k8s/api/
```

## API Documentation

### Base URL

- **Local**: `http://localhost:5226/api`
- **Docker**: `http://localhost:8081/api`

### Authentication Endpoints

#### Register User

```
POST /api/auth/register
Content-Type: application/json

{
  "username": "user@example.com",
  "password": "securePassword123"
}

Response:
{
  "message": "New user has successfully created!"
}
```

#### Login

```
POST /api/auth/login
Content-Type: application/json

{
  "username": "user@example.com",
  "password": "securePassword123"
}

Response:
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

#### Logout

```
POST /api/auth/logout
Authorization: Bearer {token}

Response:
{
  "message": "Logged out successfully"
}
```

### Movie Endpoints

All movie endpoints require JWT authentication.

#### Get All Movies

```
GET /api/movie
Authorization: Bearer {token}

Response:
[
  {
    "id": 1,
    "title": "Movie Title",
    "description": "Movie description",
    "author": "Director Name",
    "categoryId": 1,
    "rating": 4,
    "createdDate": "2026-03-05T06:59:33",
    "updatedDate": "2026-03-05T06:59:33"
  }
]
```

#### Get Movie by ID

```
GET /api/movie/{id}
Authorization: Bearer {token}

Response:
{
  "id": 1,
  "title": "Movie Title",
  "description": "Movie description",
  "author": "Director Name",
  "categoryId": 1,
  "rating": 4,
  "createdDate": "2026-03-05T06:59:33",
  "updatedDate": "2026-03-05T06:59:33"
}
```

#### Search Movies

```
GET /api/movie/search?filter={filter}
Authorization: Bearer {token}

Example filter:
{
  "title": "Inception",
  "categoryId": 1
}
```

#### Create Movie

```
POST /api/movie/add
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "New Movie",
  "description": "Movie description",
  "author": "Director Name",
  "categoryId": 1,
  "rating": 5
}

Response:
{
  "message": "Movie has successfully created!"
}
```

#### Update Movie

```
PUT /api/movie/update/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Updated Title",
  "description": "Updated description",
  "author": "Director Name",
  "categoryId": 1,
  "rating": 5
}
```

#### Delete Movie

```
DELETE /api/movie/remove/{id}
Authorization: Bearer {token}
```

### TMDB Integration Endpoints

#### Get Popular Movies

```
GET /api/tmdb/popular

Response:
{
  "results": [
    {
      "id": 1,
      "title": "Popular Movie",
      "overview": "Movie synopsis",
      "poster_path": "/path/to/poster.jpg",
      "release_date": "2026-01-01"
    }
  ]
}
```

#### Get Movie Details from TMDB

```
GET /api/tmdb/{movieId}

Response:
{
  "id": 550,
  "title": "Fight Club",
  "overview": "An insomniac office worker...",
  "runtime": 139,
  "release_date": "1999-10-15",
  "vote_average": 8.8
}
```

## Database Schema

### Movie Table

| Column      | Type         | Constraints        |
| ----------- | ------------ | ------------------ |
| Id          | INT          | PK, Auto-increment |
| Title       | VARCHAR(255) | Nullable           |
| Description | TEXT         | Nullable           |
| Author      | VARCHAR(255) | Nullable           |
| CategoryId  | INT          | FK to Category     |
| Rating      | INT          | Range(1-5)         |
| CreatedDate | DATETIME     | Default: NOW()     |
| UpdatedDate | DATETIME     | Default: NOW()     |

### Category Table

| Column     | Type         | Constraints        |
| ---------- | ------------ | ------------------ |
| CategoryId | INT          | PK, Auto-increment |
| Name       | VARCHAR(255) | Required           |

## Configuration

### JWT Settings

Update the JWT settings in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your_secret_key_must_be_at_least_32_characters_long",
    "Issuer": "MovieAPI",
    "Audience": "MovieAPIClient"
  }
}
```

### Database Connection

PostgreSQL connection string format:

```
Server=localhost;Port=5432;Database=movieapi_db;User Id=username;Password=password;
```

### Password Policy

Default password requirements:

- Minimum length: 5 characters

To modify, update `Program.cs`:

```csharp
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5; // Adjust as needed
})
```

## Development

### Building the Solution

```bash
dotnet build MovieAPI.sln
```

### Running Tests

```bash
dotnet test
```

### Database Migrations

Create a new migration:

```bash
dotnet ef migrations add MigrationName --project MovieAPI.DataAccess --startup-project MovieAPI
```

Apply migrations:

```bash
dotnet ef database update --project MovieAPI.DataAccess --startup-project MovieAPI
```

## Docker Deployment

### Build Docker Image

```bash
docker build -t movie-api:latest .
```

### Run with Docker Compose

```bash
docker-compose up -d
```

### View Logs

```bash
docker-compose logs -f movieapi
```

### Stop Services

```bash
docker-compose down -v  # -v removes volumes
```

## Kubernetes Deployment

### Prerequisites

- kubectl installed and configured
- Kubernetes cluster (local or cloud)

### Deploy PostgreSQL

```bash
kubectl apply -f k8s/postgres/deployment.yaml
kubectl apply -f k8s/postgres/service.yaml
```

### Deploy API

```bash
kubectl apply -f k8s/api/deployment.yaml
kubectl apply -f k8s/api/service.yaml
```

### Verify Deployment

```bash
kubectl get pods
kubectl get svc
```

### Port Forward (Local Testing)

```bash
kubectl port-forward svc/movieapi 8081:80
```

## Error Handling

The API returns structured error responses:

```json
{
  "message": "Error description"
}
```

Common HTTP Status Codes:

- `200 OK` - Successful request
- `400 Bad Request` - Invalid input or validation error
- `401 Unauthorized` - Missing or invalid token
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

## Security Considerations

- **JWT Tokens**: Implement token refresh mechanisms for production
- **HTTPS**: Always use HTTPS in production environments
- **Password**: Store strong passwords; never commit secrets to version control
- **CORS**: Configure CORS policies as needed
- **SQL Injection**: Entity Framework Core protects against SQL injection
- **Rate Limiting**: Consider implementing rate limiting for production deployments

## Troubleshooting

### Database Connection Issues

1. Verify PostgreSQL is running
2. Check connection string in `.env` or `appsettings.json`
3. Ensure database user has proper permissions

### JWT Token Errors

1. Verify JWT key is at least 32 characters
2. Check token hasn't expired
3. Ensure token is sent in Authorization header: `Authorization: Bearer {token}`

### Migration Issues

```bash
# Reset database (development only!)
dotnet ef database drop --project MovieAPI.DataAccess --startup-project MovieAPI
dotnet ef database update --project MovieAPI.DataAccess --startup-project MovieAPI
```

## Performance Optimization

- Entity Framework Core uses lazy loading proxies for efficient data retrieval
- Swagger/Swagger UI is disabled in production (appsettings.json)
- Consider implementing caching for TMDB API calls
- Database indexes should be added for frequently queried columns

## Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/AmazingFeature`
3. Commit changes: `git commit -m 'Add AmazingFeature'`
4. Push to the branch: `git push origin feature/AmazingFeature`
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact & Support

For issues, questions, or suggestions, please create an issue in the GitHub repository or contact the project maintainers.

---

**Last Updated**: May 2026  
**API Version**: 1.0.0

1. 🌀 Clone the repository:

   ```bash
   git clone https://github.com/MahammadPiriyev/InternIntelligence_MovieAPI.git
   ```

2. 📂 Navigate to the project directory:

   ```bash
   cd MovieAPI
   ```

3. 🔧 Restore the required packages:

   ```bash
   dotnet restore
   ```

4. 📝 Update the connection string in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=SchoolManagement;Trusted_Connection=True;TrustServerCertificate=True"
   }
   ```

5. 📦 Apply migrations and seed the database:

   ```bash
   dotnet ef database update
   ```

6. ▶️ Run the application:
   ```bash
   dotnet run
   ```

## API Endpoints (Authentication)

### 1. **POST /api/auth/register**

- Register a new user.
- Request body:
  ```
  {
    "username": "exampleuser",
    "email": "example@example.com",
    "password": "YourPassword123"
  }
  ```

### 2. **POST /api/auth/login**

- Login an existing user and receive a JWT token.
- Request body:

  ```
  {
    "username": "exampleuser",
    "password": "YourPassword123"
  }
  ```

- Response body:
  ```
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
  ```

### 3. **POST /api/auth/logout**

- Logout the authenticated user and invalidate the JWT token.
- Requires a valid JWT token in the Authorization header.

## API Endpoints (CRUD)

### Create Movie 🍿

```bash
POST /api/movie/add
{
    "id": 1,
    "title": "Inception",
    "description": "Good Film",
    "author": "Christopher Nolan",
    "categoryId": 1,
    "rating": 5,
    "createdDate": "2025-01-02T18:31:21.961",
    "updatedDate": "2025-01-02T18:31:21.961"
}
```

### Get All Movies 🎥

```bash
GET /api/movie
```

### Get Movie by ID 🎬

```bash
GET /api/movie/1
```

### Update Movie 🔄

```bash
PUT /api/movie/update/1
{
    "id": 0,
    "title": "string",
    "description": "string",
    "author": "string",
    "categoryId": 0,
    "rating": 5,
    "createdDate": "2025-01-02T18:31:21.961",
    "updatedDate": "2025-01-02T18:31:21.961"
}
```

### Delete Movie 🗑️

```bash
DELETE /api/movie/remove/1
```

## API Endpoints (TMDB)

### Get Movie by ID 🎬

```bash
GET /api/tmdb/{movieId}
{
    "id": 1156593,
    "title": "Your Fault",
    "original_language": "es",
    "release_date": "2024-12-26T00:00:00"
}
```

### Get Popular Movies 🎥

```bash
GET /api/tmdb/popular
[
  {
    "id": 558449,
    "title": "Gladiator II",
    "original_language": "en",
    "release_date": "2024-11-05T00:00:00"
  },
  {
    "id": 1156593,
    "title": "Your Fault",
    "original_language": "es",
    "release_date": "2024-12-26T00:00:00"
  },
  {
    "id": 845781,
    "title": "Red One",
    "original_language": "en",
    "release_date": "2024-10-31T00:00:00"
  }
]
```

## 👤 Author

**Mahammad Piriyev**

- LinkedIn: [My LinkedIn Profile](https://linkedin.com/in/mahammadpiriyev)
- Portfolio: [My Portfolio Website](https://mahammad.dev/)
