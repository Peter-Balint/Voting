using Voting.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Voting.DataAccess
{
    public static class DbInitializer
    {
        private static VotingDbContext _context = null!;
        private static UserManager<User> _userManager = null!;

        public static void Initialize(VotingDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

            _context.Database.Migrate();

            if (!_context.Users.Any())
            {
                SeedUsers();
            }
            if (!_context.Polls.Any())
            {
                SeedPolls();
            }

        }

        private static void SeedUsers()
        {
            var user = new User
            {
                UserName = "user@example.com",
                Name = "UserExample",
                Email = "user@example.com",
                PhoneNumber = "+36123456789"
            };
            var password = "user@123";

            var result1 = _userManager.CreateAsync(user, password).Result;
        }

        private static void SeedPolls()
        {
            var Polls = new Poll[]
            {
                new Poll
                {
                    Question = "Test Question 1",
                    StartsAt = DateTime.Now,
                    EndsAt = DateTime.Now + TimeSpan.FromDays(20)
                },
                new Poll
                {
                    Question = "Test Question 2",
                    StartsAt = DateTime.Now,
                    EndsAt = DateTime.Now + TimeSpan.FromDays(30)
                }
            };

            _context.Polls.AddRange(Polls);
            _context.SaveChanges();                

            var Answers = new Answer[]
            {
                new Answer {
                    AnswerText = "Test Answer 2.1",
                    InternalAnswerId = 1,
                    PollId = Polls[1].Id
                },
                new Answer{
                    AnswerText = "Test Answer 2.2",
                    InternalAnswerId = 2,
                    PollId = Polls[1].Id
                },
                new Answer
                {
                    AnswerText = "Test Answer 1.1",
                    InternalAnswerId = 1,
                    PollId = Polls[0].Id
                },
                new Answer{
                    AnswerText = "Test Answer 1.2",
                    InternalAnswerId = 2,
                    PollId = Polls[0].Id
                }
            };

            _context.Answers.AddRange(Answers);
            _context.SaveChanges();

        }
                
        

    }
}