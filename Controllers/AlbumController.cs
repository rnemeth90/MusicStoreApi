using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public AlbumController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: api/<AlbumController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Albums.ToListAsync());
        }

        // GET api/<AlbumController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var album = await _dbContext.Albums.FindAsync(id);
            if (album == null)
                return NotFound();
            return Ok(album);
        }

        // POST api/<AlbumController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Album album)
        {
            await _dbContext.AddAsync(album);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<AlbumController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Album albumObject)
        {
            var album = await _dbContext.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            else
            {
                album.Name = albumObject.Name;
                album.Artist = albumObject.Artist;
                album.SongCount = albumObject.SongCount;   
                album.Duration = albumObject.Duration;
                album.Songs = albumObject.Songs;
                await _dbContext.SaveChangesAsync();
                return Ok($"Updated album with ID: {id}");
            }
        }

        // DELETE api/<AlbumController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _dbContext.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Albums.Remove(album);
                await _dbContext.SaveChangesAsync();
                return Ok($"Removed album with id: {id}");
            }
        }
    }
}
