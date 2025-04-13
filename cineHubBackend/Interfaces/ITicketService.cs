using cineHubBackend.DTOs.Session;
using cineHubBackend.DTOs;
using cineHubBackend.DTOs.Ticket;

namespace cineHubBackend.Interfaces
{
      public interface ITicketService
      {
        TicketResponseDto GetById(string id);
        void Create(CreateTicketDto ticketDto);
            
        void Delete(string id);
        IEnumerable<TicketSeatDto> GetReservedSeats(string sessionId);
        public Task<PaginationResponseDto<TicketResponseDto>> GetTicketsPagination(int page, int itemsPerPage, string userId);

      }
    
}
