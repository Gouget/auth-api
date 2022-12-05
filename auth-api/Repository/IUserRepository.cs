using AuthApi.Models;

namespace AuthApi.Repository;

public interface IUserRepository
{
    void Save(UserModel userModel);
    void Update(UserModel userModel);
    bool VerifyEmail(string email);
    bool VerifyUserById(int id);
    UserModel GetUserByEmailAndPassword(string email, string password);
    UserModel GetUserById(int id);
}