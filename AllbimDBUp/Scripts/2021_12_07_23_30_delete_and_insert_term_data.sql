UPDATE InsuranceTermType
SET RelatedResource = N'PriceFluctuatiuon'
WHERE Id = 54


DELETE InsurerTermDetail;

DELETE InsurerTerm;

SET IDENTITY_INSERT [dbo].[InsurerTerm] ON 

INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (8, 14, 2, N'Transportation', N'=', N'8000', N'8000', N'+', 1, CAST(N'2021-09-17T23:58:21.6900000' AS DateTime2), NULL, CAST(N'2021-09-17T23:58:21.6900000' AS DateTime2), 1, 1, 1, 47)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (10, 14, 4, N'GlassBreaking', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:03:58.3933333' AS DateTime2), NULL, CAST(N'2021-09-18T00:03:58.3933333' AS DateTime2), 1, 2, 1, 49)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (11, 14, 5, N'AcidAndChemicalId', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:04:53.9100000' AS DateTime2), NULL, CAST(N'2021-09-18T00:04:53.9100000' AS DateTime2), 1, 2, 1, 50)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (12, 14, 6, N'StealingRequestedParts', N'=', N'10', N'0.1', N'+', 1, CAST(N'2021-09-18T00:06:48.9300000' AS DateTime2), NULL, CAST(N'2021-09-18T00:06:48.9300000' AS DateTime2), 1, 2, 1, 51)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (13, 14, 7, N'StealingAllParts', N'=', N'20', N'0.2', N'+', 1, CAST(N'2021-09-18T00:08:11.6666667' AS DateTime2), NULL, CAST(N'2021-09-18T00:08:11.6666667' AS DateTime2), 1, 2, 1, 52)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (18, 14, 8, N'FranchiseRemoval', N'=', N'20', N'0.2', N'+', 1, CAST(N'2021-09-18T00:36:50.5533333' AS DateTime2), NULL, CAST(N'2021-09-18T00:36:50.5533333' AS DateTime2), 1, 2, 1, 53)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (19, 14, 9, N'MarketFluctuateCover', N'=', N'25', N'0.15', N'+', 1, CAST(N'2021-09-18T00:37:12.6766667' AS DateTime2), NULL, CAST(N'2021-09-18T00:37:12.6766667' AS DateTime2), 1, 2, 1, 54)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (20, 14, 9, N'MarketFluctuateCover', N'=', N'50', N'0.25', N'+', 1, CAST(N'2021-09-18T00:37:25.1900000' AS DateTime2), NULL, CAST(N'2021-09-18T00:37:25.1900000' AS DateTime2), 1, 2, 1, 54)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (22, 14, 9, N'MarketFluctuateCover', N'=', N'100', N'0.4', N'+', 1, CAST(N'2021-09-18T00:41:11.4866667' AS DateTime2), NULL, CAST(N'2021-09-18T00:41:11.4866667' AS DateTime2), 1, 2, 1, 54)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (23, 14, 3, N'FloodAndEarthquakeId', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:42:21.2533333' AS DateTime2), NULL, CAST(N'2021-09-18T00:42:21.2533333' AS DateTime2), 1, 2, 1, 48)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (24, 14, 1, N'BodyBaseCost', N'>=', N'1000000000', N'0.01', N'+', 1, CAST(N'2021-09-18T00:44:29.4633333' AS DateTime2), NULL, CAST(N'2021-09-18T00:44:29.4633333' AS DateTime2), 1, 2, 4, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (41, 4, 2, N'value', N'=', N'444', N'444', N'+', NULL, CAST(N'2021-10-09T15:33:07.6133333' AS DateTime2), NULL, CAST(N'2021-10-09T15:33:07.6133333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (42, 14, 1, N'BodyBaseCost', N'<', N'1000000000', N'0.022', N'+', NULL, CAST(N'2021-10-11T23:47:52.2333333' AS DateTime2), NULL, CAST(N'2021-10-11T23:47:52.2333333' AS DateTime2), 1, 1, 3, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (43, 14, 10, N'NoDamageDiscount', N'=', N'1', N'0.25', N'-', NULL, CAST(N'2021-10-11T23:49:02.0533333' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:02.0533333' AS DateTime2), 1, 2, 1, 55)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (44, 14, 10, N'NoDamageDiscount', N'=', N'2', N'0.35', N'-', NULL, CAST(N'2021-10-11T23:49:18.1266667' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:18.1266667' AS DateTime2), 1, 2, 1, 55)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (45, 14, 10, N'NoDamageDiscount', N'=', N'3', N'0.45', N'-', NULL, CAST(N'2021-10-11T23:49:29.0466667' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:29.0466667' AS DateTime2), 1, 2, 1, 55)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (46, 14, 10, N'NoDamageDiscount', N'=', N'4', N'0.6', N'-', NULL, CAST(N'2021-10-11T23:49:42.8400000' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:42.8400000' AS DateTime2), 1, 2, 1, 55)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (47, 14, 13, N'CashDiscount', N'=', N'15', N'0.15', N'-', NULL, CAST(N'2021-10-11T23:50:33.8100000' AS DateTime2), NULL, CAST(N'2021-10-11T23:50:33.8100000' AS DateTime2), 1, 2, 1, 57)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (48, 14, 12, N'GroupDiscount', N'=', N'50', N'0.5', N'-', NULL, CAST(N'2021-10-11T23:51:56.7866667' AS DateTime2), NULL, CAST(N'2021-10-11T23:51:56.7866667' AS DateTime2), 1, 2, 1, 56)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (49, 14, 14, N'Tax', N'=', N'9', N'0.09', N'+', NULL, CAST(N'2021-10-11T23:52:35.1200000' AS DateTime2), NULL, CAST(N'2021-10-11T23:52:35.1200000' AS DateTime2), 1, 2, 1, 59)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (50, 15, 14, N'MaxFinancialCover', N'=', N'1', N'0', N'+', NULL, CAST(N'2021-10-17T23:29:35.8233333' AS DateTime2), NULL, CAST(N'2021-10-17T23:29:35.8233333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (51, 15, 14, N'MaxFinancialCover', N'=', N'2', N'0.2', N'+', NULL, CAST(N'2021-10-17T23:29:52.4333333' AS DateTime2), NULL, CAST(N'2021-10-17T23:29:52.4333333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (52, 15, 14, N'MaxFinancialCover', N'=', N'3', N'0.3', N'+', NULL, CAST(N'2021-10-17T23:30:25.9100000' AS DateTime2), NULL, CAST(N'2021-10-17T23:30:25.9100000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (53, 15, 14, N'MaxFinancialCover', N'=', N'4', N'0.8', N'+', NULL, CAST(N'2021-10-17T23:30:38.4266667' AS DateTime2), NULL, CAST(N'2021-10-17T23:30:38.4266667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (54, 15, 15, N'CreditDuration', N'=', N'1', N'0.2', N'-', NULL, CAST(N'2021-10-17T23:31:24.1800000' AS DateTime2), NULL, CAST(N'2021-10-17T23:31:24.1800000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (55, 15, 15, N'CreditDuration', N'=', N'2', N'0.3', N'-', NULL, CAST(N'2021-10-17T23:31:40.8433333' AS DateTime2), NULL, CAST(N'2021-10-17T23:31:40.8433333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (56, 15, 15, N'CreditDuration', N'=', N'3', N'0.5', N'-', NULL, CAST(N'2021-10-17T23:31:54.3533333' AS DateTime2), NULL, CAST(N'2021-10-17T23:31:54.3533333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (57, 15, 15, N'CreditDuration', N'=', N'4', N'0', N'+', NULL, CAST(N'2021-10-17T23:32:22.1700000' AS DateTime2), NULL, CAST(N'2021-10-17T23:32:22.1700000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (58, 4, 14, N'MaxFinancialCover', N'=', N'1', N'0', N'+', NULL, CAST(N'2021-10-19T19:47:07.7233333' AS DateTime2), NULL, CAST(N'2021-10-19T19:47:07.7233333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (59, 5, 14, N'MaxFinancialCover', N'=', N'1', N'0', N'+', NULL, CAST(N'2021-10-19T19:47:11.2900000' AS DateTime2), NULL, CAST(N'2021-10-19T19:47:11.2900000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (60, 7, 14, N'MaxFinancialCover', N'=', N'1', N'0', N'+', NULL, CAST(N'2021-10-19T19:47:13.8500000' AS DateTime2), NULL, CAST(N'2021-10-19T19:47:13.8500000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (61, 4, 15, N'CreditDuration', N'=', N'4', N'0', N'+', NULL, CAST(N'2021-10-19T19:47:36.9666667' AS DateTime2), NULL, CAST(N'2021-10-19T19:47:36.9666667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (62, 5, 15, N'CreditDuration', N'=', N'4', N'0', N'+', NULL, CAST(N'2021-10-19T19:47:39.3000000' AS DateTime2), NULL, CAST(N'2021-10-19T19:47:39.3000000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (63, 7, 15, N'CreditDuration', N'=', N'4', N'0', N'+', NULL, CAST(N'2021-10-19T19:47:40.9900000' AS DateTime2), NULL, CAST(N'2021-10-19T19:47:40.9900000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (64, 14, 11, N'MultipleCondition', N'=', N'1', N'0', N'-', NULL, CAST(N'2021-11-01T22:46:19.8600000' AS DateTime2), NULL, CAST(N'2021-11-01T22:46:19.8600000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (65, 6, 2, N'Transportation', N'=', N'8000', N'8000', N'+', 1, CAST(N'2021-09-17T23:58:21.6900000' AS DateTime2), NULL, CAST(N'2021-09-17T23:58:21.6900000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (66, 6, 4, N'GlassBreaking', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:03:58.3933333' AS DateTime2), NULL, CAST(N'2021-09-18T00:03:58.3933333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (67, 6, 5, N'AcidicSpray', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:04:53.9100000' AS DateTime2), NULL, CAST(N'2021-09-18T00:04:53.9100000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (68, 6, 6, N'StealingRequestedParts', N'=', N'10', N'0.1', N'+', 1, CAST(N'2021-09-18T00:06:48.9300000' AS DateTime2), NULL, CAST(N'2021-09-18T00:06:48.9300000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (69, 6, 7, N'StealingAllParts', N'=', N'20', N'0.2', N'+', 1, CAST(N'2021-09-18T00:08:11.6666667' AS DateTime2), NULL, CAST(N'2021-09-18T00:08:11.6666667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (70, 6, 8, N'FranchiseRemoval', N'=', N'20', N'0.2', N'+', 1, CAST(N'2021-09-18T00:36:50.5533333' AS DateTime2), NULL, CAST(N'2021-09-18T00:36:50.5533333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (71, 6, 9, N'MarketFluctuateCover', N'=', N'25', N'0.15', N'+', 1, CAST(N'2021-09-18T00:37:12.6766667' AS DateTime2), NULL, CAST(N'2021-09-18T00:37:12.6766667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (72, 6, 9, N'MarketFluctuateCover', N'=', N'50', N'0.25', N'+', 1, CAST(N'2021-09-18T00:37:25.1900000' AS DateTime2), NULL, CAST(N'2021-09-18T00:37:25.1900000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (73, 6, 9, N'MarketFluctuateCover', N'=', N'100', N'0.4', N'+', 1, CAST(N'2021-09-18T00:41:11.4866667' AS DateTime2), NULL, CAST(N'2021-09-18T00:41:11.4866667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (74, 6, 3, N'NaturalDisaster', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:42:21.2533333' AS DateTime2), NULL, CAST(N'2021-09-18T00:42:21.2533333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (75, 6, 1, N'BodyBaseCost', N'>=', N'1000000000', N'0.01', N'+', 1, CAST(N'2021-09-18T00:44:29.4633333' AS DateTime2), NULL, CAST(N'2021-09-18T00:44:29.4633333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (76, 6, 1, N'BodyBaseCost', N'<', N'1000000000', N'0.022', N'+', NULL, CAST(N'2021-10-11T23:47:52.2333333' AS DateTime2), NULL, CAST(N'2021-10-11T23:47:52.2333333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (77, 6, 10, N'NoDamageDiscount', N'=', N'1', N'0.25', N'-', NULL, CAST(N'2021-10-11T23:49:02.0533333' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:02.0533333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (78, 6, 10, N'NoDamageDiscount', N'=', N'2', N'0.35', N'-', NULL, CAST(N'2021-10-11T23:49:18.1266667' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:18.1266667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (79, 6, 10, N'NoDamageDiscount', N'=', N'3', N'0.45', N'-', NULL, CAST(N'2021-10-11T23:49:29.0466667' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:29.0466667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (80, 6, 10, N'NoDamageDiscount', N'=', N'4', N'0.6', N'-', NULL, CAST(N'2021-10-11T23:49:42.8400000' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:42.8400000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (81, 6, 13, N'CashDiscount', N'=', N'15', N'0.15', N'-', NULL, CAST(N'2021-10-11T23:50:33.8100000' AS DateTime2), NULL, CAST(N'2021-10-11T23:50:33.8100000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (82, 6, 12, N'GroupDiscount', N'=', N'50', N'0.5', N'-', NULL, CAST(N'2021-10-11T23:51:56.7866667' AS DateTime2), NULL, CAST(N'2021-10-11T23:51:56.7866667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (83, 6, 14, N'Tax', N'=', N'9', N'0.09', N'+', NULL, CAST(N'2021-10-11T23:52:35.1200000' AS DateTime2), NULL, CAST(N'2021-10-11T23:52:35.1200000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (84, 6, 11, N'MultipleCondition', N'=', N'1', N'0', N'-', NULL, CAST(N'2021-11-01T22:46:19.8600000' AS DateTime2), NULL, CAST(N'2021-11-01T22:46:19.8600000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (85, 8, 2, N'Transportation', N'=', N'8000', N'8000', N'+', 1, CAST(N'2021-09-17T23:58:21.6900000' AS DateTime2), NULL, CAST(N'2021-09-17T23:58:21.6900000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (86, 8, 4, N'GlassBreaking', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:03:58.3933333' AS DateTime2), NULL, CAST(N'2021-09-18T00:03:58.3933333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (87, 8, 5, N'AcidicSpray', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:04:53.9100000' AS DateTime2), NULL, CAST(N'2021-09-18T00:04:53.9100000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (88, 8, 6, N'StealingRequestedParts', N'=', N'10', N'0.1', N'+', 1, CAST(N'2021-09-18T00:06:48.9300000' AS DateTime2), NULL, CAST(N'2021-09-18T00:06:48.9300000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (89, 8, 7, N'StealingAllParts', N'=', N'20', N'0.2', N'+', 1, CAST(N'2021-09-18T00:08:11.6666667' AS DateTime2), NULL, CAST(N'2021-09-18T00:08:11.6666667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (90, 8, 8, N'FranchiseRemoval', N'=', N'20', N'0.2', N'+', 1, CAST(N'2021-09-18T00:36:50.5533333' AS DateTime2), NULL, CAST(N'2021-09-18T00:36:50.5533333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (91, 8, 9, N'MarketFluctuateCover', N'=', N'25', N'0.15', N'+', 1, CAST(N'2021-09-18T00:37:12.6766667' AS DateTime2), NULL, CAST(N'2021-09-18T00:37:12.6766667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (92, 8, 9, N'MarketFluctuateCover', N'=', N'50', N'0.25', N'+', 1, CAST(N'2021-09-18T00:37:25.1900000' AS DateTime2), NULL, CAST(N'2021-09-18T00:37:25.1900000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (93, 8, 9, N'MarketFluctuateCover', N'=', N'100', N'0.4', N'+', 1, CAST(N'2021-09-18T00:41:11.4866667' AS DateTime2), NULL, CAST(N'2021-09-18T00:41:11.4866667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (94, 8, 3, N'NaturalDisaster', N'=', N'5', N'0.05', N'+', 1, CAST(N'2021-09-18T00:42:21.2533333' AS DateTime2), NULL, CAST(N'2021-09-18T00:42:21.2533333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (95, 8, 1, N'BodyBaseCost', N'>=', N'1000000000', N'0.01', N'+', 1, CAST(N'2021-09-18T00:44:29.4633333' AS DateTime2), NULL, CAST(N'2021-09-18T00:44:29.4633333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (96, 8, 1, N'BodyBaseCost', N'<', N'1000000000', N'0.022', N'+', NULL, CAST(N'2021-10-11T23:47:52.2333333' AS DateTime2), NULL, CAST(N'2021-10-11T23:47:52.2333333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (97, 8, 10, N'NoDamageDiscount', N'=', N'1', N'0.25', N'-', NULL, CAST(N'2021-10-11T23:49:02.0533333' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:02.0533333' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (98, 8, 10, N'NoDamageDiscount', N'=', N'2', N'0.35', N'-', NULL, CAST(N'2021-10-11T23:49:18.1266667' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:18.1266667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (99, 8, 10, N'NoDamageDiscount', N'=', N'3', N'0.45', N'-', NULL, CAST(N'2021-10-11T23:49:29.0466667' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:29.0466667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (100, 8, 10, N'NoDamageDiscount', N'=', N'4', N'0.6', N'-', NULL, CAST(N'2021-10-11T23:49:42.8400000' AS DateTime2), NULL, CAST(N'2021-10-11T23:49:42.8400000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (101, 8, 13, N'CashDiscount', N'=', N'15', N'0.15', N'-', NULL, CAST(N'2021-10-11T23:50:33.8100000' AS DateTime2), NULL, CAST(N'2021-10-11T23:50:33.8100000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (102, 8, 12, N'GroupDiscount', N'=', N'50', N'0.5', N'-', NULL, CAST(N'2021-10-11T23:51:56.7866667' AS DateTime2), NULL, CAST(N'2021-10-11T23:51:56.7866667' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (103, 8, 14, N'Tax', N'=', N'9', N'0.09', N'+', NULL, CAST(N'2021-10-11T23:52:35.1200000' AS DateTime2), NULL, CAST(N'2021-10-11T23:52:35.1200000' AS DateTime2), 1, NULL, NULL, NULL)
INSERT [dbo].[InsurerTerm] ([Id], [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative], [PricingTypeId], [ConditionTypeId], [InsuranceTermTypeId]) VALUES (104, 8, 11, N'MultipleCondition', N'=', N'1', N'0', N'-', NULL, CAST(N'2021-11-01T22:46:19.8600000' AS DateTime2), NULL, CAST(N'2021-11-01T22:46:19.8600000' AS DateTime2), 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[InsurerTerm] OFF


SET IDENTITY_INSERT [dbo].[InsurerTermDetail] ON 

INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (1, 64, 1, NULL, N'BodyDiscount', N'=', N'1', N'0.1', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (2, 64, 2, 1, N'ThirdDiscount', N'=', N'2', N'0.1', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (3, 64, 3, 1, N'LifeDiscount', N'=', N'2', N'0.1', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (4, 64, 1, NULL, N'BodyDiscount', N'=', N'1', N'0.15', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (5, 64, 2, 4, N'ThirdDiscount', N'=', N'1', N'0.15', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (6, 64, 1, NULL, N'BodyDiscount', N'=', N'2', N'0.2', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (7, 64, 2, 6, N'LifeDiscount', N'=', N'1', N'0.2', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (8, 64, 1, NULL, N'BodyNoDamageDisountId', N'=', N'2', N'0.3', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (9, 64, 2, 8, N'ThirdNoDamageDisountId', N'=', N'2', N'0.3', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (10, 64, 1, NULL, N'BodyNoDamageDisountId', N'=', N'2', N'0.25', N'-', 1, NULL)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative], [PricingTypeId]) VALUES (11, 64, 2, 10, N'ThirdNoDamageDisountId', N'=', N'3', N'0.25', N'-', 1, NULL)
SET IDENTITY_INSERT [dbo].[InsurerTermDetail] OFF