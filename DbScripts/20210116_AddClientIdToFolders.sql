Alter table [dbo].[folders]
Add  ClientId int not null Default(0)
Go



ALTER procedure [dbo].[addFolders] 
@fldrName nvarchar(500),@fldrParentID int,@active bit,@iconID int,@defaultDocTypID int,@folderOrder int,@isDiwan bit,@fldrNameAr nvarchar(500),@allowWF bit ,@folderOwner int,@clientId int
as 
begin Transaction
Insert Into dbo.folders(fldrName,fldrParentID,active,iconID,defaultDocTypID,folderOrder,isDiwan,fldrNameAr,allowWF,folderOwner,ClientId) values(@fldrName,@fldrParentID,@active,@iconID,@defaultDocTypID,@folderOrder,@isDiwan,@fldrNameAr,@allowWF,@folderOwner,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.folders',16,1)
end
Commit
Go

