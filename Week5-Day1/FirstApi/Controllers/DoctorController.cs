using Microsoft.AspNetCore.Mvc;
using FirstApi.Interfaces;
using FirstApi.Models;
using FirstApi.Models.DTOs;


[ApiController]
[Route("api/[controller]")]


public class DoctorController : ControllerBase
{
    private readonly IDoctorService doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
    {
        var doctors = await doctorService.GetAllDoctors();
        if (doctors.Count == 0)
        {
            return NoContent();
        }
        return Ok(doctors);
    }

    [HttpGet("{doctorName}")]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorByName(string doctorName)
    {
        var doctors = await doctorService.GetDoctByName(doctorName);
        if (doctors.Count == 0)
        {
            return NoContent();
        }
        return Ok(doctors);
    }
    [HttpPost]
    public async Task<ActionResult<Doctor>> PostDoctor([FromBody] DoctorAddRequestDto doctor)
    {
        var new_doctor = await doctorService.AddDoctor(doctor);
        if (new_doctor == null)
        {
            return BadRequest("Error in adding Doctor");
        }
        return Created("", new_doctor);
    }
}