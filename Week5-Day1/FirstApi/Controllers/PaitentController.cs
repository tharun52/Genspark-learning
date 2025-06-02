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