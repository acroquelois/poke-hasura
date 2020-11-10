using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Skeleton.Domain.Models.Core;

namespace Skeleton.Domain.Models.Users
{
    public class User : BaseEntity<int>
    {
        [StringLength(50)]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}