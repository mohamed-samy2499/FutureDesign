using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Contexts
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Data Source=SQL5104.site4now.net;Initial Catalog=db_a8cf6a_futuredesign;User Id=db_a8cf6a_futuredesign_admin;Password=Jhong_2499");
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
