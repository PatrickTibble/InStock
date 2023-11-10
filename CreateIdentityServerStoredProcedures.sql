SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/8/23
-- Description:	Retrieve an IdentityToken using an AccessToken
-- =============================================
CREATE PROCEDURE GetIdentityTokenFromAccessToken 
	-- Add the parameters for the stored procedure here
	@AccessToken nvarchar(MAX) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IT.Id, T.TokenValue
	FROM IdentityTokens IT
	JOIN AccessTokens AT ON IT.Id = AT.IdentityTokenId
	JOIN Tokens T ON AT.TokenId = T.Id
	WHERE T.TokenValue = @AccessToken;
END
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

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Verify Refresh and Access Tokens 
--              are valid and associated
-- =============================================
CREATE PROCEDURE ValidateRefreshAndAccessTokens
    @RefreshToken NVARCHAR(MAX),
    @AccessToken NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Declare variables to store the retrieved token data
    DECLARE @RefreshTokenId INT;
    DECLARE @AccessTokenId INT;
    DECLARE @Associated BIT;

    -- Retrieve the RefreshTokenId based on the RefreshToken parameter
    SELECT @RefreshTokenId = Id
    FROM Tokens
    WHERE TokenValue = @RefreshToken;

    -- Retrieve the AccessTokenId based on the AccessToken parameter
    SELECT @AccessTokenId = Id
    FROM Tokens
    WHERE TokenValue = @AccessToken;

    -- Check if the AccessToken is associated with the RefreshToken
    SET @Associated = (
        SELECT CASE
            WHEN EXISTS (
                SELECT 1
                FROM RefreshTokens
                WHERE TokenId = @RefreshTokenId
                AND AccessTokenId = @AccessTokenId
            ) THEN 1
            ELSE 0
        END
    );
END
GO