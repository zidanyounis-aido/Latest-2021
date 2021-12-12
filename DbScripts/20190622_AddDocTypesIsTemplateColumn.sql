USE [HudHud]

ALTER TABLE [dbo].[docTypes]
Add  [isTemplate] bit;
GO
UPDATE [dbo].[docTypes] SET [isTemplate]=0;
