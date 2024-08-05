using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smartfinance.Application.Apps;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Models.AccountMovement.Create;
using smartfinance.Domain.Models.AccountMovement.Model;
using smartfinance.Domain.Models.AccountMovementCategory.Create;
using smartfinance.Domain.Models.AccountMovementCategory.Model;
using System.ComponentModel.DataAnnotations;

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

        [HttpGet]
        [ProducesResponseType(typeof(OperationResult<MovementViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<MovementViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OperationResult<MovementViewModel>>> FindByRangeAsync([FromQuery]string initialDate, [FromQuery]string finalDate, [FromQuery]int skip, [FromQuery]int take, CancellationToken cancellationToken = default)
        {
            //var result = await _movementApp.FindByIdAsync(id, cancellationToken);

            //if (result?.Model == null)
            //    return NotFound();

            //return Ok(result);

            return null;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<int>>> CreateMovement([Required] MovementCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var result = await _movementApp.CreateAsync(model, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return CreatedAtAction(nameof(FindByIdAsync), new { id = result.Model }, result.Model);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<bool>>> DeleteAccount([Required, FromRoute] int id,
                                                                  CancellationToken cancellationToken = default)
        {
            var result = await _movementApp.DeleteAsync(id, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return Ok(result);
        }

    }
}
