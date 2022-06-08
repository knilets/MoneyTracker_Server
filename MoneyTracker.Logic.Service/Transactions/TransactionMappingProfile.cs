using AutoMapper;
using MoneyTracker.Logic.Transactions;
using MoneyTracker.Storage.Models.Entities;
using MoneyTracker.Storage.Models.Enums;

namespace MoneyTracker.Logic.Service.Transactions;

public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<TransactionCreateUpdateRequest, Transaction>()
            .ForMember(ent => ent.CategoryId, opt =>
                opt.MapFrom(dto => dto.CategoryId ?? 0))
            .ForMember(ent => ent.SignId, opt =>
                opt.MapFrom(dto => (SignType)dto.SignId))
            .ForMember(ent => ent.Sum, opt =>
                opt.MapFrom(dto => Math.Abs(dto.Sum)))
            .ForMember(ent => ent.CreatedAt, opt =>
                opt.MapFrom(dto => dto.CreatedAt.ToUniversalTime()));

        CreateMap<Transaction, TransactionDto>()
            .ForMember(dto => dto.WalletName, opt => 
                opt.MapFrom(ent => ent.Wallet.Name))
            .ForMember(dto => dto.CategoryName, opt => 
                opt.MapFrom(ent => ent.Category.Name))
            .ForMember(dto => dto.CurrencyCode, opt => 
                opt.MapFrom(ent => ent.Wallet.Currency.Code))
            .ForMember(dto => dto.CurrencySymbol, opt => 
                opt.MapFrom(ent => ent.Wallet.Currency.Symbol))
            .ForMember(dto => dto.Sum, opt => 
                opt.MapFrom(ent => ent.SignId == SignType.Minus ? (-1) * ent.Sum : ent.Sum));
    }
}