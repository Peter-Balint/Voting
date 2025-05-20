using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Voting.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Voting.DataAccess
{
    public class VotingDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public VotingDbContext(DbContextOptions<VotingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Poll> Polls { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
    }
}
