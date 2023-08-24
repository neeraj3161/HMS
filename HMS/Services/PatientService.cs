using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Models;

namespace HMS.Services
{
	public static class PatientService
	{
		public static List<TreatmentRecordModal> GetAllAppointments(int patientID)
		{
            List<TreatmentRecordModal> treatmentRecordModals = new List<TreatmentRecordModal>();
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"];
            string query = $"SELECT * FROM hms.patient_treatment_record WHERE patient_id = {patientID};";
            NpgsqlConnection connection = new NpgsqlConnection(connectionString.ToString());
            try
            {
                connection.Open();

                using (NpgsqlTransaction tran = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(query.ToString(), connection, tran))
                        {
                            using (NpgsqlDataReader dr = command.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        treatmentRecordModals.Add(new TreatmentRecordModal
                                        {
                                            TreatmentId = Convert.ToInt32(dr["treatment_record_id"]),
                                            PatientId = Convert.ToInt32(dr["patient_id"]),
                                            DoctorId = Convert.ToInt32(dr["doctor_id"]),
                                            DepartmentId = Convert.ToInt32(dr["department_id"]),
                                            HospitalId = Convert.ToInt32(dr["hospital_id"]),
                                            Admitted = Convert.ToBoolean(dr["admitted"]),
                                            Age = Convert.ToInt32(dr["age"]),
                                            Is_Active = Convert.ToBoolean(dr["is_active"]),
                                            AdmissionStartDate = (dr["admission_start_date"]) != null ? Convert.ToDateTime(dr["admission_start_date"]) : default(DateTime),
                                            AdmissionEndDate = (dr["admission_end_date"]) != null ? Convert.ToDateTime(dr["admission_end_date"]) : default(DateTime),
                                            PatientBloodPressure = dr["patient_blood_pressure"] != null ? dr["patient_blood_pressure"].ToString() : string.Empty,
                                            PatientWeight = dr["patient_weight"] != null ? dr["patient_weight"].ToString() : string.Empty,
                                            Created = Convert.ToDateTime(dr["created"]),
                                            NextAppointmentDate = (dr["next_appointment_date"]) != null ? Convert.ToDateTime(dr["next_appointment_date"]) : default(DateTime)

                                        });
                                    }
                                }
                            }
                        }

                        tran.Commit();

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            tran.Rollback();
                        }
                        catch (Exception)
                        { }
                    }

                }
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return treatmentRecordModals;
		}
	}
}