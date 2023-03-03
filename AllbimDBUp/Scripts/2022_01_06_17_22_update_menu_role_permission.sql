
BEGIN TRANSACTION;

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 206)
    GO
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 206)
    GO
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 149)
    GO
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 197)
    GO
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 59)
    GO
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(2, 206)





UPDATE dbo.Menu SET Title = 'admin', Name = N'مدیریت', Icon = 'admin', [Order] = 1, ParentId = NULL, PermissionId = 172 WHERE Id = 13
    
UPDATE dbo.Menu SET Title = 'admin/persons/list', Name = N'فرد', Icon = 'admin-person', [Order] = 2, ParentId = 13, PermissionId = 172 WHERE Id = 14
    
UPDATE dbo.Menu SET Title = 'admin/users/list', Name = N'کاربر', Icon = 'admin-user', [Order] = 3, ParentId = 13, PermissionId = 61 WHERE Id = 15
    
UPDATE dbo.Menu SET Title = 'admin/permission/list', Name = N'نقش (سطح دسترسی)', Icon = 'admin-role', [Order] = 4, ParentId = 13, PermissionId = 70 WHERE Id = 16
    
UPDATE dbo.Menu SET Title = 'admin/insurance/', Name = N'بیمه', Icon = 'admin-insurance', [Order] = 5, ParentId = 13, PermissionId = 187 WHERE Id = 17
    
UPDATE dbo.Menu SET Title = 'admin/insurance/list', Name = N'قوانین کلی', Icon = 'admin-all', [Order] = 6, ParentId = 17, PermissionId = 185 WHERE Id = 18
    
UPDATE dbo.Menu SET Title = 'admin/company/', Name = N'شرکت', Icon = 'admin-company', [Order] = 11, ParentId = 13, PermissionId = 202 WHERE Id = 19
    
UPDATE dbo.Menu SET Title = 'admin/company/agents', Name = N'افراد شرکت', Icon = 'admin-agents', [Order] = 12, ParentId = 19, PermissionId = 208 WHERE Id = 20
    
UPDATE dbo.Menu SET Title = 'admin/company/regulations', Name = N'قوانین بیمه‌گری', Icon = 'admin-regulation-insurance', [Order] = 13, ParentId = 19, PermissionId = 224 WHERE Id = 21
    
UPDATE dbo.Menu SET Title = 'admin/regulation/list', Name = N'قوانین', Icon = 'admin-regulation', [Order] = 14, ParentId = 13, PermissionId = 172 WHERE Id = 22
    






UPDATE dbo.Menu SET PermissionId = 185 WHERE Title = 'admin/insurance/list' ;

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 185);

DELETE FROM dbo.Menu where Title = 'admin/requests/list' ;

update dbo.Menu SET Name='شرکت بیمه' WHERE Id = 20 ;



SET IDENTITY_INSERT Menu ON;

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(24, 'admin/insurance/list', N'انواع بیمه', 'admin-all', 5, 17, 188)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(25, 'admin/files/requests', N'درخواست بیمه', 'admin-request', 6, 13, 152)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(26, 'admin/files/requests/finished', N'بیمه های صادر شده', 'finished', 7, 25, 152)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(27, 'admin/files/requests/unfinished', N'در حال بررسی', 'unfinished', 8, 25, 152)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(28, 'admin/files/requests/all', N'تمامی درخواست ها', 'all', 9, 25, 152)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(29, 'admin/files/requests/new', N'درخواست های جدید', 'new', 10, 25, 152)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(30, 'page', N'صفحات سایت', 'page', 15, 13, 56)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(31, 'blog', N'مقالات', 'blog', 16, 13, 56)

INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(32, 'contact', N'تماس با ما', 'contact', 17, 13, 29)




SET IDENTITY_INSERT Menu OFF;


INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 185)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 152)
 
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 56)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 29)


COMMIT;