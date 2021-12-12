Alter table [dbo].[WorkFlowPaths]
Add  ClientId int not null Default(0)
Go


ALTER procedure [dbo].[addWorkFlowPaths] 
@pathDesc nvarchar(500),@fldrId int,@docTypId int,@pathDescAr nvarchar(500) ,@clientId int
as 
begin Transaction
Insert Into dbo.workFlowPaths(pathDesc,fldrId,docTypId,pathDescAr,ClientId) values(@pathDesc,@fldrId,@docTypId,@pathDescAr,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.workFlowPaths',16,1)
end
Commit
Go