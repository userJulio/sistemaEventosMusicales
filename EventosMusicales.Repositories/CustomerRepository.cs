using EventosMusicales.Entities;
using EventosMusicales.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosMusicales.Repositories
{
    public class CustomerRepository : ReposotoryBase<Customer>, ICustomerRepository
    {


        public CustomerRepository(AplicactionDbContext context) :base(context)
        {
           
        }
        public async Task<Customer?> GetByEmailAsync(string email)
        {
            var customer = await context.Set<Customer>().FirstOrDefaultAsync(x => x.Email == email);
           //var customer2 = await context.Set<Customer>().Where(x => x.Email == email).FirstOrDefaultAsync();
            return customer;

        }
    }
}
