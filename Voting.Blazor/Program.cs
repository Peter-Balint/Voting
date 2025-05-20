using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Voting.Blazor.Model;

namespace Voting.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient 
            {
                BaseAddress = new Uri(builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress)
            });

            builder.Services.AddScoped<IVotingPersistence,VotingPersistence>();
            builder.Services.AddScoped<IVotingModel,VotingModel>();
            

            await builder.Build().RunAsync();
        }
    }
}
