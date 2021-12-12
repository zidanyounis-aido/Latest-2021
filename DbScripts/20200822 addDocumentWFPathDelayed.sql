USE [hudhud]
GO
/****** Object:  StoredProcedure [dbo].[addDocumentWFPath]    Script Date: 8/22/2020 5:38:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[addDocumentWFPathDelayed] 
@docID bigint,@userID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@userNotes ntext,@receiveDate datetime ,@endDate datetime,@inboxType int ,@documentWFPathId int
as 
begin Transaction


Insert Into dbo.documentWFPathDelayed(docID,userID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType,userNotes,receiveDate,EndDate,inboxType,documentWFPathId) values(@docID,@userID,@actionDateTime,@wfPathID,@wfSeqNo,@actionType,@recipientType,@userNotes,@receiveDate,@endDate,@inboxType,@documentWFPathId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentWFPath',16,1)
end
Commit



/****** Object:  StoredProcedure [dbo].[updateDocumentWFPathByPrimaryKey]    Script Date: 03/03/2018 11:42:21 AM ******/
SET ANSI_NULLS ON