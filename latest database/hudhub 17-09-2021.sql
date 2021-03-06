USE [hudhud]
GO
/****** Object:  Schema [imprintadmin]    Script Date: 9/17/2021 11:48:26 PM ******/
CREATE SCHEMA [imprintadmin]
GO
/****** Object:  Schema [zyadmin]    Script Date: 9/17/2021 11:48:26 PM ******/
CREATE SCHEMA [zyadmin]
GO
/****** Object:  UserDefinedFunction [dbo].[getUserPosition]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getUserPosition] 
(
	@userID int
)
RETURNS nvarchar(4000)
AS
BEGIN
return (SELECT    ( dbo.positions.positionTitleAr
+ N' - قسم ' +  dbo.companies.companyNameAr + ' - ' + dbo.branchs.branchNameAr ) as userFullName
FROM         dbo.users INNER JOIN
                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
	Where
	userID = @userID)

END




GO
/****** Object:  Table [dbo].[branchFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[branchFolders](
	[branchID] [int] NOT NULL,
	[fldrID] [int] NOT NULL,
 CONSTRAINT [PK_branchFolders] PRIMARY KEY CLUSTERED 
(
	[branchID] ASC,
	[fldrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[branchs]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[branchs](
	[branchID] [int] IDENTITY(1,1) NOT NULL,
	[companyID] [int] NULL,
	[branchName] [nvarchar](500) NULL,
	[address] [nvarchar](1500) NULL,
	[tel1] [varchar](50) NULL,
	[tel2] [varchar](50) NULL,
	[zipcode] [varchar](50) NULL,
	[mainEmail] [varchar](500) NULL,
	[description] [nvarchar](4000) NULL,
	[isMainBranch] [bit] NULL,
	[branchNameAr] [nvarchar](500) NULL,
 CONSTRAINT [PK_branchs] PRIMARY KEY CLUSTERED 
(
	[branchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[browseingEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[browseingEvents](
	[browseEventID] [int] IDENTITY(1,1) NOT NULL,
	[sysEventID] [int] NULL,
	[pageID] [int] NULL,
 CONSTRAINT [PK_browseingEvents] PRIMARY KEY CLUSTERED 
(
	[browseEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[ClientName] [nvarchar](250) NULL,
	[ClientEmail] [nvarchar](250) NULL,
	[CountryId] [int] NULL,
	[DefualtLanguageID] [int] NULL,
	[ClientPhone] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Clienta] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[companies]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[companies](
	[companyID] [int] IDENTITY(1,1) NOT NULL,
	[companyName] [nvarchar](500) NULL,
	[address] [nvarchar](1500) NULL,
	[tel1] [varchar](50) NULL,
	[tel2] [varchar](50) NULL,
	[zipcode] [varchar](50) NULL,
	[mainEmail] [varchar](500) NULL,
	[description] [nvarchar](4000) NULL,
	[companyNameAr] [nvarchar](500) NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_companies] PRIMARY KEY CLUSTERED 
(
	[companyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[companyFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[companyFolders](
	[companyID] [int] NOT NULL,
	[fldrID] [int] NOT NULL,
 CONSTRAINT [PK_companyFolders] PRIMARY KEY CLUSTERED 
(
	[companyID] ASC,
	[fldrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[controlsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[controlsTypes](
	[crtlID] [int] NOT NULL,
	[ctrlDesc] [varchar](100) NULL,
	[ctrlDescAr] [nvarchar](100) NULL,
 CONSTRAINT [PK_controlsTypes] PRIMARY KEY CLUSTERED 
(
	[crtlID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dataBaseEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dataBaseEvents](
	[DBEventID] [int] IDENTITY(1,1) NOT NULL,
	[sysEventID] [int] NULL,
	[DBActionTypeID] [int] NULL,
	[tableName] [varchar](50) NULL,
	[parameters] [nvarchar](4000) NULL,
 CONSTRAINT [PK_dataBaseEvents] PRIMARY KEY CLUSTERED 
(
	[DBEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DBActionsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBActionsTypes](
	[DBActionTypeID] [int] NOT NULL,
	[DBActionTypeDescA] [nvarchar](50) NULL,
	[FBActionTypeDescE] [varchar](50) NULL,
 CONSTRAINT [PK_DBActionsTypes] PRIMARY KEY CLUSTERED 
(
	[DBActionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[departments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departments](
	[departmentID] [int] IDENTITY(1,1) NOT NULL,
	[departmentName] [nvarchar](1000) NULL,
	[headUserID] [int] NULL,
	[parentDepartmentID] [int] NULL,
	[departmentNameAr] [nvarchar](1000) NULL,
	[parentID] [int] NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_departments] PRIMARY KEY CLUSTERED 
(
	[departmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[docTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docTypes](
	[docTypID] [int] IDENTITY(1,1) NOT NULL,
	[docTypDesc] [nvarchar](500) NULL,
	[active] [bit] NULL,
	[defaultWFID] [int] NULL,
	[docTypDescAr] [nvarchar](500) NULL,
	[isTemplate] [bit] NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_docTypes] PRIMARY KEY CLUSTERED 
(
	[docTypID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documentMataValues]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documentMataValues](
	[metaID] [int] NOT NULL,
	[docID] [bigint] NOT NULL,
	[value] [nvarchar](4000) NULL,
 CONSTRAINT [PK_documentMatasVersions] PRIMARY KEY CLUSTERED 
(
	[metaID] ASC,
	[docID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documents](
	[docID] [bigint] IDENTITY(1,1) NOT NULL,
	[docTypID] [int] NULL,
	[docName] [nvarchar](500) NULL,
	[docExt] [varchar](4) NULL,
	[addedDate] [datetime] NULL,
	[addedUserID] [int] NULL,
	[lastVersion] [smallint] NULL,
	[modifyDate] [datetime] NULL,
	[modifyUserID] [int] NULL,
	[fldrID] [int] NULL,
	[ocrContent] [ntext] NULL,
	[folderSeq] [bigint] NULL,
	[docTypeSeq] [bigint] NULL,
	[folderDocTypeSeq] [bigint] NULL,
	[wfPathID] [int] NULL,
	[wfCurrentSeq] [smallint] NULL,
	[wfCurrentRecipientID] [int] NULL,
	[wfCurrentRecipientType] [smallint] NULL,
	[wfStartDateTime] [datetime] NULL,
	[wfTimeFrame] [decimal](18, 0) NULL,
	[wfStatus] [smallint] NULL,
	[meta1] [nvarchar](4000) NULL,
	[meta2] [nvarchar](4000) NULL,
	[meta3] [nvarchar](4000) NULL,
	[meta4] [nvarchar](4000) NULL,
	[meta5] [nvarchar](4000) NULL,
	[meta6] [nvarchar](4000) NULL,
	[meta7] [nvarchar](4000) NULL,
	[meta8] [nvarchar](4000) NULL,
	[meta9] [nvarchar](4000) NULL,
	[meta10] [nvarchar](4000) NULL,
	[meta11] [nvarchar](4000) NULL,
	[meta12] [nvarchar](4000) NULL,
	[meta13] [nvarchar](4000) NULL,
	[meta14] [nvarchar](4000) NULL,
	[meta15] [nvarchar](4000) NULL,
	[meta16] [nvarchar](4000) NULL,
	[meta17] [nvarchar](4000) NULL,
	[meta18] [nvarchar](4000) NULL,
	[meta19] [nvarchar](4000) NULL,
	[meta20] [nvarchar](4000) NULL,
	[meta21] [nvarchar](4000) NULL,
	[meta22] [nvarchar](4000) NULL,
	[meta23] [nvarchar](4000) NULL,
	[meta24] [nvarchar](4000) NULL,
	[meta25] [nvarchar](4000) NULL,
	[meta26] [nvarchar](4000) NULL,
	[meta27] [nvarchar](4000) NULL,
	[meta28] [nvarchar](4000) NULL,
	[meta29] [nvarchar](4000) NULL,
	[meta30] [nvarchar](4000) NULL,
	[statusId] [int] NULL,
	[submitDate] [datetime] NULL,
	[DelayTime] [int] NULL,
	[typeId] [int] NULL,
	[serial] [nvarchar](200) NULL,
	[outgoingDate] [datetime] NULL,
 CONSTRAINT [PK_documents] PRIMARY KEY CLUSTERED 
(
	[docID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documentsGroups]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documentsGroups](
	[docGroupID] [int] IDENTITY(1,1) NOT NULL,
	[docGroupTitle] [nvarchar](4000) NULL,
	[docTypeID] [int] NULL,
 CONSTRAINT [PK_documentsGroups] PRIMARY KEY CLUSTERED 
(
	[docGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documentsStatus]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documentsStatus](
	[statusId] [int] NOT NULL,
	[statusName] [nvarchar](50) NULL,
	[statusNameEN] [nvarchar](50) NULL,
	[color] [nvarchar](50) NULL,
 CONSTRAINT [PK_documentsStatus] PRIMARY KEY CLUSTERED 
(
	[statusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documentVersions]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documentVersions](
	[docID] [bigint] NOT NULL,
	[version] [smallint] NOT NULL,
	[addedDate] [datetime] NULL,
	[addedUserID] [int] NULL,
	[ext] [varchar](4) NULL,
	[docGroupID] [int] NULL,
	[FileName] [nvarchar](200) NULL,
	[DocumentFileName] [nvarchar](50) NULL,
 CONSTRAINT [PK_documentVersions] PRIMARY KEY CLUSTERED 
(
	[docID] ASC,
	[version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documentWFPath]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documentWFPath](
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
	[isRemoved] [bit] NULL,
 CONSTRAINT [PK_documentWFPath] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documentWFPathDelayed]    Script Date: 9/17/2021 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[eFormFields]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eFormFields](
	[formID] [int] NOT NULL,
	[fieldSeq] [int] NOT NULL,
	[metaDesc] [nvarchar](500) NULL,
	[metaDataType] [varchar](50) NULL,
	[required] [bit] NULL,
	[orderSeq] [int] NULL,
	[ctrlID] [int] NULL,
	[defaultTexts] [nvarchar](4000) NULL,
	[defaultValues] [nvarchar](4000) NULL,
	[visible] [bit] NULL,
 CONSTRAINT [PK_eFormFields] PRIMARY KEY CLUSTERED 
(
	[formID] ASC,
	[fieldSeq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[eForms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eForms](
	[formID] [int] IDENTITY(1,1) NOT NULL,
	[formName] [nvarchar](500) NULL,
	[defaultPathID] [int] NULL,
	[active] [bit] NULL,
	[catID] [int] NULL,
	[catPrgID] [int] NULL,
	[formNameAr] [nvarchar](500) NULL,
 CONSTRAINT [PK_eForms] PRIMARY KEY CLUSTERED 
(
	[formID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[eformsCategories]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eformsCategories](
	[catID] [int] IDENTITY(1,1) NOT NULL,
	[catTitle] [nvarchar](500) NULL,
	[catPrgID] [int] NULL,
 CONSTRAINT [PK_eformsCategories] PRIMARY KEY CLUSTERED 
(
	[catID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[eformWFPath]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eformWFPath](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[docID] [int] NULL,
	[userID] [int] NULL,
	[actionUserID] [int] NULL,
	[actionDateTime] [datetime] NULL,
	[wfPathID] [int] NULL,
	[wfSeqNo] [smallint] NULL,
	[actionType] [smallint] NULL,
	[recipientType] [smallint] NULL,
 CONSTRAINT [PK_eformWFPath] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[event]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[event](
	[event_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](4000) NULL,
	[description] [nvarchar](4000) NULL,
	[event_start] [datetime] NULL,
	[event_end] [datetime] NULL,
	[all_day] [bit] NULL,
	[CreatedBy] [int] NULL,
	[Color] [nvarchar](10) NULL,
	[DocumentId] [bigint] NULL,
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[eventsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eventsTypes](
	[eventTypeID] [int] NOT NULL,
	[eventTypeDescA] [nvarchar](50) NULL,
	[eventTypeDescE] [varchar](50) NULL,
 CONSTRAINT [PK_eventsTypes] PRIMARY KEY CLUSTERED 
(
	[eventTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[folders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[folders](
	[fldrID] [int] IDENTITY(1,1) NOT NULL,
	[fldrName] [nvarchar](500) NULL,
	[fldrParentID] [int] NULL,
	[active] [bit] NULL,
	[iconID] [int] NULL,
	[defaultDocTypID] [int] NULL,
	[folderOrder] [int] NULL,
	[isDiwan] [bit] NULL,
	[fldrNameAr] [nvarchar](500) NULL,
	[allowWF] [bit] NOT NULL,
	[folderOwner] [int] NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_folders] PRIMARY KEY CLUSTERED 
(
	[fldrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groupBlocks]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groupBlocks](
	[grpID] [int] NOT NULL,
	[docTypID] [int] NOT NULL,
	[blockNum] [int] NOT NULL,
	[blockLeft] [varchar](6) NULL,
	[blockTop] [varchar](6) NULL,
	[blockWidth] [varchar](6) NULL,
	[blockHeight] [varchar](6) NULL,
 CONSTRAINT [PK_groupBlocks] PRIMARY KEY CLUSTERED 
(
	[grpID] ASC,
	[docTypID] ASC,
	[blockNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groupFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groupFolders](
	[grpID] [int] NOT NULL,
	[fldrID] [int] NOT NULL,
	[allowInsert] [bit] NULL,
	[allowUpdate] [bit] NULL,
	[allowDelete] [bit] NULL,
	[allowCreateFldr] [bit] NULL,
	[allowRenameFldr] [bit] NULL,
	[allowRelocationFldr] [bit] NULL,
	[inheritSubFolders] [bit] NULL,
 CONSTRAINT [PK_groupFolders] PRIMARY KEY CLUSTERED 
(
	[grpID] ASC,
	[fldrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groupPrograms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groupPrograms](
	[groupID] [int] NOT NULL,
	[programID] [int] NOT NULL,
 CONSTRAINT [PK_groupPrograms] PRIMARY KEY CLUSTERED 
(
	[groupID] ASC,
	[programID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[grpID] [int] IDENTITY(1,1) NOT NULL,
	[grpDesc] [nvarchar](500) NULL,
	[companyID] [int] NULL,
	[branchID] [int] NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_groups] PRIMARY KEY CLUSTERED 
(
	[grpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[icons]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[icons](
	[iconID] [int] IDENTITY(1,1) NOT NULL,
	[iconDescription] [nvarchar](500) NULL,
 CONSTRAINT [PK_icons] PRIMARY KEY CLUSTERED 
(
	[iconID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngoingOutgoingSerials]    Script Date: 9/17/2021 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[Languages]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageId] [int] IDENTITY(1,1) NOT NULL,
	[LanguageName] [nvarchar](50) NULL,
	[LanguageISOCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[loginEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loginEvents](
	[loginID] [int] IDENTITY(1,1) NOT NULL,
	[sysEventID] [int] NULL,
	[IPAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_loginEvents] PRIMARY KEY CLUSTERED 
(
	[loginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[metaGroupsPermissions]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[metaGroupsPermissions](
	[metaID] [int] NOT NULL,
	[grpID] [int] NOT NULL,
	[allowRead] [bit] NULL,
	[allowEdit] [bit] NULL,
 CONSTRAINT [PK_metaGroupsPermissions] PRIMARY KEY CLUSTERED 
(
	[metaID] ASC,
	[grpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[metas]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[metas](
	[metaID] [int] IDENTITY(1,1) NOT NULL,
	[docTypID] [int] NULL,
	[metaDesc] [nvarchar](500) NULL,
	[metaDataType] [varchar](50) NULL,
	[required] [bit] NULL,
	[orderSeq] [int] NULL,
	[ctrlID] [int] NULL,
	[defaultTexts] [nvarchar](4000) NULL,
	[defaultValues] [nvarchar](4000) NULL,
	[visible] [bit] NULL,
	[metaDescAr] [nvarchar](500) NULL,
	[columnSeq] [int] NULL,
	[permissionType] [nvarchar](20) NULL,
	[defaultArTexts] [nvarchar](4000) NULL,
	[metaIdFK] [int] NULL,
	[width] [float] NULL,
 CONSTRAINT [PK_metas] PRIMARY KEY CLUSTERED 
(
	[metaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[metaUsersPermissions]    Script Date: 9/17/2021 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[positions]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[positions](
	[positionID] [int] IDENTITY(1,1) NOT NULL,
	[positionTitle] [nvarchar](500) NULL,
	[positionTitleAr] [nvarchar](500) NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_positions] PRIMARY KEY CLUSTERED 
(
	[positionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[programs]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[programs](
	[programID] [int] IDENTITY(1,1) NOT NULL,
	[programName] [nvarchar](500) NULL,
	[parentProgramID] [int] NULL,
	[programURL] [varchar](2000) NULL,
	[windowWidth] [int] NULL,
	[windowHeight] [int] NULL,
	[programNameAr] [nvarchar](500) NULL,
	[iconCss] [varchar](500) NULL,
	[orderNum] [int] NULL,
	[svg] [nvarchar](max) NULL,
	[IsShowOnMobile] [bit] NULL,
 CONSTRAINT [PK_programs] PRIMARY KEY CLUSTERED 
(
	[programID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[settings]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[settings](
	[ID] [int] NOT NULL,
	[allowedUsersCount] [nvarchar](1000) NULL,
	[systemActive] [nvarchar](1000) NULL,
	[systemActiveDate] [nvarchar](4000) NULL,
	[passwordStrength] [smallint] NULL,
	[passwordAllowStartSpace] [bit] NULL,
	[passwordLength] [smallint] NULL,
	[allowUsernamePasswordMatch] [bit] NULL,
	[firstLoginChangePassword] [bit] NULL,
	[passwordAgeDays] [int] NULL,
	[sessionTimeoutMinutes] [int] NULL,
	[lockTimeOut] [smallint] NULL,
	[outgoingMailServer] [varchar](100) NULL,
	[workflowEmail] [varchar](100) NULL,
	[workflowEmailPassword] [nvarchar](4000) NULL,
	[systemEmail] [varchar](100) NULL,
	[systemEmailPassword] [nvarchar](4000) NULL,
	[workflowEmailSubject] [nvarchar](150) NULL,
	[workflowEmailBody] [ntext] NULL,
	[systemEmailSignature] [nvarchar](4000) NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_settings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SignatureTB]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SignatureTB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Signture] [nvarchar](max) NULL,
	[Documnet] [nvarchar](50) NULL,
	[UserId] [int] NULL,
	[Width] [nvarchar](50) NULL,
	[Height] [nvarchar](50) NULL,
	[Top] [nvarchar](50) NULL,
	[Left] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_SignatureTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysEvents](
	[sysEventID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[eventTypeID] [int] NULL,
	[eventDateTime] [datetime] NULL,
	[URL] [nvarchar](100) NULL,
 CONSTRAINT [PK_sysEvents] PRIMARY KEY CLUSTERED 
(
	[sysEventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysSettings]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysSettings](
	[ID] [smallint] NOT NULL,
	[setting] [varchar](100) NULL,
	[value] [nvarchar](4000) NULL,
	[description] [nvarchar](4000) NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_sysSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[ToDoList]    Script Date: 9/17/2021 11:48:26 PM ******/
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
	[DocumentId] [bigint] NULL,
	[lastModifiedByUserId] [int] NULL,
	[lastModifiedDateTime] [datetime] NULL,
	[CompleteDate] [datetime] NULL,
	[RepeatType] [nvarchar](10) NULL,
	[RepeatWeekDays] [nvarchar](100) NULL,
 CONSTRAINT [PK_ToDoList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToDoListComments]    Script Date: 9/17/2021 11:48:26 PM ******/
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
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_ToDoListComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[type]    Script Date: 9/17/2021 11:48:26 PM ******/
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
/****** Object:  Table [dbo].[userDocuments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userDocuments](
	[userID] [int] NOT NULL,
	[docID] [bigint] NOT NULL,
	[allow] [bit] NULL,
	[allowInsert] [bit] NULL,
	[allowUpdate] [bit] NULL,
	[allowDelete] [bit] NULL,
 CONSTRAINT [PK_userDocuments] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[docID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userFolders](
	[userID] [int] NOT NULL,
	[fldrID] [int] NOT NULL,
	[allow] [bit] NULL,
	[allowInsert] [bit] NULL,
	[allowUpdate] [bit] NULL,
	[allowDelete] [bit] NULL,
	[allowCreateFldr] [bit] NULL,
	[allowRenameFldr] [bit] NULL,
	[allowRelocationFldr] [bit] NULL,
	[inheritSubFolders] [bit] NULL,
 CONSTRAINT [PK_userFolders] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[fldrID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userFormFields]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userFormFields](
	[userID] [int] NOT NULL,
	[formID] [int] NOT NULL,
	[fieldSeq] [int] NOT NULL,
	[value] [ntext] NULL,
 CONSTRAINT [PK_userFormFields] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[formID] ASC,
	[fieldSeq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userPrograms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userPrograms](
	[userID] [int] NOT NULL,
	[programID] [int] NOT NULL,
 CONSTRAINT [PK_userPrograms] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[programID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NULL,
	[password] [nvarchar](4000) NULL,
	[fullName] [nvarchar](500) NULL,
	[grpID] [int] NULL,
	[active] [bit] NULL,
	[companyID] [int] NULL,
	[branchID] [int] NULL,
	[departmentID] [int] NULL,
	[positionID] [int] NULL,
	[email] [varchar](500) NULL,
	[allowCustomWF] [bit] NULL,
	[allowCreateFolders] [bit] NULL,
	[allowReplaceDocuments] [bit] NULL,
	[allowDiwan] [bit] NULL,
	[isFirstLogin] [bit] NULL,
	[passwordCreationDate] [datetime] NULL,
	[passwordModifiedDate] [datetime] NULL,
	[lastPassword] [nvarchar](4000) NULL,
	[Signature] [nvarchar](max) NULL,
	[Phone] [nvarchar](50) NULL,
	[isMobileFirstLogin] [bit] NULL,
	[isEmailVerfied] [bit] NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usersForms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usersForms](
	[userID] [int] NOT NULL,
	[formID] [int] NOT NULL,
	[pathID] [int] NULL,
	[submitDateTime] [datetime] NULL,
	[status] [smallint] NULL,
 CONSTRAINT [PK_usersForms] PRIMARY KEY CLUSTERED 
(
	[userID] ASC,
	[formID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usersRemiders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usersRemiders](
	[reminderID] [bigint] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[metaID] [int] NULL,
	[docID] [bigint] NULL,
	[beforedays] [int] NULL,
	[isRemoved] [bit] NULL,
 CONSTRAINT [PK_usersRemiders] PRIMARY KEY CLUSTERED 
(
	[reminderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wfPathDetails]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wfPathDetails](
	[pathID] [int] NOT NULL,
	[seqNo] [smallint] NOT NULL,
	[recipientID] [int] NULL,
	[endOfPath] [bit] NULL,
	[recipientType] [smallint] NULL,
	[companyID] [int] NULL,
	[branchID] [int] NULL,
	[approveType] [smallint] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[duration] [int] NULL,
	[durationType] [int] NULL,
 CONSTRAINT [PK_wfPathDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workFlowPaths]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workFlowPaths](
	[pathId] [int] IDENTITY(1,1) NOT NULL,
	[pathDesc] [nvarchar](500) NULL,
	[fldrId] [int] NULL,
	[docTypId] [int] NULL,
	[pathDescAr] [nvarchar](500) NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_workFlowPaths] PRIMARY KEY CLUSTERED 
(
	[pathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetDocMetaValue]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetDocMetaValue]
(	
	@DocID int,
	@SeqNo int
)
RETURNS TABLE 
AS
RETURN 
(
	with documentMataValues AS(select row_number() over(order by metaID) as 'row', * 
                from dbo.documentMataValues Where docID = @DocID)
 select top 1 [value] from documentMataValues
where row=@SeqNo
)




GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]
(
  @List     nvarchar(max),
  @Delim    nvarchar(255)
)
RETURNS TABLE WITH SCHEMABINDING
AS
   RETURN ( WITH n(n) AS (SELECT 1 UNION ALL SELECT n+1 
       FROM n WHERE n <= LEN(@List))
       SELECT [Value] = SUBSTRING(@List, n, 
       CHARINDEX(@Delim, @List + @Delim, n) - n)
       FROM n WHERE n <= LEN(@List)
      AND SUBSTRING(@Delim + @List, n, DATALENGTH(@Delim)/2) = @Delim
   );
GO
/****** Object:  View [dbo].[showAllBrowsingEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[showAllBrowsingEvents]
AS
SELECT     dbo.users.userID, dbo.users.fullName, dbo.sysEvents.eventDateTime, dbo.sysEvents.URL, dbo.programs.programName
FROM         dbo.sysEvents INNER JOIN
                      dbo.users ON dbo.sysEvents.userID = dbo.users.userID INNER JOIN
                      dbo.browseingEvents ON dbo.sysEvents.sysEventID = dbo.browseingEvents.sysEventID INNER JOIN
                      dbo.programs ON dbo.browseingEvents.pageID = dbo.programs.programID




GO
/****** Object:  View [dbo].[showAllDBEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[showAllDBEvents]
AS
SELECT     dbo.users.userID, dbo.users.fullName, dbo.sysEvents.eventDateTime, dbo.sysEvents.URL, dbo.DBActionsTypes.FBActionTypeDescE, dbo.dataBaseEvents.tableName, 
                      dbo.dataBaseEvents.parameters
FROM         dbo.sysEvents INNER JOIN
                      dbo.dataBaseEvents ON dbo.sysEvents.sysEventID = dbo.dataBaseEvents.sysEventID INNER JOIN
                      dbo.DBActionsTypes ON dbo.dataBaseEvents.DBActionTypeID = dbo.DBActionsTypes.DBActionTypeID INNER JOIN
                      dbo.users ON dbo.sysEvents.userID = dbo.users.userID




GO
/****** Object:  View [dbo].[showAllLoginEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[showAllLoginEvents]
AS
SELECT     dbo.users.userID, dbo.users.fullName, dbo.sysEvents.eventDateTime, dbo.sysEvents.URL, dbo.loginEvents.IPAddress
FROM         dbo.sysEvents INNER JOIN
                      dbo.users ON dbo.sysEvents.userID = dbo.users.userID INNER JOIN
                      dbo.loginEvents ON dbo.sysEvents.sysEventID = dbo.loginEvents.sysEventID




GO
/****** Object:  View [dbo].[userReminders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[userReminders]
AS 
SELECT   dbo.usersRemiders.userID, dbo.usersRemiders.docID, dbo.documents.docName, dbo.metas.metaDesc, dbo.metas.metaDescAr, dbo.usersRemiders.isRemoved, 
                         dbo.usersRemiders.beforedays
FROM         dbo.usersRemiders INNER JOIN
                         dbo.metas ON dbo.usersRemiders.metaID = dbo.metas.metaID INNER JOIN
                         dbo.documents ON dbo.usersRemiders.docID = dbo.documents.docID
GO
ALTER TABLE [dbo].[companies] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[departments] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[docTypes] ADD  CONSTRAINT [DF_docTypes_isTemplate]  DEFAULT ((0)) FOR [isTemplate]
GO
ALTER TABLE [dbo].[docTypes] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[documents] ADD  CONSTRAINT [DF_documents_wfTimeFrame]  DEFAULT ((1)) FOR [wfTimeFrame]
GO
ALTER TABLE [dbo].[documents] ADD  CONSTRAINT [DF_documents_wfStatus]  DEFAULT ((0)) FOR [wfStatus]
GO
ALTER TABLE [dbo].[documentWFPath] ADD  CONSTRAINT [DF_documentWFPath_receiveDate]  DEFAULT (getdate()) FOR [receiveDate]
GO
ALTER TABLE [dbo].[folders] ADD  CONSTRAINT [DF_folders_isDiwan]  DEFAULT ((0)) FOR [isDiwan]
GO
ALTER TABLE [dbo].[folders] ADD  CONSTRAINT [DF_folders_allowWF]  DEFAULT ((0)) FOR [allowWF]
GO
ALTER TABLE [dbo].[folders] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[groups] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[IngoingOutgoingSerials] ADD  CONSTRAINT [DF_IngoingOutgoingSerials_Type]  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[positions] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[programs] ADD  DEFAULT ((0)) FOR [IsShowOnMobile]
GO
ALTER TABLE [dbo].[settings] ADD  CONSTRAINT [DF_settings_passwordAllowStartSpace]  DEFAULT ((0)) FOR [passwordAllowStartSpace]
GO
ALTER TABLE [dbo].[settings] ADD  CONSTRAINT [DF_settings_passwordLength]  DEFAULT ((8)) FOR [passwordLength]
GO
ALTER TABLE [dbo].[settings] ADD  CONSTRAINT [DF_settings_allowUsernamePasswordMatch]  DEFAULT ((0)) FOR [allowUsernamePasswordMatch]
GO
ALTER TABLE [dbo].[settings] ADD  CONSTRAINT [DF_settings_firstLoginChangePassword]  DEFAULT ((0)) FOR [firstLoginChangePassword]
GO
ALTER TABLE [dbo].[settings] ADD  CONSTRAINT [DF_settings_passwordAgeDays]  DEFAULT ((60)) FOR [passwordAgeDays]
GO
ALTER TABLE [dbo].[settings] ADD  CONSTRAINT [DF_settings_sessionTimeoutMinutes]  DEFAULT ((20)) FOR [sessionTimeoutMinutes]
GO
ALTER TABLE [dbo].[settings] ADD  CONSTRAINT [DF_settings_lockTimeOut]  DEFAULT ((4)) FOR [lockTimeOut]
GO
ALTER TABLE [dbo].[settings] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[sysSettings] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[ToDoListComments] ADD  CONSTRAINT [DF_ToDoListComments_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_isFirstLogin]  DEFAULT ((1)) FOR [isFirstLogin]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[workFlowPaths] ADD  DEFAULT ((0)) FOR [ClientId]
GO
ALTER TABLE [dbo].[eFormFields]  WITH CHECK ADD  CONSTRAINT [FK_eFormFields_eForms] FOREIGN KEY([formID])
REFERENCES [dbo].[eForms] ([formID])
GO
ALTER TABLE [dbo].[eFormFields] CHECK CONSTRAINT [FK_eFormFields_eForms]
GO
ALTER TABLE [dbo].[SignatureTB]  WITH CHECK ADD  CONSTRAINT [FK_SignatureTB_users] FOREIGN KEY([UserId])
REFERENCES [dbo].[users] ([userID])
GO
ALTER TABLE [dbo].[SignatureTB] CHECK CONSTRAINT [FK_SignatureTB_users]
GO
/****** Object:  StoredProcedure [dbo].[_addDocuments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[_addDocuments] 
@docTypID int,@docName nvarchar(500),@docExt varchar(4),@addedDate datetime,@addedUserID int,@lastVersion smallint,@modifyDate datetime,@modifyUserID int,@fldrID int,@ocrContent ntext,@folderSeq bigint,@docTypeSeq bigint,@folderDocTypeSeq bigint 
as 
begin Transaction
Insert Into dbo.documents(docTypID,docName,docExt,addedDate,addedUserID,lastVersion,
modifyDate,modifyUserID,fldrID,ocrContent,folderSeq,docTypeSeq,folderDocTypeSeq) 
values(@docTypID,@docName,@docExt,@addedDate,@addedUserID,@lastVersion,@modifyDate,
@modifyUserID,@fldrID,@ocrContent,
(select top(1) ISNULL(COUNT(docID),0) + 1 from dbo.documents where fldrID=@fldrID),
(select top(1) ISNULL(COUNT(docID),0) + 1 from dbo.documents where docTypID=@docTypID),
(select top(1) ISNULL(COUNT(docID),0) + 1 from dbo.documents where fldrID=@fldrID and docTypID=@docTypID));
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documents',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[addBranchFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addBranchFolders] 
@branchID int,@fldrID int 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(branchID) from dbo.branchFolders where @branchID=branchID and @fldrID=fldrID);
if @varID = 0
begin
Insert Into dbo.branchFolders(branchID,fldrID) values(@branchID,@fldrID);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.branchFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addBranchs]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addBranchs] 
@companyID int,@branchName nvarchar(500),@address nvarchar(1500),@tel1 varchar(50),@tel2 varchar(50),@zipcode varchar(50),@mainEmail varchar(500),@description nvarchar(4000),@isMainBranch bit,@branchNameAr nvarchar(500) 
as 
begin Transaction
Insert Into dbo.branchs(companyID,branchName,address,tel1,tel2,zipcode,mainEmail,description,isMainBranch,branchNameAr) values(@companyID,@branchName,@address,@tel1,@tel2,@zipcode,@mainEmail,@description,@isMainBranch,@branchNameAr);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.branchs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addBrowseingEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addBrowseingEvents] 
@sysEventID int,@pageID int 
as 
begin Transaction
Insert Into dbo.browseingEvents(sysEventID,pageID) values(@sysEventID,@pageID);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.browseingEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addCompanies]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[addCompanies] 
@companyName nvarchar(500),@address nvarchar(1500),@tel1 varchar(50),@tel2 varchar(50),@zipcode varchar(50),@mainEmail varchar(500),@description nvarchar(4000),@companyNameAr nvarchar(500),@clientId int
as 
begin Transaction
Insert Into dbo.companies(companyName,address,tel1,tel2,zipcode,mainEmail,description,companyNameAr,ClientId) values(@companyName,@address,@tel1,@tel2,@zipcode,@mainEmail,@description,@companyNameAr,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.companies',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[addCompanyFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addCompanyFolders] 
@companyID int,@fldrID int 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(companyID) from dbo.companyFolders where @companyID=companyID and @fldrID=fldrID);
if @varID = 0
begin
Insert Into dbo.companyFolders(companyID,fldrID) values(@companyID,@fldrID);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.companyFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addControlsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addControlsTypes] 
@crtlID int,@ctrlDesc varchar(100),@ctrlDescAr nvarchar(100) 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(crtlID) from dbo.controlsTypes where @crtlID=crtlID);
if @varID = 0
begin
Insert Into dbo.controlsTypes(crtlID,ctrlDesc,ctrlDescAr) values(@crtlID,@ctrlDesc,@ctrlDescAr);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.controlsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addDataBaseEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addDataBaseEvents] 
@sysEventID int,@DBActionTypeID int,@tableName varchar(50),@parameters nvarchar(4000) 
as 
begin Transaction
Insert Into dbo.dataBaseEvents(sysEventID,DBActionTypeID,tableName,parameters) values(@sysEventID,@DBActionTypeID,@tableName,@parameters);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.dataBaseEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addDBActionsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addDBActionsTypes] 
@DBActionTypeID int,@DBActionTypeDescA nvarchar(50),@FBActionTypeDescE varchar(50) 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(DBActionTypeID) from dbo.DBActionsTypes where @DBActionTypeID=DBActionTypeID);
if @varID = 0
begin
Insert Into dbo.DBActionsTypes(DBActionTypeID,DBActionTypeDescA,FBActionTypeDescE) values(@DBActionTypeID,@DBActionTypeDescA,@FBActionTypeDescE);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.DBActionsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addDepartments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[addDepartments] 
@departmentName nvarchar(1000),@headUserID int,@parentDepartmentID int,@departmentNameAr nvarchar(1000) ,@parentID int ,@clientId int
as 
begin Transaction
Insert Into dbo.departments(departmentName,headUserID,parentDepartmentID,departmentNameAr,parentID,ClientId) values(@departmentName,@headUserID,@parentDepartmentID,@departmentNameAr,@parentID,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.departments',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[addDocTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[addDocTypes] 
@docTypDesc nvarchar(500),@active bit,@defaultWFID int,@docTypDescAr nvarchar(500),@clientId int
as 
begin Transaction
Insert Into dbo.docTypes(docTypDesc,active,defaultWFID,docTypDescAr,ClientId) values(@docTypDesc,@active,@defaultWFID,@docTypDescAr,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.docTypes',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[addDocumentMataValues]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addDocumentMataValues] 
@metaID int,@docID bigint,@value nvarchar(4000) 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(docID) from dbo.documentMataValues where @docID=docID and @metaID=metaID);
if @varID = 0
begin
Insert Into dbo.documentMataValues(metaID,docID,value) values(@metaID,@docID,@value);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentMataValues',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addDocuments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addDocuments] 
@docTypID int,@docName nvarchar(500),@docExt varchar(4),@addedDate datetime,@addedUserID int,@lastVersion smallint,@modifyDate datetime,@modifyUserID int,@fldrID int,@ocrContent ntext,@folderSeq bigint,@docTypeSeq bigint,@folderDocTypeSeq bigint,@wfPathID int,@wfCurrentSeq smallint,@wfCurrentRecipientID int,@wfCurrentRecipientType smallint,@wfStartDateTime datetime,@wfTimeFrame decimal(18,0),@wfStatus smallint,@meta1 nvarchar(4000),@meta2 nvarchar(4000),@meta3 nvarchar(4000),@meta4 nvarchar(4000),@meta5 nvarchar(4000),@meta6 nvarchar(4000),@meta7 nvarchar(4000),@meta8 nvarchar(4000),@meta9 nvarchar(4000),@meta10 nvarchar(4000),@meta11 nvarchar(4000),@meta12 nvarchar(4000),@meta13 nvarchar(4000),@meta14 nvarchar(4000),@meta15 nvarchar(4000),@meta16 nvarchar(4000),@meta17 nvarchar(4000),@meta18 nvarchar(4000),@meta19 nvarchar(4000),@meta20 nvarchar(4000),@meta21 nvarchar(4000),@meta22 nvarchar(4000),@meta23 nvarchar(4000),@meta24 nvarchar(4000),@meta25 nvarchar(4000),@meta26 nvarchar(4000),@meta27 nvarchar(4000),@meta28 nvarchar(4000),@meta29 nvarchar(4000),@meta30 nvarchar(4000) 
as 
begin Transaction
Insert Into dbo.documents(docTypID,docName,docExt,addedDate,addedUserID,lastVersion,modifyDate,modifyUserID,fldrID,ocrContent,folderSeq,docTypeSeq,folderDocTypeSeq,wfPathID,wfCurrentSeq,wfCurrentRecipientID,wfCurrentRecipientType,wfStartDateTime,wfTimeFrame,wfStatus,meta1,meta2,meta3,meta4,meta5,meta6,meta7,meta8,meta9,meta10,meta11,meta12,meta13,meta14,meta15,meta16,meta17,meta18,meta19,meta20,meta21,meta22,meta23,meta24,meta25,meta26,meta27,meta28,meta29,meta30) values(@docTypID,@docName,@docExt,@addedDate,@addedUserID,@lastVersion,@modifyDate,@modifyUserID,@fldrID,@ocrContent,@folderSeq,@docTypeSeq,@folderDocTypeSeq,@wfPathID,@wfCurrentSeq,@wfCurrentRecipientID,@wfCurrentRecipientType,@wfStartDateTime,@wfTimeFrame,@wfStatus,@meta1,@meta2,@meta3,@meta4,@meta5,@meta6,@meta7,@meta8,@meta9,@meta10,@meta11,@meta12,@meta13,@meta14,@meta15,@meta16,@meta17,@meta18,@meta19,@meta20,@meta21,@meta22,@meta23,@meta24,@meta25,@meta26,@meta27,@meta28,@meta29,@meta30);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addDocumentsGroups]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addDocumentsGroups] 
@docGroupTitle nvarchar(4000),@docTypeID int 
as 
begin Transaction
Insert Into dbo.documentsGroups(docGroupTitle,docTypeID) values(@docGroupTitle,@docTypeID);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentsGroups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addDocumentsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addDocumentsTypes] 
@docCode int,@docTypeDesc nvarchar(50) 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(docCode) from dbo.documentsTypes where @docCode=docCode);
if @varID = 0
begin
Insert Into dbo.documentsTypes(docCode,docTypeDesc) values(@docCode,@docTypeDesc);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentsTypes',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[addDocumentVersions]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[addDocumentVersions] 
@docID bigint,@version smallint,@addedDate datetime,@addedUserID int,@ext varchar(4),@docGroupID int, @DocumentFileName nvarchar(50)
as 
begin Transaction
Declare @varID int;
set @varID = (select count(docID) from dbo.documentVersions where @docID=docID and @version=version);
if @varID = 0
begin
Insert Into dbo.documentVersions(docID,version,addedDate,addedUserID,ext,docGroupID,DocumentFileName) values(@docID,@version,@addedDate,@addedUserID,@ext,@docGroupID,@DocumentFileName);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentVersions',16,1)
end
Commit


GO
/****** Object:  StoredProcedure [dbo].[addDocumentWFPath]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[addDocumentWFPath] 
@docID bigint,@userID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@userNotes ntext,@receiveDate datetime ,@endDate datetime
as 
begin Transaction


Insert Into dbo.documentWFPath(docID,userID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType,userNotes,receiveDate,EndDate) values(@docID,@userID,@actionDateTime,@wfPathID,@wfSeqNo,@actionType,@recipientType,@userNotes,@receiveDate,@endDate);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentWFPath',16,1)
end
Commit



/****** Object:  StoredProcedure [dbo].[updateDocumentWFPathByPrimaryKey]    Script Date: 03/03/2018 11:42:21 AM ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[addDocumentWFPathDelayed]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[addDocumentWFPathDelayed] 
@docID bigint,@userID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@userNotes ntext,@receiveDate datetime ,@endDate datetime,@inboxType int ,@documentWFPathId int
as 
begin Transaction


Insert Into dbo.documentWFPathDelayed(docID,userID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType,userNotes,receiveDate,EndDate,inboxType,documentWFPathId) values(@docID,@userID,@actionDateTime,@wfPathID,@wfSeqNo,@actionType,@recipientType,@userNotes,@receiveDate,@endDate,@inboxType,@documentWFPathId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.documentWFPath',16,1)
end
Commit



/****** Object:  StoredProcedure [dbo].[updateDocumentWFPathByPrimaryKey]    Script Date: 03/03/2018 11:42:21 AM ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[addEFormFields]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addEFormFields] 
@formID int,@fieldSeq int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(fieldSeq) from dbo.eFormFields where @fieldSeq=fieldSeq and @formID=formID);
if @varID = 0
begin
Insert Into dbo.eFormFields(formID,fieldSeq,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible) values(@formID,@fieldSeq,@metaDesc,@metaDataType,@required,@orderSeq,@ctrlID,@defaultTexts,@defaultValues,@visible);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.eFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addEForms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addEForms] 
@formName nvarchar(500),@defaultPathID int,@active bit,@catID int,@catPrgID int,@formNameAr nvarchar(500) 
as 
begin Transaction
Insert Into dbo.eForms(formName,defaultPathID,active,catID,catPrgID,formNameAr) values(@formName,@defaultPathID,@active,@catID,@catPrgID,@formNameAr);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.eForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addEformsCategories]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addEformsCategories] 
@catTitle nvarchar(500),@catPrgID int 
as 
begin Transaction
Insert Into dbo.eformsCategories(catTitle,catPrgID) values(@catTitle,@catPrgID);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.eformsCategories',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addEformWFPath]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addEformWFPath] 
@docID int,@userID int,@actionUserID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint 
as 
begin Transaction
Insert Into dbo.eformWFPath(docID,userID,actionUserID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType) values(@docID,@userID,@actionUserID,@actionDateTime,@wfPathID,@wfSeqNo,@actionType,@recipientType);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.eformWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addEventsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addEventsTypes] 
@eventTypeID int,@eventTypeDescA nvarchar(50),@eventTypeDescE varchar(50) 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(eventTypeID) from dbo.eventsTypes where @eventTypeID=eventTypeID);
if @varID = 0
begin
Insert Into dbo.eventsTypes(eventTypeID,eventTypeDescA,eventTypeDescE) values(@eventTypeID,@eventTypeDescA,@eventTypeDescE);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.eventsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[addFolders] 
@fldrName nvarchar(500),@fldrParentID int,@active bit,@iconID int,@defaultDocTypID int,@folderOrder int,@isDiwan bit,@fldrNameAr nvarchar(500),@allowWF bit ,@folderOwner int,@clientId int
as 
begin Transaction
Insert Into dbo.folders(fldrName,fldrParentID,active,iconID,defaultDocTypID,folderOrder,isDiwan,fldrNameAr,allowWF,folderOwner,ClientId) values(@fldrName,@fldrParentID,@active,@iconID,@defaultDocTypID,@folderOrder,@isDiwan,@fldrNameAr,@allowWF,@folderOwner,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.folders',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[addGroupBlocks]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addGroupBlocks] 
@grpID int,@docTypID int,@blockNum int,@blockLeft varchar(6),@blockTop varchar(6),@blockWidth varchar(6),@blockHeight varchar(6) 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(blockNum) from dbo.groupBlocks where @blockNum=blockNum and @docTypID=docTypID and @grpID=grpID);
if @varID = 0
begin
Insert Into dbo.groupBlocks(grpID,docTypID,blockNum,blockLeft,blockTop,blockWidth,blockHeight) values(@grpID,@docTypID,@blockNum,@blockLeft,@blockTop,@blockWidth,@blockHeight);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.groupBlocks',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addGroupFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addGroupFolders] 
@grpID int,@fldrID int,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@allowCreateFldr bit,@allowRenameFldr bit,@allowRelocationFldr bit,@inheritSubFolders bit 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(fldrID) from dbo.groupFolders where @fldrID=fldrID and @grpID=grpID);
if @varID = 0
begin
Insert Into dbo.groupFolders(grpID,fldrID,allowInsert,allowUpdate,allowDelete,allowCreateFldr,allowRenameFldr,allowRelocationFldr,inheritSubFolders) values(@grpID,@fldrID,@allowInsert,@allowUpdate,@allowDelete,@allowCreateFldr,@allowRenameFldr,@allowRelocationFldr,@inheritSubFolders);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.groupFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addGroupPrograms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addGroupPrograms] 
@groupID int,@programID int 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(groupID) from dbo.groupPrograms where @groupID=groupID and @programID=programID);
if @varID = 0
begin
Insert Into dbo.groupPrograms(groupID,programID) values(@groupID,@programID);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.groupPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addGroups]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[addGroups] 
@grpDesc nvarchar(500),@companyID int,@branchID int ,@clientId int
as 
begin Transaction
Insert Into dbo.groups(grpDesc,companyID,branchID,ClientId) values(@grpDesc,@companyID,@branchID,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.groups',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[addIcons]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addIcons] 
@iconDescription nvarchar(500) 
as 
begin Transaction
Insert Into dbo.icons(iconDescription) values(@iconDescription);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.icons',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addLoginEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addLoginEvents] 
@sysEventID int,@IPAddress nvarchar(50) 
as 
begin Transaction
Insert Into dbo.loginEvents(sysEventID,IPAddress) values(@sysEventID,@IPAddress);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.loginEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addMetas]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[addMetas] 
@docTypID int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit,@metaDescAr nvarchar(500),@defaultArTexts  nvarchar(4000),@columnSeq int,@metaIdFK int,@width float,@permissionType nvarchar(20)
as 
begin Transaction
Insert Into dbo.metas(docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width,permissionType) values(@docTypID,@metaDesc,@metaDataType,@required,@orderSeq,@ctrlID,@defaultTexts,@defaultValues,@visible,@metaDescAr,@defaultArTexts,@columnSeq,@metaIdFK,@width,@permissionType);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.metas',16,1)
end
Commit


GO
/****** Object:  StoredProcedure [dbo].[addPathDetails]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addPathDetails] 
@pathID int,@seqNo smallint,@userID int,@endOfPath bit 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(pathID) from dbo.pathDetails where @pathID=pathID and @seqNo=seqNo);
if @varID = 0
begin
Insert Into dbo.pathDetails(pathID,seqNo,userID,endOfPath) values(@pathID,@seqNo,@userID,@endOfPath);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.pathDetails',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[addPositions]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[addPositions] 
@positionTitle nvarchar(500),@positionTitleAr nvarchar(500),@clientId int
as 
begin Transaction
Insert Into dbo.positions(positionTitle,positionTitleAr,clientId) values(@positionTitle,@positionTitleAr,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.positions',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[addPrograms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addPrograms] 
@programName nvarchar(500),@parentProgramID int,@programURL varchar(2000),@windowWidth int,@windowHeight int,@programNameAr nvarchar(500) 
as 
begin Transaction
Insert Into dbo.programs(programName,parentProgramID,programURL,windowWidth,windowHeight,programNameAr) values(@programName,@parentProgramID,@programURL,@windowWidth,@windowHeight,@programNameAr);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.programs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addSettings]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[addSettings] 
@ID int,@allowedUsersCount nvarchar(1000),@systemActive nvarchar(1000),@systemActiveDate nvarchar(4000),@passwordStrength smallint,@passwordAllowStartSpace bit,@passwordLength smallint,@allowUsernamePasswordMatch bit,@firstLoginChangePassword bit,@passwordAgeDays int,@sessionTimeoutMinutes int,@lockTimeOut smallint,@outgoingMailServer varchar(100),@workflowEmail varchar(100),@workflowEmailPassword nvarchar(4000),@systemEmail varchar(100),@systemEmailPassword nvarchar(4000),@workflowEmailSubject nvarchar(150),@workflowEmailBody ntext,@systemEmailSignature nvarchar(4000),@clientId int 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(ID) from dbo.settings where @ID=ID);
if @varID = 0
begin
Insert Into dbo.settings(ID,allowedUsersCount,systemActive,systemActiveDate,passwordStrength,passwordAllowStartSpace,passwordLength,allowUsernamePasswordMatch,firstLoginChangePassword,passwordAgeDays,sessionTimeoutMinutes,lockTimeOut,outgoingMailServer,workflowEmail,workflowEmailPassword,systemEmail,systemEmailPassword,workflowEmailSubject,workflowEmailBody,systemEmailSignature,ClientId) 
values(@ID,@allowedUsersCount,@systemActive,@systemActiveDate,@passwordStrength,@passwordAllowStartSpace,@passwordLength,@allowUsernamePasswordMatch,@firstLoginChangePassword,@passwordAgeDays,@sessionTimeoutMinutes,@lockTimeOut,@outgoingMailServer,@workflowEmail,@workflowEmailPassword,@systemEmail,@systemEmailPassword,@workflowEmailSubject,@workflowEmailBody,@systemEmailSignature,@clientId);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.settings',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[AddSigture]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddSigture]
	-- Add the parameters for the stored procedure here
@signture nvarchar(max) ,
 @document nvarchar(50), @user int , @width nvarchar(50) , @height nvarchar(50) ,@top nvarchar(50) ,@left nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @LastChangeDate as datetime
SET @LastChangeDate = GetDate()
insert into SignatureTB values(@signture ,@document ,@user ,@width ,@height ,@top ,@left,@LastChangeDate)
END




GO
/****** Object:  StoredProcedure [dbo].[addSysdiagrams]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addSysdiagrams] 
@name nvarchar(128),@principal_id int,@version int,@definition varbinary 
as 
begin Transaction
Insert Into dbo.sysdiagrams(name,principal_id,version,definition) values(@name,@principal_id,@version,@definition);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.sysdiagrams',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[addSysEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addSysEvents] 
@userID int,@eventTypeID int,@eventDateTime datetime,@URL nvarchar(100) 
as 
begin Transaction
Insert Into dbo.sysEvents(userID,eventTypeID,eventDateTime,URL) values(@userID,@eventTypeID,@eventDateTime,@URL);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.sysEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addUserDocuments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addUserDocuments] 
@userID int,@docID bigint,@allow bit,@allowInsert bit,@allowUpdate bit,@allowDelete bit 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(docID) from dbo.userDocuments where @docID=docID and @userID=userID);
if @varID = 0
begin
Insert Into dbo.userDocuments(userID,docID,allow,allowInsert,allowUpdate,allowDelete) values(@userID,@docID,@allow,@allowInsert,@allowUpdate,@allowDelete);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.userDocuments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addUserFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addUserFolders] 
@userID int,@fldrID int,@allow bit,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@allowCreateFldr bit,@allowRenameFldr bit,@allowRelocationFldr bit,@inheritSubFolders bit 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(fldrID) from dbo.userFolders where @fldrID=fldrID and @userID=userID);
if @varID = 0
begin
Insert Into dbo.userFolders(userID,fldrID,allow,allowInsert,allowUpdate,allowDelete,allowCreateFldr,allowRenameFldr,allowRelocationFldr,inheritSubFolders) values(@userID,@fldrID,@allow,@allowInsert,@allowUpdate,@allowDelete,@allowCreateFldr,@allowRenameFldr,@allowRelocationFldr,@inheritSubFolders);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.userFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addUserFormFields]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addUserFormFields] 
@userID int,@formID int,@fieldSeq int,@value ntext 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(fieldSeq) from dbo.userFormFields where @fieldSeq=fieldSeq and @formID=formID and @userID=userID);
if @varID = 0
begin
Insert Into dbo.userFormFields(userID,formID,fieldSeq,value) values(@userID,@formID,@fieldSeq,@value);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.userFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addUserPrograms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addUserPrograms] 
@userID int,@programID int 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(programID) from dbo.userPrograms where @programID=programID and @userID=userID);
if @varID = 0
begin
Insert Into dbo.userPrograms(userID,programID) values(@userID,@programID);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.userPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addUsers]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[addUsers] 
@userName varchar(50),@password nvarchar(4000),@fullName nvarchar(500),@grpID int,@active bit,@companyID int,@branchID int,@departmentID int,@positionID int,@email varchar(500),@allowCustomWF bit,@allowCreateFolders bit,@allowReplaceDocuments bit,@allowDiwan bit,@isFirstLogin bit,@passwordCreationDate datetime,@passwordModifiedDate datetime,@lastPassword nvarchar(4000), @Phone nvarchar(50),@ClientId int
as 
begin Transaction
Insert Into dbo.users(userName,password,fullName,grpID,active,companyID,branchID,departmentID,positionID,email,allowCustomWF,allowCreateFolders,allowReplaceDocuments,allowDiwan,isFirstLogin,passwordCreationDate,passwordModifiedDate,lastPassword,Phone,ClientId) values(@userName,@password,@fullName,@grpID,@active,@companyID,@branchID,@departmentID,@positionID,@email,@allowCustomWF,@allowCreateFolders,@allowReplaceDocuments,@allowDiwan,@isFirstLogin,@passwordCreationDate,@passwordModifiedDate,@lastPassword,@Phone,@ClientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.users',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[addUsersForms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[addUsersForms] 
@userID int,@formID int,@pathID int,@submitDateTime datetime,@status smallint 
as 
begin Transaction
Declare @varID int;
set @varID = (select count(formID) from dbo.usersForms where @formID=formID and @userID=userID);
if @varID = 0
begin
Insert Into dbo.usersForms(userID,formID,pathID,submitDateTime,status) values(@userID,@formID,@pathID,@submitDateTime,@status);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.usersForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addWfPathDetails]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[addWfPathDetails] 
@pathID int,@seqNo smallint,@recipientID int,@endOfPath bit,@recipientType smallint,@companyID int,@branchID int,@approveType smallint ,
@duration int,@duartionType int
as 
begin Transaction
Declare @varID int;
set @varID = (select count(pathID) from dbo.wfPathDetails where @pathID=pathID and @seqNo=seqNo);
if @varID = 0
begin
Insert Into dbo.wfPathDetails(pathID,seqNo,recipientID,endOfPath,recipientType,companyID,branchID,approveType,duration,durationType) values(@pathID,@seqNo,@recipientID,@endOfPath,@recipientType,@companyID,@branchID,@approveType,@duration,@duartionType);
select @varID;
end
else select -1;
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.wfPathDetails',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[addWorkFlowPaths]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[addWorkFlowPaths] 
@pathDesc nvarchar(500),@fldrId int,@docTypId int,@pathDescAr nvarchar(500) ,@clientId int
as 
begin Transaction
Insert Into dbo.workFlowPaths(pathDesc,fldrId,docTypId,pathDescAr,ClientId) values(@pathDesc,@fldrId,@docTypId,@pathDescAr,@clientId);
select SCOPE_IDENTITY();
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Inserting into dbo.workFlowPaths',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[changePassword]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[changePassword] 
	@userID int, @password nvarchar(4000)
AS
BEGIN
	update users set password=@password where userID=@userID
END




GO
/****** Object:  StoredProcedure [dbo].[changeUserActiveStatus]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[changeUserActiveStatus] 
	@userID int, @active bit
AS
BEGIN
	update users set active = @active
	Where userID=@userID
END




GO
/****** Object:  StoredProcedure [dbo].[checkLog]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[checkLog]
	@username nvarchar(100),
	@password nvarchar(100)
AS
BEGIN
	select userid from users where  Lower(userName) = Lower(@username) and [password] = @password;
END




GO
/****** Object:  StoredProcedure [dbo].[closeDocWF]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[closeDocWF]
@docID bigint
as
begin
update documents set wfStatus=2 where docID =@docID
end




GO
/****** Object:  StoredProcedure [dbo].[deleteBranchFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteBranchFolders] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.branchFolders' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.branchFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteBranchFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteBranchFoldersByPrimaryKey] 
@branchID int,@fldrID int 
as 
begin Transaction
delete from dbo.branchFolders where @branchID=branchID and @fldrID=fldrID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.branchFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteBranchs]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteBranchs] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.branchs' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.branchs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteBranchsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteBranchsByPrimaryKey] 
@branchID int 
as 
begin Transaction
delete from dbo.branchs where @branchID=branchID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.branchs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteBrowseingEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteBrowseingEvents] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.browseingEvents' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.browseingEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteBrowseingEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteBrowseingEventsByPrimaryKey] 
@browseEventID int 
as 
begin Transaction
delete from dbo.browseingEvents where @browseEventID=browseEventID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.browseingEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteCompanies]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteCompanies] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.companies' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.companies',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteCompaniesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteCompaniesByPrimaryKey] 
@companyID int 
as 
begin Transaction
delete from dbo.companies where @companyID=companyID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.companies',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteCompanyFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteCompanyFolders] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.companyFolders' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.companyFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteCompanyFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteCompanyFoldersByPrimaryKey] 
@companyID int,@fldrID int 
as 
begin Transaction
delete from dbo.companyFolders where @companyID=companyID and @fldrID=fldrID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.companyFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteControlsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteControlsTypes] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.controlsTypes' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.controlsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteControlsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteControlsTypesByPrimaryKey] 
@crtlID int 
as 
begin Transaction
delete from dbo.controlsTypes where @crtlID=crtlID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.controlsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDataBaseEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDataBaseEvents] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.dataBaseEvents' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.dataBaseEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDataBaseEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDataBaseEventsByPrimaryKey] 
@DBEventID int 
as 
begin Transaction
delete from dbo.dataBaseEvents where @DBEventID=DBEventID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.dataBaseEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDBActionsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDBActionsTypes] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.DBActionsTypes' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.DBActionsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDBActionsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDBActionsTypesByPrimaryKey] 
@DBActionTypeID int 
as 
begin Transaction
delete from dbo.DBActionsTypes where @DBActionTypeID=DBActionTypeID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.DBActionsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDepartments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDepartments] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.departments' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.departments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDepartmentsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDepartmentsByPrimaryKey] 
@departmentID int 
as 
begin Transaction
delete from dbo.departments where @departmentID=departmentID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.departments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocTypes] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.docTypes' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.docTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocTypesByPrimaryKey] 
@docTypID int 
as 
begin Transaction
delete from dbo.docTypes where @docTypID=docTypID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.docTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentMataValues]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentMataValues] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.documentMataValues' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentMataValues',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentMataValuesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentMataValuesByPrimaryKey] 
@docID bigint,@metaID int 
as 
begin Transaction
delete from dbo.documentMataValues where @docID=docID and @metaID=metaID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentMataValues',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocuments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocuments] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.documents' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentsByPrimaryKey] 
@docID bigint 
as 
begin Transaction
delete from dbo.documents where @docID=docID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentsGroups]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentsGroups] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.documentsGroups' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentsGroups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentsGroupsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentsGroupsByPrimaryKey] 
@docGroupID int 
as 
begin Transaction
delete from dbo.documentsGroups where @docGroupID=docGroupID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentsGroups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentsTypes] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.documentsTypes' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentsTypes',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentsTypesByPrimaryKey] 
@docCode int 
as 
begin Transaction
delete from dbo.documentsTypes where @docCode=docCode; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentsTypes',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentVersions]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentVersions] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.documentVersions' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentVersions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentVersionsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentVersionsByPrimaryKey] 
@docID bigint,@version smallint 
as 
begin Transaction
delete from dbo.documentVersions where @docID=docID and @version=version; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentVersions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentWFPath]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentWFPath] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.documentWFPath' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteDocumentWFPathByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteDocumentWFPathByPrimaryKey] 
@ID int 
as 
begin Transaction
delete from dbo.documentWFPath where @ID=ID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.documentWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEFormFields]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEFormFields] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.eFormFields' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEFormFieldsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEFormFieldsByPrimaryKey] 
@fieldSeq int,@formID int 
as 
begin Transaction
delete from dbo.eFormFields where @fieldSeq=fieldSeq and @formID=formID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEForms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEForms] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.eForms' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEFormsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEFormsByPrimaryKey] 
@formID int 
as 
begin Transaction
delete from dbo.eForms where @formID=formID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEformsCategories]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEformsCategories] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.eformsCategories' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eformsCategories',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEformsCategoriesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEformsCategoriesByPrimaryKey] 
@catID int 
as 
begin Transaction
delete from dbo.eformsCategories where @catID=catID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eformsCategories',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEformWFPath]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEformWFPath] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.eformWFPath' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eformWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEformWFPathByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEformWFPathByPrimaryKey] 
@ID int 
as 
begin Transaction
delete from dbo.eformWFPath where @ID=ID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eformWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEventsTypes]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEventsTypes] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.eventsTypes' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eventsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteEventsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteEventsTypesByPrimaryKey] 
@eventTypeID int 
as 
begin Transaction
delete from dbo.eventsTypes where @eventTypeID=eventTypeID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.eventsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteFolders] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.folders' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.folders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteFoldersByPrimaryKey] 
@fldrID int 
as 
begin Transaction
delete from dbo.folders where @fldrID=fldrID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.folders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroupBlocks]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroupBlocks] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.groupBlocks' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groupBlocks',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroupBlocksByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroupBlocksByPrimaryKey] 
@blockNum int,@docTypID int,@grpID int 
as 
begin Transaction
delete from dbo.groupBlocks where @blockNum=blockNum and @docTypID=docTypID and @grpID=grpID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groupBlocks',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroupFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroupFolders] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.groupFolders' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groupFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroupFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroupFoldersByPrimaryKey] 
@fldrID int,@grpID int 
as 
begin Transaction
delete from dbo.groupFolders where @fldrID=fldrID and @grpID=grpID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groupFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroupPrograms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroupPrograms] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.groupPrograms' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groupPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroupProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroupProgramsByPrimaryKey] 
@groupID int,@programID int 
as 
begin Transaction
delete from dbo.groupPrograms where @groupID=groupID and @programID=programID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groupPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroups]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroups] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.groups' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteGroupsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteGroupsByPrimaryKey] 
@grpID int 
as 
begin Transaction
delete from dbo.groups where @grpID=grpID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.groups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteIcons]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteIcons] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.icons' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.icons',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteIconsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteIconsByPrimaryKey] 
@iconID int 
as 
begin Transaction
delete from dbo.icons where @iconID=iconID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.icons',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteLoginEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteLoginEvents] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.loginEvents' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.loginEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteLoginEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteLoginEventsByPrimaryKey] 
@loginID int 
as 
begin Transaction
delete from dbo.loginEvents where @loginID=loginID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.loginEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteMetas]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteMetas] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.metas' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.metas',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteMetasByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteMetasByPrimaryKey] 
@metaID int 
as 
begin Transaction
delete from dbo.metas where @metaID=metaID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.metas',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deletePathDetails]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deletePathDetails] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.pathDetails' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.pathDetails',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[deletePathDetailsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deletePathDetailsByPrimaryKey] 
@pathID int,@seqNo smallint 
as 
begin Transaction
delete from dbo.pathDetails where @pathID=pathID and @seqNo=seqNo; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.pathDetails',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[deletePositions]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deletePositions] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.positions' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.positions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deletePositionsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deletePositionsByPrimaryKey] 
@positionID int 
as 
begin Transaction
delete from dbo.positions where @positionID=positionID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.positions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deletePrograms]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deletePrograms] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.programs' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.programs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteProgramsByPrimaryKey] 
@programID int 
as 
begin Transaction
delete from dbo.programs where @programID=programID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.programs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteSettings]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteSettings] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.settings' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.settings',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteSettingsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteSettingsByPrimaryKey] 
@ID int 
as 
begin Transaction
delete from dbo.settings where @ID=ID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.settings',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteSysdiagrams]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteSysdiagrams] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.sysdiagrams' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.sysdiagrams',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[deleteSysdiagramsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteSysdiagramsByPrimaryKey] 
@diagram_id int 
as 
begin Transaction
delete from dbo.sysdiagrams where @diagram_id=diagram_id; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.sysdiagrams',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[deleteSysEvents]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteSysEvents] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.sysEvents' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.sysEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteSysEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteSysEventsByPrimaryKey] 
@sysEventID int 
as 
begin Transaction
delete from dbo.sysEvents where @sysEventID=sysEventID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.sysEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserDocuments]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserDocuments] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.userDocuments' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userDocuments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserDocumentsByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserDocumentsByPrimaryKey] 
@docID bigint,@userID int 
as 
begin Transaction
delete from dbo.userDocuments where @docID=docID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userDocuments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserFolders]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserFolders] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.userFolders' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserFoldersByPrimaryKey] 
@fldrID int,@userID int 
as 
begin Transaction
delete from dbo.userFolders where @fldrID=fldrID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserFormFields]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserFormFields] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.userFormFields' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserFormFieldsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserFormFieldsByPrimaryKey] 
@fieldSeq int,@formID int,@userID int 
as 
begin Transaction
delete from dbo.userFormFields where @fieldSeq=fieldSeq and @formID=formID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserPrograms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserPrograms] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.userPrograms' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUserProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUserProgramsByPrimaryKey] 
@programID int,@userID int 
as 
begin Transaction
delete from dbo.userPrograms where @programID=programID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.userPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUsers]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUsers] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.users' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.users',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUsersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUsersByPrimaryKey] 
@userID int 
as 
begin Transaction
delete from dbo.users where @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.users',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUsersForms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUsersForms] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.usersForms' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.usersForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteUsersFormsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteUsersFormsByPrimaryKey] 
@formID int,@userID int 
as 
begin Transaction
delete from dbo.usersForms where @formID=formID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.usersForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteWfPathDetails]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteWfPathDetails] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.wfPathDetails' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.wfPathDetails',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteWfPathDetailsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteWfPathDetailsByPrimaryKey] 
@pathID int,@seqNo smallint 
as 
begin Transaction
delete from dbo.wfPathDetails where @pathID=pathID and @seqNo=seqNo; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.wfPathDetails',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteWorkFlowPaths]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteWorkFlowPaths] 
@cond nvarchar(500) 
as 
begin Transaction
exec('delete from dbo.workFlowPaths' + @cond); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.workFlowPaths',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[deleteWorkFlowPathsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[deleteWorkFlowPathsByPrimaryKey] 
@pathId int 
as 
begin Transaction
delete from dbo.workFlowPaths where @pathId=pathId; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in deleting from dbo.workFlowPaths',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[DocumentsSearch]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[DocumentsSearch] 1,1,t
Create Proc [dbo].[DocumentsSearch]
@UserID int,
@lang nvarchar(10),
@SearchText nvarchar(500)
as
Select D.docID,D.docName,
(Select Case When @lang=0 Then F.fldrName Else F.fldrNameAr End From folders F Where F.fldrID=D.fldrID) As FolderName,
(Select Case When @lang=0 Then DT.docTypDesc Else DT.docTypDescAr End From docTypes DT Where DT.docTypID=D.docTypID) As DocTypeName,
D.addedDate,(select fullname from users where userid=D.addedUserID)AddedBy,
D.modifyDate,
Convert(varchar,D.addedDate,103) as addedDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.addedDate, 100), 7)) as addedTimeOnly,
Convert(varchar,D.modifyDate,103) as modifyDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.modifyDate, 100), 7)) as modifyTimeOnly
From documents D
where D.docTypID Not IN (2,22) And D.fldrID IN (Select UF.fldrID From userFolders UF Where UF.userID=@UserID)
And (D.docName Like '%' + @SearchText + '%' OR D.meta1 Like '%' + @SearchText + '%'
     OR D.meta2 Like '%' + @SearchText + '%' OR D.meta3 Like '%' + @SearchText + '%'
	 OR D.meta4 Like '%' + @SearchText + '%')

GO
/****** Object:  StoredProcedure [dbo].[DuplicateMetaCustomPermission]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[DuplicateMetaCustomPermission]
@metaId int, @newMetaId int
As
insert into metaUsersPermissions
(metaID,userID,allowRead,allowEdit)
(select @newMetaId,m.userID,m.allowRead,m.allowEdit from metaUsersPermissions m where m.metaID = @metaId )

insert into metaGroupsPermissions
(metaID,grpID,allowRead,allowEdit)
(select @newMetaId,m.grpID,m.allowRead,m.allowEdit from metaGroupsPermissions m where m.metaID = @metaId )

GO
/****** Object:  StoredProcedure [dbo].[EventsSearch]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[EventsSearch]
@UserID int,
@SearchText nvarchar(500)
as
Select E.event_id,E.title,E.description,E.event_start,E.event_end,(select fullname from users where userid=E.CreatedBy)UserName From Event E 
where CreatedBy=@UserID 
And (E.title Like '%' + @SearchText + '%' OR E.description Like '%' + @SearchText + '%')

GO
/****** Object:  StoredProcedure [dbo].[executeCommand]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[executeCommand] 
@cond nvarchar(4000) 
as 
begin 
exec(@cond); 
end




GO
/****** Object:  StoredProcedure [dbo].[getAllBranchFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllBranchFolders] 
@cond nvarchar(1000) 
as 
begin 
exec('select branchID,fldrID from dbo.branchFolders' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllBranchs]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllBranchs] 
@cond nvarchar(1000) 
as 
begin 
exec('select branchID,companyID,branchName,address,tel1,tel2,zipcode,mainEmail,description,isMainBranch,branchNameAr from dbo.branchs' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllBrowseingEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllBrowseingEvents] 
@cond nvarchar(1000) 
as 
begin 
exec('select browseEventID,sysEventID,pageID from dbo.browseingEvents' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllCompanies]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllCompanies] 
@cond nvarchar(1000) 
as 
begin 
exec('select companyID,companyName,address,tel1,tel2,zipcode,mainEmail,description,companyNameAr from dbo.companies' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllCompanyFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllCompanyFolders] 
@cond nvarchar(1000) 
as 
begin 
exec('select companyID,fldrID from dbo.companyFolders' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllControlsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllControlsTypes] 
@cond nvarchar(1000) 
as 
begin 
exec('select crtlID,ctrlDesc,ctrlDescAr from dbo.controlsTypes' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDataBaseEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDataBaseEvents] 
@cond nvarchar(1000) 
as 
begin 
exec('select DBEventID,sysEventID,DBActionTypeID,tableName,parameters from dbo.dataBaseEvents' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDBActionsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDBActionsTypes] 
@cond nvarchar(1000) 
as 
begin 
exec('select DBActionTypeID,DBActionTypeDescA,FBActionTypeDescE from dbo.DBActionsTypes' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDepartments]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDepartments] 
@cond nvarchar(1000) 
as 
begin 
exec('select departmentID,departmentName,headUserID,parentDepartmentID,departmentNameAr from dbo.departments' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDocTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDocTypes] 
@cond nvarchar(1000) 
as 
begin 
exec('select docTypID,docTypDesc,active,defaultWFID,docTypDescAr from dbo.docTypes' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[GetAllDocumentLate]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllDocumentLate]
	-- Add the parameters for the stored procedure here
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
--	DECLARE @recipientID INT;
--	set @recipientID=(SELECT wfPathDetails.recipientID FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=19 ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=19  and dbo.documentWFPath.actionType=0 ));

    --DECLARE @recipientType INT;
	--SELECT wfPathDetails.recipientType FROM   wfPathDetails where wfPathDetails.pathID= (select top 1 dbo.documentWFPath.wfPathID from dbo.documentWFPath where dbo.documentWFPath.docID=19 ) and  wfPathDetails.seqNo=(select top 1 dbo.documentWFPath.wfSeqNo from dbo.documentWFPath where dbo.documentWFPath.docID=19  and dbo.documentWFPath.actionType=0 ));

    -- Insert statements for procedure here
SELECT documents.docID,documents.DelayTime, documents.docTypID, documents.docName, documents.docExt, documents.addedDate, documents.addedUserID, documents.lastVersion, documents.modifyDate, documents.modifyUserID, documents.fldrID, documents.ocrContent, documents.folderSeq, 
             documents.docTypeSeq, documents.folderDocTypeSeq, documents.wfPathID, documents.wfCurrentSeq, documents.wfCurrentRecipientID, documents.wfCurrentRecipientType, documents.wfStartDateTime, documents.wfTimeFrame, documents.wfStatus, documents.meta1, 
             documents.meta2, documents.meta3, documents.meta4, documents.meta5, documents.meta6, documents.meta7, documents.meta8, documents.meta9, documents.meta10, documents.meta11, documents.meta12, documents.meta13, documents.meta14, documents.meta15, 
             documents.meta16, documents.meta17, documents.meta18, documents.meta19, documents.meta20, documents.meta21, documents.meta22, documents.meta23, documents.meta24, documents.meta25, documents.meta26, documents.meta27, documents.meta28, documents.meta29, 
             documents.meta30, documents.statusId, docTypes.docTypDesc, docTypes.docTypDescAr
			 ,ISNULL((select top 1 dbo.documentWFPath.userID from dbo.documentWFPath where docID=documents.docID and actionType=0),0) as recipientID
			 ,ISNULL((select top 1 dbo.documentWFPath.recipientType from dbo.documentWFPath where docID=documents.docID and actionType=0),0) as recipientType,
			 '' as userName
FROM   documents INNER JOIN
             docTypes ON documents.docTypID = docTypes.docTypID INNER JOIN
             users ON documents.addedUserID = users.userID 
			 where statusId=3
END



--select top 1 dbo.documentWFPath.userID from dbo.documentWFPath where docID=25 and actionType=0


--select top 1 dbo.documentWFPath.recipientType from dbo.documentWFPath where docID=25 and actionType=0

GO
/****** Object:  StoredProcedure [dbo].[getAllDocumentMataValues]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDocumentMataValues] 
@cond nvarchar(1000) 
as 
begin 
exec('select metaID,docID,value from dbo.documentMataValues' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDocuments]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDocuments] 
@cond nvarchar(1000) 
as 
begin 
exec('select docID,docTypID,docName,docExt,addedDate,addedUserID,lastVersion,modifyDate,modifyUserID,fldrID,ocrContent,folderSeq,docTypeSeq,folderDocTypeSeq,wfPathID,wfCurrentSeq,wfCurrentRecipientID,wfCurrentRecipientType,wfStartDateTime,wfTimeFrame,wfStatus,meta1,meta2,meta3,meta4,meta5,meta6,meta7,meta8,meta9,meta10,meta11,meta12,meta13,meta14,meta15,meta16,meta17,meta18,meta19,meta20,meta21,meta22,meta23,meta24,meta25,meta26,meta27,meta28,meta29,meta30 from dbo.documents' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDocumentsGroups]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDocumentsGroups] 
@cond nvarchar(1000) 
as 
begin 
exec('select docGroupID,docGroupTitle,docTypeID from dbo.documentsGroups' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDocumentsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDocumentsTypes] 
@cond nvarchar(500) 
as 
begin 
exec('select docCode,docTypeDesc from dbo.documentsTypes' +  @cond); 
end




GO
/****** Object:  StoredProcedure [dbo].[getAllDocumentVersions]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDocumentVersions] 
@cond nvarchar(1000) 
as 
begin 
exec('select docID,version,addedDate,addedUserID,ext,docGroupID from dbo.documentVersions' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllDocumentWFPath]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllDocumentWFPath] 
@cond nvarchar(1000) 
as 
begin 
exec('select ID,docID,userID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType,userNotes,receiveDate from dbo.documentWFPath' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllEFormFields]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllEFormFields] 
@cond nvarchar(1000) 
as 
begin 
exec('select formID,fieldSeq,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible from dbo.eFormFields' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllEForms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllEForms] 
@cond nvarchar(1000) 
as 
begin 
exec('select formID,formName,defaultPathID,active,catID,catPrgID,formNameAr from dbo.eForms' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllEformsCategories]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllEformsCategories] 
@cond nvarchar(1000) 
as 
begin 
exec('select catID,catTitle,catPrgID from dbo.eformsCategories' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllEformWFPath]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllEformWFPath] 
@cond nvarchar(1000) 
as 
begin 
exec('select ID,docID,userID,actionUserID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType from dbo.eformWFPath' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllEventsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllEventsTypes] 
@cond nvarchar(1000) 
as 
begin 
exec('select eventTypeID,eventTypeDescA,eventTypeDescE from dbo.eventsTypes' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllFolders] 
@cond nvarchar(1000) 
as 
begin 
exec('select fldrID,fldrName,fldrParentID,active,iconID,defaultDocTypID,folderOrder,isDiwan,fldrNameAr,allowWF from dbo.folders' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllGroupBlocks]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllGroupBlocks] 
@cond nvarchar(1000) 
as 
begin 
exec('select grpID,docTypID,blockNum,blockLeft,blockTop,blockWidth,blockHeight from dbo.groupBlocks' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllGroupFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllGroupFolders] 
@cond nvarchar(1000) 
as 
begin 
exec('select grpID,fldrID,allowInsert,allowUpdate,allowDelete,allowCreateFldr,allowRenameFldr,allowRelocationFldr,inheritSubFolders from dbo.groupFolders' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllGroupPrograms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllGroupPrograms] 
@cond nvarchar(1000) 
as 
begin 
exec('select groupID,programID from dbo.groupPrograms' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllGroups]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllGroups] 
@cond nvarchar(1000) 
as 
begin 
exec('select grpID,grpDesc,companyID,branchID from dbo.groups' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllIcons]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllIcons] 
@cond nvarchar(1000) 
as 
begin 
exec('select iconID,iconDescription from dbo.icons' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllLoginEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllLoginEvents] 
@cond nvarchar(1000) 
as 
begin 
exec('select loginID,sysEventID,IPAddress from dbo.loginEvents' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllMetas]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[getAllMetas] 
@cond nvarchar(1000) 
as 
begin 
exec('select metaID,docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width,permissionType from dbo.metas' +  @cond); 
end
GO
/****** Object:  StoredProcedure [dbo].[getAllPathDetails]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllPathDetails] 
@cond nvarchar(1000) 
as 
begin 
exec('select pathID,seqNo,userID,endOfPath from dbo.pathDetails' +  @cond); 
end




GO
/****** Object:  StoredProcedure [dbo].[getAllPositions]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllPositions] 
@cond nvarchar(1000) 
as 
begin 
exec('select positionID,positionTitle,positionTitleAr from dbo.positions' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllPrograms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllPrograms] 
@cond nvarchar(1000) 
as 
begin 
exec('select programID,programName,parentProgramID,programURL,windowWidth,windowHeight,programNameAr from dbo.programs' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllSettings]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllSettings] 
@cond nvarchar(1000) 
as 
begin 
exec('select ID,allowedUsersCount,systemActive,systemActiveDate,passwordStrength,passwordAllowStartSpace,passwordLength,allowUsernamePasswordMatch,firstLoginChangePassword,passwordAgeDays,sessionTimeoutMinutes,lockTimeOut,outgoingMailServer,workflowEmail,workflowEmailPassword,systemEmail,systemEmailPassword,workflowEmailSubject,workflowEmailBody,systemEmailSignature from dbo.settings' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllSysdiagrams]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllSysdiagrams] 
@cond nvarchar(1000) 
as 
begin 
exec('select name,principal_id,diagram_id,version,definition from dbo.sysdiagrams' +  @cond); 
end




GO
/****** Object:  StoredProcedure [dbo].[getAllSysEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllSysEvents] 
@cond nvarchar(1000) 
as 
begin 
exec('select sysEventID,userID,eventTypeID,eventDateTime,URL from dbo.sysEvents' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllUserDocuments]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllUserDocuments] 
@cond nvarchar(1000) 
as 
begin 
exec('select userID,docID,allow,allowInsert,allowUpdate,allowDelete from dbo.userDocuments' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllUserFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllUserFolders] 
@cond nvarchar(1000) 
as 
begin 
exec('select userID,fldrID,allow,allowInsert,allowUpdate,allowDelete,allowCreateFldr,allowRenameFldr,allowRelocationFldr,inheritSubFolders from dbo.userFolders' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllUserFormFields]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllUserFormFields] 
@cond nvarchar(1000) 
as 
begin 
exec('select userID,formID,fieldSeq,value from dbo.userFormFields' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllUserPrograms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllUserPrograms] 
@cond nvarchar(1000) 
as 
begin 
exec('select userID,programID from dbo.userPrograms' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllUsers]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllUsers] 
@cond nvarchar(1000) 
as 
begin 
exec('select userID,userName,password,fullName,grpID,active,companyID,branchID,departmentID,positionID,email,allowCustomWF,allowCreateFolders,allowReplaceDocuments,allowDiwan,isFirstLogin,passwordCreationDate,passwordModifiedDate,lastPassword from dbo.users' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllUsersForms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllUsersForms] 
@cond nvarchar(1000) 
as 
begin 
exec('select userID,formID,pathID,submitDateTime,status from dbo.usersForms' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllWfPathDetails]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllWfPathDetails] 
@cond nvarchar(1000) 
as 
begin 
exec('select pathID,seqNo,recipientID,endOfPath,recipientType,companyID,branchID,approveType from dbo.wfPathDetails' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getAllWorkFlowPaths]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getAllWorkFlowPaths] 
@cond nvarchar(1000) 
as 
begin 
exec('select pathId,pathDesc,fldrId,docTypId,pathDescAr from dbo.workFlowPaths' +  @cond); 
end



GO
/****** Object:  StoredProcedure [dbo].[getBranchFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getBranchFoldersByPrimaryKey] 
@branchID int,@fldrID int 
as 
select branchID,fldrID from dbo.branchFolders where @branchID=branchID and @fldrID=fldrID; 



GO
/****** Object:  StoredProcedure [dbo].[getBranchsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getBranchsByPrimaryKey] 
@branchID int 
as 
select branchID,companyID,branchName,address,tel1,tel2,zipcode,mainEmail,description,isMainBranch,branchNameAr from dbo.branchs where @branchID=branchID; 



GO
/****** Object:  StoredProcedure [dbo].[getBrowseingEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getBrowseingEventsByPrimaryKey] 
@browseEventID int 
as 
select browseEventID,sysEventID,pageID from dbo.browseingEvents where @browseEventID=browseEventID; 



GO
/****** Object:  StoredProcedure [dbo].[getCompaniesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getCompaniesByPrimaryKey] 
@companyID int 
as 
select companyID,companyName,address,tel1,tel2,zipcode,mainEmail,description,companyNameAr from dbo.companies where @companyID=companyID; 



GO
/****** Object:  StoredProcedure [dbo].[getCompanyFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getCompanyFoldersByPrimaryKey] 
@companyID int,@fldrID int 
as 
select companyID,fldrID from dbo.companyFolders where @companyID=companyID and @fldrID=fldrID; 



GO
/****** Object:  StoredProcedure [dbo].[getControlsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getControlsTypesByPrimaryKey] 
@crtlID int 
as 
select crtlID,ctrlDesc,ctrlDescAr from dbo.controlsTypes where @crtlID=crtlID; 



GO
/****** Object:  StoredProcedure [dbo].[getDataBaseEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDataBaseEventsByPrimaryKey] 
@DBEventID int 
as 
select DBEventID,sysEventID,DBActionTypeID,tableName,parameters from dbo.dataBaseEvents where @DBEventID=DBEventID; 



GO
/****** Object:  StoredProcedure [dbo].[getDBActionsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDBActionsTypesByPrimaryKey] 
@DBActionTypeID int 
as 
select DBActionTypeID,DBActionTypeDescA,FBActionTypeDescE from dbo.DBActionsTypes where @DBActionTypeID=DBActionTypeID; 



GO
/****** Object:  StoredProcedure [dbo].[getDelayedDocuemnts]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[getDelayedDocuemnts]
as
begin
SELECT     *, 
Datediff(minute,wfStartDateTime,GETDATE()) as  duration
FROM         dbo.documents
where wfTimeFrame is not null and 
Datediff(minute,wfStartDateTime,GETDATE())>wfTimeFrame
and wfStatus=1
end




GO
/****** Object:  StoredProcedure [dbo].[getDepartmentsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[getDepartmentsByPrimaryKey] 
@departmentID int 
as 
select departmentID,departmentName,headUserID,parentDepartmentID,departmentNameAr,parentID from dbo.departments where @departmentID=departmentID; 
GO
/****** Object:  StoredProcedure [dbo].[getDetailedLoginEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getDetailedLoginEvents]
AS
BEGIN
	SELECT     dbo.users.userName, dbo.users.fullName, dbo.eventsTypes.eventTypeDescE, dbo.sysEvents.eventDateTime, dbo.sysEvents.URL, dbo.loginEvents.IPAddress
FROM         dbo.eventsTypes INNER JOIN
                      dbo.sysEvents ON dbo.eventsTypes.eventTypeID = dbo.sysEvents.eventTypeID INNER JOIN
                      dbo.loginEvents ON dbo.sysEvents.sysEventID = dbo.loginEvents.sysEventID INNER JOIN
                      dbo.users ON dbo.sysEvents.userID = dbo.users.userID
END




GO
/****** Object:  StoredProcedure [dbo].[getDocTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDocTypesByPrimaryKey] 
@docTypID int 
as 
select docTypID,docTypDesc,active,defaultWFID,docTypDescAr from dbo.docTypes where @docTypID=docTypID; 



GO
/****** Object:  StoredProcedure [dbo].[getDocumentMataValuesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDocumentMataValuesByPrimaryKey] 
@docID bigint,@metaID int 
as 
select metaID,docID,value from dbo.documentMataValues where @docID=docID and @metaID=metaID; 



GO
/****** Object:  StoredProcedure [dbo].[getDocumentsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDocumentsByPrimaryKey] 
@docID bigint 
as 
select docID,docTypID,docName,docExt,addedDate,addedUserID,lastVersion,modifyDate,modifyUserID,fldrID,ocrContent,folderSeq,docTypeSeq,folderDocTypeSeq,wfPathID,wfCurrentSeq,wfCurrentRecipientID,wfCurrentRecipientType,wfStartDateTime,wfTimeFrame,wfStatus,meta1,meta2,meta3,meta4,meta5,meta6,meta7,meta8,meta9,meta10,meta11,meta12,meta13,meta14,meta15,meta16,meta17,meta18,meta19,meta20,meta21,meta22,meta23,meta24,meta25,meta26,meta27,meta28,meta29,meta30 from dbo.documents where @docID=docID; 



GO
/****** Object:  StoredProcedure [dbo].[getDocumentsGroupsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDocumentsGroupsByPrimaryKey] 
@docGroupID int 
as 
select docGroupID,docGroupTitle,docTypeID from dbo.documentsGroups where @docGroupID=docGroupID; 



GO
/****** Object:  StoredProcedure [dbo].[getDocumentsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDocumentsTypesByPrimaryKey] 
@docCode int 
as 
select docCode,docTypeDesc from dbo.documentsTypes where @docCode=docCode;




GO
/****** Object:  StoredProcedure [dbo].[getDocumentVersionsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDocumentVersionsByPrimaryKey] 
@docID bigint,@version smallint 
as 
select docID,version,addedDate,addedUserID,ext,docGroupID from dbo.documentVersions where @docID=docID and @version=version; 



GO
/****** Object:  StoredProcedure [dbo].[getDocumentWFPathByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getDocumentWFPathByPrimaryKey] 
@ID int 
as 
select ID,docID,userID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType,userNotes,receiveDate from dbo.documentWFPath where @ID=ID; 



GO
/****** Object:  StoredProcedure [dbo].[getEFormFieldsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getEFormFieldsByPrimaryKey] 
@fieldSeq int,@formID int 
as 
select formID,fieldSeq,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible from dbo.eFormFields where @fieldSeq=fieldSeq and @formID=formID; 



GO
/****** Object:  StoredProcedure [dbo].[getEFormsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getEFormsByPrimaryKey] 
@formID int 
as 
select formID,formName,defaultPathID,active,catID,catPrgID,formNameAr from dbo.eForms where @formID=formID; 



GO
/****** Object:  StoredProcedure [dbo].[getEformsCategoriesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getEformsCategoriesByPrimaryKey] 
@catID int 
as 
select catID,catTitle,catPrgID from dbo.eformsCategories where @catID=catID; 



GO
/****** Object:  StoredProcedure [dbo].[getEformWFPathByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getEformWFPathByPrimaryKey] 
@ID int 
as 
select ID,docID,userID,actionUserID,actionDateTime,wfPathID,wfSeqNo,actionType,recipientType from dbo.eformWFPath where @ID=ID; 



GO
/****** Object:  StoredProcedure [dbo].[getEventsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getEventsTypesByPrimaryKey] 
@eventTypeID int 
as 
select eventTypeID,eventTypeDescA,eventTypeDescE from dbo.eventsTypes where @eventTypeID=eventTypeID; 



GO
/****** Object:  StoredProcedure [dbo].[getFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----add folderOwner 
--ALTER TABLE  [dbo].[folders]
--ADD folderOwner INT
--GO


-- alter getFoldersByPrimaryKey 
CREATE procedure [dbo].[getFoldersByPrimaryKey] 
@fldrID int 
as 
select fldrID,fldrName,fldrParentID,active,iconID,defaultDocTypID,folderOrder,isDiwan,fldrNameAr,allowWF,folderOwner from dbo.folders where @fldrID=fldrID; 



GO
/****** Object:  StoredProcedure [dbo].[getGroupBlocksByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getGroupBlocksByPrimaryKey] 
@blockNum int,@docTypID int,@grpID int 
as 
select grpID,docTypID,blockNum,blockLeft,blockTop,blockWidth,blockHeight from dbo.groupBlocks where @blockNum=blockNum and @docTypID=docTypID and @grpID=grpID; 



GO
/****** Object:  StoredProcedure [dbo].[getGroupFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getGroupFoldersByPrimaryKey] 
@fldrID int,@grpID int 
as 
select grpID,fldrID,allowInsert,allowUpdate,allowDelete,allowCreateFldr,allowRenameFldr,allowRelocationFldr,inheritSubFolders from dbo.groupFolders where @fldrID=fldrID and @grpID=grpID; 



GO
/****** Object:  StoredProcedure [dbo].[getGroupProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getGroupProgramsByPrimaryKey] 
@groupID int,@programID int 
as 
select groupID,programID from dbo.groupPrograms where @groupID=groupID and @programID=programID; 



GO
/****** Object:  StoredProcedure [dbo].[getGroupsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getGroupsByPrimaryKey] 
@grpID int 
as 
select grpID,grpDesc,companyID,branchID from dbo.groups where @grpID=grpID; 



GO
/****** Object:  StoredProcedure [dbo].[getIconsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getIconsByPrimaryKey] 
@iconID int 
as 
select iconID,iconDescription from dbo.icons where @iconID=iconID; 



GO
/****** Object:  StoredProcedure [dbo].[getLoginEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getLoginEventsByPrimaryKey] 
@loginID int 
as 
select loginID,sysEventID,IPAddress from dbo.loginEvents where @loginID=loginID; 



GO
/****** Object:  StoredProcedure [dbo].[getMetasByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[getMetasByPrimaryKey] 
@metaID int 
as 
select metaID,docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width,permissionType from dbo.metas where @metaID=metaID; 
GO
/****** Object:  StoredProcedure [dbo].[getMetaUsersAndGroupsFolderPermissions]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--getMetaUsersAndGroupsFolderPermissions 136
create Proc [dbo].[getMetaUsersAndGroupsFolderPermissions]
@metaID int
as
Select T.ID,T.Name,CAST(MAX(CAST(T.allowRead as INT)) AS BIT)allowRead,CAST(MAX(CAST(T.allowUpdate as INT)) AS BIT)allowEdit,T.PerType From (
select U.userID ID,U.fullName Name,Cast('1' as bit)allowRead,UF.allowUpdate,'u' PerType 
From userFolders UF Inner Join users U ON UF.userID=U.userID 
                    Inner Join folders F ON UF.fldrID=F.fldrID 
					Inner Join docTypes DT ON F.defaultDocTypID=DT.docTypID 
					Inner Join metas M ON DT.docTypID=M.docTypID 
Where M.metaID=@metaID
Union
select G.grpID ID,G.grpDesc Name,Cast('1' as bit)allowRead,GF.allowUpdate, 'g' PerType 
From groupFolders GF Inner Join groups G ON GF.grpID=G.grpID 
                    Inner Join folders F ON GF.fldrID=F.fldrID 
					Inner Join docTypes DT ON F.defaultDocTypID=DT.docTypID 
					Inner Join metas M ON DT.docTypID=M.docTypID ) T
Group by T.ID,T.Name,T.PerType 
GO
/****** Object:  StoredProcedure [dbo].[getMetaUsersAndGroupsPermissions]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc [dbo].[getMetaUsersAndGroupsPermissions]
@metaID int
as
select U.userID ID,U.fullName Name,UP.allowRead,UP.allowEdit,'u' PerType From metaUsersPermissions UP Inner Join users U ON UP.userID=U.userID 
Where UP.metaID=@metaID
Union
select G.grpID ID,G.grpDesc Name,GP.allowRead,GP.allowEdit, 'g' PerType From metaGroupsPermissions GP Inner Join groups G ON GP.grpID=G.grpID 
Where GP.metaID=@metaID
GO
/****** Object:  StoredProcedure [dbo].[GetMetaValue]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetMetaValue]
	@DocID int,
	@SeqNo int

AS
BEGIN
with documentMataValues AS(select row_number() over(order by metaID) as 'row', * 
                from dbo.documentMataValues Where docID = @DocID)
 select top 1 [value] from documentMataValues
where row=@SeqNo;

END




GO
/****** Object:  StoredProcedure [dbo].[getPathDetailsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getPathDetailsByPrimaryKey] 
@pathID int,@seqNo smallint 
as 
select pathID,seqNo,userID,endOfPath from dbo.pathDetails where @pathID=pathID and @seqNo=seqNo;




GO
/****** Object:  StoredProcedure [dbo].[getPositionsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getPositionsByPrimaryKey] 
@positionID int 
as 
select positionID,positionTitle,positionTitleAr from dbo.positions where @positionID=positionID; 



GO
/****** Object:  StoredProcedure [dbo].[getProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getProgramsByPrimaryKey] 
@programID int 
as 
select programID,programName,parentProgramID,programURL,windowWidth,windowHeight,programNameAr from dbo.programs where @programID=programID; 



GO
/****** Object:  StoredProcedure [dbo].[getSettingsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getSettingsByPrimaryKey] 
@ID int 
as 
select ID,allowedUsersCount,systemActive,systemActiveDate,passwordStrength,passwordAllowStartSpace,passwordLength,allowUsernamePasswordMatch,firstLoginChangePassword,passwordAgeDays,sessionTimeoutMinutes,lockTimeOut,outgoingMailServer,workflowEmail,workflowEmailPassword,systemEmail,systemEmailPassword,workflowEmailSubject,workflowEmailBody,systemEmailSignature from dbo.settings where @ID=ID; 



GO
/****** Object:  StoredProcedure [dbo].[getSysdiagramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getSysdiagramsByPrimaryKey] 
@diagram_id int 
as 
select name,principal_id,diagram_id,version,definition from dbo.sysdiagrams where @diagram_id=diagram_id; 




GO
/****** Object:  StoredProcedure [dbo].[getSysEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getSysEventsByPrimaryKey] 
@sysEventID int 
as 
select sysEventID,userID,eventTypeID,eventDateTime,URL from dbo.sysEvents where @sysEventID=sysEventID; 



GO
/****** Object:  StoredProcedure [dbo].[getTasksList]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[getTasksList] '1,2,3'
Create procedure [dbo].[getTasksList] 
@TaskIDs nvarchar(max) 
as 
SELECT 
  
  todoList.[Id], 
  todoList.[Description],
  createdbyUser.fullName as 'CreatedBy',
  assignToUser.fullName as 'AssignTo',
  todoList.[TaskDate] as 'TaskDate',
  todoList.[TaskName] as 'TaskName',
  taskType.ArText as 'TaskType',
  todoList.IsComplete
     
  FROM [dbo].[ToDoList] as todoList
  INNER JOIN [dbo].[TaskTypes] taskType on todoList.TaskType = taskType.Id
  LEFT JOIN [dbo].[users] as createdbyUser on todoList.CreatedBy = createdbyUser.[userID]
  LEFT JOIN [dbo].[users] as assignToUser on todoList.AssignTo = assignToUser.[userID]
  Where todoList.[Id] IN (Select Value From dbo.SplitString(@TaskIDs,','))
GO
/****** Object:  StoredProcedure [dbo].[getToDoListCalender]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--getToDoListCalender 1,'12/1/2020','1/30/2021'
Create procedure [dbo].[getToDoListCalender] 
@userId int,
@start datetime,
@end datetime
as 
Declare @Tasks table (Id int, TaskName nvarchar(400), TaskDate datetime, AssignTo int, CreatedBy int, TaskType int, CreatedOn datetime, IsComplete bit, IsDeleted bit, Description nvarchar(max), DocumentId bigint,CompleteDate datetime,RepeatType nvarchar(10),RepeatWeekDays nvarchar(100),IsMainTable bit)
Insert Into @Tasks (Id, TaskName, TaskDate, AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,IsMainTable)
SELECT Id, TaskName, TaskDate, AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,1
FROM dbo.ToDoList where TaskDate IS NOT NULL and IsComplete = 0 and IsDeleted = 0 and (CreatedBy= @userId or AssignTo=@userId)
And (TaskDate between @start And @end
     OR (RepeatType is not null And RepeatType<>'' And (IsComplete = 0 OR IsComplete Is Null) And TaskDate<@start) 
	 OR (RepeatType is not null And RepeatType<>'' And IsComplete=1 And CompleteDate between @start And @end))
Declare @date datetime
Set @date=@start 
while @date <= @end 
Begin
	Insert Into @Tasks (Id, TaskName, TaskDate, AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,IsMainTable)
	SELECT Id, TaskName, CONVERT(varchar, @date, 101) + ' ' + CONVERT(varchar, TaskDate, 108), AssignTo, CreatedBy, TaskType, CreatedOn, IsComplete, IsDeleted, Description, DocumentId,CompleteDate,RepeatType,RepeatWeekDays,0 
	FROM @Tasks Where IsMainTable=1 And RepeatType is not null And RepeatType<>'' 
	And @date>TaskDate  And (CompleteDate is null OR IsComplete is null OR IsComplete=0 OR @date<=CompleteDate)
	And ((RepeatType='Daily')
	  OR (RepeatType='Weekly' And DATENAME(DW,TaskDate)=DATENAME(DW,@date))
	  OR (RepeatType='WeekDays' And DATENAME(DW,@date) IN (Select Value From dbo.SplitString(RepeatWeekDays,',')))
	  OR (RepeatType='Monthly' And DatePart(DAY,TaskDate)=DATENAME(DAY,@date) And DatePart(MONTH,TaskDate)=DatePart(MONTH,@date))
	  OR (RepeatType='Yearly' And DatePart(DAYOFYEAR,TaskDate)=DatePart(DAYOFYEAR,@date))
	    )
	Set @date=DateAdd(day,1,@date)
End
Select * From @Tasks 
GO
/****** Object:  StoredProcedure [dbo].[getUserDocumentsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getUserDocumentsByPrimaryKey] 
@docID bigint,@userID int 
as 
select userID,docID,allow,allowInsert,allowUpdate,allowDelete from dbo.userDocuments where @docID=docID and @userID=userID; 



GO
/****** Object:  StoredProcedure [dbo].[getUserFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getUserFolders]
@userID int,
@isDiwan bit
AS
BEGIN
declare @companyID int;
set @companyID = (select ISNULL(companyID,0) from users where userID = @userID)
IF @companyID = 0
begin
SELECT  Distinct    dbo.companyFolders.companyID, dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName, dbo.folders.fldrNameAr, dbo.folders.fldrParentID, 
                      dbo.folders.active, dbo.folders.iconID, dbo.folders.defaultDocTypID, dbo.folders.folderOrder
FROM         dbo.companies INNER JOIN
                      dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN
                      dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID INNER JOIN
                      dbo.userFolders ON dbo.folders.fldrID = dbo.userFolders.fldrID
WHERE     (dbo.userFolders.userID = @userID and dbo.userFolders.allow=1	
--or (EXISTS(select top 1 programID from dbo.userPrograms where userID=@userID and programID=1))
) and isDiwan=@isDiwan 
ORDER BY dbo.companyFolders.companyID,dbo.folders.folderOrder
end
else
begin
SELECT  Distinct    dbo.companyFolders.companyID, dbo.companies.companyName,dbo.companies.companyNameAr, dbo.folders.fldrID, dbo.folders.fldrName, dbo.folders.fldrNameAr, dbo.folders.fldrParentID, 
                      dbo.folders.active, dbo.folders.iconID, dbo.folders.defaultDocTypID, dbo.folders.folderOrder
FROM         dbo.companies INNER JOIN
                      dbo.companyFolders ON dbo.companies.companyID = dbo.companyFolders.companyID INNER JOIN
                      dbo.folders ON dbo.companyFolders.fldrID = dbo.folders.fldrID INNER JOIN
                      dbo.userFolders ON dbo.folders.fldrID = dbo.userFolders.fldrID
WHERE     ((dbo.userFolders.userID = @userID and dbo.userFolders.allow=1	) 
--or (EXISTS(select top 1 programID from dbo.userPrograms where userID=@userID and programID=1))
) and isDiwan=@isDiwan and dbo.companyFolders.companyID = @companyID
ORDER BY dbo.companyFolders.companyID,dbo.folders.folderOrder 
end
END



GO
/****** Object:  StoredProcedure [dbo].[getUserFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getUserFoldersByPrimaryKey] 
@fldrID int,@userID int 
as 
select userID,fldrID,allow,allowInsert,allowUpdate,allowDelete,allowCreateFldr,allowRenameFldr,allowRelocationFldr,inheritSubFolders from dbo.userFolders where @fldrID=fldrID and @userID=userID; 



GO
/****** Object:  StoredProcedure [dbo].[getUserFoldersWithoutCompanies]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[getUserFoldersWithoutCompanies]
@userID int
AS
BEGIN
SELECT  Distinct   TOP (100) PERCENT  dbo.folders.fldrID, dbo.folders.fldrName, dbo.folders.fldrNameAr, dbo.folders.fldrParentID, 
                      dbo.folders.active, dbo.folders.iconID, dbo.folders.defaultDocTypID, dbo.folders.folderOrder
FROM         dbo.folders  INNER JOIN
                      dbo.userFolders ON dbo.folders.fldrID = dbo.userFolders.fldrID
WHERE     (dbo.userFolders.userID = @userID and dbo.userFolders.allow=1	)
ORDER BY dbo.folders.folderOrder,dbo.folders.fldrID

END




GO
/****** Object:  StoredProcedure [dbo].[getUserFormFieldsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getUserFormFieldsByPrimaryKey] 
@fieldSeq int,@formID int,@userID int 
as 
select userID,formID,fieldSeq,value from dbo.userFormFields where @fieldSeq=fieldSeq and @formID=formID and @userID=userID; 



GO
/****** Object:  StoredProcedure [dbo].[getUserProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getUserProgramsByPrimaryKey] 
@programID int,@userID int 
as 
select userID,programID from dbo.userPrograms where @programID=programID and @userID=userID; 



GO
/****** Object:  StoredProcedure [dbo].[getUsersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[getUsersByPrimaryKey] 
@userID int 
as 
select userID,userName,password,fullName,grpID,active,companyID,branchID,departmentID,positionID,email,allowCustomWF,allowCreateFolders,allowReplaceDocuments,allowDiwan,isFirstLogin,passwordCreationDate,passwordModifiedDate,lastPassword,Phone,ClientId from dbo.users where @userID=userID; 

GO
/****** Object:  StoredProcedure [dbo].[getUsersFormsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getUsersFormsByPrimaryKey] 
@formID int,@userID int 
as 
select userID,formID,pathID,submitDateTime,status from dbo.usersForms where @formID=formID and @userID=userID; 



GO
/****** Object:  StoredProcedure [dbo].[getWfPathDetailsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getWfPathDetailsByPrimaryKey] 
@pathID int,@seqNo smallint 
as 
select pathID,seqNo,recipientID,endOfPath,recipientType,companyID,branchID,approveType from dbo.wfPathDetails where @pathID=pathID and @seqNo=seqNo; 



GO
/****** Object:  StoredProcedure [dbo].[getWorkFlowPathsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[getWorkFlowPathsByPrimaryKey] 
@pathId int 
as 
select pathId,pathDesc,fldrId,docTypId,pathDescAr from dbo.workFlowPaths where @pathId=pathId; 



GO
/****** Object:  StoredProcedure [dbo].[getWorkflowUsers]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getWorkflowUsers] 
@lang bit,
@ClientId int
AS
BEGIN
if @lang=0
begin
	SELECT     dbo.users.userID, dbo.users.fullName as userFullName
FROM         dbo.users INNER JOIN
                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
	Where
	dbo.users.FullName <> '-'  and dbo.users.ClientId = @clientId order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
END
else
begin
	SELECT     dbo.users.userID, dbo.users.fullName as userFullName
FROM         dbo.users INNER JOIN
                      dbo.positions ON dbo.users.positionID = dbo.positions.positionID INNER JOIN
                      dbo.branchs ON dbo.users.branchID = dbo.branchs.branchID INNER JOIN
                      dbo.companies ON dbo.users.companyID = dbo.companies.companyID
	Where
	dbo.users.FullName <> '-'   and dbo.users.ClientId = @clientId order by dbo.users.PositionID,dbo.users.BranchID,dbo.users.FullName
END
End

GO
/****** Object:  StoredProcedure [dbo].[OutgoingAndIncomingDocumentsSearch]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[OutgoingAndIncomingDocumentsSearch] 1,1,ج
Create Proc [dbo].[OutgoingAndIncomingDocumentsSearch]
@UserID int,
@lang nvarchar(10),
@SearchText nvarchar(500)
as
Select D.docID,D.docName,
(Select Case When @lang=0 Then F.fldrName Else F.fldrNameAr End From folders F Where F.fldrID=D.fldrID) As FolderName,
(Select Case When @lang=0 Then DT.docTypDesc Else DT.docTypDescAr End From docTypes DT Where DT.docTypID=D.docTypID) As DocTypeName,
D.addedDate,(select fullname from users where userid=D.addedUserID)AddedBy,
D.modifyDate,
Convert(varchar,D.addedDate,103) as addedDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.addedDate, 100), 7)) as addedTimeOnly,
Convert(varchar,D.modifyDate,103) as modifyDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), D.modifyDate, 100), 7)) as modifyTimeOnly
From documents D
where D.docTypID in(2,22) And D.fldrID IN (Select UF.fldrID From userFolders UF Where UF.userID=@UserID)
And (D.docName Like '%' + @SearchText + '%' OR D.meta1 Like '%' + @SearchText + '%'
     OR D.meta2 Like '%' + @SearchText + '%' OR D.meta3 Like '%' + @SearchText + '%'
	 OR D.meta4 Like '%' + @SearchText + '%')

GO
/****** Object:  StoredProcedure [dbo].[report_actionTime]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[report_actionTime]
@startDate datetime,@endDate datetime
as
begin
SELECT dbo.users.fullName,AVG( DATEDIFF(MI,dbo.documentWFPath.receiveDate, dbo.documentWFPath.actionDateTime )) diff
FROM         dbo.users INNER JOIN
                      dbo.documentWFPath ON dbo.users.userID = dbo.documentWFPath.userID
                      where receiveDate >= @startDate and actionDateTime <= @endDate
                      group by fullName
                      Order By diff 
end




GO
/****** Object:  StoredProcedure [dbo].[report_uploadedDocuments]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[report_uploadedDocuments]
@startDate datetime,@endDate datetime
as
begin
SELECT     TOP (100) PERCENT dbo.users.fullName, COUNT(dbo.documents.docID) AS docs
FROM         dbo.users INNER JOIN
                      dbo.documents ON dbo.users.userID = dbo.documents.addedUserID
      where     (dbo.documents.addedDate between @startDate and @endDate)           
GROUP BY dbo.users.fullName,dbo.documents.addedDate
ORDER BY docs DESC
end




GO
/****** Object:  StoredProcedure [dbo].[TasksSearch]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[TasksSearch]
@UserID int,
@lang nvarchar(10),
@SearchText nvarchar(500)
as
Select T.Id,T.TaskName,(T.TaskDate) as TaskDate,(T.TaskDate) as TaskTime,T.IsComplete,
Convert(varchar,T.TaskDate,103) as TaskDateOnly,LTRIM(RIGHT(CONVERT(VARCHAR(20), T.TaskDate, 100), 7)) as TaskTimeOnly,
(select fullname from users where userid=T.AssignTo)AssignTo,
(select fullname from users where userid=T.CreatedBy)CreatedBy,
(select Case When @lang=0 Then TT.EnText Else TT.ArText End from TaskTypes TT where TT.Id=T.TaskType)TaskType
From ToDoList T
where T.IsDeleted<>1 And (T.CreatedBy=@UserID OR T.AssignTo=@UserID)
And (T.TaskName Like '%' + @SearchText + '%' OR T.Description Like '%' + @SearchText + '%')

GO
/****** Object:  StoredProcedure [dbo].[updateBranchFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateBranchFolders] 
@branchID int,@fldrID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.branchFolders set dbo.branchFolders.branchID=' +@branchID+ ',dbo.branchFolders.fldrID=' +@fldrID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.branchFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateBranchFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateBranchFoldersByPrimaryKey] 
@branchID int,@fldrID int 
as 
begin transaction
Update dbo.branchFolders set dbo.branchFolders.branchID=@branchID,dbo.branchFolders.fldrID=@fldrID where @branchID=branchID and @fldrID=fldrID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.branchFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateBranchs]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateBranchs] 
@branchID int,@companyID int,@branchName nvarchar(500),@address nvarchar(1500),@tel1 varchar(50),@tel2 varchar(50),@zipcode varchar(50),@mainEmail varchar(500),@description nvarchar(4000),@isMainBranch bit,@branchNameAr nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.branchs set dbo.branchs.companyID=' +@companyID+ ',dbo.branchs.branchName=N''' +@branchName+ ''',dbo.branchs.address=N''' +@address+ ''',dbo.branchs.tel1=''' +@tel1+ ''',dbo.branchs.tel2=''' +@tel2+ ''',dbo.branchs.zipcode=''' +@zipcode+ ''',dbo.branchs.mainEmail=''' +@mainEmail+ ''',dbo.branchs.description=N''' +@description+ ''',dbo.branchs.isMainBranch=' +@isMainBranch+ ',dbo.branchs.branchNameAr=N''' +@branchNameAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.branchs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateBranchsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateBranchsByPrimaryKey] 
@branchID int,@companyID int,@branchName nvarchar(500),@address nvarchar(1500),@tel1 varchar(50),@tel2 varchar(50),@zipcode varchar(50),@mainEmail varchar(500),@description nvarchar(4000),@isMainBranch bit,@branchNameAr nvarchar(500) 
as 
begin transaction
Update dbo.branchs set dbo.branchs.companyID=@companyID,dbo.branchs.branchName=@branchName,dbo.branchs.address=@address,dbo.branchs.tel1=@tel1,dbo.branchs.tel2=@tel2,dbo.branchs.zipcode=@zipcode,dbo.branchs.mainEmail=@mainEmail,dbo.branchs.description=@description,dbo.branchs.isMainBranch=@isMainBranch,dbo.branchs.branchNameAr=@branchNameAr where @branchID=branchID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.branchs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateBrowseingEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateBrowseingEvents] 
@browseEventID int,@sysEventID int,@pageID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.browseingEvents set dbo.browseingEvents.sysEventID=' +@sysEventID+ ',dbo.browseingEvents.pageID=' +@pageID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.browseingEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateBrowseingEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateBrowseingEventsByPrimaryKey] 
@browseEventID int,@sysEventID int,@pageID int 
as 
begin transaction
Update dbo.browseingEvents set dbo.browseingEvents.sysEventID=@sysEventID,dbo.browseingEvents.pageID=@pageID where @browseEventID=browseEventID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.browseingEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateCompanies]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateCompanies] 
@companyID int,@companyName nvarchar(500),@address nvarchar(1500),@tel1 varchar(50),@tel2 varchar(50),@zipcode varchar(50),@mainEmail varchar(500),@description nvarchar(4000),@companyNameAr nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.companies set dbo.companies.companyName=N''' +@companyName+ ''',dbo.companies.address=N''' +@address+ ''',dbo.companies.tel1=''' +@tel1+ ''',dbo.companies.tel2=''' +@tel2+ ''',dbo.companies.zipcode=''' +@zipcode+ ''',dbo.companies.mainEmail=''' +@mainEmail+ ''',dbo.companies.description=N''' +@description+ ''',dbo.companies.companyNameAr=N''' +@companyNameAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.companies',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateCompaniesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateCompaniesByPrimaryKey] 
@companyID int,@companyName nvarchar(500),@address nvarchar(1500),@tel1 varchar(50),@tel2 varchar(50),@zipcode varchar(50),@mainEmail varchar(500),@description nvarchar(4000),@companyNameAr nvarchar(500) 
as 
begin transaction
Update dbo.companies set dbo.companies.companyName=@companyName,dbo.companies.address=@address,dbo.companies.tel1=@tel1,dbo.companies.tel2=@tel2,dbo.companies.zipcode=@zipcode,dbo.companies.mainEmail=@mainEmail,dbo.companies.description=@description,dbo.companies.companyNameAr=@companyNameAr where @companyID=companyID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.companies',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateCompanyFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateCompanyFolders] 
@companyID int,@fldrID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.companyFolders set dbo.companyFolders.companyID=' +@companyID+ ',dbo.companyFolders.fldrID=' +@fldrID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.companyFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateCompanyFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateCompanyFoldersByPrimaryKey] 
@companyID int,@fldrID int 
as 
begin transaction
Update dbo.companyFolders set dbo.companyFolders.companyID=@companyID,dbo.companyFolders.fldrID=@fldrID where @companyID=companyID and @fldrID=fldrID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.companyFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateControlsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateControlsTypes] 
@crtlID int,@ctrlDesc varchar(100),@ctrlDescAr nvarchar(100),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.controlsTypes set dbo.controlsTypes.crtlID=' +@crtlID+ ',dbo.controlsTypes.ctrlDesc=''' +@ctrlDesc+ ''',dbo.controlsTypes.ctrlDescAr=N''' +@ctrlDescAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.controlsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateControlsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateControlsTypesByPrimaryKey] 
@crtlID int,@ctrlDesc varchar(100),@ctrlDescAr nvarchar(100) 
as 
begin transaction
Update dbo.controlsTypes set dbo.controlsTypes.crtlID=@crtlID,dbo.controlsTypes.ctrlDesc=@ctrlDesc,dbo.controlsTypes.ctrlDescAr=@ctrlDescAr where @crtlID=crtlID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.controlsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDataBaseEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDataBaseEvents] 
@DBEventID int,@sysEventID int,@DBActionTypeID int,@tableName varchar(50),@parameters nvarchar(4000),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.dataBaseEvents set dbo.dataBaseEvents.sysEventID=' +@sysEventID+ ',dbo.dataBaseEvents.DBActionTypeID=' +@DBActionTypeID+ ',dbo.dataBaseEvents.tableName=''' +@tableName+ ''',dbo.dataBaseEvents.parameters=N''' +@parameters+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.dataBaseEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDataBaseEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDataBaseEventsByPrimaryKey] 
@DBEventID int,@sysEventID int,@DBActionTypeID int,@tableName varchar(50),@parameters nvarchar(4000) 
as 
begin transaction
Update dbo.dataBaseEvents set dbo.dataBaseEvents.sysEventID=@sysEventID,dbo.dataBaseEvents.DBActionTypeID=@DBActionTypeID,dbo.dataBaseEvents.tableName=@tableName,dbo.dataBaseEvents.parameters=@parameters where @DBEventID=DBEventID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.dataBaseEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDBActionsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDBActionsTypes] 
@DBActionTypeID int,@DBActionTypeDescA nvarchar(50),@FBActionTypeDescE varchar(50),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.DBActionsTypes set dbo.DBActionsTypes.DBActionTypeID=' +@DBActionTypeID+ ',dbo.DBActionsTypes.DBActionTypeDescA=N''' +@DBActionTypeDescA+ ''',dbo.DBActionsTypes.FBActionTypeDescE=''' +@FBActionTypeDescE+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.DBActionsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDBActionsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDBActionsTypesByPrimaryKey] 
@DBActionTypeID int,@DBActionTypeDescA nvarchar(50),@FBActionTypeDescE varchar(50) 
as 
begin transaction
Update dbo.DBActionsTypes set dbo.DBActionsTypes.DBActionTypeID=@DBActionTypeID,dbo.DBActionsTypes.DBActionTypeDescA=@DBActionTypeDescA,dbo.DBActionsTypes.FBActionTypeDescE=@FBActionTypeDescE where @DBActionTypeID=DBActionTypeID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.DBActionsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDepartments]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDepartments] 
@departmentID int,@departmentName nvarchar(1000),@headUserID int,@parentDepartmentID int,@departmentNameAr nvarchar(1000),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.departments set dbo.departments.departmentName=N''' +@departmentName+ ''',dbo.departments.headUserID=' +@headUserID+ ',dbo.departments.parentDepartmentID=' +@parentDepartmentID+ ',dbo.departments.departmentNameAr=N''' +@departmentNameAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.departments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDepartmentsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[updateDepartmentsByPrimaryKey] 
@departmentID int,@departmentName nvarchar(1000),@headUserID int,@parentDepartmentID int,@departmentNameAr nvarchar(1000),@parentID int
as 
begin transaction
Update dbo.departments set dbo.departments.departmentName=@departmentName,dbo.departments.headUserID=@headUserID,dbo.departments.parentDepartmentID=@parentDepartmentID,dbo.departments.departmentNameAr=@departmentNameAr,dbo.departments.parentID=@parentID where @departmentID=departmentID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.departments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocTypes] 
@docTypID int,@docTypDesc nvarchar(500),@active bit,@defaultWFID int,@docTypDescAr nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.docTypes set dbo.docTypes.docTypDesc=N''' +@docTypDesc+ ''',dbo.docTypes.active=' +@active+ ',dbo.docTypes.defaultWFID=' +@defaultWFID+ ',dbo.docTypes.docTypDescAr=N''' +@docTypDescAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.docTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocTypesByPrimaryKey] 
@docTypID int,@docTypDesc nvarchar(500),@active bit,@defaultWFID int,@docTypDescAr nvarchar(500) 
as 
begin transaction
Update dbo.docTypes set dbo.docTypes.docTypDesc=@docTypDesc,dbo.docTypes.active=@active,dbo.docTypes.defaultWFID=@defaultWFID,dbo.docTypes.docTypDescAr=@docTypDescAr where @docTypID=docTypID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.docTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentMataValues]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentMataValues] 
@metaID int,@docID bigint,@value nvarchar(4000),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.documentMataValues set dbo.documentMataValues.metaID=' +@metaID+ ',dbo.documentMataValues.docID=' +@docID+ ',dbo.documentMataValues.value=N''' +@value+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentMataValues',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentMataValuesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentMataValuesByPrimaryKey] 
@metaID int,@docID bigint,@value nvarchar(4000) 
as 
begin transaction
Update dbo.documentMataValues set dbo.documentMataValues.metaID=@metaID,dbo.documentMataValues.docID=@docID,dbo.documentMataValues.value=@value where @docID=docID and @metaID=metaID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentMataValues',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocuments]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocuments] 
@docID bigint,@docTypID int,@docName nvarchar(500),@docExt varchar(4),@addedDate datetime,@addedUserID int,@lastVersion smallint,@modifyDate datetime,@modifyUserID int,@fldrID int,@ocrContent ntext,@folderSeq bigint,@docTypeSeq bigint,@folderDocTypeSeq bigint,@wfPathID int,@wfCurrentSeq smallint,@wfCurrentRecipientID int,@wfCurrentRecipientType smallint,@wfStartDateTime datetime,@wfTimeFrame decimal(18,0),@wfStatus smallint,@meta1 nvarchar(4000),@meta2 nvarchar(4000),@meta3 nvarchar(4000),@meta4 nvarchar(4000),@meta5 nvarchar(4000),@meta6 nvarchar(4000),@meta7 nvarchar(4000),@meta8 nvarchar(4000),@meta9 nvarchar(4000),@meta10 nvarchar(4000),@meta11 nvarchar(4000),@meta12 nvarchar(4000),@meta13 nvarchar(4000),@meta14 nvarchar(4000),@meta15 nvarchar(4000),@meta16 nvarchar(4000),@meta17 nvarchar(4000),@meta18 nvarchar(4000),@meta19 nvarchar(4000),@meta20 nvarchar(4000),@meta21 nvarchar(4000),@meta22 nvarchar(4000),@meta23 nvarchar(4000),@meta24 nvarchar(4000),@meta25 nvarchar(4000),@meta26 nvarchar(4000),@meta27 nvarchar(4000),@meta28 nvarchar(4000),@meta29 nvarchar(4000),@meta30 nvarchar(4000),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.documents set dbo.documents.docTypID=' +@docTypID+ ',dbo.documents.docName=N''' +@docName+ ''',dbo.documents.docExt=''' +@docExt+ ''',dbo.documents.addedDate=''' +@addedDate+ ''',dbo.documents.addedUserID=' +@addedUserID+ ',dbo.documents.lastVersion=' +@lastVersion+ ',dbo.documents.modifyDate=''' +@modifyDate+ ''',dbo.documents.modifyUserID=' +@modifyUserID+ ',dbo.documents.fldrID=' +@fldrID+ ',dbo.documents.ocrContent=N''' +@ocrContent+ ''',dbo.documents.folderSeq=' +@folderSeq+ ',dbo.documents.docTypeSeq=' +@docTypeSeq+ ',dbo.documents.folderDocTypeSeq=' +@folderDocTypeSeq+ ',dbo.documents.wfPathID=' +@wfPathID+ ',dbo.documents.wfCurrentSeq=' +@wfCurrentSeq+ ',dbo.documents.wfCurrentRecipientID=' +@wfCurrentRecipientID+ ',dbo.documents.wfCurrentRecipientType=' +@wfCurrentRecipientType+ ',dbo.documents.wfStartDateTime=''' +@wfStartDateTime+ ''',dbo.documents.wfTimeFrame=' +@wfTimeFrame+ ',dbo.documents.wfStatus=' +@wfStatus+ ',dbo.documents.meta1=N''' +@meta1+ ''',dbo.documents.meta2=N''' +@meta2+ ''',dbo.documents.meta3=N''' +@meta3+ ''',dbo.documents.meta4=N''' +@meta4+ ''',dbo.documents.meta5=N''' +@meta5+ ''',dbo.documents.meta6=N''' +@meta6+ ''',dbo.documents.meta7=N''' +@meta7+ ''',dbo.documents.meta8=N''' +@meta8+ ''',dbo.documents.meta9=N''' +@meta9+ ''',dbo.documents.meta10=N''' +@meta10+ ''',dbo.documents.meta11=N''' +@meta11+ ''',dbo.documents.meta12=N''' +@meta12+ ''',dbo.documents.meta13=N''' +@meta13+ ''',dbo.documents.meta14=N''' +@meta14+ ''',dbo.documents.meta15=N''' +@meta15+ ''',dbo.documents.meta16=N''' +@meta16+ ''',dbo.documents.meta17=N''' +@meta17+ ''',dbo.documents.meta18=N''' +@meta18+ ''',dbo.documents.meta19=N''' +@meta19+ ''',dbo.documents.meta20=N''' +@meta20+ ''',dbo.documents.meta21=N''' +@meta21+ ''',dbo.documents.meta22=N''' +@meta22+ ''',dbo.documents.meta23=N''' +@meta23+ ''',dbo.documents.meta24=N''' +@meta24+ ''',dbo.documents.meta25=N''' +@meta25+ ''',dbo.documents.meta26=N''' +@meta26+ ''',dbo.documents.meta27=N''' +@meta27+ ''',dbo.documents.meta28=N''' +@meta28+ ''',dbo.documents.meta29=N''' +@meta29+ ''',dbo.documents.meta30=N''' +@meta30+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentsByPrimaryKey] 
@docID bigint,@docTypID int,@docName nvarchar(500),@docExt varchar(4),@addedDate datetime,@addedUserID int,@lastVersion smallint,@modifyDate datetime,@modifyUserID int,@fldrID int,@ocrContent ntext,@folderSeq bigint,@docTypeSeq bigint,@folderDocTypeSeq bigint,@wfPathID int,@wfCurrentSeq smallint,@wfCurrentRecipientID int,@wfCurrentRecipientType smallint,@wfStartDateTime datetime,@wfTimeFrame decimal(18,0),@wfStatus smallint,@meta1 nvarchar(4000),@meta2 nvarchar(4000),@meta3 nvarchar(4000),@meta4 nvarchar(4000),@meta5 nvarchar(4000),@meta6 nvarchar(4000),@meta7 nvarchar(4000),@meta8 nvarchar(4000),@meta9 nvarchar(4000),@meta10 nvarchar(4000),@meta11 nvarchar(4000),@meta12 nvarchar(4000),@meta13 nvarchar(4000),@meta14 nvarchar(4000),@meta15 nvarchar(4000),@meta16 nvarchar(4000),@meta17 nvarchar(4000),@meta18 nvarchar(4000),@meta19 nvarchar(4000),@meta20 nvarchar(4000),@meta21 nvarchar(4000),@meta22 nvarchar(4000),@meta23 nvarchar(4000),@meta24 nvarchar(4000),@meta25 nvarchar(4000),@meta26 nvarchar(4000),@meta27 nvarchar(4000),@meta28 nvarchar(4000),@meta29 nvarchar(4000),@meta30 nvarchar(4000) 
as 
begin transaction
Update dbo.documents set dbo.documents.docTypID=@docTypID,dbo.documents.docName=@docName,dbo.documents.docExt=@docExt,dbo.documents.addedDate=@addedDate,dbo.documents.addedUserID=@addedUserID,dbo.documents.lastVersion=@lastVersion,dbo.documents.modifyDate=@modifyDate,dbo.documents.modifyUserID=@modifyUserID,dbo.documents.fldrID=@fldrID,dbo.documents.ocrContent=@ocrContent,dbo.documents.folderSeq=@folderSeq,dbo.documents.docTypeSeq=@docTypeSeq,dbo.documents.folderDocTypeSeq=@folderDocTypeSeq,dbo.documents.wfPathID=@wfPathID,dbo.documents.wfCurrentSeq=@wfCurrentSeq,dbo.documents.wfCurrentRecipientID=@wfCurrentRecipientID,dbo.documents.wfCurrentRecipientType=@wfCurrentRecipientType,dbo.documents.wfStartDateTime=@wfStartDateTime,dbo.documents.wfTimeFrame=@wfTimeFrame,dbo.documents.wfStatus=@wfStatus,dbo.documents.meta1=@meta1,dbo.documents.meta2=@meta2,dbo.documents.meta3=@meta3,dbo.documents.meta4=@meta4,dbo.documents.meta5=@meta5,dbo.documents.meta6=@meta6,dbo.documents.meta7=@meta7,dbo.documents.meta8=@meta8,dbo.documents.meta9=@meta9,dbo.documents.meta10=@meta10,dbo.documents.meta11=@meta11,dbo.documents.meta12=@meta12,dbo.documents.meta13=@meta13,dbo.documents.meta14=@meta14,dbo.documents.meta15=@meta15,dbo.documents.meta16=@meta16,dbo.documents.meta17=@meta17,dbo.documents.meta18=@meta18,dbo.documents.meta19=@meta19,dbo.documents.meta20=@meta20,dbo.documents.meta21=@meta21,dbo.documents.meta22=@meta22,dbo.documents.meta23=@meta23,dbo.documents.meta24=@meta24,dbo.documents.meta25=@meta25,dbo.documents.meta26=@meta26,dbo.documents.meta27=@meta27,dbo.documents.meta28=@meta28,dbo.documents.meta29=@meta29,dbo.documents.meta30=@meta30 where @docID=docID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentsGroups]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentsGroups] 
@docGroupID int,@docGroupTitle nvarchar(4000),@docTypeID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.documentsGroups set dbo.documentsGroups.docGroupTitle=N''' +@docGroupTitle+ ''',dbo.documentsGroups.docTypeID=' +@docTypeID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentsGroups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentsGroupsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentsGroupsByPrimaryKey] 
@docGroupID int,@docGroupTitle nvarchar(4000),@docTypeID int 
as 
begin transaction
Update dbo.documentsGroups set dbo.documentsGroups.docGroupTitle=@docGroupTitle,dbo.documentsGroups.docTypeID=@docTypeID where @docGroupID=docGroupID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentsGroups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentsTypes] 
@docCode int,@docTypeDesc nvarchar(50),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.documentsTypes set dbo.documentsTypes.docCode=' +@docCode+ ',dbo.documentsTypes.docTypeDesc=N''' +@docTypeDesc+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentsTypes',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[updateDocumentsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentsTypesByPrimaryKey] 
@docCode int,@docTypeDesc nvarchar(50) 
as 
begin transaction
Update dbo.documentsTypes set dbo.documentsTypes.docCode=@docCode,dbo.documentsTypes.docTypeDesc=@docTypeDesc where @docCode=docCode; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentsTypes',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[updateDocumentsWithOutMeta]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[updateDocumentsWithOutMeta] 
@docID bigint,@docTypID int,@docName nvarchar(500),@docExt varchar(4),@addedDate datetime,@addedUserID int,@lastVersion smallint,@modifyDate datetime,@modifyUserID int,@fldrID int,@ocrContent ntext,@folderSeq bigint,@docTypeSeq bigint,@folderDocTypeSeq bigint,@wfPathID int,@wfCurrentSeq smallint,@wfCurrentRecipientID int,@wfCurrentRecipientType smallint,@wfStartDateTime datetime,@wfTimeFrame decimal(18,0),@wfStatus smallint
as 
begin transaction
Update dbo.documents set dbo.documents.docTypID=@docTypID,dbo.documents.docName=@docName,dbo.documents.docExt=@docExt,dbo.documents.addedDate=@addedDate,dbo.documents.addedUserID=@addedUserID,dbo.documents.lastVersion=@lastVersion,dbo.documents.modifyDate=@modifyDate,dbo.documents.modifyUserID=@modifyUserID,dbo.documents.fldrID=@fldrID,dbo.documents.ocrContent=@ocrContent,dbo.documents.folderSeq=@folderSeq,dbo.documents.docTypeSeq=@docTypeSeq,dbo.documents.folderDocTypeSeq=@folderDocTypeSeq,dbo.documents.wfPathID=@wfPathID,dbo.documents.wfCurrentSeq=@wfCurrentSeq,dbo.documents.wfCurrentRecipientID=@wfCurrentRecipientID,
dbo.documents.wfCurrentRecipientType=@wfCurrentRecipientType,dbo.documents.wfStartDateTime=@wfStartDateTime,dbo.documents.wfTimeFrame=@wfTimeFrame,dbo.documents.wfStatus=@wfStatus where @docID=docID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documents',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[updateDocumentVersions]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentVersions] 
@docID bigint,@version smallint,@addedDate datetime,@addedUserID int,@ext varchar(4),@docGroupID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.documentVersions set dbo.documentVersions.docID=' +@docID+ ',dbo.documentVersions.version=' +@version+ ',dbo.documentVersions.addedDate=''' +@addedDate+ ''',dbo.documentVersions.addedUserID=' +@addedUserID+ ',dbo.documentVersions.ext=''' +@ext+ ''',dbo.documentVersions.docGroupID=' +@docGroupID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentVersions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentVersionsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentVersionsByPrimaryKey] 
@docID bigint,@version smallint,@addedDate datetime,@addedUserID int,@ext varchar(4),@docGroupID int 
as 
begin transaction
Update dbo.documentVersions set dbo.documentVersions.docID=@docID,dbo.documentVersions.version=@version,dbo.documentVersions.addedDate=@addedDate,dbo.documentVersions.addedUserID=@addedUserID,dbo.documentVersions.ext=@ext,dbo.documentVersions.docGroupID=@docGroupID where @docID=docID and @version=version; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentVersions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentWFPath]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateDocumentWFPath] 
@ID int,@docID bigint,@userID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@userNotes ntext,@receiveDate datetime,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.documentWFPath set dbo.documentWFPath.docID=' +@docID+ ',dbo.documentWFPath.userID=' +@userID+ ',dbo.documentWFPath.actionDateTime=''' +@actionDateTime+ ''',dbo.documentWFPath.wfPathID=' +@wfPathID+ ',dbo.documentWFPath.wfSeqNo=' +@wfSeqNo+ ',dbo.documentWFPath.actionType=' +@actionType+ ',dbo.documentWFPath.recipientType=' +@recipientType+ ',dbo.documentWFPath.userNotes=N''' +@userNotes+ ''',dbo.documentWFPath.receiveDate=''' +@receiveDate+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateDocumentWFPathByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[updateDocumentWFPathByPrimaryKey] 
@ID int,@docID bigint,@userID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@userNotes ntext,@receiveDate datetime ,@endDate datetime
as 
begin transaction
Update dbo.documentWFPath set dbo.documentWFPath.docID=@docID,dbo.documentWFPath.EndDate=@endDate,dbo.documentWFPath.userID=@userID,dbo.documentWFPath.actionDateTime=@actionDateTime,dbo.documentWFPath.wfPathID=@wfPathID,dbo.documentWFPath.wfSeqNo=@wfSeqNo,dbo.documentWFPath.actionType=@actionType,dbo.documentWFPath.recipientType=@recipientType,dbo.documentWFPath.userNotes=@userNotes,dbo.documentWFPath.receiveDate=@receiveDate where @ID=ID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.documentWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEFormFields]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEFormFields] 
@formID int,@fieldSeq int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.eFormFields set dbo.eFormFields.formID=' +@formID+ ',dbo.eFormFields.fieldSeq=' +@fieldSeq+ ',dbo.eFormFields.metaDesc=N''' +@metaDesc+ ''',dbo.eFormFields.metaDataType=''' +@metaDataType+ ''',dbo.eFormFields.required=' +@required+ ',dbo.eFormFields.orderSeq=' +@orderSeq+ ',dbo.eFormFields.ctrlID=' +@ctrlID+ ',dbo.eFormFields.defaultTexts=N''' +@defaultTexts+ ''',dbo.eFormFields.defaultValues=N''' +@defaultValues+ ''',dbo.eFormFields.visible=' +@visible + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEFormFieldsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEFormFieldsByPrimaryKey] 
@formID int,@fieldSeq int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit 
as 
begin transaction
Update dbo.eFormFields set dbo.eFormFields.formID=@formID,dbo.eFormFields.fieldSeq=@fieldSeq,dbo.eFormFields.metaDesc=@metaDesc,dbo.eFormFields.metaDataType=@metaDataType,dbo.eFormFields.required=@required,dbo.eFormFields.orderSeq=@orderSeq,dbo.eFormFields.ctrlID=@ctrlID,dbo.eFormFields.defaultTexts=@defaultTexts,dbo.eFormFields.defaultValues=@defaultValues,dbo.eFormFields.visible=@visible where @fieldSeq=fieldSeq and @formID=formID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEForms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEForms] 
@formID int,@formName nvarchar(500),@defaultPathID int,@active bit,@catID int,@catPrgID int,@formNameAr nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.eForms set dbo.eForms.formName=N''' +@formName+ ''',dbo.eForms.defaultPathID=' +@defaultPathID+ ',dbo.eForms.active=' +@active+ ',dbo.eForms.catID=' +@catID+ ',dbo.eForms.catPrgID=' +@catPrgID+ ',dbo.eForms.formNameAr=N''' +@formNameAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEFormsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEFormsByPrimaryKey] 
@formID int,@formName nvarchar(500),@defaultPathID int,@active bit,@catID int,@catPrgID int,@formNameAr nvarchar(500) 
as 
begin transaction
Update dbo.eForms set dbo.eForms.formName=@formName,dbo.eForms.defaultPathID=@defaultPathID,dbo.eForms.active=@active,dbo.eForms.catID=@catID,dbo.eForms.catPrgID=@catPrgID,dbo.eForms.formNameAr=@formNameAr where @formID=formID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEformsCategories]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEformsCategories] 
@catID int,@catTitle nvarchar(500),@catPrgID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.eformsCategories set dbo.eformsCategories.catTitle=N''' +@catTitle+ ''',dbo.eformsCategories.catPrgID=' +@catPrgID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eformsCategories',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEformsCategoriesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEformsCategoriesByPrimaryKey] 
@catID int,@catTitle nvarchar(500),@catPrgID int 
as 
begin transaction
Update dbo.eformsCategories set dbo.eformsCategories.catTitle=@catTitle,dbo.eformsCategories.catPrgID=@catPrgID where @catID=catID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eformsCategories',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEformWFPath]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEformWFPath] 
@ID int,@docID int,@userID int,@actionUserID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.eformWFPath set dbo.eformWFPath.docID=' +@docID+ ',dbo.eformWFPath.userID=' +@userID+ ',dbo.eformWFPath.actionUserID=' +@actionUserID+ ',dbo.eformWFPath.actionDateTime=''' +@actionDateTime+ ''',dbo.eformWFPath.wfPathID=' +@wfPathID+ ',dbo.eformWFPath.wfSeqNo=' +@wfSeqNo+ ',dbo.eformWFPath.actionType=' +@actionType+ ',dbo.eformWFPath.recipientType=' +@recipientType + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eformWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEformWFPathByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEformWFPathByPrimaryKey] 
@ID int,@docID int,@userID int,@actionUserID int,@actionDateTime datetime,@wfPathID int,@wfSeqNo smallint,@actionType smallint,@recipientType smallint 
as 
begin transaction
Update dbo.eformWFPath set dbo.eformWFPath.docID=@docID,dbo.eformWFPath.userID=@userID,dbo.eformWFPath.actionUserID=@actionUserID,dbo.eformWFPath.actionDateTime=@actionDateTime,dbo.eformWFPath.wfPathID=@wfPathID,dbo.eformWFPath.wfSeqNo=@wfSeqNo,dbo.eformWFPath.actionType=@actionType,dbo.eformWFPath.recipientType=@recipientType where @ID=ID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eformWFPath',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEventsTypes]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEventsTypes] 
@eventTypeID int,@eventTypeDescA nvarchar(50),@eventTypeDescE varchar(50),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.eventsTypes set dbo.eventsTypes.eventTypeID=' +@eventTypeID+ ',dbo.eventsTypes.eventTypeDescA=N''' +@eventTypeDescA+ ''',dbo.eventsTypes.eventTypeDescE=''' +@eventTypeDescE+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eventsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateEventsTypesByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateEventsTypesByPrimaryKey] 
@eventTypeID int,@eventTypeDescA nvarchar(50),@eventTypeDescE varchar(50) 
as 
begin transaction
Update dbo.eventsTypes set dbo.eventsTypes.eventTypeID=@eventTypeID,dbo.eventsTypes.eventTypeDescA=@eventTypeDescA,dbo.eventsTypes.eventTypeDescE=@eventTypeDescE where @eventTypeID=eventTypeID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.eventsTypes',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateFolders] 
@fldrID int,@fldrName nvarchar(500),@fldrParentID int,@active bit,@iconID int,@defaultDocTypID int,@folderOrder int,@isDiwan bit,@fldrNameAr nvarchar(500),@allowWF bit,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.folders set dbo.folders.fldrName=N''' +@fldrName+ ''',dbo.folders.fldrParentID=' +@fldrParentID+ ',dbo.folders.active=' +@active+ ',dbo.folders.iconID=' +@iconID+ ',dbo.folders.defaultDocTypID=' +@defaultDocTypID+ ',dbo.folders.folderOrder=' +@folderOrder+ ',dbo.folders.isDiwan=' +@isDiwan+ ',dbo.folders.fldrNameAr=N''' +@fldrNameAr+ ''',dbo.folders.allowWF=' +@allowWF + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.folders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- alter updateFoldersByPrimaryKey
CREATE procedure [dbo].[updateFoldersByPrimaryKey] 
@fldrID int,@fldrName nvarchar(500),@fldrParentID int,@active bit,@iconID int,@defaultDocTypID int,@folderOrder int,@isDiwan bit,@fldrNameAr nvarchar(500),@allowWF bit ,@folderOwner int
as 
begin transaction
Update dbo.folders set dbo.folders.fldrName=@fldrName,dbo.folders.fldrParentID=@fldrParentID,dbo.folders.active=@active,dbo.folders.iconID=@iconID,dbo.folders.defaultDocTypID=@defaultDocTypID,dbo.folders.folderOrder=@folderOrder,dbo.folders.isDiwan=@isDiwan,dbo.folders.fldrNameAr=@fldrNameAr,dbo.folders.allowWF=@allowWF,folderOwner=@folderOwner where @fldrID=fldrID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.folders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroupBlocks]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroupBlocks] 
@grpID int,@docTypID int,@blockNum int,@blockLeft varchar(6),@blockTop varchar(6),@blockWidth varchar(6),@blockHeight varchar(6),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.groupBlocks set dbo.groupBlocks.grpID=' +@grpID+ ',dbo.groupBlocks.docTypID=' +@docTypID+ ',dbo.groupBlocks.blockNum=' +@blockNum+ ',dbo.groupBlocks.blockLeft=''' +@blockLeft+ ''',dbo.groupBlocks.blockTop=''' +@blockTop+ ''',dbo.groupBlocks.blockWidth=''' +@blockWidth+ ''',dbo.groupBlocks.blockHeight=''' +@blockHeight+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groupBlocks',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroupBlocksByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroupBlocksByPrimaryKey] 
@grpID int,@docTypID int,@blockNum int,@blockLeft varchar(6),@blockTop varchar(6),@blockWidth varchar(6),@blockHeight varchar(6) 
as 
begin transaction
Update dbo.groupBlocks set dbo.groupBlocks.grpID=@grpID,dbo.groupBlocks.docTypID=@docTypID,dbo.groupBlocks.blockNum=@blockNum,dbo.groupBlocks.blockLeft=@blockLeft,dbo.groupBlocks.blockTop=@blockTop,dbo.groupBlocks.blockWidth=@blockWidth,dbo.groupBlocks.blockHeight=@blockHeight where @blockNum=blockNum and @docTypID=docTypID and @grpID=grpID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groupBlocks',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroupFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroupFolders] 
@grpID int,@fldrID int,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@allowCreateFldr bit,@allowRenameFldr bit,@allowRelocationFldr bit,@inheritSubFolders bit,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.groupFolders set dbo.groupFolders.grpID=' +@grpID+ ',dbo.groupFolders.fldrID=' +@fldrID+ ',dbo.groupFolders.allowInsert=' +@allowInsert+ ',dbo.groupFolders.allowUpdate=' +@allowUpdate+ ',dbo.groupFolders.allowDelete=' +@allowDelete+ ',dbo.groupFolders.allowCreateFldr=' +@allowCreateFldr+ ',dbo.groupFolders.allowRenameFldr=' +@allowRenameFldr+ ',dbo.groupFolders.allowRelocationFldr=' +@allowRelocationFldr+ ',dbo.groupFolders.inheritSubFolders=' +@inheritSubFolders + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groupFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroupFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroupFoldersByPrimaryKey] 
@grpID int,@fldrID int,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@allowCreateFldr bit,@allowRenameFldr bit,@allowRelocationFldr bit,@inheritSubFolders bit 
as 
begin transaction
Update dbo.groupFolders set dbo.groupFolders.grpID=@grpID,dbo.groupFolders.fldrID=@fldrID,dbo.groupFolders.allowInsert=@allowInsert,dbo.groupFolders.allowUpdate=@allowUpdate,dbo.groupFolders.allowDelete=@allowDelete,dbo.groupFolders.allowCreateFldr=@allowCreateFldr,dbo.groupFolders.allowRenameFldr=@allowRenameFldr,dbo.groupFolders.allowRelocationFldr=@allowRelocationFldr,dbo.groupFolders.inheritSubFolders=@inheritSubFolders where @fldrID=fldrID and @grpID=grpID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groupFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroupPrograms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroupPrograms] 
@groupID int,@programID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.groupPrograms set dbo.groupPrograms.groupID=' +@groupID+ ',dbo.groupPrograms.programID=' +@programID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groupPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroupProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroupProgramsByPrimaryKey] 
@groupID int,@programID int 
as 
begin transaction
Update dbo.groupPrograms set dbo.groupPrograms.groupID=@groupID,dbo.groupPrograms.programID=@programID where @groupID=groupID and @programID=programID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groupPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroups]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroups] 
@grpID int,@grpDesc nvarchar(500),@companyID int,@branchID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.groups set dbo.groups.grpDesc=N''' +@grpDesc+ ''',dbo.groups.companyID=' +@companyID+ ',dbo.groups.branchID=' +@branchID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateGroupsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateGroupsByPrimaryKey] 
@grpID int,@grpDesc nvarchar(500),@companyID int,@branchID int 
as 
begin transaction
Update dbo.groups set dbo.groups.grpDesc=@grpDesc,dbo.groups.companyID=@companyID,dbo.groups.branchID=@branchID where @grpID=grpID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.groups',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateIcons]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateIcons] 
@iconID int,@iconDescription nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.icons set dbo.icons.iconDescription=N''' +@iconDescription+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.icons',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateIconsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateIconsByPrimaryKey] 
@iconID int,@iconDescription nvarchar(500) 
as 
begin transaction
Update dbo.icons set dbo.icons.iconDescription=@iconDescription where @iconID=iconID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.icons',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateLoginEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateLoginEvents] 
@loginID int,@sysEventID int,@IPAddress nvarchar(50),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.loginEvents set dbo.loginEvents.sysEventID=' +@sysEventID+ ',dbo.loginEvents.IPAddress=N''' +@IPAddress+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.loginEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateLoginEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateLoginEventsByPrimaryKey] 
@loginID int,@sysEventID int,@IPAddress nvarchar(50) 
as 
begin transaction
Update dbo.loginEvents set dbo.loginEvents.sysEventID=@sysEventID,dbo.loginEvents.IPAddress=@IPAddress where @loginID=loginID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.loginEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateMetas]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[updateMetas] 
@metaID int,@docTypID int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit,@metaDescAr nvarchar(500),@defaultArTexts  nvarchar(4000),@columnSeq int,@metaIdFK int,@width float,@permissionType nvarchar(20),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.metas set dbo.metas.docTypID=' +@docTypID+ ',dbo.metas.metaDesc=N''' +@metaDesc+ ''',dbo.metas.metaDataType=''' +@metaDataType+ ''',dbo.metas.required=' +@required+ ',dbo.metas.orderSeq=' +@orderSeq+ ',dbo.metas.ctrlID=' +@ctrlID+ ',dbo.metas.defaultTexts=N''' +@defaultTexts+ ''',dbo.metas.defaultValues=N''' +@defaultValues+ ''',dbo.metas.visible=' +@visible+ ',dbo.metas.metaDescAr=N''' +@metaDescAr+ ''',dbo.metas.defaultArTexts=N''' +@defaultArTexts+''' ,dbo.metas.columnSeq=' +@columnSeq + ' ,dbo.metas.permissionType=''' + @permissionType +''' ,dbo.metas.metaIdFK=' + @metaIdFK +' ,dbo.metas.width=' + @width + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.metas',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[updateMetasByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[updateMetasByPrimaryKey] 
@metaID int,@docTypID int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit,@metaDescAr nvarchar(500) ,@defaultArTexts  nvarchar(4000),@columnSeq int,@metaIdFK int,@width float,@permissionType nvarchar(20)
as 
begin transaction
Update dbo.metas set dbo.metas.docTypID=@docTypID,dbo.metas.metaDesc=@metaDesc,dbo.metas.metaDataType=@metaDataType,dbo.metas.required=@required,dbo.metas.orderSeq=@orderSeq,dbo.metas.ctrlID=@ctrlID,dbo.metas.defaultTexts=@defaultTexts,dbo.metas.defaultValues=@defaultValues,dbo.metas.visible=@visible,dbo.metas.metaDescAr=@metaDescAr,dbo.metas.defaultArTexts = @defaultArTexts,dbo.metas.columnSeq = @columnSeq,dbo.metas.metaIdFK = @metaIdFK,dbo.metas.width = @width ,dbo.metas.permissionType = @permissionType where @metaID=metaID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.metas',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[updatePathDetails]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updatePathDetails] 
@pathID int,@seqNo smallint,@userID int,@endOfPath bit,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.pathDetails set dbo.pathDetails.pathID=' +@pathID+ ',dbo.pathDetails.seqNo=' +@seqNo+ ',dbo.pathDetails.userID=' +@userID+ ',dbo.pathDetails.endOfPath=' +@endOfPath + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.pathDetails',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[updatePathDetailsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updatePathDetailsByPrimaryKey] 
@pathID int,@seqNo smallint,@userID int,@endOfPath bit 
as 
begin transaction
Update dbo.pathDetails set dbo.pathDetails.pathID=@pathID,dbo.pathDetails.seqNo=@seqNo,dbo.pathDetails.userID=@userID,dbo.pathDetails.endOfPath=@endOfPath where @pathID=pathID and @seqNo=seqNo; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.pathDetails',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[updatePositions]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updatePositions] 
@positionID int,@positionTitle nvarchar(500),@positionTitleAr nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.positions set dbo.positions.positionTitle=N''' +@positionTitle+ ''',dbo.positions.positionTitleAr=N''' +@positionTitleAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.positions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updatePositionsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updatePositionsByPrimaryKey] 
@positionID int,@positionTitle nvarchar(500),@positionTitleAr nvarchar(500) 
as 
begin transaction
Update dbo.positions set dbo.positions.positionTitle=@positionTitle,dbo.positions.positionTitleAr=@positionTitleAr where @positionID=positionID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.positions',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updatePrograms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updatePrograms] 
@programID int,@programName nvarchar(500),@parentProgramID int,@programURL varchar(2000),@windowWidth int,@windowHeight int,@programNameAr nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.programs set dbo.programs.programName=N''' +@programName+ ''',dbo.programs.parentProgramID=' +@parentProgramID+ ',dbo.programs.programURL=''' +@programURL+ ''',dbo.programs.windowWidth=' +@windowWidth+ ',dbo.programs.windowHeight=' +@windowHeight+ ',dbo.programs.programNameAr=N''' +@programNameAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.programs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateProgramsByPrimaryKey] 
@programID int,@programName nvarchar(500),@parentProgramID int,@programURL varchar(2000),@windowWidth int,@windowHeight int,@programNameAr nvarchar(500) 
as 
begin transaction
Update dbo.programs set dbo.programs.programName=@programName,dbo.programs.parentProgramID=@parentProgramID,dbo.programs.programURL=@programURL,dbo.programs.windowWidth=@windowWidth,dbo.programs.windowHeight=@windowHeight,dbo.programs.programNameAr=@programNameAr where @programID=programID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.programs',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateSettings]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateSettings] 
@ID int,@allowedUsersCount nvarchar(1000),@systemActive nvarchar(1000),@systemActiveDate nvarchar(4000),@passwordStrength smallint,@passwordAllowStartSpace bit,@passwordLength smallint,@allowUsernamePasswordMatch bit,@firstLoginChangePassword bit,@passwordAgeDays int,@sessionTimeoutMinutes int,@lockTimeOut smallint,@outgoingMailServer varchar(100),@workflowEmail varchar(100),@workflowEmailPassword nvarchar(4000),@systemEmail varchar(100),@systemEmailPassword nvarchar(4000),@workflowEmailSubject nvarchar(150),@workflowEmailBody ntext,@systemEmailSignature nvarchar(4000),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.settings set dbo.settings.ID=' +@ID+ ',dbo.settings.allowedUsersCount=N''' +@allowedUsersCount+ ''',dbo.settings.systemActive=N''' +@systemActive+ ''',dbo.settings.systemActiveDate=N''' +@systemActiveDate+ ''',dbo.settings.passwordStrength=' +@passwordStrength+ ',dbo.settings.passwordAllowStartSpace=' +@passwordAllowStartSpace+ ',dbo.settings.passwordLength=' +@passwordLength+ ',dbo.settings.allowUsernamePasswordMatch=' +@allowUsernamePasswordMatch+ ',dbo.settings.firstLoginChangePassword=' +@firstLoginChangePassword+ ',dbo.settings.passwordAgeDays=' +@passwordAgeDays+ ',dbo.settings.sessionTimeoutMinutes=' +@sessionTimeoutMinutes+ ',dbo.settings.lockTimeOut=' +@lockTimeOut+ ',dbo.settings.outgoingMailServer=''' +@outgoingMailServer+ ''',dbo.settings.workflowEmail=''' +@workflowEmail+ ''',dbo.settings.workflowEmailPassword=N''' +@workflowEmailPassword+ ''',dbo.settings.systemEmail=''' +@systemEmail+ ''',dbo.settings.systemEmailPassword=N''' +@systemEmailPassword+ ''',dbo.settings.workflowEmailSubject=N''' +@workflowEmailSubject+ ''',dbo.settings.workflowEmailBody=N''' +@workflowEmailBody+ ''',dbo.settings.systemEmailSignature=N''' +@systemEmailSignature+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.settings',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateSettingsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateSettingsByPrimaryKey] 
@ID int,@allowedUsersCount nvarchar(1000),@systemActive nvarchar(1000),@systemActiveDate nvarchar(4000),@passwordStrength smallint,@passwordAllowStartSpace bit,@passwordLength smallint,@allowUsernamePasswordMatch bit,@firstLoginChangePassword bit,@passwordAgeDays int,@sessionTimeoutMinutes int,@lockTimeOut smallint,@outgoingMailServer varchar(100),@workflowEmail varchar(100),@workflowEmailPassword nvarchar(4000),@systemEmail varchar(100),@systemEmailPassword nvarchar(4000),@workflowEmailSubject nvarchar(150),@workflowEmailBody ntext,@systemEmailSignature nvarchar(4000) 
as 
begin transaction
Update dbo.settings set dbo.settings.ID=@ID,dbo.settings.allowedUsersCount=@allowedUsersCount,dbo.settings.systemActive=@systemActive,dbo.settings.systemActiveDate=@systemActiveDate,dbo.settings.passwordStrength=@passwordStrength,dbo.settings.passwordAllowStartSpace=@passwordAllowStartSpace,dbo.settings.passwordLength=@passwordLength,dbo.settings.allowUsernamePasswordMatch=@allowUsernamePasswordMatch,dbo.settings.firstLoginChangePassword=@firstLoginChangePassword,dbo.settings.passwordAgeDays=@passwordAgeDays,dbo.settings.sessionTimeoutMinutes=@sessionTimeoutMinutes,dbo.settings.lockTimeOut=@lockTimeOut,dbo.settings.outgoingMailServer=@outgoingMailServer,dbo.settings.workflowEmail=@workflowEmail,dbo.settings.workflowEmailPassword=@workflowEmailPassword,dbo.settings.systemEmail=@systemEmail,dbo.settings.systemEmailPassword=@systemEmailPassword,dbo.settings.workflowEmailSubject=@workflowEmailSubject,dbo.settings.workflowEmailBody=@workflowEmailBody,dbo.settings.systemEmailSignature=@systemEmailSignature where @ID=ID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.settings',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[UpdateSignture]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSignture]
	-- Add the parameters for the stored procedure here
@userid int,
@signature nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
  update [dbo].[users] set [Signature]=@signature where [userID]=@userid
END



GO
/****** Object:  StoredProcedure [dbo].[updateSysdiagrams]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateSysdiagrams] 
@name nvarchar(128),@principal_id int,@diagram_id int,@version int,@definition varbinary,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.sysdiagrams set dbo.sysdiagrams.name=N''' +@name+ ''',dbo.sysdiagrams.principal_id=' +@principal_id+ ',dbo.sysdiagrams.version=' +@version+ ',dbo.sysdiagrams.definition=' +@definition + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.sysdiagrams',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[updateSysdiagramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateSysdiagramsByPrimaryKey] 
@name nvarchar(128),@principal_id int,@diagram_id int,@version int,@definition varbinary 
as 
begin transaction
Update dbo.sysdiagrams set dbo.sysdiagrams.name=@name,dbo.sysdiagrams.principal_id=@principal_id,dbo.sysdiagrams.version=@version,dbo.sysdiagrams.definition=@definition where @diagram_id=diagram_id; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.sysdiagrams',16,1)
end
Commit




GO
/****** Object:  StoredProcedure [dbo].[updateSysEvents]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateSysEvents] 
@sysEventID int,@userID int,@eventTypeID int,@eventDateTime datetime,@URL nvarchar(100),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.sysEvents set dbo.sysEvents.userID=' +@userID+ ',dbo.sysEvents.eventTypeID=' +@eventTypeID+ ',dbo.sysEvents.eventDateTime=''' +@eventDateTime+ ''',dbo.sysEvents.URL=N''' +@URL+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.sysEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateSysEventsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateSysEventsByPrimaryKey] 
@sysEventID int,@userID int,@eventTypeID int,@eventDateTime datetime,@URL nvarchar(100) 
as 
begin transaction
Update dbo.sysEvents set dbo.sysEvents.userID=@userID,dbo.sysEvents.eventTypeID=@eventTypeID,dbo.sysEvents.eventDateTime=@eventDateTime,dbo.sysEvents.URL=@URL where @sysEventID=sysEventID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.sysEvents',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserDocuments]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserDocuments] 
@userID int,@docID bigint,@allow bit,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.userDocuments set dbo.userDocuments.userID=' +@userID+ ',dbo.userDocuments.docID=' +@docID+ ',dbo.userDocuments.allow=' +@allow+ ',dbo.userDocuments.allowInsert=' +@allowInsert+ ',dbo.userDocuments.allowUpdate=' +@allowUpdate+ ',dbo.userDocuments.allowDelete=' +@allowDelete + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userDocuments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserDocumentsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserDocumentsByPrimaryKey] 
@userID int,@docID bigint,@allow bit,@allowInsert bit,@allowUpdate bit,@allowDelete bit 
as 
begin transaction
Update dbo.userDocuments set dbo.userDocuments.userID=@userID,dbo.userDocuments.docID=@docID,dbo.userDocuments.allow=@allow,dbo.userDocuments.allowInsert=@allowInsert,dbo.userDocuments.allowUpdate=@allowUpdate,dbo.userDocuments.allowDelete=@allowDelete where @docID=docID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userDocuments',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserFolders]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserFolders] 
@userID int,@fldrID int,@allow bit,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@allowCreateFldr bit,@allowRenameFldr bit,@allowRelocationFldr bit,@inheritSubFolders bit,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.userFolders set dbo.userFolders.userID=' +@userID+ ',dbo.userFolders.fldrID=' +@fldrID+ ',dbo.userFolders.allow=' +@allow+ ',dbo.userFolders.allowInsert=' +@allowInsert+ ',dbo.userFolders.allowUpdate=' +@allowUpdate+ ',dbo.userFolders.allowDelete=' +@allowDelete+ ',dbo.userFolders.allowCreateFldr=' +@allowCreateFldr+ ',dbo.userFolders.allowRenameFldr=' +@allowRenameFldr+ ',dbo.userFolders.allowRelocationFldr=' +@allowRelocationFldr+ ',dbo.userFolders.inheritSubFolders=' +@inheritSubFolders + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserFoldersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserFoldersByPrimaryKey] 
@userID int,@fldrID int,@allow bit,@allowInsert bit,@allowUpdate bit,@allowDelete bit,@allowCreateFldr bit,@allowRenameFldr bit,@allowRelocationFldr bit,@inheritSubFolders bit 
as 
begin transaction
Update dbo.userFolders set dbo.userFolders.userID=@userID,dbo.userFolders.fldrID=@fldrID,dbo.userFolders.allow=@allow,dbo.userFolders.allowInsert=@allowInsert,dbo.userFolders.allowUpdate=@allowUpdate,dbo.userFolders.allowDelete=@allowDelete,dbo.userFolders.allowCreateFldr=@allowCreateFldr,dbo.userFolders.allowRenameFldr=@allowRenameFldr,dbo.userFolders.allowRelocationFldr=@allowRelocationFldr,dbo.userFolders.inheritSubFolders=@inheritSubFolders where @fldrID=fldrID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userFolders',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserFormFields]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserFormFields] 
@userID int,@formID int,@fieldSeq int,@value ntext,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.userFormFields set dbo.userFormFields.userID=' +@userID+ ',dbo.userFormFields.formID=' +@formID+ ',dbo.userFormFields.fieldSeq=' +@fieldSeq+ ',dbo.userFormFields.value=N''' +@value+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserFormFieldsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserFormFieldsByPrimaryKey] 
@userID int,@formID int,@fieldSeq int,@value ntext 
as 
begin transaction
Update dbo.userFormFields set dbo.userFormFields.userID=@userID,dbo.userFormFields.formID=@formID,dbo.userFormFields.fieldSeq=@fieldSeq,dbo.userFormFields.value=@value where @fieldSeq=fieldSeq and @formID=formID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userFormFields',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserPrograms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserPrograms] 
@userID int,@programID int,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.userPrograms set dbo.userPrograms.userID=' +@userID+ ',dbo.userPrograms.programID=' +@programID + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUserProgramsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUserProgramsByPrimaryKey] 
@userID int,@programID int 
as 
begin transaction
Update dbo.userPrograms set dbo.userPrograms.userID=@userID,dbo.userPrograms.programID=@programID where @programID=programID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.userPrograms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUsers]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUsers] 
@userID int,@userName varchar(50),@password nvarchar(4000),@fullName nvarchar(500),@grpID int,@active bit,@companyID int,@branchID int,@departmentID int,@positionID int,@email varchar(500),@allowCustomWF bit,@allowCreateFolders bit,@allowReplaceDocuments bit,@allowDiwan bit,@isFirstLogin bit,@passwordCreationDate datetime,@passwordModifiedDate datetime,@lastPassword nvarchar(4000),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.users set dbo.users.userName=''' +@userName+ ''',dbo.users.password=N''' +@password+ ''',dbo.users.fullName=N''' +@fullName+ ''',dbo.users.grpID=' +@grpID+ ',dbo.users.active=' +@active+ ',dbo.users.companyID=' +@companyID+ ',dbo.users.branchID=' +@branchID+ ',dbo.users.departmentID=' +@departmentID+ ',dbo.users.positionID=' +@positionID+ ',dbo.users.email=''' +@email+ ''',dbo.users.allowCustomWF=' +@allowCustomWF+ ',dbo.users.allowCreateFolders=' +@allowCreateFolders+ ',dbo.users.allowReplaceDocuments=' +@allowReplaceDocuments+ ',dbo.users.allowDiwan=' +@allowDiwan+ ',dbo.users.isFirstLogin=' +@isFirstLogin+ ',dbo.users.passwordCreationDate=''' +@passwordCreationDate+ ''',dbo.users.passwordModifiedDate=''' +@passwordModifiedDate+ ''',dbo.users.lastPassword=N''' +@lastPassword+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.users',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUsersByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[updateUsersByPrimaryKey] 
@userID int,@userName varchar(50),@password nvarchar(4000),@fullName nvarchar(500),@grpID int,@active bit,@companyID int,@branchID int,@departmentID int,@positionID int,@email varchar(500),@allowCustomWF bit,@allowCreateFolders bit,@allowReplaceDocuments bit,@allowDiwan bit,@isFirstLogin bit,@passwordCreationDate datetime,@passwordModifiedDate datetime,@lastPassword nvarchar(4000),@Phone nvarchar(50) 
as 
begin transaction
Update dbo.users set dbo.users.userName=@userName,dbo.users.password=@password,dbo.users.fullName=@fullName,dbo.users.grpID=@grpID,dbo.users.active=@active,dbo.users.companyID=@companyID,dbo.users.branchID=@branchID,dbo.users.departmentID=@departmentID,dbo.users.positionID=@positionID,dbo.users.email=@email,dbo.users.allowCustomWF=@allowCustomWF,dbo.users.allowCreateFolders=@allowCreateFolders,dbo.users.allowReplaceDocuments=@allowReplaceDocuments,dbo.users.allowDiwan=@allowDiwan,dbo.users.isFirstLogin=@isFirstLogin,dbo.users.passwordCreationDate=@passwordCreationDate,dbo.users.passwordModifiedDate=@passwordModifiedDate,dbo.users.lastPassword=@lastPassword,Phone=@Phone where @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.users',16,1)
end
Commit
GO
/****** Object:  StoredProcedure [dbo].[updateUsersForms]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUsersForms] 
@userID int,@formID int,@pathID int,@submitDateTime datetime,@status smallint,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.usersForms set dbo.usersForms.userID=' +@userID+ ',dbo.usersForms.formID=' +@formID+ ',dbo.usersForms.pathID=' +@pathID+ ',dbo.usersForms.submitDateTime=''' +@submitDateTime+ ''',dbo.usersForms.status=' +@status + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.usersForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateUsersFormsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateUsersFormsByPrimaryKey] 
@userID int,@formID int,@pathID int,@submitDateTime datetime,@status smallint 
as 
begin transaction
Update dbo.usersForms set dbo.usersForms.userID=@userID,dbo.usersForms.formID=@formID,dbo.usersForms.pathID=@pathID,dbo.usersForms.submitDateTime=@submitDateTime,dbo.usersForms.status=@status where @formID=formID and @userID=userID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.usersForms',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateWfPathDetails]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateWfPathDetails] 
@pathID int,@seqNo smallint,@recipientID int,@endOfPath bit,@recipientType smallint,@companyID int,@branchID int,@approveType smallint,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.wfPathDetails set dbo.wfPathDetails.pathID=' +@pathID+ ',dbo.wfPathDetails.seqNo=' +@seqNo+ ',dbo.wfPathDetails.recipientID=' +@recipientID+ ',dbo.wfPathDetails.endOfPath=' +@endOfPath+ ',dbo.wfPathDetails.recipientType=' +@recipientType+ ',dbo.wfPathDetails.companyID=' +@companyID+ ',dbo.wfPathDetails.branchID=' +@branchID+ ',dbo.wfPathDetails.approveType=' +@approveType + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.wfPathDetails',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateWfPathDetailsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[updateWfPathDetailsByPrimaryKey] 
@pathID int,@seqNo smallint,@recipientID int,@endOfPath bit,@recipientType smallint,@companyID int,@branchID int,@approveType smallint ,
@duration int,@duartionType int
as 
begin transaction
Update dbo.wfPathDetails set dbo.wfPathDetails.pathID=@pathID,dbo.wfPathDetails.seqNo=@seqNo,dbo.wfPathDetails.recipientID=@recipientID,dbo.wfPathDetails.endOfPath=@endOfPath,dbo.wfPathDetails.recipientType=@recipientType,dbo.wfPathDetails.companyID=@companyID,dbo.wfPathDetails.branchID=@branchID,dbo.wfPathDetails.approveType=@approveType,dbo.wfPathDetails.duration=@duration,dbo.wfPathDetails.durationType=@duartionType where @pathID=pathID and @seqNo=seqNo; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.wfPathDetails',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateWorkFlowPaths]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateWorkFlowPaths] 
@pathId int,@pathDesc nvarchar(500),@fldrId int,@docTypId int,@pathDescAr nvarchar(500),@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.workFlowPaths set dbo.workFlowPaths.pathDesc=N''' +@pathDesc+ ''',dbo.workFlowPaths.fldrId=' +@fldrId+ ',dbo.workFlowPaths.docTypId=' +@docTypId+ ',dbo.workFlowPaths.pathDescAr=N''' +@pathDescAr+ '''' + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.workFlowPaths',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [dbo].[updateWorkFlowPathsByPrimaryKey]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[updateWorkFlowPathsByPrimaryKey] 
@pathId int,@pathDesc nvarchar(500),@fldrId int,@docTypId int,@pathDescAr nvarchar(500) 
as 
begin transaction
Update dbo.workFlowPaths set dbo.workFlowPaths.pathDesc=@pathDesc,dbo.workFlowPaths.fldrId=@fldrId,dbo.workFlowPaths.docTypId=@docTypId,dbo.workFlowPaths.pathDescAr=@pathDescAr where @pathId=pathId; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.workFlowPaths',16,1)
end
Commit



GO
/****** Object:  StoredProcedure [imprintadmin].[executeCommand]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [imprintadmin].[executeCommand] 
@cond nvarchar(4000) 
as 
begin 
exec(@cond); 
end



GO
/****** Object:  StoredProcedure [imprintadmin].[updateSubmitDate]    Script Date: 9/17/2021 11:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [imprintadmin].[updateSubmitDate]
	-- Add the parameters for the stored procedure here
	@id int,
@date nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update [dbo].[Documents] set dbo.Documents.submitDate=@date
	where docID=@id
    -- Insert statements for procedure here
	--SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = reject
1 = approve' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'documentWFPath', @level2type=N'COLUMN',@level2name=N'actionType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=user
2=group
3=position
4=department' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'documentWFPath', @level2type=N'COLUMN',@level2name=N'recipientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=user
2=group
3=position
4=department' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wfPathDetails', @level2type=N'COLUMN',@level2name=N'recipientType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = all Must Approve
2 = voting
3 = one approve only' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'wfPathDetails', @level2type=N'COLUMN',@level2name=N'approveType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sysEvents"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "users"
            Begin Extent = 
               Top = 6
               Left = 238
               Bottom = 126
               Right = 442
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "browseingEvents"
            Begin Extent = 
               Top = 6
               Left = 480
               Bottom = 117
               Right = 643
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "programs"
            Begin Extent = 
               Top = 6
               Left = 681
               Bottom = 126
               Right = 853
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'showAllBrowsingEvents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'showAllBrowsingEvents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sysEvents"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 156
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "dataBaseEvents"
            Begin Extent = 
               Top = 6
               Left = 238
               Bottom = 159
               Right = 405
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DBActionsTypes"
            Begin Extent = 
               Top = 6
               Left = 443
               Bottom = 111
               Right = 629
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "users"
            Begin Extent = 
               Top = 6
               Left = 667
               Bottom = 126
               Right = 871
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'showAllDBEvents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'showAllDBEvents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sysEvents"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 147
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "users"
            Begin Extent = 
               Top = 6
               Left = 238
               Bottom = 157
               Right = 442
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "loginEvents"
            Begin Extent = 
               Top = 6
               Left = 480
               Bottom = 111
               Right = 640
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'showAllLoginEvents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'showAllLoginEvents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "documents"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 254
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "metas"
            Begin Extent = 
               Top = 6
               Left = 292
               Bottom = 136
               Right = 462
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "usersRemiders"
            Begin Extent = 
               Top = 6
               Left = 500
               Bottom = 136
               Right = 670
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'userReminders'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'userReminders'
GO
