using AutoMapper;
using Project.DAL.Entities;
using Project.PL.Models;

namespace Project.PL.Mappers
{
    public class SampleProfile:Profile
    {
        public SampleProfile()
        {
            CreateMap<Sample,SampleViewModel>().ReverseMap();
        }
    }
}
