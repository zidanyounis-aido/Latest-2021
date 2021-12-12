Alter table [dbo].[Departments]
Add  ClientId int not null Default(0)
Go

ALTER procedure [dbo].[addDepartments] 
@departmentName nvarchar(1000),@headUserID int,@parentDepartmentID int,@departmentNameAr nvarchar(1000) ,@parentID int ,@clientId int
as 
begin Transaction
Insert Into dbo.departments(departmentName,headUserID,parentDepartmentID,departmentNameAr,parentID,ClientId) values(@departmentName,@headUserID,@parentDepartmentID,@departmentNameAr,@parentID,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.departments',16,1)
end
Commit
go
