using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Logic.Users;
using MoneyTracker.Storage;
using MoneyTracker.Storage.Models.Entities;

namespace MoneyTracker.Logic.Service.Users;

public class UserService : IUserService
{
    private readonly MoneyTrackerContext _context;
    private readonly IMapper _mapper;

    public UserService(MoneyTrackerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateAsync(UserCreateRequest request)
    {
        var createdUser = new User
        {
            Name = request.Name,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(createdUser);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserDto?> GetAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<IList<UserDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();

        return _mapper.Map<IList<UserDto>>(users);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return;
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}