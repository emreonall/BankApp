using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Domain.Entities
{
    public class Company:BaseEntity
    {
        public required string CompanyCode { get; set; }
        public required string CompanyName { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
