using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Music.Api.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Artist Name cannot be null")]
        public string Name { get; set; }

        public string Gender { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public ICollection<Album> Albums { get; set;}
        public ICollection<Song> Songs { get; set;}
    }
}
