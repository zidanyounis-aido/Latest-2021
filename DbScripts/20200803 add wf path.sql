
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

