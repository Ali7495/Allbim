﻿using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IInsuranceFAQRepository : IRepository<InsuranceFaq>
    {
        Task<PagedResult<InsuranceFaq>> GetAllFAQ(long insuranceId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<InsuranceFaq> GetByIdNoTracking(long faqId, CancellationToken cancellationToken);
    }
}
