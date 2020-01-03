using System.Collections.Generic;
using GreenHealth.Models;

namespace GreenHealth.ViewModels
{
    public class PatientDetailViewModel
    {
        public PatientDetailViewModel()
        {
            
        }
        
        public Patient Patient { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Attendance> Attendances { get; set; }
        public int CountAppointments { get; set; }
        public int CountAttendance { get; set; }
    }
}