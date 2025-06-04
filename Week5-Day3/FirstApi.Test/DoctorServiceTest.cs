using FirstApi.Contexts;
using FirstApi.Models;
using FirstApi.Repositories;
using FirstApi.Interfaces;
using FirstApi.Services;
using FirstApi.Models.DTOs;
using FirstApi.Models.DTOs;
using FirstApi.Misc;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;

namespace FirstApi.Test
{
    public class DoctorServiceTest
    {
        private ClinicContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ClinicContext>()
                                .UseInMemoryDatabase("TestDb")
                                .Options;
            _context = new ClinicContext(options);
        }
        

        [TestCase("neurology")]
        public async Task TestGetDoctorBySpeciality(string speciality)
        {
            Mock<DoctorRepository> doctorRepositoryMock = new Mock<DoctorRepository>(_context);
            Mock<SpecialityRepository> specialityRepositoryMock = new(_context);
            Mock<DoctorSpecialityRepository> doctorSpecialityRepositoryMock = new(_context);
            Mock<UserRepository> userRepositoryMock = new(_context);
            Mock<OtherFuncinalitiesImplementation> otherContextFunctionitiesMock = new(_context);
            Mock<EncryptionService> encryptionServiceMock = new();
            Mock<IMapper> mapperMock = new();

            otherContextFunctionitiesMock.Setup(ocf => ocf.GetDoctorsBySpeciality(It.IsAny<string>()))
                                        .ReturnsAsync((string specilaity) => new List<DoctorsBySpecialityResponseDto>{
                                    new DoctorsBySpecialityResponseDto
                                            {
                                                Dname = "test",
                                                Yoe = 2,
                                                Id=1
                                            }
                                });
            IDoctorService doctorService = new DoctorService(doctorRepositoryMock.Object,
                                                            specialityRepositoryMock.Object,
                                                            doctorSpecialityRepositoryMock.Object,
                                                            userRepositoryMock.Object,
                                                            otherContextFunctionitiesMock.Object,
                                                            encryptionServiceMock.Object,
                                                            mapperMock.Object);


            //Assert.That(doctorService, Is.Not.Null);
            //Action
            var result = await doctorService.GetDoctorsBySpeciality(speciality);
            //Assert
            Assert.That(result.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task TestGetAllDoctors_ReturnsDoctors()
        {
            var doctors = new List<Doctor> { new Doctor { Id = 1, Name = "John" } };
            var doctorRepositoryMock = new Mock<IRepository<int, Doctor>>();
            doctorRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(doctors);
            var service = new DoctorService(
                doctorRepositoryMock.Object,
                Mock.Of<IRepository<int, Speciality>>(),
                Mock.Of<IRepository<int, DoctorSpeciality>>(),
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IOtherContextFunctionities>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            var result = await service.GetAllDoctors();
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestGetAllDoctors_ThrowsException_WhenEmpty()
        {
            var doctorRepositoryMock = new Mock<IRepository<int, Doctor>>();
            doctorRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<Doctor>());
            var service = new DoctorService(
                doctorRepositoryMock.Object,
                Mock.Of<IRepository<int, Speciality>>(),
                Mock.Of<IRepository<int, DoctorSpeciality>>(),
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IOtherContextFunctionities>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            Assert.ThrowsAsync<Exception>(async () => await service.GetAllDoctors());
        }

        [Test]
        public async Task TestGetDoctByName_ReturnsDoctors()
        {
            var doctors = new List<Doctor> { new Doctor { Id = 1, Name = "Alice" }, new Doctor { Id = 2, Name = "Bob" } };
            var doctorRepositoryMock = new Mock<IRepository<int, Doctor>>();
            doctorRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(doctors);
            var service = new DoctorService(
                doctorRepositoryMock.Object,
                Mock.Of<IRepository<int, Speciality>>(),
                Mock.Of<IRepository<int, DoctorSpeciality>>(),
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IOtherContextFunctionities>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            var result = await service.GetDoctByName("Alice");
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Alice"));
        }

        [Test]
        public void TestGetDoctByName_ThrowsException_WhenNotFound()
        {
            var doctors = new List<Doctor> { new Doctor { Id = 1, Name = "Alice" } };
            var doctorRepositoryMock = new Mock<IRepository<int, Doctor>>();
            doctorRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(doctors);
            var service = new DoctorService(
                doctorRepositoryMock.Object,
                Mock.Of<IRepository<int, Speciality>>(),
                Mock.Of<IRepository<int, DoctorSpeciality>>(),
                Mock.Of<IRepository<string, User>>(),
                Mock.Of<IOtherContextFunctionities>(),
                Mock.Of<IEncryptionService>(),
                Mock.Of<IMapper>()
            );
            Assert.ThrowsAsync<Exception>(async () => await service.GetDoctByName("Bob"));
        }

        [Test]
        public void TestAddDoctor_ThrowsException_OnError()
        {
            var doctorDto = new DoctorAddRequestDto { Name = "Test", Password = "pass", Specialities = new List<SpecialityAddRequestDto>() };
            var userRepositoryMock = new Mock<IRepository<string, User>>();
            userRepositoryMock.Setup(r => r.Add(It.IsAny<User>())).ThrowsAsync(new Exception("fail"));
            var doctorRepositoryMock = new Mock<IRepository<int, Doctor>>();
            var encryptionServiceMock = new Mock<IEncryptionService>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<DoctorAddRequestDto, User>(It.IsAny<DoctorAddRequestDto>())).Returns(new User());
            var service = new DoctorService(
                doctorRepositoryMock.Object,
                Mock.Of<IRepository<int, Speciality>>(),
                Mock.Of<IRepository<int, DoctorSpeciality>>(),
                userRepositoryMock.Object,
                Mock.Of<IOtherContextFunctionities>(),
                encryptionServiceMock.Object,
                mapperMock.Object
            );
            Assert.ThrowsAsync<Exception>(async () => await service.AddDoctor(doctorDto));
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

    }
}