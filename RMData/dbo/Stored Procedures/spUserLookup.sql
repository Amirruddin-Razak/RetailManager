CREATE PROCEDURE [dbo].[spUserLookup]
	@Id NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT FirstName, LastName, EmailAddress, PhoneNumber, DateCreated
	FROM [dbo].[User]
	WHERE Id = @Id
END
