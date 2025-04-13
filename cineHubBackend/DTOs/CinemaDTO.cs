﻿namespace cineHubBackend.DTOs.Cinema

{
    public class CinemaResponseDto
    {
        public string Id { get; set; }
        public string Location { get; set; }
    }

    public class CreateCinemaDto
    {
        public string Location { get; set; }
    }

    public class UpdateCinemaDto
    {
        public string Location { get; set; }
    }
}