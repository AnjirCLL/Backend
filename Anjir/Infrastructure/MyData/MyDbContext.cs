using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Authorize;
using Domain.Entities.CRM;
using Domain.Entities.Products;
using Domain.Entities.SearchHistorys;
using Domain.Entities.Shops;
using Domain.Entities.Statistic;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.MyData;

public class MyDbContext(IConfiguration configuration, IAuthorizeService authorizeService) : DbContext
{
    private readonly string dbConnection = configuration.GetConnectionString("DefaultConnection") ?? "";
    private readonly IAuthorizeService authorizeService = authorizeService;
            

    // #region CRM
    public DbSet<Sms> Sms { get; set; } = null!;
    // #endregion

    // #region Authorize
    public DbSet<AuthorizeUser> AuthorizeUsers { get; set; } = null!;
    // #endregion

    // #region User
    public DbSet<User> Users { get; set; }
    // #endregion
    // #region Shop
    public DbSet<Shop> Shops { get; set; }
    // #endregion
    // #region Product
    public DbSet<Product> Products { get; set; }
    // #endregion
    // #region Notification
    public DbSet<Notification> Notifications { get; set; }
    // #endregion
    // #region Statistics
    public DbSet<SubscribeStatistic> SubscribeStatistics { get; set; }
    public DbSet<LikeStatistic> LikeStatistics { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; }
    public DbSet<RatingStatistic> RatingStatistics { get; set; }
    public DbSet<PhoneStatistic> PhoneStatistics { get; set; }
    public DbSet<ProfileStatistic> ProfileStatistics { get; set; }
    public DbSet<ViewStatistic> ViewStatistics { get; set; }
    // #endregion
    // #region Commentary
    public DbSet<Commentary> Commentaries { get; set; }
    // #endregion



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.EnableDetailedErrors();

        optionsBuilder.UseNpgsql(dbConnection, options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        })
        .UseSnakeCaseNamingConvention();
    }
}
