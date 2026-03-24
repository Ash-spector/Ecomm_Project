using Ecomm_Project.DataAccess.Repository.IRepository;
using Ecomm_Project.Models;
using Ecomm_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public IActionResult Upsert (ProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(files.Count()>0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var upload = Path.Combine(webRootPath, "images\\products");
                    if(productVM.Product.Id !=0)
                    {
                        var imageExists = _unitofwork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageExists;
                    }
                    if (productVM.Product.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var FileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(FileStream);
                        }
                        productVM.Product.ImageUrl = @"/images\products\" + fileName + extension;
                        
                    }
                    else
                    {
                        if (productVM.Product.Id !=0)
                        {
                            var imageExists = _unitofwork.Product.Get(productVM.Product.Id).ImageUrl;
                            productVM.Product.ImageUrl = imageExists;
                        }
                    }
                    if (productVM.Product.Id == 0)
                        _unitofwork.Product.Add(productVM.Product);
                    else 
                    
                        _unitofwork.Product.Update(productVM.Product);
                        _unitofwork.Save();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                    ProductVM vM = new ProductVM()
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
                    if(productVM.Product.Id == 0)
                    {
                        productVM.Product = _unitofwork.Product.Get(productVM.Product.Id);
                        if (productVM.Product == null) return NotFound();
                    }
                    return View(productVM);
                }
            }
            return View(productVM);
        }
       
        #region Api's
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitofwork.Product.GetAll() });
        }
        
        #endregion
    }       
}