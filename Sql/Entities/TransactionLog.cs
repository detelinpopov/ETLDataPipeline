using Interfaces.Sql.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sql.Entities
{
    public class TransactionLog : ITransactionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string LogType { get; set; }

        public int Severity { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedDateUtc { get; set; }
    }
}
