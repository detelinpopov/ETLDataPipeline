namespace Core.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }

        public required string CustomerName { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Specifies the data source of the transaction. For example API, CSV, SQL, etc.
        /// </summary>
        public string DataSource { get; set; }
  
        public PaymentDetailsModel PaymentDetails { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TransactionModel other = (TransactionModel)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
