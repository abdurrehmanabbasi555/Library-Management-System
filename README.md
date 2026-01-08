# 📚 Library Management System

A clean and simple web-based Library Management System built with ASP.NET Core MVC to manage books and members with full CRUD operations, borrow/return tracking, and detailed borrow history. Features a fast, user-friendly UI with search and pagination for a smooth experience.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)

## 🎯 Overview

A modern, responsive web application designed to streamline library operations. Built with ASP.NET Core MVC, this system enables efficient management of books, members, and borrowing transactions through an intuitive interface that works seamlessly across all devices.

## ✨ Features

### 📖 Book Management
- ✅ Complete CRUD operations (Create, Read, Update, Delete)
- ✅ Add new books with detailed information (Title, Author, Category, Quantity)
- ✅ Update existing book records
- ✅ Delete books from inventory
- ✅ View complete book catalog with availability status
- ✅ Search functionality for quick book lookup
- ✅ Track available copies in real-time

### 👥 Member Management
- ✅ Register new library members
- ✅ Full CRUD operations for member records
- ✅ Update member information (Name, Email, Phone, Address)
- ✅ View all registered members in organized lists
- ✅ Search members by name or ID
- ✅ Track member borrowing activity
- ✅ Member status management

### 📅 Borrow/Return System
- ✅ Issue books to members with automatic date tracking
- ✅ Return books with real-time status updates
- ✅ Due date calculation and management
- ✅ Overdue book detection
- ✅ Automatic inventory updates on borrow/return
- ✅ Prevent borrowing unavailable books
- ✅ Transaction validation and error handling

### 📊 Borrow History
- ✅ Complete transaction history with detailed records
- ✅ Filter by member, book, or date range
- ✅ View borrowed, returned, and overdue items
- ✅ Detailed transaction information with timestamps
- ✅ Search through historical data
- ✅ Pagination for efficient data browsing

### 🎨 User Interface
- ✅ Clean, modern, and intuitive design
- ✅ Responsive layout (mobile, tablet, desktop)
- ✅ Fast page load times
- ✅ Smooth navigation between modules
- ✅ Form validation with helpful error messages
- ✅ Pagination for better performance with large datasets
- ✅ Confirmation dialogs for critical actions

## 🛠️ Technologies Used

**Backend:**
- ASP.NET Core MVC
- C# 
- Entity Framework Core (Code-First approach)
- LINQ for data queries

**Frontend:**
- HTML5
- CSS3
- JavaScript (ES6+)
- CSS

**Database:**
- Microsoft SQL Server
- Entity Framework Core for ORM

**Development Tools:**
- Visual Studio 2022
- SQL Server Management Studio (SSMS)
- Git for version control

## 📋 Prerequisites

Before running this project, ensure you have:

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express/LocalDB/Full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (recommended)

## 🚀 Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/abdurrehmanabbasi555/Library-Management-System.git
cd Library-Management-System
```

### 2. Configure Database Connection

Open `appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

**For SQL Server Express:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=LibraryManagementDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Restore NuGet Packages

```bash
dotnet restore
```

### 4. Apply Database Migrations

```bash
# Create the database and apply migrations
dotnet ef database update
```

If you encounter issues, you can create a new migration:

```bash
# Create a new migration
dotnet ef migrations add InitialCreate

# Apply the migration
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

Or press `F5` in Visual Studio to run with debugging.

### 6. Access the Application

Open your browser and navigate to:
- **HTTPS:** `https://localhost:5001`
- **HTTP:** `http://localhost:5000`

(Check your console output for the exact ports)

## 📁 Project Structure

```
Library-Management-System/
│
├── Controllers/
│   ├── HomeController.cs           # Home page and dashboard
│   ├── BooksController.cs          # Book management operations
│   ├── MembersController.cs        # Member management operations
│   ├── BorrowController.cs         # Borrow/return operations
│   └── HistoryController.cs        # Borrow history
│
├── Models/
│   ├── Book.cs                     # Book entity
│   ├── Member.cs                   # Member entity
│   ├── BorrowTransaction.cs        # Transaction entity
│   └── ViewModels/                 # View models for complex views
│
├── Views/
│   ├── Home/
│   │   └── Index.cshtml            # Dashboard/home page
│   ├── Books/
│   │   ├── Index.cshtml            # List all books
│   │   ├── Create.cshtml           # Add new book
│   │   ├── Edit.cshtml             # Edit book
│   │   ├── Details.cshtml          # Book details
│   │   └── Delete.cshtml           # Delete confirmation
│   ├── Members/
│   │   ├── Index.cshtml            # List all members
│   │   ├── Create.cshtml           # Register member
│   │   ├── Edit.cshtml             # Edit member
│   │   └── Details.cshtml          # Member details
│   ├── Borrow/
│   │   ├── Index.cshtml            # Active borrows
│   │   ├── Issue.cshtml            # Issue book
│   │   └── Return.cshtml           # Return book
│   ├── History/
│   │   └── Index.cshtml            # Borrow history
│   └── Shared/
│       ├── _Layout.cshtml          # Main layout
│       ├── _ValidationScripts.cshtml
│       └── Error.cshtml
│
├── Data/
│   └── ApplicationDbContext.cs     # EF Core DbContext
│
├── Migrations/                     # EF Core migrations
│
├── wwwroot/
│   ├── css/
│   │   └── site.css                # Custom styles
│   ├── js/
│   │   └── site.js                 # Custom JavaScript
│   └── lib/                        # Client-side libraries
│
├── appsettings.json                # Configuration
├── Program.cs                      # Application entry point
└── README.md                       # This file
```

## 💻 Usage Guide

### Dashboard
- View quick statistics (Total Books, Available Books, Total Members, Active Borrows)
- Quick navigation to all modules

### Managing Books

**Add a Book:**
1. Navigate to "Books" → "Add New"
2. Fill in book details (Title, Author, ISBN, Category, Quantity)
3. Click "Save"

**View All Books:**
- Go to "Books" to see the complete catalog
- Use search to find specific books
- Click on a book to view details

**Edit a Book:**
1. Click "Edit" on any book in the list
2. Update the information
3. Click "Update"

**Delete a Book:**
1. Click "Delete" on the book
2. Confirm deletion

### Managing Members

**Register a Member:**
1. Go to "Members" → "Register New"
2. Enter member details (Name, Email, Phone, Address)
3. Click "Save"

**View All Members:**
- Navigate to "Members" to see all registered members
- Search by name or ID
- Click on a member to view their details and history

### Borrowing & Returning Books

**Issue a Book:**
1. Go to "Borrow" → "Issue Book"
2. Select a member from the dropdown
3. Select an available book
4. Set due date (default: 14 days)
5. Click "Issue"

**Return a Book:**
1. Go to "Borrow" → "Return Book"
2. Search for the transaction
3. Click "Return"
4. Confirm the return

**View Active Borrows:**
- Navigate to "Borrow" to see all currently borrowed books
- Check due dates
- Identify overdue books

### Viewing History

**Access History:**
1. Go to "History"
2. View complete transaction log with pagination

**Filter & Search:**
- Use filters to narrow down results by:
  - Member name
  - Book title
  - Date range
  - Transaction status
- Search across all fields

## 🗄️ Database Schema

### Books Table
```sql
- BookId (INT, Primary Key, Identity)
- Title (NVARCHAR(200), Required)
- Author (NVARCHAR(100), Required)
- Category (NVARCHAR(50))
- Quantity (INT, Required)
- AvailableQuantity (INT, Required)
- CreatedDate (DATETIME)
- UpdatedDate (DATETIME)
```

### Members Table
```sql
- MemberId (INT, Primary Key, Identity)
- FirstName (NVARCHAR(50), Required)
- LastName (NVARCHAR(50), Required)
- Email (NVARCHAR(100), Unique)
- Phone (NVARCHAR(20))
- Address (NVARCHAR(200))
- JoinDate (DATETIME)
- Status (NVARCHAR(20))
- CreatedDate (DATETIME)
```

### BorrowTransactions Table
```sql
- TransactionId (INT, Primary Key, Identity)
- BookId (INT, Foreign Key)
- MemberId (INT, Foreign Key)
- BorrowDate (DATETIME, Required)
- DueDate (DATETIME, Required)
- ReturnDate (DATETIME, Nullable)
- Status (NVARCHAR(20))
- CreatedDate (DATETIME)
```

## 🔑 Key Features Explained

### Pagination
- Displays 10-20 records per page for optimal performance
- Smooth page navigation
- Maintains search/filter state across pages

### Search Functionality
- Real-time search across book titles, authors, ISBNs
- Member search by name, email, or ID
- Case-insensitive search
- Instant results

### Validation
- Client-side validation using JavaScript
- Server-side validation with Data Annotations
- User-friendly error messages
- Required field indicators

### Responsive Design
- Mobile-first approach
- Works seamlessly on phones, tablets, and desktops
- Touch-friendly interface
- Adaptive layouts

## 🐛 Troubleshooting

**Database Connection Issues:**
```bash
# Check if SQL Server is running
# Update connection string in appsettings.json
# Try recreating the database
dotnet ef database drop
dotnet ef database update
```

**Migration Issues:**
```bash
# Remove all migrations
dotnet ef migrations remove

# Create fresh migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

**Port Already in Use:**
```bash
# Change ports in Properties/launchSettings.json
# Or find and kill the process using the port
netstat -ano | findstr :5000
taskkill /PID <process_id> /F
```

**Package Restore Fails:**
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore
```

## 🚀 Future Enhancements

- [ ] User authentication and authorization
- [ ] Email notifications for due dates
- [ ] Fine calculation for overdue books
- [ ] Book reservation system
- [ ] Export reports to PDF/Excel
- [ ] Dashboard with charts and analytics
- [ ] Book cover images
- [ ] Advanced search filters
- [ ] API endpoints for mobile app
- [ ] Dark mode theme

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👨‍💻 Author

**Abdurrehman Abbasi**

- GitHub: [@abdurrehmanabbasi555](https://github.com/abdurrehmanabbasi555)
- LinkedIn: [My LinkedIn Profile](https://www.linkedin.com/in/abdurrehman-abbasi-173217226/)
- Email: [My Email](mailto:abbasiabdurrehman555@gmail.com)

## 🙏 Acknowledgments

- Built with ASP.NET Core MVC
- Inspired by real-world library management needs
- Thanks to the open-source community

---

<div align="center">

**⭐ If you find this project useful, please give it a star! ⭐**

**Made with ❤️ using ASP.NET Core MVC**

</div>
