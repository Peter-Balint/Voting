using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Voting.DataAccess;
using Voting.DataAccess.Models;
using Voting.WebAPI.Infrastructure;

namespace Voting.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDataAccess(builder.Configuration); //m
            builder.Services.AddAutoMapperCustom(); //m

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();

            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "MultiScheme";
                options.DefaultChallengeScheme = "MultiScheme";
            })
                .AddScheme<AuthenticationSchemeOptions, SimpleTokenHandler>("Token", null)
                .AddPolicyScheme("MultiScheme", "Multi Auth", options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        // Use token if there's an Authorization header, else use Cookies
                        /*var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))*/
                            return "Token";

                        //return "Identity.Application";
                    };
                });

            // Configure CORS policy for Blazor client
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("BlazorPolicy",
                    policy =>
                    {
                        policy.WithOrigins(builder.Configuration["BlazorUrl"]
                                           ?? throw new ArgumentNullException("BlazorUrl")) // Enable CORS for Blazor client
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            }); //m

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors("BlazorPolicy");
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope()) { //m
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var context = scope.ServiceProvider.GetRequiredService<VotingDbContext>();
                DbInitializer.Initialize(context,userManager);
            }

            app.Run();
        }
    }
}
