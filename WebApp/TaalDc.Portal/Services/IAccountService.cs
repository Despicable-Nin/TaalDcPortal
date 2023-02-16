using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public interface IAccountService
{
    Task<UserIndexViewModel> GetListOfUsers();
    Task<IEnumerable<BrokerListsViewModel>> GetBrokers();
    Task<IEnumerable<string>> GetRolesAsync();
    Task<string> CreateUser(UserCreateDto vm);
    Task<string> UpdateUser(string id, UserUpdateDto vm, bool resetPassword);
    Task<UserViewModel> GetUserById(string id);
    Task<string> GetToken(string email);
}