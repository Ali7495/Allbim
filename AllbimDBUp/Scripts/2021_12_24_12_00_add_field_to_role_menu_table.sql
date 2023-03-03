ALTER TABLE Role
    ADD ParentId BIGINT ;

ALTER TABLE dbo.Role
    ADD CONSTRAINT FK_Role_Role FOREIGN KEY (ParentId) REFERENCES dbo.Role (Id) ON DELETE No ACTION ON UPDATE No ACTION

DROP TABLE IF EXISTS dbo.Menu;


CREATE TABLE dbo.Menu (
                          Id bigint IDENTITY,
                          Title varchar(100) NULL,
                          Name nvarchar(100) NOT NULL,
                          Icon varchar(100) NULL,
                          [Order] bigint null,
                          ParentId BIGINT Null,
                          PermissionId bigint NULL,
                          PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]


ALTER TABLE dbo.Menu
    ADD CONSTRAINT FK_Menu_Permission FOREIGN KEY (PermissionId) REFERENCES dbo.Permission (Id) ON DELETE SET NULL ON UPDATE CASCADE

ALTER TABLE dbo.Menu
    ADD CONSTRAINT FK_Menu_Menu FOREIGN KEY (ParentId) REFERENCES dbo.Menu (Id) ON DELETE No ACTION ON UPDATE No ACTION





