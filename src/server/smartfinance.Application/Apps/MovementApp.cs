using AutoMapper;
using FluentValidation;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Common.Extensions;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Models.AccountMovement.Create;
using smartfinance.Domain.Models.AccountMovement.Model;
using smartfinance.Domain.Models.Shared;

namespace smartfinance.Application.Apps
{
    public class MovementApp : IMovementApp
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IValidator<Movement> _movementValidator;
        private readonly IMapper _mapper;

        public MovementApp(IMovementRepository movementRepository, 
            IValidator<Movement> movementValidator,
            IMapper mapper)
        {
            _mapper = mapper;
            _movementValidator = movementValidator;
            _movementRepository = movementRepository;
        }

        public async Task<OperationResult<int>> CreateAsync(MovementCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var movement = _mapper.Map<Movement>(model);

            var validateResult = await _movementValidator.ValidateAsync(movement, cancellationToken);

            if (!validateResult.IsValid)
            {
                return OperationResult<int>.Failed(validateResult.AsReponseErrors());
            }

            await _movementRepository.AddAsync(movement, cancellationToken);
            await _movementRepository.UnitOfWork.Commit();

            return OperationResult<int>.Succeeded(movement.Id);

        }

        public async Task<OperationResult<bool>?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var movement = await _movementRepository.FindByIdAsync(id, cancellationToken);

            if (movement == null)
            {
                return null;
            }

            await _movementRepository.DeleteAsync(movement);
            await _movementRepository.UnitOfWork.Commit();

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<PagedListViewModel<MovementViewModel>>> FindAllAsync(int accountId, string? searchDescriptionTerm, string? initialDate, string? finalDate, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var movements = await _movementRepository.FindAllAsync(accountId, searchDescriptionTerm, initialDate, finalDate, page, pageSize, cancellationToken);

            var model = new PagedListViewModel<MovementViewModel>();

            if (!movements.Any())
            {
                OperationResult<PagedListViewModel<MovementViewModel>>.Succeeded(model);
            }

            model = new PagedListViewModel<MovementViewModel>(_mapper.Map<IEnumerable<MovementViewModel>>(movements), page, pageSize, movements.Count());

            return OperationResult<PagedListViewModel<MovementViewModel>>.Succeeded(model);
        }

        public async Task<OperationResult<MovementViewModel>?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var movement = await _movementRepository.FindByIdAsync(id, cancellationToken);

            if (movement == null)
            {
                return null;
            }

            var model = _mapper.Map<MovementViewModel>(movement);

            return OperationResult<MovementViewModel>.Succeeded(model);
        }
    }
}
