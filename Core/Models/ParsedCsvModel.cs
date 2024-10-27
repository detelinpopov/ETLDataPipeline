using Core.Enums;

namespace Core.Models
{
    public class ParsedCsvModel
    {
        public int TransactionId { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int CustomerId { get; set; }
    }
}
