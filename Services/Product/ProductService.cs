using AutoMapper;
using DAL.Contracts;
using Models.Insurance;
using Models.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using DAL.Models;
using Models.Product;
using Services.PipeLine;

namespace Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper,
            IVehicleRepository vehicleRepository, IInsuranceRepository insuranceRepository)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _insuranceRepository = insuranceRepository;
        }


    }
}