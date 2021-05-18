using FindHelperApi.Data;
using FindHelperApi.Models;
using FindHelperApi.Models.DTO;
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

        public async Task<Area> InsertAsync(CREATEAreaDTO areaDto)
        {
            var createdArea = new Area();

            createdArea.Name = areaDto.Name;
             
            _context.Add(createdArea);
            await _context.SaveChangesAsync();

            return createdArea;
        }

        public async Task<List<Area>> FindAllAsync() =>  await _context.Areas.ToListAsync();
        
    }
}
