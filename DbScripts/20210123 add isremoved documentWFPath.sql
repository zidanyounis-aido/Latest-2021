/*
   Saturday, January 23, 20218:55:51 PM
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
ALTER TABLE dbo.documentWFPath ADD
	isRemoved bit NULL
GO
ALTER TABLE dbo.documentWFPath ADD CONSTRAINT
	DF_documentWFPath_isRemoved DEFAULT 0 FOR isRemoved
GO
ALTER TABLE dbo.documentWFPath SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.documentWFPath', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.documentWFPath', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.documentWFPath', 'Object', 'CONTROL') as Contr_Per 