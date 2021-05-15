using FindHelperApi.Data;
using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHelperApi.Services
{
    public class AreaService
    {
        private readonly FindHelperApiContext _context;

        public AreaService(FindHelperApiContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Area area)
        {
             _context.Add(area);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Area>> FindAllAsync() =>  await _context.Areas.ToListAsync();
        
    }
}
