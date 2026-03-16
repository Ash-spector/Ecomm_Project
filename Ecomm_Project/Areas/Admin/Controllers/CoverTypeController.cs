using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project.Areas.Admin.Controllers
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

            if (id == null || id == 0)
                return View(coverType);

            coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.CoverType.GetAll() });
        }
        #endregion
    }
}