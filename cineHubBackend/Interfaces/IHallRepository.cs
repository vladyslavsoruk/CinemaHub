using cineHubBackend.DTOs;
using cineHubBackend.Models;

namespace cineHubBackend.Interfaces
{
    public interface IHallRepository : IRepository<Auditorium>
    {
        public Task<PaginationResponseDto<Auditorium>> GetAllPagination(int page, int itemsPerPage, string? cinemaId);
    }
}
