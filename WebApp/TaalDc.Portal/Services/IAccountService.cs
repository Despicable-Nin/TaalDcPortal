using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public interface IAccountService
{
    Task<UserIndexViewModel> GetListOfUsersWithRoles();
    Task<IEnumerable<BrokerListsViewModel>> GetBrokers();
    Task<IEnumerable<string>> GetRolesAsync();
    Task<string> CreateUser(CreateUserViewModel vm);
}