using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TestProviderTests.FakeDB;
using PDFMaker.Models;
using PDFMaker.Infrastructure;

namespace PDFMaker.Controllers
{
    public class PDFTestController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            UserViewModel model = new UserViewModel();
            //model.User = new TestUsersRepository().All.FirstOrDefault(m => m.UserId == 1);
            //model.Stats = new TestUserStaticsticsRepository().All.FirstOrDefault(m => m.UserId == model.User.UserId);
            //TempData["model"] = model;
            return View(model);
        }

        public ActionResult GeneratePDF()
        {
            string path = Server.MapPath("/TestResults/Result.pdf");
            UserViewModel model = (UserViewModel)TempData["model"];
            DocumentMaker maker = new DocumentMaker(path, model);
            maker.MakePDF();
            return File(path, "application/pdf", "TestResults");
        }

    }
}
