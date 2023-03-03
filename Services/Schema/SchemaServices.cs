using AutoMapper;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.PageAble;
using Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class SchemaServices : ISchemaServices
    {
        private readonly ISchemaRepository _schemaRepository;
        private readonly IMapper _mapper;

        public SchemaServices(ISchemaRepository schemaRepository, IMapper mapper)
        {
            _schemaRepository = schemaRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ShemaVersionViewModel>> GetAllSchema(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<SchemaVersion> result = await _schemaRepository.GetAllSchema(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<ShemaVersionViewModel>>(result);
        }
    }
}
