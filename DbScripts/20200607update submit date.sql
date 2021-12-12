
--add submit date column
ALTER TABLE  [dbo].[Documents]
ADD submitDate datetime
GO

Create PROCEDURE updateSubmitDate
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



