using docuShare.Interfaces;
using docuShare.Models;
using docuShare.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace docuShare.Controller
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllEmployees()
        {
            var users = await _employeeService.GetAllEmployees();
            return Ok(users);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<User>> GetEmployeeId(string name)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByName(name);
                return Ok(employee);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] AddEmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("User data cannot be null");
            }
            try
            {
                var employee = await _employeeService.AddEmployee(employeeDto);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}