/*
   Friday, March 12, 20213:01:12 AM
   User: 
   Server: .
   Database: hudhud
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SignatureTB ADD
	Date datetime NULL
GO
ALTER TABLE dbo.SignatureTB SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
