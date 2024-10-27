namespace Interfaces.Sql.Entities
{
    public interface ITransaction
    {
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }

        public string DataSource { get; set; }

        public ICustomer Customer { get; set; }
    }
}
