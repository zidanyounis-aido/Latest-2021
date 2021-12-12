Alter table Groups	
Add  ClientId int not null Default(0)
Go

ALTER procedure [dbo].[addGroups] 
@grpDesc nvarchar(500),@companyID int,@branchID int ,@clientId int
as 
begin Transaction
Insert Into dbo.groups(grpDesc,companyID,branchID,ClientId) values(@grpDesc,@companyID,@branchID,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.groups',16,1)
end
Commit
Go