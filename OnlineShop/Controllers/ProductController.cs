using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(long cateId)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            var model = new ProductDao().ListByCategoryId(cateId);
            return View(model);
        }
        /*public ActionResult Search(string keyword)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord);
            ViewBag.Total = totalRecord;
            ViewBag.Keyword = keyword;

            return View(model);
        }*/
        [OutputCache (Duration =int.MaxValue, VaryByParam = "id")]
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProduct(id);
            return View(product);
        }
        public ActionResult Store()
        {
            var product = new ProductDao().ListAll();
            return View(product);
        }
    }
}