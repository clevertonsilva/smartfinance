﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Interfaces.Services.Authentication;
using smartfinance.Domain.Models.Account.Create;
using smartfinance.Domain.Models.Account.Model;
using smartfinance.Domain.Models.Account.Update;
using smartfinance.Domain.Models.Authentication.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace smartfinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountApp _accountApp;
        private readonly IIdentityUserService _identityUserService;

        public AccountController(IAccountApp accountApp, IIdentityUserService identityUserService)
        {
            _accountApp = accountApp;
            _identityUserService = identityUserService;
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(OperationResult<AccountViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<AccountViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OperationResult<AccountViewModel>>> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _accountApp.FindByIdAsync(id, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<int>>> CreateAccount([Required] AccountCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var result = await _accountApp.CreateAsync(model, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return CreatedAtAction(nameof(FindByIdAsync), new { id = result.Model }, result.Model);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<int>>> PutAccount([Required] int id,
                                                                         [Required] AccountUpdateViewModel model,
                                                                         CancellationToken cancellationToken = default)
        {
            var result = await _accountApp.UpdateAsync(id, model, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status400BadRequest)]  
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<bool>>> DeleteAccount([Required] int id,
                                                                         CancellationToken cancellationToken = default)
        {
            var result = await _accountApp.DeleteAsync(id, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return Ok(result);
        }

        [ProducesResponseType(typeof(OperationResult<IdentityUserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<ActionResult<OperationResult<IdentityUserViewModel>>> Login(LoginViewModel model)
        {
            var result = await _identityUserService.Login(model);
            
            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(typeof(OperationResult<IdentityUserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("refresh-login")]
        public async Task<ActionResult<OperationResult<IdentityUserViewModel>>> RefreshToken()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (userId == null)
                return BadRequest();

            var result = await _identityUserService.LoginWithouPassword(userId);

            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(typeof(OperationResult<IdentityUserViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("confirm-email")]
        public async Task<ActionResult<OperationResult<IdentityUserViewModel>>> ConfirmEmail(ConfirmEmailViewModel model)
        {
            var result = await _identityUserService.ConfirmEmail(model);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("forgot-password")]
        public async Task<ActionResult<OperationResult<string>>> ForgotPassword(ForgotPasswordViewModel model)
        {
            var result = await _identityUserService.ForgotPassword(model);

            if (!result.Success)
                return BadRequest(result);

            return Ok();
        }

        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("reset-password")]
        public async Task<ActionResult<OperationResult<bool>>> ResetPassword(ResetPasswordViewModel model)
        {
            var result = await _identityUserService.ResetPassword(model);

            if (!result.Success)
                return BadRequest(result);

            return Ok();
        }
    }
}
