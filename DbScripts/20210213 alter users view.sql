alter view [dbo].[userReminders]
AS 
SELECT   dbo.usersRemiders.userID, dbo.usersRemiders.docID, dbo.documents.docName, dbo.metas.metaDesc, dbo.metas.metaDescAr, dbo.usersRemiders.isRemoved, 
                         dbo.usersRemiders.beforedays
FROM         dbo.usersRemiders INNER JOIN
                         dbo.metas ON dbo.usersRemiders.metaID = dbo.metas.metaID INNER JOIN
                         dbo.documents ON dbo.usersRemiders.docID = dbo.documents.docID
						 GO