﻿using AutoMapper;
using cineHubBackend.DTOs;
using cineHubBackend.DTOs.Cinema;
using cineHubBackend.DTOs.Session;
using cineHubBackend.Interfaces;
using cineHubBackend.Models;
using Microsoft.AspNetCore.Http;

namespace cineHubBackend.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepo;
        private readonly IMapper _mapper;

        public CinemaService(ICinemaRepository cinemaRepo, IMapper mapper)
        {
            _cinemaRepo = cinemaRepo;
            _mapper = mapper;
        }

        public CinemaResponseDto GetById(string id)
        {
            var cinema = _cinemaRepo.GetByID(id);
            return cinema == null ? null : _mapper.Map<CinemaResponseDto>(cinema);
        }

        public void Create(CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);
            cinema.Id = Guid.NewGuid().ToString();
            _cinemaRepo.Insert(cinema);
            _cinemaRepo.Save();
        }

        public void Update(string id, UpdateCinemaDto cinemaDto)
        {
            var cinema = _cinemaRepo.GetByID(id);
            if (cinema == null) return;
            _mapper.Map(cinemaDto, cinema);
            _cinemaRepo.Update(cinema);
            _cinemaRepo.Save();
        }

        public void Delete(string id)
        {
            var cinema = _cinemaRepo.GetByID(id);
            if (cinema == null) return;
            if (cinema.Auditoriums != null && cinema.Auditoriums.Any())
            {
                throw new InvalidOperationException("Cannot delete cinema because there are associated halls.");
            }
            _cinemaRepo.Delete(cinema);
            _cinemaRepo.Save();
        }

        public async Task<PaginationResponseDto<CinemaResponseDto>> GetAllPagination(int page, int itemsPerPage)
        {
            var cinema = await _cinemaRepo.GetAllPagination(page, itemsPerPage);
            return new PaginationResponseDto<CinemaResponseDto>(
               cinema.Total_pages, cinema.Total_results, cinema.Page,
               _mapper.Map<IEnumerable<CinemaResponseDto>>(cinema.Results)
             );
        }

    }
}
