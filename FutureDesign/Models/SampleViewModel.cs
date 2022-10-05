using Project.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Project.PL.Models
{
    public class SampleViewModel
    {
        public int Id { get; set; }

        public string ImgPath { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public IFormFile Image { get; set; }

    }
}
