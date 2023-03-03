INSERT [dbo].[InsuranceStep] ( [InsuranceId], [StepName], [StepOrder]) VALUES ( 6, N'ThirdCumulativeDiscounts', 15)
INSERT [dbo].[InsuranceStep] ( [InsuranceId], [StepName], [StepOrder]) VALUES ( 6, N'ThirdTax', 16)

GO

INSERT [dbo].[InsurerTerm] ( [InsurerId], [Type], [Field], [Criteria], [Value], [Discount], [CalculationType], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [IsCumulative]) VALUES ( 15, 16, N'ThirdTax', N'=', N'9', N'0.09', N'+', NULL, CAST(N'2021-11-15T19:46:44.9633333' AS DateTime2), NULL, CAST(N'2021-11-15T19:46:44.9633333' AS DateTime2), 1)
