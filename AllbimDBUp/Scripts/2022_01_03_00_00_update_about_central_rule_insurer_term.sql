
BEGIN TRANSACTION;

DELETE InsurerTerm WHERE Field = N'value'

------------------ Insert InsuranceField -----------------

SET IDENTITY_INSERT [dbo].[InsuranceField] ON 
INSERT [InsuranceField] (Id,InsuranceId,[Key],[Type],[Description],InsuranceFieldTypeId) VALUES(174,11,N'BodyBaseCost',N'decimal',N'نرخ پایه بدنه',2)
INSERT [InsuranceField] (Id,InsuranceId,[Key],[Type],[Description],InsuranceFieldTypeId) VALUES(175,6,N'MaxFinancialCover',N'decimal',N'بیشترین پوشش مالی',2)
INSERT [InsuranceField] (Id,InsuranceId,[Key],[Type],[Description],InsuranceFieldTypeId) VALUES(176,6,N'CreditDuration',N'decimal',N'مدت اعتبار بیمه نامه',2)
SET IDENTITY_INSERT [dbo].[InsuranceField] OFF


------------------ Insert InsuranceTermType -----------------

SET IDENTITY_INSERT [dbo].[InsuranceTermType] ON 
INSERT [InsuranceTermType] (Id,InsuranceFieldId,TermCaption,[Order],Field,RelatedResource,ResourceTypeId,PricingTypeId) VALUES(60,174,N'نرخ پایه بدنه',1,N'BodyBaseCost',NULL,3,2)
INSERT [InsuranceTermType] (Id,InsuranceFieldId,TermCaption,[Order],Field,RelatedResource,ResourceTypeId,PricingTypeId) VALUES(61,175,N'بیشترین پوشش مالی',1,N'MaxFinancialCover',NULL,3,1)
INSERT [InsuranceTermType] (Id,InsuranceFieldId,TermCaption,[Order],Field,RelatedResource,ResourceTypeId,PricingTypeId) VALUES(62,176,N'مدت اعتبار بیمه نامه',1,N'CreditDuration',NULL,3,1)
SET IDENTITY_INSERT [dbo].[InsuranceTermType] OFF



UPDATE Enumeration
SET CategoryName = N'MarketFluctuatiuon'
WHERE CategoryName = N'PriceFluctuatiuon';

UPDATE InsuranceField
SET Description = N'ایاب و ذهاب'
WHERE [Key] = N'Transportation';


UPDATE InsuranceTermType
SET TermCaption = N'ایاب و ذهاب'
WHERE Field =  N'Transportation';


ALTER TABLE InsurerTerm
    ADD CalculationTypeId TINYINT DEFAULT (1);



COMMIT;