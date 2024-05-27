using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BulkyWeb.Models;
using Bulky.Data;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BulkyWeb.Area.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // GET: /<controller>/
        //change and replace the applicationdbcontext to the interface of category so here change the categoryrepository
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            //_categoryRepo = db;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategorylist = _unitOfWork.category.GetAll().ToList();
            return View(objCategorylist);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display order cannot exactly match the Name. ");
            }
            //if (category.Name.ToLower() =="test")
            //{
            //    ModelState.AddModelError("", "Test is the invalid value. ");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Add(category);
                _unitOfWork.save();
                TempData["success"] = "category create sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }



        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            Category categoryDb = _unitOfWork.category.Get(u=>u.Id==id);
            if (categoryDb ==null)
            {
                return NotFound();
            }
            return View(categoryDb);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.category.Update(category);
                _unitOfWork.category.save();
                TempData["success"] = "category update sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryDb = _unitOfWork.category.Get(u => u.Id == id);
            if (categoryDb == null)
            {
                return NotFound();
            }
            return View(categoryDb);
        }
        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _unitOfWork.category.Get(u => u.Id == id);
            if (obj== null)
            {
                return NotFound();
            }
            _unitOfWork.category.Remove(obj);
            _unitOfWork.category.save();
            TempData["success"] = "category delete sucessfully";
            return RedirectToAction("Index");
            //return View();
        }
    }
}

