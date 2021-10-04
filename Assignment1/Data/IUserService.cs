using Models;

namespace Data {
public interface IUserService {
    User ValidateUser(string userName, string password);
}
}