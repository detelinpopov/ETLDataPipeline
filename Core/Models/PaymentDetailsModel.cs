namespace Core.Models
{
    public class PaymentDetailsModel
    {
        public required string PaymentMethod { get; set; }

        public DateTime? PaymentCompletedDate { get; set; }
    }
}
