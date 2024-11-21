# MyBlogSpace

MyBlogSpace is a web application built using ASP.NET Core MVC that allows users to manage Blogs, Categories, Users, Roles, and Tags. This project demonstrates the implementation of database design, CRUD operations, and MVC architecture, showcasing a comprehensive understanding of the .NET ecosystem.

## Project Features

1. **Database Design**:
   - Database-first approach is used to create entities and DbContext in the `BLL` project's `DAL` folder.

2. **One-to-Many Relationship**:
   - Two entities, `User` and `Blog`, are modeled with a one-to-many relationship.
   - Each blog is associated with a single user, while a user can have multiple blogs.

3. **CRUD Operations**:
   - Full Create, Read, Update, and Delete (CRUD) operations are implemented for `User` and `Blog` entities.

4. **MVC Implementation**:
   - The application uses ASP.NET Core MVC framework.
   - Controllers and views are scaffolded using the entities, and related links are added to the layout view.

5. **Optional Enhancements**:
   - Additional features like GUI customizations are implemented.

## Database Script
The database schema for the project is defined as follows:

```sql
-- Create Role table
CREATE TABLE Role (
    Id INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

-- Create User table
CREATE TABLE User (
    Id INT PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    IsActive BOOLEAN NOT NULL,
    RoleId INT,
    FOREIGN KEY (RoleId) REFERENCES Role(Id)
);

-- Create Blog table
CREATE TABLE Blog (
    Id INT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Content TEXT NOT NULL,
    Rating DECIMAL(3, 2),
    PublishDate DATETIME NOT NULL,
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES User(Id)
);

-- Create Tag table
CREATE TABLE Tag (
    Id INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

-- Create BlogTag table with a separate Id column as primary key
CREATE TABLE BlogTag (
    Id INT PRIMARY KEY,
    BlogId INT,
    TagId INT,
    FOREIGN KEY (BlogId) REFERENCES Blog(Id) ON DELETE CASCADE,
    FOREIGN KEY (TagId) REFERENCES Tag(Id) ON DELETE CASCADE
);
