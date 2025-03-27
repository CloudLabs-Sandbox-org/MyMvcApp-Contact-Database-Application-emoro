using MyMvcApp.Models;

namespace MyMvcApp.Services;

public interface IUserService
{
    User GetUserById(int id);
    bool CreateUser(User user);
    bool UpdateUser(int id, User user);
    bool DeleteUser(int id);
}