USE [HudHudDB]
GO

/****** Object:  Table [dbo].[ToDoListComments]    Script Date: 2/17/2018 11:08:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ToDoListComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToDoListId] [int] NULL,
	[CommentText] [nvarchar](450) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ToDoListComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


