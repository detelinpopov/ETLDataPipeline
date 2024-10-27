namespace Interfaces.Sql.Entities
{
    public interface ITransactionLog
    {
        public int Id { get; set; }

        public string LogType {  get; set; }

        public int Severity {  get; set; }

        public string Message { get; set; }

        public DateTime CreatedDateUtc { get; set; }
    }
}
