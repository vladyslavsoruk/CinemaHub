using cineHubBackend.DTOs;
using cineHubBackend.Models;

namespace cineHubBackend.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        IEnumerable<Ticket> GetTicketsBySession(string sessionId);
        public Task<PaginationResponseDto<Ticket>> GetTicketsPagination(int page, int itemsPerPage, string userId);

    }
}
