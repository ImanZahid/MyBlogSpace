﻿USE OnlineBlogsDB;

-- Create Role table
CREATE TABLE Role (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL UNIQUE
);

-- Create Users table with UNIQUE constraint on UserName
CREATE TABLE [Users] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Role(Id)
);

-- Create Blog table
CREATE TABLE Blog (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Content TEXT NOT NULL,
    Rating DECIMAL(4, 2),
    PublishDate DATETIME NOT NULL,
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES [Users](Id)
);

-- Create Tag table
CREATE TABLE Tag (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

-- Create BlogTag table with a separate Id column as primary key
CREATE TABLE BlogTag (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BlogId INT NOT NULL,
    TagId INT NOT NULL,
    FOREIGN KEY (BlogId) REFERENCES Blog(Id) ON DELETE CASCADE,
    FOREIGN KEY (TagId) REFERENCES Tag(Id) ON DELETE CASCADE
);

-- Insert 'admin' role into Role table
INSERT INTO Role (Name)
VALUES ('admin');

-- Insert 'user' role into Role table
INSERT INTO Role (Name)
VALUES ('user');

-- Insert 'admin' user and assign it the 'admin' role
INSERT INTO [Users] (UserName, Password, IsActive, RoleId)
VALUES (
    'admin',
    '1234',
    1,
    (SELECT Id FROM Role WHERE Name = 'admin')
);

-- Insert 'user' user and assign it the 'admin' role
INSERT INTO [Users] (UserName, Password, IsActive, RoleId)
VALUES (
    'user',
    '1234',
    1,
    (SELECT Id FROM Role WHERE Name = 'user')
);
