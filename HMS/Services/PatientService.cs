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
                                            AdmissionStartDate = dr.IsDBNull(dr.GetOrdinal("admission_start_date")) ? default(DateTime) : Convert.ToDateTime(dr["admission_start_date"]) ,
                                            AdmissionEndDate = dr.IsDBNull(dr.GetOrdinal("admission_end_date")) ? default(DateTime) : Convert.ToDateTime(dr["admission_end_date"]),
                                            PatientBloodPressure = dr.IsDBNull(dr.GetOrdinal("patient_blood_pressure")) ? default(string) : dr["patient_blood_pressure"].ToString(),
                                            PatientWeight = dr.IsDBNull(dr.GetOrdinal("patient_weight")) ? default(string) : dr["patient_weight"].ToString(),
                                            Created = Convert.ToDateTime(dr["created"]),
                                            NextAppointmentDate = dr.IsDBNull(dr.GetOrdinal("next_appointment_date")) ? default(DateTime) : Convert.ToDateTime(dr["next_appointment_date"])

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

        public static List<GovtSchemeModal> GetAllGovtSchemes()
        {
            List<GovtSchemeModal> govtSchemeModals = new List<GovtSchemeModal>();
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"];
            string query = $"SELECT *,h.name FROM hms.gov_benefits gb " +
                $"INNER JOIN hms.gov_benefits_to_hospital gth ON gth.gov_benefits_id = gb.gov_benefits_id " +
                $"INNER JOIN hms.hospital_info h ON h.hospital_id = gth.hospital_id ;"; 
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
                                        govtSchemeModals.Add(new GovtSchemeModal
                                        {
                                            HospitalId = Convert.ToInt32(dr["hospital_id"]),
                                            HospitalType = Convert.ToInt32(dr["hospital_type"]) == 1?"Govt":"Pvt",
                                            BenefitName = dr["benefit_name"].ToString(),
                                            Description = dr["description"].ToString(),
                                            IsOPDValid = Convert.ToBoolean(dr["is_opd_valid"]),
                                            DiscountPercentage = Convert.ToInt32(dr["discount_percentage"]),
                                            HospitalName = dr["name"].ToString(),
                                            GovBenefitsId = Convert.ToInt32(dr["gov_benefits_id"])

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

            return govtSchemeModals;
        }
    }
}