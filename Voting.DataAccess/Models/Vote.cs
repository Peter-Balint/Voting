using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.DataAccess.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        public int AnswerId { get; set; }

        [ForeignKey("User")]
        public required string VoterId {  get; set; }
        public virtual User? Voter { get; set; }

        [ForeignKey("Poll")]
        public int? PollId { get; set; }
        public virtual Poll? Poll { get; set; }
    }
}
