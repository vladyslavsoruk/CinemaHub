using cineHubBackend.DTOs.Cinema;
using cineHubBackend.DTOs;
using cineHubBackend.Models;

namespace cineHubBackend.Interfaces
{
    public interface ICinemaRepository : IRepository<Cinema>
    {
        public Task<PaginationResponseDto<Cinema>> GetAllPagination(int page, int itemsPerPage);
    }
}
