using System;
using Microsoft.AspNetCore.Mvc;
using VailInstructorWikiApi.DTOs;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Services;

namespace VailInstructorWikiApi.Controllers
{
    [ApiController]
    [Route("api/runs")]
    public class RunController : ControllerBase
    {
        private readonly RunService _runService;
        private readonly AreaService _areaService;

        public RunController(RunService runService, AreaService areaService)
        {
            _runService = runService;
            _areaService = areaService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Run>> GetRunById(int id)
        {
            var run = await _runService.GetRunById(id);

            if (run == null)
            {
                return NotFound();
            }

            return run;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Run>>> GetAllRuns()
        {
            var runs = await _runService.GetAllRuns();
            return runs.ToList();
        }

        [HttpGet("get-run-by-areaId/{areaId}/")]
        public async Task<ActionResult<IEnumerable<Run>>> GetRunsByAreaId(int areaId)
        {
            var runs = await _runService.GetRunsByAreaId(areaId);
            return runs.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Run>> CreateRun(RunDto runDto)
        {
            var area = await _areaService.GetAreaById(runDto.AreaId);

            if (area == null)
            {
                return BadRequest("Invalid AreaId. Area does not exist.");
            }
            var createdRun = await _runService.CreateRun(runDto);

            return CreatedAtAction(nameof(GetRunById), new { id = createdRun.Id }, createdRun);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRun(int id, RunDto updatedRun)
        {
            var area = await _areaService.GetAreaById(updatedRun.AreaId);

            if (area == null)
            {
                return BadRequest("Invalid AreaId. Area does not exist.");
            }

            var run = await _runService.UpdateRun(id, updatedRun);

            if (run == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRun(int id)
        {
            var success = await _runService.DeleteRun(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

