CREATE TABLE [dbo].[sysSettings](
	[ID] [smallint] NOT NULL,
	[setting] [varchar](100) NULL,
	[value] [nvarchar](4000) NULL,
	[description] [nvarchar](4000) NULL,
 CONSTRAINT [PK_sysSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
