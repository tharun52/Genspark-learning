﻿using FirstApi.Contexts;
using Microsoft.EntityFrameworkCore;
using FirstApi.Models;
using FirstApi.Repositories;
using FirstApi.Interfaces;

namespace FirstApi.Test;


public class Tests
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

    [Test]
    public async Task Test1()
    {
        //arrange
        var email = " test@gmail.com";
        var password = System.Text.Encoding.UTF8.GetBytes("test123");
        var key = Guid.NewGuid().ToByteArray();
        var user = new User
        {
            Username = email,
            Password = password,
            HashKey = key,
            Role = "Doctor"
        };
        _context.Add(user);
        await _context.SaveChangesAsync();
        var doctor = new Doctor
        {
            Name = "test",
            YearsOfExperience = 2,
            Email = email
        };
        IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
        //action
        var result = await _doctorRepository.Add(doctor);
        //assert
        Assert.That(result, Is.Not.Null, "Doctor IS not addeed");
        Assert.That(result.Id, Is.EqualTo(1));
    }
    [TestCase(1)]
    [TestCase(2)]
    public async Task GetDoctorPassTest(int id)
    {
        //arrange
        var email = " test@gmail.com";
        var password = System.Text.Encoding.UTF8.GetBytes("test123");
        var key = Guid.NewGuid().ToByteArray();
        var user = new User
        {
            Username = email,
            Password = password,
            HashKey = key,
            Role = "Doctor"
        };
        // _context.Add(user);
        await _context.SaveChangesAsync();
        var doctor = new Doctor
        {
            Name = "test",
            YearsOfExperience = 2,
            Email = email
        };
        IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
        //action
        // await _doctorRepository.Add(doctor);

        //action
        var result = _doctorRepository.Get(id);
        //assert
        Assert.That(result.Id, Is.EqualTo(id));

    }

    [TestCase(3)]
    public async Task GetDoctorExceptionTest(int id)
    {
        //arrange
        var email = " test@gmail.com";
        var password = System.Text.Encoding.UTF8.GetBytes("test123");
        var key = Guid.NewGuid().ToByteArray();
        var user = new User
        {
            Username = email,
            Password = password,
            HashKey = key,
            Role = "Doctor"
        };
        // _context.Add(user);
        await _context.SaveChangesAsync();
        var doctor = new Doctor
        {
            Name = "test",
            YearsOfExperience = 2,
            Email = email
        };
        IRepository<int, Doctor> _doctorRepository = new DoctorRepository(_context);
        //action
        // await _doctorRepository.Add(doctor);
        //action

        var ex = Assert.ThrowsAsync<Exception>(() =>  _doctorRepository.Get(id));
        //assert
        // Assert.That(() => _doctorRepository.Get(id), Throws.TypeOf<Exception>());
        Assert.That(ex.Message,Is.EqualTo("No doctor with the given ID"));              
    }

    [TearDown]
    public void TearDown() 
    {
        _context.Dispose();
    }
}
