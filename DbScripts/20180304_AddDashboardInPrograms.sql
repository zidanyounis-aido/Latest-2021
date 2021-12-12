
INSERT INTO [dbo].[programs]
           ([programName]
           ,[parentProgramID]
           ,[programURL]
           ,[windowWidth]
           ,[windowHeight]
           ,[programNameAr])
     VALUES
           ('Dashboard'
           ,0
           ,'Dashboard'
           ,0
           ,0
           ,N'لوحة القيادة')
GO


insert into [dbo].[userPrograms] ([userID],[programID])
select u.userid,p.programID from users u, programs p where p.programURL = 'dashboard'