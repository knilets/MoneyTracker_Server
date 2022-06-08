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