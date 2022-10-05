using Project.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ISampleRepository SampleRepository { get; set; }

        public ICategoryRepository CategoryRepository { get; set; }
        public UnitOfWork( ISampleRepository sampleRepository, ICategoryRepository categoryRepository)
        {
            SampleRepository = sampleRepository;
            CategoryRepository = categoryRepository;
        }
    }
}
