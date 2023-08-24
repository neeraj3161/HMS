using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
	public class HospitalInfoModal
	{
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string State { get; set; }
        public int HospitalType { get; set; }
        public int Established { get; set; }
        public string Description { get; set; }

        public int PatientId { get; set; }

        public List<DoctorsLoginModal> AssociatedDoctorsInfo { get; set; }


        public HospitalInfoModal()
        {
            List<DoctorsLoginModal> doctorModal = new List<DoctorsLoginModal>();
        }
    }
}