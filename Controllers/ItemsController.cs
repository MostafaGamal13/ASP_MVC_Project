using ASP.NET_MVC.Data;
using ASP.NET_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET_MVC.Controllers
{
    public class ItemsController : Controller

    {
        private readonly AppDBContext _DB;
        public ItemsController(AppDBContext DB)
        {
            _DB = DB;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _DB.Items.ToList();
            return View(itemsList);
        }
        public void createSelectList(int selectId = 1)
        {
            //هنا بستدعي العناصر اللي جوا الجدول وبحولها لي ليست 
            List<Category> categories = _DB.Categories.ToList();
            SelectList listItems = new SelectList(categories, "Id", "Name", selectId);
            ViewBag.CategoryList = listItems;
        }
        //get method = show method
        public IActionResult New()
        {
            createSelectList();
            return View();
        }
        //post method = save data method 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            // ModelState.AddModelError("name", "Name is not valied");
            //check if data user type is valid 
            if (ModelState.IsValid)
            {
                _DB.Items.Add(item);
                _DB.SaveChanges();
                TempData["OprationNewSuccsess"] = "New Item successfully Created.";
                //if data is valid will return the index 
                return RedirectToAction("Index");
            }
            else
            {
                //if not will sta on the same page and the data user type will remain in textboxs
                return View(item);
            }
        }
        // ----------------------------
        // Edit Get method = show method
        public IActionResult Edit(int? Id)
            {
                if (Id == null)
                {
                    return NotFound();
                }
                var item = _DB.Items.Find(Id);
                if (item == null)
                {
                    return NotFound();
                }
            createSelectList(item.CategoryId);
                return View(item);
            }
            //Edit post method = save data method 

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Item item)
            {
                // ModelState.AddModelError("name", "Name is not valied");
                //check if data user type is valid 
                if (ModelState.IsValid)
                {
                    _DB.Items.Update(item);
                    _DB.SaveChanges();
                TempData["OprationEditSuccsess"] = "Item successfully edited.";

                //if data is valid will return the index 
                return RedirectToAction("Index");
                }
                else
                {
                    //if not will sta on the same page and the data user type will remain in textboxs
                    return View(item);
                }
            }
        // ----------------------------
        // delete Get method = show method
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _DB.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            createSelectList((int)item.CategoryId);
            return View(item);
        }
        //delete post method = save data method 

        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _DB.Items.Find(Id);
            _DB.Items.Remove(item);
            _DB.SaveChanges();
            TempData["OprationDeleteSuccsess"] = "Item successfully deleted.";

            return RedirectToAction("Index");


        }
    }
}

