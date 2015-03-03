using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCV.Models.Views;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

namespace ITCV.Controllers
{
    public class CvController : Controller
    {
        public ActionResult MakeCV()
        {
            ViewBag.UnderEdit = false;
            return View();
        }

        public ActionResult Education()
        {


            List<string> valuesForList = new List<string>() { "uni1", "uni2" };

            SelectList list = new SelectList(valuesForList);

            ViewBag.UniList = list;
            return PartialView("CVPartials/_EducationOverview");


        }

        public ActionResult ReturnFaculties(string name)
        {


            List<string> faculties = new List<string>() { "fac1", "fac2" };

            return Json(faculties, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Finish(CV cv)
        {
            try
            {
                //TempData["CV"] = cv;
                BinaryFormatter sr = new BinaryFormatter();
                FileStream stream = new FileStream(Server.MapPath("~/cv.dat"), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                sr.Serialize(stream, cv);
                stream.Close();
                

            }

            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return Content("OK", "text/plain");
        }

        public ActionResult ShowCV()
        {

            CV cv = new CV();
            cv.DateAdded = new DateTime(2000, 1, 1);
            cv.Education.Add(new Education() { EduBegin = new DateTime(1995, 9, 4), EduEnd = new DateTime(2, 2, 2), Faculty = new Faculty() { FacultyName = "fac1", IsApproved = true }, Qualification = "1", Specialty = "spec", University = new University() { City = "Kiev", Country = "Ukraine", IsApproved = false, IsHighSchool = false, UniversityName = "KNO" }, });
            cv.Education.Add(new Education() { EduBegin = new DateTime(3, 3, 3), EduEnd = new DateTime(4, 4, 4), Faculty = new Faculty() { FacultyName = "fac2", IsApproved = true }, Qualification = "1", Specialty = "spec", University = new University() { City = "Kiev", Country = "Ukraine", IsApproved = true, IsHighSchool = false, UniversityName = "uni1" }, });
            cv.Jobs.Add(new Job() { DateOfBeginning = new DateTime(1, 1, 1), DateOfEnding = new DateTime(1, 1, 1), Employer = "dsd", PersonalAchievements = "as", Position = "dsdsfsfr", Responsibility = "dff" });
            cv.OtherInfo = "dsff";
            cv.SkillsDesc = "sd,sd,sd";
            cv.User = new CVUser() { Address = "a1", DateOfBirth = new DateTime(1991, 12, 4), Email = "s@at.me", Name = "bobo", Phone = "323", Surname = "aba", };

            //return View((CV)TempData.Peek("CV"));
            return View(cv);
        }

        public ActionResult EditCV()
        {
            //CV cv = (CV)TempData.Peek("CV");
            CV cv = new CV();
            cv.DateAdded = new DateTime(2000, 1, 1);
            cv.Education.Add(new Education() { EduBegin = new DateTime(1995, 9, 4), EduEnd = new DateTime(2, 2, 2), Faculty = new Faculty() { FacultyName = "fac1", IsApproved = true }, Qualification = "1", Specialty = "spec", University = new University() { City = "Kiev", Country = "Ukraine", IsApproved = false, IsHighSchool = false, UniversityName = "KNO" }, });
            cv.Education.Add(new Education() { EduBegin = new DateTime(3, 3, 3), EduEnd = new DateTime(4, 4, 4), Faculty = new Faculty() { FacultyName = "fac2", IsApproved = true }, Qualification = "1", Specialty = "spec", University = new University() { City = "Kiev", Country = "Ukraine", IsApproved = true, IsHighSchool = false, UniversityName = "uni1" }, });
            cv.Jobs.Add(new Job() { DateOfBeginning = new DateTime(1, 1, 1), DateOfEnding = new DateTime(1, 1, 1), Employer = "dsd", PersonalAchievements = "as", Position = "dsdsfsfr", Responsibility = "dff" });
            cv.OtherInfo = "dsff";
            cv.SkillsDesc = "sd,sd,sd";
            cv.User = new CVUser() { Address = "a1", DateOfBirth = new DateTime(1991, 12, 4), Email = "s@at.me", Name = "bobo", Phone = "323", Surname = "aba", };

            ViewBag.UnderEdit = true;
            ViewBag.CvJson = cv;
            return View("MakeCV");
        }

        public ActionResult View1()
        {
            FileStream stream = new FileStream(Server.MapPath("~/cv.dll"), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter sr = new BinaryFormatter();
            CV deserialized = (CV)sr.Deserialize(stream);
            Debug.WriteLine(deserialized.SkillsDesc);
            return View();
        }
    }
}
