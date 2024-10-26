namespace Interfaces.Sql.Entities
{
    public interface ITransaction
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string DataSource { get; set; }

        public IPaymentDetails PaymentDetails { get; set; }
    }
}
