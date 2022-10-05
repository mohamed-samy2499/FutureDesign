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

    [Authorize(Roles = "Admin")]
    public class SampleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public IMapper Mapper { get; }
        public SampleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.SampleRepository.GetSamplesCategories().Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
            ViewBag.Categories = unitOfWork.CategoryRepository.GetAll().Result;

            return View();

            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SampleViewModel sample)
        {
            try
            {
            if (ModelState.IsValid)
            {
                var fileName = DocumentSetting.UploadFile(sample.Image);
                sample.ImgPath = $"http://mohamedsamy2499-001-site1.ctempurl.com/uploads/{fileName}";
                var Sample = Mapper.Map<SampleViewModel, Sample>(sample);
                await unitOfWork.SampleRepository.Add(Sample);
                return RedirectToAction("Index");
            }
                ViewBag.Categories = unitOfWork.CategoryRepository.GetAll().Result;

                return View(sample);
            }
            catch
            {
                return NotFound();
            }
        }
        #region Details
        public IActionResult Details(int? id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();
            Sample sample = unitOfWork.SampleRepository.GetById(id).Result;
            if (sample == null)
                return NotFound();
            Category cat = unitOfWork.CategoryRepository.GetById(sample.CategoryId).Result;
            ViewData["CatName"] = cat.Name;
            return View(ViewName, sample);
        }
        #endregion
        #region Delete
        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, Sample sample)
        {
            if (id != sample.Id)
                return NotFound();
            try
            {
                var cat = unitOfWork.SampleRepository.GetById(id).Result;
                DocumentSetting.DeleteFile("wwwroot/uploads/", cat.ImgPath);
                await unitOfWork.SampleRepository.Delete(cat);
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
            Sample sam = unitOfWork.SampleRepository.GetById(id).Result;
            ViewBag.Categories = unitOfWork.CategoryRepository.GetAll().Result;
            if (sam == null)
                return NotFound();
            return View(sam);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sample sample)
        {
            if (sample.Id != id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                await unitOfWork.SampleRepository.Update(sample);
                return RedirectToAction("Index");
            }
            return View(sample);
        }
        #endregion
        
    }
}
