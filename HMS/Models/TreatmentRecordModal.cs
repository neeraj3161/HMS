using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
	public class TreatmentRecordModal
	{
        public int TreatmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public int HospitalId { get; set; }
        public bool Admitted { get; set; }
        public DateTime? AdmissionStartDate { get; set; }
        public DateTime? AdmissionEndDate { get; set; }
        public string PatientBloodPressure { get; set; }
        public string PatientWeight { get; set; }
        public DateTime Created { get; set; }
        public DateTime? NextAppointmentDate { get; set; }

        public int Age { get; set; }

        public Boolean Is_Active { get; set; }

    }
}