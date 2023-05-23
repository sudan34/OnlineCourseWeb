using OnlineCourseWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineCourseWeb.Controllers
{
    public class DefaultController : Controller
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);
        SqlDataReader rdr = null;

        // GET: Default
        public ActionResult Index()
        {
            connection.Open();

            if ((connection.State & System.Data.ConnectionState.Open) > 0)
            {
                SqlCommand cmd = new SqlCommand("Menu", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@FilterId", "MenuLocationList"));
                //cmd.Parameters.Add(new SqlParameter("@Location", "TopBar"));

                rdr = cmd.ExecuteReader();

                ViewBag.result = rdr.ToString() + "Working !";
            }
            connection.Close();
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Menu(MenuLocationModel model)
        {
            if (ModelState.IsValid)
            {
                connection.Open();
                if ((connection.State & System.Data.ConnectionState.Open) > 0)
                {
                    SqlCommand cmd = new SqlCommand("Menu", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@FilterId", "MenuLocation"));
                    cmd.Parameters.Add(new SqlParameter("@Location", model.Location));

                    cmd.ExecuteNonQuery();

                }
                connection.Close();
                ViewBag.result = model.Location + " Working !";
            }
            return View();
        }
    
        public JsonResult menuListJson()
        {
            List<MenuLocationModel> models= new List<MenuLocationModel>();
            connection.Open();
            if ((connection.State & System.Data.ConnectionState.Open) > 0)
            {
                SqlCommand cmd = new SqlCommand("Menu", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FilterId", "MenuLocationList"));
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MenuLocationModel obj = new MenuLocationModel();
                    obj.Id = rdr.GetValue(0).ToString();
                    obj.Location = rdr.GetValue(1).ToString();
                    models.Add(obj);
                }
            }
            connection.Close();
            return Json(models.ToList(),JsonRequestBehavior.AllowGet);
        }

        public JsonResult save(MenuLocationModel modelBind)
        {
            connection.Open();
            if ((connection.State & System.Data.ConnectionState.Open) > 0)
            {
                SqlCommand cmd = new SqlCommand("Menu", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FilterId", "MenuLocationUpdate"));
                cmd.Parameters.Add(new SqlParameter("@Location", modelBind.Location));
                cmd.Parameters.Add(new SqlParameter("@Id", modelBind.Id));
                rdr = cmd.ExecuteReader();
            }
            connection.Close();
            return Json("done !");
        }

        public JsonResult delete(MenuLocationModel modelBind)
        {
            connection.Open();
            if ((connection.State & System.Data.ConnectionState.Open) > 0)
            {
                SqlCommand cmd = new SqlCommand("Menu", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FilterId", "MenuLocationDelete"));
                cmd.Parameters.Add(new SqlParameter("@Id", modelBind.Id));
                rdr = cmd.ExecuteReader();
            }
            connection.Close();
            return Json("done !");
        }


        public ActionResult Links()
        {
            return View();
        }

        public JsonResult InsertLinks(ModelLinks modelBind)
        {
            connection.Open();
            if ((connection.State & System.Data.ConnectionState.Open) > 0)
            {
                SqlCommand cmd = new SqlCommand("Menu", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FilterId", "Insert_MenuLinks"));

                cmd.Parameters.Add(new SqlParameter("@MenuLocId", modelBind.MenuLocId));
                cmd.Parameters.Add(new SqlParameter("@LinksName", modelBind.LinksName));
                cmd.Parameters.Add(new SqlParameter("@LinksURL", modelBind.LinksURL));
                rdr = cmd.ExecuteReader();
            }
            connection.Close();
            return Json("done !");
        }


        public JsonResult LinksMenuListJson()
        {
            List<MenuLocationModel> models = new List<MenuLocationModel>();
            connection.Open();
            if ((connection.State & System.Data.ConnectionState.Open) > 0)
            {
                SqlCommand cmd = new SqlCommand("Menu", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FilterId", "MenuLocationDropDown"));
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MenuLocationModel obj = new MenuLocationModel();
                    obj.Id = rdr.GetValue(0).ToString();
                    obj.Location = rdr.GetValue(1).ToString();
                    models.Add(obj);
                }
            }
            connection.Close();
            return Json(models.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}