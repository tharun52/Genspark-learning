using Microsoft.AspNetCore.Mvc;
using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Models.DTOs;




namespace FirstApi.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaitentController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PaitentController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
            if (patients.Count() == 0)
            {
                return NoContent();
            }
            return Ok(patients);
        }
        [HttpGet("{patientName}")]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatientByName(string patientName)
        {
            var patients = await _patientService.GetPatientByName(patientName);
            if (patients.Count() == 0)
            {
                return NoContent();
            }
            return Ok(patients);
        }
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient([FromBody] PatientAddRequestDto patient)
        {
            var new_patient = await _patientService.AddPatient(patient);
            if (new_patient == null)
            {
                return BadRequest("Error in adding Patient");
            }
            return Created("", new_patient);
        }
    }
}