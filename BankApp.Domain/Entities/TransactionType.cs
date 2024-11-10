﻿namespace BankApp.Domain.Entities
{
    public class TransactionType:BaseEntity
    {
        public required string Name { get; set; }
        public int ProcessTypeId { get; set; }
        public ProcessType? ProcessType { get; set; }
        public string FullName => "{Name}-({ProcessType.Symbol})";
        public ICollection<Transaction>? Transactions { get; set; }
    }
}