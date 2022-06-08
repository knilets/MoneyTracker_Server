using AutoMapper;
using MoneyTracker.Logic.Currencies;
using MoneyTracker.Storage.Models.Entities;

namespace MoneyTracker.Logic.Service.Currencies;

public class CurrencyMappingProfile : Profile
{
    public CurrencyMappingProfile()
    {
        CreateMap<Currency, CurrencyDto>();
    }
}