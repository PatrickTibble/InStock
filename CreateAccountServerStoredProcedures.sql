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