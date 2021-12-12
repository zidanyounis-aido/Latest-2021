CREATE TABLE [dbo].[usersRemiders](
	[reminderID] [bigint] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[metaID] [int] NULL,
	[docID] [bigint] NULL,
	[beforedays] [int] NULL,
 CONSTRAINT [PK_usersRemiders] PRIMARY KEY CLUSTERED 
(
	[reminderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

