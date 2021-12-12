﻿USE [HudHud]
GO
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ToDoListComments ADD
	IsDeleted bit NULL
GO
ALTER TABLE dbo.ToDoListComments ADD CONSTRAINT
	DF_ToDoListComments_IsDeleted DEFAULT 0 FOR IsDeleted
GO
ALTER TABLE dbo.ToDoListComments SET (LOCK_ESCALATION = TABLE)
GO
COMMIT