using cineHubBackend.DBContext;
using cineHubBackend.DTOs;
using cineHubBackend.Interfaces;
using cineHubBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace cineHubBackend.Repositories
{
    public class HallRepository : GenericRepository<Auditorium>, IHallRepository
    {
        public HallRepository(CineDbContext context) : base(context)
        {
        }
        public async Task<PaginationResponseDto<Auditorium>> GetAllPagination(int page, int itemsPerPage, string? cinemaId)
        {
            IQueryable<Auditorium> query = dbSet
                .Include(h => h.Cinema);
            if (!string.IsNullOrEmpty(cinemaId))
                query = query.Where(a => a.Cinema.Id == cinemaId);
            int totalResults = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalResults / (double)itemsPerPage);
            var results = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            return new PaginationResponseDto<Auditorium>(totalPages, totalResults, page, results);
        }

    }
}
