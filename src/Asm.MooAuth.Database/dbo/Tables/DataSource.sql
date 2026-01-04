CREATE TABLE [dbo].[DataSource] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(255) NULL,
    [DataSourceTypeId] INT NOT NULL,
    [Key] NVARCHAR(50) NOT NULL,
    [Config] NVARCHAR(4000) NULL,
    [Created] DATETIME2 NOT NULL CONSTRAINT DF_DataSource_Created DEFAULT SYSUTCDATETIME(),
    [Modified] DATETIME2 NOT NULL CONSTRAINT DF_DataSource_Modified DEFAULT SYSUTCDATETIME(),
    CONSTRAINT [PK_DataSource] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DataSource_DataSourceType] FOREIGN KEY ([DataSourceTypeId]) REFERENCES [dbo].[DataSourceType] ([Id]),
    CONSTRAINT [UQ_DataSource_Key] UNIQUE ([Key])
);
