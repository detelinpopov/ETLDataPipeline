using Interfaces.Sql.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sql.Entities
{
    public class User : IUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; } = new List<Transaction>();
    }
}
