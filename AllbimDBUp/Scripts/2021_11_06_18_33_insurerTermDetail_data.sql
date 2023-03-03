
SET IDENTITY_INSERT [dbo].[InsurerTermDetail] ON 

INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative]) VALUES (1, 64, 1, NULL, N'BodyDiscount', N'=', N'1', N'0.1', N'-', 0)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative]) VALUES (2, 64, 2, 1, N'ThirdDiscount', N'=', N'2', N'0.1', N'-', 0)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative]) VALUES (3, 64, 3, 1, N'LifeDiscount', N'=', N'2', N'0.1', N'-', 0)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative]) VALUES (4, 64, 1, NULL, N'BodyDiscount', N'=', N'1', N'0.15', N'-', 0)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative]) VALUES (5, 64, 2, 4, N'ThirdDiscount', N'=', N'1', N'0.15', N'-', 0)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative]) VALUES (6, 64, 1, NULL, N'BodyDiscount', N'=', N'2', N'0.2', N'-', 0)
INSERT [dbo].[InsurerTermDetail] ([Id], [InsurerTermId], [Order], [ParentId], [Field], [Criteria], [Value], [Discount], [CalculationType], [IsCumulative]) VALUES (7, 64, 2, 6, N'LifeDiscount', N'=', N'1', N'0.2', N'-', 0)
SET IDENTITY_INSERT [dbo].[InsurerTermDetail] OFF

