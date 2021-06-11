using Model.Dao;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminDao();
                var result = dao.Login(model.UserName, Encryptor.GetMD5(model.Password));
                if (result == 1)
                {
                    var admin = dao.GetById(model.UserName);
                    var adminSesstion = new UserLogin();
                    adminSesstion.UserName = admin.UserName;
                    adminSesstion.UserID = admin.ID;
                    adminSesstion.GroupID = admin.GroupID;
                    adminSesstion.Name = admin.Name;
                    var listCredentials = dao.GetListCredential(model.UserName);
                    Session.Add(CommonConstants.ADMIN_SESSION, listCredentials);
                    Session.Add(CommonConstants.ADMIN_SESSION, adminSesstion);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập.");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng!");
                }
            }
            return View("Index");
        }
    }
}