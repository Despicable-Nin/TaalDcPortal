using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public interface IAccountService
{
    Task<UserIndexViewModel> GetListOfUsers();
    Task<IEnumerable<BrokerListsViewModel>> GetBrokers();
    Task<IEnumerable<string>> GetRolesAsync();
    Task<string> CreateUser(CreateUserViewModel vm);
    Task<string> UpdateUser(string id, UpdateUserViewModel vm, bool resetPassword);
    Task<UserViewModel> GetUserById(string id);
}