using cineHubBackend.DTOs;
using cineHubBackend.DTOs.Cinema;

namespace cineHubBackend.Interfaces
{
    public interface ICinemaService
    {
        Task<PaginationResponseDto<CinemaResponseDto>> GetAllPagination(int page, int itemsPerPage);
        CinemaResponseDto GetById(string id);
        void Create(CreateCinemaDto cinemaDto);
        void Update(string id, UpdateCinemaDto cinemaDto);
        void Delete(string id);
    }
}
