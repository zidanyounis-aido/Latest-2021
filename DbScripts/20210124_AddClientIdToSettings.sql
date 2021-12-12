Alter table [dbo].[settings]
Add  ClientId int not null Default(0)
Go



ALTER procedure [dbo].[addSettings] 
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
Go
