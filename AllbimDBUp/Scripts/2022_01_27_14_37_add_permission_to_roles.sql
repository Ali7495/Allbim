
BEGIN TRANSACTION;
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(1, 151)
-- کارشناس بیمه
    INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 214)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 206)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 149)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 197)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 226)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 215)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 59)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(5, 232)


-- نماینده بیمه
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(2, 150)


-- کارشناس نمایندگی
INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(6, 214)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(6, 149)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(6, 59)

INSERT INTO dbo.RolePermission(RoleId, PermissionId) VALUES(6, 150)


COMMIT;
