USE [InStock.AccountServer];

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/10/23
-- Description:	Retrieve a User based on a 
--				Provided Username
-- =============================================
CREATE PROCEDURE sp_GetUserFromUsername
	@Username NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT U.Id, UP.FirstName, UP.LastName, U.Username
	FROM Users U
	JOIN UserProfiles UP
	ON UP.UserId = U.Id
	WHERE U.Username = @Username;
END
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_AddUserAndProfile
	@Username NVARCHAR(50),
	@PasswordHash VARBINARY(1024),
	@PasswordSalt VARBINARY(1024),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UserId INT;

	INSERT INTO Users (Username, PasswordHash, PasswordSalt)
	VALUES (@Username, @PasswordHash, @PasswordSalt);

	SET @UserId = SCOPE_IDENTITY();

	INSERT INTO UserProfiles (UserId, FirstName, LastName)
	VALUES (@UserId, @FirstName, @LastName);
END
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_GetHashedUserFromUsername
	@Username NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT U.Id, U.Username, U.PasswordHash, U.PasswordSalt
	FROM Users U
	WHERE U.Username = @Username;
END
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_AddUserClient 
	@Username NVARCHAR(50),
	@ClientId UNIQUEIDENTIFIER,
	@Name NVARCHAR(MAX),
	@Description NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	-- store the user id
	DECLARE @UserId INT;
	DECLARE @MetadataId INT;
	DECLARE @CID INT;

	SELECT @UserId = U.Id
	FROM Users U
	WHERE U.Username = @Username;

	INSERT INTO Metadata (Name, Description) VALUES (@Name, @Description);
	SET @MetadataId = SCOPE_IDENTITY();

	INSERT INTO Clients (MetadataId, Uid) VALUES (@MetadataId, @ClientId);
	SET @CID = SCOPE_IDENTITY();

	INSERT INTO UserClients (ClientId, UserId) VALUES (@CID, @UserId);
	RETURN SCOPE_IDENTITY();
END
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_GetUserTokensForClient 
	@ClientId UNIQUEIDENTIFIER
AS
BEGIN

	SET NOCOUNT ON;

	SELECT T.IdentityToken
	FROM UserClientTokens T
	JOIN UserClients U
	ON U.Id = T.UserClientId
	JOIN Clients C
	ON C.Id = U.ClientId
	WHERE C.Uid = @ClientId
END
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_AddUserTokenForClient
	@Username NVARCHAR(50),
	@ClientId UNIQUEIDENTIFIER,
	@ClientName NVARCHAR(MAX),
	@ClientDescription NVARCHAR(MAX),
	@IdentityToken NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @MetadataId INT;
	DECLARE @CID INT;
	DECLARE @UserId INT;
	DECLARE @UserClientId INT;

	SELECT @UserId = U.Id FROM Users U WHERE U.Username = @Username;

	IF NOT EXISTS(
		SELECT 1
		FROM Clients C
		WHERE C.Uid = @ClientId
		) BEGIN
			-- Client Doesn't Exist
			INSERT INTO Metadata (Name, Description) VALUES (@ClientName, @ClientDescription);
			SET @MetadataId = SCOPE_IDENTITY();

			INSERT INTO Clients (MetadataId, Uid) VALUES (@MetadataId, @ClientId);
			SET @CID = SCOPE_IDENTITY();
		END

	IF NOT EXISTS(
		SELECT 1
		FROM UserClients UC
		JOIN Clients C
		ON UC.ClientId = C.Id
		JOIN Users U
		ON UC.UserId = U.Id
		WHERE C.Uid = @ClientId
		AND U.Username = @Username
		) BEGIN
			-- this client hasn't been associated with this username yet
			SELECT @CID = C.ID FROM Clients C WHERE C.Uid = @ClientId;
			
			INSERT INTO UserClients (ClientId, UserId) VALUES (@CID, @UserId);
		END

		SELECT @UserClientId = UC.Id FROM UserClients UC WHERE UC.UserId = @UserId;

		INSERT INTO UserClientTokens (UserClientId, IdentityToken) VALUES (@UserClientId, @IdentityToken);
END
GO