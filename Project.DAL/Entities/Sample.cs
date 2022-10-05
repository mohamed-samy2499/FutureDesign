using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

namespace Project.DAL.Entities
{
    public class Sample
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }

        //public IFormatFile image { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
