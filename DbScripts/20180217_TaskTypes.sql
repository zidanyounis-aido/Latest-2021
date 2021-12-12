USE [HudHudDB]
GO

/****** Object:  Table [dbo].[TaskTypes]    Script Date: 2/17/2018 11:08:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TaskTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArText] [nvarchar](100) NULL,
	[EnText] [nvarchar](100) NULL,
	[Code] [nvarchar](100) NULL,
 CONSTRAINT [PK_TaskTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


