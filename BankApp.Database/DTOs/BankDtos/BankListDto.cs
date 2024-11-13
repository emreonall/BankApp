using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Database.DTOs.BankDtos
{
    public class BankListDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IconUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
