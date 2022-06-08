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