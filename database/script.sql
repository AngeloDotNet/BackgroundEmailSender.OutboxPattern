-- IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
-- BEGIN
    -- CREATE TABLE [__EFMigrationsHistory] (
        -- [MigrationId] nvarchar(150) NOT NULL,
        -- [ProductVersion] nvarchar(32) NOT NULL,
        -- CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    -- );
-- END;
-- GO

BEGIN TRANSACTION;
GO

CREATE TABLE [EmailMessage] (
    [Id] uniqueidentifier NOT NULL,
    [RecipientEmail] nvarchar(max) NULL,
    [Subject] nvarchar(max) NULL,
    [Message] nvarchar(max) NULL,
    CONSTRAINT [PK_EmailMessage] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [EmailOutbox] (
    [Id] uniqueidentifier NOT NULL,
    [MessageId] uniqueidentifier NOT NULL,
    [EmailMessageId] uniqueidentifier NULL,
    [CreatedDate] datetime2 NOT NULL,
    [Success] bit NOT NULL,
    CONSTRAINT [PK_EmailOutbox] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmailOutbox_EmailMessage_EmailMessageId] FOREIGN KEY ([EmailMessageId]) REFERENCES [EmailMessage] ([Id])
);
GO

CREATE INDEX [IX_EmailOutbox_EmailMessageId] ON [EmailOutbox] ([EmailMessageId]);
GO

-- INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
-- VALUES (N'20230531193801_InitialMigration', N'7.0.5');
-- GO

COMMIT;
GO