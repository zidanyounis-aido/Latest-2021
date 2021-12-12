USE [hudhud]
GO
/****** Object:  Table [dbo].[type]    Script Date: 11/18/2020 12:30:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[type](
	[typeId] [int] NOT NULL,
	[typeNameAr] [nvarchar](50) NULL,
	[typeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_type] PRIMARY KEY CLUSTERED 
(
	[typeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[type] ([typeId], [typeNameAr], [typeName]) VALUES (1, N'الصادر', N'Outcoming')
GO
INSERT [dbo].[type] ([typeId], [typeNameAr], [typeName]) VALUES (2, N'الوارد', N'Incoming')
GO
