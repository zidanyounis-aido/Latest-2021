USE [hudhud]
GO
/****** Object:  StoredProcedure [dbo].[getWorkflowUsers]    Script Date: 7/15/2020 9:09:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[getWorkflowUsers] 
@lang bit
AS
	SELECT     dbo.users.userID, dbo.users.FullName as userFullName
FROM         dbo.users INNER JOIN
                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
	Where
	dbo.users.FullName <> '-' order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
--BEGIN
--if @lang=0
--begin
--	SELECT     dbo.users.userID, ( dbo.positions.positionTitle
--+ ' - ' +  dbo.companies.companyName + ' - ' + dbo.branchs.branchName ) as userFullName
--FROM         dbo.users INNER JOIN
--                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
--                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
--                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
--	Where
--	dbo.users.FullName <> '-' order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
--END
--else
--begin
--	SELECT     dbo.users.userID, ( dbo.positions.positionTitleAr
--+ ' - ' +  dbo.companies.companyNameAr + ' - ' + dbo.branchs.branchNameAr ) as userFullName
--FROM         dbo.users INNER JOIN
--                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
--                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
--                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
--	Where
--	dbo.users.FullName <> '-' order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
--END
--End
