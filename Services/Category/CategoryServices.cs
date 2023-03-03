using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.Category;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResultViewModel> CreateNewCategory(CategoryInputViewModel model, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetBySlug(model.Slug,cancellationToken);
            if (category != null)
            {
                throw new BadRequestException("این slug قبلا ثبت شده است");
            }

            Category newCategory = _mapper.Map<Category>(model);

            await _categoryRepository.AddAsync(newCategory, cancellationToken);

            return _mapper.Map<CategoryResultViewModel>(newCategory);
        }

        public async Task<CategoryResultViewModel> GetCategory(string slug, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetBySlug(slug, cancellationToken);
            if (category == null)
            {
                throw new BadRequestException("این دسته از مقالات وجود ندارد");
            }

            return _mapper.Map<CategoryResultViewModel>(category);
        }
        public async Task<CategoryResultViewModel> GetCategoryById(long id, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetByIdAsync(cancellationToken,id);
            if (category == null)
            {
                throw new BadRequestException("این دسته از مقالات وجود ندارد");
            }

            return _mapper.Map<CategoryResultViewModel>(category);
        }

        public async Task<PagedResult<CategoryResultViewModel>> GetAllCategoriesPaging(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Category> categories = await _categoryRepository.GetAllCategories(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CategoryResultViewModel>>(categories);
        }
        public async Task<List<CategoryResultViewModel>> GetAllCategories(CancellationToken cancellationToken)
        {

            List<Category> categories = await _categoryRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<List<CategoryResultViewModel>>(categories);
        }

        public async Task<CategoryResultViewModel> UpdateCategory(long id, CategoryInputViewModel model, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetByIdAsync(cancellationToken, id);
            if (category == null)
            {
                throw new BadRequestException("این دسته از مقالات وجود ندارد");
            }

            category.Name = model.Name;
            category.Slug = model.Slug;

            await _categoryRepository.UpdateAsync(category, cancellationToken);

            return _mapper.Map<CategoryResultViewModel>(category);
        }

        public async Task<bool> DeleteCategory(long id, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetByIdAsync(cancellationToken, id);
            if (category == null)
            {
                throw new BadRequestException("این دسته از مقالات وجود ندارد");
            }

            category.IsDeleted = true;

            await _categoryRepository.UpdateAsync(category, cancellationToken);

            return true;
        }

        public async Task<PagedResult<CategoryResultViewModel>> GetArticleByCategorySlug(string slug, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Category category = await _categoryRepository.GetBySlug(slug, cancellationToken);
            if (category == null)
            {
                throw new BadRequestException("این دسته از مقالات وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Category> categoryArticles = await _categoryRepository.GetArticlesByCategorySlug(slug, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CategoryResultViewModel>>(categoryArticles);
        }
    }
}
