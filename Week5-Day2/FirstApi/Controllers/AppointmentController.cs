using Microsoft.AspNetCore.Mvc;
using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAuthorizationService _authorizationService;

        public AppointmentController(IAppointmentService appointmentService,
                                     IAuthorizationService authorizationService)
        {
            _appointmentService = appointmentService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            if (appointments.Count() == 0)
            {
                return NoContent();
            }
            return Ok(appointments);
        }
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment([FromBody] AppointmentAddRequestDto appointment)
        {
            var new_appointment = await _appointmentService.AddAppointment(appointment);
            if (new_appointment == null)
            {
                return BadRequest("Error in adding Appointment");
            }
            return Created("", new_appointment);
        }

        // [Authorize(Roles = "Doctor")]
        [HttpDelete("{appointmentNumber}")]
        public async Task<ActionResult<Appointment>> SoftDeleteAppointment(string appointmentNumber)
        {
            // Step 1: Retrieve the appointment
            var appointment = await _appointmentService.GetAppointmentByNumber(appointmentNumber);
            if (appointment == null)
            {
                return NotFound("Appointment not found");
            }

            // Step 2: Perform resource-based authorization
            var authResult = await _authorizationService.AuthorizeAsync(User, appointment, "DoctorCanDeleteAppointment");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            // Step 3: Authorized => perform soft delete
            await _appointmentService.SoftDeleteAppointment(appointmentNumber);

            return NoContent();
        }

    }
}