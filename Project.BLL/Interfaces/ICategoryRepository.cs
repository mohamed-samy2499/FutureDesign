using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Category> SearchByName(Func<Category, bool> func);
        IEnumerable<string> GetAllSamplesOfCatId(int? id);

    }
}
