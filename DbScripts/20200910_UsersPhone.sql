USE [hudhuddb]
GO
Alter Table users Add Phone nvarchar(50)
GO
ALTER procedure [dbo].[getUsersByPrimaryKey] 
@userID int 
as 
select userID,userName,password,fullName,grpID,active,companyID,branchID,departmentID,positionID,email,allowCustomWF,allowCreateFolders,allowReplaceDocuments,allowDiwan,isFirstLogin,passwordCreationDate,passwordModifiedDate,lastPassword,Phone from dbo.users where @userID=userID; 

GO

ALTER procedure [dbo].[updateUsersByPrimaryKey] 
@userID int,@userName varchar(50),@password nvarchar(4000),@fullName nvarchar(500),@grpID int,@active bit,@companyID int,@branchID int,@departmentID int,@positionID int,@email varchar(500),@allowCustomWF bit,@allowCreateFolders bit,@allowReplaceDocuments bit,@allowDiwan bit,@isFirstLogin bit,@passwordCreationDate datetime,@passwordModifiedDate datetime,@lastPassword nvarchar(4000),@Phone nvarchar(50) 
as 
begin transaction
Update dbo.users set dbo.users.userName=@userName,dbo.users.password=@password,dbo.users.fullName=@fullName,dbo.users.grpID=@grpID,dbo.users.active=@active,dbo.users.companyID=@companyID,dbo.users.branchID=@branchID,dbo.users.departmentID=@departmentID,dbo.users.positionID=@positionID,dbo.users.email=@email,dbo.users.allowCustomWF=@allowCustomWF,dbo.users.allowCreateFolders=@allowCreateFolders,dbo.users.allowReplaceDocuments=@allowReplaceDocuments,dbo.users.allowDiwan=@allowDiwan,dbo.users.isFirstLogin=@isFirstLogin,dbo.users.passwordCreationDate=@passwordCreationDate,dbo.users.passwordModifiedDate=@passwordModifiedDate,dbo.users.lastPassword=@lastPassword,Phone=@Phone where @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.users',16,1)
end
Commit
GO

ALTER procedure [dbo].[addUsers] 
@userName varchar(50),@password nvarchar(4000),@fullName nvarchar(500),@grpID int,@active bit,@companyID int,@branchID int,@departmentID int,@positionID int,@email varchar(500),@allowCustomWF bit,@allowCreateFolders bit,@allowReplaceDocuments bit,@allowDiwan bit,@isFirstLogin bit,@passwordCreationDate datetime,@passwordModifiedDate datetime,@lastPassword nvarchar(4000), @Phone nvarchar(50)
as 
begin Transaction
Insert Into dbo.users(userName,password,fullName,grpID,active,companyID,branchID,departmentID,positionID,email,allowCustomWF,allowCreateFolders,allowReplaceDocuments,allowDiwan,isFirstLogin,passwordCreationDate,passwordModifiedDate,lastPassword,Phone) values(@userName,@password,@fullName,@grpID,@active,@companyID,@branchID,@departmentID,@positionID,@email,@allowCustomWF,@allowCreateFolders,@allowReplaceDocuments,@allowDiwan,@isFirstLogin,@passwordCreationDate,@passwordModifiedDate,@lastPassword,@Phone);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.users',16,1)
end
Commit



GO



--Select * From users 