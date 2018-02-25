CREATE PROCEDURE GetContactAutoComplete
	@SearchQuery nvarchar(1024)
AS
	SELECT @SearchQuery = '"' + @SearchQuery + '*' + '"'

	SELECT DISTINCT TOP 10 CompanyName AS Suggestion FROM Contacts
	WHERE CONTAINS(CompanyName, @SearchQuery)
	UNION
	SELECT DISTINCT TOP 10 FirstName AS Suggestion FROM Contacts
	WHERE CONTAINS(FirstName, @SearchQuery)
	UNION
	SELECT DISTINCT TOP 10 Surname AS Suggestion FROM Contacts
	WHERE CONTAINS(Surname, @SearchQuery)
GO