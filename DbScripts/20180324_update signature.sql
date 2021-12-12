USE [HudHudDB]
GO

/****** Object:  Table [dbo].[SignatureTB]    Script Date: 03/26/2018 10:18:51 PM ******/
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
 CONSTRAINT [PK_SignatureTB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
ALTER TABLE [dbo].[users]
Add  [Signature] nvarchar(max);
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateSignture
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
