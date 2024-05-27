using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models;
using Bulky.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkyWeb.Area.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        // GET: /<controller>/
        //change and replace the applicationdbcontext to the interface of category so here change the categoryrepository
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            //_categoryRepo = db;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objCategoryproductlist = _unitOfWork.Product.GetAll().ToList();
            
            return View(objCategoryproductlist);
        }

        public IActionResult Upsert(int? id) //update and insert for product the in the same method
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                        Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id==null && id==0)
            {
                //insert the product
                return View(productVM);
            }
            else
            {
                //update the product
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.save();
                TempData["success"] = "category create sucessfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }
                return View(productVM);
        }
        
        //public IActionResult Edit(int id)
        //{
        //    if (id==null || id==0)
        //    {
        //        return NotFound();
        //    }
        //    Product productDb = _unitOfWork.Product.Get(u=>u.Id==id);
        //    if (productDb ==null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(product);
        //        _unitOfWork.Product.save();
        //        TempData["success"] = "category update sucessfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product productDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productDb == null)
            {
                return NotFound();
            }
            return View(productDb);
        }
        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj== null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Product.save();
            TempData["success"] = "category delete sucessfully";
            return RedirectToAction("Index");
            //return View();
        }
    }
}


