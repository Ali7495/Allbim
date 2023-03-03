using AutoMapper;
using Common.Exceptions;
using DAL.Contracts;
using DAL.Models;
using Models.Center;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class CenterService : ICenterService
    {

        #region Fields

        private readonly IRepository<Center> _repository;
        private readonly ICenterRepository _centerRepository;
        private readonly IMapper _mapper;

        #endregion

        #region CTOR

        public CenterService(IRepository<Center> repository,  ICenterRepository centerRepository, IMapper mapper)
        {
            _repository = repository;
            _centerRepository = centerRepository;
            _mapper = mapper;
        }

        #endregion

        #region Get
        public async Task<List<CenterResultViewModel>> GetAll( CancellationToken cancellationToken)
        {
            var model = await _centerRepository.GetAllAsync(cancellationToken);
            if (model == null)
                throw new CustomException("center");
            return _mapper.Map<List<CenterResultViewModel>>(model);
        }

        #endregion


        #region Create

        public async Task<CenterResultViewModel> Create(CenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Center model = new Center()
            {
                Name = viewModel.Name,
                CityId= viewModel.CityId,
                Description= viewModel.Description
            };
            await _repository.AddAsync(model, cancellationToken);
            return _mapper.Map<CenterResultViewModel>(model);
        }



        #endregion

        #region Update

        public async Task<CenterResultViewModel> Update(long id, CenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            
                Center model = await _repository.GetByIdAsync(cancellationToken, id);
                if (model == null)
                    throw new CustomException("center");

                model.Name = viewModel.Name;
                model.CityId = viewModel.CityId;
                model.Description = viewModel.Description;

                await _repository.UpdateAsync(model, cancellationToken);
                return _mapper.Map<CenterResultViewModel>(model);
         
        
        }

       

        #endregion

        #region Delete

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            Center model = await _repository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("center");
            await _repository.DeleteAsync(model, cancellationToken);
            return true;
        }

        public async Task<CenterResultViewModel> Get(long id, CancellationToken cancellationToken)
        {
            var model =await  _repository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("center");
            return _mapper.Map<CenterResultViewModel>(model);
        }



        #endregion


    }
}
