CREATE TABLE [dbo].[ConnectorType] (
    [Id] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
    [LogoUrl] NVARCHAR(255) NULL,
    CONSTRAINT PK_ConnectorType PRIMARY KEY (Id)
);