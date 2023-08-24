using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Services
{
	public class Enum
	{
        public enum MedicalSpecialty
        {
            Cardiology = 1,
            Neurology = 2,
            Orthopedics = 3,
            Gastroenterology = 4,
            Dermatology = 5,
            Pediatrics = 6,
            ObstetricsAndGynecology = 7,
            Ophthalmology = 8,
            Urology = 9,
            Nephrology = 10,
            Pulmonology = 11,
            Endocrinology = 12,
            Hematology = 13,
            Rheumatology = 14,
            Oncology = 15,
            InfectiousDiseases = 16,
            AllergyAndImmunology = 17,
            Psychiatry = 18,
            Radiology = 19,
            Anesthesiology = 20,
            GeneralSurgery = 21,
            PlasticSurgery = 22,
            Otolaryngology = 23,
            EmergencyMedicine = 24,
            FamilyMedicine = 25,
            InternalMedicine = 26,
            PhysicalMedicineAndRehabilitation = 27,
            NuclearMedicine = 28,
            Pathology = 29,
            CriticalCareMedicine = 30,
            Geriatrics = 31,
            PreventiveMedicine = 32,
            OccupationalMedicine = 33,
            SleepMedicine = 34,
            SportsMedicine = 35,
            PalliativeCare = 36,
            IntegrativeMedicine = 37,
            TravelMedicine = 38,
            AddictionMedicine = 39,
            PainMedicine = 40,
            MedicalGenetics = 41,
            ClinicalPharmacology = 42,
            AerospaceMedicine = 43,
            ForensicMedicine = 44,
            RehabilitationMedicine = 45,
            CommunityMedicine = 46,
            DisasterMedicine = 47,
            TropicalMedicine = 48,
            VeterinaryMedicine = 49,
            Dentistry = 50,
            Podiatry = 51,
            Optometry = 52,
            Naturopathy = 53,
            Chiropractic = 54,
            Homeopathy = 55,
            Acupuncture = 56,
            Osteopathy = 57,
            GeneticCounseling = 58,
            MedicalWriting = 59,
            MedicalIllustration = 60,
            Other = 61
        }

        public enum RedisKeyEnum
        { 
            DoctorLoginInfo = 1,
            PatientLoginInfo =2
        }

    }
}