using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using TestProvider_Next.Helpers;
using TestProvider_Next.Models.Repositories;
using System.Web.Script.Serialization;
using TPN.NewModel;
using TPN.NewModel.CV;
using System;
using PagedList;

namespace TestProvider_Next.Controllers
{
    [Authorize]  // Класс отображающий Кабинет пользователя
    public class UserCabinetController : BaseController
    {
        CurrentTPUser user = AuthorizationHelper.User;
        
        public UserCabinetController(IUow uow) : base(uow)
        {
        }

        // Выод всех пройденных тестов пользователем
        public ActionResult CompleteExam(int? page)
        {

            var tpUser = Uow.Users.All.FirstOrDefault(u => u.UserLoginId == user.LoginId);

            var userStats = (from userStat in Uow.UserStatistics.All
                             where userStat.UserId == tpUser.UserId
                             select userStat).FirstOrDefault();
            List<ExamStatistics> userStatList = userStats == null ? new  List<ExamStatistics>() : userStats.CompletedExams;
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(userStatList.ToPagedList(pageNumber, pageSize));
        }

        // Страница формирующая список сертификатов пользователя
        public ActionResult Certificate(int? page)
        {
            var cu = UserHelper.GetCurrentUser(Uow);
            var userStatisticId = Uow.UserStatistics.All.Where(m => m.UserId == cu.UserId).Select(s => s.UserStatisticsId).FirstOrDefault();
            var exam = Uow.ExamStatistics.All.Where(e => e.UserStatisticsId == userStatisticId).ToList();
            List<StatisticsCertificate> examList = new List<StatisticsCertificate>();
            if (exam.Count() > 0)
            {

                foreach (var item in exam)
                {
                    StatisticsCertificate infoCertification = new StatisticsCertificate();
                    var certificates = Uow.Certificates.All.Where(m => m.ExamStatisticsId == item.ExamStatisticsId).FirstOrDefault();
                    if (certificates != null)
                    {
                        infoCertification.ExamName = item.ExamName;
                        infoCertification.UserName = certificates.UserName;
                        infoCertification.ExamResult = item.ExamResult;
                        infoCertification.UniqueNumber = certificates.UniqueNumber;
                        infoCertification.ExamDate = item.ExamDate;
                        infoCertification.IsReleased = certificates.IsReleased ?? false;
                        examList.Add(infoCertification);
                    }
                }
                int pageNumber = (page ?? 1);
                int pageSize = 10;
                return View(examList.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        // подробный результат по каждому екзамену
        public ActionResult CompleteExamMore(string id)
        {
            int statisticsId = Convert.ToInt32(id);
            var examStatistics = Uow.ExamStatistics.All.Where(m => m.ExamStatisticsId == statisticsId).FirstOrDefault();
            var lessonsStatistics = Uow.LessonStatistics.All.Where(l => l.ExamStatisticsId == statisticsId).ToList();
            return View(new Tuple<ExamStatistics, List<LessonStatistics>>(examStatistics, lessonsStatistics));
        }

        // История покупок пользователя
        public ActionResult BoughtExam()
        {
            User usersPaid = Uow.Users.All.Where(m => m.UserLoginId == user.LoginId).FirstOrDefault();

            var paidStat = Uow.PaidStats.All.Where(ps => ps.UserLoginId == usersPaid.UserLoginId).ToList();

            return View(paidStat);
        }

        [HttpPost]
        public ActionResult PrintCertificate(PrintCert printcert)
        {
            var certificate = Uow.Certificates.All.FirstOrDefault(m => m.UniqueNumber == printcert.UniqueNumber);
            if (certificate != null)
            {
                certificate.UserName = printcert.UserName;
                certificate.IsReleased = true;
                Uow.Certificates.InsertOrUpdate(certificate);
            }

            Uow.PrintCerts.InsertOrUpdate(printcert);
            Uow.Save();
            return View();
        }

        public ActionResult PrePrintCertificate(PrintCert printcert)
        {
            return View(printcert);
        }

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
            TempData["CV"] = cv;
            return Content("OK","text/plain");
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
            cv.User = new CVUser() { Address = "a1", DateOfBirth = new DateTime(1991,12,4), Email = "s@at.me", Name = "bobo", Phone = "323", Surname = "aba", };

            ViewBag.UnderEdit = true;
            ViewBag.CvJson = cv;
            return View("MakeCV");
        }

        
    }

    // Попытка изобразить ViewModel очередным говнокодером
    public class StatisticsCertificate
    {
        public string ExamName { get; set; }
        public string UserName { get; set; }
        public int ExamResult { get; set; }
        public string UniqueNumber { get; set; }
        public DateTime ExamDate { get; set; }
        public bool IsReleased { get; set; }
    }


}
