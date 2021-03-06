
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventsSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EventsSearch]
GO

Create Proc [dbo].[EventsSearch]
@UserID int,
@SearchText nvarchar(500)
as
Select E.event_id,E.title,E.description,E.event_start,E.event_end,(select fullname from users where userid=E.CreatedBy)UserName From Event E 
where CreatedBy=@UserID 
And (E.title Like '%' + @SearchText + '%' OR E.description Like '%' + @SearchText + '%')

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TasksSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[TasksSearch]
GO

Create Proc [dbo].[TasksSearch]
@UserID int,
@lang nvarchar(10),
@SearchText nvarchar(500)
as
Select T.Id,T.TaskName,(T.TaskDate) as TaskDate,(T.TaskDate) as TaskTime,T.IsComplete,
Convert(varchar,T.TaskDate,103) as TaskDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), T.TaskDate, 100), 7)) as TaskTimeOnly,
(select fullname from users where userid=T.AssignTo)AssignTo,
(select fullname from users where userid=T.CreatedBy)CreatedBy,
(select Case When @lang=0 Then TT.EnText Else TT.ArText End from TaskTypes TT where TT.Id=T.TaskType)TaskType
From ToDoList T
where T.IsDeleted<>1 And (T.CreatedBy=@UserID OR T.AssignTo=@UserID)
And (T.TaskName Like '%' + @SearchText + '%' OR T.Description Like '%' + @SearchText + '%')

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DocumentsSearch]
GO
--[dbo].[DocumentsSearch] 1,1,t
Create Proc [dbo].[DocumentsSearch]
@UserID int,
@lang nvarchar(10),
@SearchText nvarchar(500)
as
Select D.docID,D.docName,
(Select Case When @lang=0 Then F.fldrName Else F.fldrNameAr End From folders F Where F.fldrID=D.fldrID) As FolderName,
(Select Case When @lang=0 Then DT.docTypDesc Else DT.docTypDescAr End From docTypes DT Where DT.docTypID=D.docTypID) As DocTypeName,
D.addedDate,(select fullname from users where userid=D.addedUserID)AddedBy,
D.modifyDate,
Convert(varchar,D.addedDate,103) as addedDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.addedDate, 100), 7)) as addedTimeOnly,
Convert(varchar,D.modifyDate,103) as modifyDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.modifyDate, 100), 7)) as modifyTimeOnly
From documents D
where D.docTypID Not IN (2,22) And D.fldrID IN (Select UF.fldrID From userFolders UF Where UF.userID=@UserID)
And (D.docName Like '%' + @SearchText + '%' OR D.meta1 Like '%' + @SearchText + '%'
     OR D.meta2 Like '%' + @SearchText + '%' OR D.meta3 Like '%' + @SearchText + '%'
	 OR D.meta4 Like '%' + @SearchText + '%')

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OutgoingAndIncomingDocumentsSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[OutgoingAndIncomingDocumentsSearch]
GO
--[dbo].[OutgoingAndIncomingDocumentsSearch] 1,1,ج
Create Proc [dbo].[OutgoingAndIncomingDocumentsSearch]
@UserID int,
@lang nvarchar(10),
@SearchText nvarchar(500)
as
Select D.docID,D.docName,
(Select Case When @lang=0 Then F.fldrName Else F.fldrNameAr End From folders F Where F.fldrID=D.fldrID) As FolderName,
(Select Case When @lang=0 Then DT.docTypDesc Else DT.docTypDescAr End From docTypes DT Where DT.docTypID=D.docTypID) As DocTypeName,
D.addedDate,(select fullname from users where userid=D.addedUserID)AddedBy,
D.modifyDate,
Convert(varchar,D.addedDate,103) as addedDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.addedDate, 100), 7)) as addedTimeOnly,
Convert(varchar,D.modifyDate,103) as modifyDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.modifyDate, 100), 7)) as modifyTimeOnly
From documents D
where D.docTypID in(2,22) And D.fldrID IN (Select UF.fldrID From userFolders UF Where UF.userID=@UserID)
And (D.docName Like '%' + @SearchText + '%' OR D.meta1 Like '%' + @SearchText + '%'
     OR D.meta2 Like '%' + @SearchText + '%' OR D.meta3 Like '%' + @SearchText + '%'
	 OR D.meta4 Like '%' + @SearchText + '%')

GO

