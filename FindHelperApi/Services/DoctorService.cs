using FindHelperApi.Data;
using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task InsertAsync(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Doctor>> FindAllAsync() => await _context.Doctors.ToListAsync();

        public async Task<Doctor> FindByIdAsync(int id) => await _context.Doctors.FindAsync(id);
        
        

    }
}
