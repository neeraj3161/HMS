using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace HMS.Models
{
	public class PatientLoginModal
	{
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }

        public List<TreatmentRecordModal> AppointmentsInfo { get; set; }
    }
}