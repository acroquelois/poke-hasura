using Skeleton.Domain.Models.Core;

namespace Skeleton.Domain.Models
{
    public class QuestionCategorie: BaseEntity<int>
    {
        public string Libelle { get; set; }
    }
}