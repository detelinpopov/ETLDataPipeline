using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public class TransactionLog : ITransactionLog
    {
        public int Id { get; set; }

        public string LogType { get; set; }

        public int Severity { get; set; }

        public string Message { get; set; }

        public DateTime CreatedDateUtc { get; set; }
    }
}
