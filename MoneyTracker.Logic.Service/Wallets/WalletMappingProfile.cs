using AutoMapper;
using MoneyTracker.Logic.Wallets;
using MoneyTracker.Storage.Models.Entities;

namespace MoneyTracker.Logic.Service.Wallets;

public class WalletMappingProfile : Profile
{
    public WalletMappingProfile()
    {
        CreateMap<Wallet, WalletDto>()
            .ForMember(dto => dto.CurrencyId, opt => opt.MapFrom(ent => ent.Currency.Id))
            .ForMember(dto => dto.CurrencyCode, opt => opt.MapFrom(ent => ent.Currency.Code))
            .ForMember(dto => dto.CurrencySymbol, opt => opt.MapFrom(ent => ent.Currency.Symbol));

        CreateMap<WalletCreateUpdateRequest, Wallet>();
    }
}