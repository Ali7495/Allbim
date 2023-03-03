
BEGIN TRANSACTION;

SET IDENTITY_INSERT Permission ON;
INSERT INTO dbo.Permission(Id, Name) VALUES(228, N'admin-PolicyRequestComission')
    INSERT INTO dbo.Permission(Id, Name) VALUES(229, N'edit-PolicyRequestComission')

INSERT INTO dbo.Permission(Id, Name) VALUES(230, N'view-PolicyRequestComission')

INSERT INTO dbo.Permission(Id, Name) VALUES(231, N'admin-MyPolicyRequestComission')

INSERT INTO dbo.Permission(Id, Name) VALUES(232, N'edit-MyPolicyRequestComission')

INSERT INTO dbo.Permission(Id, Name) VALUES(233, N'view-MyPolicyRequestComission')

SET IDENTITY_INSERT Permission OFF;

COMMIT;


BEGIN TRANSACTION;

SET IDENTITY_INSERT ResourceOperation ON;
INSERT INTO dbo.ResourceOperation(Id, Title, Class, [Key], ResourceId, PermissionId, isDeleted) VALUES(1, N'مشاهده', N'fas fa-eye', N'edit', 1, 150, CONVERT(bit, 'False'))

INSERT INTO dbo.ResourceOperation(Id, Title, Class, [Key], ResourceId, PermissionId, isDeleted) VALUES(2, N'ارجاع', N'fa fa-reply', N'ref', 1, 232, CONVERT(bit, 'False'))

INSERT INTO dbo.ResourceOperation(Id, Title, Class, [Key], ResourceId, PermissionId, isDeleted) VALUES(3, N'مشاهده', N'fas fa-eye', N'edit', 1, 153, CONVERT(bit, 'False'))

INSERT INTO dbo.ResourceOperation(Id, Title, Class, [Key], ResourceId, PermissionId, isDeleted) VALUES(4, N'ارجاع', N'fa fa-reply', N'ref', 1, 229, CONVERT(bit, 'False'))


SET IDENTITY_INSERT ResourceOperation OFF;


COMMIT;

BEGIN TRANSACTION;
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 229)
 
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 232)
   
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 232)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(3, 153)
  
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 150)
    COMMIT;
