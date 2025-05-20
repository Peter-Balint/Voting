
using System.ComponentModel.DataAnnotations;

namespace Voting.Shared.DTOs
{
    public class PollDto
    {
        public int Id { get; init; }
        public required string Question { get; init; }
        public required List<AnswerDto> AnswerOptions { get; init; }
        public DateTime StartsAt { get; init; }
        public DateTime? EndsAt { get; init; }
        public string? OwnerId { get; init; }
        public UserDto? Owner { get; init; }
        public List<VoteDto> VotesCast { get; init; } = [];
    }
}
