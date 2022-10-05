using AutoMapper;
using Project.DAL.Entities;
using Project.PL.Models;

namespace Project.PL.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryViewModel>().ReverseMap();
        }
    }
}
