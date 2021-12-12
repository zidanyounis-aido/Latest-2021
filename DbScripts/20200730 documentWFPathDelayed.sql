USE [hudhud]
GO

/****** Object:  Table [dbo].[documentWFPathDelayed]    Script Date: 8/3/2020 9:06:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[documentWFPathDelayed](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[docID] [bigint] NULL,
	[userID] [int] NULL,
	[actionDateTime] [datetime] NULL,
	[wfPathID] [int] NULL,
	[wfSeqNo] [smallint] NULL,
	[actionType] [smallint] NULL,
	[recipientType] [smallint] NULL,
	[userNotes] [ntext] NULL,
	[receiveDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[inboxType] [int] NULL,
	[documentWFPathId] [int] NULL,
 CONSTRAINT [PK_documentWFPathDelayed] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


