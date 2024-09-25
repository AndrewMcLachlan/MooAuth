CREATE TABLE [dbo].[Application] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    [LogoUrl] NVARCHAR(255) NULL,
    [Created] DATETIME2 NOT NULL,
    [Modified] DATETIME2 NOT NULL,
    CONSTRAINT PK_Application PRIMARY KEY (Id)
);