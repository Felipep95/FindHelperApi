using FindHelperApi.Data;
using FindHelperApi.Helper.CustomExceptions;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO.DoctorDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FindHelperApi.Services
{
    public class DoctorService
    {
        private readonly FindHelperApiContext _context;

        public DoctorService(FindHelperApiContext context)
        {
            _context = context;
        }

        public async Task<Doctor> InsertAsync(CREATEDoctorDTO doctorDto)
        {
            var doctor = new Doctor();

            doctor.Name = doctorDto.Name;
            doctor.Email = doctorDto.Email;
            doctor.Experience = doctorDto.Experience;
            doctor.CRM = doctorDto.CRM;

            _context.Add(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<List<Doctor>> FindAllAsync() => await _context.Doctors.ToListAsync();

        public async Task<Doctor> FindByIdAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Médico não encontrado");

            return doctor;
        }
    }
}
