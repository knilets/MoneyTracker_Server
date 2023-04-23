IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [IsShared] bit NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [Currencies] (
        [Id] int NOT NULL IDENTITY,
        [Code] nvarchar(450) NOT NULL,
        [Symbol] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Currencies] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [IdentityRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_IdentityRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [Signs] (
        [Id] int NOT NULL,
        [Symbol] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Signs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [Wallets] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [CurrencyId] int NOT NULL,
        [CreatedBy] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Wallets] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Wallets_Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currencies] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [IdentityUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserId] int NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        CONSTRAINT [PK_IdentityUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_IdentityUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [Transactions] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [WalletId] int NOT NULL,
        [CategoryId] int NOT NULL,
        [SignId] int NOT NULL,
        [Sum] float NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Transactions_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Transactions_Signs_SignId] FOREIGN KEY ([SignId]) REFERENCES [Signs] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Transactions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Transactions_Wallets_WalletId] FOREIGN KEY ([WalletId]) REFERENCES [Wallets] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE TABLE [IdentityUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_IdentityUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_IdentityUserRoles_IdentityRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_IdentityUserRoles_IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE UNIQUE INDEX [IX_Currencies_Code] ON [Currencies] ([Code]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [IdentityRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [IX_IdentityUserRoles_RoleId] ON [IdentityUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [EmailIndex] ON [IdentityUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [IX_IdentityUsers_UserId] ON [IdentityUsers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [IdentityUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [IX_Transactions_CategoryId] ON [Transactions] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [IX_Transactions_SignId] ON [Transactions] ([SignId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [IX_Transactions_UserId] ON [Transactions] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [IX_Transactions_WalletId] ON [Transactions] ([WalletId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    CREATE INDEX [IX_Wallets_CurrencyId] ON [Wallets] ([CurrencyId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    insert into [dbo].[Signs] ([Id], [Symbol]) 
    values 
          (1, N'+')
        , (2, N'-')
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    declare @dateTimeNow datetime2 = sysutcdatetime()

    insert into [dbo].[Currencies] ([Code], [Symbol], [CreatedAt])
    values 
    	  (N'EUR', N'€', @dateTimeNow)
    	, (N'USD', N'$', @dateTimeNow)
    	, (N'RUB', N'₽', @dateTimeNow)
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    declare @dateTimeNow	datetime2 = sysutcdatetime()
    declare @generalUserId	int = 0

    set identity_insert [dbo].[Users] on

    insert into [dbo].[Users] ([Id], [Name], [LastName], [CreatedAt]) 
    values 
    (
    	  @generalUserId
    	, N'General'
    	, N'User'
    	, @dateTimeNow
    )

    set identity_insert [dbo].[Users] off
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    declare @dateTimeNow		datetime2 = sysutcdatetime()
    declare @defaultCategoryId	int = 0
    declare @generalUserId		int = 0

    set identity_insert [dbo].[Categories] on

    insert into [dbo].[Categories] ([Id], [Name], [Description], [IsShared], [CreatedBy], [CreatedAt])
    values 
    (
    	  @defaultCategoryId
    	, N'Default'
    	, N'Default category of payment'
    	, 1
    	, @generalUserId
    	, @dateTimeNow
    )

    set identity_insert [dbo].[Categories] off
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221031202001_Initialization')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221031202001_Initialization', N'6.0.5');
END;
GO

COMMIT;
GO

