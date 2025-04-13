﻿using AutoMapper;
using cineHubBackend.DTOs.Auditorium;
using cineHubBackend.DTOs.Cinema;
using cineHubBackend.DTOs.Session;
using cineHubBackend.DTOs.Ticket;
using cineHubBackend.Models;
using cineHubBackend.Models;

namespace cineHubBackend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Session
            CreateMap<Session, SessionResponseDto>()
               .ForMember(dest => dest.CinemaLocation, opt => opt.MapFrom(src => src.Cinema.Location))
               .ForMember(dest => dest.AuditoriumName, opt => opt.MapFrom(src => src.Auditorium.Name))
               .ForMember(dest => dest.FilmName, opt => opt.MapFrom(src => src.FilmName));

            CreateMap<CreateSessionDto, Session>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Cinema, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditorium, opt => opt.Ignore()) 
                .ForMember(dest => dest.Tickets, opt => opt.Ignore()); 

            CreateMap<UpdateSessionDto, Session>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.FilmId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CinemaId, opt => opt.Ignore()) 
                .ForMember(dest => dest.AuditoriumId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Cinema, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditorium, opt => opt.Ignore()) 
                .ForMember(dest => dest.Tickets, opt => opt.Ignore()); 

            // Cinema
            CreateMap<Cinema, CinemaResponseDto>();

            CreateMap<CreateCinemaDto, Cinema>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditoriums, opt => opt.Ignore()); 

            CreateMap<UpdateCinemaDto, Cinema>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Auditoriums, opt => opt.Ignore()); 

            // Auditorium
            CreateMap<Auditorium, AuditoriumResponseDto>()
                .ForMember(dest => dest.CinemaName, opt => opt.MapFrom(src => src.Cinema.Location));

            CreateMap<CreateAuditoriumDto, Auditorium>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Cinema, opt => opt.Ignore()) 
                .ForMember(dest => dest.Sessions, opt => opt.Ignore());

            CreateMap<UpdateAuditoriumDto, Auditorium>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CinemaId, opt => opt.Ignore())
                .ForMember(dest => dest.Cinema, opt => opt.Ignore())
                .ForMember(dest => dest.Sessions, opt => opt.Ignore());
            
            // Ticket
            CreateMap<Ticket, TicketResponseDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
               .ForMember(dest => dest.FilmName, opt => opt.MapFrom(src => src.Session.FilmName))
               .ForMember(dest => dest.CinemaLocation, opt => opt.MapFrom(src => src.Session.Cinema.Location))
               .ForMember(dest => dest.HallName, opt => opt.MapFrom(src => src.Session.Auditorium.Name))
               .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Session.StartTime))
               .ForMember(dest => dest.FormatType, opt => opt.MapFrom(src => src.Session.FormatType));

            CreateMap<Ticket, TicketSeatDto>()
               .ForMember(dest => dest.Row, opt => opt.MapFrom(src => src.RowNumber))
               .ForMember(dest => dest.Seat, opt => opt.MapFrom(src => src.SeatNumber));

            CreateMap<CreateTicketDto, Ticket>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore()) 
                .ForMember(dest => dest.Session, opt => opt.Ignore()); 
        }
    }
}
