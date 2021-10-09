CREATE PROCEDURE GetContacts
	@SearchQuery nvarchar(1024)
AS
	SELECT * FROM Contacts
	WHERE FREETEXT(CompanyName, @SearchQuery) OR FREETEXT(Surname, @SearchQuery) OR FREETEXT(FirstName, @SearchQuery)
GO