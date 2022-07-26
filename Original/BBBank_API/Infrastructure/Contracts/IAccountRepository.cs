using Entities;
using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<ICollection<Account>> GetAllAccountsPaginated(int pageIndex, int pageSize);
    }
}
