using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.Model;

namespace SocialNetwork.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Сommunity> Сommunitys { get; set; }
}