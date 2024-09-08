using EventosMusicales.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public interface ICustomerRepository:IReposotoryBase<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);
    }
}
