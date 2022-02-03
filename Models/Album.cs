using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Album name cannot be null or empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Artist cannot be null or empty")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Song count cannot be null or empty")]
        public int SongCount { get; set; }

        [Required(ErrorMessage = "Duration cannot be null or empty")]
        public string Duration { get; set; }
        public string AlbumCoverUrl { get; set; }
        public ICollection<Song> Songs { get; set; }

        [NotMapped]
        public IFormFile AlbumCover { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
