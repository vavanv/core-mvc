# WebMVC - ASP.NET Core MVC Application

A modern ASP.NET Core MVC web application with user authentication, company management, and a clean, responsive interface.

## 🚀 Features

### 🔐 Authentication & Authorization

- **User Registration** - Create new user accounts
- **User Login/Logout** - Secure authentication with cookies
- **Protected Routes** - Menu items and controllers require authentication
- **Password Hashing** - SHA256 password security

### 👥 User Management

- **User CRUD Operations** - Create, Read, Update users
- **User Details** - View comprehensive user information
- **Active/Inactive Status** - Manage user account status
- **Last Login Tracking** - Monitor user activity
- **Read-Only Interface** - No delete functionality for data safety

### 🏢 Company Management

- **Company CRUD Operations** - Create, Read, Update, Delete companies
- **Company Details Modal** - Inline company information display with LLM and Chatbot data
- **Validation** - Duplicate company name prevention
- **Responsive Design** - Works on all devices
- **Simplified Grid Layout** - Clean, focused data display without timestamps

### 🤖 AI Integration (Company Details)

- **LLM Information** - View LLMs associated with each company
- **Chatbot Information** - View chatbots associated with each company
- **Modal Integration** - LLM and Chatbot data displayed in company details modal
- **AJAX Loading** - Dynamic content loading for better performance

### 🎨 User Interface

- **Bootstrap 5** - Modern, responsive design
- **Modal Dialogs** - Inline content display
- **Clean Navigation** - Intuitive menu structure
- **Form Validation** - Client and server-side validation
- **Simplified Layouts** - Focused on essential information

### ⚡ Performance

- **In-Memory Caching** - Fast data retrieval
- **Async/Await** - Non-blocking operations
- **Entity Framework Core** - Efficient data access
- **AJAX Loading** - Dynamic content without page refreshes

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8.0 MVC
- **Database**: SQLite with Entity Framework Core
- **ORM**: Entity Framework Core
- **Authentication**: Cookie-based authentication
- **UI Framework**: Bootstrap 5
- **Caching**: In-Memory Cache
- **Architecture**: Repository Pattern + Service Layer

## 📁 Project Structure

```
WebMVC/
├── Controllers/
│   ├── HomeController.cs
│   ├── AccountController.cs
│   ├── UsersController.cs
│   ├── CompaniesController.cs
│   └── Api/
│       ├── UsersApiController.cs
│       └── CompaniesApiController.cs
├── Models/
│   ├── User.cs
│   ├── Company.cs
│   ├── LLM.cs
│   ├── Chatbot.cs
│   ├── LoginViewModel.cs
│   └── RegisterViewModel.cs
├── Services/
│   ├── IUserService.cs
│   ├── UserService.cs
│   ├── ICompanyService.cs
│   └── CompanyService.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Repositories/
│       ├── IRepository.cs
│       ├── Repository.cs
│       ├── IUserRepository.cs
│       ├── UserRepository.cs
│       ├── ICompanyRepository.cs
│       └── CompanyRepository.cs
├── Views/
│   ├── Home/
│   ├── Account/
│   ├── Users/
│   └── Companies/
│       ├── Index.cshtml
│       ├── _LLMsPartial.cshtml
│       └── _ChatbotsPartial.cshtml
└── wwwroot/
    ├── css/
    ├── js/
    └── lib/
```

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd WebMVC
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Build the application**

   ```bash
   dotnet build
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

5. **Access the application**
   - Open your browser and navigate to `http://localhost:5017`

## 🔧 Configuration

### Database Connection

The application uses SQLite with the connection string defined in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=CoreMVC.db"
  }
}
```

### Authentication Settings

Cookie authentication is configured in `Program.cs` with:

- Login path: `/Account/Login`
- Logout path: `/Account/Logout`
- Access denied path: `/Account/AccessDenied`

## 📋 API Endpoints

### Users API (`/api/UsersApi`)

- `GET /api/UsersApi` - Get all users
- `GET /api/UsersApi/{id}` - Get user by ID
- `POST /api/UsersApi` - Create new user
- `PUT /api/UsersApi/{id}` - Update user
- `DELETE /api/UsersApi/{id}` - Delete user
- `POST /api/UsersApi/authenticate` - Authenticate user

### Companies API (`/api/CompaniesApi`)

- `GET /api/CompaniesApi` - Get all companies
- `GET /api/CompaniesApi/{id}` - Get company by ID
- `POST /api/CompaniesApi` - Create new company
- `PUT /api/CompaniesApi/{id}` - Update company
- `DELETE /api/CompaniesApi/{id}` - Delete company
- `GET /api/CompaniesApi/{id}/llms` - Get LLMs for company
- `GET /api/CompaniesApi/{id}/chatbots` - Get chatbots for company

## 🔐 Security Features

### Authentication

- **Cookie-based authentication** for session management
- **Password hashing** using SHA256
- **Protected routes** with `[Authorize]` attribute
- **CSRF protection** with anti-forgery tokens

### Authorization

- **Public access**: Home page, Login, Register
- **Authenticated access**: Users, Companies management
- **Role-based visibility**: Menu items show only when logged in

## 🎨 User Interface Features

### Navigation

- **Responsive navbar** with Bootstrap 5
- **Dynamic menu items** based on authentication status
- **Clean, modern design** with consistent styling

### Forms

- **Client-side validation** with jQuery validation
- **Server-side validation** with Data Annotations
- **Error handling** with user-friendly messages
- **Form persistence** on validation errors

### Modals

- **Company details modal** for inline information display
- **LLM and Chatbot integration** within company details
- **Bootstrap modal dialogs** for better UX
- **Responsive design** for all screen sizes

### Grid Layouts

- **Simplified data display** - Focus on essential information
- **Clean table design** - Removed timestamp columns for better readability
- **Responsive tables** - Works on all device sizes
- **Modal-based details** - Inline information without page navigation

## 🏗️ Architecture

### Repository Pattern

- **Generic repository** for common CRUD operations
- **Specific repositories** for domain-specific operations
- **Interface-based design** for testability

### Service Layer

- **Business logic encapsulation** in service classes
- **Caching implementation** for performance
- **Data validation** and error handling

### Dependency Injection

- **Service registration** in `Program.cs`
- **Interface-based dependencies** for loose coupling
- **Scoped lifetime** for request-based services

## 🧪 Testing

### Unit Tests

- **UserService tests** with Moq framework
- **xUnit testing framework** for test execution
- **Mocked dependencies** for isolated testing

### Test Structure

```
WebMVC.Tests/
└── UserServiceTests.cs
```

## 📊 Database Schema

### Users Table

- `Id` (Primary Key)
- `Email` (Unique, Required)
- `FirstName` (Required)
- `LastName` (Required)
- `PasswordHash` (Required)
- `IsActive` (Boolean)
- `CreatedAt` (DateTime)
- `LastLoginAt` (DateTime, Nullable)

### Companies Table

- `Id` (Primary Key)
- `Name` (Unique, Required)
- `Description` (Optional)
- `CreatedAt` (DateTime)

### LLMs Table

- `Id` (Primary Key)
- `Name` (Required)
- `Specialization` (Required)
- `CreatedAt` (DateTime)
- `CompanyId` (Foreign Key to Companies)

### Chatbots Table

- `Id` (Primary Key)
- `Name` (Required)
- `CreatedAt` (DateTime)
- `CompanyId` (Foreign Key to Companies)

## 🔄 Data Flow

1. **User Registration** → Password Hashing → Database Storage
2. **User Login** → Authentication → Cookie Creation → Session Management
3. **Data Access** → Repository → Service → Controller → View
4. **Caching** → Memory Cache → Performance Optimization
5. **Company Details** → AJAX Loading → LLM/Chatbot Data → Modal Display

## 🚀 Deployment

### Development

```bash
dotnet run
```

### Production

```bash
dotnet publish -c Release
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📝 License

This project is licensed under the MIT License.

## 🆘 Support

For support and questions, please open an issue in the repository.

---

**Built with ❤️ using ASP.NET Core 8.0**
