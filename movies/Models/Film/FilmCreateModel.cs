﻿using Newtonsoft.Json;

namespace movies.Models.Film
{
    public class FilmCreateModel
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("directorName")]
        public string DirectorName { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("ratingTypeName")]
        public string RatingTypeName { get; set; }
        
        [JsonProperty("ratingValue")]
        public decimal RatingValue { get; set; }
    }
}
