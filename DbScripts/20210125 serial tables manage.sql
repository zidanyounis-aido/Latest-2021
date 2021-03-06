USE [hudhud]
GO
/****** Object:  Table [dbo].[IngoingOutgoingSerials]    Script Date: 2/25/2021 1:10:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngoingOutgoingSerials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerialCode] [nvarchar](500) NULL,
	[Serial] [nvarchar](500) NULL,
	[FolderId] [int] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_IngoingOutgoingSerials] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[IngoingOutgoingSerials] ON 

INSERT [dbo].[IngoingOutgoingSerials] ([Id], [SerialCode], [Serial], [FolderId], [Type]) VALUES (1, N'code,id,text,yy', N'#,id,ING,yy', 0, 0)
INSERT [dbo].[IngoingOutgoingSerials] ([Id], [SerialCode], [Serial], [FolderId], [Type]) VALUES (2, NULL, NULL, 0, 1)
SET IDENTITY_INSERT [dbo].[IngoingOutgoingSerials] OFF
GO
ALTER TABLE [dbo].[IngoingOutgoingSerials] ADD  CONSTRAINT [DF_IngoingOutgoingSerials_Type]  DEFAULT ((0)) FOR [Type]
GO
