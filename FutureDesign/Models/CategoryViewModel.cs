using Project.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Project.PL.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public virtual ICollection<Sample> Samples { get; set; } = new HashSet<Sample>();
        public IFormFile Image { get; set; }
    }
}
