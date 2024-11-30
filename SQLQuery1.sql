-- Create Role table
CREATE TABLE Role (
    Id INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

-- Create User table
CREATE TABLE [Users] (
    Id INT PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL,
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
    FOREIGN KEY (UserId) REFERENCES [Users](Id)
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