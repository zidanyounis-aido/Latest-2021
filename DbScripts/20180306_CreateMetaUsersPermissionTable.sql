USE [HudHud]
GO

/****** Object:  Table [dbo].[metaUsersPermissions]    Script Date: 3/6/2018 3:43:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[metaUsersPermissions](
	[metaID] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[allowRead] [bit] NULL,
	[allowEdit] [bit] NULL,
 CONSTRAINT [PK_metaUsersPermissions] PRIMARY KEY CLUSTERED 
(
	[metaID] ASC,
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

