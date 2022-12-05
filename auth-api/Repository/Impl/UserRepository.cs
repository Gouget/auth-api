using AuthApi.Models;

namespace AuthApi.Repository.Impl;

public class UserRepository : IUserRepository
{
    private readonly AuthContext _context;

    public UserRepository(AuthContext context)
    {
        _context = context;
    }

    public void Save(UserModel userModel)
    {
        _context.Add(userModel);
        _context.SaveChanges();
    }

    public void Update(UserModel userModel)
    {
        _context.Update(userModel);
        _context.SaveChanges();
    }

    public bool VerifyEmail(string email)
    {
        return _context.Users.Any(x => x.Email == email);
    }

    public bool VerifyUserById(int id)
    {
        return _context.Users.Any(x => x.Id == id);
    }

    public UserModel GetUserByEmailAndPassword(string email, string password)
    {
        return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
    }

    public UserModel GetUserById(int id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }
}
