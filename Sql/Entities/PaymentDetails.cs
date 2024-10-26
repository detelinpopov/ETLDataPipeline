using Interfaces.Sql.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sql.Entities
{
    public class PaymentDetails : IPaymentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        public DateTime? PaymentCompletedDate { get; set; }

        [Required]
        public DateTime CreatedDateUtc { get; set; }
    }
}
