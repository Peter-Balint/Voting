using Microsoft.EntityFrameworkCore;
using Voting.DataAccess.Models;
using Voting.DataAccess.Exceptions;

namespace Voting.DataAccess.Services
{
    public class PollsService
    {
        private readonly VotingDbContext _context;

        public PollsService(VotingDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Poll>> GetActivePolls()
        {
            return await _context.Polls
                .OrderBy(p => p.StartsAt)
                .Where(p => p.StartsAt < DateTime.Now && p.EndsAt > DateTime.Now)
                .ToListAsync();
        }
        public async Task<Poll> GetDetailedPollById(int id)
        {
            var poll =  await _context.Polls
                .Where(p => p.Id == id)
                .Include(p => p.Owner)
                .Include(p => p.AnswerOptions)
                .Include(p => p.VotesCast)
                .FirstOrDefaultAsync();
            if(poll == null)
            {
                throw new EntityNotFoundException(nameof(poll));
            }
            return poll;
        }
    }
}
