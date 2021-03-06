/*
   Wednesday, January 13, 20212:04:43 AM
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



------------------------ alter add sign proc --------------------------


ALTER PROCEDURE [dbo].[AddSigture]
	-- Add the parameters for the stored procedure here
@signture nvarchar(max) ,
 @document nvarchar(50), @user int , @width nvarchar(50) , @height nvarchar(50) ,@top nvarchar(50) ,@left nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @LastChangeDate as datetime
SET @LastChangeDate = GetDate()
insert into SignatureTB values(@signture ,@document ,@user ,@width ,@height ,@top ,@left,@LastChangeDate)