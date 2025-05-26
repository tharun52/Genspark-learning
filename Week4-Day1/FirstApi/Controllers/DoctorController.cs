using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class DoctorController : ControllerBase
{
    static List<Doctor> doctors = new List<Doctor>
    {
        new Doctor{Id=101, Name="Tharun", Age = 40, Specialization="ETE"},
        new Doctor{Id=102, Name="Cal", Age = 50, Specialization="Cancer" },
    };

    [HttpGet]
    public ActionResult<IEnumerable<Doctor>> GetDoctors()
    {
        if (doctors.Count == 0)
        {
            return NoContent();
        }
        return Ok(doctors);
    }

    [HttpPost]
    public ActionResult<List<Doctor>> PostDoctor(Doctor doctor)
    {
        if (doctors.FirstOrDefault(p => p.Id == doctor.Id) != null)
        {
            return BadRequest("Doctor already exists");
        }
        if (doctor.Id == null || doctor.Age < 0 || string.IsNullOrWhiteSpace(doctor.Name) || string.IsNullOrWhiteSpace(doctor.Specialization))
        {
            return BadRequest();
        }
        doctors.Add(doctor);
        return Created("", doctors);
    }

    [HttpDelete]
    public ActionResult<List<Doctor>> DeleteById(int doctorId)
    {
        if (doctors.FirstOrDefault(p => p.Id == doctorId) == null)
        {
            return BadRequest("Doctor does not exists");
        }
        doctors = doctors.Where(d => d.Id != doctorId).ToList();
        return Ok(doctors);
    }

    [HttpPut]
    public ActionResult<List<Doctor>> UpdateById(int doctorId, string doctorName,int doctorAge, string doctorSpecialization)
    {   
        var doctor = doctors.FirstOrDefault(d => d.Id == doctorId);
        if (doctor == null)
        {
            return NoContent();
        }
        if (doctorAge == null || doctorAge < 0 || string.IsNullOrWhiteSpace(doctorName) || string.IsNullOrWhiteSpace(doctorSpecialization))
        {
            return BadRequest("Invalid Input");
        }
        doctor.Name = doctorName;
        doctor.Age = doctorAge;
        doctor.Specialization = doctorSpecialization;
        return Created("", doctors);
    }
}