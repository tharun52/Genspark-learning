using Microsoft.AspNetCore.Authorization;
using FirstApi.Interfaces;
using FirstApi.Models;
using System.Security.Claims;


namespace FirstApi.Policies
{
    public class DoctorCanDeleteAppointmentHandler: AuthorizationHandler<DoctorCanDeleteAppointmentRequirement, Appointment>
    {
        private readonly IDoctorService _doctorService;

        public DoctorCanDeleteAppointmentHandler(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DoctorCanDeleteAppointmentRequirement requirement,
            Appointment appointment)
        {
            // var doctorEmail = context.User.FindFirst("nameid")?.Value;
            var doctorEmail = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"doctorEmail: {doctorEmail}");
            if (string.IsNullOrEmpty(doctorEmail)) return;

            var doctor = (await _doctorService.GetAllDoctors()).FirstOrDefault(d => d.Email == doctorEmail);
            System.Console.WriteLine($"Docotor found : {doctor.Id} {doctor.YearsOfExperience}");
            if (doctor == null) return;

            if (appointment.DoctorId != doctor.Id) return;

            if (doctor.YearsOfExperience > 3)
            {
                context.Succeed(requirement);
            }
        }
    }
}