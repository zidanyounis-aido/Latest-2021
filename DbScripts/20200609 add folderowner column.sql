--add folderOwner 
ALTER TABLE  [dbo].[folders]
ADD folderOwner INT
GO


-- alter getFoldersByPrimaryKey 
ALTER procedure [dbo].[getFoldersByPrimaryKey] 
@fldrID int 
as 
select fldrID,fldrName,fldrParentID,active,iconID,defaultDocTypID,folderOrder,isDiwan,fldrNameAr,allowWF,folderOwner from dbo.folders where @fldrID=fldrID; 


-- alter updateFoldersByPrimaryKey
ALTER procedure [dbo].[updateFoldersByPrimaryKey] 
@fldrID int,@fldrName nvarchar(500),@fldrParentID int,@active bit,@iconID int,@defaultDocTypID int,@folderOrder int,@isDiwan bit,@fldrNameAr nvarchar(500),@allowWF bit ,@folderOwner int
as 
begin transaction
Update dbo.folders set dbo.folders.fldrName=@fldrName,dbo.folders.fldrParentID=@fldrParentID,dbo.folders.active=@active,dbo.folders.iconID=@iconID,dbo.folders.defaultDocTypID=@defaultDocTypID,dbo.folders.folderOrder=@folderOrder,dbo.folders.isDiwan=@isDiwan,dbo.folders.fldrNameAr=@fldrNameAr,dbo.folders.allowWF=@allowWF,folderOwner=@folderOwner where @fldrID=fldrID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.folders',16,1)
end
Commit


-- alter addfolder
ALTER procedure [dbo].[addFolders] 
@fldrName nvarchar(500),@fldrParentID int,@active bit,@iconID int,@defaultDocTypID int,@folderOrder int,@isDiwan bit,@fldrNameAr nvarchar(500),@allowWF bit ,@folderOwner int
as 
begin Transaction
Insert Into dbo.folders(fldrName,fldrParentID,active,iconID,defaultDocTypID,folderOrder,isDiwan,fldrNameAr,allowWF,folderOwner) values(@fldrName,@fldrParentID,@active,@iconID,@defaultDocTypID,@folderOrder,@isDiwan,@fldrNameAr,@allowWF,@folderOwner);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.folders',16,1)
end
Commit
