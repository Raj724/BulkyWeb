using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }


        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int ? id)
        {
            if (id!=null && id!=0)
            {
                Category = _db.categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            Category? obj = _db.categories.Find(Category.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "category delete sucessfully";
            return RedirectToPage("Index");
        }
    }
}
