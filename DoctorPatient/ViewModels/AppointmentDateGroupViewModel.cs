using System;
using System.ComponentModel.DataAnnotations;

namespace GreenHealth.ViewModels
{
    public class AppointmentDateGroupViewModel
    {
        [DataType(DataType.Date)]
        public DateTime? AppointmentDate { get; set; }

        public int PatientCount { get; set; }
    }
}