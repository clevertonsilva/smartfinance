using AutoMapper;
using FluentValidation;
using smartfinance.Application.Interfaces;
using smartfinance.Domain.Common;
using smartfinance.Domain.Common.Extensions;
using smartfinance.Domain.Entities;
using smartfinance.Domain.Interfaces.Repositories;
using smartfinance.Domain.Models.AccountMovementCategory.Create;
using smartfinance.Domain.Models.AccountMovementCategory.Model;
using smartfinance.Domain.Models.AccountMovementCategory.Update;
using smartfinance.Domain.Models.Shared;

namespace smartfinance.Application.Apps
{
    public class CategoryApp : ICategoryApp
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<Category> _categoryValidator;
        private readonly IMapper _mapper;

        public CategoryApp(ICategoryRepository categoryRepository,
            IValidator<Category> categoryValidator,
            IMapper mapper
            )
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
            _mapper = mapper;
        }

        public async Task<OperationResult<int>> CreateAsync(CategoryCreateViewModel model, CancellationToken cancellationToken = default)
        {
            var category = _mapper.Map<Category>(model);

            var validateResult = await _categoryValidator.ValidateAsync(category, cancellationToken);

            if (!validateResult.IsValid)
            {
                return OperationResult<int>.Failed(validateResult.AsReponseErrors());
            }

            await _categoryRepository.AddAsync(category, cancellationToken);
            await _categoryRepository.UnitOfWork.Commit();

            return OperationResult<int>.Succeeded(category.Id);
        }

        public async Task<OperationResult<bool>?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.FindByIdAsync(id, cancellationToken);

            if (category == null)
            {
                return null;
            }
  
            await _categoryRepository.DeleteAsync(category);
            await _categoryRepository.UnitOfWork.Commit();

            return OperationResult<bool>.Succeeded();
        }

        public async Task<OperationResult<PagedListViewModel<CategoryViewModel>>> FindAllAsync(int accountId, string? searchNameTerm, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.FindAllAsync(searchNameTerm, sortOrder, page, pageSize, cancellationToken);

            var model = new PagedListViewModel<CategoryViewModel>();

            if (!categories.Any())
            {
                OperationResult<PagedListViewModel<CategoryViewModel>>.Succeeded(model);
            }

            model = new PagedListViewModel<CategoryViewModel>(_mapper.Map<List<CategoryViewModel>>(categories), page, pageSize, categories.Count());

            return OperationResult<PagedListViewModel<CategoryViewModel>>.Succeeded(model);

        }

        public async Task<OperationResult<CategoryViewModel>?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.FindByIdAsync(id, cancellationToken);

            if (category == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<CategoryViewModel>(category);

            return OperationResult<CategoryViewModel>.Succeeded(viewModel);
        }

        public async Task<OperationResult<bool>?> UpdateAsync(CategoryUpdateViewModel model, CancellationToken cancellationToken = default)
        {
            var account = await _categoryRepository.FindByIdAsync(model.Id, cancellationToken);

            if (account == null)
            {
                return null;
            }

            account = _mapper.Map<Category>(model);

            var result = await _categoryValidator.ValidateAsync(account, cancellationToken);

            if (!result.IsValid)
            {
                return OperationResult<bool>.Failed(result.AsReponseErrors());
            }

            await _categoryRepository.DeleteAsync(account);
            await _categoryRepository.UnitOfWork.Commit();

            return OperationResult<bool>.Succeeded();
        }
    }
}
