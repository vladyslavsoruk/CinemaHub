using cineHubBackend.DBContext;
using cineHubBackend.DTOs;
using cineHubBackend.Interfaces;
using cineHubBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace cineHubBackend.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(CineDbContext context) : base(context)
        {
        }

        public async Task<PaginationResponseDto<Ticket>> GetTicketsPagination(int page, int itemsPerPage, string userId)
        {
            IQueryable<Ticket> query = dbSet
                .Include(s => s.Session)
                .Include(s => s.Session.Cinema)
                .Include(s => s.Session.Auditorium);
            query = query.Where(s => s.UserId == userId);
    
            query = query.OrderBy(s => s.Session.StartTime);

            int totalResults = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalResults / (double)itemsPerPage);
            var results = await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            return new PaginationResponseDto<Ticket>(totalPages, totalResults, page, results);
        }

        public override IEnumerable<Ticket> GetAll()
        {
            return Get(includeProperties: "User,Session");
        }
        public override Ticket GetByID(object id)
        {
            return Get(
                filter: t => t.Id == id,
                includeProperties: "User,Session"
            ).FirstOrDefault();
        }

        public IEnumerable<Ticket> GetTicketsBySession(string sessionId)
        {
            return Get(filter: s => s.SessionId == sessionId);
        }
    }
}
