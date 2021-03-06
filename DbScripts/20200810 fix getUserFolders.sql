--add id 
ALTER TABLE  [dbo].[departments]
ADD parentID INT 
GO


ALTER PROCEDURE [dbo].[getUserFolders]
@userID int,
@isDiwan bit
AS
BEGIN
declare @companyID int;
set @companyID = (select ISNULL(companyID,0) from users where userID = @userID)
IF @companyID = 0
begin
SELECT  Distinct    dbo.companyFolders.companyID, dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName, dbo.folders.fldrNameAr, dbo.folders.fldrParentID, 
                      dbo.folders.active, dbo.folders.iconID, dbo.folders.defaultDocTypID, dbo.folders.folderOrder
FROM         dbo.companies INNER JOIN
                      dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN
                      dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID INNER JOIN
                      dbo.userFolders ON dbo.folders.fldrID = dbo.userFolders.fldrID
WHERE     (dbo.userFolders.userID = @userID and dbo.userFolders.allow=1	or (EXISTS(select top 1 programID from dbo.userPrograms where userID=@userID and programID=1))) and isDiwan=@isDiwan 
ORDER BY dbo.companyFolders.companyID,dbo.folders.folderOrder
end
else
begin
SELECT  Distinct    dbo.companyFolders.companyID, dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName, dbo.folders.fldrNameAr, dbo.folders.fldrParentID, 
                      dbo.folders.active, dbo.folders.iconID, dbo.folders.defaultDocTypID, dbo.folders.folderOrder
FROM         dbo.companies INNER JOIN
                      dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN
                      dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID INNER JOIN
                      dbo.userFolders ON dbo.folders.fldrID = dbo.userFolders.fldrID
WHERE     ((dbo.userFolders.userID = @userID and dbo.userFolders.allow=1	) or (EXISTS(select top 1 programID from dbo.userPrograms where userID=@userID and programID=1))) and isDiwan=@isDiwan and dbo.companyFolders.companyID = @companyID
ORDER BY dbo.companyFolders.companyID,dbo.folders.folderOrder 
end
END




ALTER procedure [dbo].[getDepartmentsByPrimaryKey] 
@departmentID int 
as 
select departmentID,departmentName,headUserID,parentDepartmentID,departmentNameAr,parentID from dbo.departments where @departmentID=departmentID; 


ALTER procedure [dbo].[addDepartments] 
@departmentName nvarchar(1000),@headUserID int,@parentDepartmentID int,@departmentNameAr nvarchar(1000) ,@parentID int
as 
begin Transaction
Insert Into dbo.departments(departmentName,headUserID,parentDepartmentID,departmentNameAr,parentID) values(@departmentName,@headUserID,@parentDepartmentID,@departmentNameAr,@parentID);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.departments',16,1)
end
Commit




ALTER procedure [dbo].[updateDepartmentsByPrimaryKey] 
@departmentID int,@departmentName nvarchar(1000),@headUserID int,@parentDepartmentID int,@departmentNameAr nvarchar(1000),@parentID int
as 
begin transaction
Update dbo.departments set dbo.departments.departmentName=@departmentName,dbo.departments.headUserID=@headUserID,dbo.departments.parentDepartmentID=@parentDepartmentID,dbo.departments.departmentNameAr=@departmentNameAr,dbo.departments.parentID=@parentID where @departmentID=departmentID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.departments',16,1)
end
Commit