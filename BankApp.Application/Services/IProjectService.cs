using BankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Application.Services
{
    public interface IProjectService
    {
        IEnumerable<Bank> GetProjects(bool trackChanges);
    }
}
