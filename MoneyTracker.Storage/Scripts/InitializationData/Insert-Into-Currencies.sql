declare @dateTimeNow datetime2 = sysutcdatetime()

insert into [dbo].[Currencies] ([Code], [Symbol], [CreatedAt])
values 
	  (N'EUR', N'€', @dateTimeNow)
	, (N'USD', N'$', @dateTimeNow)
	, (N'RUB', N'₽', @dateTimeNow)