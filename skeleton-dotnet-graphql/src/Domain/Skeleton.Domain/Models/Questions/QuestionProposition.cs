using System.ComponentModel.DataAnnotations.Schema;
using Skeleton.Domain.Models.Core;

namespace Skeleton.Domain.Models
{
    public class QuestionProposition: BaseEntity<int>
    {
        public string Libelle { get; set; }
        
        [ForeignKey("QuestionId")] 
        public int QuestionId { get; set; }
        
        public Question Questsion { get; set; }
    }
}