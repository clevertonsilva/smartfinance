using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Models.Account.Create;
using smartfinance.Domain.Models.Account.Model;
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

        public AccountController(IAccountApp accountApp)
        {
            _accountApp = accountApp;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperationResult<AccountViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<AccountViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OperationResult<AccountViewModel>>> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _accountApp.FindByIdAsync(id, cancellationToken);

            if (result?.PayLoad == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<int>>> CreateAccount([Required] AccountCreateViewModel model, CancellationToken cancellationToken = default)
        {
            //var identity = HttpContext.GetRequestIdentity();


            var result = await _accountApp.CreateAsync(model, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return CreatedAtAction(nameof(FindByIdAsync), new { id = result.PayLoad }, result.PayLoad);
        }

        //[ProducesResponseType(typeof(UsuarioCadastroResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<ActionResult<IdentityUserViewModel>> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var resultado = await _identityService.Login(usuarioLogin);
            //if (resultado.Sucesso)
            //    return Ok(resultado);

            return Unauthorized();
        }

        //[ProducesResponseType(typeof(UsuarioCadastroResponse), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("refresh-login")]
        public async Task<ActionResult<IdentityUserViewModel>> RefreshLogin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
                return BadRequest();

            //var resultado = await _identityService.LoginSemSenha(usuarioId);
            //if (resultado.Sucesso)
            //    return Ok(resultado);

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("confirm-email")]
        public async Task<ActionResult<IdentityUserViewModel>> ConfirmEmail(ConfirmEmailViewModel model)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuarioId == null)
                return BadRequest();

            //var resultado = await _identityService.LoginSemSenha(usuarioId);
            //if (resultado.Sucesso)
            //    return Ok(resultado);

            return Unauthorized();
        }
    }
}
