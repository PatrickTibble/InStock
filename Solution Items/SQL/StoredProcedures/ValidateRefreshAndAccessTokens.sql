SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
END;