using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Api.Data;
using Music.Api.Helpers;
using Music.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Music.Api.Controllers
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
            var songs = await (from _song in _dbContext.Songs
                       select new
                       {
                            ID = _song.Id,
                            Title = _song.Title,
                            Duration = _song.Duration,
                            ImageUrl = _song.ImageUrl,
                            AudioUrl = _song.AudioUrl
                       }).ToListAsync();

            return Ok(songs);
        }

        // GET: api/<SongController>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetFeaturedSongs()
        {
            var songs = await (from _song in _dbContext.Songs
                               where _song.IsFeatured == true
                               select new
                               {
                                   ID = _song.Id,
                                   Title = _song.Title,
                                   Duration = _song.Duration,
                                   ImageUrl = _song.ImageUrl,
                                   AudioUrl = _song.AudioUrl
                               }).ToListAsync();

            return Ok(songs);
        }

        // GET: api/<SongController>
        // return the newest 5 songs
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNewSongs()
        {
            var songs = await (from _song in _dbContext.Songs
                               orderby _song.UploadedDate descending
                               select new
                               {
                                   ID = _song.Id,
                                   Title = _song.Title,
                                   Duration = _song.Duration,
                                   ImageUrl = _song.ImageUrl,
                                   AudioUrl = _song.AudioUrl
                               }).Take(5).ToListAsync();

            return Ok(songs);
        }

        // GET: api/<SongController>
        // search for a song by name
        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(string query)
        {
            var songs = await (from _song in _dbContext.Songs
                               where _song.Title.StartsWith(query)
                               select new
                               {
                                   ID = _song.Id,
                                   Title = _song.Title,
                                   Duration = _song.Duration,
                                   ImageUrl = _song.ImageUrl,
                                   AudioUrl = _song.AudioUrl
                               }).ToListAsync();

            return Ok(songs);
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
        public async Task<IActionResult> Post([FromForm] Song song)
        {
            var audioUrl = await FileHelper.UploadFile(song.AudioFile, "audio-files");
            song.AudioUrl = audioUrl;
            var uploadTime = DateTime.Now;
            song.UploadedDate = uploadTime;
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
