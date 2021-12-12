USE [hudhud]
GO
/****** Object:  Table [dbo].[programs]    Script Date: 6/2/2020 9:42:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Table [dbo].[userPrograms]    Script Date: 6/2/2020 9:42:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET IDENTITY_INSERT [dbo].[programs] ON 
GO
INSERT [dbo].[programs] ([programID], [programName], [parentProgramID], [programURL], [windowWidth], [windowHeight], [programNameAr], [iconCss], [orderNum]) VALUES (33, N'NewPage', 0, N'NewPage', 0, 0, N'الصفحة الجديدة', N'tachometer-alt', 20)

GO
INSERT [dbo].[userPrograms] ([userID], [programID]) VALUES (13, 32)
GO
