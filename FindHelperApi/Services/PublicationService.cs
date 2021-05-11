using FindHelperApi.Data;
using FindHelperApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHelperApi.Services
{
    public class PublicationService
    {
        private readonly FindHelperApiContext _context;

        public PublicationService(FindHelperApiContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Publication publication)
        {
            _context.Add(publication);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Publication>> FindAllAsync() => await _context.Publications.ToListAsync();
    }
}
