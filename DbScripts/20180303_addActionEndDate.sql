--add end date coulmn
USE [HudHudDB]
GO

Alter TABLE [dbo].[documentWFPath]
add [EndDate] [datetime] NULL;





/****** Object:  StoredProcedure [dbo].[addDocumentWFPath]    Script Date: 03/03/2018 11:40:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[addDocumentWFPath] 
@docID bigint,@userID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@userNotes ntext,@receiveDate datetime ,@endDate datetime
as 
begin Transaction
Insert Into dbo.documentWFPath(docID,userID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType,userNotes,receiveDate,EndDate) values(@docID,@userID,@actionDateTime,@wfPathID,@wfSeqNo,@actionType,@recipientType,@userNotes,@receiveDate,@endDate);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentWFPath',16,1)
end
Commit



/****** Object:  StoredProcedure [dbo].[updateDocumentWFPathByPrimaryKey]    Script Date: 03/03/2018 11:42:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[updateDocumentWFPathByPrimaryKey] 
@ID int,@docID bigint,@userID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@userNotes ntext,@receiveDate datetime ,@endDate datetime
as 
begin transaction
Update dbo.documentWFPath set dbo.documentWFPath.docID=@docID,dbo.documentWFPath.EndDate=@endDate,dbo.documentWFPath.userID=@userID,dbo.documentWFPath.actionDateTime=@actionDateTime,dbo.documentWFPath.wfPathID=@wfPathID,dbo.documentWFPath.wfSeqNo=@wfSeqNo,dbo.documentWFPath.actionType=@actionType,dbo.documentWFPath.recipientType=@recipientType,dbo.documentWFPath.userNotes=@userNotes,dbo.documentWFPath.receiveDate=@receiveDate where @ID=ID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentWFPath',16,1)
end
Commit
