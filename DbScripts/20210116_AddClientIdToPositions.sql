Alter table [positions]	
Add  ClientId int not null Default(0)
Go



ALTER procedure [dbo].[addPositions] 
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
Go

