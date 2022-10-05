using Microsoft.EntityFrameworkCore;
using Project.BLL.Interfaces;
using Project.DAL.Contexts;
using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Repositories
{
    public class SampleRepository: GenericRpository<Sample>,ISampleRepository
    {
        private readonly AppDbContext context;

        public SampleRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Sample>> GetSamplesCategories()
        {
            return await context.Samples.Include(E => E.Category).ToListAsync();
        }
    }
}
