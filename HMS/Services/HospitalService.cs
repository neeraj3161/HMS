using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using HMS.Models;

namespace HMS.Services
{
    public static class HospitalService
    {
        public static List<HospitalInfoModal> GetAllHospitalLists()
        {
            List<HospitalInfoModal> hospitalInfoModals = new List<HospitalInfoModal>();
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"];
            string query = $"SELECT * FROM hms.hospital_info;";
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
                                        hospitalInfoModals.Add(new HospitalInfoModal()
                                        {
                                            HospitalId = (int)dr["hospital_id"],
                                            Name = Convert.ToString(dr["name"]),
                                            State = Convert.ToString(dr["state"]),
                                            HospitalType = (int)dr["hospital_type"],
                                            Established = (int)dr["estabilished"],
                                            Description = Convert.ToString(dr["description"]),
                                            PhoneNo=Convert.ToString(dr["phone_no"]),
                                            Pincode = (int)dr["pincode"],
                                            Address = Convert.ToString(dr["address"])
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
                        { 
                        
                        }
                    }

                }
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return hospitalInfoModals;
        }

        
        public static List<DoctorsLoginModal> GetHospitalInfo(int hospitalId)
        {
            List<DoctorsLoginModal> hospitalDoctorsInfoModals = new List<DoctorsLoginModal>();
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"];
            string query = $"SELECT * FROM hms.doctors_info di " +
                $"INNER JOIN hms.hospital_info h ON h.hospital_id = di.associated_hospital_id " +
                $"WHERE associated_hospital_id = '{hospitalId}';";
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
                                        hospitalDoctorsInfoModals.Add(new DoctorsLoginModal()
                                        {
                                            DoctorId = (int)dr["doctor_id"],
                                            Name = Convert.ToString(dr[0]),
                                            Surname = Convert.ToString(dr["surname"]),
                                            Speciality = Convert.ToString((Enum.MedicalSpecialty)(int)dr["speciality"]),
                                            Experience = (int)dr["experience"],
                                            PatientsTreated = (int)(dr["parients_treated"]),
                                            PhoneNumber = Convert.ToString(dr["phone_number"]),
                                            AssociatedHospitalId = (int)dr["associated_hospital_id"],
                                            
                                            HospitalInfo = new List<HospitalInfoModal>() { new HospitalInfoModal() {
                                                Name=Convert.ToString(dr[10]), 
                                                Description = Convert.ToString(dr["description"]),
                                                State = Convert.ToString(dr["state"]),
                                                HospitalType = (int)(dr["hospital_type"]),
                                                Established = (int)(dr["estabilished"]),
                                                Address = Convert.ToString(dr["address"]),
                                                HospitalId=hospitalId
                                            } }

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
                        {

                        }
                    }

                }
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return hospitalDoctorsInfoModals;
        }

        public static Boolean SubmitAppointment(int patientId,int doctor_id,int department_id,int hospital_id,string treatmentInfo,int age)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"];
            string query = $"INSERT INTO hms.patient_treatment_record(patient_id,doctor_id,department_id,hospital_id,admitted,treatment_info,age) " +
                            $"VALUES({patientId},{doctor_id},{department_id},{hospital_id},false,'{treatmentInfo}',{age});";
            Boolean isSuccess = false;
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
                                isSuccess = true;
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

            return isSuccess;
        }

    }

   
}