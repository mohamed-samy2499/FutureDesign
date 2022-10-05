using AutoMapper;
using Business_Logic_Layer.Helper;
using FutureDesign.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.BLL.Interfaces;
using Project.DAL.Entities;
using Project.PL.Models;
using System.Threading.Tasks;

namespace Project.PL.Controllers
{

    public class PortfolioController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PortfolioController> _logger;


        public PortfolioController(ILogger<PortfolioController> logger, IUnitOfWork unitOfWork)
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


    }
}
