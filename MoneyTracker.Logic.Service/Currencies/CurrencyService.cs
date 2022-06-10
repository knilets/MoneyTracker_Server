using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Logic.Currencies;
using MoneyTracker.Storage;

namespace MoneyTracker.Logic.Service.Currencies;

public class CurrencyService : ICurrencyService
{
    private readonly MoneyTrackerContext _context;
    private readonly IMapper _mapper;

    public CurrencyService(MoneyTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<CurrencyDto>> GetAllAsync()
    {
        var currencies = await _context.Currencies.ToListAsync();

        return _mapper.Map<IList<CurrencyDto>>(currencies);
    }

    public async Task<CurrencyDto> GetAsync(int id)
    {
        var currency = await _context.Currencies.FindAsync(id)
                       ?? throw new KeyNotFoundException($"Currency with Id '{id}' not found.");

        return _mapper.Map<CurrencyDto>(currency);
    }
}