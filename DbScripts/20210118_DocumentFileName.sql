--Select * From documents Where docID=10040
--Select * From documentVersions Where docID=10040
--Select * From [dbo].[documentMataValues] Where docID=10040

Alter Table documentVersions Add DocumentFileName nvarchar(50)

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[addDocumentVersions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'ALTER procedure [dbo].[addDocumentVersions] 
@docID bigint,@version smallint,@addedDate datetime,@addedUserID int,@ext varchar(4),@docGroupID int, @DocumentFileName nvarchar(50)
as 
begin Transaction
Declare @varID int;
set @varID = (select count(docID) from dbo.documentVersions where @docID=docID and @version=version);
if @varID = 0
begin
Insert Into dbo.documentVersions(docID,version,addedDate,addedUserID,ext,docGroupID,DocumentFileName) values(@docID,@version,@addedDate,@addedUserID,@ext,@docGroupID,@DocumentFileName);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR (''Error in Inserting into dbo.documentVersions'',16,1)
end
Commit


' 
END
GO

