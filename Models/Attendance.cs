using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenHealth.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string ClinicRemarks { get; set; }
        public string Diagnosis { get; set; }
        public string SecondDiagnosis { get; set; }
        public string ThirdDiagnosis { get; set; }
        public string Therapy { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
