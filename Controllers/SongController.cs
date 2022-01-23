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
    public class SongController : ControllerBase
    {
            private ApiDbContext _dbContext;
            public SongController(ApiDbContext dbcontext)
            {
                _dbContext = dbcontext;
            }

        // GET: api/<SongController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Songs.ToListAsync());
        }

        // GET api/<SongController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound("$No record found with id: {id}");
            }
            else
            {
                return Ok(song);
            }
        }

        // POST api/<SongController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songObject)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if (song==null)
            {
                return NotFound($"No record found with id: {id}");
            }
            else
            {
                song.Title = songObject.Title;
                song.Language = songObject.Language;
                await _dbContext.SaveChangesAsync();
                return Ok($"{songObject.Id} updated successfully");
            }
        }

        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);
            if (song==null)
            {
                return NotFound($"No record found with id: {id}");
            }
            else
            {
                _dbContext.Songs.Remove(song);
                await _dbContext.SaveChangesAsync();
                return Ok($"Record {id} deleted");
            }
        }
    }
}
