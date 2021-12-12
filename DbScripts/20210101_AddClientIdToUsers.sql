Alter table Users	
Add  ClientId int not null Default(0)
Go


ALTER procedure [dbo].[addUsers] 
@userName varchar(50),@password nvarchar(4000),@fullName nvarchar(500),@grpID int,@active bit,@companyID int,@branchID int,@departmentID int,@positionID int,@email varchar(500),@allowCustomWF bit,@allowCreateFolders bit,@allowReplaceDocuments bit,@allowDiwan bit,@isFirstLogin bit,@passwordCreationDate datetime,@passwordModifiedDate datetime,@lastPassword nvarchar(4000), @Phone nvarchar(50),@ClientId int
as 
begin Transaction
Insert Into dbo.users(userName,password,fullName,grpID,active,companyID,branchID,departmentID,positionID,email,allowCustomWF,allowCreateFolders,allowReplaceDocuments,allowDiwan,isFirstLogin,passwordCreationDate,passwordModifiedDate,lastPassword,Phone,ClientId) values(@userName,@password,@fullName,@grpID,@active,@companyID,@branchID,@departmentID,@positionID,@email,@allowCustomWF,@allowCreateFolders,@allowReplaceDocuments,@allowDiwan,@isFirstLogin,@passwordCreationDate,@passwordModifiedDate,@lastPassword,@Phone,@ClientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.users',16,1)
end
Commit
Go

ALTER procedure [dbo].[getUsersByPrimaryKey] 
@userID int 
as 
select userID,userName,password,fullName,grpID,active,companyID,branchID,departmentID,positionID,email,allowCustomWF,allowCreateFolders,allowReplaceDocuments,allowDiwan,isFirstLogin,passwordCreationDate,passwordModifiedDate,lastPassword,Phone,ClientId from dbo.users where @userID=userID; 

go

ALTER PROCEDURE [dbo].[getWorkflowUsers] 
@lang bit,
@ClientId int
AS
BEGIN
if @lang=0
begin
	SELECT     dbo.users.userID, dbo.users.fullName as userFullName
FROM         dbo.users INNER JOIN
                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
	Where
	dbo.users.FullName <> '-'  and dbo.users.ClientId = @clientId order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
END
else
begin
	SELECT     dbo.users.userID, dbo.users.fullName as userFullName
FROM         dbo.users INNER JOIN
                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
	Where
	dbo.users.FullName <> '-'   and dbo.users.ClientId = @clientId order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
END
End

Go