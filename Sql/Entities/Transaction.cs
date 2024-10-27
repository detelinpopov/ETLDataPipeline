using Interfaces.Sql.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sql.Entities
{
    public class Transaction : ITransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
      
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string DataSource { get; set; }

        public string PaymentMethod { get; set; }

        public virtual Customer Customer { get; set; }

        public int CustomerId { get; set; }

        ICustomer ITransaction.Customer
        {
            get => Customer;
            set => Customer = value as Customer;
        }

        [Required]
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
