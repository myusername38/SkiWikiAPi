using System;
using Microsoft.AspNetCore.Mvc;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Services;

namespace VailInstructorWikiApi.Controllers
{
    [ApiController]
    [Route("api/levels")]
    public class LevelController : ControllerBase
    {
        private readonly LevelService _levelService;

        public LevelController(LevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Level>>> GetAllLevels()
        {
            var levels = await _levelService.GetAllLevelsAsync();
            return Ok(levels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Level>> GetLevelById(int id)
        {
            var level = await _levelService.GetLevelByIdAsync(id);
            if (level == null)
            {
                return NotFound();
            }

            return Ok(level);
        }

        [HttpPost]
        public async Task<ActionResult<Level>> CreateLevel(LevelDto levelDto)
        {
            var level = await _levelService.CreateLevelAsync(levelDto);
            return CreatedAtAction(nameof(GetLevelById), new { id = level.Id }, level);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Level>> UpdateLevel(int id, LevelDto levelDto)
        {
            var updatedLevel = await _levelService.UpdateLevelAsync(id, levelDto);
            if (updatedLevel == null)
            {
                return NotFound();
            }

            return Ok(updatedLevel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel(int id)
        {
            await _levelService.DeleteLevelAsync(id);
            return NoContent();
        }
    }
}
