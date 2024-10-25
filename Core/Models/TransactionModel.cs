using Interfaces.Sql.Entities;
namespace Core.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }

        public required string CustomerName { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string DataSource { get; set; }

        public IUser User { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is TransactionModel other)
            {
                return CustomerName == other.CustomerName &&
                       Amount == other.Amount &&
                       TransactionDate == other.TransactionDate;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CustomerName, Amount, TransactionDate);
        }
    }
}
