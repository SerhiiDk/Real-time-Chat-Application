using ChatApplication.Shared.V1.Dtos;
using ChatApplication.Shared.V1.Models.User;

namespace ChatApplication.API.V1.Services.UserService;

public interface IUserService
{
    Task<bool> CreateUser(CreateUserModel model, CancellationToken cancellationToken);
    Task<UserDTO> Login(LoginUserModel model, CancellationToken cancellationToken);
}
