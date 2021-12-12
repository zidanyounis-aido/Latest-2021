Alter table Metas
Add	[metaDescAr] [nvarchar](500) NULL;
Go

Alter table Metas
Add	[defaultArTexts] [nvarchar](4000) NULL;
Go

Alter table Metas
Add	[columnSeq] [int] NULL;
Go

Alter table Metas
Add	[metaIdFK] [int] NULL;
Go

Alter table Metas
Add	[width] [float] NULL;
Go



ALTER procedure [dbo].[addMetas] 
@docTypID int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit,@metaDescAr nvarchar(500),@defaultArTexts  nvarchar(4000),@columnSeq int,@metaIdFK int,@width float
as 
begin Transaction
Insert Into dbo.metas(docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width) values(@docTypID,@metaDesc,@metaDataType,@required,@orderSeq,@ctrlID,@defaultTexts,@defaultValues,@visible,@metaDescAr,@defaultArTexts,@columnSeq,@metaIdFK,@width);
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
exec('select metaID,docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width from dbo.metas' +  @cond); 
end
Go

ALTER procedure [dbo].[getMetasByPrimaryKey] 
@metaID int 
as 
select metaID,docTypID,metaDesc,metaDataType,required,orderSeq,ctrlID,defaultTexts,defaultValues,visible,metaDescAr,defaultArTexts,columnSeq,metaIdFK,width from dbo.metas where @metaID=metaID; 
Go


ALTER procedure [dbo].[updateMetas] 
@metaID int,@docTypID int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit,@metaDescAr nvarchar(500),@defaultArTexts  nvarchar(4000),@columnSeq int,@metaIdFK int,@width float,@condition nvarchar(500) 
as 
begin transaction
exec('Update dbo.metas set dbo.metas.docTypID=' +@docTypID+ ',dbo.metas.metaDesc=N''' +@metaDesc+ ''',dbo.metas.metaDataType=''' +@metaDataType+ ''',dbo.metas.required=' +@required+ ',dbo.metas.orderSeq=' +@orderSeq+ ',dbo.metas.ctrlID=' +@ctrlID+ ',dbo.metas.defaultTexts=N''' +@defaultTexts+ ''',dbo.metas.defaultValues=N''' +@defaultValues+ ''',dbo.metas.visible=' +@visible+ ',dbo.metas.metaDescAr=N''' +@metaDescAr+ ''',dbo.metas.defaultArTexts=N''' +@defaultArTexts+''' ,dbo.metas.columnSeq=' +@columnSeq +' ,dbo.metas.metaIdFK=' + @metaIdFK +' ,dbo.metas.width=' + @width + @condition); 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.metas',16,1)
end
Commit
Go

ALTER procedure [dbo].[updateMetasByPrimaryKey] 
@metaID int,@docTypID int,@metaDesc nvarchar(500),@metaDataType varchar(50),@required bit,@orderSeq int,@ctrlID int,@defaultTexts nvarchar(4000),@defaultValues nvarchar(4000),@visible bit,@metaDescAr nvarchar(500) ,@defaultArTexts  nvarchar(4000),@columnSeq int,@metaIdFK int,@width float
as 
begin transaction
Update dbo.metas set dbo.metas.docTypID=@docTypID,dbo.metas.metaDesc=@metaDesc,dbo.metas.metaDataType=@metaDataType,dbo.metas.required=@required,dbo.metas.orderSeq=@orderSeq,dbo.metas.ctrlID=@ctrlID,dbo.metas.defaultTexts=@defaultTexts,dbo.metas.defaultValues=@defaultValues,dbo.metas.visible=@visible,dbo.metas.metaDescAr=@metaDescAr,dbo.metas.defaultArTexts = @defaultArTexts,dbo.metas.columnSeq = @columnSeq,dbo.metas.metaIdFK = @metaIdFK,dbo.metas.width = @width where @metaID=metaID; 
if @@ERROR<>0
begin
rollback
RAISERROR ('Error in Updating on dbo.metas',16,1)
end
Commit
Go

INSERT [dbo].[controlsTypes] ([crtlID], [ctrlDesc], [ctrlDescAr]) VALUES (6, N'Table', N'جدول')
GO
INSERT [dbo].[controlsTypes] ([crtlID], [ctrlDesc], [ctrlDescAr]) VALUES (7, N'Map', N'خريطة')
GO
INSERT [dbo].[controlsTypes] ([crtlID], [ctrlDesc], [ctrlDescAr]) VALUES (8, N'Image', N'صورة')
GO
INSERT [dbo].[controlsTypes] ([crtlID], [ctrlDesc], [ctrlDescAr]) VALUES (9, N'Link', N'رابط')
GO

INSERT [dbo].[programs] ([programName], [parentProgramID], [programURL], [windowWidth], [windowHeight], [programNameAr], [iconCss], [orderNum])
VALUES ( N'newForms', 2, N'../Admin/newManageDocTypes', 1200, 400, N'النماذج الجديدة', N'list-alt', 38)
GO