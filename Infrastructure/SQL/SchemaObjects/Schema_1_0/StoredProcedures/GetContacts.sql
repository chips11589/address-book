CREATE PROCEDURE GetContacts
	@SearchQuery nvarchar(1024)
AS
	SELECT c.*, t.* FROM Contacts c
	LEFT JOIN ContactTag ct ON c.Id = ct.ContactsId
	LEFT JOIN Tags t ON t.Id = ct.TagsId
	WHERE FREETEXT(CompanyName, @SearchQuery) OR FREETEXT(Surname, @SearchQuery) OR FREETEXT(FirstName, @SearchQuery)
GO