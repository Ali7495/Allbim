
BEGIN TRANSACTION;

SET IDENTITY_INSERT [dbo].[PaymentStatus] ON 
INSERT [dbo].[PaymentStatus] ([Id], [Name], [isDeleted]) VALUES (2, N'در حال پرداخت', 0)
INSERT [dbo].[PaymentStatus] ([Id], [Name], [isDeleted]) VALUES (3, N'پرداخت شده', 0)
INSERT [dbo].[PaymentStatus] ([Id], [Name], [isDeleted]) VALUES (4, N'پرداخت ناموفق', 0)
SET IDENTITY_INSERT [dbo].[PaymentStatus] OFF

COMMIT;
