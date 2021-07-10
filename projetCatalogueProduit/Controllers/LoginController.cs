using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using projetCatalogueProduit.Models;
using System.IO;

namespace projetCatalogueProduit.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(projetCatalogueProduit.Models.User userModel)
        {
            using(CATALOGUE_Entities db = new CATALOGUE_Entities())
            {
                var userDetails = db.User.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();

                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Username ou Mot de Passe incorrecte";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Home");
                }

            }
            
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}