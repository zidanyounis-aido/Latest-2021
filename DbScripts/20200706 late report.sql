Create PROCEDURE [dbo].[GetAllDocumentLate]
	-- Add the parameters for the stored procedure here
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure here
SELECT documents.docID, documents.docTypID, documents.docName, documents.docExt, documents.addedDate, documents.addedUserID, documents.lastVersion, documents.modifyDate, documents.modifyUserID, documents.fldrID, documents.ocrContent, documents.folderSeq, 
             documents.docTypeSeq, documents.folderDocTypeSeq, documents.wfPathID, documents.wfCurrentSeq, documents.wfCurrentRecipientID, documents.wfCurrentRecipientType, documents.wfStartDateTime, documents.wfTimeFrame, documents.wfStatus, documents.meta1, 
             documents.meta2, documents.meta3, documents.meta4, documents.meta5, documents.meta6, documents.meta7, documents.meta8, documents.meta9, documents.meta10, documents.meta11, documents.meta12, documents.meta13, documents.meta14, documents.meta15, 
             documents.meta16, documents.meta17, documents.meta18, documents.meta19, documents.meta20, documents.meta21, documents.meta22, documents.meta23, documents.meta24, documents.meta25, documents.meta26, documents.meta27, documents.meta28, documents.meta29, 
             documents.meta30, documents.statusId, docTypes.docTypDesc, docTypes.docTypDescAr, users.userName
FROM   documents INNER JOIN
             docTypes ON documents.docTypID = docTypes.docTypID INNER JOIN
             users ON documents.addedUserID = users.userID 
			 where statusId=3
END

-----------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[programs] ON 
GO
INSERT [dbo].[programs] ([programID], [programName], [parentProgramID], [programURL], [windowWidth], [windowHeight], [programNameAr], [iconCss], [orderNum]) VALUES (34, N'Late Document Reports', 0, N'DocumentLateReports', 0, 0, N'الصفحة الجديدة', N'tachometer-alt', 20)

GO
INSERT [dbo].[userPrograms] ([userID], [programID]) VALUES (1, 34)
GO
