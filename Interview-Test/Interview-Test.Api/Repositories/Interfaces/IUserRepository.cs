using Interview_Test.Models;

namespace Interview_Test.Repositories.Interfaces;

public interface IUserRepository
{
    dynamic GetUserById(string id);
    IEnumerable<dynamic> GetAllUsers();
    int CreateUser(UserModel user);
}