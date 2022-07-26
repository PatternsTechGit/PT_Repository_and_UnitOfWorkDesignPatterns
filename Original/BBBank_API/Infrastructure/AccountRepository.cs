using Entities;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    // Implementation of specialized repository pattern that is inherited from SQLRepository
    // will have everything in SQLRepository plus some special cases of Datalogic 
    public class AccountRepository : SQLRepository<Account>, IAccountRepository
    {
        private DbContext _context;
        public AccountRepository(DbContext context)
            : base(context)
        {
            this._context = context;
        }

        public async Task<ICollection<Account>> GetAllAccountsPaginated(int pageIndex, int pageSize)
        {
            return await this.DbSet.Include(x => x.User)
                // first n number of records will be skpped based on pageSize and pageIndex
                // for example for pageIndex 2 of pageSize is 10 first 10 records will be skipped.
                .Skip((pageIndex) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
