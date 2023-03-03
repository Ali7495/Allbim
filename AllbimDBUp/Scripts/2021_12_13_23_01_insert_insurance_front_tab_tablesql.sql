SET IDENTITY_INSERT InsuranceFrontTab on
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(1, 6, 'receiptThird', 'اطلاعات تکميلي', CONVERT(bit, 'False'))
GO
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(2, 6, 'receiver', 'نحوه ارسال', CONVERT(bit, 'False'))
GO
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(3, 6, 'agent', 'انتخاب نماينده', CONVERT(bit, 'False'))
GO
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(4, 6, 'payment', 'اطلاعات پرداخت', CONVERT(bit, 'False'))
GO
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(5, 11, 'receiptBody', 'اطلاعات تکميلي', CONVERT(bit, 'False'))
GO
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(6, 11, 'visitBody', 'بازديد فني', CONVERT(bit, 'False'))
GO
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(7, 11, 'receiver', 'نحوه ارسال', CONVERT(bit, 'False'))
GO
INSERT INTO dbo.InsuranceFrontTab(Id, InsuranceId, Name, Title, IsDeleted) VALUES(8, 11, 'bodyPayment', 'اطلاعات پرداخت', CONVERT(bit, 'False'))
GO
  SET IDENTITY_INSERT InsuranceFrontTab off