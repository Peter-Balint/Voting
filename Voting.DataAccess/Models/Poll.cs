using System.ComponentModel.DataAnnotations;

namespace Voting.DataAccess.Models
{
    public class Poll
    {
        [Key]
        public int Id { get; set; }

        public required string Question {  get; set; }

        [MinLength(2)]
        public virtual ICollection<Answer> AnswerOptions { get; set; } = [];

        public DateTime StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public string? OwnerId { get; set; } = null;
        public virtual User? Owner { get; set; }
        public virtual ICollection<Vote> VotesCast { get; set; } = [];
    }

}
