using Microsoft.AspNetCore.Mvc;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Services;

namespace VailInstructorWikiApi.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _areaService;

        public AreaController(AreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAllAreas()
        {
            var areas = await _areaService.GetAllAreas();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetAreaById(int id)
        {
            var area = await _areaService.GetAreaById(id);
            if (area == null)
            {
                return NotFound();
            }

            return Ok(area);
        }

        [HttpPost]
        public async Task<ActionResult<Area>> CreateArea(AreaDto areaDto)
        {
            var area = await _areaService.CreateArea(areaDto);
            return CreatedAtAction(nameof(GetAreaById), new { id = area.Id }, area);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Area>> UpdateArea(int id, AreaDto areaDto)
        {
            var updatedArea = await _areaService.UpdateArea(id, areaDto);
            if (updatedArea == null)
            {
                return NotFound();
            }

            return Ok(updatedArea);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var deleted = await _areaService.DeleteArea(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}