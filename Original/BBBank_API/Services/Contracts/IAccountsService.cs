using Entities;
using Entities.RequestDTO;
using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAccountsService
    {
        Task OpenAccount(AccountRequestDTO account);
    }
}
