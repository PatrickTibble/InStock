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