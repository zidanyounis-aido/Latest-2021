ALTER PROCEDURE [dbo].[getWorkflowUsers] 
@lang bit
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
	dbo.users.FullName <> '-' order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
END
else
begin
	SELECT     dbo.users.userID, dbo.users.fullName as userFullName
FROM         dbo.users INNER JOIN
                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
	Where
	dbo.users.FullName <> '-' order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
END
End
