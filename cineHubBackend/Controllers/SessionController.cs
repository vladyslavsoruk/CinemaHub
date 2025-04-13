﻿using cineHubBackend.DTOs.Session;
using cineHubBackend.Interfaces;
using cineHubBackend.Models;
using cineHubBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;

namespace cineHubBackend.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagination([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10, [FromQuery] string? cinemaId = null, [FromQuery] string? hallId = null, [FromQuery] int? filmId = null, [FromQuery] DateTime? date = null)
        {
            if (page < 1 || itemsPerPage < 1)
                return BadRequest(new { message = "Page number and items per page must be greater than 0." });

            var sessions = await _sessionService.GetSessionsPagination(page, itemsPerPage, cinemaId, hallId, filmId, date);
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var session = _sessionService.GetById(id);
           
            if (session == null)
                return NotFound(new { message = "Session not found" });

            return Ok(session);
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Create([FromBody] CreateSessionsDto sessionsDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var endDate = sessionsDto.EndDate;
            if (!endDate.HasValue)
                endDate = sessionsDto.StartDateTime;
            Console.WriteLine(endDate);
            var i = 0;
            for (DateTime day = sessionsDto.StartDateTime; day <= endDate; day = day.AddDays(1))
            {
                _sessionService.Create(new CreateSessionDto
                {
                    StartTime = day,
                    EndTime = day.AddMinutes(sessionsDto.Runtime),
                    FormatType = sessionsDto.FormatType,
                    Price = sessionsDto.Price,
                    FilmId = sessionsDto.FilmId,
                    FilmName = sessionsDto.FilmName,
                    CinemaId = sessionsDto.CinemaId,
                    AuditoriumId = sessionsDto.AuditoriumId,
                });
                i++;
            }
            return Ok(new { message = i.ToString() + " sessions created" });
        }

        [HttpPut("{id}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Update([FromRoute] string id, [FromBody] UpdateSessionRequestDto sessionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _sessionService.Update(id, new UpdateSessionDto
            {
                StartTime = sessionDto.StartTime,
                EndTime = sessionDto.StartTime.AddMinutes(sessionDto.Runtime),
                FormatType = sessionDto.FormatType,
                Price = sessionDto.Price,
                FilmId = sessionDto.FilmId,
                FilmName = sessionDto.FilmName,
            });
            return Ok(new { message = "Session updated" });
        }

        [HttpDelete("{id}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult Delete([FromRoute] string id)
        {
            _sessionService.Delete(id);
            return Ok(new { message = "Session deleted" });
        }
       
        [HttpGet("{id}/available-seats")]
        public IActionResult GetAvailableSeats([FromRoute] string id)
        {
            var session = _sessionService.GetSessionEntityById(id);
            
            if (session == null)
                return NotFound(new { message = "Session not found" });

            var auditorium = session.Auditorium;
           
            if (auditorium == null)
                return NotFound(new { message = "Auditorium not found" });
          
            var allSeats = new List<(int row, int seat)>();
            for (int r = 1; r <= auditorium.RowCount; r++)
            {
                for (int s = 1; s <= auditorium.SeatsPerRow; s++)
                {
                    allSeats.Add((r, s));
                }
            }
          
            var occupiedSeats = session.Tickets?.Select(t => (t.RowNumber, t.SeatNumber)).ToHashSet() ?? new HashSet<(int, int)>();
            var availableSeats = allSeats.Where(seat => !occupiedSeats.Contains(seat)).ToList();
            var formattedSeats = availableSeats.Select(s => new { row = s.Item1, seat = s.Item2 }).ToList();
            var json = JsonConvert.SerializeObject(formattedSeats, Formatting.Indented);

            return Ok(json);
        }
    }
}