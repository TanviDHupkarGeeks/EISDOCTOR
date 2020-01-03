using AutoMapper;
using GreenHealth;
using GreenHealth.Dto;
using GreenHealth.Models;
using System.Linq;
using System.Web.Http;

namespace GreenHealth.Controllers.Api
{
    public class PatientsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IHttpActionResult GetPatients()
        {
            var patientsQuery = _unitOfWork.Patients.GetPatients();


            //var patientDto = Mapper.Map<PatientDto>(patientsQuery);
            //                             //.Select(v => Mapper.Map<Patient, PatientDto>(v));

            return Ok(patientsQuery);

        }


        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var patient = _unitOfWork.Patients.GetPatient(id);
            _unitOfWork.Patients.Remove(patient);
            _unitOfWork.Complete();
            return Ok();
        }

    }
}
