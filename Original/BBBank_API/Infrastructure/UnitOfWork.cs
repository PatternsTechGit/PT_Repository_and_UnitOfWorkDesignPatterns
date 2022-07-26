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
    public class UnitOfWork : IUnitOfWork
    {
        //DIining DbContext here will give it an instance of BBBankContext
        protected readonly DbContext _context;
        //UOW also acting as a wrapper on all the repositories .
        // So that all the repositories can be accessed from UOW in services layer
        public IAccountRepository AccountRepository { get; }
        public IRepository<Transaction> TransactionRepository { get; }
        public IRepository<User> UserRepository { get; }

        public UnitOfWork(DbContext context, IAccountRepository accountRepository,
                         IRepository<Transaction> transactionRepositoy,
                         IRepository<User> userRepositoy)
        {
            this.AccountRepository = accountRepository;
            this.TransactionRepository = transactionRepositoy;
            this.UserRepository = userRepositoy;
            this._context = context;
        }
        // and calling SaveChangesAsync on context will persist all the changes EF is tracking as one transaction.
        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }


    }
}
