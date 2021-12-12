USE [HudHudDB]
GO

/****** Object:  Table [dbo].[ToDoList]    Script Date: 2/17/2018 11:06:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ToDoList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskName] [nvarchar](400) NULL,
	[TaskDate] [datetime] NULL,
	[AssignTo] [int] NULL,
	[CreatedBy] [int] NULL,
	[TaskType] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[IsComplete] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ToDoList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

