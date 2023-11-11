﻿use [dbo.AccountService];

CREATE TABLE Users (
    Id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARBINARY(1024) NOT NULL,
    PasswordSalt VARBINARY(1024) NOT NULL
);

CREATE TABLE UserProfiles (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    UserId INT FOREIGN KEY REFERENCES Users(Id),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL
);