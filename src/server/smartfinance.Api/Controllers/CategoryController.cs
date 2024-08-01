using Microsoft.AspNetCore.Mvc;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Models.AccountMovementCategory.Create;
using smartfinance.Domain.Models.AccountMovementCategory.Model;
using smartfinance.Domain.Models.AccountMovementCategory.Update;
using System.ComponentModel.DataAnnotations;

namespace smartfinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApp _categoryApp;

        public CategoryController(ICategoryApp categoryApp)
        {
            _categoryApp = categoryApp;
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperationResult<CategoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<CategoryViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OperationResult<CategoryViewModel>>> FindAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _categoryApp.FindAllAsync(cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperationResult<CategoryViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(OperationResult<CategoryViewModel>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OperationResult<CategoryViewModel>>> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _categoryApp.FindByIdAsync(id, cancellationToken);

            if (result?.Model == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<int>>> CreateCategory([Required] CategoryCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var result = await _categoryApp.CreateAsync(model, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return CreatedAtAction(nameof(FindByIdAsync), new { id = result.Model }, result.Model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(OperationResult<int>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<int>>> PutAccount([Required, FromRoute] int id,
                                                                         [Required] CategoryUpdateViewModel model,
                                                                         CancellationToken cancellationToken = default)
        {
            var result = await _categoryApp.UpdateAsync(model, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(OperationResult<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OperationResult<bool>>> DeleteAccount([Required, FromRoute] int id,
                                                                         CancellationToken cancellationToken = default)
        {
            var result = await _categoryApp.DeleteAsync(id, cancellationToken);

            if (!result.Success)
                return this.UnprocessableEntity(result);

            return Ok(result);
        }
    }
}
