-- The database should already exist. In this case, it is [InStock.IdentityServer].

USE [InStock.IdentityServer];

CREATE TABLE Tokens (
    Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    TokenValue NVARCHAR(MAX) NOT NULL,
    Invalidated BIT DEFAULT 0
);

CREATE TABLE IdentityTokens (
    Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    TokenId INT NOT NULL,
    CONSTRAINT FK_IdentityTokens_Tokens FOREIGN KEY (TokenId) REFERENCES Tokens (Id),
    CONSTRAINT UQ_IdentityTokens_Username UNIQUE (Username)
);

CREATE TABLE AccessTokens (
    Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    TokenId INT NOT NULL,
    IdentityTokenId INT NOT NULL,
    CONSTRAINT FK_AccessTokens_Tokens FOREIGN KEY (TokenId) REFERENCES Tokens (Id),
    CONSTRAINT FK_AccessTokens_IdentityTokens FOREIGN KEY (IdentityTokenId) REFERENCES IdentityTokens (Id),
    CONSTRAINT UQ_AccessTokens_TokenId UNIQUE (TokenId)
);

CREATE TABLE RefreshTokens (
    Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    TokenId INT NOT NULL,
    AccessTokenId INT NOT NULL,
    CONSTRAINT FK_RefreshTokens_Tokens FOREIGN KEY (TokenId) REFERENCES Tokens (Id),
    CONSTRAINT FK_RefreshTokens_AccessTokens FOREIGN KEY (AccessTokenId) REFERENCES AccessTokens (Id),
    CONSTRAINT UQ_RefreshTokens_TokenId UNIQUE (TokenId),
    CONSTRAINT UQ_RefreshTokens_AccessTokenId UNIQUE (AccessTokenId)
);

CREATE TABLE TokenFamilies (
    Id INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    OriginatorTokenId INT NOT NULL,
    TokenId INT NOT NULL,
    CONSTRAINT FK_TokenFamilies_OriginatorTokens FOREIGN KEY (OriginatorTokenId) REFERENCES Tokens (Id),
    CONSTRAINT FK_TokenFamilies_Tokens FOREIGN KEY (TokenId) REFERENCES Tokens (Id),
    CONSTRAINT UQ_TokenFamilies_TokenId UNIQUE (TokenId)
);