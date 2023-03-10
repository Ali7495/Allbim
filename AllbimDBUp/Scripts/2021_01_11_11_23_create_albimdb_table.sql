
CREATE TABLE dbo.VehicleType (
                                 Id bigint IDENTITY,
                                 Name nvarchar(200) NOT NULL,
                                 Description text NULL,
                                 CreatedBy bigint NULL,
                                 CreatedAt datetime2 NULL DEFAULT (getdate()),
                                 PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]

CREATE TABLE dbo.VehicleBrand (
                                  Id bigint IDENTITY,
                                  Name nvarchar(200) NOT NULL,
                                  Description text NULL,
                                  CreatedBy bigint NULL,
                                  CreatedAt datetime2 NULL DEFAULT (getdate()),
                                  VehicleTypeId bigint NOT NULL,
                                  PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]

ALTER TABLE dbo.VehicleBrand
    ADD CONSTRAINT FK_VehicleBrand_VehicleType FOREIGN KEY (VehicleTypeId) REFERENCES dbo.VehicleType (Id)
    


CREATE TABLE dbo.VehicleApplication (
                                        Id bigint IDENTITY,
                                        VehicleTypeId bigint NOT NULL,
                                        Name nvarchar(200) NULL,
                                        CONSTRAINT PK_VehicleApplication_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    


ALTER TABLE dbo.VehicleApplication
    ADD CONSTRAINT FK_VehicleApplication_VehicleTypeId FOREIGN KEY (VehicleTypeId) REFERENCES dbo.VehicleType (Id)

CREATE TABLE dbo.VehicleRuleCategory (
                                         Id bigint IDENTITY,
                                         Name varchar(50) NULL,
                                         CONSTRAINT PK_VehicleRuleCategory_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
   

CREATE TABLE dbo.Vehicle (
                             Id bigint IDENTITY,
                             Name nvarchar(200) NOT NULL,
                             Description text NULL,
                             CreatedBy bigint NULL,
                             CreatedAt datetime2 NULL DEFAULT (getdate()),
                             UpdatedBy bigint NULL,
                             UpdatedAt datetime2 NULL DEFAULT (getdate()),
                             VehicleBrandId bigint NOT NULL,
                             VehicleRuleCategoryId bigint NOT NULL,
                             PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Vehicle_VehicleBrand] on table [dbo].[Vehicle]
--
    PRINT (N'Create foreign key [FK_Vehicle_VehicleBrand] on table [dbo].[Vehicle]')
    GO
ALTER TABLE dbo.Vehicle
    ADD CONSTRAINT FK_Vehicle_VehicleBrand FOREIGN KEY (VehicleBrandId) REFERENCES dbo.VehicleBrand (Id)
    GO

--
-- Create foreign key [FK_Vehicle_VehicleRuleCategoryId] on table [dbo].[Vehicle]
--
PRINT (N'Create foreign key [FK_Vehicle_VehicleRuleCategoryId] on table [dbo].[Vehicle]')
GO
ALTER TABLE dbo.Vehicle
    ADD CONSTRAINT FK_Vehicle_VehicleRuleCategoryId FOREIGN KEY (VehicleRuleCategoryId) REFERENCES dbo.VehicleRuleCategory (Id)
    GO

--
-- Create table [dbo].[sysdiagrams]
--
PRINT (N'Create table [dbo].[sysdiagrams]')
GO
CREATE TABLE dbo.sysdiagrams (
                                 name sysname NOT NULL,
                                 principal_id int NOT NULL,
                                 diagram_id int IDENTITY,
                                 version int NULL,
                                 definition varbinary(max) NULL,
                                 PRIMARY KEY CLUSTERED (diagram_id),
                                 CONSTRAINT UK_principal_name UNIQUE (principal_id, name)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create table [dbo].[Role]
--
    PRINT (N'Create table [dbo].[Role]')
    GO
CREATE TABLE dbo.Role (
                          Id bigint IDENTITY,
                          Name nvarchar(100) NOT NULL,
                          IsDeleted bit NOT NULL DEFAULT (0),
                          PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[ReminderPeriod]
--
    PRINT (N'Create table [dbo].[ReminderPeriod]')
    GO
CREATE TABLE dbo.ReminderPeriod (
                                    Id bigint IDENTITY,
                                    Name varchar(50) NULL,
                                    IsDelete bit NOT NULL DEFAULT (0),
                                    CONSTRAINT PK_ReminderPeriod_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[RegisterTemp]
--
    PRINT (N'Create table [dbo].[RegisterTemp]')
    GO
CREATE TABLE dbo.RegisterTemp (
                                  Id bigint IDENTITY,
                                  Mobile varchar(20) NULL,
                                  Code varchar(10) NULL,
                                  Ip varchar(50) NULL,
                                  ExpirationDate datetime2 NULL,
                                  CreatedDate datetime2 NOT NULL DEFAULT (getdate()),
                                  CONSTRAINT PK_RegisterTemp_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[Province]
--
    PRINT (N'Create table [dbo].[Province]')
    GO
CREATE TABLE dbo.Province (
                              Id bigint IDENTITY,
                              Name nvarchar(200) NOT NULL,
                              CreatedBy bigint NULL,
                              CreatedAt datetime2 NULL DEFAULT (getdate()),
                              UpdatedBy bigint NULL,
                              UpdatedAt datetime2 NULL DEFAULT (getdate()),
                              IsDeleted bit NOT NULL DEFAULT (0),
                              PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[TownShip]
--
    PRINT (N'Create table [dbo].[TownShip]')
    GO
CREATE TABLE dbo.TownShip (
                              Id bigint IDENTITY,
                              Name nvarchar(200) NOT NULL,
                              ProvinceId bigint NOT NULL,
                              CreatedBy bigint NULL,
                              CreatedAt datetime2 NULL DEFAULT (getdate()),
                              UpdatedBy bigint NULL,
                              UpdatedAt datetime2 NULL DEFAULT (getdate()),
                              PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_TownShip_Province] on table [dbo].[TownShip]
--
    PRINT (N'Create foreign key [FK_TownShip_Province] on table [dbo].[TownShip]')
    GO
ALTER TABLE dbo.TownShip WITH NOCHECK
    ADD CONSTRAINT FK_TownShip_Province FOREIGN KEY (ProvinceId) REFERENCES dbo.Province (Id) ON DELETE CASCADE ON UPDATE CASCADE
GO

--
-- Create table [dbo].[City]
--
PRINT (N'Create table [dbo].[City]')
GO
CREATE TABLE dbo.City (
                          Id bigint IDENTITY,
                          Name nvarchar(200) NOT NULL,
                          TownShipId bigint NOT NULL,
                          CreatedBy bigint NULL,
                          CreatedAt datetime2 NULL DEFAULT (getdate()),
                          UpdatedBy bigint NULL,
                          UpdatedAt datetime2 NULL DEFAULT (getdate()),
                          IsDeleted bit NOT NULL DEFAULT (0),
                          PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_City_TownShip] on table [dbo].[City]
--
    PRINT (N'Create foreign key [FK_City_TownShip] on table [dbo].[City]')
    GO
ALTER TABLE dbo.City WITH NOCHECK
    ADD CONSTRAINT FK_City_TownShip FOREIGN KEY (TownShipId) REFERENCES dbo.TownShip (Id) ON DELETE CASCADE ON UPDATE CASCADE
GO

--
-- Create table [dbo].[Centers]
--
PRINT (N'Create table [dbo].[Centers]')
GO
CREATE TABLE dbo.Centers (
                             Id bigint IDENTITY,
                             Name nvarchar(200) NOT NULL,
                             Description nvarchar(max) NULL,
                             CityId bigint NULL,
                             PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Centers_CityId] on table [dbo].[Centers]
--
    PRINT (N'Create foreign key [FK_Centers_CityId] on table [dbo].[Centers]')
    GO
ALTER TABLE dbo.Centers
    ADD CONSTRAINT FK_Centers_CityId FOREIGN KEY (CityId) REFERENCES dbo.City (Id)
    GO

--
-- Create table [dbo].[Address]
--
PRINT (N'Create table [dbo].[Address]')
GO
CREATE TABLE dbo.Address (
                             Id bigint IDENTITY,
                             Code uniqueidentifier NOT NULL DEFAULT (newid()),
                             Name nvarchar(200) NULL,
                             CityId bigint NOT NULL,
                             Description text NULL,
                             ZoneNumber nvarchar(10) NULL,
                             CreatedBy bigint NULL,
                             CreatedAt datetime2 NULL DEFAULT (getdate()),
                             UpdatedBy bigint NULL,
                             UpdatedAt datetime2 NULL DEFAULT (getdate()),
                             IsDeleted bit NOT NULL DEFAULT (0),
                             Phone varchar(20) NULL,
                             Mobile varchar(20) NULL,
                             PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Address_City] on table [dbo].[Address]
--
    PRINT (N'Create foreign key [FK_Address_City] on table [dbo].[Address]')
    GO
ALTER TABLE dbo.Address
    ADD CONSTRAINT FK_Address_City FOREIGN KEY (CityId) REFERENCES dbo.City (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[PolicyRequestStatus]
--
PRINT (N'Create table [dbo].[PolicyRequestStatus]')
GO
CREATE TABLE dbo.PolicyRequestStatus (
                                         Id bigint IDENTITY,
                                         Name nvarchar(50) NULL,
                                         isDeleted bit NOT NULL DEFAULT (0),
                                         CONSTRAINT PK_PolicyRequestStatus_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[Place]
--
    PRINT (N'Create table [dbo].[Place]')
    GO
CREATE TABLE dbo.Place (
                           Id bigint IDENTITY,
                           Name nvarchar(200) NOT NULL,
                           Description text NULL,
                           CreatedBy bigint NULL,
                           CreatedAt datetime2 NULL DEFAULT (getdate()),
                           UpdatedBy bigint NULL,
                           UpdatedAt datetime2 NULL DEFAULT (getdate()),
                           PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create table [dbo].[PlaceAddress]
--
    PRINT (N'Create table [dbo].[PlaceAddress]')
    GO
CREATE TABLE dbo.PlaceAddress (
                                  Id bigint IDENTITY,
                                  AddressId bigint NOT NULL,
                                  PlaceId bigint NOT NULL,
                                  CreatedBy bigint NULL,
                                  CreatedAt datetime2 NULL DEFAULT (getdate()),
                                  UpdatedBy bigint NULL,
                                  UpdatedAt datetime2 NULL DEFAULT (getdate()),
                                  PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PlaceAddress_Address] on table [dbo].[PlaceAddress]
--
    PRINT (N'Create foreign key [FK_PlaceAddress_Address] on table [dbo].[PlaceAddress]')
    GO
ALTER TABLE dbo.PlaceAddress
    ADD CONSTRAINT FK_PlaceAddress_Address FOREIGN KEY (AddressId) REFERENCES dbo.Address (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create foreign key [FK_PlaceAddress_Place] on table [dbo].[PlaceAddress]
--
PRINT (N'Create foreign key [FK_PlaceAddress_Place] on table [dbo].[PlaceAddress]')
GO
ALTER TABLE dbo.PlaceAddress
    ADD CONSTRAINT FK_PlaceAddress_Place FOREIGN KEY (PlaceId) REFERENCES dbo.Place (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[Person]
--
PRINT (N'Create table [dbo].[Person]')
GO
CREATE TABLE dbo.Person (
                            Id bigint IDENTITY,
                            Code uniqueidentifier NOT NULL DEFAULT (newid()),
                            FirstName nvarchar(100) NULL,
                            LastName nvarchar(100) NULL,
                            NationalCode nvarchar(10) NULL,
    [Identity] nvarchar(10) NULL,
    FatherName nvarchar(100) NULL,
    BirthDate datetime2 NULL,
    GenderId tinyint NOT NULL,
    MarriageId tinyint NULL,
    MillitaryId tinyint NULL,
    IsDeleted bit NOT NULL DEFAULT (0),
    JobName varchar(1000) NULL,
    PRIMARY KEY CLUSTERED (Id)
    )
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[User]
--
    PRINT (N'Create table [dbo].[User]')
    GO
CREATE TABLE dbo.[User] (
                            Id bigint IDENTITY,
                            PersonId bigint NOT NULL,
                            Code uniqueidentifier NOT NULL DEFAULT (newid()),
    Username nvarchar(50) NOT NULL,
    Password nvarchar(200) NULL,
    Email nvarchar(200) NULL,
    EmailVerifiedAt datetime2 NULL,
    IsEnable tinyint NULL DEFAULT (0),
    IsVerified tinyint NULL DEFAULT (0),
    IsTwoStepVerification tinyint NULL DEFAULT (0),
    TwoStepCode nvarchar(10) NULL,
    TwoStepExpiration datetime2 NULL,
    IsLoginNotify tinyint NULL DEFAULT (0),
    VerificationCode varchar(10) NULL,
    VerificationExpiration datetime2 NULL,
    LastLogOnDate datetime2 NULL,
    CreatedDate datetime2 NULL,
    ChangePasswordCode varchar(10) NULL,
    IsDeleted bit NOT NULL DEFAULT (0),
    PRIMARY KEY CLUSTERED (Id)
    )
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_User_Person] on table [dbo].[User]
--
    PRINT (N'Create foreign key [FK_User_Person] on table [dbo].[User]')
    GO
ALTER TABLE dbo.[User]
    ADD CONSTRAINT FK_User_Person FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id) ON DELETE CASCADE ON UPDATE CASCADE
GO

--
-- Create table [dbo].[UserRole]
--
PRINT (N'Create table [dbo].[UserRole]')
GO
CREATE TABLE dbo.UserRole (
                              Id bigint IDENTITY,
                              RoleId bigint NOT NULL,
                              UserId bigint NOT NULL,
                              PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_UserRole_Role] on table [dbo].[UserRole]
--
    PRINT (N'Create foreign key [FK_UserRole_Role] on table [dbo].[UserRole]')
    GO
ALTER TABLE dbo.UserRole
    ADD CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId) REFERENCES dbo.Role (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create foreign key [FK_UserRole_User] on table [dbo].[UserRole]
--
PRINT (N'Create foreign key [FK_UserRole_User] on table [dbo].[UserRole]')
GO
ALTER TABLE dbo.UserRole
    ADD CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId) REFERENCES dbo.[User] (Id) ON DELETE CASCADE ON UPDATE CASCADE
GO

--
-- Create table [dbo].[PersonAddress]
--
PRINT (N'Create table [dbo].[PersonAddress]')
GO
CREATE TABLE dbo.PersonAddress (
                                   Id bigint IDENTITY,
                                   AddressId bigint NOT NULL,
                                   PersonId bigint NOT NULL,
                                   CreatedBy bigint NULL,
                                   CreatedAt datetime2 NULL DEFAULT (getdate()),
                                   UpdatedBy bigint NULL,
                                   UpdatedAt datetime2 NULL DEFAULT (getdate()),
                                   IsDeleted bit NOT NULL DEFAULT (0),
                                   AddressTypeId bigint NOT NULL DEFAULT (2),
                                   PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PersonAddress_Address] on table [dbo].[PersonAddress]
--
    PRINT (N'Create foreign key [FK_PersonAddress_Address] on table [dbo].[PersonAddress]')
    GO
ALTER TABLE dbo.PersonAddress
    ADD CONSTRAINT FK_PersonAddress_Address FOREIGN KEY (AddressId) REFERENCES dbo.Address (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create foreign key [FK_PersonAddress_Person] on table [dbo].[PersonAddress]
--
PRINT (N'Create foreign key [FK_PersonAddress_Person] on table [dbo].[PersonAddress]')
GO
ALTER TABLE dbo.PersonAddress
    ADD CONSTRAINT FK_PersonAddress_Person FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[Article]
--
PRINT (N'Create table [dbo].[Article]')
GO
CREATE TABLE dbo.Article (
                             Id bigint IDENTITY,
                             Title nvarchar(256) NULL,
                             Summary nvarchar(500) NULL,
                             Description nvarchar(max) NULL,
                             Priority tinyint NULL,
                             IsActivated bit NOT NULL DEFAULT (1),
                             IsArchived bit NOT NULL DEFAULT (0),
                             CreatedDateTime datetime2 NOT NULL DEFAULT (getdate()),
                             IsDeleted bit NOT NULL DEFAULT (0),
                             AuthorId bigint NULL,
                             slug nvarchar(50) NULL,
                             CONSTRAINT PK_Article_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Article_AuthorId] on table [dbo].[Article]
--
    PRINT (N'Create foreign key [FK_Article_AuthorId] on table [dbo].[Article]')
    GO
ALTER TABLE dbo.Article
    ADD CONSTRAINT FK_Article_AuthorId FOREIGN KEY (AuthorId) REFERENCES dbo.Person (Id)
    GO

--
-- Create table [dbo].[Company]
--
PRINT (N'Create table [dbo].[Company]')
GO
CREATE TABLE dbo.Company (
                             Id bigint IDENTITY,
                             Code uniqueidentifier NOT NULL DEFAULT (newid()),
                             Name nvarchar(200) NOT NULL,
                             Description text NULL,
                             IsDeleted bit NOT NULL DEFAULT (0),
                             AvatarUrl nvarchar(255) NULL,
                             Summary varchar(max) NULL,
  ArticleId bigint NULL,
  EstablishedYear nvarchar(25) NULL,
  BranchNumber int NULL,
  WealthLevel int NULL,
  DamagePaymentSatisfactionRating int NULL,
  IsInsurer bit NOT NULL DEFAULT (0),
  PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Create foreign key [FK_Company_ArticleId] on table [dbo].[Company]
--
PRINT (N'Create foreign key [FK_Company_ArticleId] on table [dbo].[Company]')
GO
ALTER TABLE dbo.Company
    ADD CONSTRAINT FK_Company_ArticleId FOREIGN KEY (ArticleId) REFERENCES dbo.Article (Id)
    GO

--
-- Create table [dbo].[PersonCompany]
--
PRINT (N'Create table [dbo].[PersonCompany]')
GO
CREATE TABLE dbo.PersonCompany (
                                   Id bigint IDENTITY,
                                   PersonId bigint NULL,
                                   CompanyId bigint NULL,
                                   Position nvarchar(50) NULL,
                                   CreatedDate datetime2 NOT NULL DEFAULT (getdate()),
                                   IsDeleted bit NOT NULL DEFAULT (0),
                                   CONSTRAINT PK_table1_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PersonCompany_CompanyId] on table [dbo].[PersonCompany]
--
    PRINT (N'Create foreign key [FK_PersonCompany_CompanyId] on table [dbo].[PersonCompany]')
    GO
ALTER TABLE dbo.PersonCompany
    ADD CONSTRAINT FK_PersonCompany_CompanyId FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id)
    GO

--
-- Create foreign key [FK_PersonCompany_PersonId] on table [dbo].[PersonCompany]
--
PRINT (N'Create foreign key [FK_PersonCompany_PersonId] on table [dbo].[PersonCompany]')
GO
ALTER TABLE dbo.PersonCompany
    ADD CONSTRAINT FK_PersonCompany_PersonId FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create table [dbo].[CompanyCenter]
--
PRINT (N'Create table [dbo].[CompanyCenter]')
GO
CREATE TABLE dbo.CompanyCenter (
                                   Id bigint IDENTITY,
                                   CompanyId bigint NULL,
                                   Name nvarchar(200) NOT NULL,
                                   Description nvarchar(max) NULL,
                                   CityId bigint NULL,
                                   PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_CompanyCenter_CompanyId] on table [dbo].[CompanyCenter]
--
    PRINT (N'Create foreign key [FK_CompanyCenter_CompanyId] on table [dbo].[CompanyCenter]')
    GO
ALTER TABLE dbo.CompanyCenter
    ADD CONSTRAINT FK_CompanyCenter_CompanyId FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id)
    GO

--
-- Create foreign key [FK_InsuranceCenter_CityId] on table [dbo].[CompanyCenter]
--
PRINT (N'Create foreign key [FK_InsuranceCenter_CityId] on table [dbo].[CompanyCenter]')
GO
ALTER TABLE dbo.CompanyCenter
    ADD CONSTRAINT FK_InsuranceCenter_CityId FOREIGN KEY (CityId) REFERENCES dbo.City (Id)
    GO

--
-- Create table [dbo].[CompanyCenterSchedule]
--
PRINT (N'Create table [dbo].[CompanyCenterSchedule]')
GO
CREATE TABLE dbo.CompanyCenterSchedule (
                                           Id bigint IDENTITY,
                                           CompanyCenterId bigint NULL,
                                           Name nvarchar(200) NOT NULL,
                                           Description nvarchar(max) NULL,
                                           PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_CompanyCenterSchedule_CompanyCenterId] on table [dbo].[CompanyCenterSchedule]
--
    PRINT (N'Create foreign key [FK_CompanyCenterSchedule_CompanyCenterId] on table [dbo].[CompanyCenterSchedule]')
    GO
ALTER TABLE dbo.CompanyCenterSchedule
    ADD CONSTRAINT FK_CompanyCenterSchedule_CompanyCenterId FOREIGN KEY (CompanyCenterId) REFERENCES dbo.CompanyCenter (Id)
    GO

--
-- Create table [dbo].[CompanyAgent]
--
PRINT (N'Create table [dbo].[CompanyAgent]')
GO
CREATE TABLE dbo.CompanyAgent (
                                  Id bigint IDENTITY,
                                  CompanyId bigint NOT NULL,
                                  PersonId bigint NOT NULL,
                                  CityId bigint NOT NULL,
                                  Description nvarchar(max) NULL,
                                  PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_CompanyAgent_CityId] on table [dbo].[CompanyAgent]
--
    PRINT (N'Create foreign key [FK_CompanyAgent_CityId] on table [dbo].[CompanyAgent]')
    GO
ALTER TABLE dbo.CompanyAgent
    ADD CONSTRAINT FK_CompanyAgent_CityId FOREIGN KEY (CityId) REFERENCES dbo.City (Id)
    GO

--
-- Create foreign key [FK_CompanyAgent_CompanyId] on table [dbo].[CompanyAgent]
--
PRINT (N'Create foreign key [FK_CompanyAgent_CompanyId] on table [dbo].[CompanyAgent]')
GO
ALTER TABLE dbo.CompanyAgent
    ADD CONSTRAINT FK_CompanyAgent_CompanyId FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id)
    GO

--
-- Create foreign key [FK_CompanyAgent_PersonId] on table [dbo].[CompanyAgent]
--
PRINT (N'Create foreign key [FK_CompanyAgent_PersonId] on table [dbo].[CompanyAgent]')
GO
ALTER TABLE dbo.CompanyAgent
    ADD CONSTRAINT FK_CompanyAgent_PersonId FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create table [dbo].[CompanyAddress]
--
PRINT (N'Create table [dbo].[CompanyAddress]')
GO
CREATE TABLE dbo.CompanyAddress (
                                    Id bigint IDENTITY,
                                    AddressId bigint NOT NULL,
                                    CompanyId bigint NOT NULL,
                                    CreatedBy bigint NULL,
                                    CreatedAt datetime2 NULL DEFAULT (getdate()),
                                    UpdatedBy bigint NULL,
                                    UpdatedAt datetime2 NULL DEFAULT (getdate()),
                                    IsDeleted bit NOT NULL DEFAULT (0),
                                    PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_CompanyAddress_Address] on table [dbo].[CompanyAddress]
--
    PRINT (N'Create foreign key [FK_CompanyAddress_Address] on table [dbo].[CompanyAddress]')
    GO
ALTER TABLE dbo.CompanyAddress
    ADD CONSTRAINT FK_CompanyAddress_Address FOREIGN KEY (AddressId) REFERENCES dbo.Address (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create foreign key [FK_CompanyAddress_Company] on table [dbo].[CompanyAddress]
--
PRINT (N'Create foreign key [FK_CompanyAddress_Company] on table [dbo].[CompanyAddress]')
GO
ALTER TABLE dbo.CompanyAddress
    ADD CONSTRAINT FK_CompanyAddress_Company FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[Comment]
--
PRINT (N'Create table [dbo].[Comment]')
GO
CREATE TABLE dbo.Comment (
                             Id bigint IDENTITY,
                             AuthorId bigint NULL,
                             ArticleId bigint NOT NULL,
                             ParentId bigint NULL,
                             Description nvarchar(max) NULL,
                             IsDelete bit NOT NULL DEFAULT (0),
                             IsApproved bit NOT NULL DEFAULT (0),
                             Score int NULL,
                             CONSTRAINT PK_Comment_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Comment_ArticleId] on table [dbo].[Comment]
--
    PRINT (N'Create foreign key [FK_Comment_ArticleId] on table [dbo].[Comment]')
    GO
ALTER TABLE dbo.Comment
    ADD CONSTRAINT FK_Comment_ArticleId FOREIGN KEY (ArticleId) REFERENCES dbo.Article (Id)
    GO

--
-- Create foreign key [FK_Comment_AuthorId] on table [dbo].[Comment]
--
PRINT (N'Create foreign key [FK_Comment_AuthorId] on table [dbo].[Comment]')
GO
ALTER TABLE dbo.Comment
    ADD CONSTRAINT FK_Comment_AuthorId FOREIGN KEY (AuthorId) REFERENCES dbo.Person (Id)
    GO

--
-- Create foreign key [FK_Comment_ParentId] on table [dbo].[Comment]
--
PRINT (N'Create foreign key [FK_Comment_ParentId] on table [dbo].[Comment]')
GO
ALTER TABLE dbo.Comment
    ADD CONSTRAINT FK_Comment_ParentId FOREIGN KEY (ParentId) REFERENCES dbo.Comment (Id)
    GO

--
-- Create table [dbo].[ArticleSection]
--
PRINT (N'Create table [dbo].[ArticleSection]')
GO
CREATE TABLE dbo.ArticleSection (
                                    Id bigint IDENTITY,
                                    ArticleId bigint NOT NULL,
                                    SectionId int NOT NULL,
                                    CONSTRAINT PK_ArticleSection_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_ArticleSection_ArticleId] on table [dbo].[ArticleSection]
--
    PRINT (N'Create foreign key [FK_ArticleSection_ArticleId] on table [dbo].[ArticleSection]')
    GO
ALTER TABLE dbo.ArticleSection
    ADD CONSTRAINT FK_ArticleSection_ArticleId FOREIGN KEY (ArticleId) REFERENCES dbo.Article (Id)
    GO

--
-- Create table [dbo].[ArticleMetaKey]
--
PRINT (N'Create table [dbo].[ArticleMetaKey]')
GO
CREATE TABLE dbo.ArticleMetaKey (
                                    Id bigint IDENTITY,
                                    ArticleId bigint NOT NULL,
    [Key] nvarchar(255) NULL,
    Value nvarchar(255) NULL,
    IsDeleted bit NOT NULL DEFAULT (0),
    CONSTRAINT PK_ArticleMeta_Id PRIMARY KEY CLUSTERED (Id)
    )
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_ArticleMetaKey_ArticleId] on table [dbo].[ArticleMetaKey]
--
    PRINT (N'Create foreign key [FK_ArticleMetaKey_ArticleId] on table [dbo].[ArticleMetaKey]')
    GO
ALTER TABLE dbo.ArticleMetaKey
    ADD CONSTRAINT FK_ArticleMetaKey_ArticleId FOREIGN KEY (ArticleId) REFERENCES dbo.Article (Id)
    GO

--
-- Create table [dbo].[Permission]
--
PRINT (N'Create table [dbo].[Permission]')
GO
CREATE TABLE dbo.Permission (
                                Id bigint IDENTITY,
                                Name nvarchar(100) NOT NULL,
                                PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[RolePermission]
--
    PRINT (N'Create table [dbo].[RolePermission]')
    GO
CREATE TABLE dbo.RolePermission (
                                    Id bigint IDENTITY,
                                    RoleId bigint NOT NULL,
                                    PermissionId bigint NOT NULL,
                                    PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_RolePermission_Permission] on table [dbo].[RolePermission]
--
    PRINT (N'Create foreign key [FK_RolePermission_Permission] on table [dbo].[RolePermission]')
    GO
ALTER TABLE dbo.RolePermission
    ADD CONSTRAINT FK_RolePermission_Permission FOREIGN KEY (PermissionId) REFERENCES dbo.Permission (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create foreign key [FK_RolePermission_Role] on table [dbo].[RolePermission]
--
PRINT (N'Create foreign key [FK_RolePermission_Role] on table [dbo].[RolePermission]')
GO
ALTER TABLE dbo.RolePermission
    ADD CONSTRAINT FK_RolePermission_Role FOREIGN KEY (RoleId) REFERENCES dbo.Role (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[Menu]
--
PRINT (N'Create table [dbo].[Menu]')
GO
CREATE TABLE dbo.Menu (
                          Id bigint IDENTITY,
                          Caption varchar(50) NULL,
                          Name nvarchar(100) NOT NULL,
                          link varchar(50) NULL,
                          PermissionId bigint NOT NULL,
                          PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Menu_Permission] on table [dbo].[Menu]
--
    PRINT (N'Create foreign key [FK_Menu_Permission] on table [dbo].[Menu]')
    GO
ALTER TABLE dbo.Menu
    ADD CONSTRAINT FK_Menu_Permission FOREIGN KEY (PermissionId) REFERENCES dbo.Permission (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[PaymentGateway]
--
PRINT (N'Create table [dbo].[PaymentGateway]')
GO
CREATE TABLE dbo.PaymentGateway (
                                    Id bigint IDENTITY,
                                    TerminalId varchar(255) NULL,
                                    Username varchar(50) NULL,
                                    Password varchar(50) NULL,
                                    CardNumber varchar(255) NULL,
                                    AccountNumber varchar(255) NULL,
                                    AllowOnline varchar(255) NULL,
                                    AllowManual varchar(255) NULL,
                                    CONSTRAINT PK_PaymentGateway_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[Payment]
--
    PRINT (N'Create table [dbo].[Payment]')
    GO
CREATE TABLE dbo.Payment (
                             Id bigint IDENTITY,
                             Price decimal NOT NULL,
                             Status tinyint NOT NULL DEFAULT (1),
                             CreatedDateTime datetime2 NOT NULL DEFAULT (getdate()),
                             PaidDateTime datetime2 NULL,
                             PaymentCode varchar(100) NULL,
                             Description text NULL,
                             CONSTRAINT PK_Payment_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create table [dbo].[OnlinePayment]
--
    PRINT (N'Create table [dbo].[OnlinePayment]')
    GO
CREATE TABLE dbo.OnlinePayment (
                                   Id bigint IDENTITY,
                                   PaymentId bigint NOT NULL,
                                   PaymentSettle bit NOT NULL DEFAULT (0),
                                   PaymentVerify bit NOT NULL DEFAULT (0),
                                   RefId varchar(50) NULL,
                                   SaleOrderId bigint NULL,
                                   SaleReferenceId varchar(50) NULL,
                                   SettleDate datetime2 NULL,
                                   VerifyDate datetime2 NULL,
                                   CONSTRAINT PK_OnlinePayment_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_OnlinePayment_PaymentId] on table [dbo].[OnlinePayment]
--
    PRINT (N'Create foreign key [FK_OnlinePayment_PaymentId] on table [dbo].[OnlinePayment]')
    GO
ALTER TABLE dbo.OnlinePayment
    ADD CONSTRAINT FK_OnlinePayment_PaymentId FOREIGN KEY (PaymentId) REFERENCES dbo.Payment (Id)
    GO

--
-- Create table [dbo].[IssueSession]
--
PRINT (N'Create table [dbo].[IssueSession]')
GO
CREATE TABLE dbo.IssueSession (
                                  Id bigint IDENTITY,
                                  Name nvarchar(200) NULL,
                                  Description nvarchar(300) NULL,
                                  IsDeleted bit NOT NULL DEFAULT (0),
                                  CONSTRAINT PK_IssueSession_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[InsurerTermDetail]
--
    PRINT (N'Create table [dbo].[InsurerTermDetail]')
    GO
CREATE TABLE dbo.InsurerTermDetail (
                                       Id bigint IDENTITY,
                                       InsurerTermId bigint NOT NULL,
    [Order] int NULL,
                                       ParentId bigint NULL,
                                       Field nvarchar(200) NULL,
                                       Criteria nvarchar(100) NULL,
                                       Value nvarchar(100) NULL,
                                       Discount nvarchar(100) NULL,
                                       CalculationType nvarchar(100) NULL,
                                       IsCumulative bit NOT NULL DEFAULT (1),
                                       CONSTRAINT PK_InsurerTermDetail_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[Insurance]
--
    PRINT (N'Create table [dbo].[Insurance]')
    GO
CREATE TABLE dbo.Insurance (
                               Id bigint IDENTITY,
                               Name nvarchar(200) NOT NULL,
                               Description nvarchar(max) NULL,
                               CreatedBy bigint NULL,
                               CreatedAt datetime2 NULL DEFAULT (getdate()),
                               UpdatedBy bigint NULL,
                               UpdatedAt datetime2 NULL DEFAULT (getdate()),
                               Slug nvarchar(100) NULL,
                               AvatarUrl nvarchar(255) NULL,
                               IsDeleted bit NOT NULL DEFAULT (0),
                               Summary nvarchar(max) NULL,
                               PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create table [dbo].[Reminder]
--
    PRINT (N'Create table [dbo].[Reminder]')
    GO
CREATE TABLE dbo.Reminder (
                              Id bigint IDENTITY,
                              InsuranceId bigint NULL,
                              ReminderPeriodId bigint NULL,
                              Description text NULL,
                              DueDate datetime2 NULL,
                              CityId bigint NULL,
                              PersonId bigint NULL,
                              IsDeleted bit NOT NULL DEFAULT (0),
                              CONSTRAINT PK_Reminder_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Reminder_CityId] on table [dbo].[Reminder]
--
    PRINT (N'Create foreign key [FK_Reminder_CityId] on table [dbo].[Reminder]')
    GO
ALTER TABLE dbo.Reminder
    ADD CONSTRAINT FK_Reminder_CityId FOREIGN KEY (CityId) REFERENCES dbo.City (Id)
    GO

--
-- Create foreign key [FK_Reminder_InsuranceId] on table [dbo].[Reminder]
--
PRINT (N'Create foreign key [FK_Reminder_InsuranceId] on table [dbo].[Reminder]')
GO
ALTER TABLE dbo.Reminder
    ADD CONSTRAINT FK_Reminder_InsuranceId FOREIGN KEY (InsuranceId) REFERENCES dbo.Insurance (Id)
    GO

--
-- Create foreign key [FK_Reminder_PersonId] on table [dbo].[Reminder]
--
PRINT (N'Create foreign key [FK_Reminder_PersonId] on table [dbo].[Reminder]')
GO
ALTER TABLE dbo.Reminder
    ADD CONSTRAINT FK_Reminder_PersonId FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create foreign key [FK_Reminder_ReminderPeriodId] on table [dbo].[Reminder]
--
PRINT (N'Create foreign key [FK_Reminder_ReminderPeriodId] on table [dbo].[Reminder]')
GO
ALTER TABLE dbo.Reminder
    ADD CONSTRAINT FK_Reminder_ReminderPeriodId FOREIGN KEY (ReminderPeriodId) REFERENCES dbo.ReminderPeriod (Id)
    GO

--
-- Create table [dbo].[Insurer]
--
PRINT (N'Create table [dbo].[Insurer]')
GO
CREATE TABLE dbo.Insurer (
                             Id bigint IDENTITY,
                             InsuranceId bigint NOT NULL,
                             CompanyId bigint NOT NULL,
                             CreatedBy bigint NULL,
                             CreatedAt datetime2 NULL DEFAULT (getdate()),
                             UpdatedBy bigint NULL,
                             UpdatedAt datetime2 NULL DEFAULT (getdate()),
                             ArticleId bigint NULL,
                             PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Insurer_ArticleId] on table [dbo].[Insurer]
--
    PRINT (N'Create foreign key [FK_Insurer_ArticleId] on table [dbo].[Insurer]')
    GO
ALTER TABLE dbo.Insurer
    ADD CONSTRAINT FK_Insurer_ArticleId FOREIGN KEY (ArticleId) REFERENCES dbo.Article (Id)
    GO

--
-- Create foreign key [FK_Insurer_Company] on table [dbo].[Insurer]
--
PRINT (N'Create foreign key [FK_Insurer_Company] on table [dbo].[Insurer]')
GO
ALTER TABLE dbo.Insurer
    ADD CONSTRAINT FK_Insurer_Company FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create foreign key [FK_Insurer_Insurance] on table [dbo].[Insurer]
--
PRINT (N'Create foreign key [FK_Insurer_Insurance] on table [dbo].[Insurer]')
GO
ALTER TABLE dbo.Insurer
    ADD CONSTRAINT FK_Insurer_Insurance FOREIGN KEY (InsuranceId) REFERENCES dbo.Insurance (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[PolicyRequest]
--
PRINT (N'Create table [dbo].[PolicyRequest]')
GO
CREATE TABLE dbo.PolicyRequest (
                                   Id bigint IDENTITY,
                                   Code uniqueidentifier NOT NULL DEFAULT (newid()),
                                   RequestPersonId bigint NOT NULL,
                                   Title nvarchar(100) NULL,
                                   InsurerId bigint NOT NULL,
                                   PolicyNumber nvarchar(100) NOT NULL,
                                   Description nvarchar(max) NULL,
                                   IsDeleted tinyint NOT NULL DEFAULT (0),
                                   CreatedDate datetime2 NULL DEFAULT (getdate()),
                                   PolicyRequestStatusId bigint NULL,
                                   ReviewerId bigint NULL,
                                   PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequest_Insurer] on table [dbo].[PolicyRequest]
--
    PRINT (N'Create foreign key [FK_PolicyRequest_Insurer] on table [dbo].[PolicyRequest]')
    GO
ALTER TABLE dbo.PolicyRequest
    ADD CONSTRAINT FK_PolicyRequest_Insurer FOREIGN KEY (InsurerId) REFERENCES dbo.Insurer (Id)
    GO

--
-- Create foreign key [FK_PolicyRequest_Person] on table [dbo].[PolicyRequest]
--
PRINT (N'Create foreign key [FK_PolicyRequest_Person] on table [dbo].[PolicyRequest]')
GO
ALTER TABLE dbo.PolicyRequest
    ADD CONSTRAINT FK_PolicyRequest_Person FOREIGN KEY (RequestPersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create foreign key [FK_PolicyRequest_PolicyRequestStatusId] on table [dbo].[PolicyRequest]
--
PRINT (N'Create foreign key [FK_PolicyRequest_PolicyRequestStatusId] on table [dbo].[PolicyRequest]')
GO
ALTER TABLE dbo.PolicyRequest
    ADD CONSTRAINT FK_PolicyRequest_PolicyRequestStatusId FOREIGN KEY (PolicyRequestStatusId) REFERENCES dbo.PolicyRequestStatus (Id)
    GO

--
-- Create foreign key [FK_PolicyRequest_ReviewerId] on table [dbo].[PolicyRequest]
--
PRINT (N'Create foreign key [FK_PolicyRequest_ReviewerId] on table [dbo].[PolicyRequest]')
GO
ALTER TABLE dbo.PolicyRequest
    ADD CONSTRAINT FK_PolicyRequest_ReviewerId FOREIGN KEY (ReviewerId) REFERENCES dbo.Person (Id)
    GO

--
-- Create table [dbo].[PolicyRequestIssue]
--
PRINT (N'Create table [dbo].[PolicyRequestIssue]')
GO
CREATE TABLE dbo.PolicyRequestIssue (
                                        Id bigint IDENTITY,
                                        PolicyRequestId bigint NOT NULL,
                                        EmailAddress nvarchar(100) NULL,
                                        NeedPrint bit NULL DEFAULT (0),
                                        ReceiverAddressId bigint NULL,
                                        ReceiveDate datetime2 NULL,
                                        IssueSessionId bigint NULL,
                                        WalletId bigint NULL,
                                        Description nvarchar(max) NULL,
                                        PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequestIssue_PolicyRequestId] on table [dbo].[PolicyRequestIssue]
--
    PRINT (N'Create foreign key [FK_PolicyRequestIssue_PolicyRequestId] on table [dbo].[PolicyRequestIssue]')
    GO
ALTER TABLE dbo.PolicyRequestIssue
    ADD CONSTRAINT FK_PolicyRequestIssue_PolicyRequestId FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestIssue_ReceiverAddressId] on table [dbo].[PolicyRequestIssue]
--
PRINT (N'Create foreign key [FK_PolicyRequestIssue_ReceiverAddressId] on table [dbo].[PolicyRequestIssue]')
GO
ALTER TABLE dbo.PolicyRequestIssue
    ADD CONSTRAINT FK_PolicyRequestIssue_ReceiverAddressId FOREIGN KEY (ReceiverAddressId) REFERENCES dbo.Address (Id)
    GO

--
-- Create table [dbo].[PolicyRequestHolder]
--
PRINT (N'Create table [dbo].[PolicyRequestHolder]')
GO
CREATE TABLE dbo.PolicyRequestHolder (
                                         Id bigint IDENTITY,
                                         PolicyRequestId bigint NOT NULL,
                                         PersonId bigint NULL,
                                         CompanyId bigint NULL,
                                         IssuedPersonType tinyint NULL,
                                         IssuedPersonRelation varchar(50) NULL,
                                         AddressId bigint NULL,
                                         PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequestHolder_AddressId] on table [dbo].[PolicyRequestHolder]
--
    PRINT (N'Create foreign key [FK_PolicyRequestHolder_AddressId] on table [dbo].[PolicyRequestHolder]')
    GO
ALTER TABLE dbo.PolicyRequestHolder
    ADD CONSTRAINT FK_PolicyRequestHolder_AddressId FOREIGN KEY (AddressId) REFERENCES dbo.Address (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestHolder_Company] on table [dbo].[PolicyRequestHolder]
--
PRINT (N'Create foreign key [FK_PolicyRequestHolder_Company] on table [dbo].[PolicyRequestHolder]')
GO
ALTER TABLE dbo.PolicyRequestHolder
    ADD CONSTRAINT FK_PolicyRequestHolder_Company FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestHolder_Person] on table [dbo].[PolicyRequestHolder]
--
PRINT (N'Create foreign key [FK_PolicyRequestHolder_Person] on table [dbo].[PolicyRequestHolder]')
GO
ALTER TABLE dbo.PolicyRequestHolder
    ADD CONSTRAINT FK_PolicyRequestHolder_Person FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestHolder_PolicyRequest] on table [dbo].[PolicyRequestHolder]
--
PRINT (N'Create foreign key [FK_PolicyRequestHolder_PolicyRequest] on table [dbo].[PolicyRequestHolder]')
GO
ALTER TABLE dbo.PolicyRequestHolder
    ADD CONSTRAINT FK_PolicyRequestHolder_PolicyRequest FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create table [dbo].[PolicyRequestFactor]
--
PRINT (N'Create table [dbo].[PolicyRequestFactor]')
GO
CREATE TABLE dbo.PolicyRequestFactor (
                                         Id bigint IDENTITY,
                                         PaymentId bigint NULL,
                                         PolicyRequestId bigint NULL,
                                         CONSTRAINT PK_PolicyRequestFactor_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequestFactor_PaymentId] on table [dbo].[PolicyRequestFactor]
--
    PRINT (N'Create foreign key [FK_PolicyRequestFactor_PaymentId] on table [dbo].[PolicyRequestFactor]')
    GO
ALTER TABLE dbo.PolicyRequestFactor
    ADD CONSTRAINT FK_PolicyRequestFactor_PaymentId FOREIGN KEY (PaymentId) REFERENCES dbo.Payment (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestFactor_PolicyRequestId] on table [dbo].[PolicyRequestFactor]
--
PRINT (N'Create foreign key [FK_PolicyRequestFactor_PolicyRequestId] on table [dbo].[PolicyRequestFactor]')
GO
ALTER TABLE dbo.PolicyRequestFactor
    ADD CONSTRAINT FK_PolicyRequestFactor_PolicyRequestId FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create table [dbo].[PolicyRequestComment]
--
PRINT (N'Create table [dbo].[PolicyRequestComment]')
GO
CREATE TABLE dbo.PolicyRequestComment (
                                          Id bigint IDENTITY,
                                          PolicyRequestId bigint NOT NULL,
                                          Description nvarchar(max) NULL,
                                          AuthorId bigint NOT NULL,
                                          AuthorTypeId tinyint NULL,
                                          IsDeleted bit NOT NULL DEFAULT (0),
                                          CONSTRAINT PK_PolicyRequestComment_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequestComment_AuthorId] on table [dbo].[PolicyRequestComment]
--
    PRINT (N'Create foreign key [FK_PolicyRequestComment_AuthorId] on table [dbo].[PolicyRequestComment]')
    GO
ALTER TABLE dbo.PolicyRequestComment
    ADD CONSTRAINT FK_PolicyRequestComment_AuthorId FOREIGN KEY (AuthorId) REFERENCES dbo.Person (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestComment_PolicyRequestId] on table [dbo].[PolicyRequestComment]
--
PRINT (N'Create foreign key [FK_PolicyRequestComment_PolicyRequestId] on table [dbo].[PolicyRequestComment]')
GO
ALTER TABLE dbo.PolicyRequestComment
    ADD CONSTRAINT FK_PolicyRequestComment_PolicyRequestId FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create table [dbo].[InsuredRequest]
--
PRINT (N'Create table [dbo].[InsuredRequest]')
GO
CREATE TABLE dbo.InsuredRequest (
                                    Id bigint IDENTITY,
                                    PolicyRequestId bigint NOT NULL,
                                    PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuredRequest_PolicyRequest] on table [dbo].[InsuredRequest]
--
    PRINT (N'Create foreign key [FK_InsuredRequest_PolicyRequest] on table [dbo].[InsuredRequest]')
    GO
ALTER TABLE dbo.InsuredRequest
    ADD CONSTRAINT FK_InsuredRequest_PolicyRequest FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create table [dbo].[InsuredRequestVehicle]
--
PRINT (N'Create table [dbo].[InsuredRequestVehicle]')
GO
CREATE TABLE dbo.InsuredRequestVehicle (
                                           Id bigint IDENTITY,
                                           InsuredRequestId bigint NOT NULL,
                                           OwnerPersonId bigint NULL,
                                           OwnerCompanyId bigint NULL,
                                           VehicleId bigint NOT NULL,
                                           PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuredRequestVehicle_Company] on table [dbo].[InsuredRequestVehicle]
--
    PRINT (N'Create foreign key [FK_InsuredRequestVehicle_Company] on table [dbo].[InsuredRequestVehicle]')
    GO
ALTER TABLE dbo.InsuredRequestVehicle
    ADD CONSTRAINT FK_InsuredRequestVehicle_Company FOREIGN KEY (OwnerCompanyId) REFERENCES dbo.Company (Id)
    GO

--
-- Create foreign key [FK_InsuredRequestVehicle_InsuredRequest] on table [dbo].[InsuredRequestVehicle]
--
PRINT (N'Create foreign key [FK_InsuredRequestVehicle_InsuredRequest] on table [dbo].[InsuredRequestVehicle]')
GO
ALTER TABLE dbo.InsuredRequestVehicle
    ADD CONSTRAINT FK_InsuredRequestVehicle_InsuredRequest FOREIGN KEY (InsuredRequestId) REFERENCES dbo.InsuredRequest (Id)
    GO

--
-- Create foreign key [FK_InsuredRequestVehicle_Person] on table [dbo].[InsuredRequestVehicle]
--
PRINT (N'Create foreign key [FK_InsuredRequestVehicle_Person] on table [dbo].[InsuredRequestVehicle]')
GO
ALTER TABLE dbo.InsuredRequestVehicle
    ADD CONSTRAINT FK_InsuredRequestVehicle_Person FOREIGN KEY (OwnerPersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create foreign key [FK_InsuredRequestVehicle_Vehicle] on table [dbo].[InsuredRequestVehicle]
--
PRINT (N'Create foreign key [FK_InsuredRequestVehicle_Vehicle] on table [dbo].[InsuredRequestVehicle]')
GO
ALTER TABLE dbo.InsuredRequestVehicle
    ADD CONSTRAINT FK_InsuredRequestVehicle_Vehicle FOREIGN KEY (VehicleId) REFERENCES dbo.Vehicle (Id)
    GO

--
-- Create table [dbo].[InsuredRequestVehicleDetails]
--
PRINT (N'Create table [dbo].[InsuredRequestVehicleDetails]')
GO
CREATE TABLE dbo.InsuredRequestVehicleDetails (
                                                  Id bigint IDENTITY,
                                                  InsuredRequestVehicleId bigint NOT NULL,
    [Key] nvarchar(200) NOT NULL,
    Value nvarchar(200) NOT NULL,
    CreatedYear int NOT NULL,
    Description text NULL,
    CreatedBy bigint NULL,
    CreatedAt datetime2 NULL DEFAULT (getdate()),
    UpdatedBy bigint NULL,
    UpdatedAt datetime2 NULL DEFAULT (getdate()),
    PRIMARY KEY CLUSTERED (Id)
    )
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuredRequestVehicleDetails_InsuredRequestVehicle] on table [dbo].[InsuredRequestVehicleDetails]
--
    PRINT (N'Create foreign key [FK_InsuredRequestVehicleDetails_InsuredRequestVehicle] on table [dbo].[InsuredRequestVehicleDetails]')
    GO
ALTER TABLE dbo.InsuredRequestVehicleDetails
    ADD CONSTRAINT FK_InsuredRequestVehicleDetails_InsuredRequestVehicle FOREIGN KEY (InsuredRequestVehicleId) REFERENCES dbo.InsuredRequestVehicle (Id)
    GO

--
-- Create table [dbo].[InsuredRequestRelatedPerson]
--
PRINT (N'Create table [dbo].[InsuredRequestRelatedPerson]')
GO
CREATE TABLE dbo.InsuredRequestRelatedPerson (
                                                 Id bigint IDENTITY,
                                                 InsuredRequestId bigint NOT NULL,
                                                 RelationTypeId int NOT NULL,
                                                 PersonId bigint NOT NULL,
                                                 PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuredRequestRelatedPerson_InsuredRequest] on table [dbo].[InsuredRequestRelatedPerson]
--
    PRINT (N'Create foreign key [FK_InsuredRequestRelatedPerson_InsuredRequest] on table [dbo].[InsuredRequestRelatedPerson]')
    GO
ALTER TABLE dbo.InsuredRequestRelatedPerson
    ADD CONSTRAINT FK_InsuredRequestRelatedPerson_InsuredRequest FOREIGN KEY (InsuredRequestId) REFERENCES dbo.InsuredRequest (Id)
    GO

--
-- Create foreign key [FK_InsuredRequestRelatedPerson_Person] on table [dbo].[InsuredRequestRelatedPerson]
--
PRINT (N'Create foreign key [FK_InsuredRequestRelatedPerson_Person] on table [dbo].[InsuredRequestRelatedPerson]')
GO
ALTER TABLE dbo.InsuredRequestRelatedPerson
    ADD CONSTRAINT FK_InsuredRequestRelatedPerson_Person FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create table [dbo].[InsuredRequestPlace]
--
PRINT (N'Create table [dbo].[InsuredRequestPlace]')
GO
CREATE TABLE dbo.InsuredRequestPlace (
                                         Id bigint IDENTITY,
                                         InsuredRequestId bigint NOT NULL,
                                         PlaceTypeId int NOT NULL,
                                         PlaceId bigint NOT NULL,
                                         PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuredRequestPlace_InsuredRequest] on table [dbo].[InsuredRequestPlace]
--
    PRINT (N'Create foreign key [FK_InsuredRequestPlace_InsuredRequest] on table [dbo].[InsuredRequestPlace]')
    GO
ALTER TABLE dbo.InsuredRequestPlace
    ADD CONSTRAINT FK_InsuredRequestPlace_InsuredRequest FOREIGN KEY (InsuredRequestId) REFERENCES dbo.InsuredRequest (Id)
    GO

--
-- Create foreign key [FK_InsuredRequestPlace_Place] on table [dbo].[InsuredRequestPlace]
--
PRINT (N'Create foreign key [FK_InsuredRequestPlace_Place] on table [dbo].[InsuredRequestPlace]')
GO
ALTER TABLE dbo.InsuredRequestPlace
    ADD CONSTRAINT FK_InsuredRequestPlace_Place FOREIGN KEY (PlaceId) REFERENCES dbo.Place (Id)
    GO

--
-- Create table [dbo].[InsuredRequestPerson]
--
PRINT (N'Create table [dbo].[InsuredRequestPerson]')
GO
CREATE TABLE dbo.InsuredRequestPerson (
                                          Id bigint IDENTITY,
                                          InsuredRequestId bigint NOT NULL,
                                          PersonId bigint NOT NULL,
                                          PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuredRequestPerson__Person] on table [dbo].[InsuredRequestPerson]
--
    PRINT (N'Create foreign key [FK_InsuredRequestPerson__Person] on table [dbo].[InsuredRequestPerson]')
    GO
ALTER TABLE dbo.InsuredRequestPerson
    ADD CONSTRAINT FK_InsuredRequestPerson__Person FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create foreign key [FK_InsuredRequestPerson_InsuredRequest] on table [dbo].[InsuredRequestPerson]
--
PRINT (N'Create foreign key [FK_InsuredRequestPerson_InsuredRequest] on table [dbo].[InsuredRequestPerson]')
GO
ALTER TABLE dbo.InsuredRequestPerson
    ADD CONSTRAINT FK_InsuredRequestPerson_InsuredRequest FOREIGN KEY (InsuredRequestId) REFERENCES dbo.InsuredRequest (Id)
    GO

--
-- Create table [dbo].[InsuredRequestCompany]
--
PRINT (N'Create table [dbo].[InsuredRequestCompany]')
GO
CREATE TABLE dbo.InsuredRequestCompany (
                                           Id bigint IDENTITY,
                                           InsuredRequestId bigint NOT NULL,
                                           CompanyId bigint NOT NULL,
                                           PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuredRequestCompany__Company] on table [dbo].[InsuredRequestCompany]
--
    PRINT (N'Create foreign key [FK_InsuredRequestCompany__Company] on table [dbo].[InsuredRequestCompany]')
    GO
ALTER TABLE dbo.InsuredRequestCompany
    ADD CONSTRAINT FK_InsuredRequestCompany__Company FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id)
    GO

--
-- Create foreign key [FK_InsuredRequestCompany_InsuredRequest] on table [dbo].[InsuredRequestCompany]
--
PRINT (N'Create foreign key [FK_InsuredRequestCompany_InsuredRequest] on table [dbo].[InsuredRequestCompany]')
GO
ALTER TABLE dbo.InsuredRequestCompany
    ADD CONSTRAINT FK_InsuredRequestCompany_InsuredRequest FOREIGN KEY (InsuredRequestId) REFERENCES dbo.InsuredRequest (Id)
    GO

--
-- Create table [dbo].[InsurerTerm]
--
PRINT (N'Create table [dbo].[InsurerTerm]')
GO
CREATE TABLE dbo.InsurerTerm (
                                 Id bigint IDENTITY,
                                 InsurerId bigint NOT NULL,
                                 Type tinyint NOT NULL,
                                 Field nvarchar(200) NOT NULL,
                                 Criteria nvarchar(100) NOT NULL,
                                 Value nvarchar(100) NOT NULL,
                                 Discount nvarchar(100) NOT NULL,
                                 CalculationType nvarchar(100) NOT NULL,
                                 CreatedBy bigint NULL,
                                 CreatedAt datetime2 NULL DEFAULT (getdate()),
                                 UpdatedBy bigint NULL,
                                 UpdatedAt datetime2 NULL DEFAULT (getdate()),
                                 IsCumulative bit NOT NULL DEFAULT (0),
                                 PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsurerTerm_Insurer] on table [dbo].[InsurerTerm]
--
    PRINT (N'Create foreign key [FK_InsurerTerm_Insurer] on table [dbo].[InsurerTerm]')
    GO
ALTER TABLE dbo.InsurerTerm
    ADD CONSTRAINT FK_InsurerTerm_Insurer FOREIGN KEY (InsurerId) REFERENCES dbo.Insurer (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[PolicyRequestDetails]
--
PRINT (N'Create table [dbo].[PolicyRequestDetails]')
GO
CREATE TABLE dbo.PolicyRequestDetails (
                                          Id bigint IDENTITY,
                                          PolicyRequestId bigint NOT NULL,
                                          Type tinyint NOT NULL,
                                          Field nvarchar(200) NOT NULL,
                                          Criteria nvarchar(100) NOT NULL,
                                          Value nvarchar(100) NOT NULL,
                                          Discount nvarchar(100) NOT NULL,
                                          CalculationType nvarchar(100) NOT NULL,
                                          UserInput varchar(100) NULL,
                                          InsurerTermId bigint NULL,
                                          IsCumulative bit NOT NULL DEFAULT (0),
                                          PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequestDetails_InsurerTermId] on table [dbo].[PolicyRequestDetails]
--
    PRINT (N'Create foreign key [FK_PolicyRequestDetails_InsurerTermId] on table [dbo].[PolicyRequestDetails]')
    GO
ALTER TABLE dbo.PolicyRequestDetails
    ADD CONSTRAINT FK_PolicyRequestDetails_InsurerTermId FOREIGN KEY (InsurerTermId) REFERENCES dbo.InsurerTerm (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestDetails_PolicyRequest] on table [dbo].[PolicyRequestDetails]
--
PRINT (N'Create foreign key [FK_PolicyRequestDetails_PolicyRequest] on table [dbo].[PolicyRequestDetails]')
GO
ALTER TABLE dbo.PolicyRequestDetails
    ADD CONSTRAINT FK_PolicyRequestDetails_PolicyRequest FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create table [dbo].[Discount]
--
PRINT (N'Create table [dbo].[Discount]')
GO
CREATE TABLE dbo.Discount (
                              Id bigint IDENTITY,
                              PersonId bigint NULL,
                              InsuranceId bigint NULL,
                              InsurerId bigint NULL,
                              Value int NULL,
                              ExpirationDateTime datetime2 NULL,
                              IsUsed bit NOT NULL DEFAULT (0),
                              CreatedDateTime datetime2 NOT NULL DEFAULT (getdate()),
                              IsDeleted bit NOT NULL DEFAULT (0),
                              CONSTRAINT PK_Discount_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Discount_InsuranceId] on table [dbo].[Discount]
--
    PRINT (N'Create foreign key [FK_Discount_InsuranceId] on table [dbo].[Discount]')
    GO
ALTER TABLE dbo.Discount
    ADD CONSTRAINT FK_Discount_InsuranceId FOREIGN KEY (InsuranceId) REFERENCES dbo.Insurance (Id)
    GO

--
-- Create foreign key [FK_Discount_InsurerId] on table [dbo].[Discount]
--
PRINT (N'Create foreign key [FK_Discount_InsurerId] on table [dbo].[Discount]')
GO
ALTER TABLE dbo.Discount
    ADD CONSTRAINT FK_Discount_InsurerId FOREIGN KEY (InsurerId) REFERENCES dbo.Insurer (Id)
    GO

--
-- Create foreign key [FK_Discount_PersonId] on table [dbo].[Discount]
--
PRINT (N'Create foreign key [FK_Discount_PersonId] on table [dbo].[Discount]')
GO
ALTER TABLE dbo.Discount
    ADD CONSTRAINT FK_Discount_PersonId FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO

--
-- Create table [dbo].[InsuranceStep]
--
PRINT (N'Create table [dbo].[InsuranceStep]')
GO
CREATE TABLE dbo.InsuranceStep (
                                   Id bigint IDENTITY,
                                   InsuranceId bigint NULL,
                                   StepName varchar(100) NULL,
                                   StepOrder int NOT NULL DEFAULT (1),
                                   CONSTRAINT PK_InsuranceStep_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuranceStep_InsuranceId] on table [dbo].[InsuranceStep]
--
    PRINT (N'Create foreign key [FK_InsuranceStep_InsuranceId] on table [dbo].[InsuranceStep]')
    GO
ALTER TABLE dbo.InsuranceStep
    ADD CONSTRAINT FK_InsuranceStep_InsuranceId FOREIGN KEY (InsuranceId) REFERENCES dbo.Insurance (Id)
    GO

--
-- Create table [dbo].[InsuranceField]
--
PRINT (N'Create table [dbo].[InsuranceField]')
GO
CREATE TABLE dbo.InsuranceField (
                                    Id bigint IDENTITY,
                                    InsuranceId bigint NOT NULL,
    [Key] nvarchar(200) NOT NULL,
    Type nvarchar(200) NULL,
    Description nvarchar(max) NULL,
    PRIMARY KEY CLUSTERED (Id)
    )
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuranceField_Insurance] on table [dbo].[InsuranceField]
--
    PRINT (N'Create foreign key [FK_InsuranceField_Insurance] on table [dbo].[InsuranceField]')
    GO
ALTER TABLE dbo.InsuranceField
    ADD CONSTRAINT FK_InsuranceField_Insurance FOREIGN KEY (InsuranceId) REFERENCES dbo.Insurance (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[InsuranceFAQ]
--
PRINT (N'Create table [dbo].[InsuranceFAQ]')
GO
CREATE TABLE dbo.InsuranceFAQ (
                                  Id bigint IDENTITY,
                                  Question nvarchar(max) NULL,
                                  Answer nvarchar(max) NULL,
                                  IsDeleted bit NOT NULL DEFAULT (0),
                                  InsuranceId bigint NOT NULL,
                                  CONSTRAINT PK_InsuranceFAQ_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuranceFAQ_InsuranceId] on table [dbo].[InsuranceFAQ]
--
    PRINT (N'Create foreign key [FK_InsuranceFAQ_InsuranceId] on table [dbo].[InsuranceFAQ]')
    GO
ALTER TABLE dbo.InsuranceFAQ
    ADD CONSTRAINT FK_InsuranceFAQ_InsuranceId FOREIGN KEY (InsuranceId) REFERENCES dbo.Insurance (Id)
    GO

--
-- Create table [dbo].[InsuranceCentralRule]
--
PRINT (N'Create table [dbo].[InsuranceCentralRule]')
GO
CREATE TABLE dbo.InsuranceCentralRule (
                                          Id bigint IDENTITY,
                                          InsuranceId bigint NOT NULL,
                                          Type tinyint NOT NULL,
                                          JalaliYear nvarchar(100) NOT NULL,
                                          GregorianYear nvarchar(100) NOT NULL,
                                          FieldType nvarchar(200) NOT NULL,
                                          FieldId varchar(50) NULL,
                                          Value nvarchar(100) NOT NULL,
                                          CreatedBy bigint NULL,
                                          CreatedAt datetime2 NULL DEFAULT (getdate()),
                                          IsCumulative bit NOT NULL DEFAULT (0),
                                          PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_InsuranceCentralRule_Insurance] on table [dbo].[InsuranceCentralRule]
--
    PRINT (N'Create foreign key [FK_InsuranceCentralRule_Insurance] on table [dbo].[InsuranceCentralRule]')
    GO
ALTER TABLE dbo.InsuranceCentralRule
    ADD CONSTRAINT FK_InsuranceCentralRule_Insurance FOREIGN KEY (InsuranceId) REFERENCES dbo.Insurance (Id) ON DELETE CASCADE ON UPDATE CASCADE
    GO

--
-- Create table [dbo].[InspectionSession]
--
PRINT (N'Create table [dbo].[InspectionSession]')
GO
CREATE TABLE dbo.InspectionSession (
                                       Id bigint IDENTITY,
                                       Name nvarchar(200) NULL,
                                       Description nvarchar(300) NULL,
                                       IsDeleted bit NOT NULL DEFAULT (0),
                                       CONSTRAINT PK_InspectionSession_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[PolicyRequestInspection]
--
    PRINT (N'Create table [dbo].[PolicyRequestInspection]')
    GO
CREATE TABLE dbo.PolicyRequestInspection (
                                             Id bigint IDENTITY,
                                             PolicyRequestId bigint NOT NULL,
                                             InspectionTypeId tinyint NULL DEFAULT (0),
                                             InspectionAddressId bigint NULL,
                                             InspectionSessionDate datetime2 NULL,
                                             CompanyCenterScheduleId bigint NULL,
                                             CreatedDateTime datetime2 NULL,
                                             InspectionSessionId bigint NULL,
                                             PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequesInspection_InspectionAddressId] on table [dbo].[PolicyRequestInspection]
--
    PRINT (N'Create foreign key [FK_PolicyRequesInspection_InspectionAddressId] on table [dbo].[PolicyRequestInspection]')
    GO
ALTER TABLE dbo.PolicyRequestInspection
    ADD CONSTRAINT FK_PolicyRequesInspection_InspectionAddressId FOREIGN KEY (InspectionAddressId) REFERENCES dbo.Address (Id)
    GO

--
-- Create foreign key [FK_PolicyRequesInspection_PolicyRequestId] on table [dbo].[PolicyRequestInspection]
--
PRINT (N'Create foreign key [FK_PolicyRequesInspection_PolicyRequestId] on table [dbo].[PolicyRequestInspection]')
GO
ALTER TABLE dbo.PolicyRequestInspection
    ADD CONSTRAINT FK_PolicyRequesInspection_PolicyRequestId FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestInspection_CompanyCenterScheduleId] on table [dbo].[PolicyRequestInspection]
--
PRINT (N'Create foreign key [FK_PolicyRequestInspection_CompanyCenterScheduleId] on table [dbo].[PolicyRequestInspection]')
GO
ALTER TABLE dbo.PolicyRequestInspection
    ADD CONSTRAINT FK_PolicyRequestInspection_CompanyCenterScheduleId FOREIGN KEY (CompanyCenterScheduleId) REFERENCES dbo.CompanyCenterSchedule (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestInspection_InspectionSessionId] on table [dbo].[PolicyRequestInspection]
--
PRINT (N'Create foreign key [FK_PolicyRequestInspection_InspectionSessionId] on table [dbo].[PolicyRequestInspection]')
GO
ALTER TABLE dbo.PolicyRequestInspection
    ADD CONSTRAINT FK_PolicyRequestInspection_InspectionSessionId FOREIGN KEY (InspectionSessionId) REFERENCES dbo.InspectionSession (Id)
    GO

--
-- Create table [dbo].[Info]
--
PRINT (N'Create table [dbo].[Info]')
GO
CREATE TABLE dbo.Info (
                          Id bigint IDENTITY,
    [Key] nvarchar(255) NULL,
    Value nvarchar(max) NULL,
    Slug nvarchar(100) NULL,
    IsDeleted bit NOT NULL DEFAULT (0),
    CONSTRAINT PK_Info_Id PRIMARY KEY CLUSTERED (Id)
    )
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create table [dbo].[FAQ]
--
    PRINT (N'Create table [dbo].[FAQ]')
    GO
CREATE TABLE dbo.FAQ (
                         Id bigint IDENTITY,
                         Question nvarchar(max) NULL,
                         Answer nvarchar(max) NULL,
                         IsDeleted bit NOT NULL DEFAULT (0),
                         CONSTRAINT PK_FAQ_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create table [dbo].[Enumeration]
--
    PRINT (N'Create table [dbo].[Enumeration]')
    GO
CREATE TABLE dbo.Enumeration (
                                 Id bigint IDENTITY,
                                 ParentId bigint NULL,
                                 CategoryName nvarchar(100) NOT NULL,
                                 CategoryCaption nvarchar(100) NOT NULL,
                                 EnumId int NOT NULL,
                                 EnumCaption nvarchar(100) NOT NULL,
    [Order] tinyint NULL DEFAULT (1),
    IsEnable tinyint NULL DEFAULT (1),
    Description text NULL,
    CreatedBy bigint NULL,
    CreatedAt datetime2 NULL DEFAULT (getdate()),
    UpdatedBy bigint NULL,
    UpdatedAt datetime2 NULL DEFAULT (getdate()),
    PRIMARY KEY CLUSTERED (Id)
    )
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_Enumeration_Enumeration] on table [dbo].[Enumeration]
--
    PRINT (N'Create foreign key [FK_Enumeration_Enumeration] on table [dbo].[Enumeration]')
    GO
ALTER TABLE dbo.Enumeration
    ADD CONSTRAINT FK_Enumeration_Enumeration FOREIGN KEY (ParentId) REFERENCES dbo.Enumeration (Id)
    GO

--
-- Create table [dbo].[Category]
--
PRINT (N'Create table [dbo].[Category]')
GO
CREATE TABLE dbo.Category (
                              Id bigint IDENTITY,
                              Name nvarchar(100) NULL,
                              Slug nvarchar(255) NULL,
                              IsDeleted bit NOT NULL DEFAULT (0),
                              CONSTRAINT PK_Category_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create table [dbo].[ArticleCategory]
--
    PRINT (N'Create table [dbo].[ArticleCategory]')
    GO
CREATE TABLE dbo.ArticleCategory (
                                     Id bigint IDENTITY,
                                     ArticleId bigint NULL,
                                     CategoryId bigint NULL,
                                     IsDeleted bit NOT NULL DEFAULT (0),
                                     CONSTRAINT PK_ArticleCategory_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_ArticleCategory_ArticleId] on table [dbo].[ArticleCategory]
--
    PRINT (N'Create foreign key [FK_ArticleCategory_ArticleId] on table [dbo].[ArticleCategory]')
    GO
ALTER TABLE dbo.ArticleCategory
    ADD CONSTRAINT FK_ArticleCategory_ArticleId FOREIGN KEY (ArticleId) REFERENCES dbo.Article (Id)
    GO

--
-- Create foreign key [FK_ArticleCategory_CategoryId] on table [dbo].[ArticleCategory]
--
PRINT (N'Create foreign key [FK_ArticleCategory_CategoryId] on table [dbo].[ArticleCategory]')
GO
ALTER TABLE dbo.ArticleCategory
    ADD CONSTRAINT FK_ArticleCategory_CategoryId FOREIGN KEY (CategoryId) REFERENCES dbo.Category (Id)
    GO

--
-- Create table [dbo].[Attachment]
--
PRINT (N'Create table [dbo].[Attachment]')
GO
CREATE TABLE dbo.Attachment (
                                Id bigint IDENTITY,
                                Code uniqueidentifier NOT NULL DEFAULT (newid()),
                                Name nvarchar(256) NULL,
                                Path nvarchar(500) NULL,
                                Extension nvarchar(100) NULL,
                                Data varbinary(max) NULL,
                                IsDeleted bit NOT NULL DEFAULT (0),
                                PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
    GO

    --
-- Create table [dbo].[PolicyRequestCommentAttachment]
--
    PRINT (N'Create table [dbo].[PolicyRequestCommentAttachment]')
    GO
CREATE TABLE dbo.PolicyRequestCommentAttachment (
                                                    Id bigint IDENTITY,
                                                    PolicyRequestCommentId bigint NULL,
                                                    AttachmentId bigint NULL,
                                                    AttachmentTypeId int NULL,
                                                    IsDeleted bit NOT NULL DEFAULT (0),
                                                    CONSTRAINT PK_PolicyRequestCommentAttachment_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequestCommentAttachment_AttachmentId] on table [dbo].[PolicyRequestCommentAttachment]
--
    PRINT (N'Create foreign key [FK_PolicyRequestCommentAttachment_AttachmentId] on table [dbo].[PolicyRequestCommentAttachment]')
    GO
ALTER TABLE dbo.PolicyRequestCommentAttachment
    ADD CONSTRAINT FK_PolicyRequestCommentAttachment_AttachmentId FOREIGN KEY (AttachmentId) REFERENCES dbo.Attachment (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestCommentAttachment_PolicyRequestCommentId] on table [dbo].[PolicyRequestCommentAttachment]
--
PRINT (N'Create foreign key [FK_PolicyRequestCommentAttachment_PolicyRequestCommentId] on table [dbo].[PolicyRequestCommentAttachment]')
GO
ALTER TABLE dbo.PolicyRequestCommentAttachment
    ADD CONSTRAINT FK_PolicyRequestCommentAttachment_PolicyRequestCommentId FOREIGN KEY (PolicyRequestCommentId) REFERENCES dbo.PolicyRequestComment (Id)
    GO

--
-- Create table [dbo].[PolicyRequestAttachment]
--
PRINT (N'Create table [dbo].[PolicyRequestAttachment]')
GO
CREATE TABLE dbo.PolicyRequestAttachment (
                                             Id bigint IDENTITY,
                                             PolicyRequestId bigint NOT NULL,
                                             AttachmentId bigint NOT NULL,
                                             Name nvarchar(100) NULL,
                                             TypeId int NOT NULL,
                                             PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PolicyRequestAttachment_Attachment] on table [dbo].[PolicyRequestAttachment]
--
    PRINT (N'Create foreign key [FK_PolicyRequestAttachment_Attachment] on table [dbo].[PolicyRequestAttachment]')
    GO
ALTER TABLE dbo.PolicyRequestAttachment
    ADD CONSTRAINT FK_PolicyRequestAttachment_Attachment FOREIGN KEY (AttachmentId) REFERENCES dbo.Attachment (Id)
    GO

--
-- Create foreign key [FK_PolicyRequestAttachment_PolicyRequest] on table [dbo].[PolicyRequestAttachment]
--
PRINT (N'Create foreign key [FK_PolicyRequestAttachment_PolicyRequest] on table [dbo].[PolicyRequestAttachment]')
GO
ALTER TABLE dbo.PolicyRequestAttachment
    ADD CONSTRAINT FK_PolicyRequestAttachment_PolicyRequest FOREIGN KEY (PolicyRequestId) REFERENCES dbo.PolicyRequest (Id)
    GO

--
-- Create table [dbo].[PersonAttachment]
--
PRINT (N'Create table [dbo].[PersonAttachment]')
GO
CREATE TABLE dbo.PersonAttachment (
                                      Id bigint IDENTITY,
                                      PersonId bigint NULL,
                                      AttachmentId bigint NULL,
                                      TypeId int NOT NULL,
                                      IsDeleted bit NOT NULL DEFAULT (0),
                                      CONSTRAINT PK_PersonAttachment_Id PRIMARY KEY CLUSTERED (Id)
)
    ON [PRIMARY]
    GO

    --
-- Create foreign key [FK_PersonAttachment_AttachmentId] on table [dbo].[PersonAttachment]
--
    PRINT (N'Create foreign key [FK_PersonAttachment_AttachmentId] on table [dbo].[PersonAttachment]')
    GO
ALTER TABLE dbo.PersonAttachment
    ADD CONSTRAINT FK_PersonAttachment_AttachmentId FOREIGN KEY (AttachmentId) REFERENCES dbo.Attachment (Id)
    GO

--
-- Create foreign key [FK_PersonAttachment_PersonId] on table [dbo].[PersonAttachment]
--
PRINT (N'Create foreign key [FK_PersonAttachment_PersonId] on table [dbo].[PersonAttachment]')
GO
ALTER TABLE dbo.PersonAttachment
    ADD CONSTRAINT FK_PersonAttachment_PersonId FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
    GO