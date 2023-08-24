using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
	public class DoctorsLoginModal
	{
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Speciality { get; set; }
        public int Experience { get; set; }
        public int PatientsTreated { get; set; }
        public string PhoneNumber { get; set; }
        public int AssociatedHospitalId { get; set; }

        public List<HospitalInfoModal> HospitalInfo { get; set; }

        public DoctorsLoginModal()
        {
            HospitalInfo = new List<HospitalInfoModal>();
        }
    }
}