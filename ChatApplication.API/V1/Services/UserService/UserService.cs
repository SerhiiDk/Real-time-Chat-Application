using ChatApplication.API.V1.Extensions;
using ChatApplication.DataAccess.Context;
using ChatApplication.DataAccess.Entities;
using ChatApplication.Shared.V1.Dtos;
using ChatApplication.Shared.V1.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.API.V1.Services.UserService;

public class UserService : IUserService
{
    private readonly ChatDbContext _chatDbContext;

    public UserService(ChatDbContext chatDbContext)
    {
        _chatDbContext = chatDbContext;
    }

    public async Task<bool> CreateUser(CreateUserModel model, CancellationToken cancellationToken)
    {
        var userAlreadyCreated = _chatDbContext.Users
            .Where(x => x.Email == model.Email).Any();

        if (userAlreadyCreated)
            return false;

        var user = new User()
        {
            UserName = model.UserName!,
            Email = model.Email!,
            PasswordHash = model.Password!.GenerateHash()

        };

        _chatDbContext.Users.Add(user);
        await _chatDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<UserDTO> Login(LoginUserModel model, CancellationToken cancellationToken)
    {
        var test = model.Password.GenerateHash();
        var userName = await _chatDbContext.Users
            .Where(x => x.Email == model.Email)
            .Where(x => x.PasswordHash == model.Password!.GenerateHash())
            .Select(x => x.UserName)
            .FirstOrDefaultAsync(cancellationToken);


        return new UserDTO
        {
            UserName = userName
        };
    }
}
