-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		Patrick Tibble
-- Create date: 11/8/23
-- Description:	Inserts an AccessToken and Refresh Token pair
-- ================================================
CREATE PROCEDURE InsertAccessRefreshTokenPair 
	-- Add the parameters for the stored procedure here
	@AccessToken nvarchar(MAX) NOT NULL, 
	@IdentityTokenId int NOT NULL,
	@RefreshToken nvarchar(MAX) NOT NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here.
	INSERT INTO Tokens (TokenValue)
	VALUES (@AccessToken);

	DECLARE @AccessTokenId INT;
	SET @AccessTokenId = SCOPE_IDENTITY();

	INSERT INTO AccessTokens (TokenId, IdentityTokenId)
	VALUES (@AccessTokenId, @IdentityTokenId);

	INSERT INTO Tokens (TokenValue)
	VALUES (@RefreshToken);

	DECLARE @RefreshTokenId INT;
	SET @RefreshTokenId = SCOPE_IDENTITY();

	INSERT INTO RefreshTokens (TokenId, AccessTokenId)
	VALUES (@RefreshTokenId, @AccessTokenId);
END
GO
