using Microsoft.AspNetCore.Mvc;
using FirstApi.Repositories;
using FirstApi.Services;
using FirstApi.Interfaces;
using FirstApi.Models;


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
    public ActionResult<IEnumerable<Doctor>> GetDoctors()
    {
        var doctors = doctorService.GetAllDoctors();
        if (doctors.Count == 0)
        {
            return NoContent();
        }
        return Ok(doctors);
    }

    [HttpPost]
    public ActionResult<List<Doctor>> PostDoctor([FromBody] Doctor doctor)
    {
        int id = doctorService.AddDoctor(doctor);
        if (id == -1)
        {
            return BadRequest("Error in adding Doctor");
        }
        return Created("", doctorService.GetAllDoctors());
    }


    [HttpDelete]
    public ActionResult<List<Doctor>> DeleteById(int doctorId)
    {
        int id = doctorService.DeleteDoctor(doctorId);
        if (id == -1)
        {
            return BadRequest("Error in Deleting Doctor");
        }
        return Ok(doctorService.GetAllDoctors());
    }
    [HttpPut("{doctorId}")]
    public ActionResult<Doctor> UpdateById(int doctorId, [FromBody] Doctor updatedDoctor)
    {
        if (doctorId != updatedDoctor.Id)
        {
            return BadRequest("Doctor ID in URL and body do not match");
        }

        int id = doctorService.UpdateDoctor(updatedDoctor);

        if (id == -1)
        {
            return BadRequest("Error in Updating Doctor");
        }

        // Return the updated doctor with 200 OK
        return Ok(updatedDoctor);
    }

}