Alter table [dbo].[DocTypes]
Add  ClientId int not null Default(0)
Go



ALTER procedure [dbo].[addDocTypes] 
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
Go

