
BEGIN TRANSACTION;



---------------------------------------- Insert InsuranceField Data --------------------------------------------

    SET IDENTITY_INSERT [dbo].[InsuranceField] ON

    INSERT [dbo].[InsuranceField] ([Id], [InsuranceId], [Key], [Type], [Description], [InsuranceFieldTypeId]) VALUES (171, 6, N'MaxDelayPenaltyDay', N'int', N'بیشترین روز جریمه دیرکرد',2)
    INSERT [dbo].[InsuranceField] ([Id], [InsuranceId], [Key], [Type], [Description], [InsuranceFieldTypeId]) VALUES (172, 6, N'DelayPenalty', N'decimal', N'جریمه دیرکرد',2)
    INSERT [dbo].[InsuranceField] ([Id], [InsuranceId], [Key], [Type], [Description], [InsuranceFieldTypeId]) VALUES (173, 6, N'FinancialDamage', N'int', N'خسارت مالی',2)

    SET IDENTITY_INSERT [dbo].[InsuranceField] OFF


---------------------------------------- Update InsuranceField Table --------------------------------------------

Update [dbo].[InsuranceField]
SET [InsuranceFieldTypeId] = 2
Where [Key] = N'VehicleRuleCategoryId'


---------------------------------------- Insert CentralRuleType Data --------------------------------------------

SET IDENTITY_INSERT [dbo].[CentralRuleType] ON

INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (1, 1, N'نوع خودرو', 1, N'VehicleTypeId', N'vehicle-type', 1, 1)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (2, 2, N'برند خودرو', 1, N'VehicleBrandId', N'vehicle-brand', 1, 1)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (3, 3, N'دسته بندی قانونی خودرو', 1, N'VehicleRuleCategoryId', N'vehicle-rule-category', 1, 1)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (4, 4, N'سال ساخت خودرو', 1, N'VehicleConstructionYear', NULL, 3, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (5, 5, N'خوودرو', 1, N'VehicleId', N'vehicle', 1, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (6, 6, N'کاربری خودرو', 1, N'VehicleApplicationId', N'vehicle', 1, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (7, 7, N'بدون بیمه است؟', 1, N'IsWithoutInsurance', N'IsWithoutInsurance', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (8, 8, N'بیمه گر قبلی', 1, N'OldInsurerId', N'insurance/{slug}/insurer', 1, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (9, 9, N'تاریخ شروع بیمه قبلی', 1, N'OldInsurerStartDate', NULL, 3, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (10, 10, N'تاریخ پایان بیمه قبلی', 1, N'OldInsurerExpireDate', NULL, 3, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (11, 11, N'تغییر مالکیت', 1, N'IsChangedOwner', N'IsWithoutInsurance', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (12, 12, N'تخفیف شخص ثالث', 1, N'ThirdDiscountId', N'ThirdDiscount', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (13, 13, N'تخفیف حوادث راننده', 1, N'DriverDiscountId', N'DriverDiscount', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (14, 14, N'خسارت  جانی ثالث', 1, N'ThirdLifeDamageId', N'ThirdLifeDamage', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (15, 15, N'خسارت مالی ثالث', 1, N'ThirdFinancialDamageId', N'ThirdFinancialDamage', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (16, 16, N'خسارت جانی راننده', 1, N'DriverLifeDamageId', N'DriverLifeDamage', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (17, 17, N'آیا صفر کیلومتر است؟', 1, N'IsZeroKilometer', N'IsZeroKilometer', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (18, 18, N'آیا قبلا خسارت داشته؟', 1, N'IsPrevDamaged', N'IsPrevDamaged', 2, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (19, 19, N'تاریخ ترخیص خودرو', 1, N'VehicleClearanceDate', NULL, 3, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (20, 20, N'بیمه گر', 1, N'InsurerId', N'insurance/{slug}/insurer', 1, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (21, 171, N'بیشترین روز جریمه دیرکرد', 1, N'MaxDelayPenaltyDay', NULL, 3, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (22, 172, N'جریمه دیرکرد', 1, N'DelayPenalty', NULL, 3, 2)
INSERT [dbo].[CentralRuleType] ([Id], [InsuranceFieldId], [RuleCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (23, 173, N'خسارت مالی', 1, N'FinancialDamageId', N'ThirdFinancialDamage', 2, 2)

SET IDENTITY_INSERT [dbo].[CentralRuleType] OFF




---------------------------------------- Update InsuranceCentralRule Data --------------------------------------------

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 3,
    Discount = N'1937500',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 1,
    ConditionTypeId = 1
WHERE Id = 4

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 6,
    Discount = N'10',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 2,
    ConditionTypeId = 1
WHERE Id = 6

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 23,
    Discount = N'20',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 2,
    ConditionTypeId = 1
WHERE Id = 7

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 23,
    Discount = N'30',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 2,
    ConditionTypeId = 1
WHERE Id = 8

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 22,
    Discount = N'5308',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 1,
    ConditionTypeId = 1
WHERE Id = 11

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 3,
    Discount = N'2294300',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 1,
    ConditionTypeId = 1
WHERE Id = 13

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 3,
    Discount = N'2697100',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 1,
    ConditionTypeId = 1
WHERE Id = 14

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 3,
    Discount = N'3018400',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 1,
    ConditionTypeId = 1
WHERE Id = 15

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 6,
    Discount = N'10',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 2,
    ConditionTypeId = 1
WHERE Id = 16

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 6,
    Discount = N'20',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 2,
    ConditionTypeId = 1
WHERE Id = 17

UPDATE InsuranceCentralRule
SET CentralRuleTypeId = 23,
    Discount = N'40',
    Criteria = N'=',
    CalculationTypeId = 2,
    PricingTypeId = 2,
    ConditionTypeId = 1
WHERE Id = 19



    COMMIT;