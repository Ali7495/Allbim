
DELETE Menu;
SET IDENTITY_INSERT Menu ON;
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(1, 'insurer', N'شرکت بیمه', 'insurer', 1, NULL, 214)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(2, 'insurer/agents/insurance', N'اشخاص', 'agent', 2, 1, 206)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(3, 'insurer/users/list', N'کاربران', 'user', 3, 1, 206)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(4, 'insurer/files', N'پرونده های بیمه', 'files', 4, 1, 149)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(5, 'insurer/files/requests/finished', N'بیمه های صادر شده', 'finished', 5, 4, 149)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(6, 'insurer/files/requests/unfinished', N'در حال بررسی', 'unfinished', 6, 4, 149)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(7, 'insurer/files/requests/all', N'تمامی درخواست ها', 'all', 7, 4, 149)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(8, 'insurer/files/requests/new', N'درخواست های جدید', 'new', 8, 4, 149)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(9, 'insurer/centers/list', N'مراکز', 'center', 9, 1, 197)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(10, 'insurer/regulations/list', N'قوانین بیمه', 'regulation', 10, 1, 226)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(11, 'insurer/info/list', N'اطلاعات شرکت', 'info', 11, 1, 215)
    
INSERT INTO dbo.Menu(Id, Title, Name, Icon, [Order], ParentId, PermissionId) VALUES(12, 'profile', N'ویرایش پروفایل', 'profile', 12, NULL, 59)
    
SET IDENTITY_INSERT Menu OFF;


INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 214)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 206)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 149)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 197)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 226)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 215)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 59)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(2, 214)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(2, 149)
    
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(2, 59)
