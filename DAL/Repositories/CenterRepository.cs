using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CenterRepository : Repository<Center>, ICenterRepository
    {
        public CenterRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

           
    }
}
