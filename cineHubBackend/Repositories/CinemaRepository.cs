using cineHubBackend.DBContext;
using cineHubBackend.DTOs;
using cineHubBackend.Interfaces;
using cineHubBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace cineHubBackend.Repositories
{
    public class CinemaRepository : GenericRepository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(CineDbContext context) : base(context)
        {
        }

        public async Task<PaginationResponseDto<Cinema>> GetAllPagination(int page, int itemsPerPage)
        {
            int totalResults = await dbSet.CountAsync();
            int totalPages = (int)Math.Ceiling(totalResults / (double)itemsPerPage);
            var results = await dbSet.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();
            return new PaginationResponseDto<Cinema>(totalPages, totalResults, page, results);
        }
    }
}
