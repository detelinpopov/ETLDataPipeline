namespace Interfaces.Sql.Entities
{
    public interface ITransaction
    {
        int Id { get; set; }

        string CustomerName { get; set; }

        DateTime TransactionDate { get; set; }

        decimal Amount { get; set; }

        public string DataSource { get; set; }

        public IUser User { get; set; }
    }
}
