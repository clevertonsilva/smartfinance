using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartfinance.Application.Apps;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Models.AccountMovement.Model;
using smartfinance.Domain.Models.AccountMovementCategory.Model;

namespace smartfinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly IMovementApp _movementApp;

        public MovementController(IMovementApp movementApp)
        {
            _movementApp = movementApp;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperationResult<MovementViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<MovementViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OperationResult<MovementViewModel>>> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _movementApp.FindByIdAsync(id, cancellationToken);

            if (result?.Model == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperationResult<MovementViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<MovementViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OperationResult<MovementViewModel>>> FindByRangeAsync([FromQuery]string initialDate, [FromQuery]string finalDate, CancellationToken cancellationToken = default)
        {
            //var result = await _movementApp.FindByIdAsync(id, cancellationToken);

            //if (result?.Model == null)
            //    return NotFound();

            //return Ok(result);

            return null;
        }
    }
}
