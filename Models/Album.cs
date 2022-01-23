using System.Collections.Generic;

namespace MusicApi.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int SongCount { get; set; }
        public string Duration { get; set; }
        public List<Song> Songs { get; set; }
    }
}
