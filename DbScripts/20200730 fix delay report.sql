USE [hudhud]
GO
/****** Object:  StoredProcedure [dbo].[GetAllDocumentLate]    Script Date: 8/3/2020 7:26:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetAllDocumentLate]
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



--select top 1 dbo.documentWFPath.userID from dbo.documentWFPath where docID=25 and actionType=0


--select top 1 dbo.documentWFPath.recipientType from dbo.documentWFPath where docID=25 and actionType=0
