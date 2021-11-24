using System.Threading.Tasks;
using FamilyWebAPI.Models;

namespace FamilyWebAPI.Data {
public interface IUserService {
    Task<User> ValidateUserAsync(string userName, string password);
}
}