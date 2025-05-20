using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.DataAccess.Models
{
    [Owned]
    public class Answer
    {
        [Key]
        public int AnswerId {  get; set; }
        public int InternalAnswerId {  get; set; }
        public required string AnswerText { get; set; }

        [ForeignKey("Poll")]
        public required int PollId {  get; set; }
        public virtual Poll? Poll { get; set; }
    }
}
