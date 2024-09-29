CREATE TABLE [dbo].[Permission] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    [ApplicationId] INT NOT NULL,
    [Created] DATETIME2 NOT NULL,
    [Modified] DATETIME2 NOT NULL,
    CONSTRAINT PK_Permission PRIMARY KEY (Id),
    CONSTRAINT FK_Permission_Application FOREIGN KEY (ApplicationId) REFERENCES [dbo].[Application](Id)
);