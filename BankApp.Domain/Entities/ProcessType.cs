namespace BankApp.Domain.Entities
{
    public class ProcessType : BaseEntity
    {
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public string FullName =>string.Join(" ",Name,"(",Symbol,")") ;
        public int Multiplier { get; set; } = 1;

    }
}
