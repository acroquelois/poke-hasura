using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skeleton.Domain.Models.Core
{
    public interface IBaseEntity
    {
    }
    
    public abstract class BaseEntity<TKey> : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }
    }
}