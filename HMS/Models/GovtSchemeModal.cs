using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
	public class GovtSchemeModal
	{
        public int GovBenefitsId { get; set; }
        public string HospitalType { get; set; }
        public string BenefitName { get; set; }
        public string Description { get; set; }
        public bool IsOPDValid { get; set; }
        public int DiscountPercentage { get; set; }
        public string HospitalName { get; set; }
        public int HospitalId { get; set; }
    }
}