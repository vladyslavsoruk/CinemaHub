﻿using AutoMapper;
using cineHubBackend.DTOs;
using cineHubBackend.DTOs.Auditorium;
using cineHubBackend.DTOs.Cinema;
using cineHubBackend.DTOs.Session;
using cineHubBackend.Interfaces;
using cineHubBackend.Models;
using Microsoft.AspNetCore.Http;

namespace cineHubBackend.Services
{
    public class HallService : IHallService
    {
        private readonly IHallRepository _hallRepo;
        private readonly ICinemaRepository _cinemaRepo;
        private readonly IMapper _mapper;
        public HallService(IHallRepository hallRepo, ICinemaRepository cinemaRepo, IMapper mapper)
        {
            _hallRepo = hallRepo;
            _cinemaRepo = cinemaRepo;
            _mapper = mapper;
        }

        public AuditoriumResponseDto GetById(string id)
        {
            var hall = _hallRepo.GetByID(id);
            return hall == null ? null : _mapper.Map<AuditoriumResponseDto>(hall);
        }

        public void Create(CreateAuditoriumDto hallDto)
        {
            var cinema = _cinemaRepo.GetByID(hallDto.CinemaId);
            if (cinema == null)
            {
                throw new Exception("Cinema with given ID not found.");
            }

            var hall = _mapper.Map<Auditorium>(hallDto);
            hall.Id = Guid.NewGuid().ToString();
            _hallRepo.Insert(hall);
            _hallRepo.Save();
        }

        public void Update(string id, UpdateAuditoriumDto hallDto)
        {
            var hall = _hallRepo.GetByID(id);
            if (hall == null) return;
            _mapper.Map(hallDto, hall);
            _hallRepo.Update(hall);
            _hallRepo.Save();
        }

        public void Delete(string id)
        {
            var hall = _hallRepo.GetByID(id);
            if (hall == null) return;
            if (hall.Sessions != null && hall.Sessions.Any())
            {
                throw new InvalidOperationException("Cannot delete hall because there are associated Sessions.");
            }
            _hallRepo.Delete(hall);
            _hallRepo.Save();
        }

        public async Task<PaginationResponseDto<AuditoriumResponseDto>> GetAllPagination(int page, int itemsPerPage, string? cinemaId)
        {
            var hall = await _hallRepo.GetAllPagination(page, itemsPerPage, cinemaId);
            return new PaginationResponseDto<AuditoriumResponseDto>(
               hall.Total_pages, hall.Total_results, hall.Page,
               _mapper.Map<IEnumerable<AuditoriumResponseDto>>(hall.Results)
             );
        }

    }
}
