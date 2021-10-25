using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blogger.Models;
using Blogger.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly BlogsService _blogsService;


        public AccountController(AccountService accountService, BlogsService blogsService)
        {
            _accountService = accountService;
            _blogsService = blogsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{blogs}")]
        [Authorize]
        public async Task<ActionResult<List<Blog>>> GetBlogsByAccount()
        {
            try
            {
              Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
              return Ok(_blogsService.GetBlogsByAccount(userInfo.Id));
            }
            catch (System.Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<Account>> Update(string accountEmail, [FromBody] Account accountData)
    {
      try
      {
       Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       accountEmail = userInfo.Email;
        var myAccount = _accountService.Edit(accountData, accountEmail);
       return Ok(myAccount);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    }


}