using TaalDc.Portal.ViewModels.Users;

namespace TaalDc.Portal.Services;

public interface IAccountService
{
    Task<UserIndexViewModel> GetListOfUsersWithRoles();
    Task<IEnumerable<BrokerListsViewModel>> GetBrokers();
}