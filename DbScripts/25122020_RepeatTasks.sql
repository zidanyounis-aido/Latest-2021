--Select * From ToDoList order by id desc
Alter Table ToDoList Add CompleteDate Datetime
GO
Alter Table ToDoList Add RepeatType nvarchar(10)
GO
Alter Table ToDoList Add RepeatWeekDays nvarchar(100)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getToDoListCalender]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getToDoListCalender]
GO
--getToDoListCalender 1,'12/1/2020','1/30/2021'
Create procedure [dbo].[getToDoListCalender] 
@userId int,
@start datetime,
@end datetime
as 
Declare @Tasks table (Id int, TaskName nvarchar(400), TaskDate datetime, AssignTo int, CreatedBy int, TaskType int, CreatedOn datetime, IsComplete bit, IsDeleted bit, Description nvarchar(max), DocumentId bigint,CompleteDate datetime,RepeatType nvarchar(10),RepeatWeekDays nvarchar(100),IsMainTable bit)
Insert Into @Tasks (Id, TaskName, TaskDate, AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,IsMainTable)
SELECT Id, TaskName, TaskDate, AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,1
FROM dbo.ToDoList where TaskDate IS NOT NULL and IsComplete = 0 and IsDeleted = 0 and (CreatedBy= @userId or AssignTo=@userId)
And (TaskDate between @start And @end
     OR (RepeatType is not null And RepeatType<>'' And (IsComplete = 0 OR IsComplete Is Null) And TaskDate<@start) 
	 OR (RepeatType is not null And RepeatType<>'' And IsComplete=1 And CompleteDate between @start And @end))
Declare @date datetime
Set @date=@start 
while @date <= @end 
Begin
	Insert Into @Tasks (Id, TaskName, TaskDate, AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,IsMainTable)
	SELECT Id, TaskName, CONVERT(varchar, @date, 101) + ' ' + CONVERT(varchar, TaskDate, 108), AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,0 
	FROM @Tasks Where IsMainTable=1 And RepeatType is not null And RepeatType<>'' 
	And @date>TaskDate  And (CompleteDate is null OR IsComplete is null OR IsComplete=0 OR @date<=CompleteDate)
	And ((RepeatType='Daily')
	  OR (RepeatType='Weekly' And DATENAME(DW,TaskDate)=DATENAME(DW,@date))
	  OR (RepeatType='WeekDays' And DATENAME(DW,@date) IN (Select Value From dbo.SplitString(RepeatWeekDays,',')))
	  OR (RepeatType='Monthly' And DatePart(DAY,TaskDate)=DATENAME(DAY,@date) And DatePart(MONTH,TaskDate)=DatePart(MONTH,@date))
	  OR (RepeatType='Yearly' And DatePart(DAYOFYEAR,TaskDate)=DatePart(DAYOFYEAR,@date))
	    )
	Set @date=DateAdd(day,1,@date)
End
Select * From @Tasks 
GO

