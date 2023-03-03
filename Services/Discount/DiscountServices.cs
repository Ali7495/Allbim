using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.Discount;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class DiscountServices : IDiscountServices
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountServices(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<DiscountResultViewModel> CreateDiscount(DiscountInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Discount discount = _mapper.Map<Discount>(viewModel);

            await _discountRepository.AddAsync(discount, cancellationToken);

            return _mapper.Map<DiscountResultViewModel>(discount);
        }

        public async Task<bool> DeleteDiscount(long id, CancellationToken cancellationToken)
        {
            Discount discount = await _discountRepository.GetByIdAsync(cancellationToken, id);
            if (discount == null)
            {
                throw new BadRequestException("این تخفیف وجود ندارد");
            }

            discount.IsDeleted = true;

            await _discountRepository.UpdateAsync(discount,cancellationToken);

            return true;
        }

        public async Task<PagedResult<DiscountResultViewModel>> GetAllDiscounts(PageAbleResult pageAbleResult ,CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<Discount> discounts = await _discountRepository.GetAllDiscounts(pageAbleModel,cancellationToken);

            return _mapper.Map<PagedResult<DiscountResultViewModel>>(discounts);
        }

        public async Task<DiscountResultViewModel> GetDiscount(long id, CancellationToken cancellationToken)
        {
            Discount discount = await _discountRepository.GetByIdAsync(cancellationToken, id);
            if (discount == null)
            {
                throw new BadRequestException("این تخفیف وجود ندارد");
            }

            return _mapper.Map<DiscountResultViewModel>(discount);
        }

        public async Task<DiscountResultViewModel> UpdateDiscount(long id, DiscountInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Discount discount = await _discountRepository.GetByIdAsync(cancellationToken, id);
            if (discount == null)
            {
                throw new BadRequestException("این تخفیف وجود ندارد");
            }

            discount.InsuranceId = viewModel.InsuranceId;
            discount.PersonId = viewModel.PersonId;
            discount.InsurerId = viewModel.InsurerId;
            discount.Value = viewModel.Value;
            discount.ExpirationDateTime = viewModel.ExpirationDateTime;
            discount.IsUsed = viewModel.IsUsed;

            await _discountRepository.UpdateAsync(discount, cancellationToken);

            return _mapper.Map<DiscountResultViewModel>(discount);
        }
    }
}
