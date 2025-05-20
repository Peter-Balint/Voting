
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.Shared.DTOs
{
    public class VoteDto
    {
        public int Id { get; init; }
        public int AnswerId { get; init; }
        public required string VoterId { get; init; }
        public required UserDto Voter { get; init; }
        public int PollId { get; init; }
        public PollDto? Poll { get; init; }
    }
}
