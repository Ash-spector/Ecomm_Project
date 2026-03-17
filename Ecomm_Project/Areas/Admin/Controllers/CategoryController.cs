using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace Ecomm_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();

            if (id == null || id == 0)
            {
                return View(category);
            }
            //edit
            category = _unitofwork.Category.Get(id.GetValueOrDefault());

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            if (category.id == 0)
                _unitofwork.Category.Add(category);
            else
                _unitofwork.Category.Update(category);
                _unitofwork.Save();

            return RedirectToAction(nameof(Index));
        }

        #region APIs

        [HttpGet]
        public IActionResult GetAll()
        {
            var CategoryList = _unitofwork.Category.GetAll();
            return Json(new { data = CategoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoryInDb = _unitofwork.Category.Get(id);
            if (categoryInDb == null)
                return Json(new { success = false, message = "Unable to delete data!" });
            _unitofwork.Category.Remove(categoryInDb);
            _unitofwork.Save();
            return Json(new { success = true, message = "Delete successful" });

            return RedirectToAction(nameof(Index));
        
        #endregion
        }
    }
}