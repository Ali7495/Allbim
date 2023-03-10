SET IDENTITY_INSERT [dbo].[InsuranceTermType] ON 

INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (1, 37, N'هزینه حمل و نقل', 1, N'Transportation', N'Input', 3, 1)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (2, 38, N'سیل و زلزله', 2, N'FloodAndEarthquakeId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (3, 39, N'شکست شیشه', 3, N'GlassBreakingId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (4, 40, N'پاشش اسید', 4, N'AcidAndChemicalId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (5, 41, N'سرقت قطعات درهواست شده', 5, N'StealingRequestedPartsId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (6, 42, N'سرقت همه قطعات', 6, N'StealingAllPartsId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (7, 43, N'حذف فرانشیز', 7, N'FranchiseRemovalId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (8, 44, N'نوسان قیمت', 8, N'MarketFluctuateCoverId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (9, 45, N'تخفیف عدم خسارت', 9, N'NoDamageDiscountId', N'Enum', 2, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (10, 46, N'تخفیف گروهی', 10, N'GroupDiscountId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (11, 47, N'تخفیف نقدی', 11, N'CashDiscountId', N'Model', 1, 2)
INSERT [dbo].[InsuranceTermType] ([Id], [InsuranceFieldId], [TermCaption], [Order], [Field], [RelatedResource], [ResourceTypeId], [PricingTypeId]) VALUES (12, 49, N'تخفیف عدم خسارت بدنه', 12, N'BodyNoDamageDisountId', N'Enum', 2, 2)
SET IDENTITY_INSERT [dbo].[InsuranceTermType] OFF