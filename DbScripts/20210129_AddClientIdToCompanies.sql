Alter table [dbo].[Companies]
Add  ClientId int not null Default(0)
Go

ALTER procedure [dbo].[addCompanies] 
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
Go