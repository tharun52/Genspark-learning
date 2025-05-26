using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]

public class PaitentController : ControllerBase
{
    static List<Paitent> paitents = new List<Paitent>{
        new Paitent { Id = 1, Name = "Tharun", DoctorName = "Dr. House", Age = 30, Diagnosis = "All well"}
    };

    [HttpGet]
    public ActionResult<IEnumerable<Paitent>> GetGreet()
    {
        if (paitents.Count == 0)
        {
            return NoContent();
        }
        return Ok(paitents);
    }

    [HttpPost]
    public ActionResult<List<Paitent>> PostPaitent(Paitent paitent)
    {
        if (paitents.FirstOrDefault(p => p.Id == paitent.Id) != null)
        {
            return BadRequest("Patient already exists");
        }
        if (paitent.Id == null || string.IsNullOrWhiteSpace(paitent.Name) || string.IsNullOrWhiteSpace(paitent.DoctorName) || paitent.Age < 0)
        {
            return BadRequest("Invalid Input");
        }
        paitents.Add(paitent);
        return Created("", paitents);
    }

    [HttpDelete]
    public ActionResult<List<Paitent>> DeleteById(int paitentId)
    {
        if (paitents.FirstOrDefault(p => p.Id == paitentId) == null)
        {
            return BadRequest("Patient does not exists");
        }
        paitents = paitents.Where(patient => patient.Id != paitentId).ToList();
        return Ok(paitents);
    }

    [HttpPut]
    public ActionResult<List<Paitent>> UpdateById(int paitentId, string paitentName, string paitentDoctorName, int patientAge, string paitentDiagnosis)
    {
        var paitent = paitents.FirstOrDefault(p => p.Id == paitentId);
        if (paitent == null)
        {
            return NoContent();
        }
        if (patientAge == null || patientAge < 0 || string.IsNullOrWhiteSpace(paitentName) || string.IsNullOrWhiteSpace(paitentDoctorName) || string.IsNullOrWhiteSpace(paitentDiagnosis))
        {
            return BadRequest("Invalid Input"); 
        }
        paitent.Name = paitentName;
        paitent.DoctorName = paitentDoctorName;
        paitent.Age = patientAge;
        paitent.Diagnosis = paitentDiagnosis;
        return Created("", paitents);
    }
}
