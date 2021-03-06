USE [hudhud]
GO
/****** Object:  StoredProcedure [dbo].[addWfPathDetails]    Script Date: 6/3/2020 11:28:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[addWfPathDetails] 
@pathID int,@seqNo smallint,@recipientID int,@endOfPath bit,@recipientType smallint,@companyID int,@branchID int,@approveType smallint ,
@duration int,@duartionType int
as 
begin Transaction
Declare @varID int;
set @varID = (select count(pathID) from dbo.wfPathDetails where @pathID=pathID and @seqNo=seqNo);
if @varID = 0
begin
Insert Into dbo.wfPathDetails(pathID,seqNo,recipientID,endOfPath,recipientType,companyID,branchID,approveType,duration,durationType) values(@pathID,@seqNo,@recipientID,@endOfPath,@recipientType,@companyID,@branchID,@approveType,@duration,@duartionType);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.wfPathDetails',16,1)
end
Commit

