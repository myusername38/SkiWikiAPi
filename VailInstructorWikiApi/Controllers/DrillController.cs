using System;
using Microsoft.AspNetCore.Mvc;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Services;

namespace VailInstructorWikiApi.Controllers
{
    [ApiController]
    [Route("api/drills")]
    public class DrillController : ControllerBase
    {
        private readonly DrillService _drillService;

        public DrillController(DrillService drillService)
        {
            _drillService = drillService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drill>>> GetAllDrills()
        {
            var drills = await _drillService.GetAllDrills();
            return Ok(drills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Drill>> GetDrillById(int id)
        {
            var drill = await _drillService.GetDrillById(id);
            if (drill == null)
            {
                return NotFound();
            }

            return Ok(drill);
        }

        [HttpPost]
        public async Task<ActionResult<Drill>> CreateDrill(DrillDto drillDto)
        {
            var drill = await _drillService.CreateDrill(drillDto);
            return CreatedAtAction(nameof(GetDrillById), new { id = drill.Id }, drill);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Drill>> UpdateDrill(int id, DrillDto drillDto)
        {
            var updatedDrill = await _drillService.UpdateDrill(id, drillDto);
            if (updatedDrill == null)
            {
                return NotFound();
            }

            return Ok(updatedDrill);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrill(int id)
        {
            var deleted = await _drillService.DeleteDrill(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

