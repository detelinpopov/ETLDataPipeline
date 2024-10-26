namespace Interfaces.Sql.Entities
{
    public class IPaymentDetails
    {
        public int Id { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime? PaymentCompletedDate { get; set; }

        public DateTime CreatedDateUtc { get; set; }
    }
}
