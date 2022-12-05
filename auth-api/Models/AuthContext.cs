using Microsoft.EntityFrameworkCore;

namespace AuthApi.Models;

public class AuthContext : DbContext
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
}