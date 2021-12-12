

--drop old primary keu
ALTER TABLE [dbo].[wfPathDetails] -- Table Name
DROP CONSTRAINT PK_wfPathDetails


--add id 
ALTER TABLE  [dbo].[wfPathDetails]
ADD id INT IDENTITY(1,1)
GO

-- add new primary key constraint on new column   
ALTER TABLE [dbo].[wfPathDetails]
ADD CONSTRAINT PK_wfPathDetails
PRIMARY KEY CLUSTERED (id)