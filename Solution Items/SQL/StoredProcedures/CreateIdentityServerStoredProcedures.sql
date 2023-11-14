SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Retrieve an AccessToken using
--              its TokenValue
-- =============================================
CREATE PROCEDURE sp_GetAccessTokenFromTokenValue
	@TokenValue NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	AT.Id,
			AT.IdentityTokenId,
			T.Invalidated, 
			T.TokenValue
	FROM AccessTokens AT
	JOIN Tokens T
	ON AT.TokenId = T.Id
	WHERE T.TokenValue = @TokenValue;
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Retrieve an IdentityToken using
--              its Id
-- =============================================
CREATE PROCEDURE sp_GetIdentityTokenFromId
	@IdentityTokenId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	IT.Id,
			T.Invalidated,
			T.TokenValue
	FROM IdentityTokens IT
	JOIN Tokens T
	ON IT.TokenId = T.Id
	WHERE IT.Id = @IdentityTokenId;
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Retrieve an IdentityToken using
--              its TokenValue
-- =============================================
CREATE PROCEDURE sp_GetIdentityTokenFromTokenValue
	@TokenValue NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	IT.Id, 
			T.Invalidated, 
			T.TokenValue
	FROM IdentityTokens IT
	JOIN Tokens T
	ON IT.TokenId = T.Id
	WHERE T.TokenValue = @TokenValue;
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Retrieve an RefreshToken using
--              its TokenValue
-- =============================================
CREATE PROCEDURE sp_GetRefreshTokenFromTokenValue
	@TokenValue NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	RT.Id,
			RT.AccessTokenId,
			T.Invalidated,
			T.TokenValue
	FROM RefreshTokens RT
	JOIN Tokens T
	ON RT.TokenId = T.Id
	WHERE T.TokenValue = @TokenValue;
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/14/23
-- Description:	Invalidates all Tokens using
--              an IdentityToken
-- =============================================
CREATE PROCEDURE sp_InvalidateTokenFamily
    @IdentityTokenId INT
AS
BEGIN
    SET NOCOUNT ON;

    CREATE TABLE #TempTokens (
        TokenId INT
    );
	
	CREATE TABLE #TempAccessTokens (
		Id INT,
		TokenId INT
	);
	-- get access token ids for associated refresh tokens
	INSERT INTO #TempAccessTokens (Id, TokenId)
	SELECT Id, TokenId
	FROM AccessTokens
	WHERE IdentityTokenId = @IdentityTokenId;
	-- get access token tokenids for invalidation
	INSERT INTO #TempTokens (TokenId)
	SELECT TokenId
	FROM #TempAccessTokens;

	INSERT INTO #TempTokens (TokenId)
	SELECT TokenId
	FROM RefreshTokens
	WHERE AccessTokenId in (SELECT Id FROM #TempAccessTokens);
	
	DROP TABLE #TempAccessTokens;
	
	INSERT INTO #TempTokens (TokenId)
	SELECT TokenId
	FROM IdentityTokens
	WHERE Id = @IdentityTokenId;

    UPDATE Tokens
    SET Invalidated = 1
    WHERE Id IN (SELECT TokenId FROM #TempTokens);

    DROP TABLE #TempTokens;
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Invalidates all Tokens using
--              a RefreshToken
-- =============================================
CREATE PROCEDURE [dbo].[sp_InvalidateTokenFamilyForRefreshToken]
    @RefreshToken NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RefreshTokenId INT;
	DECLARE @OriginatorTokenId INT;
    DECLARE @TokenId INT;

    SELECT @RefreshTokenId = Id
    FROM Tokens
    WHERE TokenValue = @RefreshToken;

	SELECT @OriginatorTokenId = OriginatorTokenId
	FROM TokenFamilies
	WHERE TokenId = @RefreshTokenId;

    CREATE TABLE #TempTokens (
        TokenId INT
    );

    INSERT INTO #TempTokens (TokenId)
    SELECT TokenId
    FROM TokenFamilies
    WHERE OriginatorTokenId = @OriginatorTokenId;

    UPDATE Tokens
    SET Invalidated = 1
    WHERE Id IN (SELECT TokenId FROM #TempTokens);

    DROP TABLE #TempTokens;
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Invalidates an AccessToken and 
--				RefreshToken using their Ids
-- =============================================
CREATE PROCEDURE sp_InvalidateTokens
    @RefreshTokenId INT,
	@AccessTokenId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Tokens
    SET Invalidated = 1
    WHERE Id IN (@AccessTokenId, @RefreshTokenId);
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Saves a set of Tokens
-- =============================================
CREATE PROCEDURE sp_SaveTokens
    @RefreshToken NVARCHAR(MAX),
	@AccessToken NVARCHAR(MAX),
	@IdentityToken NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RefreshTokenId INT;
	DECLARE @AccessTokenId INT;
	DECLARE @IdentityTokenId INT;

	-- IF The IdentityToken exists, retrieve its Id
	IF EXISTS (
		SELECT 1
		FROM IdentityTokens IT
		JOIN Tokens T
		ON IT.TokenId = T.Id
		WHERE T.TokenValue = @IdentityToken
		AND T.Invalidated = 0
	) BEGIN
		SELECT @IdentityTokenId = IT.Id
		FROM IdentityTokens IT
		JOIN Tokens T
		ON IT.TokenId = T.Id
		WHERE T.TokenValue = @IdentityToken
		AND T.Invalidated = 0;
	END 
	ELSE BEGIN
		-- ELSE Create the IdentityToken
		INSERT INTO Tokens (TokenValue)
		VALUES (@IdentityToken);

		SET @IdentityTokenId = SCOPE_IDENTITY();
		
		INSERT INTO IdentityTokens (TokenId)
		VALUES (@IdentityTokenId);

		SET @IdentityTokenId = SCOPE_IDENTITY();
	END
	
	INSERT INTO Tokens (TokenValue)
	VALUES (@AccessToken);

	SET @AccessTokenId = SCOPE_IDENTITY();
	
	INSERT INTO AccessTokens (TokenId, IdentityTokenId)
	VALUES (@AccessTokenId, @IdentityTokenId);

	SET @AccessTokenId = SCOPE_IDENTITY();

    INSERT INTO Tokens (TokenValue)
	VALUES (@RefreshToken);

	SET @RefreshTokenId = SCOPE_IDENTITY();

	INSERT INTO RefreshTokens (TokenId, AccessTokenId)
	VALUES (@RefreshTokenId, @AccessTokenId);

	INSERT INTO TokenFamilies (TokenId, OriginatorTokenId)
	VALUES (@RefreshTokenId, @IdentityTokenId);
END
GO

-- =============================================
-- Author:		Patrick Tibble
-- Create date: 11/9/23
-- Description:	Validates a Token using
--              its TokenValue
-- =============================================
CREATE PROCEDURE sp_ValidateToken
	@TokenValue NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ReturnVal INT;

	SELECT @ReturnVal = CASE
            WHEN EXISTS (
                SELECT 1
                FROM Tokens
                WHERE TokenValue = @TokenValue
				AND Invalidated = 0
            ) THEN 1
            ELSE 0
        END

	RETURN @ReturnVal;
END
GO
