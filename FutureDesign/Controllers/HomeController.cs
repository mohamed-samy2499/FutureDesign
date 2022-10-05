using FutureDesign.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.BLL.Interfaces;
using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FutureDesign.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(string SearchValue = "", string SearchType = "")
        {
            try
            {

                if (string.IsNullOrEmpty(SearchValue))
                {
                    var categories = unitOfWork.CategoryRepository.GetAll().Result;
                    return View(categories);
                }
                else 
                {
                        SearchValue = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(SearchValue.ToLower());

                        var Category = unitOfWork.CategoryRepository.SearchByName(E => E.Name.ToString().Contains(SearchValue));
                        return View(Category);
                }
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult Samples(int? id) 
        {
            if (id == null)
                return NotFound();
            Category cat = unitOfWork.CategoryRepository.GetById(id).Result;
            if(cat == null)
                return BadRequest();
            ViewData["Cat"] = cat.Name;
            var SamplePaths = unitOfWork.CategoryRepository.GetAllSamplesOfCatId(id);
            if (SamplePaths == null)
                return NotFound();
            return View(SamplePaths);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
