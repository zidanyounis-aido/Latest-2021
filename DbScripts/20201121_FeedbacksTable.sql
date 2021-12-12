Create Table Feedbacks(
FeedbackID int identity(1,1) Primary Key,
UserID int References users(userID),
FeedbackMessage nvarchar(max),
PageURL nvarchar(100),
CDate DateTime Default(GetDate())
)
GO

IF EXISTS(SELECT * FROM sys.procedures WHERE NAME LIKE 'InsertFeedbacks')
   DROP PROCEDURE InsertFeedbacks
GO

Create Procedure InsertFeedbacks
@UserID int,
@FeedbackMessage nvarchar(max),
@PageURL nvarchar(100),
@CDate datetime
as
INSERT INTO Feedbacks([UserID],[FeedbackMessage],[PageURL],[CDate])
     VALUES(@UserID,@FeedbackMessage,@PageURL,@CDate)
Select SCOPE_IDENTITY()
GO

Select * From Feedbacks
