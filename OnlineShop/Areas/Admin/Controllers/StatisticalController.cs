using Model.Dao;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class StatisticalController : BaseController
    {
        // GET: Admin/Statistical
        public ActionResult Index()
        {
            var dao = new StatisticalDao();
            ViewBag.listSta = dao.listStatistical();
            return View();
        }
        public ActionResult Search(string keyword)
        {
            int totalRecord = 0;
            var model = new StatisticalDao().Search(keyword, ref totalRecord);
            ViewBag.Total = totalRecord;
            ViewBag.Keyword = keyword;
            return View(model);
        }
        [HttpGet]
        public ActionResult Export()
        {
            // Gọi lại hàm để tạo file excel
            var stream = new StatisticalDao().CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=ExcelDemo.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index <img draggable="false" role="img" class="emoji" alt="😀" src="https://s0.wp.com/wp-content/mu-plugins/wpcom-smileys/twemoji/2/svg/1f600.svg" scale="0">
            return RedirectToAction("Index");
        }
    }
}