use [InStock.AccountServer];

CREATE TABLE Users (
    Id              INT             NOT NULL IDENTITY(1,1),
    Username        NVARCHAR(50)    NOT NULL UNIQUE,
    PasswordHash    VARBINARY(1024) NOT NULL,
    PasswordSalt    VARBINARY(1024) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE UserProfiles (
    Id          INT             IDENTITY(1,1) NOT NULL,
    UserId      INT             NOT NULL,
    FirstName   NVARCHAR(50)    NOT NULL,
    LastName    NVARCHAR(50)    NOT NULL,
    CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserProfiles_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Metadata] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Metadata] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Clients] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [MetadataId] INT              NOT NULL,
    [Uid]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Clients_Metadata] FOREIGN KEY ([MetadataId]) REFERENCES [dbo].[Metadata] ([Id])
);

CREATE TABLE [dbo].[UserClients] (
    [Id]       INT              IDENTITY (1, 1) NOT NULL,
    [UserId]   INT              NOT NULL,
    [ClientId] INT              NOT NULL,
    CONSTRAINT [PK_UserClients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClients_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    CONSTRAINT [FK_UserClients_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id])
);

CREATE TABLE [dbo].[UserClientTokens] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [UserClientId]  INT            NOT NULL,
    [IdentityToken] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_UserClientTokens] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserClientTokens_UserClients] FOREIGN KEY ([UserClientId]) REFERENCES [dbo].[UserClients] ([Id])
);