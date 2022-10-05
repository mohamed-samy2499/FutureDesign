using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public ISampleRepository SampleRepository { get; set; }

        public ICategoryRepository CategoryRepository { get; set; }

    }
}
