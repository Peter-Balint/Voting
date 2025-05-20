using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Voting.DataAccess.Services;
using Microsoft.AspNetCore.Identity;
using Voting.DataAccess.Models;

namespace Voting.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<VotingDbContext>(options => options
                .UseSqlServer(connectionString)
            );

            services.AddTransient<PollsService>();
            services.AddTransient<AnswersService>();
            services.AddTransient<UsersService>();

            services.AddIdentity<User,IdentityRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<VotingDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
