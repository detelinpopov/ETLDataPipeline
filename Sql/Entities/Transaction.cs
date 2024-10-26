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
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [MaxLength(50)]
        public string DataSource { get; set; }

        public virtual PaymentDetails PaymentDetails { get; set; }

        public int PaymentDetailsId { get; set; }

        IPaymentDetails ITransaction.PaymentDetails
        {
            get => PaymentDetails;
            set => PaymentDetails = value as PaymentDetails;
        }

        [Required]
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
