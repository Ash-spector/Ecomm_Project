using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project_101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null) return View(coverType);//create
                                                   //edit
            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null) return BadRequest();
            if (!ModelState.IsValid) return View(coverType);
            if (coverType.Id == 0)
            {
                _unitOfWork.CoverType.Add(coverType);
            }
            else
            {

                _unitOfWork.CoverType.Update(coverType);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        #region Apis
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.CoverType.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CoverTypeInDb = _unitOfWork.CoverType.Get(id);
            if (CoverTypeInDb == null)
            { return Json(new { success = false, message = "Unable To Delete Data !!! " }); }
            _unitOfWork.CoverType.Remove(CoverTypeInDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data Deleted Succesfully !!!" });

        }
        #endregion
    }
}