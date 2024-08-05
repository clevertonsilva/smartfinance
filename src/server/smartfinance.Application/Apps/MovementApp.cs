using AutoMapper;
using FluentValidation;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Common.Extensions;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Models.AccountMovement.Create;
using smartfinance.Domain.Models.AccountMovement.Model;
using smartfinance.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartfinance.Application.Apps
{
    public class MovementApp : IMovementApp
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IValidator<Movement> _movementValidator;
        private readonly IMapper _mapper;

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

        public async Task<OperationResult<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _movementRepository.FindByIdAsync(id, cancellationToken);

            if (category == null)
            {
                return null;
            }

            await _movementRepository.DeleteAsync(category);
            await _movementRepository.UnitOfWork.Commit();

            return OperationResult<bool>.Succeeded();
        }

        public Task<OperationResult<MovementViewModel>> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<IEnumerable<MovementViewModel>>> FindByRangeAsync(string initialDate, string finalDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<IEnumerable<MovementViewModel>>> FindByRangeAsync(string initialDate, string finalDate, int skip, int take, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
