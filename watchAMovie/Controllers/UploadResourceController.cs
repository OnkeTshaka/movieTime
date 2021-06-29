using watchAMovie.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace watchAMovie.Controllers
{
    public class UploadResourceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Search(string searching, int? page)
        {
            var videoList = db.watchTrailers.OrderByDescending(x => x.Vid).Where(m => m.Vname.Contains(searching) || searching == null).Take(3).ToList();
            return View(videoList);
        }
        // GET: UploadResource
        [HttpGet]
        public ActionResult Index()
        {
            List<watchTrailer> videolist = new List<watchTrailer>();
            string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from watchTrailers";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            while (sdr.Read())
            {
                watchTrailer uc = new watchTrailer();
                uc.Vname = sdr["Vname"].ToString();
                uc.Vpath = sdr["Vpath"].ToString();
                videolist.Add(uc);
            }
            return View(videolist);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            watchTrailer uploadTraining = db.watchTrailers.Find(id);
            if (uploadTraining == null)
            {
                return HttpNotFound();
            }
            return View(uploadTraining);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            watchTrailer uploadTraining = db.watchTrailers.Find(id);
            db.watchTrailers.Remove(uploadTraining);
            db.SaveChanges();
            return RedirectToAction("Search");
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase videofile)
        {
            if (videofile != null)
            {
                string filename = Path.GetFileName(videofile.FileName);
                if (videofile.ContentLength < 1048576000)
                {
                    videofile.SaveAs(Server.MapPath("/Videofiles/" + filename));

                    string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection sqlconn = new SqlConnection(mainconn);
                    string sqlquery = "insert into watchTrailers values (@Vname, @Vpath)";
                    SqlCommand sqlComm = new SqlCommand(sqlquery, sqlconn);
                    sqlconn.Open();
                    sqlComm.Parameters.AddWithValue("@Vname", filename);
                    sqlComm.Parameters.AddWithValue("@Vpath", "/Videofiles/" + filename);
                    sqlComm.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            return RedirectToAction("Search");
        }

    }
}