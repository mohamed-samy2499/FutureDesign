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
    public class CategoryRepository : GenericRpository<Category>, ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context):base(context)
        {
            this.context = context;
        }

        public IEnumerable<string> GetAllSamplesOfCatId(int? id)
        {
            var joinResult = context.Categories.Join(context.Samples,
                category => category.Id,
                sample => sample.CategoryId,
                (category, sample) => new
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    SamplePath = sample.ImgPath
                }).Where(category => category.CategoryId == id);
            var joinList = joinResult.ToList();
            var pathArray = new List<string>();
            foreach(var row in joinList)
            {
                pathArray.Add(row.SamplePath);
            }
            return pathArray;
        }

        public IEnumerable<Category> SearchByName(Func<Category, bool> func)
        {
            return context.Categories.Where(func).ToList();
        }
    }
}
