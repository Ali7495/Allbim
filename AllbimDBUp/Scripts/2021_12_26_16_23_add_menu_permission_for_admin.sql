SET IDENTITY_INSERT menu ON;
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(13, 'admin', N'مدیریت', 'admin', 1, NULL, 172)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(14, 'admin/persons/list', N'فرد', 'admin-person', 2, 13, 172)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(15, 'admin/users/list', N'کاربر', 'admin-user', 3, 13, 61)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(16, 'admin/permission/list', N'نقش (سطح دسترسی)', 'admin-role', 4, 13, 70)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(17, 'admin/insurance/', N'بیمه', 'admin-insurance', 5, 13, 187)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(18, 'admin/insurance/list', N'قوانین کلی', 'admin-all', 6, 17, 187)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(19, 'admin/company/', N'شرکت', 'admin-company', 7, 13, 202)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(20, 'admin/company/agents', N'افراد شرکت', 'admin-agents', 8, 19, 208)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(21, 'admin/company/regulations', N'قوانین بیمه‌گری', 'admin-regulation-insurance', 9, 19, 224)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(22, 'admin/regulation/list', N'قوانین', 'admin-regulation', 10, 13, 172)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(23, 'admin/requests/list', N'درخواست بیمه', 'admin-request', 11, 13, 151)
    
UPDATE dbo.Menu SET [Order] = 100 WHERE Id = 12
SET IDENTITY_INSERT menu OFF;

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 59)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 172)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 61)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 70)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 187)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 202)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 208)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 224)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 151)
