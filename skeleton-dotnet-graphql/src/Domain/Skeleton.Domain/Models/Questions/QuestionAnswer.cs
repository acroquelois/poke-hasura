using Skeleton.Domain.Models.Core;

namespace Skeleton.Domain.Models
{
    public class QuestionAnswer: BaseEntity<int>
    {
        public string Libelle { get; set; }
    }
}