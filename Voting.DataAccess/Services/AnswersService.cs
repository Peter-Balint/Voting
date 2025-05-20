using Voting.DataAccess.Models;
using Voting.DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace Voting.DataAccess.Services
{
    public class AnswersService
    {
        private readonly VotingDbContext _context;

        public AnswersService(VotingDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Answer>> GetAllAnswers()
        {
            var answers = await _context.Answers
                .ToListAsync();
            return answers;
        }

        public async Task<IReadOnlyCollection<Answer>> GetAnswersByPollId(int id)
        {
            var answers = await _context.Answers
                .Where(a => a.PollId == id)
                .OrderBy(a => a.InternalAnswerId)
                .ToListAsync();

            if(answers.Count == 0)
            {
                throw new EntityNotFoundException(nameof(answers));
            }
            return answers;
        }
    }
}
