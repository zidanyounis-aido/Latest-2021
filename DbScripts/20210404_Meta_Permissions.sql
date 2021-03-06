IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getMetaUsersAndGroupsPermissions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].getMetaUsersAndGroupsPermissions
GO
--getMetaUsersAndGroupsPermissions 156
create Proc getMetaUsersAndGroupsPermissions
@metaID int
as
select U.userID ID,U.fullName Name,UP.allowRead,UP.allowEdit,'u' PerType From metaUsersPermissions UP Inner Join users U ON UP.userID=U.userID 
Where UP.metaID=@metaID
Union
select G.grpID ID,G.grpDesc Name,GP.allowRead,GP.allowEdit, 'g' PerType From metaGroupsPermissions GP Inner Join groups G ON GP.grpID=G.grpID 
Where GP.metaID=@metaID
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getMetaUsersAndGroupsFolderPermissions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].getMetaUsersAndGroupsFolderPermissions
GO
--getMetaUsersAndGroupsFolderPermissions 136
create Proc getMetaUsersAndGroupsFolderPermissions
@metaID int
as
Select T.ID,T.Name,CAST(MAX(CAST(T.allowRead as INT)) AS BIT)allowRead,CAST(MAX(CAST(T.allowUpdate as INT)) AS BIT)allowEdit,T.PerType From (
select U.userID ID,U.fullName Name,Cast('1' as bit)allowRead,UF.allowUpdate,'u' PerType 
From userFolders UF Inner Join users U ON UF.userID=U.userID 
                    Inner Join folders F ON UF.fldrID=F.fldrID 
					Inner Join docTypes DT ON F.defaultDocTypID=DT.docTypID 
					Inner Join metas M ON DT.docTypID=M.docTypID 
Where M.metaID=@metaID
Union
select G.grpID ID,G.grpDesc Name,Cast('1' as bit)allowRead,GF.allowUpdate, 'g' PerType 
From groupFolders GF Inner Join groups G ON GF.grpID=G.grpID 
                    Inner Join folders F ON GF.fldrID=F.fldrID 
					Inner Join docTypes DT ON F.defaultDocTypID=DT.docTypID 
					Inner Join metas M ON DT.docTypID=M.docTypID ) T
Group by T.ID,T.Name,T.PerType 
Go

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
WHERE     (dbo.userFolders.userID = @userID and dbo.userFolders.allow=1	
--or (EXISTS(select top 1 programID from dbo.userPrograms where userID=@userID and programID=1))
) and isDiwan=@isDiwan 
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
WHERE     ((dbo.userFolders.userID = @userID and dbo.userFolders.allow=1	) 
--or (EXISTS(select top 1 programID from dbo.userPrograms where userID=@userID and programID=1))
) and isDiwan=@isDiwan and dbo.companyFolders.companyID = @companyID
ORDER BY dbo.companyFolders.companyID,dbo.folders.folderOrder 
end
END

GO

