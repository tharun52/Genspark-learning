using FirstApi.Models;
using FirstApi.Models.DTOs;
using FirstApi.Services;
using FirstApi.Interfaces;
using FirstApi.Misc;
using Moq;
using AutoMapper;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Test
{
    public class PatientServiceTest
    {
        [Test]
        public async Task TestGetAllPatients_ReturnsPatients()
        {
            var patients = new List<Patient> { new Patient { Id = 1, Name = "John" } };
            var patientRepositoryMock = new Mock<IRepository<int, Patient>>();
            patientRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(patients);
            var service = new PatientService(
                patientRepositoryMock.Object,
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            var result = await service.GetAllPatients();
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task TestGetAllPatients_ReturnsEmpty()
        {
            var patientRepositoryMock = new Mock<IRepository<int, Patient>>();
            patientRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<Patient>());
            var service = new PatientService(
                patientRepositoryMock.Object,
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            var result = await service.GetAllPatients();
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task TestGetPatientByName_ReturnsMatches()
        {
            var patients = new List<Patient> {
                new Patient { Id = 1, Name = "Alice" },
                new Patient { Id = 2, Name = "Bob" }
            };
            var patientRepositoryMock = new Mock<IRepository<int, Patient>>();
            patientRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(patients);
            var service = new PatientService(
                patientRepositoryMock.Object,
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            var result = await service.GetPatientByName("Alice");
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Alice"));
        }

        [Test]
        public async Task TestGetPatientByName_ReturnsEmpty()
        {
            var patients = new List<Patient> { new Patient { Id = 1, Name = "Alice" } };
            var patientRepositoryMock = new Mock<IRepository<int, Patient>>();
            patientRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(patients);
            var service = new PatientService(
                patientRepositoryMock.Object,
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            var result = await service.GetPatientByName("Bob");
            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
