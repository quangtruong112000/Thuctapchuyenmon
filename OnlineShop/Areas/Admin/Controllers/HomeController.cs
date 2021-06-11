using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.countbenefit = new StatisticalDao().countbenefit();
            ViewBag.countrevenue = new StatisticalDao().countrevenue();
            ViewBag.counttask = new OrderDao().counttask();
            ViewBag.countfeedback = new FeedbackDao().countfeedback();
            ViewBag.listSta = new StatisticalDao().listStatistical();
            ViewBag.revenue5 = new StatisticalDao().revenue5();
            ViewBag.revenue6 = new StatisticalDao().revenue6();
            ViewBag.benefit5 = new StatisticalDao().benefit5();
            ViewBag.benefit6 = new StatisticalDao().benefit6();
            ViewBag.revenue4 = new StatisticalDao().revenue4();
            ViewBag.benefit4 = new StatisticalDao().benefit4();
            return View();
        }
    }
}