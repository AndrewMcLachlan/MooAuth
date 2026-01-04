CREATE TABLE [dbo].[DataSourceValue] (
    [Id] INT NOT NULL IDENTITY(1,1),
    [DataSourceId] INT NOT NULL,
    [Key] NVARCHAR(100) NOT NULL,
    [DisplayValue] NVARCHAR(255) NOT NULL,
    [SortOrder] INT NOT NULL CONSTRAINT DF_DataSourceValue_SortOrder DEFAULT 0,
    CONSTRAINT [PK_DataSourceValue] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DataSourceValue_DataSource] FOREIGN KEY ([DataSourceId]) REFERENCES [dbo].[DataSource] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_DataSourceValue_DataSource_Key] UNIQUE ([DataSourceId], [Key])
);
