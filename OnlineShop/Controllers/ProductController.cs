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
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(long cateId, int page = 1, int pageSize = 8)
        {
            var category = new CategoryDao().ViewDetail(cateId);
            ViewBag.Category = category;
            int totalRecod = 0;
            var model = new ProductDao().ListByCategoryId(cateId, ref totalRecod, page, pageSize);
            ViewBag.Total = totalRecod;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecod / pageSize));
            ViewBag.totalPage = totalPage;
            ViewBag.maxPage = maxPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.Last = maxPage;
            ViewBag.First = 1;
            return View(model);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            return View(model);
        }
        [OutputCache (Duration =int.MaxValue, VaryByParam = "id")]
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProduct(id);
            return View(product);
        }
        public ActionResult Store(int page = 1, int pageSize = 9)
        {
            ViewBag.listpromotion = new ProductDao().ListPromotion();
            ViewBag.category = new ProductCategoryDao().ListAll();
            int totalRecord = 0;
            var product = new ProductDao().ProductPaging(ref totalRecord, page,pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.totalPage = totalPage;
            ViewBag.maxPage = maxPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.Last = maxPage;
            ViewBag.First = 1;
            return View(product);
        }
    }
}