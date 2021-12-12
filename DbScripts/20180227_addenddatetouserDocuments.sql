--add end date to user documnet
/****** Object:  Table [dbo].[userDocuments]    Script Date: 02/27/2018 12:41:54 AM ******/
Alter TABLE [dbo].[userDocuments]
ADD	[EndDate] [datetime] NULL


--edit adduserdocumnet proc

USE [HudHudDB]
GO
/****** Object:  StoredProcedure [dbo].[addUserDocuments]    Script Date: 02/27/2018 12:46:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[addUserDocuments] 
@userID int,@docID bigint,@allow bit,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@enddate datetime 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(docID) from dbo.userDocuments where @docID=docID and @userID=userID);
if @varID = 0
begin
Insert Into dbo.userDocuments(userID,docID,allow,allowInsert,allowUpdate,allowDelete,EndDate) values(@userID,@docID,@allow,@allowInsert,@allowUpdate,@allowDelete,@enddate);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.userDocuments',16,1)
end
Commit


