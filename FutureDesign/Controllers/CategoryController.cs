using AutoMapper;
using Business_Logic_Layer.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Interfaces;
using Project.DAL.Entities;
using Project.PL.Models;
using System.Threading.Tasks;

namespace Project.PL.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public IMapper Mapper { get; }


        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.Mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.CategoryRepository.GetAll().Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                
                category.Name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(category.Name.ToLower());
                var fileName = DocumentSetting.UploadFile(category.Image);
                category.ImgPath = $"http://mohamedsamy2499-001-site1.ctempurl.com/uploads/{fileName}";
                var Cat = Mapper.Map<CategoryViewModel, Category>(category);
                await unitOfWork.CategoryRepository.Add(Cat);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        #region Details
        public IActionResult Details(int? id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            Category category = unitOfWork.CategoryRepository.GetById(id).Result;
            if (category == null)
                return NotFound();
            return View(ViewName, category);
        }
        #endregion
        #region Delete
        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, Category category)
        {
            if (id != category.Id)
                return NotFound();
            try
            {
                var cat = unitOfWork.CategoryRepository.GetById(id).Result;

                await unitOfWork.CategoryRepository.Delete(cat);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Category cat = unitOfWork.CategoryRepository.GetById(id).Result;
            if (cat == null)
                return NotFound();
            return View(cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (category.Id != id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                await unitOfWork.CategoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        #endregion

    }
}
