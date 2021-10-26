CREATE FULLTEXT CATALOG production_catalog;  
GO

CREATE FULLTEXT INDEX ON Contacts
 (   
  CompanyName
     Language 1033,  
  FirstName  
     Language 1033,  
  Surname   
     Language 1033       
 )
  KEY INDEX PK_Contacts
      ON production_catalog;   
GO  