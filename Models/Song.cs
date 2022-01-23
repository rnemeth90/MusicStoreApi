namespace MusicApi.Models
{
    // represents a song
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }

        public string Duration { get; set; }
    }
}
