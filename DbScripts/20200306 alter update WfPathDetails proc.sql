USE [hudhud]
GO
/****** Object:  StoredProcedure [dbo].[updateWfPathDetailsByPrimaryKey]    Script Date: 6/3/2020 11:37:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[updateWfPathDetailsByPrimaryKey] 
@pathID int,@seqNo smallint,@recipientID int,@endOfPath bit,@recipientType smallint,@companyID int,@branchID int,@approveType smallint ,
@duration int,@duartionType int
as 
begin transaction
Update dbo.wfPathDetails set dbo.wfPathDetails.pathID=@pathID,dbo.wfPathDetails.seqNo=@seqNo,dbo.wfPathDetails.recipientID=@recipientID,dbo.wfPathDetails.endOfPath=@endOfPath,dbo.wfPathDetails.recipientType=@recipientType,dbo.wfPathDetails.companyID=@companyID,dbo.wfPathDetails.branchID=@branchID,dbo.wfPathDetails.approveType=@approveType,dbo.wfPathDetails.duration=@duration,dbo.wfPathDetails.durationType=@duartionType where @pathID=pathID and @seqNo=seqNo; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.wfPathDetails',16,1)
end
Commit

