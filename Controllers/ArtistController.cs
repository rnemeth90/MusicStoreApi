using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.Api.Data;
using Music.Api.Helpers;
using Music.Api.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Music.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public ArtistController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST api/<ArtistController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artist artist)
        {
            var imageUrl = FileHelper.GetFileUrl(artist.Image, "artist-headshots");
            artist.ImageUrl = imageUrl;
            await _dbContext.Artists.AddAsync(artist);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }


        // GET api/<ArtistController>
        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            var artists = await (from artist in _dbContext.Artists
                        select new
                        {
                            Id = artist.Id,
                            Name = artist.Name,
                            ImageUrl = artist.ImageUrl
                        }).ToListAsync();
            return Ok(artists);
        }

        // GET /api/<
        [HttpGet("[action]")]
        public async Task<IActionResult> GetArtistDetails(int artistId)
        {
            var artistDetails = await _dbContext.Artists.Where(a=>a.Id == artistId).Include(a => a.Songs).ToListAsync();
            return Ok(artistDetails);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var artist = await _dbContext.Artists.FindAsync(id);
            _dbContext.Artists.Remove(artist); 
            _dbContext.SaveChanges();
            return Ok($"{artist.Id} removed");
        }
    }
}
