using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MusicApi.Models
{
    // represents a song
    public class Song
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title cannot be null or empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Language cannot be null or empty")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Duration cannot be null or empty")]
        public string Duration { get; set; }
    }
}
