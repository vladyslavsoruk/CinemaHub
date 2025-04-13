using cineHubBackend.DTOs;
using cineHubBackend.DTOs.Auditorium;
using cineHubBackend.DTOs.Cinema;
using cineHubBackend.Models;

namespace cineHubBackend.Interfaces
{
    public interface IHallService
    {
        Task<PaginationResponseDto<AuditoriumResponseDto>> GetAllPagination(int page, int itemsPerPage, string? cinemaId);
        AuditoriumResponseDto GetById(string id);
        void Create(CreateAuditoriumDto auditoriumDto);
        void Update(string id, UpdateAuditoriumDto auditoriumDto);
        void Delete(string id);
    }
}
