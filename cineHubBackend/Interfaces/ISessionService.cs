using cineHubBackend.DTOs;
using cineHubBackend.DTOs.Session;
using cineHubBackend.Models;

namespace cineHubBackend.Interfaces
{
    public interface ISessionService
    {
        SessionResponseDto GetById(string id);
        void Create(CreateSessionDto sessionDto);
        void Update(string id, UpdateSessionDto sessionDto);
        void Delete(string id);
        Session GetSessionEntityById(string id);
        int GetRowCount(string id);
        int GetSeatsCount(string id);
        Task<PaginationResponseDto<SessionResponseDto>> GetSessionsPagination(int page, int itemsPerPage, string? cinemaId, string? hallId, int? filmId, DateTime? date);
    }
}