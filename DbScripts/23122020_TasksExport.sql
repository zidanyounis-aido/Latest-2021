CREATE FUNCTION dbo.SplitString
(
  @List     nvarchar(max),
  @Delim    nvarchar(255)
)
RETURNS TABLE WITH SCHEMABINDING
AS
   RETURN ( WITH n(n) AS (SELECT 1 UNION ALL SELECT n+1 
       FROM n WHERE n <= LEN(@List))
       SELECT [Value] = SUBSTRING(@List, n, 
       CHARINDEX(@Delim, @List + @Delim, n) - n)
       FROM n WHERE n <= LEN(@List)
      AND SUBSTRING(@Delim + @List, n, DATALENGTH(@Delim)/2) = @Delim
   );
GO
--Select * From dbo.SplitString('red,green,blue,yellow',',')
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getTasksList]') AND type in (N'P', N'PC'))
BEGIN
drop PROCEDURE [dbo].[getTasksList] 
END
GO
--[getTasksList] '1,2,3'
Create procedure [dbo].[getTasksList] 
@TaskIDs nvarchar(max) 
as 
SELECT 
  
  todoList.[Id], 
  todoList.[Description],
  createdbyUser.fullName as 'CreatedBy',
  assignToUser.fullName as 'AssignTo',
  todoList.[TaskDate] as 'TaskDate',
  todoList.[TaskName] as 'TaskName',
  taskType.ArText as 'TaskType',
  todoList.IsComplete
     
  FROM [dbo].[ToDoList] as todoList
  INNER JOIN [dbo].[TaskTypes] taskType on todoList.TaskType = taskType.Id
  LEFT JOIN [dbo].[users] as createdbyUser on todoList.CreatedBy = createdbyUser.[userID]
  LEFT JOIN [dbo].[users] as assignToUser on todoList.AssignTo = assignToUser.[userID]
  Where todoList.[Id] IN (Select Value From dbo.SplitString(@TaskIDs,','))
GO


