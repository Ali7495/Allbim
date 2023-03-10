
CREATE TABLE dbo.SystemErrorLog (
                                    ID bigint IDENTITY,
                                    CreatedDateTime datetime NOT NULL DEFAULT (getdate()),
                                    ServiceName nvarchar(300) NULL,
                                    RequestUrl nvarchar(max) NULL,
                                    RequestQueryParams nvarchar(max) NULL,
                                    RequestPayload nvarchar(max) NULL,
                                    ExceptionStr nvarchar(max) NULL,
                                    AuthenticatedUser bigint NULL,
                                    Method nvarchar(max) NULL,
                                    CONSTRAINT PK_SystemErrorLog PRIMARY KEY CLUSTERED (ID)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]



CREATE TABLE dbo.OperationLog (
                                  ID bigint IDENTITY,
                                  CreateDateTime datetime NOT NULL,
                                  MethodName nvarchar(200) NOT NULL,
                                  ServiceName nvarchar(200) NOT NULL,
                                  FormBodyParameters nvarchar(max) NULL,
                                  ExecuteTime nvarchar(50) NULL,
                                  StatusCode nvarchar(50) NULL,
                                  RequestUrl nvarchar(max) NULL,
                                  QueryStringParameters nvarchar(max) NULL,
                                  Response nvarchar(max) NULL,
                                  AuthenticatedUser varchar(50) NULL,
                                  CONSTRAINT PK_OperationLog PRIMARY KEY CLUSTERED (ID)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]

CREATE TABLE dbo.HandledErrorLog (
                                     ID bigint IDENTITY,
                                     ServiceName nvarchar(300) NULL,
                                     MethodName nvarchar(300) NULL,
                                     CreatedDateTime datetime NOT NULL DEFAULT (getdate()),
                                     ErrorCode nvarchar(100) NULL,
                                     ErrorMessage nvarchar(300) NULL,
                                     RequestUrl nvarchar(max) NULL,
                                     RequestQueryParams nvarchar(max) NULL,
                                     RequestPayload nvarchar(max) NULL,
                                     AuthenticatedUser bigint NULL,
                                     CONSTRAINT PK_HandledErrorLog PRIMARY KEY CLUSTERED (ID)
)
    ON [PRIMARY]
    TEXTIMAGE_ON [PRIMARY]
