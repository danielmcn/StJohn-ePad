using iTextSharp.text;
using iTextSharp.text.pdf;
using StJohnEPAD.DAL;
using StJohnEPAD.Models;
using StJohnEPAD.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StJohnEPAD.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ReportsController : Controller
    {

        SJAContext db = new SJAContext();
        //

        // GET: /Reports/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Create a collection of all users and their hours to compare each member's efforts
        /// </summary>
        /// <returns>List of HoursPerUser</returns>
        public ActionResult DutyPerAllUsers()
        {
            //ICollection<HoursPerUserViewModel> result = new LinkedList<HoursPerUserViewModel>();

            Dictionary<int, HoursPerUserViewModel> dictionary = new Dictionary<int, HoursPerUserViewModel>();

            var allHours = db.ConfirmedDutyHours.OrderBy(x => x.UserId).ToList();

            //Populate our view model with emptyu recrods for each user
            foreach(UserProfile up in db.Users.ToList())
            {
                var hpuvm = new HoursPerUserViewModel
                {
                    UserId = up.UserId,
                    UserName = up.Name,
                    Hours = 0,
                    Duties = 0,
                };
                dictionary.Add(hpuvm.UserId, hpuvm);
            }

            foreach (ConfirmedDutyHours cdh in allHours)
            {
                HoursPerUserViewModel hpuvm;
                dictionary.TryGetValue(cdh.UserId, out hpuvm);
                hpuvm.Hours += cdh.DutyHourAmount;
                hpuvm.Duties++;
                dictionary[cdh.UserId] = hpuvm;
            }

            //Data for our google chart
            //If we have 10 users+, we want to do 9 + "misc"
            Dictionary<string, int> chartDictionary = new Dictionary<string, int>();
            foreach (int i in dictionary.Keys)
            {
                chartDictionary.Add(dictionary[i].UserName, dictionary[i].Duties);
            }
            string[] cNames = chartDictionary.Keys.ToArray();
            int[] cNumbers = chartDictionary.Values.ToArray();

            ViewBag.ChartNames = cNames;
            ViewBag.ChartNumbers = cNumbers;

            return View(dictionary.Values.ToList());
        }




        /// <summary>
        /// Make a list of all duties for a specific user, between the selected dates
        /// </summary>
        /// <returns></returns>
        public ActionResult DutyPerUser(int id, string from = null,string to = null)
        {
            ICollection<ConfirmedDutyHours> cdh = new LinkedList<ConfirmedDutyHours>();
            //using(SJAContext ctx = new SJAContext())
            {
                DateTime fromDate;
                DateTime toDate;

                if (DateTime.TryParse(from, out fromDate) && DateTime.TryParse(to, out toDate))
                    cdh = db.ConfirmedDutyHours.Where(x => x.UserId == id && x.Duty.DutyDate <= toDate && x.Duty.DutyDate >= fromDate).ToList();
                else if (DateTime.TryParse(from, out fromDate))
                    cdh = db.ConfirmedDutyHours.Where(x => x.UserId == id && x.Duty.DutyDate >= fromDate).ToList();
                else if (DateTime.TryParse(to, out toDate))
                    cdh = db.ConfirmedDutyHours.Where(x => x.UserId == id && x.Duty.DutyDate <= toDate).ToList();
                else
                    cdh = db.ConfirmedDutyHours.Where(x => x.UserId == id).ToList();
            }
            return View(cdh);
        }


        public ActionResult HoursPerDuty(string from = null, string to = null)
        {
            ICollection<ConfirmedDutyHours> cdh = new LinkedList<ConfirmedDutyHours>();
            //using(SJAContext ctx = new SJAContext())
            {
                DateTime fromDate;
                DateTime toDate;

                if (DateTime.TryParse(from, out fromDate) && DateTime.TryParse(to, out toDate))
                    cdh = db.ConfirmedDutyHours.Where(x => x.Duty.DutyDate <= toDate && x.Duty.DutyDate >= fromDate).ToList();
                else if (DateTime.TryParse(from, out fromDate))
                    cdh = db.ConfirmedDutyHours.Where(x => x.Duty.DutyDate >= fromDate).ToList();
                else if (DateTime.TryParse(to, out toDate))
                    cdh = db.ConfirmedDutyHours.Where(x => x.Duty.DutyDate <= toDate).ToList();
                else
                    cdh = db.ConfirmedDutyHours.ToList();
            }

            ICollection<HoursPerDutyViewModel> hoursPerDutyList = new LinkedList<HoursPerDutyViewModel>();
            int totalDuties = 0;
            double totalHours = 0.00;
            int totalMembers = 0;
            foreach (PostDutyReport duty in db.PostDutyReport.ToList())
            {
                double hours = 0.00;
                int members = 0;
                if(duty.Attendees != null)
                foreach (ConfirmedDutyHours v in duty.Attendees)
                {
                    hours += v.DutyHourAmount;
                    members++;
                }
                hoursPerDutyList.Add(new HoursPerDutyViewModel { DutyID = duty.DutyID, DutyName = duty.Duty.DutyName, HoursPerDuty = (float)hours, MembersPerDuty = members });
                totalHours += hours;
                totalMembers += members;
                totalDuties++;
            }

            ViewBag.TotalDuties = totalDuties;
            ViewBag.TotalMembers = totalMembers;
            ViewBag.TotalHours = totalHours;

            #region Itextsharp
            /*
            iTextSharp.text.Document doc = null;
            try
            {
                doc = new iTextSharp.text.Document();
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc,
                    new System.IO.FileStream("C:\\Users\\Daniel\\Desktop\\ScienceReport.pdf",
                        System.IO.FileMode.Create));
                doc.SetMargins(50, 50, 50, 50);
                doc.SetPageSize(new iTextSharp.text.Rectangle(iTextSharp.text.PageSize.LETTER.Width,
                    iTextSharp.text.PageSize.LETTER.Height));
                doc.Open();
                doc.Add(new Paragraph("Hello World!"));
                doc.Close();
                writer.Close();

            }
            catch (iTextSharp.text.DocumentException ex)
            {
                //ex
            }
            finally
            {
                doc.Close();
                doc = null;
            } 

            */
            #endregion

            return View(hoursPerDutyList);
        }

        public ActionResult Equipment()
        {
            return View();
        }

        public ActionResult Training()
        {
            return View();
        }

        public ActionResult Duties()
        {
            return View();
        }

        public ActionResult CoverCalculator()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
