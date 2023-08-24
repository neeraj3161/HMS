using HMS.Models;
using HMS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HMS.Services.Enum;

namespace HMS.Controllers
{
	public class PatientMainController : Controller
	{
		public ActionResult Index()
		{
			if (LoginRegisterationService.IsLoggedIn())
			{
				HospitalService.GetAllHospitalLists();
				string jsonDataString = LoginRegisterationService.ReadRedisData(RedisKeyEnum.PatientLoginInfo.ToString()).Trim();
				
				string result = jsonDataString.Substring(1, jsonDataString.Length - 2);
				string cleanJson = result.Replace("\\", "");
				var appointmentsList = PatientService.GetAllAppointments(GetPatientId());
				var modals = JsonConvert.DeserializeObject<PatientLoginModal>(cleanJson);
				var modal = new PatientLoginModal() { Name = modals.Name, Surname = modals.Surname,AppointmentsInfo = appointmentsList};
			return View("patientMain",modal);
			}
			return Redirect("/");

		}

		[HttpPost]

		public JsonResult GetAllHospitalInfo()
		{
			List<HospitalInfoModal>hospitalInfoList = HospitalService.GetAllHospitalLists();
			return Json(new {result = hospitalInfoList, success=true });
		}

		[HttpPost]

		public JsonResult Logout()
		{
			var success = false;
			success=LoginRegisterationService.DeleteRedisData(RedisKeyEnum.PatientLoginInfo.ToString());
			return Json(new { success=success });
		}

		public ActionResult HospitalInfo()
		{
			int id = TempData.ContainsKey("doctID") ? (int)TempData["doctID"] : 1;
			var doctorModalData = HospitalService.GetHospitalInfo(id);
			//Convert to hospital modal data
			List<DoctorsLoginModal> doctorModal = new List<DoctorsLoginModal>();
			foreach (var data in doctorModalData)
			{
				doctorModal.Add(new DoctorsLoginModal() {
					Name = data.Name,
					Surname = data.Surname,
					Speciality = data.Speciality,
					AssociatedHospitalId = data.AssociatedHospitalId,
					DoctorId = data.DoctorId,
					Experience = data.Experience,
					PatientsTreated = data.PatientsTreated,
					PhoneNumber = data.PhoneNumber
				});
			}
			var hospitalBasicInfo = doctorModalData.FirstOrDefault().HospitalInfo.FirstOrDefault();

			HospitalInfoModal hospitalInfoModal = new HospitalInfoModal()
			{

				AssociatedDoctorsInfo = doctorModal,
				Name = hospitalBasicInfo.Name,
				PhoneNo = hospitalBasicInfo.PhoneNo,
				Address = hospitalBasicInfo.Address,
				Description = hospitalBasicInfo.Description,
				Established = hospitalBasicInfo.Established,
				HospitalId = hospitalBasicInfo.HospitalId,
				HospitalType = hospitalBasicInfo.HospitalType,
				Pincode = hospitalBasicInfo.Pincode,
				State = hospitalBasicInfo.State,
				PatientId = GetPatientId()

			};
			return View("HospitalInfo", hospitalInfoModal);
		}
		
		[HttpPost]
		public JsonResult HospitalInfo(int Id)
		{
			TempData["doctID"] = Id;
			return Json(new { success=true});
		}

		[HttpPost]

		public JsonResult GetPatientInfo()

		{
			string jsonDataString = LoginRegisterationService.ReadRedisData(RedisKeyEnum.PatientLoginInfo.ToString()).Trim();
			string result = jsonDataString.Substring(1, jsonDataString.Length - 2);
			string cleanJson = result.Replace("\\", "");
			var modal = JsonConvert.DeserializeObject<PatientLoginModal>(cleanJson);
			return Json(new { patientModal = modal });
		}

		public int GetPatientId()
		{
			string jsonDataString = LoginRegisterationService.ReadRedisData(RedisKeyEnum.PatientLoginInfo.ToString()).Trim();
			string result = jsonDataString.Substring(1, jsonDataString.Length - 2);
			string cleanJson = result.Replace("\\", "");
			return JsonConvert.DeserializeObject<PatientLoginModal>(cleanJson).PatientId;
		}

		[HttpPost]

		public JsonResult InsertAppointment(int patientId, int doctor_id, int department_id, int hospital_id, string treatmentInfo,int age)
		{
			var success = HospitalService.SubmitAppointment(patientId, doctor_id, department_id, hospital_id, treatmentInfo,age);

			return Json(new {success=success});
		}
	}
}