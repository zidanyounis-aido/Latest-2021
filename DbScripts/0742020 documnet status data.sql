USE [hudhud]
GO
INSERT [dbo].[documentsStatus] ([statusId], [statusName], [statusNameEN], [color]) VALUES (1, N'قيد الإجراء', N'in process
', N'#2ecc71')
INSERT [dbo].[documentsStatus] ([statusId], [statusName], [statusNameEN], [color]) VALUES (2, N'مؤرشف', N'Archived', N'#7f8c8d')
INSERT [dbo].[documentsStatus] ([statusId], [statusName], [statusNameEN], [color]) VALUES (3, N'متأخر', N'Late', N'#e74c3c')
