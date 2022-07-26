using Entities;
using Entities.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace BBBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost]
        [Route("OpenAccount")]
        public async Task<ActionResult> OpenAccount(AccountRequestDTO accountRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _accountsService.OpenAccount(accountRequest);
                    return new OkObjectResult("New Account Created.");
                }
                else
                {
                    return new BadRequestObjectResult(ModelState.Values);
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
