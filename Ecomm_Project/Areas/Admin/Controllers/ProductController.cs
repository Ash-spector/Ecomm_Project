using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Ecomm_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecomm_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController (IUnitOfWork unitwork,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitwork;
            _webHostEnvironment = webHostEnvironment;
        }
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitofwork.Category.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.id.ToString()
                }),
                CoverTypeList = _unitofwork.CoverType.GetAll().Select(ct => new SelectListItem()
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()
                })
            };

            if (id == null) return View(productVM);

            productVM.Product = _unitofwork.Product.Get(id.GetValueOrDefault());

            if (productVM.Product == null) return NotFound();

            return View(productVM);
        }
        [HttpPost]
       
        #region Api's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitofwork.Product.GetAll() });
        }
        
        #endregion
    }       
}