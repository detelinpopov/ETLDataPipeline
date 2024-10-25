using Interfaces.Sql.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sql.Entities
{
    public class Transaction : ITransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string DataSource { get; set; }

        public int UserId { get; set; }
      
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
