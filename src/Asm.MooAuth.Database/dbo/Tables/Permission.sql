CREATE TABLE [dbo].[Permission] (
    [Id] INT NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    [Created] DATETIME2 NOT NULL,
    [Modified] DATETIME2 NOT NULL
);