using AutoMapper;
using Business_Logic_Layer.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Interfaces;
using Project.DAL.Entities;
using Project.PL.Models;
using System.Threading.Tasks;

namespace Project.PL.Controllers
{

    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
