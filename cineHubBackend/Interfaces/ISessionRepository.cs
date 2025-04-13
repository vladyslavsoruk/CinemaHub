using cineHubBackend.DTOs;
using cineHubBackend.Models;

namespace cineHubBackend.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        public Task<PaginationResponseDto<Session>> GetSessionsPagination(int page, int itemsPerPage, string? cinemaId, string? hallId, int? filmId, DateTime? date);
    }
}
