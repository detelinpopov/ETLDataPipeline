namespace Interfaces.Sql.Entities
{
    public class ICustomer
    {
        public int Id { get; set; }

        public string Name { get; set; }
      
        public DateTime CreatedDateUtc { get; set; }
    }
}
