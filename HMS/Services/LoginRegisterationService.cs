using HMS.Models;
using Newtonsoft.Json;
using Npgsql;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HMS.Services.Enum;

namespace HMS.Services
{
	public static class LoginRegisterationService
	{
		public static Boolean IsLoggedIn()
		{
			if (IsRedisKeyExists(RedisKeyEnum.PatientLoginInfo.ToString()))
				return true;
			return false;
		}

		private static Boolean IsRedisKeyExists(string key)
		{
			using (RedisClient client = new RedisClient("localhost"))
			{
				if (client.Get<String>(key) != null)
				{
					client.Quit();
					return true;
				}
				client.Quit();
			}
			return false;
		}

		private static Boolean SaveRedisData(string key, string value)
		{
			if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(value))
			{
				using (RedisClient client = new RedisClient("localhost"))
				{
					client.Set(key, value);
					client.Quit();
					return true;
				}
			}
			return false;
		}

		public static Boolean DeleteRedisData(string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				using (RedisClient client = new RedisClient("localhost"))
				{
					client.Remove(key);
					client.Quit();
					return true;
				}
			}
			return false;
		}

		public static string ReadRedisData(string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				using (RedisClient client = new RedisClient("localhost"))
				{
					var data=client.GetValue(key);
					client.Quit();
					return data;
				}
			}
			return string.Empty;
		}

		public static Boolean ValidateLoginCredentials(string key, string password, Boolean isDoctor= false)
		{
			if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(password))
			{
				
					var saved = ValidateCredentials(key, password, isDoctor);
				if (saved)
					return true;
				return false;
				
			}
			return true;
		}

		private static Boolean ValidateCredentials( string key, string password, Boolean isDoctor = false)
		{
			
			if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(password))
			{
				string query = string.Empty;
				if (isDoctor)
				{
					query = $"SELECT * FROM hms.doctors_info WHERE phone_number = '{key}' AND password = '{password}' ;";
				}
				else {
					query = $"SELECT * FROM hms.patient_info WHERE email = '{key}' AND password = '{password}' ;";
				}
				var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"];

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
								using (NpgsqlDataReader reader = command.ExecuteReader())
								{
									
									if (reader.HasRows)
									{
										while (reader.Read())
										{
											if (isDoctor)
											{
												DoctorsLoginModal doctorModal = new DoctorsLoginModal()
												{
													DoctorId = (int)reader["doctor_id"],
													Name = Convert.ToString(reader["name"]),
													Surname = Convert.ToString(reader["surname"]),
													Speciality = Convert.ToString((Enum.MedicalSpecialty)(int)reader["speciality"]),
													Experience = (int)reader["experience"],
													PatientsTreated = (int)reader["parients_treated"],
													PhoneNumber = Convert.ToString(reader["phone_number"]),
													AssociatedHospitalId = (int)reader["associated_hospital_id"]
												};

												string jsonString = JsonConvert.SerializeObject(doctorModal);

												if (doctorModal != default(DoctorsLoginModal))
												{

													var saved = SaveRedisData(RedisKeyEnum.DoctorLoginInfo.ToString(), jsonString);

													if (saved)
														return true;
													return false;
												}
												return false;
											}

											else 
											{
												PatientLoginModal patientModal = new PatientLoginModal()
												{
													PatientId = (int)reader["patient_id"],
													Name = Convert.ToString(reader["name"]),
													Surname = Convert.ToString(reader["surname"]),
													PhoneNo = Convert.ToString(reader["phone_no"]),
													Address = Convert.ToString(reader["address"]),
													Pincode = (int)reader["pincode"],
													State = Convert.ToString(reader["state"]),
													Email = Convert.ToString(reader["email"]),
													Gender = (int)reader["gender"]
												};
												string jsonString = JsonConvert.SerializeObject(patientModal);

												if (patientModal != default(PatientLoginModal))
												{
													var saved = SaveRedisData(RedisKeyEnum.PatientLoginInfo.ToString(), jsonString);
													if (saved)
														return true;
													return false;
												}
												return false;
											}

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


			}
			return false;
		
		}
	}
}