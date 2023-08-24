using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
	public class DoctorLoginInfoWrapper
	{
	 	public List<DoctorsLoginModal> doctorsLoginModals { get; set; }

		public DoctorLoginInfoWrapper()
		{
			doctorsLoginModals = new List<DoctorsLoginModal>();
		}
	}
}