using HMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Models;
using static HMS.Services.Enum;
using Newtonsoft.Json;

namespace HMS.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			if (LoginRegisterationService.IsLoggedIn())
			{			
				return Redirect("/patientMain");
			}
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[HttpPost]

		public JsonResult PatientLogin(string email, string password)
		{
			var success = false;
			var isValidAndSaved = LoginRegisterationService.ValidateLoginCredentials(email, password);
			if (isValidAndSaved)
				success = true;

			return Json(new {success = success });
		}


		[HttpPost]

		public JsonResult DoctorLogin(string email, string password)
		{
			var success = false;
			var isValidAndSaved = LoginRegisterationService.ValidateLoginCredentials(email, password,true);
			if (isValidAndSaved)
				success = true;

			return Json(new { success = success });
		}


	}
}