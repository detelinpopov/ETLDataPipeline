using Interfaces.Sql.Entities;
using Sql.Entities;

namespace Sql.Mappers
{
    public static class TransactionLogMapper
    {
        public static TransactionLog ToDbTransactionLog(this ITransactionLog log)
        {
            var transactionLog = new TransactionLog();
            transactionLog.LogType = log.LogType;
            transactionLog.Severity = log.Severity;
            transactionLog.Message = log.Message;
            transactionLog.CreatedDateUtc = DateTime.UtcNow;
            return transactionLog;
        }
    }
}
