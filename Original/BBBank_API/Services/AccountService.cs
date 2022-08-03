using AutoMapper;
using Entities;
using Entities.RequestDTO;
using Entities.Responses;
using Infrastructure;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountsService
    {
        private readonly IMapper _mapper;
        // Instead of DIing BBContext directly and perferming Data operations in Service layer
        // we will inject UOW here that will have access to all the repositories and we dont have to injext indivisual repos.
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task OpenAccount(AccountRequestDTO accountRequest)
        {
            // If the user with the same User ID is already in teh system we simply set the userId forign Key of Account with it else 
            // first we create that user and then use it's ID.

            var user = await _unitOfWork.UserRepository.GetAsync(accountRequest.User.Id);
            if (user == null)
            {
                await _unitOfWork.UserRepository.AddAsync(accountRequest.User);
                accountRequest.UserId = accountRequest.User.Id;
            }
            else
            {
                accountRequest.UserId = user.Id;
            }

            var account = _mapper.Map<Account>(accountRequest);

            // Setting up ID of new incoming Account to be created.
            account.Id = Guid.NewGuid().ToString();
            // Once User ID forigen key and Account ID Primary Key is set we add the new accoun in Accounts.
            await this._unitOfWork.AccountRepository.AddAsync(account);
            // Once everything in place we make the Database call.
            await this._unitOfWork.CommitAsync();
        }
    }
}