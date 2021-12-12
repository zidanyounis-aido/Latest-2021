Alter table metas
add defaultArTexts  nvarchar(4000)
Go
Alter table metas
add metaIdFK int
Go
Alter table metas
add width float
Go
Alter table metas
add permissionType nvarchar(20)
Go

CREATE TABLE [dbo].[metaGroupsPermissions](
	[metaID] [int] NOT NULL,
	[grpID] [int] NOT NULL,
	[allowRead] [bit] NULL,
	[allowEdit] [bit] NULL,
 CONSTRAINT [PK_metaGroupsPermissions] PRIMARY KEY CLUSTERED 
(
	[metaID] ASC,
	[grpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

Go
Create Proc DuplicateMetaCustomPermission
@metaId int, @newMetaId int
As
insert into metaUsersPermissions
(metaID,userID,allowRead,allowEdit)
(select @newMetaId,m.userID,m.allowRead,m.allowEdit from metaUsersPermissions m where m.metaID = @metaId )

insert into metaGroupsPermissions
(metaID,grpID,allowRead,allowEdit)
(select @newMetaId,m.grpID,m.allowRead,m.allowEdit from metaGroupsPermissions m where m.metaID = @metaId )

Go





ALTER procedure [dbo].[addMetas] 
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


Go


ALTER procedure [dbo].[getAllMetas] 
@cond nvarchar(1000) 
as 
begin 
exec('select metaID,docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width,permissionType from dbo.metas' +  @cond); 
end
go

ALTER procedure [dbo].[getMetasByPrimaryKey] 
@metaID int 
as 
select metaID,docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width,permissionType from dbo.metas where @metaID=metaID; 
Go



ALTER procedure [dbo].[updateMetas] 
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
Go


ALTER procedure [dbo].[updateMetasByPrimaryKey] 
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
Go