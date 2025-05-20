
namespace Voting.Shared.DTOs
{
    public class AnswerDto
    {
        public int AnswerId { get; init; }
        public int InternalAnswerId { get; init; }
        public required string AnswerText { get; init; }
        public int PollId {  get; init; }
    }
}
