﻿delete Enumeration where CategoryName = N'PricingType'

INSERT [dbo].[Enumeration] ( [ParentId], [CategoryName], [CategoryCaption], [EnumId], [EnumCaption], [Order], [IsEnable], [Description], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES ( NULL, N'PricingType', N'نوع قیمت گذاری', 1, N'تومان', 1, 1, NULL, NULL, CAST(N'2021-08-29T23:58:03.9200000' AS DateTime2), NULL, CAST(N'2021-08-29T23:58:03.9200000' AS DateTime2))
INSERT [dbo].[Enumeration] ( [ParentId], [CategoryName], [CategoryCaption], [EnumId], [EnumCaption], [Order], [IsEnable], [Description], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt]) VALUES ( NULL, N'PricingType', N'نوع قیمت گذاری', 2, N'درصد', 1, 1, NULL, NULL, CAST(N'2021-08-29T23:58:03.9200000' AS DateTime2), NULL, CAST(N'2021-08-29T23:58:03.9200000' AS DateTime2))